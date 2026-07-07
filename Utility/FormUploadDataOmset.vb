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
Imports MySql.Data.MySqlClient

Public Class FormUploadDataOmset
    Dim thread As System.Threading.Thread

    ' Guard re-entrancy: cegah double-click / proses paralel yang membuat data & jurnal dobel
    Private isBusy As Boolean = False

    ' Set Connection
    Private DB_Keuangan As TLcon
    Private DB_Keuangan2 As TLcon
    Private DB_Jurnal As TLcon
    Private DB_Omset As TLcon

    ' Set Variabel Private
    Private formName As String = "UPLOAD OMSET"
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

    ' === Helper koneksi MySQL yang MELAPOR error (tidak ditelan seperti WriteThemysql) ===
    Private Function BuildMySqlConnString(ByVal conn As TLcon) As String
        Return "datasource=" & conn.Tip & ";username=" & conn.Tid & ";password=" & conn.Tpass & ";database=" & conn.Tdb
    End Function

    ' Eksekusi 1 perintah ke MySQL. Melempar exception bila gagal (dipakai untuk gating flag jurnal).
    Private Sub WriteMySqlChecked(ByVal sql As String, ByVal conn As TLcon)
        Using mysqlConn As New MySqlConnection(BuildMySqlConnString(conn))
            mysqlConn.Open()
            Using cmd As New MySqlCommand(sql, mysqlConn)
                cmd.CommandTimeout = 120
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ' Eksekusi beberapa perintah dalam SATU transaksi MySQL (all-or-nothing). Rollback + Throw bila gagal.
    Private Sub ExecuteMySqlTransaction(ByVal conn As TLcon, ByVal ParamArray statements As String())
        Using mysqlConn As New MySqlConnection(BuildMySqlConnString(conn))
            mysqlConn.Open()
            Dim tx As MySqlTransaction = mysqlConn.BeginTransaction()
            Try
                For Each sql As String In statements
                    If Not String.IsNullOrEmpty(sql) Then
                        Using cmd As New MySqlCommand(sql, mysqlConn, tx)
                            cmd.CommandTimeout = 120
                            cmd.ExecuteNonQuery()
                        End Using
                    End If
                Next
                tx.Commit()
            Catch
                tx.Rollback()
                Throw
            End Try
        End Using
    End Sub

    ' Kunci/buka tombol + set status sibuk. Dipanggil SINKRON di UI thread saat klik, dan di Finally worker.
    Private Sub SetUiBusy(ByVal busy As Boolean)
        isBusy = busy
        ButtonKirimDataOmset.Enabled = Not busy
        ButtonKirimJurnalUlang.Enabled = Not busy
        ButtonSearchDataOmset.Enabled = Not busy
        If busy Then
            ButtonKirimDataOmset.BackColor = Color.FromArgb(225, 225, 225)
        Else
            ButtonKirimDataOmset.BackColor = Color.FromArgb(0, 100, 200)
        End If
    End Sub

    ' Tulis ke SQL Server lokal. MELEMPAR exception bila gagal; kembalikan jumlah baris terpengaruh.
    ' Beda dengan WriteSQLResult (yang menelan error & kembalikan 0 sehingga gagal vs 0-baris tak terbedakan).
    Private Function WriteSqlChecked(ByVal sql As String) As Integer
        Using con As New SqlConnection(PusatSQL.Con.ConnectionString)
            con.Open()
            Using cmd As New SqlCommand(sql, con)
                cmd.CommandTimeout = 120
                Return cmd.ExecuteNonQuery()
            End Using
        End Using
    End Function

    ' Cek apakah SATU server MySQL tujuan benar-benar bisa dihubungi: buka koneksi singkat lalu tutup.
    ' Lebih andal daripada ping ICMP ke Google — membuktikan service MySQL hidup + jaringan ke host tujuan OK
    ' (bukan sekadar "internet nyala"), dan tidak terhalang firewall yang memblokir ICMP.
    ' Timeout dipersingkat agar tidak menggantung lama bila server mati.
    Private Function CekKoneksiServer(ByVal conn As TLcon) As Boolean
        Try
            Using mysqlConn As New MySqlConnection(BuildMySqlConnString(conn) & ";Connection Timeout=8")
                mysqlConn.Open()
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ' Gerbang sebelum kirim: pastikan SEMUA server tujuan (Keuangan/Omset online + Jurnal) terjangkau.
    ' Menampilkan server mana yang bermasalah dan mengembalikan False bila ada yang tak dapat dihubungi.
    Private Function CekKoneksiServerKirim() As Boolean
        Dim serverBermasalah As New StringBuilder()

        If CekKoneksiServer(DB_Keuangan) = False Then
            serverBermasalah.AppendLine("- Server Keuangan/Omset (" & DB_Keuangan.Tip & ")")
        End If

        If CekKoneksiServer(DB_Jurnal) = False Then
            serverBermasalah.AppendLine("- Server Jurnal (" & DB_Jurnal.Tip & ")")
        End If

        If serverBermasalah.Length > 0 Then
            MsgBox("Tidak bisa mengirim: server tujuan berikut tidak dapat dihubungi." & vbCrLf & vbCrLf & _
                   serverBermasalah.ToString() & vbCrLf & _
                   "Periksa koneksi internet/VPN dan pastikan server aktif, lalu coba lagi.", _
                   MsgBoxStyle.Critical, "Koneksi Server Gagal")
            Return False
        End If

        Return True
    End Function

    Private Sub FormUploadDataOmset_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        Me.MdiParent = PoliKlinik
        Me.Dock = DockStyle.Fill

        ' Set default tanggal transaksi awal & akhir ke H-1 (kemarin), sesuai aturan upload maksimal H-1
        Dim tglH1 As Date = DateTime.Now.AddDays(-1).Date
        DateTimePickerTanggalTransaksiAwal.Value = tglH1
        DateTimePickerTanggalTransaksiAkhir.Value = tglH1

        SetConnectionFromConfig()
        LabelProgress.ResetText()
    End Sub

    Private Sub ButtonClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClose.Click
        Me.Close()
    End Sub

    ' Cegah menutup form saat proses upload/jurnal masih berjalan (thread bisa mengakses kontrol yang sudah dispose -> crash)
    Private Sub FormUploadDataOmset_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If isBusy Then
            MsgBox("Proses upload/jurnal masih berjalan. Mohon tunggu hingga selesai sebelum menutup form.", MsgBoxStyle.Exclamation, "Sedang Proses")
            e.Cancel = True
        End If
    End Sub

    Function JumlahTransaksiPending() As Integer
        Dim query As New StringBuilder()

        query.AppendLine("SELECT COUNT(*) ")
        query.AppendLine("FROM ( ")
        query.AppendLine("    SELECT TransPoliID ")
        query.AppendLine("    FROM TransaksiPoli a ")
        query.AppendLine("    WHERE (CONVERT(DATE, TglPoli) BETWEEN '" & Format(DateTimePickerTanggalTransaksiAwal.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePickerTanggalTransaksiAkhir.Value, "yyyy-MM-dd") & "') ")
        query.AppendLine("      AND Deleted = 0 ")
        query.AppendLine("      AND TransPoliID IN ( ")
        query.AppendLine("          SELECT TransPoliID ")
        query.AppendLine("          FROM TransaksiPoliDetail ")
        query.AppendLine("          WHERE StatusPembayaran = 'PENDING' AND DeletedDate IS NULL")
        query.AppendLine("      ) ")
        query.AppendLine("    UNION ALL ")
        query.AppendLine("    SELECT TransGigiID ")
        query.AppendLine("    FROM TransaksiGigi a ")
        query.AppendLine("    WHERE (CONVERT(DATE, TglGigi) BETWEEN '" & Format(DateTimePickerTanggalTransaksiAwal.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePickerTanggalTransaksiAkhir.Value, "yyyy-MM-dd") & "') ")
        query.AppendLine("      AND Deleted = 0 ")
        query.AppendLine("      AND TransGigiID IN ( ")
        query.AppendLine("          SELECT TransPoliID ")
        query.AppendLine("          FROM TransaksiPoliDetail ")
        query.AppendLine("          WHERE StatusPembayaran = 'PENDING' AND DeletedDate IS NULL")
        query.AppendLine("      ) ")
        query.AppendLine(") AS CombinedResult;")

        Dim jumlahTransaksi As String = ReadSQLNIK(query.ToString())

        Return CDbl(jumlahTransaksi)
    End Function

    Private Function ValidasiDataUpload() As Boolean
        ' Cek sambungan internet
        If CheckInternetConnection() = False Then
            MessageBox.Show("Eror : Tidak ada koneksi internet.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            Return False
        End If

        ' Validasi Tanggal Transaksi
        If DateTimePickerTanggalTransaksiAkhir.Value.Date > DateTime.Now.AddDays(-1).Date Then
            MessageBox.Show("Eror : Tanggal transaksi maksimal H-1.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            ' Kembalikan ke tanggal kemarin
            DateTimePickerTanggalTransaksiAkhir.Value = DateTime.Now.AddDays(-1)

            Return False
        End If

        ' Validasi transaksi pending
        If JumlahTransaksiPending() > 0 Then
            MessageBox.Show("Eror : Terdapat transaksi pending yang belum diselesaikan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            Return False
        End If

        Return True
    End Function

    Private Sub ButtonSearchDataOmset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSearchDataOmset.Click
        Try
            If ValidasiDataUpload() = True Then
                ViewDataOmset()
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ViewDataOmset()
        Dim tblPoliUmum As DataTable = GetDataUploadOmsetPoliUmum()
        Dim tblPoliGigi As DataTable = GetDataUploadOmsetPoliGigi()
        Dim tblLaboratorium As DataTable = GetDataUploadOmsetLaboratorium()

        ' Buat tabel gabungan dengan struktur yang sama
        Dim tblOmset As DataTable = tblPoliUmum.Clone()

        ' Gabungkan data dari kedua tabel
        tblOmset.Merge(tblPoliUmum)
        tblOmset.Merge(tblPoliGigi)
        tblOmset.Merge(tblLaboratorium)

        If tblOmset.Rows.Count > 0 Then
            For Each Tcol As DataColumn In tblOmset.Columns
                Tcol.ReadOnly = True
            Next

            GridControlTransaksi.DataSource = tblOmset
            GridViewTransaksi.Columns("JenisTransaksi").Visible = False
            GridViewTransaksi.BestFitColumns()
        Else
            MsgBox("Tidak Ada Data.", MsgBoxStyle.Exclamation, "Warning")
        End If
    End Sub

    Function GetDataUploadOmsetPoliUmum() As DataTable
        Dim tglTransaksiAwal As Date = DateTimePickerTanggalTransaksiAwal.Value
        Dim tglTransaksiAkhir As Date = DateTimePickerTanggalTransaksiAkhir.Value
        Dim tblResult As New DataTable

        Dim query As New StringBuilder()

        query.AppendLine("WITH FilteredTransPoli AS (")
        query.AppendLine("    SELECT TransPoliID")
        query.AppendLine("    FROM TransaksiPoli")
        query.AppendLine("    WHERE (CONVERT(DATE, TglPoli) BETWEEN '" & Format(tglTransaksiAwal.Date, "yyyyMMdd") & "' AND '" & Format(tglTransaksiAkhir.Date, "yyyyMMdd") & "') AND Upload = 0 AND Deleted = 0")
        query.AppendLine(")")
        query.AppendLine("SELECT tblBayar.TransPoliID AS 'ID', t.NoUrut, CONVERT(DATE, t.TglPoli) AS 'Tgl Transaksi',")
        query.AppendLine("       d.JenisPoli, p.Nama, p.NoRekamMedis, tblBayar.JenisBayar, tblBayar.NoJenisBayar, tblBayar.kartu AS 'JenisKartu', tblBayar.mesin AS 'JenisMesin', d.JenisTransaksi,")
        query.AppendLine("       SUM(JumlahBayar - Kembali) AS TotalBayar")
        query.AppendLine("FROM (")
        query.AppendLine("    SELECT tb.TransPoliID, tb.JenisBayar, tb.NoJenisBayar, tb.JumlahBayar, tb.Kembali, tb.kartu, tb.mesin")
        query.AppendLine("    FROM TransaksiPoliBayar tb")
        query.AppendLine("    INNER JOIN FilteredTransPoli ft ON tb.TransPoliID = ft.TransPoliID")
        query.AppendLine("    WHERE tb.JumlahBayar > 0 AND tb.JenisBayar IN ('TAGIHAN', 'TUNAI', 'DEBET', 'CARD')")
        query.AppendLine("    UNION ALL")
        query.AppendLine("    SELECT tba.TransPoliID, tba.JenisBayar, tba.NoJenisBayar, tba.JumlahBayar, tba.Kembali, tba.kartu, tba.mesin")
        query.AppendLine("    FROM TransaksiPoliBayarAwal tba")
        query.AppendLine("    INNER JOIN FilteredTransPoli ft ON tba.TransPoliID = ft.TransPoliID")
        query.AppendLine("    WHERE tba.JumlahBayar > 0 AND tba.JenisBayar IN ('TAGIHAN', 'TUNAI', 'DEBET', 'CARD')")
        query.AppendLine(") AS tblBayar")
        query.AppendLine("LEFT JOIN TransaksiPoli t ON tblBayar.TransPoliID = t.TransPoliID")
        query.AppendLine("LEFT JOIN TransaksiDetail d ON t.NoUrut = d.NoUrut")
        query.AppendLine("LEFT JOIN MasterPasien p ON t.IDInternet = p.NoRekamMedis")
        query.AppendLine("GROUP BY tblBayar.TransPoliID, t.NoUrut, CONVERT(DATE, t.TglPoli),")
        query.AppendLine("         d.JenisPoli, p.Nama, p.NoRekamMedis, tblBayar.JenisBayar, tblBayar.NoJenisBayar, tblBayar.kartu, tblBayar.mesin, d.JenisTransaksi")

        tblResult = ReadSQL(query.ToString())

        Return tblResult
    End Function

    Function GetDataUploadOmsetPoliGigi() As DataTable
        Dim tglTransaksiAwal As Date = DateTimePickerTanggalTransaksiAwal.Value
        Dim tglTransaksiAkhir As Date = DateTimePickerTanggalTransaksiAkhir.Value
        Dim tblResult As New DataTable

        Dim query As New StringBuilder()

        query.AppendLine("WITH FilteredTransGigi AS (")
        query.AppendLine("    SELECT TransGigiID")
        query.AppendLine("    FROM TransaksiGigi")
        query.AppendLine("    WHERE (CONVERT(DATE, TglGigi) BETWEEN '" & Format(tglTransaksiAwal.Date, "yyyyMMdd") & "' AND '" & Format(tglTransaksiAkhir.Date, "yyyyMMdd") & "') AND Upload = 0 AND Deleted = 0")
        query.AppendLine(")")
        query.AppendLine("SELECT tblBayar.TransGigiID AS 'ID', t.NoUrut, CONVERT(DATE, t.TglGigi) AS 'Tgl Transaksi',")
        query.AppendLine("       d.JenisPoli, p.Nama, p.NoRekamMedis, tblBayar.JenisBayar, tblBayar.NoJenisBayar, tblBayar.kartu AS 'JenisKartu', tblBayar.mesin AS 'JenisMesin', d.JenisTransaksi,")
        query.AppendLine("       SUM(JumlahBayar - Kembali) AS TotalBayar")
        query.AppendLine("FROM (")
        query.AppendLine("    SELECT tb.TransGigiID, tb.JenisBayar, tb.NoJenisBayar, tb.JumlahBayar, tb.Kembali, tb.kartu, tb.mesin")
        query.AppendLine("    FROM TransaksiGigiBayar tb")
        query.AppendLine("    INNER JOIN FilteredTransGigi ft ON tb.TransGigiID = ft.TransGigiID")
        query.AppendLine("    WHERE tb.JumlahBayar > 0 AND tb.JenisBayar IN ('TAGIHAN', 'TUNAI', 'DEBET', 'CARD')")
        query.AppendLine("    UNION ALL")
        query.AppendLine("    SELECT tba.TransGigiID, tba.JenisBayar, tba.NoJenisBayar, tba.JumlahBayar, tba.Kembali, tba.kartu, tba.mesin")
        query.AppendLine("    FROM TransaksiGigiBayarAwal tba")
        query.AppendLine("    INNER JOIN FilteredTransGigi ft ON tba.TransGigiID = ft.TransGigiID")
        query.AppendLine("    WHERE tba.JumlahBayar > 0 AND tba.JenisBayar IN ('TAGIHAN', 'TUNAI', 'DEBET', 'CARD')")
        query.AppendLine(") AS tblBayar")
        query.AppendLine("LEFT JOIN TransaksiGigi t ON tblBayar.TransGigiID = t.TransGigiID")
        query.AppendLine("LEFT JOIN TransaksiDetail d ON t.NoUrut = d.NoUrut")
        query.AppendLine("LEFT JOIN MasterPasien p ON t.IDInternet = p.NoRekamMedis")
        query.AppendLine("GROUP BY tblBayar.TransGigiID, t.NoUrut, CONVERT(DATE, t.TglGigi),")
        query.AppendLine("         d.JenisPoli, p.Nama, p.NoRekamMedis, tblBayar.JenisBayar, tblBayar.NoJenisBayar, tblBayar.kartu, tblBayar.mesin, d.JenisTransaksi")

        tblResult = ReadSQL(query.ToString())

        Return tblResult
    End Function

    Function GetDataUploadOmsetLaboratorium() As DataTable
        Dim tglTransaksiAwal As Date = DateTimePickerTanggalTransaksiAwal.Value
        Dim tglTransaksiAkhir As Date = DateTimePickerTanggalTransaksiAkhir.Value
        Dim tblResult As New DataTable

        Dim query As New StringBuilder()

        query.AppendLine("WITH FilteredTransLab AS (")
        query.AppendLine("    SELECT TransLabID")
        query.AppendLine("    FROM TransaksiLab")
        query.AppendLine("    WHERE (CONVERT(DATE, TglLab) BETWEEN '" & Format(tglTransaksiAwal.Date, "yyyyMMdd") & "' AND '" & Format(tglTransaksiAkhir.Date, "yyyyMMdd") & "') AND Upload = 0 AND Deleted = 0 AND SendKeu IS NOT NULL")
        query.AppendLine(")")
        query.AppendLine("SELECT tblBayar.TransLabID AS 'ID', t.NoUrut, CONVERT(DATE, t.TglLab) AS 'Tgl Transaksi',")
        query.AppendLine("       d.JenisPoli, p.Nama, p.NoRekamMedis, tblBayar.JenisBayar, tblBayar.NoJenisBayar, tblBayar.kartu AS 'JenisKartu', tblBayar.mesin AS 'JenisMesin', d.JenisTransaksi,")
        query.AppendLine("       SUM(JumlahBayar - Kembali) AS TotalBayar")
        query.AppendLine("FROM (")
        query.AppendLine("    SELECT tb.TransLabID, tb.JenisBayar, tb.NoJenisBayar, tb.JumlahBayar, tb.Kembali, tb.kartu, tb.mesin")
        query.AppendLine("    FROM TransaksiLabBayar tb")
        query.AppendLine("    INNER JOIN FilteredTransLab ft ON tb.TransLabID = ft.TransLabID")
        query.AppendLine("    WHERE tb.JumlahBayar > 0 AND tb.JenisBayar IN ('TAGIHAN', 'TUNAI', 'DEBET', 'CARD')")
        query.AppendLine(") AS tblBayar")
        query.AppendLine("LEFT JOIN TransaksiLab t ON tblBayar.TransLabID = t.TransLabID")
        query.AppendLine("LEFT JOIN TransaksiDetail d ON t.NoUrut = d.NoUrut")
        query.AppendLine("LEFT JOIN MasterPasien p ON t.IDInternet = p.NoRekamMedis")
        query.AppendLine("GROUP BY tblBayar.TransLabID, t.NoUrut, CONVERT(DATE, t.TglLab),")
        query.AppendLine("         d.JenisPoli, p.Nama, p.NoRekamMedis, tblBayar.JenisBayar, tblBayar.NoJenisBayar, tblBayar.kartu, tblBayar.mesin, d.JenisTransaksi")

        tblResult = ReadSQL(query.ToString())

        Return tblResult
    End Function

    Private Sub ButtonKirimDataOmset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKirimDataOmset.Click
        ' Guard double-click: kunci SINKRON di UI thread sebelum thread mulai (Bug D)
        If isBusy Then
            MsgBox("Proses masih berjalan. Mohon tunggu hingga selesai.", MsgBoxStyle.Exclamation, "Sedang Proses")
            Exit Sub
        End If
        SetUiBusy(True)

        thread = New System.Threading.Thread(AddressOf KirimOmsetHarian)
        thread.IsBackground = True
        thread.Start()
    End Sub

    Private Sub KirimOmsetHarian()
      Try
        ' P1.2: validasi sama seperti tombol Cari (internet, maks H-1, transaksi PENDING) sebelum kirim
        If ValidasiDataUpload() = False Then Exit Sub

        ' Pastikan server tujuan (Keuangan online + Jurnal) benar-benar terjangkau, bukan sekadar internet nyala
        If CekKoneksiServerKirim() = False Then Exit Sub

        Dim tblPoliUmum As DataTable = GetDataUploadOmsetPoliUmum()
        Dim tblPoliGigi As DataTable = GetDataUploadOmsetPoliGigi()
        Dim tblLaboratorium As DataTable = GetDataUploadOmsetLaboratorium()

        ' Buat tabel gabungan dengan struktur yang sama
        Dim tblOmset As DataTable = tblPoliUmum.Clone()

        ' Gabungkan data dari kedua tabel
        tblOmset.Merge(tblPoliUmum)
        tblOmset.Merge(tblPoliGigi)
        tblOmset.Merge(tblLaboratorium)

        If tblOmset.Rows.Count > 0 Then
            Dim queryStringRekap As String = ""
            Dim transaksiProgres As Integer = 0
            Dim persentaseProgres As Integer = 0

            ' Set Data Pasien Umum
            Dim rowPasienUmum As Integer = 0
            Dim queryStringUmum As New StringBuilder()
            queryStringUmum.AppendLine("INSERT INTO acctranspendapatandetail (inctgl, loc, incjenis, jenis, kartu, mesin, incnilai, createdate, inctransid, idinternet, pasien, no_transaksi, jenis_poli, flagdate, flagby, flag) VALUES ")

            ' Set Data Pasien Tagihan
            Dim rowPasienTagihan As Integer = 0
            Dim queryStringTagihan As New StringBuilder()
            queryStringTagihan.AppendLine("INSERT INTO acctranstagihandetail(inctgl, loc, incjenis, jenis, kartu, mesin, incnilai, createdate, inctransid, idinternet, pasien, rekapid, lockeddate, lockedby, no_transaksi, jenis_poli) VALUES ")

            ' Set Data No Transaksi
            Dim noTransaksiPoli As String = ""
            Dim noTransaksiGigi As String = ""
            Dim noTransaksiLab As String = ""

            ' Set HashSet untuk setiap jenis transaksi
            Dim tempTransaksiPoli As New HashSet(Of String)()
            Dim tempTransaksiLab As New HashSet(Of String)()
            Dim tempTransaksiGigi As New HashSet(Of String)()

            For Each rowOmset As DataRow In tblOmset.Rows
                ' Cek Jumlah Proses
                transaksiProgres += 1
                persentaseProgres = transaksiProgres * 100 / tblOmset.Rows.Count

                ' Update ProgressBar
                ProgressBarUpload.Value = persentaseProgres
                LabelProgress.Text = "Progress : " & persentaseProgres & " %"

                ' Set Variabel Input Data
                Dim tglSekarang As String = Format(DateAndTime.Now, "yyyy-MM-dd")
                Dim tglPeriksa As String = Format(rowOmset("Tgl Transaksi"), "yyyy-MM-dd")
                Dim jenisKartu As String = rowOmset("JenisKartu")
                Dim jenisMesin As String = rowOmset("JenisMesin")
                Dim idTransaksi As String = rowOmset("ID")
                Dim noRekamMedis As String = rowOmset("NoRekamMedis")
                Dim namaPasien As String = rowOmset("Nama").ToString.Replace("'", "''")
                Dim noTransaksi As String = rowOmset("NoUrut")
                Dim jenisPoli As String = rowOmset("JenisPoli")
                Dim keteranganBayar As String = rowOmset("NoJenisBayar")
                Dim jenisTransaksi As String = rowOmset("JenisTransaksi")
                Dim nominalTransaksi As String = rowOmset("TotalBayar")
                Dim jenisBayar As String = ""

                ' Set Keterangan Jenis Bayar
                If rowOmset("JenisBayar").ToString = "TUNAI" Then
                    jenisBayar = "Tunai"
                ElseIf rowOmset("JenisBayar").ToString = "DEBET" Then
                    jenisBayar = "Debet"
                ElseIf rowOmset("JenisBayar").ToString = "CARD" Then
                    jenisBayar = "Kredit"
                ElseIf rowOmset("JenisBayar").ToString = "TAGIHAN" Then
                    jenisBayar = "Tagihan"
                End If

                If jenisBayar = "TAGIHAN" Then ' Get Rekap Tagihan Per Transaksi
                    Dim keteranganPerusahaan, kodePerusahaan As String
                    If keteranganBayar.Contains("-") = True Then
                        Dim splitKeterangan() As String = keteranganBayar.Split("-")
                        keteranganPerusahaan = splitKeterangan(0)
                        kodePerusahaan = splitKeterangan(1)
                    Else
                        keteranganPerusahaan = keteranganBayar
                        kodePerusahaan = ""
                    End If

                    Select Case jenisTransaksi
                        Case "POLI"
                            queryStringRekap = "SELECT a.RekapID, b.CreateDate, b.CreateBy FROM TransaksiPoli a LEFT JOIN RekapTagihan b ON a.RekapID=b.RekapID WHERE a.TransPoliID='" & idTransaksi & "'"
                        Case "GIGI"
                            queryStringRekap = "SELECT a.RekapID, b.CreateDate, b.CreateBy FROM TransaksiGigi a LEFT JOIN RekapTagihan b ON a.RekapID=b.RekapID WHERE a.TransGigiID='" & idTransaksi & "'"
                        Case "LAB"
                            queryStringRekap = "SELECT a.RekapID, b.CreateDate, b.CreateBy FROM TransaksiLab a LEFT JOIN RekapTagihan b ON a.RekapID=b.RekapID WHERE a.translabid='" & idTransaksi & "'"
                    End Select

                    Dim TblsRekap As DataTable = ReadSQL(queryStringRekap)

                    For Each TrowRekap As DataRow In TblsRekap.Rows
                        Dim idRekap As String = "NULL"
                        Dim tglRekap As String = "9998-12-31 00:00:00"
                        Dim namaUserRekap As String = ""

                        If IsDBNull(TrowRekap("RekapID")) = False Then idRekap = TrowRekap("RekapID")
                        If IsDBNull(TrowRekap("CreateDate")) = False Then tglRekap = Format(TrowRekap("CreateDate"), "yyyy-MM-dd HH:mm:ss")
                        If IsDBNull(TrowRekap("CreateBy")) = False Then namaUserRekap = TrowRekap("CreateBy")

                        queryStringTagihan.AppendLine("('" & tglPeriksa & "','" & kodeLokasiKlinik & "','" & jenisTransaksi & "','" & jenisBayar & "','" & keteranganPerusahaan & "','" & kodePerusahaan & "','" & nominalTransaksi & "',NOW(),'" & idTransaksi & "','" & noRekamMedis & "','" & namaPasien & "', " & idRekap & ", '" & tglRekap & "', '" & namaUserRekap & "', '" & noTransaksi & "', '" & jenisPoli & "'),")
                        rowPasienTagihan += 1
                    Next
                Else
                    queryStringUmum.AppendLine("('" & tglPeriksa & "','" & kodeLokasiKlinik & "','" & jenisTransaksi & "','" & jenisBayar & "','" & jenisKartu & "','" & jenisMesin & "','" & nominalTransaksi & "',NOW(),'" & idTransaksi & "','" & noRekamMedis & "','" & namaPasien & "', '" & noTransaksi & "', '" & jenisPoli & "', NOW(), '" & userInput & "', 'Auto Jurnal Pendapatan'),")
                    rowPasienUmum += 1
                End If

                Select Case jenisTransaksi
                    Case "POLI"
                        If tempTransaksiPoli.Add(idTransaksi) Then
                            noTransaksiPoli &= "'" & idTransaksi & "',"
                        End If
                    Case "LAB"
                        If tempTransaksiLab.Add(idTransaksi) Then
                            noTransaksiLab &= "'" & idTransaksi & "',"
                        End If
                    Case "GIGI"
                        If tempTransaksiGigi.Add(idTransaksi) Then
                            noTransaksiGigi &= "'" & idTransaksi & "',"
                        End If
                End Select
            Next

            ' Susun statement online (buang koma terakhir)
            Dim sqlStatementUmum As String = ""
            Dim sqlStatementTagihan As String = ""

            If rowPasienUmum > 0 Then
                sqlStatementUmum = queryStringUmum.ToString().TrimEnd()
                If sqlStatementUmum.EndsWith(",") Then sqlStatementUmum = sqlStatementUmum.Substring(0, sqlStatementUmum.Length - 1)
            End If

            If rowPasienTagihan > 0 Then
                sqlStatementTagihan = queryStringTagihan.ToString().TrimEnd()
                If sqlStatementTagihan.EndsWith(",") Then sqlStatementTagihan = sqlStatementTagihan.Substring(0, sqlStatementTagihan.Length - 1)
            End If

            ' === Bug A: Post umum + tagihan ke online dalam SATU transaksi.
            '     Bila GAGAL -> rollback & KELUAR tanpa men-set Upload=1, supaya data masih bisa dikirim ulang. ===
            If rowPasienUmum > 0 OrElse rowPasienTagihan > 0 Then
                Try
                    ExecuteMySqlTransaction(DB_Keuangan, sqlStatementUmum, sqlStatementTagihan)
                Catch exUpload As Exception
                    MsgBox("Gagal upload omset ke online. Flag lokal TIDAK diubah, silakan periksa koneksi lalu kirim ulang." & vbCrLf & vbCrLf & exUpload.Message, MsgBoxStyle.Critical, "Upload Gagal")
                    Exit Sub
                End Try
            End If

            ' Update Flag Upload — P2.4: pakai WriteSqlChecked (melempar error) supaya GAGAL terdeteksi.
            ' Online SUDAH commit di titik ini; bila flag lokal gagal, operator HARUS tahu agar TIDAK kirim ulang (dobel).
            Try
                If tempTransaksiPoli.Count > 0 Then
                    noTransaksiPoli = Strings.Left(noTransaksiPoli, noTransaksiPoli.Length - 1)
                    WriteSqlChecked("UPDATE TransaksiPoli SET Upload = 1, UploadDate = GETDATE() WHERE TransPoliID IN (" & noTransaksiPoli & ")")
                End If

                If tempTransaksiLab.Count > 0 Then
                    noTransaksiLab = Strings.Left(noTransaksiLab, noTransaksiLab.Length - 1)
                    WriteSqlChecked("UPDATE TransaksiLab SET Upload = 1, UploadDate = GETDATE() WHERE TransLabID IN (" & noTransaksiLab & ")")
                End If

                If tempTransaksiGigi.Count > 0 Then
                    noTransaksiGigi = Strings.Left(noTransaksiGigi, noTransaksiGigi.Length - 1)
                    WriteSqlChecked("UPDATE TransaksiGigi SET Upload = 1, UploadDate = GETDATE() WHERE TransGigiID IN (" & noTransaksiGigi & ")")
                End If
            Catch exFlag As Exception
                MsgBox("PENTING: Omset SUDAH terkirim ke online, tetapi update flag 'Upload' di database lokal GAGAL." & vbCrLf & vbCrLf & "JANGAN kirim ulang omset ini (berisiko dobel di online). Hubungi IT untuk set Upload = 1 secara manual." & vbCrLf & vbCrLf & exFlag.Message, MsgBoxStyle.Critical, "Flag Lokal Gagal")
            End Try

            Dim jumlahTransaksi As Integer = tempTransaksiPoli.Count + tempTransaksiLab.Count + tempTransaksiGigi.Count

            ' Send Jurnal
            SendingJurnal()

            MsgBox(jumlahTransaksi & " Data telah berhasil upload.", MsgBoxStyle.Information, "Success!")
        Else
            MsgBox("Tidak Ada Data.", MsgBoxStyle.Exclamation, "Warning")
        End If
      Catch ex As Exception
        MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
      Finally
        SetUiBusy(False)
      End Try
    End Sub

    Private Sub ButtonKirimJurnalUlang_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKirimJurnalUlang.Click
        ' Guard double-click: kunci SINKRON sebelum thread mulai (Bug D)
        If isBusy Then
            MsgBox("Proses masih berjalan. Mohon tunggu hingga selesai.", MsgBoxStyle.Exclamation, "Sedang Proses")
            Exit Sub
        End If
        SetUiBusy(True)

        thread = New System.Threading.Thread(AddressOf SendingJurnalUlang)
        thread.IsBackground = True
        thread.Start()
    End Sub

    ' Pembungkus SendingJurnal untuk jalur "Kirim Jurnal Ulang": pastikan UI selalu dipulihkan walau error.
    Private Sub SendingJurnalUlang()
        Try
            ' Jurnal butuh server online (Jurnal + Omset) — pastikan server tujuan terjangkau, bukan sekadar internet nyala
            If CekKoneksiServerKirim() = False Then Exit Sub

            SendingJurnal()
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            SetUiBusy(False)
        End Try
    End Sub

    Private Sub SendingJurnal()
        Dim tblPoliUmum As DataTable = GetDataUploadJurnalPoliUmum()
        Dim tblPoliGigi As DataTable = GetDataUploadJurnalPoliGigi()
        Dim tblLaboratorium As DataTable = GetDataUploadJurnalLaboratorium()

        ' Buat tabel gabungan dengan struktur yang sama
        Dim tblTransaksi As DataTable = tblPoliUmum.Clone()

        ' Gabungkan data dari kedua tabel
        tblTransaksi.Merge(tblPoliUmum)
        tblTransaksi.Merge(tblPoliGigi)
        tblTransaksi.Merge(tblLaboratorium)

        Dim transaksiProgres As Integer = 0
        Dim persentaseProgres As Integer = 0

        Dim tglTransaksiAwal As Date = DateTimePickerTanggalTransaksiAwal.Value
        Dim tglTransaksiAkhir As Date = DateTimePickerTanggalTransaksiAkhir.Value

        ' P1.1: ringkas hasil (hindari MsgBox per baris yang membanjiri layar bila server jurnal bermasalah)
        Dim jmlSukses As Integer = 0
        Dim jmlGagal As Integer = 0
        Dim jmlHilang As Integer = 0
        Dim jmlFlagGagal As Integer = 0
        Dim jmlDilewati As Integer = 0
        Dim gagalBeruntun As Integer = 0
        Dim pesanDetail As New StringBuilder()

        ' Kumpulan kunci grup yang flag-nya BERHASIL di-set + daftar baris yang GAGAL buat jurnal.
        ' Dipakai untuk rekonsiliasi di akhir: baris gagal yang grup-nya sudah ke-flag transaksi lain
        ' segrup (kartu/mesin berbeda, jenis bayar sama) TIDAK boleh dilaporkan "aman dikirim ulang" —
        ' kirim ulang akan melewatinya (FlagJurnal... IS NULL tak terpenuhi) sehingga jurnalnya hilang.
        Dim flaggedKeys As New HashSet(Of String)()
        Dim gagalKeys As New List(Of String)()
        Dim gagalInfo As New List(Of String)()

        If tblTransaksi.Rows.Count > 0 Then
            For Each rowTransaksi As DataRow In tblTransaksi.Rows
                ' Cek Jumlah Proses
                transaksiProgres += 1
                persentaseProgres = transaksiProgres * 100 / tblTransaksi.Rows.Count

                ' Update ProgressBar
                ProgressBarUpload.Value = persentaseProgres
                LabelProgress.Text = "Progress : " & persentaseProgres & " %"

                ' Set ID Lokasi Jurnal
                Dim tblMasterJurnal As New DataTable
                Dim idLokasiJurnal As String = ""
                Dim keteranganLokasiJurnal As String = ""

                If tipeJurnal = "Poli" Then
                    tblMasterJurnal = ReadThemysql("SELECT a.KeteranganLokasi AS 'Lokasi', a.KeteranganPoli AS 'Keterangan', a.KodeLokasiJurnal, a.KodeCoaPendapatan, a.KodeCoaPiutangPending, a.KodeCoaPendapatanTunai FROM MasterLokasiJurnal a WHERE a.KodeLokasi = '" & kodeLokasiKlinik & "' AND a.KeteranganPoli = '" & rowTransaksi("JenisPoli") & "' LIMIT 1;", DB_Omset)
                ElseIf tipeJurnal = "Transaksi" Then
                    tblMasterJurnal = ReadThemysql("SELECT a.KeteranganLokasi AS 'Lokasi', a.JenisTransaksi AS 'Keterangan', a.KodeLokasiJurnal, a.KodeCoaPendapatan, a.KodeCoaPiutangPending, a.KodeCoaPendapatanTunai FROM MasterLokasiJurnal a WHERE a.KodeLokasi = '" & kodeLokasiKlinik & "' AND a.JenisTransaksi = '" & rowTransaksi("JenisTransaksi") & "' LIMIT 1;", DB_Omset)
                End If

                ' Bug C: lewati baris bila tidak ada MasterLokasiJurnal (hindari crash Rows(0) yang membatalkan seluruh batch)
                If tblMasterJurnal.Rows.Count = 0 Then
                    jmlDilewati += 1
                    pesanDetail.AppendLine("- Dilewati (tak ada MasterLokasiJurnal): " & rowTransaksi("JenisTransaksi").ToString & " " & Format(rowTransaksi("Tgl Transaksi"), "dd/MM/yyyy"))
                    Continue For
                End If

                Dim rowDataJurnal As DataRow = tblMasterJurnal.Rows(0)
                idLokasiJurnal = rowDataJurnal("KodeLokasiJurnal")
                keteranganLokasiJurnal = rowDataJurnal("Lokasi") & " (" & rowDataJurnal("Keterangan") & ")"

                ' Set Flag / Refference
                Dim refferenceID As String = GetUUID()

                ' Set Mesin EDC
                Dim mesinEDC As String = rowTransaksi("mesin").ToString.Trim
                Dim keteranganMesinEDC As String = " "
                Dim coaMesinEDC As String = ""

                If rowTransaksi("JenisBayar").ToString = "DEBET" Or rowTransaksi("JenisBayar").ToString = "CARD" Then
                    ' Set Keterangan Mesin EDC
                    If rowTransaksi("mesin").ToString.Trim <> "" Then
                        keteranganMesinEDC = " " & rowTransaksi("mesin").ToString.Trim & " "
                    End If

                    ' Set COA Mesin EDC
                    If mesinEDC.Contains("QRIS") = True Then
                        coaMesinEDC = ReadThemysqlNIK("SELECT a.KodeAkun FROM MasterCoaJurnal a WHERE a.NamaAkun = 'Piutang QRIS'", DB_Omset)
                    ElseIf mesinEDC = "BCA" Then
                        coaMesinEDC = ReadThemysqlNIK("SELECT a.KodeAkun FROM MasterCoaJurnal a WHERE a.NamaAkun = 'Piutang BCA'", DB_Omset)
                    ElseIf mesinEDC = "MANDIRI" Then
                        coaMesinEDC = ReadThemysqlNIK("SELECT a.KodeAkun FROM MasterCoaJurnal a WHERE a.NamaAkun = 'Piutang MANDIRI'", DB_Omset)
                    ElseIf mesinEDC = "CIMB" Or mesinEDC = "NIAGA" Then
                        coaMesinEDC = ReadThemysqlNIK("SELECT a.KodeAkun FROM MasterCoaJurnal a WHERE a.NamaAkun = 'Piutang CIMB'", DB_Omset)
                    End If
                End If

                    ' Set Keterangan Jenis Bayar
                    Dim keteranganJenisBayar As String = ""
                    If rowTransaksi("JenisBayar").ToString = "TUNAI" Then
                        keteranganJenisBayar = "Tunai"
                    ElseIf rowTransaksi("JenisBayar").ToString = "DEBET" Then
                        keteranganJenisBayar = "Debet"
                    ElseIf rowTransaksi("JenisBayar").ToString = "CARD" Then
                        keteranganJenisBayar = "Kredit"
                    ElseIf rowTransaksi("JenisBayar").ToString = "TAGIHAN" Then
                        keteranganJenisBayar = "Tagihan"
                    End If

                    ' Set Keterangan Jurnal
                    Dim keteranganJurnal As String = "TERIMA - OMZET " & keteranganJenisBayar.ToUpper() & keteranganMesinEDC.ToUpper() & keteranganLokasiJurnal & " " & Format(rowTransaksi("Tgl Transaksi"), "dd/MM/yyyy") & ""

                    ' Set Jenis Poli
                    Dim jenisPoli As String = ""
                    If tipeJurnal = "Poli" Then
                        jenisPoli = rowTransaksi("JenisPoli").ToString
                    ElseIf tipeJurnal = "Transaksi" Then
                        jenisPoli = ""
                    End If

                    ' Kunci grup untuk deteksi "jurnal hilang": HARUS sama cakupannya dengan set-flag
                    ' (tabel transaksi + tanggal + jenis bayar + poli). Flag Lab TIDAK memfilter poli,
                    ' begitu pula mode "Transaksi" (jenisPoli = ""), jadi poliKey mengikuti keduanya.
                    Dim jenisTransaksiKey As String = rowTransaksi("JenisTransaksi").ToString.Trim.ToUpper()
                    Dim poliKey As String = jenisPoli
                    If jenisTransaksiKey = "LAB" Then poliKey = ""
                    Dim grupKey As String = jenisTransaksiKey & "|" & Format(rowTransaksi("Tgl Transaksi"), "yyyyMMdd") & "|" & keteranganJenisBayar & "|" & poliKey

                    ' Tentukan COA lawan sesuai jenis bayar
                    Dim coaLawan As String = ""
                    Dim adaJurnal As Boolean = True
                    If keteranganJenisBayar = "Tagihan" Then
                        coaLawan = rowDataJurnal("KodeCoaPiutangPending").ToString()
                    ElseIf keteranganJenisBayar = "Tunai" Then
                        coaLawan = rowDataJurnal("KodeCoaPendapatanTunai").ToString()
                    ElseIf keteranganJenisBayar = "Debet" Or keteranganJenisBayar = "Kredit" Then
                        coaLawan = coaMesinEDC
                    Else
                        adaJurnal = False
                    End If

                    If Not adaJurnal Then
                        jmlDilewati += 1
                        Continue For
                    End If

                    Dim sqlJurnal As String = "SELECT F_MakeUMH('" & refferenceID & "', 'out', '', '" & Format(rowTransaksi("Tgl Transaksi"), "yyyyMMdd") & "', '" & rowDataJurnal("KodeCoaPendapatan").ToString() & "', '" & coaLawan & "', 'IDR', 1, " & CDbl(rowTransaksi("JumlahBayar")) & ", '" & keteranganJurnal.ToUpper() & "', '1', 1, '', '" & keteranganJurnal.ToUpper() & "', '', '', '" & idLokasiJurnal & "', 4, " & CDbl(rowTransaksi("JumlahBayar")) & ", 'K')"

                    Dim jurnalDibuat As Boolean = False
                    Try
                        ' 1) Bug B: buat jurnal di server jurnal — akan Throw bila gagal
                        WriteMySqlChecked(sqlJurnal, DB_Jurnal)
                        jurnalDibuat = True

                        ' 2) Set flag dengan retry. Flag inilah yang mencegah aggregate muncul lagi -> retry tak menduplikasi.
                        SetFlagJurnalDenganRetry(rowTransaksi, refferenceID, jenisPoli, keteranganJenisBayar)

                        ' Flag grup ini SUDAH ter-set -> catat, agar saudara segrup yang gagal terdeteksi sebagai "hilang".
                        flaggedKeys.Add(grupKey)

                        ' 3) Simpan Log Jurnal Omset (best-effort)
                        WriteSQL("INSERT INTO LogJurnalKeuangan (JurnalFlag, JurnalDate, JurnalBy, Status, JenisPoli, JenisTransaksi) VALUES ('" & refferenceID & "', GETDATE(), '" & userInput & "', '" & keteranganJurnal & "', '" & jenisPoli & "', '" & rowTransaksi("JenisTransaksi").ToString & "')", 4)

                        jmlSukses += 1
                        gagalBeruntun = 0
                    Catch exJurnal As Exception
                        If jurnalDibuat Then
                            ' KRITIS: jurnal SUDAH dibuat tapi flag GAGAL walau sudah retry.
                            ' Kirim ulang massal BISA menduplikasi -> minta operator set flag manual dulu.
                            jmlFlagGagal += 1
                            gagalBeruntun = 0
                            pesanDetail.AppendLine("- KRITIS (set flag manual sebelum kirim ulang): jurnal DIBUAT (ref " & refferenceID & ") tapi flag GAGAL -> " & rowTransaksi("JenisTransaksi").ToString & " (" & keteranganJenisBayar & ") " & Format(rowTransaksi("Tgl Transaksi"), "dd/MM/yyyy") & " : " & exJurnal.Message)
                        Else
                            ' Jurnal BELUM dibuat. Klasifikasi aman/hilang DITUNDA sampai akhir loop: baru setelah
                            ' semua baris diproses kita tahu apakah ada saudara segrup yang sukses men-set flag
                            ' (bisa diproses sebelum ATAU sesudah baris ini).
                            gagalBeruntun += 1
                            gagalKeys.Add(grupKey)
                            gagalInfo.Add(rowTransaksi("JenisTransaksi").ToString & " (" & keteranganJenisBayar & ") " & Format(rowTransaksi("Tgl Transaksi"), "dd/MM/yyyy") & " : " & exJurnal.Message)

                            ' Stop dini bila gagal-buat beruntun (kemungkinan server jurnal down)
                            If gagalBeruntun >= 5 Then
                                pesanDetail.AppendLine("... dihentikan: 5 kegagalan beruntun, kemungkinan server jurnal bermasalah.")
                                Exit For
                            End If
                        End If
                    End Try
            Next
        End If

        ' Rekonsiliasi akhir baris gagal-buat-jurnal: bila grup-nya ternyata SUDAH ke-flag transaksi lain
        ' segrup (jenis bayar sama, tgl/poli sama, beda kartu/mesin), kirim ulang akan MELEWATI baris ini
        ' (FlagJurnal... IS NULL tak lagi terpenuhi) -> jurnalnya HILANG -> harus dibuat manual, bukan "aman".
        For i As Integer = 0 To gagalKeys.Count - 1
            If flaggedKeys.Contains(gagalKeys(i)) Then
                jmlHilang += 1
                pesanDetail.AppendLine("- HILANG (BUAT JURNAL MANUAL; flag sudah ke-set transaksi lain segrup, kirim ulang TIDAK memprosesnya): " & gagalInfo(i))
            Else
                jmlGagal += 1
                pesanDetail.AppendLine("- GAGAL buat jurnal (aman dikirim ulang): " & gagalInfo(i))
            End If
        Next

        ' P1.1: tampilkan SATU ringkasan di akhir (bukan MsgBox per baris)
        If jmlGagal > 0 OrElse jmlDilewati > 0 OrElse jmlFlagGagal > 0 OrElse jmlHilang > 0 Then
            MsgBox("Kirim jurnal selesai." & vbCrLf & _
                   "Sukses   : " & jmlSukses & vbCrLf & _
                   "Gagal    : " & jmlGagal & "  (flag tetap NULL, aman dikirim ulang)" & vbCrLf & _
                   "Hilang   : " & jmlHilang & "  (gagal buat jurnal TAPI flag sudah ke-set transaksi lain segrup — BUAT JURNAL MANUAL, jangan kirim ulang)" & vbCrLf & _
                   "Kritis   : " & jmlFlagGagal & "  (jurnal dibuat tapi flag gagal — set flag manual dulu)" & vbCrLf & _
                   "Dilewati : " & jmlDilewati & vbCrLf & vbCrLf & _
                   "Detail:" & vbCrLf & pesanDetail.ToString(), _
                   MsgBoxStyle.Exclamation, "Hasil Kirim Jurnal")
        End If
    End Sub

    ' Set flag jurnal dengan retry. UPDATE-nya idempoten (WHERE FlagJurnalHarian... IS NULL) & ke DB lokal yang andal.
    ' Bila flag berhasil, aggregate ini tak akan muncul lagi di run berikut -> kirim ulang TIDAK menduplikasi jurnal.
    ' Melempar exception bila tetap gagal setelah 3x -> pemanggil menandainya KRITIS (perlu set flag manual).
    Private Sub SetFlagJurnalDenganRetry(ByVal rowTransaksi As DataRow, ByVal refferenceID As String, ByVal jenisPoli As String, ByVal keteranganJenisBayar As String)
        Dim tgl As String = Format(rowTransaksi("Tgl Transaksi"), "yyyyMMdd")
        Dim jenisBayar As String = rowTransaksi("JenisBayar").ToString
        Dim jenisTransaksi As String = rowTransaksi("JenisTransaksi").ToString.Trim
        Dim lastEx As Exception = Nothing

        For attempt As Integer = 1 To 3
            Try
                If jenisTransaksi = "POLI" Then
                    UpdateFlagPoliUmum(tgl, refferenceID, jenisPoli, jenisBayar, keteranganJenisBayar)
                ElseIf jenisTransaksi = "GIGI" Then
                    UpdateFlagPoliGigi(tgl, refferenceID, jenisPoli, jenisBayar, keteranganJenisBayar)
                ElseIf jenisTransaksi = "LAB" Then
                    UpdateFlagLaboratorium(tgl, refferenceID, jenisPoli, jenisBayar, keteranganJenisBayar)
                End If
                Return ' sukses
            Catch ex As Exception
                lastEx = ex
                System.Threading.Thread.Sleep(500)
            End Try
        Next

        Throw New Exception("Gagal set flag jurnal setelah 3x percobaan. " & lastEx.Message)
    End Sub

    Private Sub UpdateFlagPoliUmum(ByVal tglTransaksi As String, ByVal flagJurnal As String, ByVal namaPoli As String, ByVal jenisBayar As String, ByVal stringFlag As String)
        Dim query As New StringBuilder()

        query.AppendLine("UPDATE TransaksiPoli")
        query.AppendLine("SET FlagJurnalHarian" & stringFlag & " = '" & flagJurnal & "'")
        query.AppendLine("WHERE CONVERT(DATE, TglPoli) = '" & tglTransaksi & "'")
        query.AppendLine("  AND Deleted = 0")
        query.AppendLine("  AND FlagJurnalHarian" & stringFlag & " IS NULL")
        query.AppendLine("  AND TransPoliID IN (")
        query.AppendLine("      SELECT tp.TransPoliID")
        query.AppendLine("      FROM TransaksiPoliBayar tb")
        query.AppendLine("      INNER JOIN TransaksiPoli tp ON tp.TransPoliID = tb.TransPoliID")
        query.AppendLine("      WHERE tb.JenisBayar = '" & jenisBayar & "' AND tb.JumlahBayar > 0")
        query.AppendLine("        AND tp.NoUrut IN (")
        query.AppendLine("            SELECT td.NoUrut")
        query.AppendLine("            FROM TransaksiDetail td")
        If tipeJurnal = "Poli" Then
            query.AppendLine("            WHERE td.JenisPoli = '" & namaPoli & "'")
        End If
        query.AppendLine("        )")
        query.AppendLine("      UNION ALL")
        query.AppendLine("      SELECT tp.TransPoliID")
        query.AppendLine("      FROM TransaksiPoliBayarAwal tba")
        query.AppendLine("      INNER JOIN TransaksiPoli tp ON tp.TransPoliID = tba.TransPoliID")
        query.AppendLine("      WHERE tba.JenisBayar = '" & jenisBayar & "' AND tba.JumlahBayar > 0")
        query.AppendLine("        AND tp.NoUrut IN (")
        query.AppendLine("            SELECT td.NoUrut")
        query.AppendLine("            FROM TransaksiDetail td")
        If tipeJurnal = "Poli" Then
            query.AppendLine("            WHERE td.JenisPoli = '" & namaPoli & "'")
        End If
        query.AppendLine("        )")
        query.AppendLine("  )")

        ' Exec Query
        WriteSqlChecked(query.ToString())
    End Sub

    Private Sub UpdateFlagPoliGigi(ByVal tglTransaksi As String, ByVal flagJurnal As String, ByVal namaPoli As String, ByVal jenisBayar As String, ByVal stringFlag As String)
        Dim query As New StringBuilder()

        query.AppendLine("UPDATE TransaksiGigi")
        query.AppendLine("SET FlagJurnalHarian" & stringFlag & " = '" & flagJurnal & "'")
        query.AppendLine("WHERE CONVERT(DATE, TglGigi) = '" & tglTransaksi & "'")
        query.AppendLine("  AND Deleted = 0")
        query.AppendLine("  AND FlagJurnalHarian" & stringFlag & " IS NULL")
        query.AppendLine("  AND TransGigiID IN (")
        query.AppendLine("      SELECT tp.TransGigiID")
        query.AppendLine("      FROM TransaksiGigiBayar tb")
        query.AppendLine("      INNER JOIN TransaksiGigi tp ON tp.TransGigiID = tb.TransGigiID")
        query.AppendLine("      WHERE tb.JenisBayar = '" & jenisBayar & "' AND tb.JumlahBayar > 0")
        query.AppendLine("        AND tp.NoUrut IN (")
        query.AppendLine("            SELECT td.NoUrut")
        query.AppendLine("            FROM TransaksiDetail td")
        If tipeJurnal = "Poli" Then
            query.AppendLine("            WHERE td.JenisPoli = '" & namaPoli & "'")
        End If
        query.AppendLine("        )")
        query.AppendLine("      UNION ALL")
        query.AppendLine("      SELECT tp.TransGigiID")
        query.AppendLine("      FROM TransaksiGigiBayarAwal tba")
        query.AppendLine("      INNER JOIN TransaksiGigi tp ON tp.TransGigiID = tba.TransGigiID")
        query.AppendLine("      WHERE tba.JenisBayar = '" & jenisBayar & "' AND tba.JumlahBayar > 0")
        query.AppendLine("        AND tp.NoUrut IN (")
        query.AppendLine("            SELECT td.NoUrut")
        query.AppendLine("            FROM TransaksiDetail td")
        If tipeJurnal = "Poli" Then
            query.AppendLine("            WHERE td.JenisPoli = '" & namaPoli & "'")
        End If
        query.AppendLine("        )")
        query.AppendLine("  )")

        ' Exec Query
        WriteSqlChecked(query.ToString())
    End Sub

    Private Sub UpdateFlagLaboratorium(ByVal tglTransaksi As String, ByVal flagJurnal As String, ByVal namaPoli As String, ByVal jenisBayar As String, ByVal stringFlag As String)
        Dim query As New StringBuilder()

        query.AppendLine("UPDATE TransaksiLab")
        query.AppendLine("SET FlagJurnalHarian" & stringFlag & " = '" & flagJurnal & "'")
        query.AppendLine("WHERE CONVERT(DATE, TglLab) = '" & tglTransaksi & "'")
        query.AppendLine("  AND Deleted = 0 AND SendKeu IS NOT NULL")
        query.AppendLine("  AND FlagJurnalHarian" & stringFlag & " IS NULL")
        query.AppendLine("  AND TransLabID IN (")
        query.AppendLine("      SELECT tb.TransLabID")
        query.AppendLine("      FROM TransaksiLabBayar tb")
        query.AppendLine("      WHERE tb.JenisBayar = '" & jenisBayar & "' AND tb.JumlahBayar > 0")
        query.AppendLine("  )")

        ' Exec Query
        WriteSqlChecked(query.ToString())
    End Sub

    Function GetDataUploadJurnalPoliUmum() As DataTable
        Dim tglTransaksiAwal As Date = DateTimePickerTanggalTransaksiAwal.Value
        Dim tglTransaksiAkhir As Date = DateTimePickerTanggalTransaksiAkhir.Value
        Dim tblResult As New DataTable

        Dim query As New StringBuilder()

        query.AppendLine("WITH FilteredTransPoli AS (")
        query.AppendLine("    SELECT TransPoliID, FlagJurnalHarianTagihan, FlagJurnalHarianTunai, FlagJurnalHarianDebet, FlagJurnalHarianKredit")
        query.AppendLine("    FROM TransaksiPoli")
        ' Filter per-jenis-bayar: ikutkan transaksi bila MINIMAL satu flag masih NULL (bukan harus keempatnya NULL)
        query.AppendLine("    WHERE (CONVERT(DATE, TglPoli) BETWEEN '" & Format(tglTransaksiAwal.Date, "yyyyMMdd") & "' AND '" & Format(tglTransaksiAkhir.Date, "yyyyMMdd") & "') AND Deleted = 0 AND (FlagJurnalHarianTagihan IS NULL OR FlagJurnalHarianTunai IS NULL OR FlagJurnalHarianDebet IS NULL OR FlagJurnalHarianKredit IS NULL)")
        query.AppendLine(")")
        If tipeJurnal = "Poli" Then
            query.AppendLine("SELECT SUM(tblBayar.JumlahBayar - tblBayar.Kembali) AS 'JumlahBayar', tblBayar.JenisBayar, tblBayar.kartu, tblBayar.mesin, d.JenisPoli, d.JenisTransaksi, CONVERT(DATE, t.TglPoli) AS 'Tgl Transaksi'")
        ElseIf tipeJurnal = "Transaksi" Then
            query.AppendLine("SELECT SUM(tblBayar.JumlahBayar - tblBayar.Kembali) AS 'JumlahBayar', tblBayar.JenisBayar, tblBayar.kartu, tblBayar.mesin, d.JenisTransaksi, CONVERT(DATE, t.TglPoli) AS 'Tgl Transaksi'")
        End If
        query.AppendLine("FROM (")
        query.AppendLine("    SELECT tb.TransPoliID, tb.JenisBayar, tb.JumlahBayar, tb.Kembali, tb.kartu, tb.mesin")
        query.AppendLine("    FROM TransaksiPoliBayar tb")
        query.AppendLine("    INNER JOIN FilteredTransPoli ft ON tb.TransPoliID = ft.TransPoliID")
        query.AppendLine("    WHERE tb.JumlahBayar > 0 AND tb.JenisBayar IN ('TAGIHAN', 'TUNAI', 'DEBET', 'CARD')")
        query.AppendLine("      AND ((tb.JenisBayar = 'TUNAI' AND ft.FlagJurnalHarianTunai IS NULL) OR (tb.JenisBayar = 'DEBET' AND ft.FlagJurnalHarianDebet IS NULL) OR (tb.JenisBayar = 'CARD' AND ft.FlagJurnalHarianKredit IS NULL) OR (tb.JenisBayar = 'TAGIHAN' AND ft.FlagJurnalHarianTagihan IS NULL))")
        query.AppendLine("    UNION ALL")
        query.AppendLine("    SELECT tba.TransPoliID, tba.JenisBayar, tba.JumlahBayar, tba.Kembali, tba.kartu, tba.mesin")
        query.AppendLine("    FROM TransaksiPoliBayarAwal tba")
        query.AppendLine("    INNER JOIN FilteredTransPoli ft ON tba.TransPoliID = ft.TransPoliID")
        query.AppendLine("    WHERE tba.JumlahBayar > 0 AND tba.JenisBayar IN ('TAGIHAN', 'TUNAI', 'DEBET', 'CARD')")
        query.AppendLine("      AND ((tba.JenisBayar = 'TUNAI' AND ft.FlagJurnalHarianTunai IS NULL) OR (tba.JenisBayar = 'DEBET' AND ft.FlagJurnalHarianDebet IS NULL) OR (tba.JenisBayar = 'CARD' AND ft.FlagJurnalHarianKredit IS NULL) OR (tba.JenisBayar = 'TAGIHAN' AND ft.FlagJurnalHarianTagihan IS NULL))")
        query.AppendLine(") AS tblBayar")
        query.AppendLine("LEFT JOIN TransaksiPoli t ON tblBayar.TransPoliID = t.TransPoliID")
        query.AppendLine("LEFT JOIN TransaksiDetail d ON t.NoUrut = d.NoUrut")
        If tipeJurnal = "Poli" Then
            query.AppendLine("GROUP BY CONVERT(DATE, t.TglPoli), d.JenisPoli, d.JenisTransaksi, tblBayar.JenisBayar, tblBayar.kartu, tblBayar.mesin")
        ElseIf tipeJurnal = "Transaksi" Then
            query.AppendLine("GROUP BY CONVERT(DATE, t.TglPoli), d.JenisTransaksi, tblBayar.JenisBayar, tblBayar.kartu, tblBayar.mesin")
        End If

        tblResult = ReadSQL(query.ToString())

        Return tblResult
    End Function

    Function GetDataUploadJurnalPoliGigi() As DataTable
        Dim tglTransaksiAwal As Date = DateTimePickerTanggalTransaksiAwal.Value
        Dim tglTransaksiAkhir As Date = DateTimePickerTanggalTransaksiAkhir.Value
        Dim tblResult As New DataTable

        Dim query As New StringBuilder()

        query.AppendLine("WITH FilteredTransGigi AS (")
        query.AppendLine("    SELECT TransGigiID, FlagJurnalHarianTagihan, FlagJurnalHarianTunai, FlagJurnalHarianDebet, FlagJurnalHarianKredit")
        query.AppendLine("    FROM TransaksiGigi")
        query.AppendLine("    WHERE (CONVERT(DATE, TglGigi) BETWEEN '" & Format(tglTransaksiAwal.Date, "yyyyMMdd") & "' AND '" & Format(tglTransaksiAkhir.Date, "yyyyMMdd") & "')")
        ' Filter per-jenis-bayar: minimal satu flag NULL (bukan harus keempatnya)
        query.AppendLine("      AND Deleted = 0 AND (FlagJurnalHarianTagihan IS NULL OR FlagJurnalHarianTunai IS NULL OR FlagJurnalHarianDebet IS NULL OR FlagJurnalHarianKredit IS NULL)")
        query.AppendLine(")")
        If tipeJurnal = "Poli" Then
            query.AppendLine("SELECT SUM(tblBayar.JumlahBayar - tblBayar.Kembali) AS 'JumlahBayar', tblBayar.JenisBayar, tblBayar.kartu, tblBayar.mesin, d.JenisPoli, d.JenisTransaksi, CONVERT(DATE, t.TglGigi) AS 'Tgl Transaksi'")
        ElseIf tipeJurnal = "Transaksi" Then
            query.AppendLine("SELECT SUM(tblBayar.JumlahBayar - tblBayar.Kembali) AS 'JumlahBayar', tblBayar.JenisBayar, tblBayar.kartu, tblBayar.mesin, d.JenisTransaksi, CONVERT(DATE, t.TglGigi) AS 'Tgl Transaksi'")
        End If
        query.AppendLine("FROM (")
        query.AppendLine("    SELECT tb.TransGigiID, tb.JenisBayar, tb.JumlahBayar, tb.Kembali, tb.kartu, tb.mesin")
        query.AppendLine("    FROM TransaksiGigiBayar tb")
        query.AppendLine("    INNER JOIN FilteredTransGigi ft ON tb.TransGigiID = ft.TransGigiID")
        query.AppendLine("    WHERE tb.JumlahBayar > 0 AND tb.JenisBayar IN ('TAGIHAN', 'TUNAI', 'DEBET', 'CARD')")
        query.AppendLine("      AND ((tb.JenisBayar = 'TUNAI' AND ft.FlagJurnalHarianTunai IS NULL) OR (tb.JenisBayar = 'DEBET' AND ft.FlagJurnalHarianDebet IS NULL) OR (tb.JenisBayar = 'CARD' AND ft.FlagJurnalHarianKredit IS NULL) OR (tb.JenisBayar = 'TAGIHAN' AND ft.FlagJurnalHarianTagihan IS NULL))")
        query.AppendLine("    UNION ALL")
        query.AppendLine("    SELECT tba.TransGigiID, tba.JenisBayar, tba.JumlahBayar, tba.Kembali, tba.kartu, tba.mesin")
        query.AppendLine("    FROM TransaksiGigiBayarAwal tba")
        query.AppendLine("    INNER JOIN FilteredTransGigi ft ON tba.TransGigiID = ft.TransGigiID")
        query.AppendLine("    WHERE tba.JumlahBayar > 0 AND tba.JenisBayar IN ('TAGIHAN', 'TUNAI', 'DEBET', 'CARD')")
        query.AppendLine("      AND ((tba.JenisBayar = 'TUNAI' AND ft.FlagJurnalHarianTunai IS NULL) OR (tba.JenisBayar = 'DEBET' AND ft.FlagJurnalHarianDebet IS NULL) OR (tba.JenisBayar = 'CARD' AND ft.FlagJurnalHarianKredit IS NULL) OR (tba.JenisBayar = 'TAGIHAN' AND ft.FlagJurnalHarianTagihan IS NULL))")
        query.AppendLine(") AS tblBayar")
        query.AppendLine("LEFT JOIN TransaksiGigi t ON tblBayar.TransGigiID = t.TransGigiID")
        query.AppendLine("LEFT JOIN TransaksiDetail d ON t.NoUrut = d.NoUrut")
        If tipeJurnal = "Poli" Then
            query.AppendLine("GROUP BY CONVERT(DATE, t.TglGigi), d.JenisPoli, d.JenisTransaksi, tblBayar.JenisBayar, tblBayar.kartu, tblBayar.mesin")
        ElseIf tipeJurnal = "Transaksi" Then
            query.AppendLine("GROUP BY CONVERT(DATE, t.TglGigi), d.JenisTransaksi, tblBayar.JenisBayar, tblBayar.kartu, tblBayar.mesin")
        End If

        tblResult = ReadSQL(query.ToString())

        Return tblResult
    End Function

    Function GetDataUploadJurnalLaboratorium() As DataTable
        Dim tglTransaksiAwal As Date = DateTimePickerTanggalTransaksiAwal.Value
        Dim tglTransaksiAkhir As Date = DateTimePickerTanggalTransaksiAkhir.Value
        Dim tblResult As New DataTable

        Dim query As New StringBuilder()

        query.AppendLine("WITH FilteredTransLab AS (")
        query.AppendLine("    SELECT TransLabID, FlagJurnalHarianTagihan, FlagJurnalHarianTunai, FlagJurnalHarianDebet, FlagJurnalHarianKredit")
        query.AppendLine("    FROM TransaksiLab")
        query.AppendLine("    WHERE (CONVERT(DATE, TglLab) BETWEEN '" & Format(tglTransaksiAwal.Date, "yyyyMMdd") & "' AND '" & Format(tglTransaksiAkhir.Date, "yyyyMMdd") & "')")
        ' Filter per-jenis-bayar: minimal satu flag NULL (bukan harus keempatnya)
        query.AppendLine("      AND Deleted = 0 AND (FlagJurnalHarianTagihan IS NULL OR FlagJurnalHarianTunai IS NULL OR FlagJurnalHarianDebet IS NULL OR FlagJurnalHarianKredit IS NULL)")
        query.AppendLine("      AND SendKeu IS NOT NULL")
        query.AppendLine(")")
        If tipeJurnal = "Poli" Then
            query.AppendLine("SELECT SUM(tblBayar.JumlahBayar - tblBayar.Kembali) AS 'JumlahBayar', tblBayar.JenisBayar, tblBayar.kartu, tblBayar.mesin, d.JenisPoli, d.JenisTransaksi, CONVERT(DATE, t.TglLab) AS 'Tgl Transaksi'")
        ElseIf tipeJurnal = "Transaksi" Then
            query.AppendLine("SELECT SUM(tblBayar.JumlahBayar - tblBayar.Kembali) AS 'JumlahBayar', tblBayar.JenisBayar, tblBayar.kartu, tblBayar.mesin, d.JenisTransaksi, CONVERT(DATE, t.TglLab) AS 'Tgl Transaksi'")
        End If
        query.AppendLine("FROM (")
        query.AppendLine("    SELECT tb.TransLabID, tb.JenisBayar, tb.JumlahBayar, tb.Kembali, tb.kartu, tb.mesin")
        query.AppendLine("    FROM TransaksiLabBayar tb")
        query.AppendLine("    INNER JOIN FilteredTransLab ft ON tb.TransLabID = ft.TransLabID")
        query.AppendLine("    WHERE tb.JumlahBayar > 0 AND tb.JenisBayar IN ('TAGIHAN', 'TUNAI', 'DEBET', 'CARD')")
        query.AppendLine("      AND ((tb.JenisBayar = 'TUNAI' AND ft.FlagJurnalHarianTunai IS NULL) OR (tb.JenisBayar = 'DEBET' AND ft.FlagJurnalHarianDebet IS NULL) OR (tb.JenisBayar = 'CARD' AND ft.FlagJurnalHarianKredit IS NULL) OR (tb.JenisBayar = 'TAGIHAN' AND ft.FlagJurnalHarianTagihan IS NULL))")
        query.AppendLine(") AS tblBayar")
        query.AppendLine("LEFT JOIN TransaksiLab t ON tblBayar.TransLabID = t.TransLabID")
        query.AppendLine("LEFT JOIN TransaksiDetail d ON t.NoUrut = d.NoUrut")
        If tipeJurnal = "Poli" Then
            query.AppendLine("GROUP BY CONVERT(DATE, t.TglLab), d.JenisPoli, d.JenisTransaksi, tblBayar.JenisBayar, tblBayar.kartu, tblBayar.mesin")
        ElseIf tipeJurnal = "Transaksi" Then
            query.AppendLine("GROUP BY CONVERT(DATE, t.TglLab), d.JenisTransaksi, tblBayar.JenisBayar, tblBayar.kartu, tblBayar.mesin")
        End If
        tblResult = ReadSQL(query.ToString())

        Return tblResult
    End Function
End Class