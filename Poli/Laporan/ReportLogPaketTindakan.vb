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
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports Microsoft.Office.Interop

Public Class ReportLogPaketTindakan
    Private formName As String = "RIWAYAT PAKET TINDAKAN"
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

    End Sub

    'COBA COBA
    'Private Sub SearchDataRiwayatPaketReguler()

    '    Dim dtHeader As DataTable = GetDataHeaderPaketReguler()
    '    Dim dtDetail As DataTable = GetDataDetailPaketReguler()

    '    Dim ds As New DataSet()

    '    dtHeader.TableName = "Header"
    '    dtDetail.TableName = "Detail"

    '    ds.Tables.Add(dtHeader.Copy())
    '    ds.Tables.Add(dtDetail.Copy())

    '    ' 🔥 WAJIB: disable dulu
    '    ds.EnforceConstraints = False

    '    ' RELASI (HANYA 2 KOLOM)
    '    Dim parentCols() As DataColumn = { _
    '        ds.Tables("Header").Columns("PaketID"), _
    '        ds.Tables("Header").Columns("KodeVoucher") _
    '    }

    '    Dim childCols() As DataColumn = { _
    '        ds.Tables("Detail").Columns("PaketID"), _
    '        ds.Tables("Detail").Columns("KodeVoucher") _
    '    }

    '    ds.Relations.Add("MasterDetail", parentCols, childCols)

    '    ' aktifkan lagi
    '    ds.EnforceConstraints = True

    '    ' BIND GRID
    '    GridControlRiwayatPaket.DataSource = ds.Tables("Header")

    '    Dim mainView As DevExpress.XtraGrid.Views.Grid.GridView = CType(GridControlRiwayatPaket.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

    '    mainView.OptionsBehavior.Editable = False

    '    If mainView.Columns("PaketID") IsNot Nothing Then mainView.Columns("PaketID").Visible = False
    '    If mainView.Columns("KodeVoucher") IsNot Nothing Then mainView.Columns("KodeVoucher").Visible = False

    'End Sub

    Private Sub SearchDataRiwayatPaketReguler()
        Dim queryStringHeader As New StringBuilder()
        Dim queryStringDetail As New StringBuilder()

        'queryStringHeader.AppendLine("SELECT ")
        'queryStringHeader.AppendLine("    tblPasien.Nama, ")
        'queryStringHeader.AppendLine("    tblPaket.NoRekamMedis AS 'No RM', ")
        'queryStringHeader.AppendLine("    tblPaket.NamaPaket AS 'Nama Paket', ")
        'queryStringHeader.AppendLine("    STUFF(( ")
        'queryStringHeader.AppendLine("        SELECT ', ' + tblPaketDetail.NamaTindakan ")
        'queryStringHeader.AppendLine("        FROM TransaksiPaketTindakanDetail tblPaketDetail ")
        'queryStringHeader.AppendLine("        WHERE tblPaketDetail.PaketID = tblPaket.PaketID AND tblPaketDetail.KodeVoucher = tblPaket.KodeVoucher ")
        'queryStringHeader.AppendLine("        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') ")
        'queryStringHeader.AppendLine("    AS 'Nama Tindakan', ")
        'queryStringHeader.AppendLine("    tblPaket.JenisPaket AS 'Jenis Paket', ")
        'queryStringHeader.AppendLine("    tblPaket.Kuota, ")
        'queryStringHeader.AppendLine("    tblPaket.TanggalMulaiPaket AS 'Tgl Mulai', ")
        'queryStringHeader.AppendLine("    tblPaket.TanggalAkhirPaket AS 'Tgl Berakhir', ")
        'queryStringHeader.AppendLine("    tblPaket.PaketID, ")
        'queryStringHeader.AppendLine("    tblPaket.KodeVoucher, ")
        'queryStringHeader.AppendLine("    0 AS 'TindakanID' ")
        'queryStringHeader.AppendLine("FROM TransaksiPaketTindakan tblPaket ")
        'queryStringHeader.AppendLine("LEFT JOIN MasterPasien tblPasien ON tblPasien.NoRekamMedis = tblPaket.NoRekamMedis ")
        'queryStringHeader.AppendLine("WHERE tblPaket.JenisPaket = 'Paket Reguler'")
        ''queryStringHeader.AppendLine("AND tblPaket.NoRekamMedis = 'L003-0000129'")
        'queryStringHeader.AppendLine("ORDER BY tblPaket.PaketID;")

        ' Ambil Data Header
        'Dim tblRiwayatHeader As DataTable = ReadSQL(queryStringHeader.ToString())
        Dim tblRiwayatHeader As DataTable = GetDataHeaderPaketReguler()

        'queryStringDetail.AppendLine("SELECT tblLog.NoTransaksi, tblPasien.Nama, tblLog.NoRekamMedis AS 'No RM', ")
        'queryStringDetail.AppendLine("       tblLog.KodeVoucher, tblLog.PaketID, ")
        'queryStringDetail.AppendLine("       tblLog.TanggalTransaksi AS 'Tgl Transaksi'")
        'queryStringDetail.AppendLine("FROM LogPenggunaanPaket tblLog")
        'queryStringDetail.AppendLine("INNER JOIN TransaksiPaketTindakan tblPaket ")
        'queryStringDetail.AppendLine("    ON tblLog.KodeVoucher = tblPaket.KodeVoucher ")
        'queryStringDetail.AppendLine("    AND tblLog.PaketID = tblPaket.PaketID")
        'queryStringDetail.AppendLine("INNER JOIN MasterPasien tblPasien ")
        'queryStringDetail.AppendLine("    ON tblLog.NoRekamMedis = tblPasien.NoRekamMedis")
        'queryStringDetail.AppendLine("WHERE tblPaket.JenisPaket = 'Paket Reguler'")
        ''queryStringDetail.AppendLine(" AND tblPaket.NoRekamMedis = 'L003-0000129'")
        'queryStringDetail.AppendLine("ORDER BY tblLog.PaketID, tblLog.KodeVoucher;")

        ' Ambil Data Detail
        'Dim tblRiwayatDetail As DataTable = ReadSQL(queryStringDetail.ToString())
        Dim tblRiwayatDetail As DataTable = GetDataDetailPaketReguler()

        ' Inisialisasi DataSet
        Dim DataSet As New DataSet()

        ' Pastikan tabel memiliki nama yang sesuai sebelum dimasukkan ke DataSet
        tblRiwayatHeader.TableName = "TransaksiPaketTindakan"
        tblRiwayatDetail.TableName = "LogPenggunaanPaket"

        ' Tambahkan ke DataSet
        DataSet.Tables.Add(tblRiwayatHeader.Copy())
        DataSet.Tables.Add(tblRiwayatDetail.Copy())

        ' Definisikan array kolom untuk relasi master (Parent Table)
        'Dim parentColumns() As DataColumn = { _
        '    DataSet.Tables("TransaksiPaketTindakan").Columns("PaketID"), _
        '        DataSet.Tables("TransaksiPaketTindakan").Columns("KodeVoucher") _
        '}

        '' Definisikan array kolom untuk relasi detail (Child Table)
        'Dim childColumns() As DataColumn = { _
        '    DataSet.Tables("LogPenggunaanPaket").Columns("PaketID"), _
        '        DataSet.Tables("LogPenggunaanPaket").Columns("KodeVoucher") _
        '}

        Dim parentColumns() As DataColumn = { _
            DataSet.Tables("TransaksiPaketTindakan").Columns("PaketID"), _
            DataSet.Tables("TransaksiPaketTindakan").Columns("KodeVoucher") _
        }

        Dim childColumns() As DataColumn = { _
            DataSet.Tables("LogPenggunaanPaket").Columns("PaketID"), _
            DataSet.Tables("LogPenggunaanPaket").Columns("KodeVoucher") _
        }

        ' Buat DataRelation dengan 2 kolom
        Dim relation As New DataRelation("MasterDetail", parentColumns, childColumns)

        ' Tambahkan relasi ke DataSet
        DataSet.Relations.Add(relation)

        ' Bind Data ke GridControl
        GridControlRiwayatPaket.DataSource = DataSet.Tables("TransaksiPaketTindakan")

        ' Ambil GridView utama dari GridControl
        Dim mainView As DevExpress.XtraGrid.Views.Grid.GridView = CType(GridControlRiwayatPaket.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

        ' Set ReadOnly
        mainView.OptionsBehavior.Editable = False
        mainView.OptionsSelection.EnableAppearanceFocusedRow = False
        mainView.OptionsSelection.EnableAppearanceFocusedCell = False

        ' Sembunyikan kolom PaketID dan KodeVoucher di GridView utama
        If mainView.Columns("PaketID") IsNot Nothing Then mainView.Columns("PaketID").Visible = False
        If mainView.Columns("KodeVoucher") IsNot Nothing Then mainView.Columns("KodeVoucher").Visible = False
        If mainView.Columns("TindakanID") IsNot Nothing Then mainView.Columns("TindakanID").Visible = False

        ' Tambahkan handler untuk event ViewRegistered
        AddHandler GridControlRiwayatPaket.ViewRegistered, AddressOf GridControlRiwayatPaket_ViewRegistered

        AddHandler GridViewRiwayatPaket.RowStyle, AddressOf GridViewRiwayatPaket_RowStyle

        GridViewRiwayatPaket.ExpandAllGroups()
        GridViewRiwayatPaket.BestFitColumns()
    End Sub

    'OLD
    Private Sub SearchDataRiwayatPaketPerTindakan()

        Dim queryStringHeader As New StringBuilder()
        Dim queryStringDetail As New StringBuilder()

        Dim tglAwal As Date = TglAwalTransaksi.Value
        Dim tglAkhir As Date = TglAkhirTransaksi.Value

        ' =========================
        ' HEADER
        ' =========================
        queryStringHeader.AppendLine("SELECT tblPasien.Nama, tblPaket.NoRekamMedis AS 'No RM', tblPaket.NoTransaksi, ")
        queryStringHeader.AppendLine("       tblPaket.NamaPaket AS 'Nama Paket', tblPaketDetail.NamaTindakan AS 'Nama Tindakan', ")
        queryStringHeader.AppendLine("       tblPaket.JenisPaket AS 'Jenis Paket', ")
        queryStringHeader.AppendLine("       tblPaketDetail.KuotaTindakan AS 'Kuota', ")
        queryStringHeader.AppendLine("       tblPaket.TanggalMulaiPaket AS 'Tgl Mulai', ")
        queryStringHeader.AppendLine("       tblPaket.TanggalAkhirPaket AS 'Tgl Berakhir', ")
        queryStringHeader.AppendLine("       tblPaket.PaketID, tblPaket.KodeVoucher, tblPaketDetail.TindakanID ")
        queryStringHeader.AppendLine("FROM TransaksiPaketTindakanDetail tblPaketDetail ")
        queryStringHeader.AppendLine("INNER JOIN TransaksiPaketTindakan tblPaket ")
        queryStringHeader.AppendLine("       ON tblPaket.PaketID = tblPaketDetail.PaketID ")
        queryStringHeader.AppendLine("       AND tblPaket.KodeVoucher = tblPaketDetail.KodeVoucher ")
        queryStringHeader.AppendLine("INNER JOIN MasterPasien tblPasien ")
        queryStringHeader.AppendLine("       ON tblPasien.NoRekamMedis = tblPaket.NoRekamMedis ")
        queryStringHeader.AppendLine("WHERE tblPaket.JenisPaket = 'Paket Per-Tindakan' ")
        queryStringHeader.AppendLine("AND tblPaket.DeletedDate IS NULL ")

        ' Filter No RM
        If TextBoxNomorRekamMedis.Text <> "" Then
            queryStringHeader.AppendLine("AND tblPaket.NoRekamMedis = '" & TextBoxNomorRekamMedis.Text & "' ")
        End If

        ' 🔥 FILTER TANGGAL HEADER (FIX)
        If Not CheckBoxFilterTanggal.Checked Then
            queryStringHeader.AppendLine("AND tblPaket.TanggalMulaiPaket <= '" & Format(tglAkhir, "yyyy-MM-dd") & " 23:59:59'")
            queryStringHeader.AppendLine("AND tblPaket.TanggalAkhirPaket >= '" & Format(tglAwal, "yyyy-MM-dd") & " 00:00:00'")
        End If

        Dim tblRiwayatHeader As DataTable = ReadSQL(queryStringHeader.ToString())

        ' =========================
        ' DETAIL
        ' =========================
        queryStringDetail.AppendLine("SELECT tblLog.NoTransaksi, tblPasien.Nama, tblLog.NoRekamMedis AS 'No RM', ")
        queryStringDetail.AppendLine("       tblLog.KodeVoucher, tblLog.PaketID, tblLog.TindakanID, ")
        queryStringDetail.AppendLine("       tblLog.TanggalTransaksi AS 'Tgl Transaksi'")
        queryStringDetail.AppendLine("FROM LogPenggunaanPaket tblLog")
        queryStringDetail.AppendLine("INNER JOIN TransaksiPaketTindakan tblPaket ")
        queryStringDetail.AppendLine("    ON tblLog.KodeVoucher = tblPaket.KodeVoucher ")
        queryStringDetail.AppendLine("    AND tblLog.PaketID = tblPaket.PaketID")
        queryStringDetail.AppendLine("INNER JOIN MasterPasien tblPasien ")
        queryStringDetail.AppendLine("    ON tblLog.NoRekamMedis = tblPasien.NoRekamMedis")
        queryStringDetail.AppendLine("WHERE tblPaket.JenisPaket = 'Paket Per-Tindakan' ")
        queryStringDetail.AppendLine("AND tblLog.DeletedDate IS NULL ")

        ' Filter No RM
        If TextBoxNomorRekamMedis.Text <> "" Then
            queryStringDetail.AppendLine("AND tblPaket.NoRekamMedis = '" & TextBoxNomorRekamMedis.Text & "' ")
        End If

        ' 🔥 FILTER TANGGAL DETAIL (FIX INCLUSIVE)
        If Not CheckBoxFilterTanggal.Checked Then
            queryStringDetail.AppendLine("AND tblLog.TanggalTransaksi >= '" & Format(tglAwal, "yyyy-MM-dd") & " 00:00:00'")
            queryStringDetail.AppendLine("AND tblLog.TanggalTransaksi < DATEADD(DAY, 1, '" & Format(tglAkhir, "yyyy-MM-dd") & "')")
        End If

        ' 🔥 PASTIKAN DETAIL PUNYA HEADER (ANTI ERROR RELATION)
        queryStringDetail.AppendLine("AND EXISTS (")
        queryStringDetail.AppendLine("    SELECT 1 FROM TransaksiPaketTindakanDetail D")
        queryStringDetail.AppendLine("    WHERE D.PaketID = tblLog.PaketID")
        queryStringDetail.AppendLine("    AND D.TindakanID = tblLog.TindakanID")
        queryStringDetail.AppendLine("    AND D.KodeVoucher = tblLog.KodeVoucher")
        queryStringDetail.AppendLine(")")

        queryStringDetail.AppendLine("ORDER BY tblLog.PaketID, tblLog.KodeVoucher, tblLog.TanggalTransaksi")

        Dim tblRiwayatDetail As DataTable = ReadSQL(queryStringDetail.ToString())

        ' =========================
        ' DATASET
        ' =========================
        Dim ds As New DataSet()

        tblRiwayatHeader.TableName = "TransaksiPaketTindakan"
        tblRiwayatDetail.TableName = "LogPenggunaanPaket"

        ds.Tables.Add(tblRiwayatHeader.Copy())
        ds.Tables.Add(tblRiwayatDetail.Copy())

        ' 🔥 WAJIB (ANTI ERROR RELATION)
        ds.EnforceConstraints = False

        ' RELASI MASTER DETAIL
        Dim parentColumns() As DataColumn = { _
            ds.Tables("TransaksiPaketTindakan").Columns("PaketID"), _
            ds.Tables("TransaksiPaketTindakan").Columns("TindakanID"), _
            ds.Tables("TransaksiPaketTindakan").Columns("KodeVoucher") _
        }

        Dim childColumns() As DataColumn = { _
            ds.Tables("LogPenggunaanPaket").Columns("PaketID"), _
            ds.Tables("LogPenggunaanPaket").Columns("TindakanID"), _
            ds.Tables("LogPenggunaanPaket").Columns("KodeVoucher") _
        }

        Dim relation As New DataRelation("MasterDetail", parentColumns, childColumns)
        ds.Relations.Add(relation)

        ' =========================
        ' BIND GRID
        ' =========================
        GridControlRiwayatPaket.DataSource = ds.Tables("TransaksiPaketTindakan")

        Dim mainView As DevExpress.XtraGrid.Views.Grid.GridView = CType(GridControlRiwayatPaket.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

        mainView.OptionsBehavior.Editable = False

        If mainView.Columns("PaketID") IsNot Nothing Then mainView.Columns("PaketID").Visible = False
        If mainView.Columns("KodeVoucher") IsNot Nothing Then mainView.Columns("KodeVoucher").Visible = False
        If mainView.Columns("TindakanID") IsNot Nothing Then mainView.Columns("TindakanID").Visible = False

        AddHandler GridControlRiwayatPaket.ViewRegistered, AddressOf GridControlRiwayatPaket_ViewRegistered
        AddHandler GridViewRiwayatPaket.RowStyle, AddressOf GridViewRiwayatPaket_RowStyle

        GridViewRiwayatPaket.ExpandAllGroups()
        GridViewRiwayatPaket.BestFitColumns()

    End Sub

    ' Method untuk menangani event ViewRegistered
    Private Sub GridControlRiwayatPaket_ViewRegistered(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.ViewOperationEventArgs)
        Dim detailView As DevExpress.XtraGrid.Views.Grid.GridView = TryCast(e.View, DevExpress.XtraGrid.Views.Grid.GridView)
        If detailView IsNot Nothing Then
            ' Sembunyikan kolom PaketID dan KodeVoucher di GridView detail
            If detailView.Columns("PaketID") IsNot Nothing Then detailView.Columns("PaketID").Visible = False
            If detailView.Columns("KodeVoucher") IsNot Nothing Then detailView.Columns("KodeVoucher").Visible = False
            If detailView.Columns("TindakanID") IsNot Nothing Then detailView.Columns("TindakanID").Visible = False
        End If
    End Sub

    Private Sub GridViewRiwayatPaket_RowStyle(ByVal sender As Object, ByVal e As RowStyleEventArgs)
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

        If e.RowHandle >= 0 Then
            Dim jenisPaket As String = view.GetRowCellValue(e.RowHandle, "Jenis Paket").ToString()
            Dim kuota As Integer = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "Kuota"))

            ' Jika kuota tinggal sedikit (misalnya ≤ 2), warnai dengan merah muda
            If kuota = 0 Then
                e.Appearance.BackColor = Color.LightCoral
                'e.Appearance.ForeColor = Color.DarkRed
                'e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
            Else
                e.Appearance.BackColor = Color.LightGreen
            End If
        End If
    End Sub

    Function Checking() As Boolean
        'If ComboBoxPoliTujuan.SelectedIndex = -1 Then
        '    MsgBox("Pilih poli tujuan terlebih dahulu.", MsgBoxStyle.Exclamation, "Warning!")
        '    Return False
        'End If

        Return True
    End Function

    Private Sub ButtonSearchRiwayatPaket_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSearchRiwayatPaket.Click
        If RadioButtonOptionPaketReguler.Checked = True Then
            SearchDataRiwayatPaketReguler()
        ElseIf RadioButtonOptionPaketPerTindakan.Checked = True Then
            SearchDataRiwayatPaketPerTindakan()
        End If
    End Sub

    Private Sub ButtonCetakDiagnosa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CetakDataDiagnosa()
    End Sub

    Private Sub CetakDataDiagnosa()
        'Dim Tbls As DataTable = LoadDataDiagnosa()

        'If Tbls.Rows.Count > 0 Then
        '    Report = New XtraReport

        '    Dim kodePPK As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'KodePPK'")
        '    Dim namaPPK As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'NamaPPK'")

        '    Report = XtraReport.FromFile(Application.StartupPath + "\Reports\Klinik\Laporan10Diagnosa.repx", True)
        '    Report.DataSource = Tbls

        '    'Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelFKTP").Text = namaPPK & " - " & kodePPK
        '    'Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelBulan").Text = ComboBoxBulan.Text & " " & ComboBoxTahun.Text
        '    'Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelPelayanan").Text = ComboBoxTingkatPelayanan.Text
        '    'Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelIndikator").Text = ComboBoxIndikator.Text
        '    'Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelPoli").Text = ComboBoxPoliTujuan.Text

        '    Report.Bands.Item(BandKind.PageFooter).Controls("lblprinted").Text = Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") & " By " & PoliKlinik.Lbluser.Text

        '    'Report.RunDesigner()
        '    Report.ShowPreview()
        'Else
        '    MsgBox("Data tidak ditemukan.", MsgBoxStyle.Exclamation, "Warning!")
        'End If
    End Sub

    'Function LoadDataDiagnosa() As DataTable
    '    Dim queryString As String = ""

    '    ' Set Bulan & Tahun
    '    Dim bulanSearch As String = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(ComboBoxBulan.Text, "Januari", "01"), "Februari", "02"), "Maret", "03"), "April", "04"), "Mei", "05"), "Juni", "06"), "Juli", "07"), "Agustus", "08"), "September", "09"), "Oktober", "10"), "November", "11"), "Desember", "12")
    '    Dim tahunSearch As String = ComboBoxTahun.Text

    '    ' Set Tingkat Pelayanan
    '    Dim tingkatPelayananSearch As String = ""
    '    If ComboBoxTingkatPelayanan.Text <> "Semua" Then
    '        tingkatPelayananSearch = " AND JenisRawat = '" & ComboBoxTingkatPelayanan.Text & "'"
    '    End If

    '    ' Set Indikator
    '    Dim indikatorSearch As String = ""
    '    If ComboBoxIndikator.Text = "Kunjungan" Then
    '        indikatorSearch = " AND StatusPulang LIKE '%Berobat Jalan%'"
    '    ElseIf ComboBoxIndikator.Text = "Rujukan" Then
    '        indikatorSearch = " AND StatusPulang LIKE '%Rujuk%'"
    '    End If

    '    ' Set Jenis Poli
    '    Dim jenisPoliSearch As String = ""
    '    If ComboBoxPoliTujuan.Text <> "Semua Transaksi" Then
    '        jenisPoliSearch = " AND JenisPoli = '" & ComboBoxPoliTujuan.Text & "'"
    '    End If

    '    queryString = "SELECT TOP(10) ROW_NUMBER() OVER (ORDER BY COUNT(KodeDiagnosa) DESC) AS 'No', KodeDiagnosa AS 'Kode Diagnosa', KeteranganDiagnosa AS 'Nama Diagnosa', COUNT(KodeDiagnosa) AS 'Jumlah' FROM TransaksiDetailDiagnosa " & _
    '                  "WHERE DeletedDate IS NULL AND NoTransaksi IN (SELECT NoUrut FROM TransaksiDetail WHERE YEAR(TanggalTransaksi) = '" & tahunSearch & "' AND MONTH(TanggalTransaksi) = '" & bulanSearch & "'" & tingkatPelayananSearch & indikatorSearch & jenisPoliSearch & ") " & _
    '                  "GROUP BY KodeDiagnosa, KeteranganDiagnosa " & _
    '                  "ORDER BY COUNT(KodeDiagnosa) DESC"

    '    Dim Tbls As DataTable = ReadSQL(queryString)

    '    Return Tbls
    'End Function

    Private Sub ButtonCariPasien_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCariPasien.Click
        FindPasien.SetForm("ReportLogPaketTindakan")
        FindPasien.ShowDialog()
    End Sub

    Public Sub LoadPasien(ByVal Trow As DataRowView)
        'Set Data From Pencarian
        TextBoxNomorRekamMedis.Text = Trow("RM").ToString
        TextBoxNamaRekamMedis.Text = Trow("Nama").ToString
    End Sub

    'NEW
    Function GetDataHeaderPaketReguler() As DataTable

        Dim tblResult As New DataTable
        Dim query As New StringBuilder()

        query.AppendLine("SELECT ")
        query.AppendLine("    tblPasien.Nama, ")
        query.AppendLine("    tblPaket.NoRekamMedis AS [No RM], ")
        query.AppendLine("    tblPaket.NoTransaksi AS NoTransaksi, ")
        query.AppendLine("    tblPaket.NamaPaket AS [Nama Paket], ")

        query.AppendLine("    STUFF(( ")
        query.AppendLine("        SELECT ', ' + d.NamaTindakan ")
        query.AppendLine("        FROM TransaksiPaketTindakanDetail d ")
        query.AppendLine("        WHERE d.PaketID = tblPaket.PaketID ")
        query.AppendLine("        AND d.KodeVoucher = tblPaket.KodeVoucher ")
        query.AppendLine("        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ")
        query.AppendLine("    AS [Nama Tindakan], ")

        query.AppendLine("    tblPaket.JenisPaket AS [Jenis Paket], ")
        query.AppendLine("    tblPaket.Kuota, ")
        query.AppendLine("    tblPaket.TanggalMulaiPaket AS [Tgl Mulai], ")
        query.AppendLine("    tblPaket.TanggalAkhirPaket AS [Tgl Berakhir], ")
        query.AppendLine("    tblPaket.PaketID, ")
        query.AppendLine("    tblPaket.KodeVoucher, ")
        query.AppendLine("    0 AS TindakanID ")

        query.AppendLine("FROM TransaksiPaketTindakan tblPaket ")

        ' JOIN ke log supaya filter konsisten
        query.AppendLine("INNER JOIN LogPenggunaanPaket tblLog ")
        query.AppendLine("    ON tblLog.PaketID = tblPaket.PaketID ")
        query.AppendLine("    AND tblLog.KodeVoucher = tblPaket.KodeVoucher ")

        query.AppendLine("LEFT JOIN MasterPasien tblPasien ")
        query.AppendLine("    ON tblPasien.NoRekamMedis = tblPaket.NoRekamMedis ")

        query.AppendLine("WHERE tblPaket.JenisPaket = 'Paket Reguler' ")
        query.AppendLine("AND tblPaket.DeletedDate IS NULL ")
        query.AppendLine("AND tblLog.DeletedDate IS NULL ")

        If TextBoxNomorRekamMedis.Text <> "" Then
            query.AppendLine("AND tblPaket.NoRekamMedis = '" & TextBoxNomorRekamMedis.Text & "'")
        End If

        If Not CheckBoxFilterTanggal.Checked Then
            query.AppendLine("AND tblLog.TanggalTransaksi >= '" & _
            Format(TglAwalTransaksi.Value, "yyyy-MM-dd") & "'")

            query.AppendLine("AND tblLog.TanggalTransaksi < DATEADD(DAY,1,'" & _
            Format(TglAkhirTransaksi.Value, "yyyy-MM-dd") & "')")
        End If

        query.AppendLine("GROUP BY ")
        query.AppendLine("    tblPasien.Nama, ")
        query.AppendLine("    tblPaket.NoRekamMedis, ")
        query.AppendLine("    tblPaket.NoTransaksi, ")
        query.AppendLine("    tblPaket.NamaPaket, ")
        query.AppendLine("    tblPaket.JenisPaket, ")
        query.AppendLine("    tblPaket.Kuota, ")
        query.AppendLine("    tblPaket.TanggalMulaiPaket, ")
        query.AppendLine("    tblPaket.TanggalAkhirPaket, ")
        query.AppendLine("    tblPaket.PaketID, ")
        query.AppendLine("    tblPaket.KodeVoucher ")

        query.AppendLine("ORDER BY tblPaket.PaketID")

        tblResult = ReadSQL(query.ToString())
        Return tblResult

    End Function

    'Function GetDataHeaderPaketReguler() As DataTable

    '    Dim tblResult As New DataTable
    '    Dim query As New StringBuilder()

    '    query.AppendLine("SELECT ")
    '    query.AppendLine("    MP.Nama, ")
    '    query.AppendLine("    LPP.NoRekamMedis AS [No RM], ")
    '    query.AppendLine("    LPP.NoTransaksi AS [No Transaksi], ")
    '    query.AppendLine("    MPT.NamaPaket AS [Nama Paket], ")

    '    query.AppendLine("    STUFF(( ")
    '    query.AppendLine("        SELECT ', ' + D.NamaTindakan ")
    '    query.AppendLine("        FROM MasterPaketTindakanDetail D ")
    '    query.AppendLine("        WHERE D.PaketID = LPP.PaketID ")
    '    query.AppendLine("        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ")
    '    query.AppendLine("    AS [Nama Tindakan], ")

    '    query.AppendLine("    'Paket Reguler' AS [Jenis Paket], ")
    '    query.AppendLine("    MPT.Kuota, ")

    '    query.AppendLine("    MIN(LPP.TanggalTransaksi) AS [Tgl Mulai], ")
    '    query.AppendLine("    MAX(LPP.TanggalTransaksi) AS [Tgl Berakhir], ")

    '    query.AppendLine("    LPP.PaketID, ")
    '    query.AppendLine("    LPP.KodeVoucher, ")
    '    query.AppendLine("    0 AS [TindakanID], ")

    '    query.AppendLine("    COUNT(*) AS TotalPemakaian ")

    '    query.AppendLine("FROM LogPenggunaanPaket LPP ")

    '    query.AppendLine("INNER JOIN MasterPasien MP ")
    '    query.AppendLine("    ON MP.NoRekamMedis = LPP.NoRekamMedis ")

    '    query.AppendLine("INNER JOIN MasterPaketTindakan MPT ")
    '    query.AppendLine("    ON MPT.PaketID = LPP.PaketID ")

    '    query.AppendLine("WHERE LPP.DeletedDate IS NULL ")
    '    query.AppendLine("AND MPT.JenisPaket = 'Paket Reguler' ")

    '    If TextBoxNomorRekamMedis.Text <> "" Then
    '        query.AppendLine("AND LPP.NoRekamMedis = '" & TextBoxNomorRekamMedis.Text & "'")
    '    End If

    '    If Not CheckBoxFilterTanggal.Checked Then
    '        query.AppendLine("AND CONVERT(DATE, LPP.TanggalTransaksi) BETWEEN '" _
    '                         & Format(TglAwalTransaksi.Value, "yyyyMMdd") & "' AND '" _
    '                         & Format(TglAkhirTransaksi.Value, "yyyyMMdd") & "'")
    '    End If

    '    query.AppendLine("GROUP BY ")
    '    query.AppendLine("    MP.Nama, ")
    '    query.AppendLine("    LPP.NoRekamMedis, ")
    '    query.AppendLine("    LPP.NoTransaksi, ")
    '    query.AppendLine("    MPT.NamaPaket, ")
    '    query.AppendLine("    MPT.Kuota, ")
    '    query.AppendLine("    LPP.PaketID, ")
    '    query.AppendLine("    LPP.KodeVoucher ")

    '    query.AppendLine("ORDER BY LPP.PaketID ")

    '    tblResult = ReadSQL(query.ToString())

    '    Return tblResult

    'End Function

    'OLD
    'Function GetDataHeaderPaketReguler() As DataTable
    '    Dim tglTransaksiAwal As Date = TglAwalTransaksi.Value
    '    Dim tglTransaksiAkhir As Date = TglAkhirTransaksi.Value
    '    Dim tblResult As New DataTable

    '    Dim query As New StringBuilder()

    '    query.AppendLine("SELECT ")
    '    query.AppendLine("    tblPasien.Nama, ")
    '    query.AppendLine("    tblPaket.NoRekamMedis AS 'No RM', ")
    '    query.AppendLine("    tblPaket.NoTransaksi AS 'No Transaksi', ")
    '    query.AppendLine("    tblPaket.NamaPaket AS 'Nama Paket', ")
    '    query.AppendLine("    STUFF(( ")
    '    query.AppendLine("        SELECT ', ' + tblPaketDetail.NamaTindakan ")
    '    query.AppendLine("        FROM TransaksiPaketTindakanDetail tblPaketDetail ")
    '    query.AppendLine("        WHERE tblPaketDetail.PaketID = tblPaket.PaketID AND tblPaketDetail.KodeVoucher = tblPaket.KodeVoucher ")
    '    query.AppendLine("        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') ")
    '    query.AppendLine("    AS 'Nama Tindakan', ")
    '    query.AppendLine("    tblPaket.JenisPaket AS 'Jenis Paket', ")
    '    query.AppendLine("    tblPaket.Kuota, ")
    '    query.AppendLine("    tblPaket.TanggalMulaiPaket AS 'Tgl Mulai', ")
    '    query.AppendLine("    tblPaket.TanggalAkhirPaket AS 'Tgl Berakhir', ")
    '    query.AppendLine("    tblPaket.PaketID, ")
    '    query.AppendLine("    tblPaket.KodeVoucher, ")
    '    query.AppendLine("    0 AS 'TindakanID' ")
    '    query.AppendLine("FROM TransaksiPaketTindakan tblPaket ")
    '    query.AppendLine("LEFT JOIN MasterPasien tblPasien ON tblPasien.NoRekamMedis = tblPaket.NoRekamMedis ")
    '    query.AppendLine("WHERE tblPaket.JenisPaket = 'Paket Reguler'")
    '    If TextBoxNomorRekamMedis.Text <> "" Then
    '        query.AppendLine("AND tblPaket.NoRekamMedis = '" & TextBoxNomorRekamMedis.Text & "'")
    '    End If
    '    query.AppendLine("AND (CONVERT(DATE, tblPaket.TanggalMulaiPaket) BETWEEN '" & Format(tglTransaksiAwal.Date, "yyyyMMdd") & "' AND '" & Format(tglTransaksiAkhir.Date, "yyyyMMdd") & "') AND tblPaket.DeletedDate IS NULL")
    '    query.AppendLine("ORDER BY tblPaket.PaketID;")

    '    tblResult = ReadSQL(query.ToString())

    '    Return tblResult
    'End Function

    'NEW
    Function GetDataDetailPaketReguler() As DataTable

        Dim tblResult As New DataTable
        Dim query As New StringBuilder()

        query.AppendLine("SELECT ")
        query.AppendLine("    tblLog.NoTransaksi, ")
        query.AppendLine("    tblPasien.Nama, ")
        query.AppendLine("    tblLog.NoRekamMedis AS [No RM], ")
        query.AppendLine("    tblLog.KodeVoucher, ")
        query.AppendLine("    tblLog.PaketID, ")
        query.AppendLine("    tblLog.TanggalTransaksi AS [Tgl Transaksi] ")

        query.AppendLine("FROM LogPenggunaanPaket tblLog ")

        query.AppendLine("INNER JOIN TransaksiPaketTindakan tblPaket ")
        query.AppendLine("    ON tblLog.KodeVoucher = tblPaket.KodeVoucher ")
        query.AppendLine("    AND tblLog.PaketID = tblPaket.PaketID ")

        query.AppendLine("INNER JOIN MasterPasien tblPasien ")
        query.AppendLine("    ON tblLog.NoRekamMedis = tblPasien.NoRekamMedis ")

        query.AppendLine("WHERE tblPaket.JenisPaket = 'Paket Reguler' ")
        query.AppendLine("AND tblLog.DeletedDate IS NULL ")

        If TextBoxNomorRekamMedis.Text <> "" Then
            query.AppendLine("AND tblPaket.NoRekamMedis = '" & TextBoxNomorRekamMedis.Text & "'")
        End If

        If Not CheckBoxFilterTanggal.Checked Then
            query.AppendLine("AND tblLog.TanggalTransaksi >= '" & _
            Format(TglAwalTransaksi.Value, "yyyy-MM-dd") & "'")

            query.AppendLine("AND tblLog.TanggalTransaksi < DATEADD(DAY,1,'" & _
            Format(TglAkhirTransaksi.Value, "yyyy-MM-dd") & "')")
        End If

        ' 🔥 TAMBAHKAN DI SINI
        query.AppendLine("AND EXISTS (")
        query.AppendLine("    SELECT 1 FROM TransaksiPaketTindakan h ")
        query.AppendLine("    WHERE h.PaketID = tblLog.PaketID ")
        query.AppendLine("    AND h.KodeVoucher = tblLog.KodeVoucher")
        query.AppendLine(")")

        query.AppendLine("ORDER BY tblLog.PaketID, tblLog.KodeVoucher")

        tblResult = ReadSQL(query.ToString())
        Return tblResult

    End Function

    'Function GetDataDetailPaketReguler() As DataTable

    '    Dim tblResult As New DataTable
    '    Dim query As New StringBuilder()

    '    ' 🔹 Format tanggal dulu biar rapi
    '    Dim tglAwal As String = Format(TglAwalTransaksi.Value, "yyyy-MM-dd")
    '    Dim tglAkhir As String = Format(TglAkhirTransaksi.Value, "yyyy-MM-dd")

    '    query.AppendLine("SELECT ")
    '    query.AppendLine("    tblLog.NoTransaksi, ")
    '    query.AppendLine("    tblPasien.Nama, ")
    '    query.AppendLine("    tblLog.NoRekamMedis AS [No RM], ")
    '    query.AppendLine("    tblLog.KodeVoucher, ")
    '    query.AppendLine("    tblLog.PaketID, ")
    '    query.AppendLine("    tblLog.TanggalTransaksi AS [Tgl Transaksi] ")

    '    query.AppendLine("FROM LogPenggunaanPaket tblLog ")

    '    query.AppendLine("INNER JOIN MasterPasien tblPasien ")
    '    query.AppendLine("    ON tblLog.NoRekamMedis = tblPasien.NoRekamMedis ")

    '    query.AppendLine("INNER JOIN MasterPaketTindakan MPT ")
    '    query.AppendLine("    ON MPT.PaketID = tblLog.PaketID ")

    '    query.AppendLine("WHERE tblLog.DeletedDate IS NULL ")
    '    query.AppendLine("AND MPT.JenisPaket = 'Paket Reguler' ")

    '    ' 🔹 Filter No RM
    '    If TextBoxNomorRekamMedis.Text.Trim() <> "" Then
    '        query.AppendLine("AND tblLog.NoRekamMedis = '" & TextBoxNomorRekamMedis.Text.Trim() & "'")
    '    End If

    '    ' 🔹 Filter tanggal (jika checkbox tidak dicentang)
    '    If Not CheckBoxFilterTanggal.Checked Then
    '        query.AppendLine("AND CONVERT(DATE, tblLog.TanggalTransaksi) BETWEEN '" & tglAwal & "' AND '" & tglAkhir & "'")
    '    End If

    '    query.AppendLine("ORDER BY tblLog.PaketID, tblLog.KodeVoucher, tblLog.TanggalTransaksi ")

    '    ' 🔹 DEBUG (opsional, tapi sangat membantu)
    '    ' MsgBox(query.ToString())

    '    tblResult = ReadSQL(query.ToString())

    '    Return tblResult

    'End Function

    'OLD
    'Function GetDataDetailPaketReguler() As DataTable
    '    Dim tglTransaksiAwal As Date = TglAwalTransaksi.Value
    '    Dim tglTransaksiAkhir As Date = TglAkhirTransaksi.Value
    '    Dim tblResult As New DataTable

    '    Dim query As New StringBuilder()

    '    query.AppendLine("SELECT tblLog.NoTransaksi, tblPasien.Nama, tblLog.NoRekamMedis AS 'No RM', ")
    '    query.AppendLine("       tblLog.KodeVoucher, tblLog.PaketID, ")
    '    query.AppendLine("       tblLog.TanggalTransaksi AS 'Tgl Transaksi'")
    '    query.AppendLine("FROM LogPenggunaanPaket tblLog")
    '    query.AppendLine("INNER JOIN TransaksiPaketTindakan tblPaket ")
    '    query.AppendLine("    ON tblLog.KodeVoucher = tblPaket.KodeVoucher ")
    '    query.AppendLine("    AND tblLog.PaketID = tblPaket.PaketID")
    '    query.AppendLine("INNER JOIN MasterPasien tblPasien ")
    '    query.AppendLine("    ON tblLog.NoRekamMedis = tblPasien.NoRekamMedis")
    '    query.AppendLine("WHERE tblPaket.JenisPaket = 'Paket Reguler'")
    '    If TextBoxNomorRekamMedis.Text <> "" Then
    '        query.AppendLine("AND tblPaket.NoRekamMedis = '" & TextBoxNomorRekamMedis.Text & "'")
    '    End If
    '    query.AppendLine("AND (CONVERT(DATE, tblPaket.TanggalMulaiPaket) BETWEEN '" & Format(tglTransaksiAwal.Date, "yyyyMMdd") & "' AND '" & Format(tglTransaksiAkhir.Date, "yyyyMMdd") & "') ")
    '    query.AppendLine("AND tblLog.DeletedDate IS NULL")
    '    query.AppendLine("ORDER BY tblLog.PaketID, tblLog.KodeVoucher;")

    '    tblResult = ReadSQL(query.ToString())

    '    Return tblResult
    'End Function

    Private Sub ButtonClearData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClearData.Click
        SetClearGrid()
    End Sub

    Private Sub SetClearGrid()
        TextBoxNomorRekamMedis.ResetText()
        TextBoxNamaRekamMedis.ResetText()
    End Sub

    Function GetDataDetailPaketRegulerLaporan(ByVal NoRM As String) As DataTable

        Dim tglTransaksiAwal As Date = TglAwalTransaksi.Value
        Dim tglTransaksiAkhir As Date = TglAkhirTransaksi.Value

        Dim tblResult As New DataTable
        Dim query As New StringBuilder()

        ' 🔥 Checkbox:
        ' dicentang = semua tanggal (0)
        ' tidak dicentang = pakai filter (1)
        Dim pakaiFilterTanggal As Integer = If(CheckBoxFilterTanggal.Checked, 0, 1)

        query.AppendLine("DECLARE @NoRM VARCHAR(50) = '" & NoRM & "'")
        query.AppendLine("DECLARE @TglAwal DATE = '" & Format(tglTransaksiAwal, "yyyy-MM-dd") & "'")
        query.AppendLine("DECLARE @TglAkhir DATE = '" & Format(tglTransaksiAkhir, "yyyy-MM-dd") & "'")
        query.AppendLine("DECLARE @PakaiFilterTanggal BIT = " & pakaiFilterTanggal)

        query.AppendLine("SELECT ")
        query.AppendLine("    TD.JenisPoli,")
        query.AppendLine("    LPP.NoTransaksi,")
        query.AppendLine("    LPP.KodeVoucher,")
        query.AppendLine("    MAX(LPP.TanggalTransaksi) AS Tgl,")
        query.AppendLine("    MP.Nama AS NamaPasien,")
        query.AppendLine("    MPT.NamaPaket,")
        query.AppendLine("    MPTD.NamaTindakan,")
        query.AppendLine("    MPT.Kuota AS KuotaAwal,")
        query.AppendLine("    COUNT(*) AS PenggunaanTransaksi,")

        query.AppendLine("    MPT.Kuota -")
        query.AppendLine("    (")
        query.AppendLine("        SELECT COUNT(*)")
        query.AppendLine("        FROM LogPenggunaanPaket LPP2")
        query.AppendLine("        WHERE LPP2.KodeVoucher = LPP.KodeVoucher")
        query.AppendLine("        AND LPP2.PaketID = LPP.PaketID")
        query.AppendLine("        AND LPP2.NoTransaksi <= LPP.NoTransaksi")
        query.AppendLine("        AND LPP2.DeletedBy IS NULL")
        query.AppendLine("    ) AS SisaKuota,")
        query.AppendLine("    CAST(MAX(TPT.TanggalAkhirPaket) AS DATE) AS TanggalAkhirPaket")

        query.AppendLine("FROM LogPenggunaanPaket LPP")

        query.AppendLine("INNER JOIN MasterPasien MP")
        query.AppendLine("    ON MP.NoRekamMedis = LPP.NoRekamMedis")

        query.AppendLine("INNER JOIN MasterPaketTindakan MPT")
        query.AppendLine("    ON MPT.PaketID = LPP.PaketID")

        query.AppendLine("INNER JOIN MasterPaketTindakanDetail MPTD")
        query.AppendLine("    ON MPTD.PaketID = MPT.PaketID")

        query.AppendLine("INNER JOIN (")
        query.AppendLine("    SELECT DISTINCT NoUrut, JenisPoli")
        query.AppendLine("    FROM TransaksiDetail")
        query.AppendLine(") TD")
        query.AppendLine("    ON TD.NoUrut = LPP.NoTransaksi")

        query.AppendLine("OUTER APPLY (")
        query.AppendLine("    SELECT TOP 1 TanggalAkhirPaket")
        query.AppendLine("    FROM TransaksiPaketTindakan TPT")
        query.AppendLine("    WHERE TPT.KodeVoucher = LPP.KodeVoucher")
        query.AppendLine(") TPT")


        query.AppendLine("WHERE ")
        query.AppendLine("    LPP.DeletedBy IS NULL")
        query.AppendLine("    AND (@NoRM = '' OR LPP.NoRekamMedis = @NoRM)")
        query.AppendLine("    AND MPT.JenisPaket = 'Paket Reguler'")

        query.AppendLine("GROUP BY ")
        query.AppendLine("    TD.JenisPoli,")
        query.AppendLine("    LPP.NoTransaksi,")
        query.AppendLine("    LPP.KodeVoucher,")
        query.AppendLine("    LPP.PaketID,")
        query.AppendLine("    MP.Nama,")
        query.AppendLine("    MPT.NamaPaket,")
        query.AppendLine("    MPTD.NamaTindakan,")
        query.AppendLine("    MPT.Kuota")

        query.AppendLine("HAVING ")
        query.AppendLine("    (")
        query.AppendLine("        @PakaiFilterTanggal = 0")
        query.AppendLine("        OR (MAX(LPP.TanggalTransaksi) >= @TglAwal ")
        query.AppendLine("            AND MAX(LPP.TanggalTransaksi) < DATEADD(DAY, 1, @TglAkhir))")
        query.AppendLine("    )")

        query.AppendLine("ORDER BY ")
        query.AppendLine("    TD.JenisPoli,")
        query.AppendLine("    Tgl")

        ' 🔍 DEBUG (opsional tapi sangat disarankan)
        ' MsgBox(query.ToString())

        tblResult = ReadSQL(query.ToString())

        Return tblResult

    End Function

    Function GetDataDetailPaketPerTindakanLaporan(ByVal NoRM As String) As DataTable

        Dim tglTransaksiAwal As Date = TglAwalTransaksi.Value
        Dim tglTransaksiAkhir As Date = TglAkhirTransaksi.Value

        Dim tblResult As New DataTable
        Dim query As New StringBuilder()

        query.AppendLine("SELECT ")
        query.AppendLine("    TD.JenisPoli,")
        query.AppendLine("    LPP.NoTransaksi,")
        query.AppendLine("    MAX(LPP.TanggalTransaksi) AS Tgl,")
        query.AppendLine("    MAX(p.Nama) AS NamaPasien,")
        query.AppendLine("    MAX(pkt.NamaPaket) AS NamaPaket,")
        query.AppendLine("    det.NamaTindakan,")

        ' 🔥 FIX: pakai Jumlah
        query.AppendLine("    det.Jumlah AS KuotaAwal,")

        query.AppendLine("    COUNT(*) AS PenggunaanTransaksi,")

        ' 🔥 FIX: SisaKuota pakai Jumlah juga
        query.AppendLine("    det.Jumlah -")
        query.AppendLine("    (")
        query.AppendLine("        SELECT COUNT(*)")
        query.AppendLine("        FROM LogPenggunaanPaket LPP2")
        query.AppendLine("        WHERE LPP2.KodeVoucher = LPP.KodeVoucher")
        query.AppendLine("        AND LPP2.PaketID = LPP.PaketID")
        query.AppendLine("        AND LPP2.TindakanID = LPP.TindakanID")
        query.AppendLine("        AND LPP2.NoTransaksi <= LPP.NoTransaksi")
        query.AppendLine("        AND LPP2.DeletedBy IS NULL")
        query.AppendLine("    ) AS SisaKuota,")
        query.AppendLine("    MAX(TPT.TanggalAkhirPaket) AS TanggalAkhirPaket")

        query.AppendLine("FROM LogPenggunaanPaket LPP")

        query.AppendLine("INNER JOIN TransaksiPaketTindakan pkt")
        query.AppendLine("    ON pkt.KodeVoucher = LPP.KodeVoucher")
        query.AppendLine("    AND pkt.PaketID = LPP.PaketID")

        query.AppendLine("INNER JOIN TransaksiPaketTindakanDetail det")
        query.AppendLine("    ON det.KodeVoucher = pkt.KodeVoucher")
        query.AppendLine("    AND det.PaketID = pkt.PaketID")
        query.AppendLine("    AND det.TindakanID = LPP.TindakanID")

        query.AppendLine("INNER JOIN (")
        query.AppendLine("    SELECT DISTINCT NoUrut, JenisPoli")
        query.AppendLine("    FROM TransaksiDetail")
        query.AppendLine(") TD ON TD.NoUrut = LPP.NoTransaksi")

        query.AppendLine("LEFT JOIN MasterPasien p")
        query.AppendLine("    ON p.NoRekamMedis = pkt.NoRekamMedis")

        query.AppendLine("OUTER APPLY (")
        query.AppendLine("    SELECT TOP 1 TanggalAkhirPaket")
        query.AppendLine("    FROM TransaksiPaketTindakan TPT")
        query.AppendLine("    WHERE TPT.KodeVoucher = LPP.KodeVoucher")
        query.AppendLine("    AND TPT.PaketID = LPP.PaketID")
        query.AppendLine("    ORDER BY TanggalAkhirPaket DESC")
        query.AppendLine(") TPT")

        query.AppendLine("WHERE LPP.DeletedBy IS NULL")
        query.AppendLine("AND ('" & NoRM & "' = '' OR LPP.NoRekamMedis = '" & NoRM & "')")

        ' 🔥 FILTER TANGGAL (AMAN & FIX BUG 1 HARI)
        If Not CheckBoxFilterTanggal.Checked Then
            query.AppendLine("AND LPP.TanggalTransaksi >= '" & Format(tglTransaksiAwal, "yyyy-MM-dd") & " 00:00:00'")
            query.AppendLine("AND LPP.TanggalTransaksi < DATEADD(DAY, 1, '" & Format(tglTransaksiAkhir, "yyyy-MM-dd") & "')")
        End If

        query.AppendLine("GROUP BY ")
        query.AppendLine("    TD.JenisPoli,")
        query.AppendLine("    LPP.NoTransaksi,")
        query.AppendLine("    LPP.KodeVoucher,")
        query.AppendLine("    LPP.PaketID,")
        query.AppendLine("    LPP.TindakanID,")
        query.AppendLine("    det.NamaTindakan,")
        query.AppendLine("    det.Jumlah") ' 🔥 wajib ikut GROUP BY

        query.AppendLine("ORDER BY TD.JenisPoli, MAX(LPP.TanggalTransaksi)")

        tblResult = ReadSQL(query.ToString())

        Return tblResult

    End Function

    Private Sub LaporanRiwayatPaket()

        Dim NoRM As String = TextBoxNomorRekamMedis.Text.Trim()

        'If NoRM = "" Then
        '    MessageBox.Show("Nomor Rekam Medis belum diisi.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Exit Sub
        'End If

        Dim tblData As New DataTable

        'If RadioButtonOptionPaketReguler.Checked Then
        '    tblData = GetDataDetailPaketRegulerLaporan()
        'Else
        '    tblData = GetDataDetailPaketPerTindakanLaporan()
        'End If

        If RadioButtonOptionPaketReguler.Checked Then
            tblData = GetDataDetailPaketRegulerLaporan(NoRM)
        Else
            tblData = GetDataDetailPaketPerTindakanLaporan(NoRM)
        End If

        If tblData.Rows.Count = 0 Then
            MessageBox.Show("Data paket tidak ditemukan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If tblData.Columns.Contains("No") = False Then
            tblData.Columns.Add("No", GetType(Integer))
        End If

        tblData.Columns("No").ReadOnly = False

        Dim rowNumber As Integer = 0
        Dim lastPoli As String = ""

        If Not tblData.Columns.Contains("TglFormat") Then
            tblData.Columns.Add("TglFormat", GetType(String))
        End If

        For Each Trow As DataRow In tblData.Rows

            If Not IsDBNull(Trow("Tgl")) Then
                Trow("TglFormat") = Format(CDate(Trow("Tgl")), "MM/dd/yyyy HH:mm")
            Else
                Trow("TglFormat") = ""
            End If

            If lastPoli <> Trow("JenisPoli").ToString() Then
                rowNumber = 1
                lastPoli = Trow("JenisPoli").ToString()
            Else
                rowNumber += 1
            End If

            Trow("No") = rowNumber

        Next

        Report = New XtraReport
        Report = XtraReport.FromFile(Application.StartupPath & "\Reports\LaporanPengambilanPaket.repx", True)


        'HEADER LAPORAN
        Dim tglAwal As Date = TglAwalTransaksi.Value
        Dim tglAkhir As Date = TglAkhirTransaksi.Value

        Dim stringTglAwal As String = Format(tglAwal, "dd MMMM yyyy")
        Dim stringTglAkhir As String = Format(tglAkhir, "dd MMMM yyyy")

        If RadioButtonOptionPaketReguler.Checked Then
            Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelJudulLaporan").Text = "Laporan Pengambilan Paket Reguler"
        Else
            Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelJudulLaporan").Text = "Laporan Pengambilan Paket Per-Tindakan"
        End If

        If CheckBoxFilterTanggal.Checked Then
            Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelTanggalLaporan").Text = "Semua Tanggal"
        Else
            Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelTanggalLaporan").Text = stringTglAwal & " - " & stringTglAkhir
        End If

        'Ambil semua GroupHeader
        For Each band As DevExpress.XtraReports.UI.Band In Report.Bands

            If TypeOf band Is DevExpress.XtraReports.UI.GroupHeaderBand Then

                Dim gh As DevExpress.XtraReports.UI.GroupHeaderBand = CType(band, DevExpress.XtraReports.UI.GroupHeaderBand)

                gh.GroupFields.Clear()
                gh.GroupFields.Add(New DevExpress.XtraReports.UI.GroupField("JenisPoli", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending))

            End If

        Next

        'FOOTER CETAK
        Report.Bands.Item(BandKind.PageFooter).Controls("xrLabelInfoCetak").Text = Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") & " By " & userInput


        'BIND DATA
        Report.DataSource = tblData


        'PREVIEW
        Report.ShowPreview()
        'Report.RunDesigner()
    End Sub

    Private Function GetDataDetailTindakanLaporan() As DataTable

        Dim tglTransaksiAwal As Date = TglAwalTransaksi.Value
        Dim tglTransaksiAkhir As Date = TglAkhirTransaksi.Value

        Dim tblResult As New DataTable
        Dim query As New StringBuilder()

        ' 🔥 Checkbox:
        ' dicentang = semua tanggal (0)
        ' tidak dicentang = pakai filter (1)
        Dim pakaiFilterTanggal As Integer = If(CheckBoxFilterTanggal.Checked, 0, 1)

        query.AppendLine("DECLARE @TglAwal DATE = '" & Format(tglTransaksiAwal, "yyyy-MM-dd") & "'")
        query.AppendLine("DECLARE @TglAkhir DATE = '" & Format(tglTransaksiAkhir, "yyyy-MM-dd") & "'")
        query.AppendLine("DECLARE @PakaiFilterTanggal BIT = " & pakaiFilterTanggal)

        query.AppendLine("SELECT")
        query.AppendLine("    TP.NoUrut,")
        query.AppendLine("    TP.CreatedDate AS Tgl,")
        query.AppendLine("    MP.Nama AS NamaPasien,")
        query.AppendLine("    ISNULL(TPT.NamaPaket, '-') AS NamaPaket,")
        query.AppendLine("    TPD.NamaTindakan,")
        query.AppendLine("    RTRIM(LTRIM(UPPER(TD.JenisPoli))) AS JenisPoli")

        query.AppendLine("FROM TransaksiPoli TP")

        query.AppendLine("INNER JOIN TransaksiPoliDetail TPD")
        query.AppendLine("    ON TP.TransPoliID = TPD.TransPoliID")

        query.AppendLine("INNER JOIN MasterPasien MP")
        query.AppendLine("    ON TP.IDInternet = MP.NoRekamMedis")

        ' 🔥 FIX DUPLIKASI POLI
        query.AppendLine("INNER JOIN (")
        query.AppendLine("    SELECT DISTINCT NoUrut, RTRIM(LTRIM(UPPER(JenisPoli))) AS JenisPoli")
        query.AppendLine("    FROM TransaksiDetail")
        query.AppendLine(") TD ON TP.NoUrut = TD.NoUrut")

        query.AppendLine("LEFT JOIN TransaksiPaketTindakan TPT")
        query.AppendLine("    ON TPD.KodeVoucher = TPT.KodeVoucher")

        query.AppendLine("WHERE")
        query.AppendLine("    TPD.JenisTindakan NOT LIKE '%Konsultasi%'")
        query.AppendLine("    AND TPD.JenisTindakan <> '-'")

        query.AppendLine("    AND (")
        query.AppendLine("        @PakaiFilterTanggal = 0")
        query.AppendLine("        OR (TPD.transaksitime >= @TglAwal")
        query.AppendLine("            AND TPD.transaksitime < DATEADD(DAY, 1, @TglAkhir))")
        query.AppendLine("    )")

        query.AppendLine("ORDER BY")
        query.AppendLine("    JenisPoli,")
        query.AppendLine("    TPD.NamaTindakan,")
        query.AppendLine("    TP.CreatedDate,")
        query.AppendLine("    TP.NoUrut")

        ' Debug kalau perlu
        ' MsgBox(query.ToString())

        tblResult = ReadSQL(query.ToString())

        Return tblResult

    End Function

    Private Sub LaporanTindakan()

        Dim tblData As New DataTable
        tblData = GetDataDetailTindakanLaporan()

        Dim tglAwal As Date = TglAwalTransaksi.Value
        Dim tglAkhir As Date = TglAkhirTransaksi.Value

        ' VALIDASI
        If tblData.Rows.Count = 0 Then
            MessageBox.Show("Data tindakan tidak ditemukan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' TAMBAH KOLOM NO
        If Not tblData.Columns.Contains("No") Then
            tblData.Columns.Add("No", GetType(Integer))
        End If

        ' FORMAT TANGGAL
        If Not tblData.Columns.Contains("TglFormat") Then
            tblData.Columns.Add("TglFormat", GetType(String))
        End If

        Dim rowNumber As Integer = 0
        Dim lastGroup As String = ""

        For Each row As DataRow In tblData.Rows

            ' Format tanggal
            If Not IsDBNull(row("Tgl")) Then
                row("TglFormat") = Format(CDate(row("Tgl")), "MM/dd/yyyy HH:mm")
            Else
                row("TglFormat") = ""
            End If

            ' Reset nomor per Poli + Tindakan
            Dim currentGroup As String = row("JenisPoli").ToString() & "|" & row("NamaTindakan").ToString()

            If lastGroup <> currentGroup Then
                rowNumber = 1
                lastGroup = currentGroup
            Else
                rowNumber += 1
            End If

            row("No") = rowNumber

        Next

        ' LOAD REPORT
        Report = New XtraReport
        Report = XtraReport.FromFile(Application.StartupPath & "\Reports\LaporanTindakan.repx", True)

        ' HEADER
        Dim stringTglAwal As String = Format(tglAwal, "dd MMMM yyyy")
        Dim stringTglAkhir As String = Format(tglAkhir, "dd MMMM yyyy")

        Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelJudulLaporan").Text = "Laporan Tindakan Pasien"

        If CheckBoxFilterTanggal.Checked Then
            Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelTanggalLaporan").Text = "Semua Tanggal"
        Else
            Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelTanggalLaporan").Text = stringTglAwal & " - " & stringTglAkhir
        End If

        ' GROUPING FIX (DIRECT ASSIGN)
        For Each band As DevExpress.XtraReports.UI.Band In Report.Bands

            If TypeOf band Is DevExpress.XtraReports.UI.GroupHeaderBand Then

                Dim gh As DevExpress.XtraReports.UI.GroupHeaderBand = CType(band, DevExpress.XtraReports.UI.GroupHeaderBand)

                If gh.Level = 1 Then
                    ' Poli
                    gh.GroupFields.Clear()
                    gh.GroupFields.Add(New DevExpress.XtraReports.UI.GroupField("JenisPoli", XRColumnSortOrder.Ascending))
                ElseIf gh.Level = 0 Then
                    ' Tindakan
                    gh.GroupFields.Clear()
                    gh.GroupFields.Add(New DevExpress.XtraReports.UI.GroupField("NamaTindakan", XRColumnSortOrder.Ascending))
                End If

            End If

        Next

        ' FOOTER
        Report.Bands.Item(BandKind.PageFooter).Controls("xrLabelInfoCetak").Text = Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") & " By " & userInput

        ' BIND
        Report.DataSource = tblData

        ' PREVIEW
        'Report.RunDesigner()
        Report.ShowPreview()

    End Sub

    Private Sub ButtonLaporanPdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLaporanPdf.Click
        If CheckBoxDownloadLaporanTindakan.Checked Then
            LaporanTindakan()
        Else
            LaporanRiwayatPaket()
        End If
    End Sub
End Class