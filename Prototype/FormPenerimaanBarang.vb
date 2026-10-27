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
Public Class FormPenerimaanBarang

    Private userInput As String = PoliKlinik.Lbluser.Text
    Private dtCartridge As DataTable

    Private Sub LoadGridTempApiKuota()

        Dim sql As String = _
             "SELECT " & _
             "TempID, " & _
             "IsSelected, " & _
             "KodeAset, " & _
             "NamaAset, " & _
             "Jumlah, " & _
             "Trmerm, " & _
             "CASE WHEN AlatID IS NULL THEN 'TIDAK DITEMUKAN' ELSE 'COCOK' END AS StatusMapping, " & _
             "CASE WHEN AlatID IS NULL THEN 0 ELSE 1 END AS IsMapped " & _
             "FROM TempApiKuotaAlat " & _
             "WHERE CreatedBy = '" & userInput & "' " & _
             "AND IsProcessed = 0"

        Dim dt As DataTable = ReadSQL(sql)

        DataGridViewBarang.DataSource = dt

        FormatGridTempApiKuota()

    End Sub

    Private Sub FormatGridTempApiKuota()
        With DataGridViewBarang
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With

        ' 1. Pengaturan Kolom Checkbox
        With DataGridViewBarang.Columns("IsSelected")
            .HeaderText = "Pilih"
            .ReadOnly = False
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With

        ' 2. Pengaturan Kolom Data menjadi ReadOnly
        DataGridViewBarang.Columns("ItemCode").ReadOnly = True
        DataGridViewBarang.Columns("ItemName").ReadOnly = True
        DataGridViewBarang.Columns("RoomName").ReadOnly = True
        DataGridViewBarang.Columns("Qty").ReadOnly = False
        DataGridViewBarang.Columns("Satuan").ReadOnly = True

        ' 3. Pengaturan Alignment / Perataan Teks
        DataGridViewBarang.Columns("Qty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewBarang.Columns("Satuan").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        ' Tempatkan penguncian kolom di baris paling akhir
        DataGridViewBarang.AutoGenerateColumns = False
    End Sub

    Private Sub DataGridViewBarang_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) _
    Handles DataGridViewBarang.CellFormatting

        If DataGridViewBarang.Columns(e.ColumnIndex).Name = "StatusMapping" Then

            Dim status As String = e.Value.ToString().ToUpper()

            If status = "TIDAK DITEMUKAN" Then
                e.CellStyle.BackColor = Color.MistyRose
                e.CellStyle.ForeColor = Color.DarkRed
            Else
                e.CellStyle.BackColor = Color.Honeydew
                e.CellStyle.ForeColor = Color.DarkGreen
            End If

        End If

    End Sub


    'Private Sub DataGridViewBarang_CellBeginEdit(ByVal sender As Object, ByVal e As DataGridViewCellCancelEventArgs) _
    'Handles DataGridViewBarang.CellBeginEdit

    '    If DataGridViewBarang.Columns(e.ColumnIndex).Name = "IsSelected" Then

    '        Dim isMapped As Boolean = CBool(DataGridViewBarang.Rows(e.RowIndex).Cells("IsMapped").Value)

    '        If Not isMapped Then
    '            e.Cancel = True
    '            Beep()
    '        End If

    '    End If

    'End Sub

    Private Sub DataGridViewBarang_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewBarang.CurrentCellDirtyStateChanged
        ' Jika yang diklik adalah kolom checkbox, paksa grid untuk langsung menyimpan perubahan nilai (Commit)
        If TypeOf DataGridViewBarang.CurrentCell Is DataGridViewCheckBoxCell Then
            DataGridViewBarang.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub DataGridViewBarang_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) _
    Handles DataGridViewBarang.DataError

        e.ThrowException = False
    End Sub

    Private Sub DataGridViewBarang_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridViewBarang.CellValueChanged
        ' Pastikan baris data valid dan perubahan terjadi di kolom Checkbox "IsSelected"
        If e.RowIndex >= 0 AndAlso DataGridViewBarang.Columns(e.ColumnIndex).Name = "IsSelected" Then
            Dim totalStokDipilih As Integer = 0

            ' Lakukan looping untuk memeriksa setiap baris di DataGridViewBarang
            For Each row As DataGridViewRow In DataGridViewBarang.Rows
                ' Ambil status centang (Boolean)
                Dim isChecked As Boolean = Convert.ToBoolean(row.Cells("IsSelected").Value)

                If isChecked Then
                    ' Jika dicentang, ambil nilai dari kolom Qty dan jumlahkan
                    Dim qty As Integer = Convert.ToInt32(row.Cells("Qty").Value)
                    totalStokDipilih += qty
                End If
            Next

            ' Tampilkan hasil penjumlahan ke LabelDipilih
            LabelDipilih.Text = totalStokDipilih.ToString()
        End If
    End Sub

    'Private Sub DataGridViewBarang_CellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) _
    'Handles DataGridViewBarang.CellValueChanged

    '    If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub

    '    ' 👉 GANTI DI SINI
    '    If DataGridViewBarang.Columns(e.ColumnIndex).Name <> "IsSelected" Then Exit Sub

    '    ' Ambil TempID
    '    Dim cellTempID = DataGridViewBarang.Rows(e.RowIndex).Cells("TempID").Value
    '    If cellTempID Is Nothing OrElse IsDBNull(cellTempID) Then Exit Sub

    '    Dim tempID As Integer
    '    If Not Integer.TryParse(cellTempID.ToString(), tempID) Then Exit Sub

    '    ' Ambil nilai checkbox
    '    Dim isChecked As Boolean = False
    '    Dim cellValue = DataGridViewBarang.Rows(e.RowIndex).Cells("IsSelected").Value
    '    If cellValue IsNot Nothing AndAlso Not IsDBNull(cellValue) Then
    '        Boolean.TryParse(cellValue.ToString(), isChecked)
    '    End If

    '    ' Update DB
    '    WriteSQL( _
    '        "UPDATE TempApiKuotaAlat SET IsSelected = " & _
    '        If(isChecked, 1, 0) & _
    '        " WHERE TempID = " & tempID, 4)

    '    ' Update total
    '    HitungTotalPilihan()

    'End Sub

    Private Sub HitungTotalPilihan()

        Dim totalDipilih As Integer = 0
        Dim totalKuota As Integer = 0

        For Each row As DataGridViewRow In DataGridViewBarang.Rows

            If row.IsNewRow Then Continue For

            Dim isChecked As Boolean = False
            If row.Cells("IsSelected").Value IsNot Nothing Then
                Boolean.TryParse(row.Cells("IsSelected").Value.ToString(), isChecked)
            End If

            If isChecked Then
                totalDipilih += 1

                Dim jumlah As Integer = 0
                Integer.TryParse(row.Cells("Jumlah").Value.ToString(), jumlah)
                totalKuota += jumlah
            End If

        Next

        LabelDipilih.Text = totalDipilih.ToString()
        LabelSaldoAlat.Text = totalKuota.ToString()

        LabelSaldoAlat.ForeColor = If(totalKuota > 0, Color.Green, Color.Black)

    End Sub

    Private Sub ButtonSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSubmit.Click
        If CInt(LabelSaldoAlat.Text) = 0 Then
            MsgBox("Tidak ada alat yang dipilih.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If Not CheckPersetujuan.Checked Then
            MsgBox("Silakan centang persetujuan terlebih dahulu.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim dtInvalid As DataTable = ReadSQL( _
            "SELECT 1 FROM TempApiKuotaAlat " & _
            "WHERE IsSelected = 1 " & _
            "AND AlatID IS NULL " & _
            "AND CreatedBy = '" & userInput & "'")

        If dtInvalid.Rows.Count > 0 Then
            MsgBox("Terdapat alat yang belum terdaftar di master." & vbCrLf & _
               "Silakan periksa kembali data.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim dt As DataTable = ReadSQL( _
           "SELECT TempID, AlatID, Jumlah, KodeAset, NamaAset " & _
           "FROM TempApiKuotaAlat " & _
           "WHERE IsSelected = 1 " & _
           "AND IsProcessed = 0 " & _
           "AND CreatedBy = '" & userInput & "'")

        If dt.Rows.Count = 0 Then
            MsgBox("Data tidak valid atau sudah diproses.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim dbTran As New DBTransaction

        Try
            dbTran.BeginTransaction()

            For Each row As DataRow In dt.Rows
                Dim tempID As Integer = CInt(row("TempID"))
                Dim alatID As Integer = CInt(row("AlatID"))
                Dim jumlah As Integer = CInt(row("Jumlah"))
                Dim kodeAset As String = row("KodeAset").ToString()
                Dim namaAset As String = row("NamaAset").ToString()

                'Log top up
                dbTran.Execute( _
                    "INSERT INTO LogTopUpAlat " & _
                    "(AlatID, JumlahTopUp, JenisTopUp, Keterangan, CreatedBy) VALUES (" & _
                    alatID & "," & jumlah & ",'API'," & _
                    "'Top up dari API - " & kodeAset & " (" & namaAset.Replace("'", "''") & ")'," & _
                    "'" & userInput & "')")

                'Update kuota total
                dbTran.Execute( _
                    "UPDATE MasterAlat SET " & _
                    "KuotaTotal = KuotaTotal + " & jumlah & " " & _
                    "WHERE AlatID = " & alatID)

                'Tandai staging sudah diproses
                dbTran.Execute( _
                    "UPDATE TempApiKuotaAlat SET " & _
                    "IsProcessed = 1 " & _
                    "WHERE TempID = " & tempID)

            Next

            dbTran.Commit()

            Dim dtTrmerm As DataTable = ReadSQL( _
                "SELECT DISTINCT Trmerm " & _
                "FROM TempApiKuotaAlat " & _
                "WHERE IsSelected = 1 " & _
                "AND CreatedBy = '" & userInput & "'")

            For Each r As DataRow In dtTrmerm.Rows

                Dim trmerm As String = r("Trmerm").ToString()
                If trmerm = "" Then Continue For

                Try
                    Dim baseURL As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'ws_RM'")

                    If Not baseURL.EndsWith("/") Then baseURL &= "/"

                    Dim url As String = baseURL & "api/lumiera-luzelle/approve-kuota"

                    Dim jsonParam As String = "{""trmerm"":""" & trmerm & """}"

                    Dim client As New WebClient()
                    client.Headers.Add("Content-Type", "application/json")
                    client.Headers.Add("X-API-KEY", "M1tR4M3D1c@rECaH4Y42025")

                    Dim response As String = client.UploadString(url, "POST", jsonParam)

                    WriteSQL( _
                            "UPDATE TempApiKuotaAlat SET IsApprovedGudang = 1 " & _
                            "WHERE Trmerm = '" & trmerm & "'", 4)

                    ' Optional: cek respon
                    If Not response.ToLower().Contains("success") Then
                        MsgBox("Approve gudang gagal untuk ID: " & trmerm, MsgBoxStyle.Exclamation)
                    End If

                Catch ex As Exception
                    MsgBox("Gagal mengirim approve ke gudang (" & trmerm & "): " & ex.Message, MsgBoxStyle.Exclamation)
                End Try

            Next


            MsgBox("Top up kuota berhasil diproses.", MsgBoxStyle.Information)

            '   Refresh UI
            LoadGridTempApiKuota()
            HitungTotalPilihan()
            CheckPersetujuan.Checked = False

        Catch ex As Exception
            dbTran.Rollback()
            MsgBox("Gagal memproses top up kuota: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub LoadComboCartridge()
        Try
            Dim sql As String = _
                "SELECT MC.NamaCartridge, MC.KodeCartridge, MA.IdRuangan " & _
                "FROM MasterAlat MA " & _
                "INNER JOIN MasterCartridge MC ON MA.AlatID = MC.AlatID"

            ' Gunakan fungsi ReadSQL milikmu untuk mengambil data ke DataTable
            dtCartridge = ReadSQL(sql)

            If dtCartridge IsNot Nothing AndAlso dtCartridge.Rows.Count > 0 Then
                ' Putus hubungan event sementara agar tidak memicu error saat binding data
                RemoveHandler ComboBoxNamaAlat.SelectedIndexChanged, AddressOf ComboBoxNamaAlat_SelectedIndexChanged

                ' Proses Binding Data ke ComboBox
                ComboBoxNamaAlat.DataSource = dtCartridge
                ComboBoxNamaAlat.DisplayMember = "NamaCartridge" ' Yang tampil di layar (Nama)
                ComboBoxNamaAlat.ValueMember = "KodeCartridge"   ' Value di balik layar (Kode)

                ' Hubungkan kembali event setelah binding selesai
                AddHandler ComboBoxNamaAlat.SelectedIndexChanged, AddressOf ComboBoxNamaAlat_SelectedIndexChanged

                ' Set TextBox pertama kali berdasarkan item index 0
                UpdateIdRuanganTextBox()
            Else
                ComboBoxNamaAlat.DataSource = Nothing
                TextBoxIdRuangan.Text = ""
            End If

        Catch ex As Exception
            MsgBox("Gagal meload data cartridge: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub UpdateIdRuanganTextBox()
        ' Pastikan ComboBox ada isinya dan datanya valid
        If ComboBoxNamaAlat.SelectedIndex <> -1 AndAlso dtCartridge IsNot Nothing Then
            ' Ambil baris data yang sesuai dengan index yang dipilih pada ComboBox
            Dim selectedIndex As Integer = ComboBoxNamaAlat.SelectedIndex

            If selectedIndex < dtCartridge.Rows.Count Then
                Dim row As DataRow = dtCartridge.Rows(selectedIndex)
                TextBoxIdRuangan.Text = row("IdRuangan").ToString()
            End If
        End If
    End Sub

    Private Sub ComboBoxNamaAlat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxNamaAlat.SelectedIndexChanged
        UpdateIdRuanganTextBox()
    End Sub

    Private Sub FormPenerimaanBarang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadComboCartridge()
    End Sub

    Private Sub ButtonAmbilData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAmbilData.Click
        If ComboBoxNamaAlat.SelectedValue Is Nothing Then
            MsgBox("Silakan pilih Cartridge terlebih dahulu!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If String.IsNullOrEmpty(TextBoxIdRuangan.Text) Then
            MsgBox("ID Ruangan tidak boleh kosong!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Try
            Dim itemCode As String = ComboBoxNamaAlat.SelectedValue.ToString()
            Dim roomId As String = TextBoxIdRuangan.Text.Trim()
            Dim baseUrl As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'ws_RM'")

            ' FIX: Menghapus spasi setelah tanda '=' pada room_id
            Dim apiUrl As String = baseUrl & "api/bridging/stock-item?item_code=" & itemCode & "&room_id=" & roomId

            Dim apiResponse As String = GetDataAPIRM(apiUrl)
            Dim jsonResult As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.Linq.JObject.Parse(apiResponse)

            If jsonResult("error") IsNot Nothing AndAlso jsonResult("error").ToString().ToLower() = "false" Then
                Dim dataObj As Newtonsoft.Json.Linq.JObject = jsonResult("data")

                ' Bersihkan kolom lama terlebih dahulu jika ingin memastikan pembuatan ulang bersih
                DataGridViewBarang.Columns.Clear()

                ' 1. Tambah Kolom Checkbox
                Dim chkCol As New DataGridViewCheckBoxColumn()
                chkCol.Name = "IsSelected"
                chkCol.HeaderText = "Pilih"
                DataGridViewBarang.Columns.Add(chkCol)

                ' 2. Tambah Kolom Data Biasa
                DataGridViewBarang.Columns.Add("ItemCode", "Kode Item")
                DataGridViewBarang.Columns.Add("ItemName", "Nama Item")
                DataGridViewBarang.Columns.Add("RoomName", "Nama Ruangan")
                DataGridViewBarang.Columns.Add("Qty", "Stok")
                DataGridViewBarang.Columns.Add("Satuan", "Satuan")

                ' Panggil format grid SETELAH kolom dipastikan sukses dibuat
                FormatGridTempApiKuota()

                ' Bersihkan data baris lama
                DataGridViewBarang.Rows.Clear()

                ' Masukkan data ke baris baru
                Dim n As Integer = DataGridViewBarang.Rows.Add()

                DataGridViewBarang.Rows(n).Cells("IsSelected").Value = True
                DataGridViewBarang.Rows(n).Cells("ItemCode").Value = dataObj("item_code").ToString()
                DataGridViewBarang.Rows(n).Cells("ItemName").Value = dataObj("item_name").ToString()
                DataGridViewBarang.Rows(n).Cells("RoomName").Value = dataObj("room_name").ToString()
                DataGridViewBarang.Rows(n).Cells("Qty").Value = Convert.ToInt32(dataObj("qty"))
                DataGridViewBarang.Rows(n).Cells("Satuan").Value = "Shoot"

                LabelDipilih.Text = dataObj("qty").ToString()

                MsgBox("Data stock berhasil ditampilkan di tabel!", MsgBoxStyle.Information)
            Else
                Dim msg As String = If(jsonResult("message") IsNot Nothing, jsonResult("message").ToString(), "Gagal mengambil data.")
                MsgBox("Pesan API: " & msg, MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            MsgBox("Terjadi kesalahan saat memuat ke tabel: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
End Class