Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Drawing

Imports Excel = Microsoft.Office.Interop.Excel
Imports Microsoft.Office
Imports Microsoft.Office.Interop
Imports System.Text

Imports Newtonsoft.Json
Imports System.Data.SqlClient
Imports System.Net

Public Class FormSynchronizationPanBrothers
    Dim thread As System.Threading.Thread

    ' Set Connection
    Private DB_Keuangan As TLcon
    Private DB_Keuangan2 As TLcon
    Private DB_Jurnal As TLcon
    Private DB_Omset As TLcon

    ' Set Variabel Private
    Private formName As String = "SYNCHRONIZATION PAN BROTHERS"
    Private tipeJurnal As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'tipe_jurnal'")
    Private kodeLokasiKlinik As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'NoPoli'")
    Private autoJurnal As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'AutoJurnal'")
    Private userInput As String = PoliKlinik.Lbluser.Text

    Private Sub PanelTop_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PanelTop.Paint
        Try
            e.Graphics.DrawString(formName, New Font("Arial", 12, FontStyle.Bold), Brushes.White, New Point(10, 10))
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SetConnectionFromConfig()
        ' Set Parameter Server Online
        Dim serverOnline_IP As String = Utility.GetConfig("ServerOnline_IP")
        Dim serverOnline_UserID As String = Utility.GetConfig("ServerOnline_UserID")
        Dim serverOnline_Password As String = KASLibrary.Utility.DecryptTripleDES(Utility.GetConfig("ServerOnline_Password"), "albert")

        ' Set DB Keuangan 1 (mitramed_keu)
        DB_Keuangan.Tip = serverOnline_IP
        DB_Keuangan.Tid = serverOnline_UserID
        DB_Keuangan.Tpass = serverOnline_Password
        DB_Keuangan.Tdb = Utility.GetConfig("ServerOnlineKeuangan_DB")

        ' Set DB Keuangan 2 (mitramed_keu2019)
        DB_Keuangan2.Tip = serverOnline_IP
        DB_Keuangan2.Tid = serverOnline_UserID
        DB_Keuangan2.Tpass = serverOnline_Password
        DB_Keuangan2.Tdb = Utility.GetConfig("ServerOnlineKeuangan2_DB")

        ' Set DB Jurnal
        DB_Jurnal.Tip = Utility.GetConfig("ServerJurnal_IP")
        DB_Jurnal.Tid = Utility.GetConfig("ServerJurnal_UserID")
        DB_Jurnal.Tpass = KASLibrary.Utility.DecryptTripleDES(Utility.GetConfig("ServerJurnal_Password"), "albert")
        DB_Jurnal.Tdb = Utility.GetConfig("ServerJurnal_DB")

        ' Set DB Omset (mitramedicare_omset_keuangan)
        DB_Omset.Tip = serverOnline_IP
        DB_Omset.Tid = serverOnline_UserID
        DB_Omset.Tpass = serverOnline_Password
        DB_Omset.Tdb = Utility.GetConfig("ServerOnlineOmset_DB")
    End Sub

    Private Sub FormSynchronizationPanBrothers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        Me.MdiParent = PoliKlinik
        Me.Dock = DockStyle.Fill

        SetConnectionFromConfig()
        LabelProgress.ResetText()
    End Sub

    Private Sub ButtonClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClose.Click
        Me.Close()
    End Sub

    Private Function ValidasiDataUpload() As Boolean
        ' Cek sambungan internet
        If CheckInternetConnection() = False Then
            MessageBox.Show("Eror : Tidak ada koneksi internet.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            Return False
        End If

        Return True
    End Function

    Private Sub ViewDataTransaksi()
        Dim tglTransaksiAwal As Date = DateTimePickerTanggalTransaksiAwal.Value
        Dim tglTransaksiAkhir As Date = DateTimePickerTanggalTransaksiAkhir.Value
        Dim queryString As New StringBuilder()

        queryString.AppendLine("SELECT ")
        queryString.AppendLine("    tblTransaksi.NoUrut AS 'No Transaksi',")
        queryString.AppendLine("    CONVERT(VARCHAR(19), tblTransaksi.TglPoli, 120) AS 'Tgl Transaksi',")
        queryString.AppendLine("    tblPasien.Nama AS 'Nama Pasien',")
        queryString.AppendLine("    tblPasien.NoNIK AS 'No KTP',")
        queryString.AppendLine("    tblPasien.NoKartuBPJS AS 'No BPJS',")
        queryString.AppendLine("    tblDetail.KeluhanUtama AS 'Keluhan',")
        queryString.AppendLine("    ISNULL(CONVERT(VARCHAR(10), tblFisik.Sistole) + '/' + CONVERT(VARCHAR(10), tblFisik.Diastole), '-') AS 'Tensi',")
        queryString.AppendLine("    ISNULL(DiagnosaList.Diagnosa, '-') AS 'Diagnosa',")
        queryString.AppendLine("    ISNULL(TindakanList.Tindakan, '-') AS 'Tindakan'")
        queryString.AppendLine("FROM TransaksiPoli tblTransaksi")
        queryString.AppendLine("JOIN MasterPasien tblPasien ")
        queryString.AppendLine("    ON tblPasien.NoRekamMedis = tblTransaksi.IDInternet")
        queryString.AppendLine("LEFT JOIN TransaksiDetail tblDetail ")
        queryString.AppendLine("    ON tblDetail.NoUrut = tblTransaksi.NoUrut")
        queryString.AppendLine("LEFT JOIN TransaksiDetailFisik tblFisik")
        queryString.AppendLine("    ON tblFisik.NoTransaksi = tblTransaksi.NoUrut")
        queryString.AppendLine("")
        queryString.AppendLine("-- Diagnosa")
        queryString.AppendLine("LEFT JOIN (")
        queryString.AppendLine("    SELECT ")
        queryString.AppendLine("        NoTransaksi,")
        queryString.AppendLine("        STUFF((")
        queryString.AppendLine("            SELECT '; ' + KeteranganDiagnosa")
        queryString.AppendLine("            FROM TransaksiDetailDiagnosa d2")
        queryString.AppendLine("            WHERE d2.NoTransaksi = d1.NoTransaksi")
        queryString.AppendLine("            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, ''")
        queryString.AppendLine("        ) AS Diagnosa")
        queryString.AppendLine("    FROM TransaksiDetailDiagnosa d1")
        queryString.AppendLine("    GROUP BY d1.NoTransaksi")
        queryString.AppendLine(") DiagnosaList ")
        queryString.AppendLine("    ON DiagnosaList.NoTransaksi = tblTransaksi.NoUrut")
        queryString.AppendLine("")
        queryString.AppendLine("-- Tindakan")
        queryString.AppendLine("LEFT JOIN (")
        queryString.AppendLine("    SELECT ")
        queryString.AppendLine("        TransPoliID,")
        queryString.AppendLine("        STUFF((")
        queryString.AppendLine("            SELECT '; ' + NamaTindakan")
        queryString.AppendLine("            FROM TransaksiPoliDetail t2")
        queryString.AppendLine("            WHERE t2.TransPoliID = t1.TransPoliID AND t2.Awal = 0 AND t2.DeletedDate IS NULL")
        queryString.AppendLine("            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, ''")
        queryString.AppendLine("        ) AS Tindakan")
        queryString.AppendLine("    FROM TransaksiPoliDetail t1")
        queryString.AppendLine("    WHERE t1.Awal = 0 AND t1.DeletedDate IS NULL")
        queryString.AppendLine("    GROUP BY t1.TransPoliID")
        queryString.AppendLine(") TindakanList ")
        queryString.AppendLine("    ON TindakanList.TransPoliID = tblTransaksi.TransPoliID")
        queryString.AppendLine("WHERE (CONVERT(DATE, tblTransaksi.TglPoli) BETWEEN '" & Format(tglTransaksiAwal.Date, "yyyyMMdd") & "' AND '" & Format(tglTransaksiAkhir.Date, "yyyyMMdd") & "') AND tblTransaksi.Deleted = 0 AND tblTransaksi.StatusTransaksi = 'finished'")
        queryString.AppendLine("AND (tblTransaksi.ArrivedDate IS NOT NULL AND tblTransaksi.InProgressDate IS NOT NULL AND tblTransaksi.FinishedDate IS NOT NULL)")

        Dim tblPoliUmum As DataTable = ReadSQL(queryString.ToString())

        If tblPoliUmum.Rows.Count > 0 Then
            For Each Tcol As DataColumn In tblPoliUmum.Columns
                Tcol.ReadOnly = True
            Next

            GridControlTransaksi.DataSource = tblPoliUmum
            GridViewTransaksi.Columns("Keluhan").Visible = False
            'GridViewTransaksi.BestFitColumns()
        Else
            MsgBox("Tidak Ada Data.", MsgBoxStyle.Exclamation, "Warning")
        End If
    End Sub

    Private Sub ButtonSearchData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSearchData.Click
        Try
            If ValidasiDataUpload() = True Then
                ViewDataTransaksi()
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ButtonSynchronization_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSynchronization.Click
        thread = New System.Threading.Thread(AddressOf Synchronization)
        thread.Start()
    End Sub

    Private Sub Synchronization()
        Dim tblTransaksi As DataTable = GridControlTransaksi.DataSource

        If tblTransaksi IsNot Nothing AndAlso tblTransaksi.Rows.Count > 0 Then
            ' Disable Button
            ButtonSynchronization.BackColor = Color.FromArgb(225, 225, 225)
            ButtonSynchronization.Enabled = False
            ButtonSearchData.BackColor = Color.FromArgb(225, 225, 225)
            ButtonSearchData.Enabled = False

            Dim queryStringRekap As String = ""
            Dim transaksiProgres As Integer = 0
            Dim persentaseProgres As Integer = 0
            Dim listTransaksiPoli As String = ""

            For Each rowTransaksi As DataRow In tblTransaksi.Rows
                ' Cek Jumlah Proses
                transaksiProgres += 1
                persentaseProgres = transaksiProgres * 100 / tblTransaksi.Rows.Count

                ' Update ProgressBar
                ProgressBarUpload.Value = persentaseProgres
                LabelProgress.Text = "Progress : " & persentaseProgres & " %"

                ' Set Variabel POST Data
                Dim noTransaksi As String = rowTransaksi("No Transaksi").ToString()
                Dim tglPeriksa As String = rowTransaksi("Tgl Transaksi").ToString()
                Dim nomorKTP As String = rowTransaksi("No KTP").ToString()
                Dim nomorBPJS As String = rowTransaksi("No BPJS").ToString()
                Dim tekananDarah As String = rowTransaksi("Tensi").ToString()
                Dim keluhan As String = rowTransaksi("Keluhan").ToString()
                Dim diagnosa As String = rowTransaksi("Diagnosa").ToString()
                Dim tindakan As String = rowTransaksi("Tindakan").ToString()
                Dim tipeKlinik As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'pan_brothers_tipe_klinik'")

                ' Set JSON obat
                Dim jsonObat As String = GetObatJsonByNoTransaksi(noTransaksi)

                ' Set JSON body dari request
                Dim jsonData As String = "{" & _
                                        """tglperiksa"": """ & tglPeriksa & """," & _
                                        """noktp"": """ & nomorKTP & """," & _
                                        """npbpjs"": """ & nomorBPJS & """," & _
                                        """tensi"": """ & tekananDarah & """," & _
                                        """keluhan"": """ & keluhan & """," & _
                                        """diagnosa"": """ & diagnosa & """," & _
                                        """tindakanmedis"": """ & tindakan & """," & _
                                        """type"": """ & tipeKlinik & """," & _
                                        """obat"": [" & jsonObat & "]" & _
                                        "}"

                ' Upload Data
                Dim responseAPI As String = KirimDataKeAPI(jsonData)

                ' Delay 300 ms untuk hindari too many requests
                Threading.Thread.Sleep(300)

                ' Insert Log
                WriteSQL("INSERT INTO LogSynchronizationPanBrothers (NoTransaksi, RequestJson, Response, CreatedDate, CreatedBy) VALUES ('" & noTransaksi & "', '" & jsonData & "', '" & responseAPI & "', GETDATE(), '" & userInput & "')", 4)

                ' Set Nomor Transaksi
                listTransaksiPoli &= "'" & noTransaksi & "',"
            Next

            ' Update Flag Upload
            If listTransaksiPoli <> "" Then
                listTransaksiPoli = Strings.Left(listTransaksiPoli, listTransaksiPoli.Length - 1)
                WriteSQL("UPDATE TransaksiPoli SET UploadPanBrothers = 1 WHERE NoUrut IN (" & listTransaksiPoli & ")", 4)
            End If

            ' Enable Button / TextBox
            ButtonSynchronization.BackColor = Color.White
            ButtonSynchronization.Enabled = True
            ButtonSearchData.BackColor = Color.White
            ButtonSearchData.Enabled = True

            MsgBox(tblTransaksi.Rows.Count & " Data telah berhasil upload.", MsgBoxStyle.Information, "Success!")
        Else
            MsgBox("Tidak Ada Data.", MsgBoxStyle.Exclamation, "Warning")
        End If
    End Sub

    Private Function GetObatJsonByNoTransaksi(ByVal noTransaksi As String) As String
        ' Ambil data obat dari tabel berdasarkan NoTransaksi
        Dim dtObat As DataTable = ReadSQL("SELECT ObatNama, Jumlah FROM TransaksiObat WHERE NoTransaksi = '" & noTransaksi & "' AND DeletedDate IS NULL")

        ' Bangun list JSON item obat
        Dim obatList As New List(Of String)

        For Each rowObat As DataRow In dtObat.Rows
            Dim namaObat As String = rowObat("ObatNama").ToString()
            Dim jumlah As Integer = CInt(rowObat("Jumlah"))

            Dim item As String = "{" & _
                                 """namaobat"": """ & namaObat & """," & _
                                 """jml"": " & jumlah.ToString() & _
                                 "}"
            obatList.Add(item)
        Next

        ' Kembalikan string JSON array obat
        Return String.Join(",", obatList.ToArray())
    End Function

    Public Function KirimDataKeAPI(ByVal jsonData As String) As String
        Dim url As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'pan_brothers_base_url'")
        Dim authorization As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'pan_brothers_authorization'")

        ' Buat request
        Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
        request.Method = "POST"
        request.ContentType = "application/json"
        request.Headers.Add("Bearer", authorization)

        ' Tulis body ke stream
        Dim byteData As Byte() = Encoding.UTF8.GetBytes(jsonData)
        request.ContentLength = byteData.Length
        Using stream As Stream = request.GetRequestStream()
            stream.Write(byteData, 0, byteData.Length)
        End Using

        ' Ambil respon
        Try
            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream())
                    Return reader.ReadToEnd()
                End Using
            End Using
        Catch ex As WebException
            If ex.Response IsNot Nothing Then
                Using reader As New StreamReader(ex.Response.GetResponseStream())
                    Return "Error: " & reader.ReadToEnd()
                End Using
            Else
                Return "Error: " & ex.Message
            End If
        End Try
    End Function
End Class