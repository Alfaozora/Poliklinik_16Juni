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

    Private Sub ButtonAmbilData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAmbilData.Click
        Try

            Dim baseURL As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'ws_RM'")

            If Not baseURL.EndsWith("/") Then baseURL &= "/"

            Dim url As String = baseURL & "api/lumiera-luzelle/get-kuota"

            Dim client As New WebClient()
            client.Headers.Add("X-API-KEY", "M1tR4M3D1c@rECaH4Y42025")

            Dim response As String = client.DownloadString(url)
            Dim json As JObject = JObject.Parse(response)

            Dim dataArray As JArray = CType(json("data"), JArray)

            If dataArray Is Nothing OrElse dataArray.Count = 0 Then
                MsgBox("Data kuota dari API kosong.", MsgBoxStyle.Information)
                Exit Sub
            End If

            WriteSQL("DELETE FROM TempApiKuotaAlat WHERE CreatedBy = '" & userInput & "'", 4)

            For Each item As JObject In dataArray

                Dim trmerm As String = item("id").ToString()
                Dim kodeAset As String = item("kode_aset").ToString()
                Dim namaAset As String = item("nama_aset").ToString()
                Dim jumlah As Integer = CInt(item("jumlah"))

                ' Mapping ke MasterAlat
                Dim alatIDObj As Object = ReadSQLNIK("SELECT AlatID FROM MasterAlat WHERE KodeAlat = '" & kodeAset & "'")

                Dim alatIDValue As String = "NULL"

                If alatIDObj IsNot Nothing _
                   AndAlso alatIDObj.ToString().Trim() <> "" _
                   AndAlso IsNumeric(alatIDObj.ToString()) Then

                    alatIDValue = alatIDObj.ToString()

                End If

                'Debug.Print( _
                '    "INSERT INTO TempApiKuotaAlat (KodeAset, NamaAset, Jumlah, AlatID, CreatedBy) VALUES (" & _
                '    "'" & kodeAset & "','" & namaAset & "'," & jumlah & "," & alatIDValue & ",'" & userInput & "')")


                ' Insert ke staging
                WriteSQL( _
                    "INSERT INTO TempApiKuotaAlat " & _
                    "(KodeAset, NamaAset, Jumlah, AlatID, CreatedBy, Trmerm) VALUES (" & _
                    "'" & kodeAset & "'," & _
                    "'" & namaAset.Replace("'", "''") & "'," & _
                    jumlah & "," & _
                    alatIDValue & "," & _
                    "'" & userInput & "'," & _
                    "'" & trmerm & "'" & _
                    ")", 4)

            Next

            LoadGridTempApiKuota()

            MsgBox("Data kuota dari API berhasil dimuat.", MsgBoxStyle.Information)
        Catch ex As WebException
            MsgBox("Gagal mengambil data API: " & ex.Message, MsgBoxStyle.Critical)
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

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
            .Dock = DockStyle.Fill
            .AutoGenerateColumns = True
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With

        ' Checkbox → BOLEH DIPILIH
        With DataGridViewBarang.Columns("IsSelected")
            .HeaderText = "Pilih"
            .ReadOnly = False
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With

        ' Kolom lain → READ ONLY
        DataGridViewBarang.Columns("TempID").Visible = False
        DataGridViewBarang.Columns("IsSelected").ReadOnly = False
        DataGridViewBarang.Columns("KodeAset").ReadOnly = True
        DataGridViewBarang.Columns("NamaAset").ReadOnly = True
        DataGridViewBarang.Columns("Jumlah").ReadOnly = True
        DataGridViewBarang.Columns("StatusMapping").ReadOnly = True
        DataGridViewBarang.Columns("Trmerm").Visible = True

        ' Alignment
        DataGridViewBarang.Columns("Jumlah").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DataGridViewBarang.Columns("StatusMapping").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

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


    Private Sub DataGridViewBarang_CellBeginEdit(ByVal sender As Object, ByVal e As DataGridViewCellCancelEventArgs) _
    Handles DataGridViewBarang.CellBeginEdit

        If DataGridViewBarang.Columns(e.ColumnIndex).Name = "IsSelected" Then

            Dim isMapped As Boolean = CBool(DataGridViewBarang.Rows(e.RowIndex).Cells("IsMapped").Value)

            If Not isMapped Then
                e.Cancel = True
                Beep()
            End If

        End If

    End Sub

    Private Sub DataGridViewBarang_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs) _
    Handles DataGridViewBarang.CurrentCellDirtyStateChanged

        If DataGridViewBarang.IsCurrentCellDirty Then
            DataGridViewBarang.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If

    End Sub

    Private Sub DataGridViewBarang_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) _
    Handles DataGridViewBarang.DataError

        e.ThrowException = False
    End Sub

    Private Sub DataGridViewBarang_CellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) _
    Handles DataGridViewBarang.CellValueChanged

        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub

        ' 👉 GANTI DI SINI
        If DataGridViewBarang.Columns(e.ColumnIndex).Name <> "IsSelected" Then Exit Sub

        ' Ambil TempID
        Dim cellTempID = DataGridViewBarang.Rows(e.RowIndex).Cells("TempID").Value
        If cellTempID Is Nothing OrElse IsDBNull(cellTempID) Then Exit Sub

        Dim tempID As Integer
        If Not Integer.TryParse(cellTempID.ToString(), tempID) Then Exit Sub

        ' Ambil nilai checkbox
        Dim isChecked As Boolean = False
        Dim cellValue = DataGridViewBarang.Rows(e.RowIndex).Cells("IsSelected").Value
        If cellValue IsNot Nothing AndAlso Not IsDBNull(cellValue) Then
            Boolean.TryParse(cellValue.ToString(), isChecked)
        End If

        ' Update DB
        WriteSQL( _
            "UPDATE TempApiKuotaAlat SET IsSelected = " & _
            If(isChecked, 1, 0) & _
            " WHERE TempID = " & tempID, 4)

        ' Update total
        HitungTotalPilihan()

    End Sub

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
        LabelKuota.Text = totalKuota.ToString()

        LabelKuota.ForeColor = If(totalKuota > 0, Color.Green, Color.Black)

    End Sub

    Private Sub ButtonSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSubmit.Click
        If CInt(LabelKuota.Text) = 0 Then
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
End Class