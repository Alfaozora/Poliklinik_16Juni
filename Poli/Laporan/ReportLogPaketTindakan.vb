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
        queryStringHeader.AppendLine("INNER JOIN TransaksiDetail TD ")
        queryStringHeader.AppendLine("       ON TD.NoUrut = tblPaket.NoTransaksi ")
        queryStringHeader.AppendLine("WHERE tblPaket.JenisPaket = 'Paket Per-Tindakan' ")
        queryStringHeader.AppendLine("AND tblPaket.DeletedDate IS NULL ")

        If ComboBoxPoli.Text <> "" Then
            queryStringHeader.AppendLine("AND TD.JenisPoli = '" & ComboBoxPoli.Text & "' ")
        End If

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
        queryStringDetail.AppendLine("INNER JOIN TransaksiDetail TD ")
        queryStringDetail.AppendLine("       ON TD.NoUrut = tblPaket.NoTransaksi ")
        queryStringDetail.AppendLine("WHERE tblPaket.JenisPaket = 'Paket Per-Tindakan' ")
        queryStringDetail.AppendLine("AND tblLog.DeletedDate IS NULL ")

        If ComboBoxPoli.Text <> "" Then
            queryStringDetail.AppendLine("AND TD.JenisPoli = '" & ComboBoxPoli.Text & "' ")
        End If

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
            Dim kuota As Integer = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "Kuota"))
            Dim objTglBerakhir As Object = view.GetRowCellValue(e.RowHandle, "Tgl Berakhir")

            ' 1. Jika kuota 0 -> LightCoral (Merah Murni/Habis)
            If kuota = 0 Then
                e.Appearance.BackColor = Color.LightCoral
                Exit Sub
            End If

            ' 2. Jika kuota masih ada DAN tanggal berakhir terlewati -> Khaki / Yellow (Kuning/Kadaluwarsa)
            If objTglBerakhir IsNot DBNull.Value AndAlso objTglBerakhir IsNot Nothing Then
                Dim tglBerakhir As Date = Convert.ToDateTime(objTglBerakhir).Date

                If tglBerakhir < Date.Today Then
                    e.Appearance.BackColor = Color.Khaki
                    Exit Sub
                End If
            End If

            ' 3. Jika kuota masih ada DAN belum terlewati -> LightGreen (Hijau/Aktif)
            e.Appearance.BackColor = Color.LightGreen
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
        If ComboBoxPoli.Text.Trim() = "" Or ComboBoxPoli.SelectedIndex = -1 Then
            MsgBox("Silakan pilih Jenis Poli terlebih dahulu!", MsgBoxStyle.Exclamation, "Validasi")
            ComboBoxPoli.Focus()
            Exit Sub
        End If

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
    Public Function GetDataHeaderPaketReguler() As DataTable
        Dim tblResult As New DataTable
        Dim query As New StringBuilder()

        Dim namaPoli As String = ComboBoxPoli.Text.Trim().Replace("'", "''")
        Dim noRM As String = TextBoxNomorRekamMedis.Text.Trim().Replace("'", "''")

        query.AppendLine("SELECT ")
        query.AppendLine("    tblPasien.Nama, ")
        query.AppendLine("    tblPaket.NoRekamMedis AS [No RM], ")
        query.AppendLine("    tblPaket.NoTransaksi AS NoTransaksi, ")
        query.AppendLine("    tblPaket.NamaPaket AS [Nama Paket], ")

        query.AppendLine("    STUFF(( ")
        query.AppendLine("        SELECT ', ' + d.NamaTindakan ")
        query.AppendLine("        FROM TransaksiPaketTindakanDetail d WITH(NOLOCK) ")
        query.AppendLine("        WHERE d.PaketID = tblPaket.PaketID ")
        query.AppendLine("        AND d.KodeVoucher = tblPaket.KodeVoucher ")
        query.AppendLine("        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') ")
        query.AppendLine("    AS [Nama Tindakan], ")

        query.AppendLine("    tblPaket.JenisPaket AS [Jenis Paket], ")
        query.AppendLine("    tblPaket.Kuota, ")
        query.AppendLine("    tblPaket.TanggalMulaiPaket AS [Tgl Mulai], ")
        query.AppendLine("    tblPaket.TanggalAkhirPaket AS [Tgl Berakhir], ")
        query.AppendLine("    tblPaket.PaketID, ")
        query.AppendLine("    tblPaket.KodeVoucher, ")
        query.AppendLine("    0 AS TindakanID ")

        query.AppendLine("FROM TransaksiPaketTindakan tblPaket WITH(NOLOCK) ")

        ' LEFT JOIN agar paket yang belum terpakai / terpakai di poli lain tetap terakomodasi
        query.AppendLine("LEFT JOIN LogPenggunaanPaket tblLog WITH(NOLOCK) ")
        query.AppendLine("    ON tblLog.PaketID = tblPaket.PaketID ")
        query.AppendLine("    AND tblLog.KodeVoucher = tblPaket.KodeVoucher ")
        query.AppendLine("    AND tblLog.DeletedDate IS NULL ")

        ' Join ke TransaksiDetail Penggunaan (Tempat Pakai)
        query.AppendLine("LEFT JOIN TransaksiDetail TDLog WITH(NOLOCK) ")
        query.AppendLine("    ON TDLog.NoUrut = tblLog.NoTransaksi ")

        query.AppendLine("INNER JOIN MasterPasien tblPasien WITH(NOLOCK) ")
        query.AppendLine("    ON tblPasien.NoRekamMedis = tblPaket.NoRekamMedis ")

        ' Join ke TransaksiDetail Pembelian (Tempat Beli)
        query.AppendLine("LEFT JOIN TransaksiDetail TDPembelian WITH(NOLOCK) ")
        query.AppendLine("    ON TDPembelian.NoUrut = tblPaket.NoTransaksi ")

        query.AppendLine("WHERE tblPaket.JenisPaket = 'Paket Reguler' ")
        query.AppendLine("AND tblPaket.DeletedDate IS NULL ")

        ' Filter Poli (Mengecek Poli Beli ATAU Poli Pakai)
        If namaPoli <> "" Then
            query.AppendLine("AND (LTRIM(RTRIM(TDPembelian.JenisPoli)) = '" & namaPoli & "' OR LTRIM(RTRIM(TDLog.JenisPoli)) = '" & namaPoli & "') ")
        End If

        ' Filter No RM
        If noRM <> "" Then
            query.AppendLine("AND tblPaket.NoRekamMedis = '" & noRM & "' ")
        End If

        ' Filter Tanggal (Jika CheckBoxFilterTanggal TIDAK dicentang)
        If Not CheckBoxFilterTanggal.Checked Then
            Dim tglAwal As String = TglAwalTransaksi.Value.ToString("yyyy-MM-dd 00:00:00")
            Dim tglAkhir As String = TglAkhirTransaksi.Value.ToString("yyyy-MM-dd 23:59:59")

            query.AppendLine("AND (tblLog.PaketID IS NULL OR (tblLog.TanggalTransaksi >= '" & tglAwal & "' AND tblLog.TanggalTransaksi <= '" & tglAkhir & "')) ")
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

        query.AppendLine("ORDER BY tblPaket.PaketID ASC")

        ' Eksekusi query ke database
        tblResult = ReadSQL(query.ToString())
        Return tblResult

    End Function

    'Function GetDataHeaderPaketReguler() As DataTable
    '    Dim tblResult As New DataTable
    '    Dim query As New StringBuilder()

    '    query.AppendLine("SELECT ")
    '    query.AppendLine("    tblPasien.Nama, ")
    '    query.AppendLine("    tblPaket.NoRekamMedis AS [No RM], ")
    '    query.AppendLine("    tblPaket.NoTransaksi AS NoTransaksi, ")
    '    query.AppendLine("    tblPaket.NamaPaket AS [Nama Paket], ")

    '    query.AppendLine("    STUFF(( ")
    '    query.AppendLine("        SELECT ', ' + d.NamaTindakan ")
    '    query.AppendLine("        FROM TransaksiPaketTindakanDetail d ")
    '    query.AppendLine("        WHERE d.PaketID = tblPaket.PaketID ")
    '    query.AppendLine("        AND d.KodeVoucher = tblPaket.KodeVoucher ")
    '    query.AppendLine("        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ")
    '    query.AppendLine("    AS [Nama Tindakan], ")

    '    query.AppendLine("    tblPaket.JenisPaket AS [Jenis Paket], ")
    '    query.AppendLine("    tblPaket.Kuota, ")
    '    query.AppendLine("    tblPaket.TanggalMulaiPaket AS [Tgl Mulai], ")
    '    query.AppendLine("    tblPaket.TanggalAkhirPaket AS [Tgl Berakhir], ")
    '    query.AppendLine("    tblPaket.PaketID, ")
    '    query.AppendLine("    tblPaket.KodeVoucher, ")
    '    query.AppendLine("    0 AS TindakanID ")

    '    query.AppendLine("FROM TransaksiPaketTindakan tblPaket ")

    '    query.AppendLine("INNER JOIN LogPenggunaanPaket tblLog ")
    '    query.AppendLine("    ON tblLog.PaketID = tblPaket.PaketID ")
    '    query.AppendLine("    AND tblLog.KodeVoucher = tblPaket.KodeVoucher ")
    '    query.AppendLine("    AND tblLog.DeletedDate IS NULL ")

    '    query.AppendLine("INNER JOIN MasterPasien tblPasien ")
    '    query.AppendLine("    ON tblPasien.NoRekamMedis = tblPaket.NoRekamMedis ")

    '    query.AppendLine("INNER JOIN TransaksiDetail TD ")
    '    query.AppendLine("    ON TD.NoUrut = tblPaket.NoTransaksi ")

    '    query.AppendLine("WHERE tblPaket.JenisPaket = 'Paket Reguler' ")
    '    query.AppendLine("AND tblPaket.DeletedDate IS NULL ")

    '    ' Filter Poli
    '    If ComboBoxPoli.Text <> "" Then
    '        query.AppendLine("AND TD.JenisPoli = '" & ComboBoxPoli.Text.Replace("'", "''") & "' ")
    '    End If

    '    ' Filter No RM
    '    If TextBoxNomorRekamMedis.Text <> "" Then
    '        query.AppendLine("AND tblPaket.NoRekamMedis = '" & TextBoxNomorRekamMedis.Text.Replace("'", "''") & "' ")
    '    End If

    '    ' Filter Tanggal (Langsung passing string format yyyy-MM-dd HH:mm:ss)
    '    If Not CheckBoxFilterTanggal.Checked Then
    '        Dim tglAwal As String = TglAwalTransaksi.Value.ToString("yyyy-MM-dd 00:00:00")
    '        Dim tglAkhir As String = TglAkhirTransaksi.Value.ToString("yyyy-MM-dd 23:59:59")

    '        query.AppendLine("AND (tblLog.PaketID IS NULL OR (tblLog.TanggalTransaksi >= '" & tglAwal & "' AND tblLog.TanggalTransaksi <= '" & tglAkhir & "')) ")
    '    End If

    '    query.AppendLine("GROUP BY ")
    '    query.AppendLine("    tblPasien.Nama, ")
    '    query.AppendLine("    tblPaket.NoRekamMedis, ")
    '    query.AppendLine("    tblPaket.NoTransaksi, ")
    '    query.AppendLine("    tblPaket.NamaPaket, ")
    '    query.AppendLine("    tblPaket.JenisPaket, ")
    '    query.AppendLine("    tblPaket.Kuota, ")
    '    query.AppendLine("    tblPaket.TanggalMulaiPaket, ")
    '    query.AppendLine("    tblPaket.TanggalAkhirPaket, ")
    '    query.AppendLine("    tblPaket.PaketID, ")
    '    query.AppendLine("    tblPaket.KodeVoucher ")

    '    query.AppendLine("ORDER BY tblPaket.PaketID")

    '    ' Eksekusi query
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


        If ComboBoxPoli.Text <> "" Then
            query.AppendLine("AND EXISTS (")
            query.AppendLine("    SELECT 1 FROM TransaksiDetail TD ")
            query.AppendLine("    WHERE TD.NoUrut = tblLog.NoTransaksi ")
            query.AppendLine("    AND TD.JenisPoli = '" & ComboBoxPoli.Text & "'")
            query.AppendLine(")")
        End If

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

    Public Function GetDataDetailPaketRegulerLaporan(ByVal NoRM As String) As DataTable

        Dim tblResult As New DataTable
        Dim query As New StringBuilder()

        Dim pakaiFilterTanggal As Boolean = Not CheckBoxFilterTanggal.Checked
        Dim cleanNoRM As String = NoRM.Trim().Replace("'", "''")
        Dim cleanPoli As String = ComboBoxPoli.Text.Trim().Replace("'", "''")

        ' Format Tanggal Standar ISO (yyyy-MM-dd HH:mm:ss)
        Dim tglAwalStr As String = TglAwalTransaksi.Value.ToString("yyyy-MM-dd 00:00:00")
        Dim tglAkhirStr As String = TglAkhirTransaksi.Value.ToString("yyyy-MM-dd 23:59:59")

        query.AppendLine("SELECT ")
        query.AppendLine("    ISNULL(TD.JenisPoli, TDPembelian.JenisPoli) AS JenisPoli,")
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
        query.AppendLine("        FROM LogPenggunaanPaket LPP2 WITH(NOLOCK)")
        query.AppendLine("        WHERE LPP2.KodeVoucher = LPP.KodeVoucher")
        query.AppendLine("        AND LPP2.PaketID = LPP.PaketID")
        query.AppendLine("        AND LPP2.NoTransaksi <= LPP.NoTransaksi")
        query.AppendLine("        AND LPP2.DeletedBy IS NULL")
        query.AppendLine("    ) AS SisaKuota,")
        query.AppendLine("    CAST(MAX(TPT.TanggalAkhirPaket) AS DATE) AS TanggalAkhirPaket")

        query.AppendLine("FROM LogPenggunaanPaket LPP WITH(NOLOCK)")

        query.AppendLine("INNER JOIN MasterPasien MP WITH(NOLOCK)")
        query.AppendLine("    ON MP.NoRekamMedis = LPP.NoRekamMedis")

        query.AppendLine("INNER JOIN MasterPaketTindakan MPT WITH(NOLOCK)")
        query.AppendLine("    ON MPT.PaketID = LPP.PaketID")

        query.AppendLine("INNER JOIN MasterPaketTindakanDetail MPTD WITH(NOLOCK)")
        query.AppendLine("    ON MPTD.PaketID = MPT.PaketID")

        ' Join Transaksi Pemakaian (Poli Tempat Pemakaian)
        query.AppendLine("LEFT JOIN (")
        query.AppendLine("    SELECT DISTINCT NoUrut, JenisPoli")
        query.AppendLine("    FROM TransaksiDetail WITH(NOLOCK)")
        query.AppendLine(") TD")
        query.AppendLine("    ON TD.NoUrut = LPP.NoTransaksi")

        ' Outer Apply untuk Ambil Data Paket Pembelian
        query.AppendLine("OUTER APPLY (")
        query.AppendLine("    SELECT TOP 1 TanggalAkhirPaket, NoTransaksi")
        query.AppendLine("    FROM TransaksiPaketTindakan TPT WITH(NOLOCK)")
        query.AppendLine("    WHERE TPT.KodeVoucher = LPP.KodeVoucher")
        query.AppendLine("    AND TPT.PaketID = LPP.PaketID")
        query.AppendLine(") TPT")

        ' Join Transaksi Pembelian Paket (Poli Tempat Pembelian)
        query.AppendLine("LEFT JOIN (")
        query.AppendLine("    SELECT DISTINCT NoUrut, JenisPoli")
        query.AppendLine("    FROM TransaksiDetail WITH(NOLOCK)")
        query.AppendLine(") TDPembelian")
        query.AppendLine("    ON TDPembelian.NoUrut = TPT.NoTransaksi")

        query.AppendLine("WHERE ")
        query.AppendLine("    LPP.DeletedBy IS NULL")
        query.AppendLine("    AND MPT.JenisPaket = 'Paket Reguler'")

        ' Filter Nomor Rekam Medis
        If cleanNoRM <> "" Then
            query.AppendLine("    AND LPP.NoRekamMedis = '" & cleanNoRM & "'")
        End If

        ' Filter Poli (Pilih berdasarkan Poli Pemakaian ATAU Poli Pembelian)
        If cleanPoli <> "" Then
            query.AppendLine("    AND (LTRIM(RTRIM(TD.JenisPoli)) = '" & cleanPoli & "' OR LTRIM(RTRIM(TDPembelian.JenisPoli)) = '" & cleanPoli & "')")
        End If

        ' Filter Tanggal Pemakaian
        If pakaiFilterTanggal Then
            query.AppendLine("    AND LPP.TanggalTransaksi >= '" & tglAwalStr & "'")
            query.AppendLine("    AND LPP.TanggalTransaksi <= '" & tglAkhirStr & "'")
        End If

        query.AppendLine("GROUP BY ")
        query.AppendLine("    TD.JenisPoli,")
        query.AppendLine("    TDPembelian.JenisPoli,")
        query.AppendLine("    LPP.NoTransaksi,")
        query.AppendLine("    LPP.KodeVoucher,")
        query.AppendLine("    LPP.PaketID,")
        query.AppendLine("    MP.Nama,")
        query.AppendLine("    MPT.NamaPaket,")
        query.AppendLine("    MPTD.NamaTindakan,")
        query.AppendLine("    MPT.Kuota")

        query.AppendLine("ORDER BY ")
        query.AppendLine("    ISNULL(TD.JenisPoli, TDPembelian.JenisPoli),")
        query.AppendLine("    Tgl ASC")

        tblResult = ReadSQL(query.ToString())
        Return tblResult

    End Function

    'Function GetDataDetailPaketRegulerLaporan(ByVal NoRM As String) As DataTable

    '    Dim tblResult As New DataTable
    '    Dim query As New StringBuilder()

    '    Dim pakaiFilterTanggal As Boolean = Not CheckBoxFilterTanggal.Checked
    '    Dim cleanNoRM As String = NoRM.Replace("'", "''")
    '    Dim cleanPoli As String = ComboBoxPoli.Text.Replace("'", "''")

    '    ' Gunakan format yyyyMMdd murni (ISO standard) agar bebas dari bug regional server
    '    Dim tglAwalStr As String = TglAwalTransaksi.Value.ToString("yyyyMMdd 00:00:00")
    '    Dim tglAkhirStr As String = TglAkhirTransaksi.Value.ToString("yyyyMMdd 23:59:59")

    '    query.AppendLine("SELECT ")
    '    query.AppendLine("    TD.JenisPoli,")
    '    query.AppendLine("    LPP.NoTransaksi,")
    '    query.AppendLine("    LPP.KodeVoucher,")
    '    query.AppendLine("    MAX(LPP.TanggalTransaksi) AS Tgl,")
    '    query.AppendLine("    MP.Nama AS NamaPasien,")
    '    query.AppendLine("    MPT.NamaPaket,")
    '    query.AppendLine("    MPTD.NamaTindakan,")
    '    query.AppendLine("    MPT.Kuota AS KuotaAwal,")
    '    query.AppendLine("    COUNT(*) AS PenggunaanTransaksi,")

    '    query.AppendLine("    MPT.Kuota -")
    '    query.AppendLine("    (")
    '    query.AppendLine("        SELECT COUNT(*)")
    '    query.AppendLine("        FROM LogPenggunaanPaket LPP2")
    '    query.AppendLine("        WHERE LPP2.KodeVoucher = LPP.KodeVoucher")
    '    query.AppendLine("        AND LPP2.PaketID = LPP.PaketID")
    '    query.AppendLine("        AND LPP2.NoTransaksi <= LPP.NoTransaksi")
    '    query.AppendLine("        AND LPP2.DeletedBy IS NULL")
    '    query.AppendLine("    ) AS SisaKuota,")
    '    query.AppendLine("    CAST(MAX(TPT.TanggalAkhirPaket) AS DATE) AS TanggalAkhirPaket")

    '    query.AppendLine("FROM LogPenggunaanPaket LPP")

    '    query.AppendLine("INNER JOIN MasterPasien MP")
    '    query.AppendLine("    ON MP.NoRekamMedis = LPP.NoRekamMedis")

    '    query.AppendLine("INNER JOIN MasterPaketTindakan MPT")
    '    query.AppendLine("    ON MPT.PaketID = LPP.PaketID")

    '    query.AppendLine("INNER JOIN MasterPaketTindakanDetail MPTD")
    '    query.AppendLine("    ON MPTD.PaketID = MPT.PaketID")

    '    query.AppendLine("INNER JOIN (")
    '    query.AppendLine("    SELECT DISTINCT NoUrut, JenisPoli")
    '    query.AppendLine("    FROM TransaksiDetail")
    '    query.AppendLine(") TD")
    '    query.AppendLine("    ON TD.NoUrut = LPP.NoTransaksi")

    '    query.AppendLine("OUTER APPLY (")
    '    query.AppendLine("    SELECT TOP 1 TanggalAkhirPaket")
    '    query.AppendLine("    FROM TransaksiPaketTindakan TPT")
    '    query.AppendLine("    WHERE TPT.KodeVoucher = LPP.KodeVoucher")
    '    query.AppendLine(") TPT")

    '    query.AppendLine("WHERE ")
    '    query.AppendLine("    LPP.DeletedBy IS NULL")
    '    query.AppendLine("    AND MPT.JenisPaket = 'Paket Reguler'")

    '    ' Filter Langsung di WHERE (Jauh lebih cepat & stabil daripada DECLARE / HAVING)
    '    If cleanNoRM <> "" Then
    '        query.AppendLine("    AND LPP.NoRekamMedis = '" & cleanNoRM & "'")
    '    End If

    '    If cleanPoli <> "" Then
    '        query.AppendLine("    AND TD.JenisPoli = '" & cleanPoli & "'")
    '    End If

    '    If pakaiFilterTanggal Then
    '        query.AppendLine("    AND LPP.TanggalTransaksi >= '" & tglAwalStr & "'")
    '        query.AppendLine("    AND LPP.TanggalTransaksi <= '" & tglAkhirStr & "'")
    '    End If

    '    query.AppendLine("GROUP BY ")
    '    query.AppendLine("    TD.JenisPoli,")
    '    query.AppendLine("    LPP.NoTransaksi,")
    '    query.AppendLine("    LPP.KodeVoucher,")
    '    query.AppendLine("    LPP.PaketID,")
    '    query.AppendLine("    MP.Nama,")
    '    query.AppendLine("    MPT.NamaPaket,")
    '    query.AppendLine("    MPTD.NamaTindakan,")
    '    query.AppendLine("    MPT.Kuota")

    '    query.AppendLine("ORDER BY ")
    '    query.AppendLine("    TD.JenisPoli,")
    '    query.AppendLine("    Tgl")

    '    tblResult = ReadSQL(query.ToString())
    '    Return tblResult

    'End Function

    Function GetDataDetailPaketPerTindakanLaporan(ByVal NoRM As String) As DataTable

        Dim tglTransaksiAwal As Date = TglAwalTransaksi.Value
        Dim tglTransaksiAkhir As Date = TglAkhirTransaksi.Value

        Dim tblResult As New DataTable
        Dim query As New StringBuilder()

        query.AppendLine("DECLARE @JenisPoli VARCHAR(50) = '" & ComboBoxPoli.Text & "'")

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
        query.AppendLine("AND (@JenisPoli = '' OR TD.JenisPoli = @JenisPoli)")

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

    Public Function GetDataLaporanHeaderPaketReguler(ByVal NoRM As String) As DataTable
        Dim tblResult As New DataTable()
        Dim query As New StringBuilder()

        Dim namaPoli As String = ComboBoxPoli.Text.Trim().Replace("'", "''")
        Dim cleanNoRM As String = NoRM.Trim().Replace("'", "''")

        query.AppendLine("SELECT ")
        query.AppendLine("    tblPasien.Nama AS NamaPasien, ")
        query.AppendLine("    tblPaket.NoRekamMedis AS NoRM, ")
        query.AppendLine("    tblPaket.NoTransaksi AS NoTransaksiBeli, ")
        query.AppendLine("    tblPaket.NamaPaket, ")
        query.AppendLine("    MPT.Kuota AS KuotaAwal, ")         ' Kuota dari MasterPaketTindakan
        query.AppendLine("    tblPaket.Kuota AS KuotaBeli, ")     ' Kuota dari TransaksiPaketTindakan
        query.AppendLine("    STUFF(( ")
        query.AppendLine("        SELECT ', ' + d.NamaTindakan ")
        query.AppendLine("        FROM TransaksiPaketTindakanDetail d WITH(NOLOCK) ")
        query.AppendLine("        WHERE d.PaketID = tblPaket.PaketID ")
        query.AppendLine("        AND d.KodeVoucher = tblPaket.KodeVoucher ")
        query.AppendLine("        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS ListTindakan, ")
        query.AppendLine("    tblPaket.JenisPaket, ")
        query.AppendLine("    tblPaket.TanggalMulaiPaket AS TglMulai, ")
        query.AppendLine("    tblPaket.TanggalAkhirPaket AS TglBerakhir, ")
        query.AppendLine("    tblPaket.PaketID, ")
        query.AppendLine("    tblPaket.KodeVoucher ")
        query.AppendLine("FROM TransaksiPaketTindakan tblPaket WITH(NOLOCK) ")
        query.AppendLine("INNER JOIN MasterPaketTindakan MPT WITH(NOLOCK) ")
        query.AppendLine("    ON MPT.PaketID = tblPaket.PaketID ")
        query.AppendLine("INNER JOIN MasterPasien tblPasien WITH(NOLOCK) ")
        query.AppendLine("    ON tblPasien.NoRekamMedis = tblPaket.NoRekamMedis ")
        query.AppendLine("LEFT JOIN TransaksiDetail TDPembelian WITH(NOLOCK) ")
        query.AppendLine("    ON TDPembelian.NoUrut = tblPaket.NoTransaksi ")
        query.AppendLine("WHERE tblPaket.JenisPaket = 'Paket Reguler' ")
        query.AppendLine("AND tblPaket.DeletedBy IS NULL ")

        ' 🔥 WAJIB: HANYA TAMPILKAN JIKA MINIMAL 1 KALI TERPAKAI DI LOG PENGGUNAAN
        query.AppendLine("AND EXISTS ( ")
        query.AppendLine("    SELECT 1 FROM LogPenggunaanPaket tblLog WITH(NOLOCK) ")
        query.AppendLine("    LEFT JOIN TransaksiDetail TDLog WITH(NOLOCK) ON TDLog.NoUrut = tblLog.NoTransaksi ")
        query.AppendLine("    WHERE tblLog.PaketID = tblPaket.PaketID ")
        query.AppendLine("      AND tblLog.KodeVoucher = tblPaket.KodeVoucher ")
        query.AppendLine("      AND tblLog.DeletedBy IS NULL ")

        ' Filter Poli pada Log Pemakaian
        If namaPoli <> "" Then
            query.AppendLine("      AND (LTRIM(RTRIM(TDPembelian.JenisPoli)) = '" & namaPoli & "' OR LTRIM(RTRIM(TDLog.JenisPoli)) = '" & namaPoli & "') ")
        End If

        ' Filter Tanggal Transaksi Pemakaian
        If Not CheckBoxFilterTanggal.Checked Then
            Dim tglAwal As String = TglAwalTransaksi.Value.ToString("yyyy-MM-dd 00:00:00")
            Dim tglAkhir As String = TglAkhirTransaksi.Value.ToString("yyyy-MM-dd 23:59:59")
            query.AppendLine("      AND tblLog.TanggalTransaksi >= '" & tglAwal & "' ")
            query.AppendLine("      AND tblLog.TanggalTransaksi <= '" & tglAkhir & "' ")
        End If

        query.AppendLine(") ")

        ' Filter No RM Pasien
        If cleanNoRM <> "" Then
            query.AppendLine("AND tblPaket.NoRekamMedis = '" & cleanNoRM & "' ")
        End If

        query.AppendLine("GROUP BY ")
        query.AppendLine("    tblPasien.Nama, tblPaket.NoRekamMedis, tblPaket.NoTransaksi, ")
        query.AppendLine("    tblPaket.NamaPaket, MPT.Kuota, tblPaket.Kuota, tblPaket.JenisPaket, ")
        query.AppendLine("    tblPaket.TanggalMulaiPaket, tblPaket.TanggalAkhirPaket, ")
        query.AppendLine("    tblPaket.PaketID, tblPaket.KodeVoucher ")
        query.AppendLine("ORDER BY tblPaket.PaketID ASC")

        tblResult = ReadSQL(query.ToString())
        Return tblResult
    End Function

    Public Function GetDataLaporanDetailPaketReguler(ByVal NoRM As String) As DataTable
        Dim tblResult As New DataTable()
        Dim query As New StringBuilder()

        Dim namaPoli As String = ComboBoxPoli.Text.Trim().Replace("'", "''")
        Dim cleanNoRM As String = NoRM.Trim().Replace("'", "''")

        query.AppendLine("SELECT ")
        query.AppendLine("    tblLog.NoTransaksi AS NoTransaksiPakai, ")
        query.AppendLine("    tblPasien.Nama AS NamaPasien, ")
        query.AppendLine("    tblLog.NoRekamMedis AS NoRM, ")
        query.AppendLine("    tblLog.KodeVoucher, ")
        query.AppendLine("    tblLog.PaketID, ")
        query.AppendLine("    tblLog.TanggalTransaksi AS TglPakai, ")
        query.AppendLine("    ISNULL(TD.JenisPoli, 'N/A') AS JenisPoli ")
        query.AppendLine("FROM LogPenggunaanPaket tblLog WITH(NOLOCK) ")
        query.AppendLine("INNER JOIN TransaksiPaketTindakan tblPaket WITH(NOLOCK) ")
        query.AppendLine("    ON tblLog.KodeVoucher = tblPaket.KodeVoucher ")
        query.AppendLine("    AND tblLog.PaketID = tblPaket.PaketID ")
        query.AppendLine("INNER JOIN MasterPasien tblPasien WITH(NOLOCK) ")
        query.AppendLine("    ON tblLog.NoRekamMedis = tblPasien.NoRekamMedis ")
        query.AppendLine("LEFT JOIN ( ")
        query.AppendLine("    SELECT DISTINCT NoUrut, JenisPoli FROM TransaksiDetail WITH(NOLOCK) ")
        query.AppendLine(") TD ON TD.NoUrut = tblLog.NoTransaksi ")
        query.AppendLine("WHERE tblPaket.JenisPaket = 'Paket Reguler' ")
        query.AppendLine("AND tblLog.DeletedBy IS NULL ")

        If cleanNoRM <> "" Then
            query.AppendLine("AND tblPaket.NoRekamMedis = '" & cleanNoRM & "' ")
        End If

        If Not CheckBoxFilterTanggal.Checked Then
            Dim tglAwal As String = TglAwalTransaksi.Value.ToString("yyyy-MM-dd 00:00:00")
            Dim tglAkhir As String = TglAkhirTransaksi.Value.ToString("yyyy-MM-dd 23:59:59")
            query.AppendLine("AND tblLog.TanggalTransaksi >= '" & tglAwal & "' AND tblLog.TanggalTransaksi <= '" & tglAkhir & "' ")
        End If

        If namaPoli <> "" Then
            query.AppendLine("AND LTRIM(RTRIM(TD.JenisPoli)) = '" & namaPoli & "' ")
        End If

        query.AppendLine("ORDER BY tblLog.PaketID, tblLog.KodeVoucher, tblLog.TanggalTransaksi ASC")

        tblResult = ReadSQL(query.ToString())
        Return tblResult
    End Function

    Public Function GetDataLaporanHeaderPaketPerTindakan(ByVal NoRM As String) As DataTable
        Dim tblResult As New DataTable()
        Dim query As New StringBuilder()

        Dim namaPoli As String = ComboBoxPoli.Text.Trim().Replace("'", "''")
        Dim cleanNoRM As String = NoRM.Trim().Replace("'", "''")

        query.AppendLine("SELECT ")
        query.AppendLine("    tblPasien.Nama AS NamaPasien, ")
        query.AppendLine("    tblPaket.NoRekamMedis AS NoRM, ")
        query.AppendLine("    tblPaket.NoTransaksi AS NoTransaksiBeli, ")
        query.AppendLine("    tblPaket.NamaPaket, ")
        query.AppendLine("    tblPaketDetail.NamaTindakan, ")
        query.AppendLine("    tblPaketDetail.Jumlah AS KuotaBeli, ")          ' Kuota Awal Transaksi
        query.AppendLine("    tblPaketDetail.KuotaTindakan AS SisaKuotaDb, ") ' Total Sisa Kuota Langsung dari DB
        query.AppendLine("    tblPaket.JenisPaket, ")
        query.AppendLine("    tblPaket.TanggalMulaiPaket AS TglMulai, ")
        query.AppendLine("    tblPaket.TanggalAkhirPaket AS TglBerakhir, ")
        query.AppendLine("    tblPaket.PaketID, ")
        query.AppendLine("    tblPaket.KodeVoucher, ")
        query.AppendLine("    tblPaketDetail.TindakanID ")
        query.AppendLine("FROM TransaksiPaketTindakanDetail tblPaketDetail WITH(NOLOCK) ")
        query.AppendLine("INNER JOIN TransaksiPaketTindakan tblPaket WITH(NOLOCK) ")
        query.AppendLine("    ON tblPaket.PaketID = tblPaketDetail.PaketID ")
        query.AppendLine("    AND tblPaket.KodeVoucher = tblPaketDetail.KodeVoucher ")
        query.AppendLine("INNER JOIN MasterPasien tblPasien WITH(NOLOCK) ")
        query.AppendLine("    ON tblPasien.NoRekamMedis = tblPaket.NoRekamMedis ")
        query.AppendLine("LEFT JOIN TransaksiDetail TDPembelian WITH(NOLOCK) ")
        query.AppendLine("    ON TDPembelian.NoUrut = tblPaket.NoTransaksi ")
        query.AppendLine("WHERE tblPaket.JenisPaket = 'Paket Per-Tindakan' ")
        query.AppendLine("AND tblPaket.DeletedBy IS NULL ")

        ' Filter: Hanya tampilkan tindakan yang MINIMAL SUDAH 1x TERPAKAI di tabel LogPenggunaanPaket
        query.AppendLine("AND EXISTS ( ")
        query.AppendLine("    SELECT 1 FROM LogPenggunaanPaket tblLog WITH(NOLOCK) ")
        query.AppendLine("    LEFT JOIN TransaksiDetail TDLog WITH(NOLOCK) ON TDLog.NoUrut = tblLog.NoTransaksi ")
        query.AppendLine("    WHERE tblLog.PaketID = tblPaketDetail.PaketID ")
        query.AppendLine("      AND tblLog.KodeVoucher = tblPaketDetail.KodeVoucher ")
        query.AppendLine("      AND tblLog.TindakanID = tblPaketDetail.TindakanID ")
        query.AppendLine("      AND tblLog.DeletedBy IS NULL ")

        If namaPoli <> "" Then
            query.AppendLine("      AND LTRIM(RTRIM(TDLog.JenisPoli)) = '" & namaPoli & "' ")
        End If

        If Not CheckBoxFilterTanggal.Checked Then
            Dim tglAwal As String = TglAwalTransaksi.Value.ToString("yyyy-MM-dd 00:00:00")
            Dim tglAkhir As String = TglAkhirTransaksi.Value.ToString("yyyy-MM-dd 23:59:59")
            query.AppendLine("      AND tblLog.TanggalTransaksi >= '" & tglAwal & "' ")
            query.AppendLine("      AND tblLog.TanggalTransaksi <= '" & tglAkhir & "' ")
        End If

        query.AppendLine(") ")

        If cleanNoRM <> "" Then
            query.AppendLine("AND tblPaket.NoRekamMedis = '" & cleanNoRM & "' ")
        End If

        query.AppendLine("ORDER BY tblPaket.PaketID, tblPaket.KodeVoucher, tblPaketDetail.TindakanID ASC")

        tblResult = ReadSQL(query.ToString())
        Return tblResult
    End Function

    Public Function GetDataLaporanDetailPaketPerTindakan(ByVal NoRM As String) As DataTable
        Dim tblResult As New DataTable()
        Dim query As New StringBuilder()

        Dim namaPoli As String = ComboBoxPoli.Text.Trim().Replace("'", "''")
        Dim cleanNoRM As String = NoRM.Trim().Replace("'", "''")

        query.AppendLine("SELECT ")
        query.AppendLine("    tblLog.NoTransaksi AS NoTransaksiPakai, ")
        query.AppendLine("    tblPasien.Nama AS NamaPasien, ")
        query.AppendLine("    tblLog.NoRekamMedis AS NoRM, ")
        query.AppendLine("    tblLog.KodeVoucher, ")
        query.AppendLine("    tblLog.PaketID, ")
        query.AppendLine("    tblLog.TindakanID, ")
        query.AppendLine("    tblLog.TanggalTransaksi AS TglPakai, ")
        query.AppendLine("    ISNULL(TD.JenisPoli, 'N/A') AS JenisPoli ")
        query.AppendLine("FROM LogPenggunaanPaket tblLog WITH(NOLOCK) ")
        query.AppendLine("INNER JOIN TransaksiPaketTindakan tblPaket WITH(NOLOCK) ")
        query.AppendLine("    ON tblLog.KodeVoucher = tblPaket.KodeVoucher ")
        query.AppendLine("    AND tblLog.PaketID = tblPaket.PaketID ")
        query.AppendLine("INNER JOIN MasterPasien tblPasien WITH(NOLOCK) ")
        query.AppendLine("    ON tblLog.NoRekamMedis = tblPasien.NoRekamMedis ")
        query.AppendLine("LEFT JOIN ( ")
        query.AppendLine("    SELECT DISTINCT NoUrut, JenisPoli FROM TransaksiDetail WITH(NOLOCK) ")
        query.AppendLine(") TD ON TD.NoUrut = tblLog.NoTransaksi ")
        query.AppendLine("WHERE tblPaket.JenisPaket = 'Paket Per-Tindakan' ")
        query.AppendLine("AND tblLog.DeletedBy IS NULL ")

        If cleanNoRM <> "" Then
            query.AppendLine("AND tblPaket.NoRekamMedis = '" & cleanNoRM & "' ")
        End If

        If namaPoli <> "" Then
            query.AppendLine("AND LTRIM(RTRIM(TD.JenisPoli)) = '" & namaPoli & "' ")
        End If

        If Not CheckBoxFilterTanggal.Checked Then
            Dim tglAwal As String = TglAwalTransaksi.Value.ToString("yyyy-MM-dd 00:00:00")
            Dim tglAkhir As String = TglAkhirTransaksi.Value.ToString("yyyy-MM-dd 23:59:59")
            query.AppendLine("AND tblLog.TanggalTransaksi >= '" & tglAwal & "' AND tblLog.TanggalTransaksi <= '" & tglAkhir & "' ")
        End If

        ' Pastikan Detail Memiliki Header (Mencegah Constraint/Relation Error)
        query.AppendLine("AND EXISTS (")
        query.AppendLine("    SELECT 1 FROM TransaksiPaketTindakanDetail D WITH(NOLOCK) ")
        query.AppendLine("    WHERE D.PaketID = tblLog.PaketID ")
        query.AppendLine("      AND D.TindakanID = tblLog.TindakanID ")
        query.AppendLine("      AND D.KodeVoucher = tblLog.KodeVoucher ")
        query.AppendLine(")")

        query.AppendLine("ORDER BY tblLog.PaketID, tblLog.KodeVoucher, tblLog.TindakanID, tblLog.TanggalTransaksi ASC")

        tblResult = ReadSQL(query.ToString())
        Return tblResult
    End Function

    Private Sub LaporanRiwayatPaket()

        Dim NoRM As String = TextBoxNomorRekamMedis.Text.Trim()

        ' 1. Ambil Data Header dan Detail
        Dim dtHeader As New DataTable()
        Dim dtDetail As New DataTable()

        If RadioButtonOptionPaketReguler.Checked Then
            dtHeader = GetDataLaporanHeaderPaketReguler(NoRM)
            dtDetail = GetDataLaporanDetailPaketReguler(NoRM)
        Else
            dtHeader = GetDataLaporanHeaderPaketPerTindakan(NoRM)
            dtDetail = GetDataLaporanDetailPaketPerTindakan(NoRM)
        End If

        ' Validasi awal jika data tidak ditemukan dari query
        If dtHeader Is Nothing OrElse dtHeader.Rows.Count = 0 Then
            MessageBox.Show("Data paket tidak ditemukan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' 2. Beri Nama Tabel Eksplisit
        dtHeader.TableName = "HeaderPaket"
        dtDetail.TableName = "DetailPaket"

        ' 3. Format Kolom Tambahan pada Detail
        If Not dtDetail.Columns.Contains("TglFormat") Then
            dtDetail.Columns.Add("TglFormat", GetType(String))
        End If

        For Each rowDetail As DataRow In dtDetail.Rows
            If Not IsDBNull(rowDetail("TglPakai")) Then
                rowDetail("TglFormat") = Format(CDate(rowDetail("TglPakai")), "dd/MM/yyyy HH:mm")
            Else
                rowDetail("TglFormat") = ""
            End If
        Next

        ' 4. Tambahkan Kolom Kalkulasi pada Header
        If Not dtHeader.Columns.Contains("TotalTerpakai") Then
            dtHeader.Columns.Add("TotalTerpakai", GetType(Integer))
        End If

        If Not dtHeader.Columns.Contains("SisaKuota") Then
            dtHeader.Columns.Add("SisaKuota", GetType(Integer))
        End If

        If Not dtHeader.Columns.Contains("PasienLengkap") Then
            dtHeader.Columns.Add("PasienLengkap", GetType(String))
        End If

        ' 5. Proses Hitung Total Terpakai dan Sisa Kuota per Paket
        For i As Integer = dtHeader.Rows.Count - 1 To 0 Step -1
            Dim rowHeader As DataRow = dtHeader.Rows(i)
            Dim paketID As String = rowHeader("PaketID").ToString()
            Dim kodeVoucher As String = rowHeader("KodeVoucher").ToString()
            Dim kuotaTransaksi As Integer = Convert.ToInt32(IIf(IsDBNull(rowHeader("KuotaBeli")), 0, rowHeader("KuotaBeli")))

            Dim totalTerpakai As Integer = 0

            If RadioButtonOptionPaketReguler.Checked Then
                totalTerpakai = dtDetail.Select("PaketID = '" & paketID & "' AND KodeVoucher = '" & kodeVoucher & "'").Length
            Else
                Dim tindakanID As String = rowHeader("TindakanID").ToString()
                totalTerpakai = dtDetail.Select("PaketID = '" & paketID & "' AND KodeVoucher = '" & kodeVoucher & "' AND TindakanID = '" & tindakanID & "'").Length
            End If

            ' 🔥 HAPUS DARI HEADER JIKA PAKET PER-TINDAKAN SAMA SEKALI BELUM TERPAKAI (0)
            If Not RadioButtonOptionPaketReguler.Checked AndAlso totalTerpakai = 0 Then
                dtHeader.Rows.RemoveAt(i)
            Else
                rowHeader("TotalTerpakai") = totalTerpakai

                ' 🔥 PERBAIKAN LOGIKA SISA KUOTA:
                If RadioButtonOptionPaketReguler.Checked Then
                    Dim sisa As Integer = kuotaTransaksi - totalTerpakai
                    rowHeader("SisaKuota") = IIf(sisa < 0, 0, sisa)
                Else
                    ' Paket Per-Tindakan: Sisa Kuota diambil langsung dari kolom database (KuotaTindakan/SisaKuotaDb)
                    rowHeader("SisaKuota") = Convert.ToInt32(IIf(IsDBNull(rowHeader("SisaKuotaDb")), 0, rowHeader("SisaKuotaDb")))
                End If

                rowHeader("PasienLengkap") = rowHeader("NamaPasien").ToString() & " (" & rowHeader("NoRM").ToString() & ")"
            End If
        Next

        ' 🔥 VALIDASI KEDUA: Jika setelah pembersihan tidak ada baris tersisa
        If dtHeader.Rows.Count = 0 Then
            MessageBox.Show("Data transaksi pemakaian paket tidak ditemukan pada periode ini.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' 6. Masukkan ke dalam DataSet & Buat Relasi Parent-Child
        Dim dsLaporan As New DataSet("dsMutasiPaket")
        dsLaporan.Tables.Add(dtHeader)
        dsLaporan.Tables.Add(dtDetail)

        Dim parentKeys() As DataColumn
        Dim childKeys() As DataColumn

        If RadioButtonOptionPaketReguler.Checked Then
            parentKeys = New DataColumn() { _
                dsLaporan.Tables("HeaderPaket").Columns("PaketID"), _
                dsLaporan.Tables("HeaderPaket").Columns("KodeVoucher") _
            }
            childKeys = New DataColumn() { _
                dsLaporan.Tables("DetailPaket").Columns("PaketID"), _
                dsLaporan.Tables("DetailPaket").Columns("KodeVoucher") _
            }
        Else
            parentKeys = New DataColumn() { _
                dsLaporan.Tables("HeaderPaket").Columns("PaketID"), _
                dsLaporan.Tables("HeaderPaket").Columns("KodeVoucher"), _
                dsLaporan.Tables("HeaderPaket").Columns("TindakanID") _
            }
            childKeys = New DataColumn() { _
                dsLaporan.Tables("DetailPaket").Columns("PaketID"), _
                dsLaporan.Tables("DetailPaket").Columns("KodeVoucher"), _
                dsLaporan.Tables("DetailPaket").Columns("TindakanID") _
            }
        End If

        Dim rel As New DataRelation("RelasiPaketDetail", parentKeys, childKeys, False)
        dsLaporan.Relations.Add(rel)

        ' 7. Inisialisasi DevExpress XtraReport
        Report = New DevExpress.XtraReports.UI.XtraReport()

        Dim reportPath As String = ""

        If RadioButtonOptionPaketReguler.Checked Then
            reportPath = Application.StartupPath & "\Reports\LaporanPengambilanPaketReguler.repx"
        Else
            reportPath = Application.StartupPath & "\Reports\LaporanPengambilanPaketPerTindakan.repx"
        End If

        Report = DevExpress.XtraReports.UI.XtraReport.FromFile(reportPath, True)

        ' Header Tanggal & Judul Laporan
        Dim stringTglAwal As String = Format(TglAwalTransaksi.Value, "dd MMMM yyyy")
        Dim stringTglAkhir As String = Format(TglAkhirTransaksi.Value, "dd MMMM yyyy")

        If RadioButtonOptionPaketReguler.Checked Then
            Report.Bands.Item(DevExpress.XtraReports.UI.BandKind.ReportHeader).Controls("xrLabelJudulLaporan").Text = "Laporan Mutasi Paket Reguler"
        Else
            Report.Bands.Item(DevExpress.XtraReports.UI.BandKind.ReportHeader).Controls("xrLabelJudulLaporan").Text = "Laporan Mutasi Paket Per-Tindakan"
        End If

        If CheckBoxFilterTanggal.Checked Then
            Report.Bands.Item(DevExpress.XtraReports.UI.BandKind.ReportHeader).Controls("xrLabelTanggalLaporan").Text = "Semua Tanggal"
        Else
            Report.Bands.Item(DevExpress.XtraReports.UI.BandKind.ReportHeader).Controls("xrLabelTanggalLaporan").Text = stringTglAwal & " - " & stringTglAkhir
        End If

        ' Footer Cetak
        Report.Bands.Item(DevExpress.XtraReports.UI.BandKind.PageFooter).Controls("xrLabelInfoCetak").Text = Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") & " By " & userInput

        ' Bind DataSet
        Report.DataSource = dsLaporan
        Report.DataMember = "HeaderPaket"

        ' Preview / Designer
        Report.ShowPreview()
        'Report.RunDesigner()

    End Sub

    'Private Sub LaporanRiwayatPaket()

    '    Dim NoRM As String = TextBoxNomorRekamMedis.Text.Trim()

    '    'If NoRM = "" Then
    '    '    MessageBox.Show("Nomor Rekam Medis belum diisi.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    '    Exit Sub
    '    'End If

    '    Dim tblData As New DataTable

    '    'If RadioButtonOptionPaketReguler.Checked Then
    '    '    tblData = GetDataDetailPaketRegulerLaporan()
    '    'Else
    '    '    tblData = GetDataDetailPaketPerTindakanLaporan()
    '    'End If

    '    If RadioButtonOptionPaketReguler.Checked Then
    '        tblData = GetDataDetailPaketRegulerLaporan(NoRM)
    '    Else
    '        tblData = GetDataDetailPaketPerTindakanLaporan(NoRM)
    '    End If

    '    If tblData.Rows.Count = 0 Then
    '        MessageBox.Show("Data paket tidak ditemukan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Exit Sub
    '    End If

    '    If tblData.Columns.Contains("No") = False Then
    '        tblData.Columns.Add("No", GetType(Integer))
    '    End If

    '    tblData.Columns("No").ReadOnly = False

    '    Dim rowNumber As Integer = 0
    '    Dim lastPoli As String = ""

    '    If Not tblData.Columns.Contains("TglFormat") Then
    '        tblData.Columns.Add("TglFormat", GetType(String))
    '    End If

    '    For Each Trow As DataRow In tblData.Rows

    '        If Not IsDBNull(Trow("Tgl")) Then
    '            Trow("TglFormat") = Format(CDate(Trow("Tgl")), "MM/dd/yyyy HH:mm")
    '        Else
    '            Trow("TglFormat") = ""
    '        End If

    '        If lastPoli <> Trow("JenisPoli").ToString() Then
    '            rowNumber = 1
    '            lastPoli = Trow("JenisPoli").ToString()
    '        Else
    '            rowNumber += 1
    '        End If

    '        Trow("No") = rowNumber

    '    Next

    '    Report = New XtraReport
    '    Report = XtraReport.FromFile(Application.StartupPath & "\Reports\LaporanPengambilanPaket.repx", True)


    '    'HEADER LAPORAN
    '    Dim tglAwal As Date = TglAwalTransaksi.Value
    '    Dim tglAkhir As Date = TglAkhirTransaksi.Value

    '    Dim stringTglAwal As String = Format(tglAwal, "dd MMMM yyyy")
    '    Dim stringTglAkhir As String = Format(tglAkhir, "dd MMMM yyyy")

    '    If RadioButtonOptionPaketReguler.Checked Then
    '        Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelJudulLaporan").Text = "Laporan Pengambilan Paket Reguler"
    '    Else
    '        Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelJudulLaporan").Text = "Laporan Pengambilan Paket Per-Tindakan"
    '    End If

    '    If CheckBoxFilterTanggal.Checked Then
    '        Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelTanggalLaporan").Text = "Semua Tanggal"
    '    Else
    '        Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelTanggalLaporan").Text = stringTglAwal & " - " & stringTglAkhir
    '    End If

    '    'Ambil semua GroupHeader
    '    For Each band As DevExpress.XtraReports.UI.Band In Report.Bands

    '        If TypeOf band Is DevExpress.XtraReports.UI.GroupHeaderBand Then

    '            Dim gh As DevExpress.XtraReports.UI.GroupHeaderBand = CType(band, DevExpress.XtraReports.UI.GroupHeaderBand)

    '            gh.GroupFields.Clear()
    '            gh.GroupFields.Add(New DevExpress.XtraReports.UI.GroupField("JenisPoli", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending))

    '        End If

    '    Next

    '    'FOOTER CETAK
    '    Report.Bands.Item(BandKind.PageFooter).Controls("xrLabelInfoCetak").Text = Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") & " By " & userInput


    '    'BIND DATA
    '    Report.DataSource = tblData


    '    'PREVIEW
    '    Report.ShowPreview()
    '    'Report.RunDesigner()
    'End Sub

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
        query.AppendLine("DECLARE @JenisPoli VARCHAR(50) = '" & ComboBoxPoli.Text.Trim.ToUpper & "'")

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
        query.AppendLine("    AND (@JenisPoli = '' OR TD.JenisPoli = @JenisPoli)")
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

    Private Function Laporan10TindakanTerbanyak() As DataTable

        If String.IsNullOrEmpty(ComboBoxPoli.Text) Then
            MessageBox.Show("Poli wajib dipilih sebelum menampilkan laporan.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ComboBoxPoli.Focus()
            Exit Function
        End If

        Dim tglAwal As Date = TglAwalTransaksi.Value
        Dim tglAkhir As Date = TglAkhirTransaksi.Value

        Dim tblResult As New DataTable
        Dim query As New StringBuilder()

        ' 🔥 Checkbox:
        ' dicentang = semua tanggal (0)
        ' tidak dicentang = pakai filter (1)
        Dim pakaiFilterTanggal As Integer = If(CheckBoxFilterTanggal.Checked, 0, 1)

        ' =========================
        ' PARAMETER
        ' =========================
        query.AppendLine("DECLARE @JenisPoli VARCHAR(50) = '" & ComboBoxPoli.Text.Trim.ToUpper & "'")
        query.AppendLine("DECLARE @TglAwal DATE = '" & Format(tglAwal, "yyyy-MM-dd") & "'")
        query.AppendLine("DECLARE @TglAkhir DATE = '" & Format(tglAkhir, "yyyy-MM-dd") & "'")
        query.AppendLine("DECLARE @PakaiFilterTanggal BIT = " & pakaiFilterTanggal)

        ' =========================
        ' QUERY
        ' =========================
        query.AppendLine("SELECT TOP 100")
        query.AppendLine("    TPD.TindakanID,")
        query.AppendLine("    TPD.NamaTindakan,")
        query.AppendLine("    TD.JenisPoli,")
        query.AppendLine("    COUNT(DISTINCT TP.NoUrut + '-' + CAST(TPD.TindakanID AS VARCHAR)) AS Jumlah")

        query.AppendLine("FROM TransaksiPoli TP")

        query.AppendLine("INNER JOIN TransaksiPoliDetail TPD")
        query.AppendLine("    ON TP.TransPoliID = TPD.TransPoliID")

        query.AppendLine("INNER JOIN TransaksiDetail TD")
        query.AppendLine("    ON TD.NoUrut = TP.NoUrut")

        query.AppendLine("WHERE")
        query.AppendLine("    TPD.JenisTindakan NOT LIKE '%Konsultasi%'")
        query.AppendLine("    AND TPD.JenisTindakan <> '-'")

        ' 🔥 FILTER POLI
        query.AppendLine("    AND (@JenisPoli = '' OR TD.JenisPoli = @JenisPoli)")

        ' 🔥 FILTER TANGGAL (AMAN)
        query.AppendLine("    AND (")
        query.AppendLine("        @PakaiFilterTanggal = 0")
        query.AppendLine("        OR (TPD.transaksitime >= @TglAwal")
        query.AppendLine("            AND TPD.transaksitime < DATEADD(DAY, 1, @TglAkhir))")
        query.AppendLine("    )")

        query.AppendLine("GROUP BY")
        query.AppendLine("    TPD.TindakanID,")
        query.AppendLine("    TD.JenisPoli,")
        query.AppendLine("    TPD.NamaTindakan")

        query.AppendLine("ORDER BY")
        query.AppendLine("    COUNT(*) DESC")

        ' =========================
        ' EKSEKUSI
        ' =========================
        ' MsgBox(query.ToString()) ' debug kalau perlu

        tblResult = ReadSQL(query.ToString())

        ' TAMBAH KOLOM NO (ranking)
        If Not tblResult.Columns.Contains("No") Then
            tblResult.Columns.Add("No", GetType(Integer))
        End If

        tblResult.DefaultView.Sort = "Jumlah DESC"
        tblResult = tblResult.DefaultView.ToTable()
        Dim rowNumber As Integer = 1

        For Each row As DataRow In tblResult.Rows
            row("No") = rowNumber
            rowNumber += 1
        Next

        Report = New XtraReport
        Report = XtraReport.FromFile(Application.StartupPath & "\Reports\LaporanTindakanTerbanyak.repx", True)

        Dim stringTglAwal As String = Format(tglAwal, "dd MMMM yyyy")
        Dim stringTglAkhir As String = Format(tglAkhir, "dd MMMM yyyy")

        Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelJudulLaporan").Text = "Laporan 10 Tindakan Terbanyak"

        'If ComboBoxPoli.Text <> "" Then
        '    Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelSubJudul").Text = "Poli: " & ComboBoxPoli.Text
        'Else
        '    Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelSubJudul").Text = "Semua Poli"
        'End If

        If CheckBoxFilterTanggal.Checked Then
            Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelTanggalLaporan").Text = "Semua Tanggal"
        Else
            Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelTanggalLaporan").Text = stringTglAwal & " - " & stringTglAkhir
        End If

        Report.Bands.Item(BandKind.PageFooter).Controls("xrLabelInfoCetak").Text = Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") & " By " & userInput

        Report.DataSource = tblResult
        'Report.RunDesigner()
        Report.ShowPreview()

        Return tblResult
    End Function

    Private Sub ButtonLaporanPdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLaporanPdf.Click
        If CheckBoxDownloadLaporanTindakan.Checked Then
            LaporanTindakan()
        ElseIf CheckBoxDownload10Tindakan.Checked Then
            Laporan10TindakanTerbanyak()
        Else
            LaporanRiwayatPaket()
        End If
    End Sub
End Class