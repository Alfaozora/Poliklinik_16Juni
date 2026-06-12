Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Linq
Imports Newtonsoft.Json
Imports Newtonsoft
Imports System.Security.Cryptography
Imports System.Collections.Generic
Imports Newtonsoft.Json.Linq
Imports System.Drawing.Imaging
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Drawing
Imports PoliKlinik_Net2.ShapeDrawingFunctions
Imports System.Text
Imports System.Globalization
Imports System.Net
Public Class FormPencarianAlatSarpras

    Private Sub TextBoxCariAlat_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles TextBoxCariAlat.KeyDown
        ' Cek apakah tombol yang ditekan adalah tombol Enter
        If e.KeyCode = Keys.Enter Then
            ' Menghilangkan bunyi "ding" (alert) bawaan Windows saat menekan Enter di TextBox
            e.SuppressKeyPress = True

            ' Memanggil event klik pada tombol secara terprogram
            ButtonCariAlat.PerformClick()
        End If
    End Sub

    Private Sub TextBoxCariAlat_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TextBoxCariAlat.KeyPress
        ' Cek apakah karakter yang masuk adalah Enter (ASCII 13)
        If e.KeyChar = ChrW(Keys.Enter) Then

            ' Ini adalah kunci utamanya: Mencegat karakter Enter agar tidak diketikkan ke TextBox
            e.Handled = True

            ' Jalankan pencarian
            ButtonCariAlat.PerformClick()

        End If
    End Sub

    Private Sub ButtonCariAlat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCariAlat.Click
        Dim keyword As String = TextBoxCariAlat.Text.Trim()

        If keyword = "" Then
            MessageBox.Show("Silakan masukkan nama alat yang ingin dicari!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim baseUrl As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'ws_RM'")

        ' PERBAIKAN 1: Tambahkan garis miring (/) agar URL tidak rusak
        Dim apiUrl As String = baseUrl & "api/bridging/get-asset?item_name=" & Uri.EscapeDataString(keyword)

        ' Ubah tampilan tombol saat loading
        ButtonCariAlat.Enabled = False
        ButtonCariAlat.Text = "Mencari..."
        Application.DoEvents() ' Mencegah UI freeze sejenak saat merender tombol

        Try
            Dim responseString As String = GetDataAPIRM(apiUrl)
            Dim jsonResult As JObject = JObject.Parse(responseString)

            ' PERBAIKAN 2: Blok If ini terhapus di kode Anda sebelumnya
            If jsonResult("status") IsNot Nothing AndAlso jsonResult("status").ToString() = "success" Then

                ' 1. Siapkan kerangka tabel (DataTable)
                Dim dt As New DataTable()
                dt.Columns.Add("Kode Item", GetType(String))
                dt.Columns.Add("Nama Master Alat", GetType(String))
                dt.Columns.Add("Kode Aset", GetType(String))
                dt.Columns.Add("Merk", GetType(String))
                dt.Columns.Add("Tipe", GetType(String))
                dt.Columns.Add("Model", GetType(String))
                dt.Columns.Add("Serial Number", GetType(String))
                dt.Columns.Add("Distributor", GetType(String))

                ' 2. Ambil array 'data' terluar
                Dim dataItems = jsonResult("data")

                If dataItems IsNot Nothing Then
                    ' 3. Looping untuk setiap Master Item
                    For Each item In dataItems
                        Dim itemCode As String = item("item_code").ToString()
                        Dim itemName As String = item("item_name").ToString()

                        ' Pengecekan aman untuk melihat apakah 'asset_detail' dan 'data'-nya ada
                        If item("asset_detail") IsNot Nothing AndAlso item("asset_detail")("data") IsNot Nothing Then
                            Dim assetDetails = item("asset_detail")("data")

                            ' 4. Cek apakah array data aset fisiknya ada isinya (> 0)
                            If assetDetails.Count > 0 Then
                                ' Looping untuk mengekstrak aset fisiknya (BTL Exion & BTL EMSELLA masuk ke sini)
                                For Each asset In assetDetails
                                    Dim assetCode As String = If(asset("code").Type <> JTokenType.Null, asset("code").ToString(), "-")
                                    Dim assetMerk As String = If(asset("merk").Type <> JTokenType.Null, asset("merk").ToString(), "-")
                                    Dim assetType As String = If(asset("type").Type <> JTokenType.Null, asset("type").ToString(), "-")
                                    Dim assetModel As String = If(asset("model").Type <> JTokenType.Null, asset("model").ToString(), "-")
                                    Dim assetSn As String = If(asset("serial_number").Type <> JTokenType.Null, asset("serial_number").ToString(), "-")
                                    Dim assetDist As String = If(asset("distributor").Type <> JTokenType.Null, asset("distributor").ToString(), "-")

                                    ' Masukkan satu baris lengkap ke DataTable
                                    dt.Rows.Add(itemCode, itemName, assetCode, assetMerk, assetType, assetModel, assetSn, assetDist)
                                Next

                            Else
                                ' PERBAIKAN: Masukkan alat ke Grid meskipun fisik asetnya kosong (Cartridge masuk ke sini)
                                ' Kita isi kolom sisanya dengan tanda strip "-"
                                dt.Rows.Add(itemCode, itemName, "-", "-", "-", "-", "-", "-")
                            End If
                        End If
                    Next
                End If

                ' 5. Tempelkan DataTable ke DataGridView
                DataGridViewAlatSarpras.DataSource = dt

                ' 6. Rapikan ukuran kolom agar menyesuaikan isi teks
                DataGridViewAlatSarpras.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

                If dt.Rows.Count = 0 Then
                    MessageBox.Show("Barang ditemukan di Master Item, namun tidak ada fisik aset yang tercatat di lokasi LLMR.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Else ' Pasangan Else ini sekarang sudah memiliki If-nya di atas
                Dim errorMsg As String = If(jsonResult("message") IsNot Nothing, jsonResult("message").ToString(), "Terjadi kesalahan tidak dikenal.")
                MessageBox.Show(errorMsg, "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan sistem (Gagal memproses JSON/Koneksi): " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ButtonCariAlat.Enabled = True
            ButtonCariAlat.Text = "Cari Alat"
        End Try
    End Sub

    Private Sub DataGridViewAlatSarpras_CellDoubleClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles DataGridViewAlatSarpras.CellDoubleClick
        ' 1. Pastikan area yang di-klik adalah baris data yang valid (bukan header tabel)
        If e.RowIndex >= 0 Then
            ' 2. Ambil baris yang sedang di-klik
            Dim row As DataGridViewRow = DataGridViewAlatSarpras.Rows(e.RowIndex)

            ' 3. Ambil semua nilai sel yang berpotensi dibutuhkan
            ' Pastikan nama kolom sesuai dengan yang Anda buat di DataTable sebelumnya ("Kode Aset (LLMR)" atau "Kode Aset")
            Dim kodeItem As String = row.Cells("Kode Item").Value.ToString()
            Dim kodeAset As String = row.Cells("Kode Aset").Value.ToString()
            Dim namaAlat As String = row.Cells("Nama Master Alat").Value.ToString()

            ' 4. Cek Tab mana yang sedang aktif di form utama
            Dim tabAktif As String = MasterTindakanKlinik.TabControl1.SelectedTab.Name

            ' 5. Tentukan logika berdasarkan Tab yang aktif
            If tabAktif = "TabPage1" Then
                ' --- LOGIKA UNTUK ALAT (TabPage1) ---

                ' Cegah user memilih barang yang belum punya fisik aset
                If kodeAset = "-" Then
                    MessageBox.Show("Alat ini tercatat di Master Item, tetapi belum ada fisik asetnya (Kode Aset kosong). Silakan pilih yang lain.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                ' Lempar nilai Kode Aset ke form
                MasterTindakanKlinik.TextBoxKodeAlat.Text = kodeAset
                MasterTindakanKlinik.TextBoxNamaAlat.Text = namaAlat

            ElseIf tabAktif = "TabPage2" Then
                ' --- LOGIKA UNTUK CARTRIDGE (TabPage2) ---

                ' Tidak ada pengecekan kodeAset = "-" di sini, jadi yang kosong pun lolos

                ' Lempar nilai Kode Item (bukan Kode Aset) ke form
                MasterTindakanKlinik.TextBoxKodeCartridge.Text = kodeItem
                MasterTindakanKlinik.TextBoxNamaCartridge.Text = namaAlat

            End If

            ' 6. Tutup form pencarian ini (kembali ke form MasterTindakanKlinik)
            Me.Close()
        End If
    End Sub
End Class