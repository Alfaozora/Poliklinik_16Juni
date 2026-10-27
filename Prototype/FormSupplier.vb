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
Public Class FormSupplier

    Private Shared cachedSupplierData As DataTable = Nothing

    Private Sub FormSupplier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' 2. CEK CACHE: Jika data sudah ada di memori, langsung tampilkan tanpa tembak API!
        If cachedSupplierData IsNot Nothing AndAlso cachedSupplierData.Rows.Count > 0 Then
            TampilkanKeGrid(cachedSupplierData)
            Return
        End If

        ' Jika cache masih kosong (baru pertama kali buka aplikasi), ambil dari API
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        Try
            Dim baseUrl As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'ws_RM'")
            Dim apiUrl As String = baseUrl & "api/bridging/get-supplier"

            Dim responseString As String = GetDataAPIRM(apiUrl)
            Dim jsonResult As JObject = JObject.Parse(responseString)

            If jsonResult("code") IsNot Nothing AndAlso jsonResult("code").ToString() = "200" Then
                Dim dt As New DataTable()
                dt.Columns.Add("Nama Supplier", GetType(String))
                dt.Columns.Add("Alamat", GetType(String))
                dt.Columns.Add("Bank", GetType(String))
                dt.Columns.Add("No. Rekening", GetType(String))
                dt.Columns.Add("Atas Nama Rekening", GetType(String))
                dt.Columns.Add("Metode Pembayaran", GetType(String))
                dt.Columns.Add("Kode Bank", GetType(String))


                Dim dataItems = jsonResult("data")

                If dataItems IsNot Nothing AndAlso dataItems.Count > 0 Then
                    For Each item In dataItems
                        Dim supName As String = If(item("supplier_name").Type <> JTokenType.Null, item("supplier_name").ToString(), "-")
                        Dim supAddr As String = If(item("supplier_address").Type <> JTokenType.Null, item("supplier_address").ToString(), "-")

                        Dim rekDetails = item("supplier_rekening")
                        If rekDetails IsNot Nothing AndAlso rekDetails.Count > 0 Then
                            For Each rek In rekDetails
                                Dim bankName As String = If(rek("bank_short_name").Type <> JTokenType.Null, rek("bank_short_name").ToString(), "-")
                                Dim rekNo As String = If(rek("supplier_rek_no").Type <> JTokenType.Null, rek("supplier_rek_no").ToString(), "-")
                                Dim rekName As String = If(rek("supplier_rek_name").Type <> JTokenType.Null, rek("supplier_rek_name").ToString(), "-")
                                Dim payMethod As String = If(rek("supplier_payment_method").Type <> JTokenType.Null, rek("supplier_payment_method").ToString(), "-")
                                Dim bankCode As String = If(rek("supplier_bank").Type <> JTokenType.Null, rek("supplier_bank").ToString(), "-")

                                dt.Rows.Add(supName, supAddr, bankName, rekNo, rekName, payMethod, bankCode)
                            Next
                        Else
                            dt.Rows.Add(supName, supAddr, "-", "-", "-", "-", "-")
                        End If
                    Next
                Else
                    MessageBox.Show("Data supplier kosong dari server.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                ' 3. SIMPAN KE CACHE: Masukkan hasil tabel yang baru dibuat ke variabel memori
                cachedSupplierData = dt

                ' 4. Tampilkan ke DataGridView
                TampilkanKeGrid(cachedSupplierData)

            Else
                Dim errorMsg As String = If(jsonResult("message") IsNot Nothing, jsonResult("message").ToString(), "Gagal mengambil data dari server.")
                MessageBox.Show(errorMsg, "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan sistem (Gagal memproses JSON/Koneksi): " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ' Subroutine terpisah untuk mengatur DataGridView agar kode lebih bersih
    Private Sub TampilkanKeGrid(ByVal dataTabel As DataTable)
        DataGridView1.DataSource = dataTabel
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.ReadOnly = True
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub

    Private Sub TextBoxCariSupplier_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxCariSupplier.TextChanged
        If DataGridView1.DataSource IsNot Nothing Then

            ' Ambil keyword yang diketik
            Dim keyword As String = TextBoxCariSupplier.Text.Trim()

            ' Ubah DataSource kembali menjadi DataTable untuk mengakses fitur filternya
            Dim dt As DataTable = CType(DataGridView1.DataSource, DataTable)
            Dim dv As DataView = dt.DefaultView

            ' Jika kotak teks kosong, tampilkan semua data
            If keyword = "" Then
                dv.RowFilter = ""
            Else
                ' Mencegah error jika user mengetik tanda kutip satu (')
                Dim safeKeyword As String = keyword.Replace("'", "''")

                ' Logika Pencarian: Mencari di kolom "Nama Supplier", "Atas Nama Rekening", atau "Bank"
                ' Tanda % berfungsi sebagai wildcard (mencari kata di mana saja)
                dv.RowFilter = String.Format("[Nama Supplier] LIKE '%{0}%' OR [Atas Nama Rekening] LIKE '%{0}%' OR [Bank] LIKE '%{0}%'", safeKeyword)
            End If

        End If
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        ' 1. Pastikan area yang di-klik adalah baris data yang valid (bukan header tabel)
        If e.RowIndex >= 0 Then
            ' 2. Ambil baris yang sedang di-klik
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            ' 3. Ambil nilai dari sel berdasarkan nama kolom yang kita buat di DataTable
            Dim namaSupplier As String = row.Cells("Nama Supplier").Value.ToString()
            Dim alamat As String = row.Cells("Alamat").Value.ToString()
            Dim bank As String = row.Cells("Bank").Value.ToString()
            Dim noRek As String = row.Cells("No. Rekening").Value.ToString()
            Dim namaRek As String = row.Cells("Atas Nama Rekening").Value.ToString()
            Dim metodeBayar As String = row.Cells("Metode Pembayaran").Value.ToString()
            Dim bankCode As String = row.Cells("Kode Bank").Value.ToString()

            ' 4. Lempar nilai tersebut ke form FormPreOrderAlat
            FormPreOrderAlat.TextBoxNamaSupp.Text = namaSupplier
            FormPreOrderAlat.TextBoxAlamatSupp.Text = alamat
            FormPreOrderAlat.TextBoxBank.Text = bank
            FormPreOrderAlat.TextBoxNoRek.Text = noRek
            FormPreOrderAlat.TextBoxNamaRek.Text = namaRek
            FormPreOrderAlat.TextBoxPay.Text = metodeBayar
            FormPreOrderAlat.TextBoxBankCode.Text = bankCode

            ' 5. Tutup form pencarian supplier ini
            Me.Close()
        End If
    End Sub
End Class