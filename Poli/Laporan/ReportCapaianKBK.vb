Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Drawing
Imports Newtonsoft.Json
Imports Newtonsoft
Imports System.Security.Cryptography
Imports System.Collections.Generic
Imports Newtonsoft.Json.Linq
Imports System.Web.UI.WebControls
Imports System.Text

Public Class ReportCapaianKBK
    Private formName As String = "CAPAIAN KBK"
    Private userInput As String = PoliKlinik.Lbluser.Text

    Private Sub PanelTop_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PanelTop.Paint
        Try
            e.Graphics.DrawString(formName, New Font("Arial", 12, FontStyle.Bold), Brushes.White, New Point(10, 10))
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub CreateDataGridViewKunjunganPasien()
        ' Add columns
        DataGridViewDaftarKunjungan.Columns.Add("ColumnTanggalProses", "Tanggal Proses")
        DataGridViewDaftarKunjungan.Columns.Add("ColumnBulanPeriode", "Bulan Periode")
        DataGridViewDaftarKunjungan.Columns.Add("ColumnTahun", "Tahun")
        DataGridViewDaftarKunjungan.Columns.Add("ColumnJumlahKontak", "Jumlah Kontak")
        DataGridViewDaftarKunjungan.Columns.Add("ColumnPasienTerdaftar", "Pasien Terdaftar")
        'DataGridViewDaftarKunjungan.Columns.Add("ColumnJenisKelamin", "Jenis Kelamin")
        'DataGridViewDaftarKunjungan.Columns.Add("ColumnDiagnosa", "Diagnosa")
        'DataGridViewDaftarKunjungan.Columns.Add("ColumnLayanan", "Layanan")
        'DataGridViewDaftarKunjungan.Columns.Add("ColumnNamaDokter", "Dokter")
        'DataGridViewDaftarKunjungan.Columns.Add("ColumnTagihan", "Tagihan")

        ' Setting
        DataGridViewDaftarKunjungan.DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)
        DataGridViewDaftarKunjungan.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold)
        DataGridViewDaftarKunjungan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        ' Mengatur lebar maksimum untuk kolom kode diagnosa
        'Dim increamentDataColumn As DataGridViewColumn = DataGridViewDaftarKunjungan.Columns("ColumnTanggalProses")
        'increamentDataColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None ' Menonaktifkan penyesuaian otomatis
        'increamentDataColumn.Width = 50 ' Mengatur lebar maksimums
    End Sub

    Private Sub Find_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Control.CheckForIllegalCrossThreadCalls = False
            Me.MdiParent = PoliKlinik
            Me.Dock = DockStyle.Fill

            ' Clear data
            SetAwal()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SetAwal()
        ' Set ComboBox Bulan
        ComboBoxBulan.Items.Clear()
        LoadComboBoxBulan()

        ' Set ComboBox Tahun
        ComboBoxTahun.Items.Clear()
        LoadComboBoxTahun()

        ' Create DataGridView
        CreateDataGridViewKunjunganPasien()
        ButtonSearchDaftarPeserta.PerformClick()

        With DataGridViewDaftarKunjungan.Columns
            .Clear()
            .Add("TanggalProses", "Tanggal Proses")
            .Add("JumlahKontak", "Jumlah Kontak")
            .Add("PesertaTerdaftar", "Peserta Terdaftar")
            .Add("Angka Kontak", "Angka Kontak")
            .Add("Rating AK", "Rating AK")
            .Add("Bobot AK", "Bobot AK")
            .Add("Capaian AK", "Capaian AK")
            .Add("Rujukan Non Spesialis", "Rujukan Non Spesialis")
            .Add("JumlahRujukan", "Jumlah Rujukan")
        End With
    End Sub

    Private Sub LoadComboBoxBulan()
        ComboBoxBulan.Items.Add("Januari")
        ComboBoxBulan.Items.Add("Februari")
        ComboBoxBulan.Items.Add("Maret")
        ComboBoxBulan.Items.Add("April")
        ComboBoxBulan.Items.Add("Mei")
        ComboBoxBulan.Items.Add("Juni")
        ComboBoxBulan.Items.Add("Juli")
        ComboBoxBulan.Items.Add("Agustus")
        ComboBoxBulan.Items.Add("September")
        ComboBoxBulan.Items.Add("Oktober")
        ComboBoxBulan.Items.Add("November")
        ComboBoxBulan.Items.Add("Desember")

        ' Set default bulan
        Dim bulanSekarang As Integer = DateTime.Now.Month
        ComboBoxBulan.SelectedIndex = bulanSekarang - 1 ' Indeks dimulai dari 0, sedangkan bulan dimulai dari 1
    End Sub

    Private Sub LoadComboBoxTahun()
        Dim vartahun As Integer = Date.Today.Year
        ComboBoxTahun.Items.Add(vartahun)
        ComboBoxTahun.Items.Add(vartahun - 1)
        ComboBoxTahun.Items.Add(vartahun - 2)
        ComboBoxTahun.Items.Add(vartahun - 3)
        ComboBoxTahun.Items.Add(vartahun - 4)

        ' Set default tahun
        Dim tahunSekarang As Integer = DateTime.Now.Year
        ComboBoxTahun.Text = tahunSekarang.ToString()
    End Sub

    Private Sub SearchDataDaftarKunjungan()
        If ValidasiSearch() = True Then
            DataGridViewDaftarKunjungan.Rows.Clear()

            Dim Tbls As DataTable = LoadDataDaftarKunjungan()

            If Tbls.Rows.Count > 0 Then
                DataGridViewDaftarKunjungan.Rows.Clear()

                For Each Trow As DataRow In Tbls.Rows
                    Dim tanggalProses As String = Format(Trow("TanggalProses"))
                    Dim jumlahKontak As Integer = Convert.ToInt32(Trow("JumlahKontak"))
                    Dim pesertaTerdaftar As Integer = Convert.ToInt32(Trow("PesertaTerdaftar"))
                    Dim angkaKontak As Double = If(IsDBNull(Trow("AngkaKontak")), 0, Convert.ToDouble(Trow("AngkaKontak")))
                    Dim jumlahRujukan As Integer = Convert.ToInt32(Trow("JumlahRujukan"))
                    Dim ratingAk As Integer = Convert.ToInt32(Trow("RatingAK"))
                    Dim bobotAk As String = Convert.ToInt32(Trow("BobotAK")).ToString() & "%"
                    Dim capaianAk As Double = If(IsDBNull(Trow("CapaianAK")), 0, Convert.ToDouble(Trow("CapaianAK")))
                    Dim jmlRujukanNon As Integer = If(IsDBNull(Trow("RujukanNonSpesialis")), 0, Convert.ToInt32(Trow("RujukanNonSpesialis")))

                    DataGridViewDaftarKunjungan.Rows.Add(tanggalProses, jumlahKontak, pesertaTerdaftar, Math.Round(angkaKontak, 2), ratingAk, bobotAk, Math.Round(capaianAk, 1), jmlRujukanNon, jumlahRujukan)
                Next

                DataGridViewDaftarKunjungan.CurrentCell = Nothing
            Else
                MsgBox("Data tidak ditemukan.", MsgBoxStyle.Exclamation, "Warning!")
            End If

        End If
    End Sub

    Private Function GetJenisTransaksiByPoli(ByVal namaPoli As String) As String
        ' Hindari karakter petik tunggal error dengan replace (basic sanitasi)
        Dim safePoli As String = namaPoli.Replace("'", "''")

        Dim query As String = "SELECT KodeTransaksi FROM MasterPoliTujuan WHERE NamaPoli = '" & safePoli & "'"
        Dim dt As DataTable = ReadSQL(query)

        If dt.Rows.Count > 0 Then
            Dim kode As String = dt.Rows(0)("KodeTransaksi").ToString().ToUpper()
            If kode = "U" Then
                Return "UMUM"
            ElseIf kode = "G" Then
                Return "GIGI"
            End If
        End If

        Return "" ' Jika tidak ditemukan
    End Function

    Private Sub DataGridViewDaftarKunjungan_CellDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)
        ' Pastikan baris yang diklik valid
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim dgv As DataGridView = CType(sender, DataGridView)

            ' Ambil data dari baris yang diklik
            Dim noUrut As String = dgv.Rows(e.RowIndex).Cells(1).Value.ToString() 'no transaksi
            Dim noRekamMedis As String = dgv.Rows(e.RowIndex).Cells(3).Value.ToString() 'noRM
            Dim tglPelayanan As String = dgv.Rows(e.RowIndex).Cells(2).Value.ToString() 'tgl pelayanan
            Dim namaPoli As String = dgv.Rows(e.RowIndex).Cells(7).Value.ToString() 'jenis poli
            'Dim namaPasien As String = dgv.Rows(e.RowIndex).Cells(4).Value.ToString()
            Dim jenisPoli As String = GetJenisTransaksiByPoli(namaPoli)
            Dim frm As New FormRekamMedisDokter()
            frm.TglAwalFilter = tglPelayanan
            frm.TglAkhirFilter = Date.Now
            frm.SetDataFromPendaftaran(noRekamMedis, noUrut, jenisPoli)
            frm.Show()
            frm.BukaTabRiwayat()
            frm.GetRiwayatKunjungan()
        End If
    End Sub

    'Private Sub DataGridViewKunjunganPasien_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridViewDaftarKunjungan.CellDoubleClick
    '    If e.RowIndex >= 0 Then
    '        ' Set Data Transaksi
    '        Dim jenisUser As String = ReadSQLNIK("SELECT GroupID FROM Users WHERE UserID = '" & userInput & "'")
    '        Dim noTransaksi As String = DataGridViewDaftarKunjungan.Rows(e.RowIndex).Cells("ColumnNoTransaksi").Value.ToString()
    '        Dim jenisPoli As String = DataGridViewDaftarKunjungan.Rows(e.RowIndex).Cells("ColumnJenisPoli").Value.ToString()
    '        Dim noRM As String = DataGridViewDaftarKunjungan.Rows(e.RowIndex).Cells("ColumnNoRM").Value.ToString()
    '        Dim namaPoli As String = DataGridViewDaftarKunjungan.Rows(e.RowIndex).Cells("ColumnNamaPoli").Value.ToString()
    '        Dim noBPJS As String = DataGridViewDaftarKunjungan.Rows(e.RowIndex).Cells("ColumnNoBPJS").Value.ToString()
    '        Dim keluhan As String = DataGridViewDaftarKunjungan.Rows(e.RowIndex).Cells("ColumnKeluhan").Value.ToString()
    '        Dim noNIK As String = DataGridViewDaftarKunjungan.Rows(e.RowIndex).Cells("ColumnNoNIK").Value.ToString()
    '        Dim jenisRawat As String = DataGridViewDaftarKunjungan.Rows(e.RowIndex).Cells("ColumnJenisRawat").Value.ToString()

    '        ' Set Status Transaksi
    '        Dim statusTransaksi As String = GetStatusTransaksi(noTransaksi, jenisPoli)

    '        '# Set data dictionary yang akan dikirim ke form popup
    '        Dim dataDictionary As New Dictionary(Of String, Object)()

    '        If (statusTransaksi = "Belum Diperiksa" And jenisUser.Contains("Dokter") = False) Or (statusTransaksi = "Sudah dilakukan TTV" And jenisUser.Contains("Dokter") = True) Then
    '            dataDictionary.Add("No Transaksi", noTransaksi)
    '            dataDictionary.Add("No RM", noRM)
    '            dataDictionary.Add("Jenis Poli", jenisPoli)
    '            dataDictionary.Add("Status Transaksi", statusTransaksi)
    '            dataDictionary.Add("Jenis User", jenisUser)
    '            dataDictionary.Add("Nama Poli", namaPoli)
    '            dataDictionary.Add("Nomor BPJS", noBPJS)
    '            dataDictionary.Add("Keluhan", keluhan)
    '            dataDictionary.Add("Nomor NIK", noNIK)
    '            dataDictionary.Add("Jenis Rawat", jenisRawat)

    '            dataDictionary.Add("Text Button Start Proses", "Mulai Pemeriksaan")
    '            dataDictionary.Add("Jenis Proses", "Pendaftaran - Pemeriksaan")

    '            DialogBoxPanggilPasien.DataDictionary = dataDictionary
    '            DialogBoxPanggilPasien.ShowDialog()
    '        Else
    '            FormRekamMedisDokter.SetDataFromPendaftaran(noRM, noTransaksi, jenisPoli)
    '            FormRekamMedisDokter.Show()
    '            FormRekamMedisDokter.BringToFront()
    '        End If

    '        ' Harusnya refresh grid terbaru
    '        'FindQuery("Nothing")
    '    End If

    'End Sub

    Function ValidasiSearch() As Boolean
        

        Return True
    End Function

    Private Sub ButtonSearchDaftarPeserta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSearchDaftarPeserta.Click
        SearchDataDaftarKunjungan()
    End Sub

    Private Sub CetakDataDaftarKunjungan()
        Dim Tbls As DataTable = LoadDataDaftarKunjungan()

        If Tbls.Rows.Count > 0 Then
            Report = New XtraReport

            Dim kodePPK As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'KodePPK'")
            Dim namaPPK As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'NamaPPK'")

            Report = XtraReport.FromFile(Application.StartupPath + "\Reports\Klinik\Laporan10Diagnosa.repx", True)
            Report.DataSource = Tbls

            Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelFKTP").Text = namaPPK & " - " & kodePPK
            Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelBulan").Text = ComboBoxBulan.Text & " " & ComboBoxTahun.Text
            'Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelPelayanan").Text = ComboBoxTingkatPelayanan.Text
            'Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelIndikator").Text = ComboBoxIndikator.Text
            'Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelPoli").Text = ComboBoxPoliTujuan.Text

            Report.Bands.Item(BandKind.PageFooter).Controls("lblprinted").Text = Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") & " By " & PoliKlinik.Lbluser.Text

            'Report.RunDesigner()
            Report.ShowPreview()
        Else
            MsgBox("Data tidak ditemukan.", MsgBoxStyle.Exclamation, "Warning!")
        End If
    End Sub

    Function LoadDataDaftarKunjungan() As DataTable
        Dim queryString As String = ""
        Dim queryStringCondition As String = ""
        Dim queryStringJenisKunjungan As String = ""
        Dim kodePPK As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'KodePPK'")

        If ComboBoxTahun.SelectedItem Is Nothing Then
            MsgBox("Pilih tahun terlebih dahulu.")
            Return Nothing
        End If

        Dim tahunTerpilih As String = ComboBoxTahun.SelectedItem.ToString()
        Dim queryBuilder As New StringBuilder()

        ' 1. WITH Bulan
        Dim tahunSekarang As Integer = DateTime.Now.Year
        Dim bulanMaksimal As Integer = 12

        If Val(tahunTerpilih) = tahunSekarang Then
            bulanMaksimal = DateTime.Now.Month
        End If

        queryBuilder.AppendLine("WITH Bulan AS (")
        For bulan As Integer = 1 To bulanMaksimal
            Dim barisUnion As String = "    SELECT " & tahunTerpilih & " AS Tahun, " & bulan & " AS Bulan"
            If bulan < bulanMaksimal Then
                barisUnion &= " UNION ALL"
            End If
            queryBuilder.AppendLine(barisUnion)
        Next

        queryBuilder.AppendLine(")")

        ' 2. PesertaTerdaftar
        queryBuilder.AppendLine(", PesertaTerdaftar AS (")
        queryBuilder.AppendLine("    SELECT ")
        queryBuilder.AppendLine("        B.Tahun,")
        queryBuilder.AppendLine("        B.Bulan,")
        queryBuilder.AppendLine("        COUNT(DISTINCT MPB.NoKartu) AS PesertaTerdaftar")
        queryBuilder.AppendLine("    FROM Bulan B")
        queryBuilder.AppendLine("    LEFT JOIN MasterPasienBPJS MPB ON MPB.KdProviderPst = '" & kodePPK & "'")
        queryBuilder.AppendLine("    LEFT JOIN MasterPasien MP ON MPB.NoKartu = MP.NoKartuBPJS")
        queryBuilder.AppendLine("    WHERE MP.CreatedDate IS NOT NULL AND")
        queryBuilder.AppendLine("          MP.CreatedDate <= CAST(CAST(B.Tahun AS VARCHAR(4)) + '-' +")
        queryBuilder.AppendLine("                              RIGHT('0' + CAST(B.Bulan AS VARCHAR(2)), 2) + '-06' AS DATETIME)")
        queryBuilder.AppendLine("    GROUP BY B.Tahun, B.Bulan")
        queryBuilder.AppendLine(")")

        ' 3. KunjunganKontak
        queryBuilder.AppendLine(", KunjunganKontak AS (")
        queryBuilder.AppendLine("    SELECT ")
        queryBuilder.AppendLine("        YEAR(TD.TanggalTransaksi) AS Tahun,")
        queryBuilder.AppendLine("        MONTH(TD.TanggalTransaksi) AS Bulan,")
        queryBuilder.AppendLine("        TD.IDInternet")
        queryBuilder.AppendLine("    FROM TransaksiDetail TD")
        queryBuilder.AppendLine("    INNER JOIN MasterPasien MP ON TD.IDInternet = MP.NoRekamMedis")
        queryBuilder.AppendLine("    INNER JOIN MasterPasienBPJS MB ON MP.NoKartuBPJS = MB.NoKartu")
        queryBuilder.AppendLine("    WHERE TD.JenisKunjungan IN ('Kunjungan Sakit', 'Kunjungan Sehat')")
        queryBuilder.AppendLine("      AND TD.TanggalTransaksi >= '" & tahunTerpilih & "-01-01'")
        queryBuilder.AppendLine("      AND TD.DeletedDate IS NULL")
        queryBuilder.AppendLine("      AND MB.KdProviderPst = '" & kodePPK & "'")
        queryBuilder.AppendLine("    GROUP BY YEAR(TD.TanggalTransaksi), MONTH(TD.TanggalTransaksi), TD.IDInternet")
        queryBuilder.AppendLine(")")

        ' 4. RujukanVertikal
        queryBuilder.AppendLine(", RujukanVertikal AS (")
        queryBuilder.AppendLine("    SELECT ")
        queryBuilder.AppendLine("        YEAR(TD.TanggalTransaksi) AS Tahun,")
        queryBuilder.AppendLine("        MONTH(TD.TanggalTransaksi) AS Bulan,")
        queryBuilder.AppendLine("        TD.IDInternet")
        queryBuilder.AppendLine("    FROM TransaksiDetail TD")
        queryBuilder.AppendLine("    INNER JOIN MasterPasien MP ON TD.IDInternet = MP.NoRekamMedis")
        queryBuilder.AppendLine("    INNER JOIN MasterPasienBPJS MB ON MP.NoKartuBPJS = MB.NoKartu")
        queryBuilder.AppendLine("    WHERE TD.StatusPulang = 'Rujuk Vertikal'")
        queryBuilder.AppendLine("      AND TD.TanggalTransaksi >= '" & tahunTerpilih & "-01-01'")
        queryBuilder.AppendLine("      AND TD.DeletedDate IS NULL")
        queryBuilder.AppendLine("      AND MB.KdProviderPst = '" & kodePPK & "'")
        queryBuilder.AppendLine("    GROUP BY YEAR(TD.TanggalTransaksi), MONTH(TD.TanggalTransaksi), TD.IDInternet")
        queryBuilder.AppendLine(")")

        ' 5. RujukanNonSpesialis
        queryBuilder.AppendLine(", RujukanNonSpesialis AS (")
        queryBuilder.AppendLine("    SELECT ")
        queryBuilder.AppendLine("        YEAR(TD.TanggalTransaksi) AS Tahun,")
        queryBuilder.AppendLine("        MONTH(TD.TanggalTransaksi) AS Bulan,")
        queryBuilder.AppendLine("        COUNT(*) AS JumlahRujukanNonSpesialis")
        queryBuilder.AppendLine("    FROM TransaksiDetail TD")
        queryBuilder.AppendLine("    INNER JOIN TransaksiDetailRujukan TDR ON TD.NoUrut = TDR.NoTransaksi")
        queryBuilder.AppendLine("    INNER JOIN MasterPasien MP ON TD.IDInternet = MP.NoRekamMedis")
        queryBuilder.AppendLine("    INNER JOIN MasterPasienBPJS MB ON MP.NoKartuBPJS = MB.NoKartu")
        queryBuilder.AppendLine("    WHERE TD.TanggalTransaksi >= '" & tahunTerpilih & "-01-01'")
        queryBuilder.AppendLine("      AND TD.DeletedDate IS NULL")
        queryBuilder.AppendLine("      AND MB.KdProviderPst = '" & kodePPK & "'")
        queryBuilder.AppendLine("      AND ISNULL(TDR.JenisRujukan, '') <> 'Spesialis'")
        queryBuilder.AppendLine("    GROUP BY YEAR(TD.TanggalTransaksi), MONTH(TD.TanggalTransaksi)")
        queryBuilder.AppendLine(")")

        ' 6 Final Select
        queryBuilder.AppendLine("SELECT ")
        queryBuilder.AppendLine("    CONVERT(VARCHAR, CAST(CAST(B.Tahun AS VARCHAR(4)) + '-' + ")
        queryBuilder.AppendLine("    RIGHT('0' + CAST(B.Bulan AS VARCHAR(2)), 2) + '-06' AS DATETIME), 103) AS TanggalProses,")
        queryBuilder.AppendLine("    ISNULL(KC.JumlahKontak, 0) AS JumlahKontak,")
        queryBuilder.AppendLine("    ISNULL(PT.PesertaTerdaftar, 0) AS PesertaTerdaftar,")
        queryBuilder.AppendLine("    ISNULL(RV.JumlahRujukan, 0) AS JumlahRujukan,")
        queryBuilder.AppendLine("    ISNULL(RNS.JumlahNonSpesialis, 0) AS [Jumlah Rujukan Kasus Non Spesialis],")
        queryBuilder.AppendLine("    ISNULL((CAST(ISNULL(KC.JumlahKontak, 0) AS FLOAT) / NULLIF(ISNULL(PT.PesertaTerdaftar, 0), 0)) * 1000, 0) AS AngkaKontak,")
        queryBuilder.AppendLine("    CASE ")
        queryBuilder.AppendLine("        WHEN (CAST(ISNULL(KC.JumlahKontak, 0) AS FLOAT) / NULLIF(ISNULL(PT.PesertaTerdaftar, 0), 0)) * 1000 >= 150 THEN 4")
        queryBuilder.AppendLine("        WHEN (CAST(ISNULL(KC.JumlahKontak, 0) AS FLOAT) / NULLIF(ISNULL(PT.PesertaTerdaftar, 0), 0)) * 1000 > 145 THEN 3")
        queryBuilder.AppendLine("        WHEN (CAST(ISNULL(KC.JumlahKontak, 0) AS FLOAT) / NULLIF(ISNULL(PT.PesertaTerdaftar, 0), 0)) * 1000 > 140 THEN 2")
        queryBuilder.AppendLine("        ELSE 1")
        queryBuilder.AppendLine("    END AS RatingAK,")
        queryBuilder.AppendLine("    40 AS BobotAK,")
        queryBuilder.AppendLine("    (CASE ")
        queryBuilder.AppendLine("        WHEN (CAST(ISNULL(KC.JumlahKontak, 0) AS FLOAT) / NULLIF(ISNULL(PT.PesertaTerdaftar, 0), 0)) * 1000 >= 150 THEN 4")
        queryBuilder.AppendLine("        WHEN (CAST(ISNULL(KC.JumlahKontak, 0) AS FLOAT) / NULLIF(ISNULL(PT.PesertaTerdaftar, 0), 0)) * 1000 > 145 THEN 3")
        queryBuilder.AppendLine("        WHEN (CAST(ISNULL(KC.JumlahKontak, 0) AS FLOAT) / NULLIF(ISNULL(PT.PesertaTerdaftar, 0), 0)) * 1000 > 140 THEN 2")
        queryBuilder.AppendLine("        ELSE 1")
        queryBuilder.AppendLine("    END * 0.4) AS CapaianAK")

        queryBuilder.AppendLine("FROM Bulan B")
        queryBuilder.AppendLine("LEFT JOIN (")
        queryBuilder.AppendLine("    SELECT Tahun, Bulan, COUNT(DISTINCT IDInternet) AS JumlahKontak")
        queryBuilder.AppendLine("    FROM KunjunganKontak")
        queryBuilder.AppendLine("    GROUP BY Tahun, Bulan")
        queryBuilder.AppendLine(") KC ON KC.Tahun = B.Tahun AND KC.Bulan = B.Bulan")

        queryBuilder.AppendLine("LEFT JOIN (")
        queryBuilder.AppendLine("    SELECT Tahun, Bulan, COUNT(DISTINCT IDInternet) AS JumlahRujukan")
        queryBuilder.AppendLine("    FROM RujukanVertikal")
        queryBuilder.AppendLine("    GROUP BY Tahun, Bulan")
        queryBuilder.AppendLine(") RV ON RV.Tahun = B.Tahun AND RV.Bulan = B.Bulan")

        queryBuilder.AppendLine("LEFT JOIN (")
        queryBuilder.AppendLine("    SELECT Tahun, Bulan, COUNT(DISTINCT IDInternet) AS JumlahNonSpesialis")
        queryBuilder.AppendLine("    FROM RujukanNonSpesialis")
        queryBuilder.AppendLine("    GROUP BY Tahun, Bulan")
        queryBuilder.AppendLine(") RNS ON RNS.Tahun = B.Tahun AND RNS.Bulan = B.Bulan")

        queryBuilder.AppendLine("LEFT JOIN PesertaTerdaftar PT ON PT.Tahun = B.Tahun AND PT.Bulan = B.Bulan")

        queryBuilder.AppendLine("ORDER BY B.Tahun, B.Bulan")

        Dim Tbls As DataTable = ReadSQL(queryBuilder.ToString())

        Return Tbls
    End Function

    Private Sub ButtonCariPeserta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        FindPasien.SetForm("ReportDaftarKunjunganPeserta")
        FindPasien.ShowDialog()
    End Sub

    Public Sub LoadPasien(ByVal Trow As DataRowView)
        'Set Data From Pencarian

    End Sub

    'Private Sub DataGridViewDaftarDiagnosa_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewDaftarKunjungan.CellFormatting
    '    ' Hanya melakukan perubahan pada kolom nomor urut
    '    If e.ColumnIndex = 0 AndAlso e.RowIndex >= 0 Then ' Kolom nomor urut adalah kolom indeks 0
    '        e.Value = e.RowIndex + 1 ' Nomor urut dimulai dari 1
    '    End If
    'End Sub
End Class