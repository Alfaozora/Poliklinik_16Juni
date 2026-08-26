Imports System.Net
Imports System.IO
Imports System.Text

Public Class FormRevenueSelling
    Dim dtRaw As DataTable
    Private userInput As String = PoliKlinik.Lbluser.Text

    Private Sub FormRevenueSelling_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim namaBulan() As String = { _
            "Januari", "Februari", "Maret", "April", "Mei", "Juni", _
            "Juli", "Agustus", "September", "Oktober", "November", "Desember" _
        }

        ComboBoxBulan.Items.Clear()
        ComboBoxBulan.Items.AddRange(namaBulan)

        ComboBoxBulan.SelectedIndex = Date.Today.Month - 1

        Dim tahunSekarang As Integer = Date.Today.Year

        ComboBoxTahun.Items.Clear()
        For i As Integer = tahunSekarang To (tahunSekarang - 4) Step -1
            ComboBoxTahun.Items.Add(i.ToString())
        Next

        ComboBoxTahun.SelectedIndex = 0
    End Sub

    'Private Sub ButtonCetakLaporan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCetakLaporan.Click
    '    If ComboBoxPoli.SelectedIndex = -1 Then
    '        MsgBox("Silahkan pilih Jenis Poli terlebih dahulu.", MsgBoxStyle.Exclamation)
    '        Exit Sub
    '    End If

    '    Dim jenisPoli As String = ComboBoxPoli.Text.Trim()
    '    Dim tglAwal As Date
    '    Dim tglAkhir As Date

    '    ' 2. TENTUKAN RENTANG TANGGAL
    '    If RadioButtonHarian.Checked Then
    '        tglAwal = dtAwal.Value.Date
    '        tglAkhir = dtAkhir.Value.Date
    '    ElseIf RadioButtonBulanan.Checked Then
    '        If ComboBoxBulan.SelectedIndex = -1 OrElse ComboBoxTahun.SelectedIndex = -1 Then
    '            MsgBox("Silahkan pilih Bulan dan Tahun terlebih dahulu.", MsgBoxStyle.Exclamation)
    '            Exit Sub
    '        End If
    '        Dim bulan As Integer = ComboBoxBulan.SelectedIndex + 1
    '        Dim tahun As Integer = Convert.ToInt32(ComboBoxTahun.Text)
    '        tglAwal = New Date(tahun, bulan, 1)
    '        tglAkhir = New Date(tahun, bulan, Date.DaysInMonth(tahun, bulan))
    '    Else
    '        If ComboBoxTahun.SelectedIndex = -1 Then
    '            MsgBox("Silahkan pilih Tahun terlebih dahulu.", MsgBoxStyle.Exclamation)
    '            Exit Sub
    '        End If
    '        Dim tahun As Integer = Convert.ToInt32(ComboBoxTahun.Text)
    '        tglAwal = New Date(tahun, 1, 1)
    '        tglAkhir = New Date(tahun, 12, 31)
    '    End If

    '    Dim strAwal As String = tglAwal.ToString("yyyy-MM-dd 00:00:00")
    '    Dim strAkhir As String = tglAkhir.ToString("yyyy-MM-dd 23:59:59")
    '    Dim strTglAwalShort As String = tglAwal.ToString("yyyy-MM-dd")
    '    Dim strTglAkhirShort As String = tglAkhir.ToString("yyyy-MM-dd")

    '    Dim sqlTodayCollection As String = String.Format( _
    '        "WITH FilteredTransPoli AS ( " & _
    '        "    SELECT TransPoliID " & _
    '        "    FROM TransaksiPoli " & _
    '        "    WHERE (CONVERT(DATE, TglPoli) BETWEEN '{1}' AND '{2}') AND Deleted = 0 " & _
    '        ") " & _
    '        "SELECT ISNULL(SUM(tblBayar.JumlahBayar - tblBayar.Kembali), 0) AS TotalTodayCollection " & _
    '        "FROM ( " & _
    '        "    SELECT tb.TransPoliID, tb.JumlahBayar, tb.Kembali " & _
    '        "    FROM TransaksiPoliBayar tb " & _
    '        "    INNER JOIN FilteredTransPoli ft ON tb.TransPoliID = ft.TransPoliID " & _
    '        "    WHERE tb.JenisBayar IN ('Tagihan', 'Tunai', 'Debet', 'Card') AND tb.JumlahBayar > 0 " & _
    '        "    UNION ALL " & _
    '        "    SELECT tba.TransPoliID, tba.JumlahBayar, tba.Kembali " & _
    '        "    FROM TransaksiPoliBayarAwal tba " & _
    '        "    INNER JOIN FilteredTransPoli ft ON tba.TransPoliID = ft.TransPoliID " & _
    '        "    WHERE tba.JenisBayar IN ('Tagihan', 'Tunai', 'Debet', 'Card') AND tba.JumlahBayar > 0 " & _
    '        ") AS tblBayar " & _
    '        "LEFT JOIN TransaksiPoli tblPoli ON tblBayar.TransPoliID = tblPoli.TransPoliID " & _
    '        "LEFT JOIN TransaksiDetail tblDetail ON tblPoli.NoUrut = tblDetail.NoUrut " & _
    '        "WHERE ('{0}' = 'SEMUA TRANSAKSI' OR tblDetail.JenisPoli = '{0}')", jenisPoli, strTglAwalShort, strTglAkhirShort)

    '    Dim dtToday As DataTable = ReadSQL(sqlTodayCollection)
    '    Dim totalTodayCollection As Decimal = 0
    '    If dtToday IsNot Nothing AndAlso dtToday.Rows.Count > 0 Then
    '        totalTodayCollection = Convert.ToDecimal(dtToday.Rows(0)("TotalTodayCollection"))
    '    End If

    '    Dim sqlSellingRevenue As String = String.Format( _
    '        "SELECT " & _
    '        "    ISNULL(S.Tgl, R.Tgl) AS Tgl, " & _
    '        "    ISNULL(S.TotalSelling, 0) AS SELLING, " & _
    '        "    ISNULL(R.TotalRevenue, 0) AS REVENUE " & _
    '        "FROM ( " & _
    '        "    SELECT " & _
    '        "        Tgl, " & _
    '        "        SUM(TotalHarga) AS TotalSelling " & _
    '        "    FROM ( " & _
    '        "        SELECT " & _
    '        "            CAST(TPT.CreatedDate AS DATE) AS Tgl, " & _
    '        "            TPT.HargaNett AS TotalHarga " & _
    '        "        FROM TransaksiPaketTindakan TPT " & _
    '        "        INNER JOIN TransaksiDetail TD ON TPT.NoTransaksi = TD.NoUrut " & _
    '        "        WHERE TPT.DeletedDate IS NULL " & _
    '        "          AND TD.JenisPoli = '{0}' " & _
    '        "          AND TD.DeletedBy IS NULL " & _
    '        "          AND TPT.CreatedDate BETWEEN '{1}' AND '{2}' " & _
    '        " " & _
    '        "        UNION ALL " & _
    '        " " & _
    '        "        SELECT " & _
    '        "            CAST(TPD.CreatedDate AS DATE) AS Tgl, " & _
    '        "            TPD.NettBayar AS TotalHarga " & _
    '        "        FROM TransaksiPoliDetail TPD " & _
    '        "        INNER JOIN TransaksiPoli TP ON TPD.TransPoliID = TP.TransPoliID " & _
    '        "        INNER JOIN TransaksiDetail TD ON TP.NoUrut = TD.NoUrut " & _
    '        "        WHERE TPD.DeletedDate IS NULL " & _
    '        "          AND TD.JenisPoli = '{0}' " & _
    '        "          AND TD.DeletedBy IS NULL " & _
    '        "          AND TPD.CreatedDate BETWEEN '{1}' AND '{2}' " & _
    '        "    ) CombinedSelling " & _
    '        "    GROUP BY Tgl " & _
    '        ") S " & _
    '        "FULL OUTER JOIN ( " & _
    '        "    SELECT " & _
    '        "        CAST(TPD.CreatedDate AS DATE) AS Tgl, " & _
    '        "        SUM(TPD.NettBayar) AS TotalRevenue " & _
    '        "    FROM TransaksiPoliDetail TPD " & _
    '        "    INNER JOIN TransaksiPoli TP ON TPD.TransPoliID = TP.TransPoliID " & _
    '        "    INNER JOIN TransaksiDetail TD ON TP.NoUrut = TD.NoUrut " & _
    '        "    WHERE TPD.DeletedDate IS NULL " & _
    '        "      AND TD.JenisPoli = '{0}' " & _
    '        "      AND TD.DeletedBy IS NULL " & _
    '        "      AND TPD.CreatedDate BETWEEN '{1}' AND '{2}' " & _
    '        "    GROUP BY CAST(TPD.CreatedDate AS DATE) " & _
    '        ") R ON S.Tgl = R.Tgl " & _
    '        "ORDER BY Tgl ASC", jenisPoli, strAwal, strAkhir)

    '    Dim dtRaw As DataTable = ReadSQL(sqlSellingRevenue)

    '    If dtRaw.Rows.Count = 0 Then
    '        MsgBox("Tidak ada data laporan untuk " & jenisPoli & " pada periode tersebut.", MsgBoxStyle.Information)
    '        Exit Sub
    '    End If

    '    dtRaw.Columns("SELLING").ReadOnly = False
    '    dtRaw.Columns("SELLING").AllowDBNull = True

    '    dtRaw.Columns("REVENUE").ReadOnly = False
    '    dtRaw.Columns("REVENUE").AllowDBNull = True
    '    dtRaw.Columns.Add("Yesterday_Collection", GetType(Decimal))
    '    dtRaw.Columns.Add("Today_Collection", GetType(Decimal))
    '    dtRaw.Columns.Add("ActualCollectionMTD", GetType(Decimal))
    '    dtRaw.Columns.Add("ActualAchievement", GetType(Decimal))
    '    dtRaw.Columns.Add("Gap_To_Target", GetType(Decimal))
    '    dtRaw.Columns.Add("Projection", GetType(Decimal))
    '    dtRaw.Columns.Add("DailyNeeds", GetType(Decimal))
    '    dtRaw.Columns.Add("TargetBulanan", GetType(Decimal))

    '    Dim targetBulan As Integer = tglAwal.Month
    '    Dim targetTahun As Integer = tglAwal.Year
    '    Dim sqlTarget As String = "SELECT ISNULL(TargetBulanan, 0) FROM MasterTargetPoli WHERE JenisPoli = '" & jenisPoli & "' AND Bulan = " & targetBulan & " AND Tahun = " & targetTahun
    '    Dim dtTarget As DataTable = ReadSQL(sqlTarget)
    '    Dim nominalTarget As Decimal = If(dtTarget.Rows.Count > 0, CDec(dtTarget.Rows(0)(0)), 0)

    '    Dim akumulasiKemarin As Decimal = 0
    '    Dim tanggalTerakhirData As Date = tglAwal

    '    ' Isi data kolom suntikan
    '    For Each row As DataRow In dtRaw.Rows
    '        Dim sellingHariIni As Decimal = CDec(row("SELLING"))
    '        Dim revenueHariIni As Decimal = CDec(row("REVENUE"))

    '        ' Catat tanggal baris berjalan
    '        tanggalTerakhirData = CDate(row("Tgl"))

    '        row("Yesterday_Collection") = Convert.ToDecimal(akumulasiKemarin)
    '        row("TargetBulanan") = Convert.ToDecimal(nominalTarget)

    '        If sellingHariIni = 0 Then row("SELLING") = DBNull.Value
    '        If revenueHariIni = 0 Then row("REVENUE") = DBNull.Value

    '        ' Akumulasikan untuk iterasi berikutnya
    '        akumulasiKemarin = akumulasiKemarin + (sellingHariIni + revenueHariIni)
    '    Next

    '    '2. SUNTIK BARIS BAYANGAN UNTUK HARI INI (H-0 / Tgl 16) 
    '    ' Jika tanggal terakhir di database belum mencapai tanggal akhir filter (tglAkhir)
    '    If tanggalTerakhirData.Date < tglAkhir.Date Then
    '        Dim newRow As DataRow = dtRaw.NewRow()
    '        newRow("Tgl") = tglAkhir.Date
    '        newRow("SELLING") = DBNull.Value
    '        newRow("REVENUE") = DBNull.Value
    '        newRow("Yesterday_Collection") = Convert.ToDecimal(akumulasiKemarin)
    '        newRow("TargetBulanan") = Convert.ToDecimal(nominalTarget)
    '        dtRaw.Rows.Add(newRow)
    '    End If

    '    Dim sqlHariKerja As String = "SELECT ISNULL(HariKerja, 0) FROM MasterTargetPoli WHERE JenisPoli = '" & jenisPoli & "' AND Bulan = " & targetBulan & " AND Tahun = " & targetTahun
    '    Dim dtHariKerja As DataTable = ReadSQL(sqlHariKerja)
    '    Dim totalHariKerja As Integer = If(dtHariKerja.Rows.Count > 0, Convert.ToInt32(dtHariKerja.Rows(0)(0)), 1)
    '    If totalHariKerja = 0 Then totalHariKerja = 1

    '    Dim hariBerjalan As Integer = HitungHariKerjaBerjalan(tglAwal, tglAkhir)
    '    Dim sisaHariKerja As Integer = totalHariKerja - hariBerjalan
    '    If sisaHariKerja <= 0 Then sisaHariKerja = 1

    '    For Each row As DataRow In dtRaw.Rows
    '        Dim yesterdayVal As Decimal = If(IsDBNull(row("Yesterday_Collection")), 0, CDec(row("Yesterday_Collection")))
    '        Dim sellingHariIni As Decimal = If(IsDBNull(row("SELLING")), 0, CDec(row("SELLING")))
    '        Dim revenueHariIni As Decimal = If(IsDBNull(row("REVENUE")), 0, CDec(row("REVENUE")))

    '        Dim mtdVal As Decimal = yesterdayVal + sellingHariIni + revenueHariIni

    '        row("Today_Collection") = Convert.ToDecimal(totalTodayCollection)
    '        row("ActualCollectionMTD") = Convert.ToDecimal(mtdVal)

    '        Dim currentAch As Decimal = 0
    '        If nominalTarget > 0 Then
    '            currentAch = Convert.ToDecimal(mtdVal / nominalTarget)
    '        End If
    '        row("ActualAchievement") = currentAch

    '        row("Gap_To_Target") = Convert.ToDecimal(mtdVal - nominalTarget)

    '        Dim projeksiDesimal As Decimal = (currentAch / Convert.ToDecimal(hariBerjalan)) * Convert.ToDecimal(totalHariKerja)
    '        row("Projection") = Convert.ToDecimal(projeksiDesimal)

    '        Dim sisaTarget As Decimal = nominalTarget - mtdVal
    '        If sisaTarget > 0 Then
    '            If sisaHariKerja > 0 Then
    '                ' Kondisi Normal: Masih ada sisa hari kerja esok hari
    '                row("DailyNeeds") = Convert.ToDecimal(sisaTarget / Convert.ToDecimal(sisaHariKerja))
    '            Else
    '                ' Kondisi Akhir Bulan (Sisa Hari Kerja = 0): 
    '                ' Daily Needs hari terakhir disamakan dengan sisa target yang harus dikejar HARI INI
    '                row("DailyNeeds") = Convert.ToDecimal(sisaTarget)
    '            End If
    '        Else
    '            ' Jika target sudah tercapai/surplus, otomatis 0
    '            row("DailyNeeds") = Convert.ToDecimal(0)
    '        End If
    '    Next


    '    Dim report As New DevExpress.XtraReports.UI.XtraReport
    '    report = XtraReport.FromFile(Application.StartupPath & "\reports\LaporanRevenueSelling.repx", True)

    '    Dim bandHeader As ReportHeaderBand = CType(report.Bands(BandKind.ReportHeader), ReportHeaderBand)
    '    If bandHeader IsNot Nothing Then
    '        If bandHeader.Controls("lblPoli") IsNot Nothing Then bandHeader.Controls("lblPoli").Text = jenisPoli
    '        If bandHeader.Controls("lblPeriode") IsNot Nothing Then bandHeader.Controls("lblPeriode").Text = tglAwal.ToString("dd MMM yyyy") & " - " & tglAkhir.ToString("dd MMM yyyy")
    '    End If

    '    report.DataSource = dtRaw
    '    'report.RunDesigner()
    '    report.ShowPreview()
    'End Sub

    Private Sub ButtonCetakLaporan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCetakLaporan.Click
        If ComboBoxPoli.SelectedIndex = -1 Then
            MsgBox("Silahkan pilih Jenis Poli terlebih dahulu.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim jenisPoli As String = ComboBoxPoli.Text.Trim()
        Dim tglAwal As Date
        Dim tglAkhir As Date

        ' 1. TENTUKAN RENTANG TANGGAL
        If RadioButtonHarian.Checked Then
            tglAwal = dtAwal.Value.Date
            tglAkhir = dtAkhir.Value.Date
        ElseIf RadioButtonBulanan.Checked Then
            If ComboBoxBulan.SelectedIndex = -1 OrElse ComboBoxTahun.SelectedIndex = -1 Then
                MsgBox("Silahkan pilih Bulan dan Tahun terlebih dahulu.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            Dim bulan As Integer = ComboBoxBulan.SelectedIndex + 1
            Dim tahun As Integer = Convert.ToInt32(ComboBoxTahun.Text)
            tglAwal = New Date(tahun, bulan, 1)
            tglAkhir = New Date(tahun, bulan, Date.DaysInMonth(tahun, bulan))
        Else
            If ComboBoxTahun.SelectedIndex = -1 Then
                MsgBox("Silahkan pilih Tahun terlebih dahulu.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            Dim tahun As Integer = Convert.ToInt32(ComboBoxTahun.Text)
            tglAwal = New Date(tahun, 1, 1)
            tglAkhir = New Date(tahun, 12, 31)
        End If

        Dim strAwal As String = tglAwal.ToString("yyyy-MM-dd 00:00:00")
        Dim strAkhir As String = tglAkhir.AddDays(1).ToString("yyyy-MM-dd 00:00:00")

        'Pra Cut Off
        'Dim sqlSellingRevenue As String = String.Format( _
        '            "SELECT " & _
        '            "    ISNULL(S.Tgl, R.Tgl) AS Tgl, " & _
        '            "    ISNULL(S.TotalSelling, 0) AS SELLING, " & _
        '            "    ISNULL(R.TotalRevenue, 0) AS REVENUE " & _
        '            "FROM ( " & _
        '            "    SELECT Tgl, SUM(TotalHarga) AS TotalSelling " & _
        '            "    FROM ( " & _
        '            "        SELECT CAST(TPT.CreatedDate AS DATE) AS Tgl, TPT.HargaNett AS TotalHarga " & _
        '            "        FROM TransaksiPaketTindakan TPT " & _
        '            "        INNER JOIN TransaksiDetail TD ON TPT.NoTransaksi = TD.NoUrut " & _
        '            "        WHERE TPT.DeletedDate IS NULL AND TD.JenisPoli = '{0}' AND TD.DeletedBy IS NULL AND TPT.CreatedDate BETWEEN '{1}' AND '{2}' " & _
        '            "        UNION ALL " & _
        '            "        SELECT CAST(TPD.CreatedDate AS DATE) AS Tgl, TPD.NettBayar AS TotalHarga " & _
        '            "        FROM TransaksiPoliDetail TPD " & _
        '            "        INNER JOIN TransaksiPoli TP ON TPD.TransPoliID = TP.TransPoliID " & _
        '            "        INNER JOIN TransaksiDetail TD ON TP.NoUrut = TD.NoUrut " & _
        '            "        WHERE TPD.DeletedDate IS NULL AND TD.JenisPoli = '{0}' AND TD.DeletedBy IS NULL AND TPD.CreatedDate BETWEEN '{1}' AND '{2}' " & _
        '            "    ) CombinedSelling " & _
        '            "    GROUP BY Tgl " & _
        '            ") S " & _
        '            "FULL OUTER JOIN ( " & _
        '            "    SELECT " & _
        '            "        CAST(TD.TanggalTransaksi AS DATE) AS Tgl, " & _
        '            "        SUM(TPD.NettBayar) AS TotalRevenue " & _
        '            "    FROM TransaksiDetail TD " & _
        '            "    INNER JOIN TransaksiPoliDetail TPD ON TD.NoTransaksi = TPD.TransPoliID " & _
        '            "    WHERE TPD.DeletedBy IS NULL " & _
        '            "      AND TD.DeletedBy IS NULL " & _
        '            "      AND TPD.JenisTindakan = 'Tindakan' " & _
        '            "      AND TPD.StatusPembayaran = 'PAID' " & _
        '            "      AND TD.JenisPoli = '{0}' " & _
        '            "      AND TD.TanggalTransaksi BETWEEN '{1}' AND '{2}' " & _
        '            "      AND EXISTS ( " & _
        '            "          SELECT 1 FROM ( " & _
        '            "              SELECT TransPoliID FROM TransaksiPoliBayar WHERE ISNULL(JumlahBayar, 0) > 0 " & _
        '            "              UNION ALL " & _
        '            "              SELECT TransPoliID FROM TransaksiPoliBayarAwal WHERE ISNULL(JumlahBayar, 0) > 0 " & _
        '            "          ) PB WHERE PB.TransPoliID = TPD.TransPoliID " & _
        '            "      ) " & _
        '            "    GROUP BY CAST(TD.TanggalTransaksi AS DATE) " & _
        '            ") R ON S.Tgl = R.Tgl " & _
        '            "ORDER BY Tgl ASC", jenisPoli, strAwal, strAkhir)

        Dim sqlSellingRevenue As String = String.Format( _
            "SELECT " & _
            "    ISNULL(S.Tgl, R.Tgl) AS Tgl, " & _
            "    ISNULL(S.TotalSelling, 0) AS SELLING, " & _
            "    ISNULL(R.TotalRevenue, 0) AS REVENUE " & _
            "FROM ( " & _
            "    SELECT Tgl, SUM(TotalHarga) AS TotalSelling " & _
            "    FROM ( " & _
            "        SELECT CAST(TPT.CreatedDate AS DATE) AS Tgl, TPT.HargaNett AS TotalHarga " & _
            "        FROM TransaksiPaketTindakan TPT " & _
            "        INNER JOIN TransaksiDetail TD ON CAST(TPT.NoTransaksi AS VARCHAR(100)) = CAST(TD.NoUrut AS VARCHAR(100)) " & _
            "        WHERE TPT.DeletedDate IS NULL AND TD.JenisPoli = '{0}' AND TD.DeletedBy IS NULL AND TPT.CreatedDate BETWEEN '{1}' AND '{2}' " & _
            "        UNION ALL " & _
            "        SELECT CAST(TPD.CreatedDate AS DATE) AS Tgl, (TPD.NettBayar + ISNULL(TP.Pembulatan, 0)) AS TotalHarga " & _
            "        FROM TransaksiPoliDetail TPD " & _
            "        INNER JOIN TransaksiPoli TP ON TPD.TransPoliID = TP.TransPoliID " & _
            "        INNER JOIN TransaksiDetail TD ON CAST(TP.NoUrut AS VARCHAR(100)) = CAST(TD.NoUrut AS VARCHAR(100)) " & _
            "        WHERE TPD.DeletedDate IS NULL AND TD.JenisPoli = '{0}' AND TPD.StatusPembayaran = 'PAID' AND TD.DeletedBy IS NULL AND TPD.CreatedDate BETWEEN '{1}' AND '{2}' " & _
            "    ) CombinedSelling " & _
            "    GROUP BY Tgl " & _
            ") S " & _
            "FULL OUTER JOIN ( " & _
            "    SELECT Tgl, SUM(NettBayar) AS TotalRevenue " & _
            "    FROM ( " & _
            "        SELECT CAST(TD.TanggalTransaksi AS DATE) AS Tgl, (TPD.NettBayar + ISNULL(TP.Pembulatan, 0)) AS NettBayar " & _
            "        FROM TransaksiPoliDetail TPD " & _
            "        INNER JOIN TransaksiPoli TP ON CAST(TPD.TransPoliID AS VARCHAR(100)) = CAST(TP.TransPoliID AS VARCHAR(100)) " & _
            "        INNER JOIN TransaksiDetail TD ON CAST(TP.NoUrut AS VARCHAR(100)) = CAST(TD.NoUrut AS VARCHAR(100)) " & _
            "        WHERE TPD.DeletedBy IS NULL AND TD.DeletedBy IS NULL AND TPD.StatusPembayaran = 'PAID' AND TD.JenisPoli = '{0}' " & _
            "          AND TPD.JenisTindakan = 'Tindakan' AND TD.TanggalTransaksi BETWEEN '{1}' AND '{2}' " & _
            "          AND ISNULL(TPD.PaketID, '0') <> '0' AND TPD.PaketID <> '' " & _
            "          AND EXISTS ( " & _
            "              SELECT 1 FROM TransaksiPaketTindakan TPT " & _
            "              WHERE CAST(TPT.PaketID AS VARCHAR(100)) = CAST(TPD.PaketID AS VARCHAR(100)) " & _
            "                AND (ISNULL(TPD.KodeVoucher, '') = '' OR TPT.KodeVoucher = TPD.KodeVoucher) " & _
            "                AND TPT.DeletedDate IS NULL AND TPT.CreatedDate >= '2026-08-01 00:00:00' " & _
            "          ) " & _
            "          AND EXISTS ( " & _
            "              SELECT 1 FROM ( " & _
            "                  SELECT CAST(TransPoliID AS VARCHAR(100)) AS TransPoliID FROM TransaksiPoliBayar WHERE ISNULL(JumlahBayar, 0) > 0 " & _
            "                  UNION ALL " & _
            "                  SELECT CAST(TransPoliID AS VARCHAR(100)) AS TransPoliID FROM TransaksiPoliBayarAwal WHERE ISNULL(JumlahBayar, 0) > 0 " & _
            "              ) PB WHERE PB.TransPoliID = CAST(TPD.TransPoliID AS VARCHAR(100)) " & _
            "          ) " & _
            "        UNION ALL " & _
            "        SELECT CAST(TD.TanggalTransaksi AS DATE) AS Tgl, (TPD.NettBayar + ISNULL(TP.Pembulatan, 0)) AS NettBayar " & _
            "        FROM TransaksiDetail TD " & _
            "        INNER JOIN TransaksiPoliDetail TPD ON CAST(TD.NoTransaksi AS VARCHAR(100)) = CAST(TPD.TransPoliID AS VARCHAR(100)) " & _
            "        INNER JOIN TransaksiPoli TP ON CAST(TPD.TransPoliID AS VARCHAR(100)) = CAST(TP.TransPoliID AS VARCHAR(100)) " & _
            "        WHERE TPD.DeletedBy IS NULL AND TD.DeletedBy IS NULL AND TPD.StatusPembayaran = 'PAID' AND TD.JenisPoli = '{0}' " & _
            "          AND TPD.JenisTindakan = 'Tindakan' AND TD.TanggalTransaksi BETWEEN '{1}' AND '{2}' " & _
            "          AND (ISNULL(TPD.PaketID, '') = '' OR TPD.PaketID = '0') " & _
            "          AND EXISTS ( " & _
            "              SELECT 1 FROM ( " & _
            "                  SELECT CAST(TransPoliID AS VARCHAR(100)) AS TransPoliID FROM TransaksiPoliBayar WHERE ISNULL(JumlahBayar, 0) > 0 " & _
            "                  UNION ALL " & _
            "                  SELECT CAST(TransPoliID AS VARCHAR(100)) AS TransPoliID FROM TransaksiPoliBayarAwal WHERE ISNULL(JumlahBayar, 0) > 0 " & _
            "              ) PB WHERE PB.TransPoliID = CAST(TPD.TransPoliID AS VARCHAR(100)) " & _
            "          ) " & _
            "    ) CombinedRevenue " & _
            "    GROUP BY Tgl " & _
            ") R ON S.Tgl = R.Tgl " & _
            "ORDER BY Tgl ASC", jenisPoli, strAwal, strAkhir)


        Dim dtRaw As DataTable = ReadSQL(sqlSellingRevenue)

        If dtRaw IsNot Nothing AndAlso dtRaw.Rows.Count = 0 Then
            MsgBox("Tidak ada data laporan untuk " & jenisPoli & " pada periode tersebut.", MsgBoxStyle.Information)
            Exit Sub
        End If

        dtRaw.Columns("SELLING").ReadOnly = False
        dtRaw.Columns("SELLING").AllowDBNull = True
        dtRaw.Columns("REVENUE").ReadOnly = False
        dtRaw.Columns("REVENUE").AllowDBNull = True

        ' Tambahkan kolom pendukung laporan
        dtRaw.Columns.Add("Yesterday_Collection", GetType(Decimal))
        dtRaw.Columns.Add("ActualCollectionMTD", GetType(Decimal))
        dtRaw.Columns.Add("ActualAchievement", GetType(Decimal))
        dtRaw.Columns.Add("Gap_To_Target", GetType(Decimal))
        dtRaw.Columns.Add("Projection", GetType(Decimal))
        dtRaw.Columns.Add("DailyNeeds", GetType(Decimal))
        dtRaw.Columns.Add("TargetBulanan", GetType(Decimal))
        dtRaw.Columns.Add("Persentase_Selling_Revenue", GetType(Decimal))

        dtRaw.Columns.Add("TotalHariKalender", GetType(Integer))
        dtRaw.Columns.Add("TotalHariKerja", GetType(Integer))
        dtRaw.Columns.Add("HariBerjalan", GetType(Integer))
        dtRaw.Columns.Add("SisaHariKerja", GetType(Integer))

        ' Get Target Bulanan & Hari Kerja
        Dim targetBulan As Integer = tglAwal.Month
        Dim targetTahun As Integer = tglAwal.Year
        Dim sqlTarget As String = "SELECT ISNULL(TargetBulanan, 0), ISNULL(HariKerja, 0) FROM MasterTargetPoli WHERE JenisPoli = '" & jenisPoli & "' AND Bulan = " & targetBulan & " AND Tahun = " & targetTahun
        Dim dtTarget As DataTable = ReadSQL(sqlTarget)
        Dim nominalTarget As Decimal = If(dtTarget.Rows.Count > 0, CDec(dtTarget.Rows(0)(0)), 0)
        Dim totalHariKerja As Integer = If(dtTarget.Rows.Count > 0 AndAlso CInt(dtTarget.Rows(0)(1)) > 0, CInt(dtTarget.Rows(0)(1)), 1)

        ' SUNTIK BARIS BAYANGAN JIKA BELUM ADA TRANSAKSI DI TANGGAL AKHIR
        Dim tanggalTerakhirData As Date = tglAwal
        For Each r As DataRow In dtRaw.Rows
            tanggalTerakhirData = CDate(r("Tgl"))
        Next

        If tanggalTerakhirData.Date < tglAkhir.Date Then
            Dim newRow As DataRow = dtRaw.NewRow()
            newRow("Tgl") = tglAkhir.Date
            newRow("SELLING") = DBNull.Value
            newRow("REVENUE") = DBNull.Value
            dtRaw.Rows.Add(newRow)
        End If

        ' METERAN KALENDER & HARI KERJA
        Dim totalHariKalender As Integer = Date.DaysInMonth(tglAwal.Year, tglAwal.Month)
        Dim hariBerjalan As Integer = HitungHariKerjaBerjalan(tglAwal, tglAkhir)
        Dim sisaHariKerja As Integer = Math.Max(0, totalHariKerja - hariBerjalan)

        ' PERBAIKAN UTAMA: HITUNG RUNNING TOTAL (MTD & YESTERDAY)
        Dim runningTotalMTD As Decimal = 0

        For Each row As DataRow In dtRaw.Rows
            Dim sellingHariIni As Decimal = If(IsDBNull(row("SELLING")), 0, CDec(row("SELLING")))
            Dim revenueHariIni As Decimal = If(IsDBNull(row("REVENUE")), 0, CDec(row("REVENUE")))

            ' Set Yesterday = MTD Kumulatif Sebelum Hari Ini
            Dim yesterdayCol As Decimal = runningTotalMTD

            ' PERBAIKAN: Gunakan revenueHariIni agar Actual Collection sesuai dengan Revenue
            Dim todayCol As Decimal = sellingHariIni

            ' Update MTD = Yesterday + Today
            runningTotalMTD += todayCol

            ' Assignment Nilai Kolom
            row("Yesterday_Collection") = yesterdayCol
            row("ActualCollectionMTD") = runningTotalMTD

            row("TargetBulanan") = nominalTarget
            row("TotalHariKalender") = totalHariKalender
            row("TotalHariKerja") = totalHariKerja
            row("HariBerjalan") = hariBerjalan
            row("SisaHariKerja") = sisaHariKerja

            ' Rasio Selling vs Revenue
            row("Persentase_Selling_Revenue") = If(sellingHariIni > 0, revenueHariIni / sellingHariIni, 0)

            If sellingHariIni = 0 Then row("SELLING") = DBNull.Value
            If revenueHariIni = 0 Then row("REVENUE") = DBNull.Value

            ' Achievement & Gap
            Dim currentAch As Decimal = If(nominalTarget > 0, runningTotalMTD / nominalTarget, 0)
            row("ActualAchievement") = currentAch
            row("Gap_To_Target") = runningTotalMTD - nominalTarget

            ' Projection
            Dim projeksiDesimal As Decimal = If(hariBerjalan > 0, (currentAch / CDec(hariBerjalan)) * CDec(totalHariKerja), 0)
            row("Projection") = projeksiDesimal

            ' Daily Needs
            Dim sisaTarget As Decimal = nominalTarget - runningTotalMTD
            If sisaTarget > 0 Then
                row("DailyNeeds") = If(sisaHariKerja > 0, sisaTarget / CDec(sisaHariKerja), sisaTarget)
            Else
                row("DailyNeeds") = 0
            End If
        Next

        Dim nilaiTodayCollection As Decimal = 0
        If dtRaw.Rows.Count > 0 Then
            Dim lastRow As DataRow = dtRaw.Rows(dtRaw.Rows.Count - 1)
            nilaiTodayCollection = If(IsDBNull(lastRow("REVENUE")), 0, CDec(lastRow("REVENUE")))
        End If

        ' BUKA REPORT & RUN DESIGNER
        Dim report As New DevExpress.XtraReports.UI.XtraReport
        report = DevExpress.XtraReports.UI.XtraReport.FromFile(Application.StartupPath & "\reports\LaporanRevenueSelling.repx", True)

        Dim bandHeader As DevExpress.XtraReports.UI.ReportHeaderBand = CType(report.Bands(DevExpress.XtraReports.UI.BandKind.ReportHeader), DevExpress.XtraReports.UI.ReportHeaderBand)
        If bandHeader IsNot Nothing Then
            If bandHeader.Controls("lblPoli") IsNot Nothing Then bandHeader.Controls("lblPoli").Text = jenisPoli
            If bandHeader.Controls("lblPeriode") IsNot Nothing Then bandHeader.Controls("lblPeriode").Text = tglAwal.ToString("dd MMM yyyy") & " - " & tglAkhir.ToString("dd MMM yyyy")
        End If

        ' Untuk Cell Tabel Gunakan XRTableCell (bukan XRLabel)
        Dim lblTodayCollection As DevExpress.XtraReports.UI.XRTableCell = CType(report.FindControl("lblTodayCollection", True), DevExpress.XtraReports.UI.XRTableCell)

        If lblTodayCollection IsNot Nothing Then
            ' Tambahkan string "Rp " sebelum angka ribuan
            lblTodayCollection.Text = "Rp " & nilaiTodayCollection.ToString("N0")
        Else
            ' Fallback jika di Report berupa XRLabel biasa
            Dim lblTodayLabel As DevExpress.XtraReports.UI.XRLabel = CType(report.FindControl("lblTodayCollection", True), DevExpress.XtraReports.UI.XRLabel)
            If lblTodayLabel IsNot Nothing Then
                lblTodayLabel.Text = "Rp " & nilaiTodayCollection.ToString("N0")
            End If
        End If

        report.DataSource = dtRaw
        'report.RunDesigner()
        report.ShowPreview()
    End Sub

    Private Function HitungHariKerjaBerjalan(ByVal tglAwal As Date, ByVal tglAkhir As Date) As Integer
        Dim jumlahHariKerja As Integer = 0
        Dim tglBerjalan As Date = tglAwal.Date

        ' ⭐ 1. TARIK SEMUA DAFTAR LIBUR SEKALIGUS DI AWAL PERIODE
        Dim sqlSemuaLibur As String = "SELECT Tanggal FROM MasterLibur WHERE Tanggal BETWEEN '" & _
                                      tglAwal.ToString("yyyy-MM-dd") & "' AND '" & _
                                      tglAkhir.ToString("yyyy-MM-dd") & "'"
        Dim dtSemuaLibur As DataTable = ReadSQL(sqlSemuaLibur)

        ' 2. LOOPING KALENDER HARI DEMI HARI
        Do While tglBerjalan <= tglAkhir.Date
            ' Cek jika bukan hari Minggu
            If tglBerjalan.DayOfWeek <> DayOfWeek.Sunday Then

                ' ⭐ 3. FILTER DATA DI MEMORI LOCAL (MENGGUNAKAN .SELECT DATATABLE)
                ' Kita cari apakah tanggal berjalan ada di dalam dtSemuaLibur
                ' Karena kolom bertipe Date/DateTime, gunakan format ekspresi string untuk filter ADO.NET (#yyyy-MM-dd#)
                Dim filterEkspresi As String = "Tanggal = #" & tglBerjalan.ToString("yyyy-MM-dd") & "#"
                Dim barisLibur() As DataRow = dtSemuaLibur.Select(filterEkspresi)

                ' Jika barisLibur.Length = 0 artinya tanggal ini tidak terdaftar di MasterLibur (Hari Kerja Aktif)
                If barisLibur.Length = 0 Then
                    jumlahHariKerja = jumlahHariKerja + 1
                End If
            End If

            ' Lanjut ke tanggal berikutnya
            tglBerjalan = tglBerjalan.AddDays(1)
        Loop

        ' Proteksi pembagian angka 0 (Division by Zero)
        If jumlahHariKerja = 0 Then jumlahHariKerja = 1

        Return jumlahHariKerja
    End Function

    Private Sub RadioButtonHarian_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonHarian.CheckedChanged
        dtAwal.Enabled = RadioButtonHarian.Checked
        dtAkhir.Enabled = RadioButtonHarian.Checked
    End Sub

    Private Sub RadioButtonBulanan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonBulanan.CheckedChanged
        ComboBoxBulan.Enabled = RadioButtonBulanan.Checked
        ComboBoxTahun.Enabled = RadioButtonBulanan.Checked
    End Sub

    Private Sub RadioButtonTahunan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonTahunan.CheckedChanged
        ComboBoxBulan.Enabled = False
        ComboBoxTahun.Enabled = RadioButtonTahunan.Checked
    End Sub

    Private Sub ButtonSimpanTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSimpanTarget.Click
        If ComboBoxPoliTarget.SelectedIndex = -1 Then
            MsgBox("Silahkan pilih Jenis Poli terlebih dahulu.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If TextBoxTargetBulanan.Text.Trim() = "" OrElse Not IsNumeric(TextBoxTargetBulanan.Text) Then
            MsgBox("Nominal target bulanan harus diisi dengan angka.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If TextBoxHariKerja.Text.Trim() = "" OrElse Not IsNumeric(TextBoxHariKerja.Text) Then
            MsgBox("Jumlah hari kerja harus diisi dengan angka.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim jenisPoli As String = ComboBoxPoliTarget.Text.Trim()
        Dim targetNominal As Decimal = CDec(TextBoxTargetBulanan.Text.Trim())
        Dim hariKerja As Integer = CInt(TextBoxHariKerja.Text.Trim())

        Dim bulanSekarang As Integer = Date.Today.Month
        Dim tahunSekarang As Integer = Date.Today.Year

        Dim sqlCek As String = "SELECT COUNT(*) FROM MasterTargetPoli " & _
                               "WHERE JenisPoli = '" & jenisPoli & "' " & _
                               "AND Bulan = " & bulanSekarang & " " & _
                               "AND Tahun = " & tahunSekarang

        Dim dtCek As DataTable = ReadSQL(sqlCek)
        Dim sudahAda As Integer = CInt(dtCek.Rows(0)(0))

        If sudahAda > 0 Then
            MsgBox("Target untuk " & jenisPoli & " pada bulan ini sudah pernah diset!" & vbCrLf & _
                   "Tidak dapat menyimpan data ganda dalam 1 bulan.", MsgBoxStyle.Critical, "Akses Ditolak")
            Exit Sub
        End If

        Dim sqlSimpan As String = "INSERT INTO MasterTargetPoli (JenisPoli, TargetBulanan, HariKerja, Bulan, Tahun, CreatedAt) " & _
                         "VALUES ('" & jenisPoli & "', " & targetNominal & ", " & hariKerja & ", " & _
                         bulanSekarang & ", " & tahunSekarang & ", GETDATE())"

        Try
            WriteSQL(sqlSimpan, 4)

            MsgBox("Target bulanan untuk " & jenisPoli & " berhasil disimpan!", MsgBoxStyle.Information, "Sukses")

            TextBoxTargetBulanan.Clear()
            TextBoxHariKerja.Clear()
            ComboBoxPoliTarget.SelectedIndex = -1
        Catch ex As Exception
            MsgBox("Gagal menyimpan data target: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ButtonCetakCostumer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCetakCostumer.Click
        Dim jenisPoli As String = ComboBoxPoli.Text
        Dim tglAwal As DateTime = dtAwal.Value
        Dim tglAkhir As DateTime = dtAkhir.Value

        Dim strTglAwal As String = tglAwal.ToString("yyyy-MM-dd")
        Dim strTglAkhir As String = tglAkhir.ToString("yyyy-MM-dd")

        ' Query CTE Dasar untuk mengelompokkan Status Customer
        Dim cteBase As String = _
            "WITH CustomerLifespan AS ( " & _
            "    SELECT IDInternet, JenisPoli, MIN(CreatedDate) AS FirstTransaction, MAX(CreatedDate) AS LastTransaction " & _
            "    FROM TransaksiDetail " & _
            "    WHERE DeletedDate IS NULL AND JenisPoli = '" & jenisPoli & "' " & _
            "    GROUP BY IDInternet, JenisPoli " & _
            "), " & _
            "CustomerStatus AS ( " & _
            "    SELECT IDInternet, JenisPoli, FirstTransaction, LastTransaction, " & _
            "        CASE " & _
            "            WHEN FirstTransaction BETWEEN '" & strTglAwal & "' AND '" & strTglAkhir & "' THEN 'New' " & _
            "            WHEN LastTransaction < DATEADD(month, -6, '" & strTglAkhir & "') THEN 'Sleeping' " & _
            "            ELSE 'Existing' " & _
            "        END AS StatusCustomer " & _
            "    FROM CustomerLifespan " & _
            ") "

        ' 1. Query Detail Data
        Dim sqlCustomer As String = cteBase & _
            "SELECT CS.IDInternet, MP.Nama, MP.NoHandphone, CS.JenisPoli, CS.StatusCustomer, " & _
            "       CS.FirstTransaction AS [Kunjungan Pertama], CS.LastTransaction AS [Kunjungan Terakhir] " & _
            "FROM CustomerStatus CS " & _
            "INNER JOIN MasterPasien MP ON CS.IDInternet = MP.NoRekamMedis " & _
            "ORDER BY CS.StatusCustomer, MP.Nama"

        ' 2. Query Summary untuk Header (Menghitung langsung dari CustomerStatus)
        Dim sqlSummary As String = cteBase & _
            "SELECT " & _
            "    SUM(CASE WHEN StatusCustomer = 'New' THEN 1 ELSE 0 END) AS TotalNew, " & _
            "    SUM(CASE WHEN StatusCustomer = 'Existing' THEN 1 ELSE 0 END) AS TotalExisting, " & _
            "    SUM(CASE WHEN StatusCustomer = 'Sleeping' THEN 1 ELSE 0 END) AS TotalSleeping " & _
            "FROM CustomerStatus"

        Dim dtCustomer As DataTable = ReadSQL(sqlCustomer)
        Dim dtSummary As DataTable = ReadSQL(sqlSummary)

        If dtCustomer IsNot Nothing AndAlso dtCustomer.Rows.Count > 0 Then
            Try
                Dim report As New DevExpress.XtraReports.UI.XtraReport
                report = DevExpress.XtraReports.UI.XtraReport.FromFile(Application.StartupPath & "\reports\LaporanCostumer.repx", True)

                ' Set Header Text
                Dim bandHeader As ReportHeaderBand = CType(report.Bands(BandKind.ReportHeader), ReportHeaderBand)
                If bandHeader IsNot Nothing Then
                    If bandHeader.Controls("lblPoli") IsNot Nothing Then bandHeader.Controls("lblPoli").Text = jenisPoli
                    If bandHeader.Controls("lblPeriode") IsNot Nothing Then
                        bandHeader.Controls("lblPeriode").Text = tglAwal.ToString("dd MMM yyyy") & " - " & tglAkhir.ToString("dd MMM yyyy")
                    End If

                    ' Bind Total ke Label Header
                    If dtSummary IsNot Nothing AndAlso dtSummary.Rows.Count > 0 Then
                        Dim row As DataRow = dtSummary.Rows(0)
                        If bandHeader.Controls("lblTotalNew") IsNot Nothing Then bandHeader.Controls("lblTotalNew").Text = Convert.ToInt32(row("TotalNew")).ToString("N0") & " Orang"
                        If bandHeader.Controls("lblTotalExisting") IsNot Nothing Then bandHeader.Controls("lblTotalExisting").Text = Convert.ToInt32(row("TotalExisting")).ToString("N0") & " Orang"
                        If bandHeader.Controls("lblTotalSleeping") IsNot Nothing Then bandHeader.Controls("lblTotalSleeping").Text = Convert.ToInt32(row("TotalSleeping")).ToString("N0") & " Orang"
                    End If
                End If

                report.DataSource = dtCustomer
                'report.RunDesigner()
                report.ShowPreview()

            Catch ex As Exception
                MessageBox.Show("Gagal memproses laporan: " & ex.Message, "Error DevExpress", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Data tidak ditemukan untuk periode dan poli ini.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub ButtonKirimLaporan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKirimLaporan.Click
        Try
            ' 1. Konfirmasi User
            Dim confirmResult As DialogResult = MessageBox.Show( _
                "Apakah Anda yakin ingin mengirim data Selling Revenue dan Detail Pendapatan periode ini ke Server Web?", _
                "Konfirmasi Pengiriman", _
                MessageBoxButtons.YesNo, _
                MessageBoxIcon.Question, _
                MessageBoxDefaultButton.Button2)

            If confirmResult = DialogResult.No Then Exit Sub

            ' 2. Persiapan Parameter Tanggal
            Dim tglAwal As Date = dtAwal.Value.Date
            Dim tglAkhir As Date = dtAkhir.Value.Date

            Dim strAwal As String = tglAwal.ToString("yyyy-MM-dd 00:00:00")
            Dim strAkhir As String = tglAkhir.ToString("yyyy-MM-dd 23:59:59")

            ' Mapping Poli untuk Analisa Selling & Revenue
            Dim listPoli As New Dictionary(Of String, String)()
            listPoli.Add("POLI KECANTIKAN", "LMIE")
            listPoli.Add("POLI HBOT", "HBOT")
            listPoli.Add("POLI FISIOTERAPI", "FISIOTERAPI")
            listPoli.Add("POLI REHABILITASI MEDIK", "RHBMDK")
            listPoli.Add("POLI EYELASH & NAIL", "EYELASHNAIL")


            ' 3. Inisialisasi UI Progress Bar
            Dim totalHari As Integer = (tglAkhir - tglAwal).Days + 1
            Dim totalStepAnalisa As Integer = listPoli.Count * totalHari

            ProgressBar1.Minimum = 0
            ProgressBar1.Maximum = totalStepAnalisa + 10
            ProgressBar1.Value = 0
            ProgressBar1.Visible = True

            ' =========================================================================
            ' PROSES 1: PROSES & KIRIM ANALISA SELLING REVENUE
            ' =========================================================================
            lblStatusProgress.Text = "Menyiapkan data analisa Selling & Revenue..."
            lblStatusProgress.Visible = True
            Application.DoEvents()

            Dim sbJsonAnalisa As New StringBuilder()
            sbJsonAnalisa.Append("{")
            sbJsonAnalisa.Append("""user"": """ & userInput & """,")
            sbJsonAnalisa.Append("""laporan"": [")

            Dim isFirstItemAnalisa As Boolean = True
            Dim currentProgressVal As Integer = 0

            For Each kvp As KeyValuePair(Of String, String) In listPoli
                Dim namaPoliDB As String = kvp.Key
                Dim kodePoliAPI As String = kvp.Value

                lblStatusProgress.Text = "Mengambil data analisa " & namaPoliDB & "..."
                Application.DoEvents()

                ' Query Selling & Revenue Terbaru per Poli
                Dim sqlQueryAnalisa As String = String.Format( _
                    "SELECT " & _
                    "    ISNULL(S.Tgl, R.Tgl) AS Tgl, " & _
                    "    ISNULL(S.TotalSelling, 0) AS SELLING, " & _
                    "    ISNULL(R.TotalRevenue, 0) AS REVENUE " & _
                    "FROM ( " & _
                    "    SELECT Tgl, SUM(TotalHarga) AS TotalSelling " & _
                    "    FROM ( " & _
                    "        SELECT CAST(TPT.CreatedDate AS DATE) AS Tgl, TPT.HargaNett AS TotalHarga " & _
                    "        FROM TransaksiPaketTindakan TPT " & _
                    "        INNER JOIN TransaksiDetail TD ON CAST(TPT.NoTransaksi AS VARCHAR(100)) = CAST(TD.NoUrut AS VARCHAR(100)) " & _
                    "        WHERE TPT.DeletedDate IS NULL AND TD.JenisPoli = '{0}' AND TD.DeletedBy IS NULL AND TPT.CreatedDate BETWEEN '{1}' AND '{2}' " & _
                    "        UNION ALL " & _
                    "        SELECT CAST(TPD.CreatedDate AS DATE) AS Tgl, (TPD.NettBayar + ISNULL(TP.Pembulatan, 0)) AS TotalHarga " & _
                    "        FROM TransaksiPoliDetail TPD " & _
                    "        INNER JOIN TransaksiPoli TP ON TPD.TransPoliID = TP.TransPoliID " & _
                    "        INNER JOIN TransaksiDetail TD ON CAST(TP.NoUrut AS VARCHAR(100)) = CAST(TD.NoUrut AS VARCHAR(100)) " & _
                    "        WHERE TPD.DeletedDate IS NULL AND TD.JenisPoli = '{0}' AND TPD.StatusPembayaran = 'PAID' AND TD.DeletedBy IS NULL AND TPD.CreatedDate BETWEEN '{1}' AND '{2}' " & _
                    "    ) CombinedSelling " & _
                    "    GROUP BY Tgl " & _
                    ") S " & _
                    "FULL OUTER JOIN ( " & _
                    "    SELECT Tgl, SUM(NettBayar) AS TotalRevenue " & _
                    "    FROM ( " & _
                    "        SELECT CAST(TD.TanggalTransaksi AS DATE) AS Tgl, (TPD.NettBayar + ISNULL(TP.Pembulatan, 0)) AS NettBayar " & _
                    "        FROM TransaksiPoliDetail TPD " & _
                    "        INNER JOIN TransaksiPoli TP ON CAST(TPD.TransPoliID AS VARCHAR(100)) = CAST(TP.TransPoliID AS VARCHAR(100)) " & _
                    "        INNER JOIN TransaksiDetail TD ON CAST(TP.NoUrut AS VARCHAR(100)) = CAST(TD.NoUrut AS VARCHAR(100)) " & _
                    "        WHERE TPD.DeletedBy IS NULL AND TD.DeletedBy IS NULL AND TPD.StatusPembayaran = 'PAID' AND TD.JenisPoli = '{0}' " & _
                    "          AND TPD.JenisTindakan = 'Tindakan' AND TD.TanggalTransaksi BETWEEN '{1}' AND '{2}' " & _
                    "          AND ISNULL(TPD.PaketID, '0') <> '0' AND TPD.PaketID <> '' " & _
                    "          AND EXISTS ( " & _
                    "              SELECT 1 FROM TransaksiPaketTindakan TPT " & _
                    "              WHERE CAST(TPT.PaketID AS VARCHAR(100)) = CAST(TPD.PaketID AS VARCHAR(100)) " & _
                    "                AND (ISNULL(TPD.KodeVoucher, '') = '' OR TPT.KodeVoucher = TPD.KodeVoucher) " & _
                    "                AND TPT.DeletedDate IS NULL AND TPT.CreatedDate >= '2026-08-01 00:00:00' " & _
                    "          ) " & _
                    "          AND EXISTS ( " & _
                    "              SELECT 1 FROM ( " & _
                    "                  SELECT CAST(TransPoliID AS VARCHAR(100)) AS TransPoliID FROM TransaksiPoliBayar WHERE ISNULL(JumlahBayar, 0) > 0 " & _
                    "                  UNION ALL " & _
                    "                  SELECT CAST(TransPoliID AS VARCHAR(100)) AS TransPoliID FROM TransaksiPoliBayarAwal WHERE ISNULL(JumlahBayar, 0) > 0 " & _
                    "              ) PB WHERE PB.TransPoliID = CAST(TPD.TransPoliID AS VARCHAR(100)) " & _
                    "          ) " & _
                    "        UNION ALL " & _
                    "        SELECT CAST(TD.TanggalTransaksi AS DATE) AS Tgl, (TPD.NettBayar + ISNULL(TP.Pembulatan, 0)) AS NettBayar " & _
                    "        FROM TransaksiDetail TD " & _
                    "        INNER JOIN TransaksiPoliDetail TPD ON CAST(TD.NoTransaksi AS VARCHAR(100)) = CAST(TPD.TransPoliID AS VARCHAR(100)) " & _
                    "        INNER JOIN TransaksiPoli TP ON CAST(TPD.TransPoliID AS VARCHAR(100)) = CAST(TP.TransPoliID AS VARCHAR(100)) " & _
                    "        WHERE TPD.DeletedBy IS NULL AND TD.DeletedBy IS NULL AND TPD.StatusPembayaran = 'PAID' AND TD.JenisPoli = '{0}' " & _
                    "          AND TPD.JenisTindakan = 'Tindakan' AND TD.TanggalTransaksi BETWEEN '{1}' AND '{2}' " & _
                    "          AND (ISNULL(TPD.PaketID, '') = '' OR TPD.PaketID = '0') " & _
                    "          AND EXISTS ( " & _
                    "              SELECT 1 FROM ( " & _
                    "                  SELECT CAST(TransPoliID AS VARCHAR(100)) AS TransPoliID FROM TransaksiPoliBayar WHERE ISNULL(JumlahBayar, 0) > 0 " & _
                    "                  UNION ALL " & _
                    "                  SELECT CAST(TransPoliID AS VARCHAR(100)) AS TransPoliID FROM TransaksiPoliBayarAwal WHERE ISNULL(JumlahBayar, 0) > 0 " & _
                    "              ) PB WHERE PB.TransPoliID = CAST(TPD.TransPoliID AS VARCHAR(100)) " & _
                    "          ) " & _
                    "    ) CombinedRevenue " & _
                    "    GROUP BY Tgl " & _
                    ") R ON S.Tgl = R.Tgl " & _
                    "ORDER BY Tgl ASC", namaPoliDB, strAwal, strAkhir)

                Dim dtPoli As DataTable = ReadSQL(sqlQueryAnalisa)

                Dim currentTgl As Date = tglAwal
                While currentTgl <= tglAkhir
                    Dim strTglCurrent As String = currentTgl.ToString("yyyy-MM-dd")

                    Dim sellingVal As Decimal = 0
                    Dim revenueVal As Decimal = 0

                    If dtPoli IsNot Nothing AndAlso dtPoli.Rows.Count > 0 Then
                        Dim rowsMatch() As DataRow = dtPoli.Select("Tgl = #" & strTglCurrent & "#")
                        If rowsMatch.Length > 0 Then
                            sellingVal = If(IsDBNull(rowsMatch(0)("SELLING")), 0, CDec(rowsMatch(0)("SELLING")))
                            revenueVal = If(IsDBNull(rowsMatch(0)("REVENUE")), 0, CDec(rowsMatch(0)("REVENUE")))
                        End If
                    End If

                    If Not isFirstItemAnalisa Then sbJsonAnalisa.Append(",")

                    sbJsonAnalisa.Append("{")
                    sbJsonAnalisa.Append("""tanggal"": """ & strTglCurrent & """,")
                    sbJsonAnalisa.Append("""kode_poli"": """ & kodePoliAPI & """,")
                    sbJsonAnalisa.Append("""selling"": " & sellingVal.ToString(System.Globalization.CultureInfo.InvariantCulture) & ",")
                    sbJsonAnalisa.Append("""revenue"": " & revenueVal.ToString(System.Globalization.CultureInfo.InvariantCulture) & "")
                    sbJsonAnalisa.Append("}")

                    isFirstItemAnalisa = False
                    currentTgl = currentTgl.AddDays(1)

                    currentProgressVal += 1
                    If currentProgressVal <= ProgressBar1.Maximum Then ProgressBar1.Value = currentProgressVal
                    Application.DoEvents()
                End While
            Next

            sbJsonAnalisa.Append("]")
            sbJsonAnalisa.Append("}")

            ' =========================================================================
            ' PROSES 2: QUERY DETAIL LAPORAN PENDAPATAN (UNTUK sp_InsertPendapatan)
            ' =========================================================================
            lblStatusProgress.Text = "Mengambil data detail transaksi pendapatan..."
            Application.DoEvents()

            ' Menambahkan TPD.TransPoliIDDetail ke SELECT Query
            Dim sqlQueryPendapatan As String = String.Format( _
                "SELECT " & _
                "    TD.JenisPoli AS [NamaPoli], " & _
                "    TD.NoUrut AS [NoTransaksi], " & _
                "    TPD.TransPoliIDDetail AS [TransPoliIDDetail], " & _
                "    TD.TanggalTransaksi AS [TanggalKunjungan], " & _
                "    MP.Nama AS [NamaPasien], " & _
                "    TPD.NamaTindakan AS [NamaTindakan], " & _
                "    ISNULL(STUFF(( " & _
                "        SELECT DISTINCT ' + ' + PB.JenisBayar + " & _
                "               CASE WHEN ISNULL(PB.mesin,'') <> '' OR ISNULL(PB.kartu,'') <> '' " & _
                "                    THEN ' (' + NULLIF(PB.mesin,'') + CASE WHEN ISNULL(PB.mesin,'') <> '' AND ISNULL(PB.kartu,'') <> '' THEN ' - ' ELSE '' END + NULLIF(PB.kartu,'') + ')' " & _
                "                    ELSE '' END " & _
                "        FROM ( " & _
                "            SELECT TransPoliID, JenisBayar, mesin, kartu FROM TransaksiPoliBayar WHERE ISNULL(JumlahBayar, 0) > 0 AND ISNULL(TPD.PaketID, '0') = '0' " & _
                "            UNION ALL " & _
                "            SELECT TransPoliID, CASE WHEN UPPER(JenisBayar) = 'VOUCHER' THEN 'PENGGUNAAN PAKET' ELSE JenisBayar END AS JenisBayar, mesin, kartu FROM TransaksiPoliBayarAwal WHERE ISNULL(JumlahBayar, 0) > 0 AND ISNULL(TPD.PaketID, '0') <> '0' AND TPD.JenisTindakan = 'Tindakan' " & _
                "        ) PB " & _
                "        WHERE PB.TransPoliID = TPD.TransPoliID " & _
                "        FOR XML PATH('') " & _
                "    ), 1, 3, ''), '-') AS [JenisBayar], " & _
                "    TPD.BiayaTindakan AS [HargaRil], " & _
                "    TPD.DiscTindakan AS [Diskon], " & _
                "    CASE WHEN ISNULL(TPD.BiayaTindakan, 0) > 0 THEN (TPD.DiscTindakan / TPD.BiayaTindakan) * 100 ELSE 0 END AS [PctDiskon], " & _
                "    TPD.NettBayar AS [Pembayaran], " & _
                "    (TPD.NettBayar * ISNULL((SELECT TOP 1 CASE " & _
                "        WHEN UPPER(PB.kartu) LIKE '%QRIS%' OR UPPER(PB.JenisBayar) LIKE '%QRIS%' THEN 0.7 " & _
                "        WHEN UPPER(PB.mesin) LIKE '%MANDIRI%' THEN " & _
                "            CASE WHEN UPPER(PB.JenisBayar) LIKE '%DEBET%' OR UPPER(PB.JenisBayar) LIKE '%DEBIT%' THEN CASE WHEN UPPER(PB.kartu) LIKE '%MANDIRI%' THEN 0.15 ELSE 1.0 END " & _
                "                 WHEN UPPER(PB.JenisBayar) LIKE '%CARD%' OR UPPER(PB.JenisBayar) LIKE '%CREDIT%' OR UPPER(PB.JenisBayar) LIKE '%KREDIT%' THEN 1.5 ELSE 0 END " & _
                "        WHEN UPPER(PB.mesin) LIKE '%BCA%' THEN " & _
                "            CASE WHEN UPPER(PB.JenisBayar) LIKE '%DEBET%' OR UPPER(PB.JenisBayar) LIKE '%DEBIT%' THEN CASE WHEN UPPER(PB.kartu) LIKE '%BCA%' THEN 0.15 ELSE 1.0 END " & _
                "                 WHEN UPPER(PB.JenisBayar) LIKE '%CARD%' OR UPPER(PB.JenisBayar) LIKE '%CREDIT%' OR UPPER(PB.JenisBayar) LIKE '%KREDIT%' THEN CASE WHEN UPPER(PB.kartu) LIKE '%BCA%' THEN 1.5 ELSE 2.0 END ELSE 0 END " & _
                "        ELSE CASE WHEN UPPER(PB.JenisBayar) LIKE '%DEBET%' OR UPPER(PB.JenisBayar) LIKE '%DEBIT%' THEN 1.0 " & _
                "                  WHEN UPPER(PB.JenisBayar) LIKE '%CARD%' OR UPPER(PB.JenisBayar) LIKE '%CREDIT%' OR UPPER(PB.JenisBayar) LIKE '%KREDIT%' THEN 2.0 ELSE 0 END " & _
                "    END FROM TransaksiPoliBayar PB WHERE PB.TransPoliID = TPD.TransPoliID AND ISNULL(PB.JumlahBayar, 0) > 0 AND ISNULL(TPD.PaketID, '0') = '0'), 0)) / 100.0 AS [FeeAdminBank], " & _
                "    MA.NamaAlat, " & _
                "    ISNULL(LPA.ShootDipakai * LPA.HargaBeli, 0) AS [BHPAlat], " & _
                "    CASE WHEN ISNULL(TPD.NettBayar, 0) > 0 THEN (ISNULL(LPA.ShootDipakai * LPA.HargaBeli, 0) / TPD.NettBayar) * 100 ELSE 0 END AS [PctBHPAlat], " & _
                "    TPD.TotalBayarBHP AS [BHPApotek], " & _
                "    CASE WHEN ISNULL(TPD.NettBayar, 0) > 0 THEN (ISNULL(TPD.TotalBayarBHP, 0) / TPD.NettBayar) * 100 ELSE 0 END AS [PctBHPApotek], " & _
                "    CASE WHEN ISNULL(TPD.ClosedFeeDokter, 0) = 1 THEN ISNULL(TPD.FeeDokter, 0) ELSE 0 END AS [FeeDokter], " & _
                "    CASE WHEN ISNULL(TPD.NettBayar, 0) > 0 THEN (CASE WHEN ISNULL(TPD.ClosedFeeDokter, 0) = 1 THEN ISNULL(TPD.FeeDokter, 0) ELSE 0 END / TPD.NettBayar) * 100 ELSE 0 END AS [PctFeeDokter], " & _
                "    CASE WHEN ISNULL(TPD.ClosedFeePerawat, 0) = 1 THEN ISNULL(TPD.FeePerawat, 0) ELSE 0 END AS [FeePerawat], " & _
                "    CASE WHEN ISNULL(TPD.NettBayar, 0) > 0 THEN (CASE WHEN ISNULL(TPD.ClosedFeePerawat, 0) = 1 THEN ISNULL(TPD.FeePerawat, 0) ELSE 0 END / TPD.NettBayar) * 100 ELSE 0 END AS [PctFeePerawat], " & _
                "    0 AS [BiayaOverhead], " & _
                "    0 AS [PctOverhead], " & _
                "    (TPD.NettBayar - ( " & _
                "        (TPD.NettBayar * ISNULL((SELECT TOP 1 CASE WHEN UPPER(PB.kartu) LIKE '%QRIS%' OR UPPER(PB.JenisBayar) LIKE '%QRIS%' THEN 0.7 WHEN UPPER(PB.mesin) LIKE '%MANDIRI%' THEN CASE WHEN UPPER(PB.JenisBayar) LIKE '%DEBET%' OR UPPER(PB.JenisBayar) LIKE '%DEBIT%' THEN CASE WHEN UPPER(PB.kartu) LIKE '%MANDIRI%' THEN 0.15 ELSE 1.0 END WHEN UPPER(PB.JenisBayar) LIKE '%CARD%' OR UPPER(PB.JenisBayar) LIKE '%CREDIT%' OR UPPER(PB.JenisBayar) LIKE '%KREDIT%' THEN 1.5 ELSE 0 END WHEN UPPER(PB.mesin) LIKE '%BCA%' THEN CASE WHEN UPPER(PB.JenisBayar) LIKE '%DEBET%' OR UPPER(PB.JenisBayar) LIKE '%DEBIT%' THEN CASE WHEN UPPER(PB.kartu) LIKE '%BCA%' THEN 0.15 ELSE 1.0 END WHEN UPPER(PB.JenisBayar) LIKE '%CARD%' OR UPPER(PB.JenisBayar) LIKE '%CREDIT%' OR UPPER(PB.JenisBayar) LIKE '%KREDIT%' THEN CASE WHEN UPPER(PB.kartu) LIKE '%BCA%' THEN 1.5 ELSE 2.0 END ELSE 0 END ELSE CASE WHEN UPPER(PB.JenisBayar) LIKE '%DEBET%' OR UPPER(PB.JenisBayar) LIKE '%DEBIT%' THEN 1.0 WHEN UPPER(PB.JenisBayar) LIKE '%CARD%' OR UPPER(PB.JenisBayar) LIKE '%CREDIT%' OR UPPER(PB.JenisBayar) LIKE '%KREDIT%' THEN 2.0 ELSE 0 END END FROM TransaksiPoliBayar PB WHERE PB.TransPoliID = TPD.TransPoliID AND ISNULL(PB.JumlahBayar, 0) > 0 AND ISNULL(TPD.PaketID, '0') = '0'), 0)) / 100.0 " & _
                "        + ISNULL(LPA.ShootDipakai * LPA.HargaBeli, 0) " & _
                "        + ISNULL(TPD.TotalBayarBHP, 0) " & _
                "        + CASE WHEN ISNULL(TPD.ClosedFeeDokter, 0) = 1 THEN ISNULL(TPD.FeeDokter, 0) ELSE 0 END " & _
                "        + CASE WHEN ISNULL(TPD.ClosedFeePerawat, 0) = 1 THEN ISNULL(TPD.FeePerawat, 0) ELSE 0 END " & _
                "    )) AS [PendapatanBersih], " & _
                "    CASE WHEN ISNULL(TPD.NettBayar, 0) > 0 THEN ((TPD.NettBayar - ((TPD.NettBayar * ISNULL((SELECT TOP 1 CASE WHEN UPPER(PB.kartu) LIKE '%QRIS%' OR UPPER(PB.JenisBayar) LIKE '%QRIS%' THEN 0.7 WHEN UPPER(PB.mesin) LIKE '%MANDIRI%' THEN CASE WHEN UPPER(PB.JenisBayar) LIKE '%DEBET%' OR UPPER(PB.JenisBayar) LIKE '%DEBIT%' THEN CASE WHEN UPPER(PB.kartu) LIKE '%MANDIRI%' THEN 0.15 ELSE 1.0 END WHEN UPPER(PB.JenisBayar) LIKE '%CARD%' OR UPPER(PB.JenisBayar) LIKE '%CREDIT%' OR UPPER(PB.JenisBayar) LIKE '%KREDIT%' THEN 1.5 ELSE 0 END WHEN UPPER(PB.mesin) LIKE '%BCA%' THEN CASE WHEN UPPER(PB.JenisBayar) LIKE '%DEBET%' OR UPPER(PB.JenisBayar) LIKE '%DEBIT%' THEN CASE WHEN UPPER(PB.kartu) LIKE '%BCA%' THEN 0.15 ELSE 1.0 END WHEN UPPER(PB.JenisBayar) LIKE '%CARD%' OR UPPER(PB.JenisBayar) LIKE '%CREDIT%' OR UPPER(PB.JenisBayar) LIKE '%KREDIT%' THEN CASE WHEN UPPER(PB.kartu) LIKE '%BCA%' THEN 1.5 ELSE 2.0 END ELSE 0 END ELSE CASE WHEN UPPER(PB.JenisBayar) LIKE '%DEBET%' OR UPPER(PB.JenisBayar) LIKE '%DEBIT%' THEN 1.0 WHEN UPPER(PB.JenisBayar) LIKE '%CARD%' OR UPPER(PB.JenisBayar) LIKE '%CREDIT%' OR UPPER(PB.JenisBayar) LIKE '%KREDIT%' THEN 2.0 ELSE 0 END END FROM TransaksiPoliBayar PB WHERE PB.TransPoliID = TPD.TransPoliID AND ISNULL(PB.JumlahBayar, 0) > 0 AND ISNULL(TPD.PaketID, '0') = '0'), 0)) / 100.0 + ISNULL(LPA.ShootDipakai * LPA.HargaBeli, 0) + ISNULL(TPD.TotalBayarBHP, 0) + CASE WHEN ISNULL(TPD.ClosedFeeDokter, 0) = 1 THEN ISNULL(TPD.FeeDokter, 0) ELSE 0 END + CASE WHEN ISNULL(TPD.ClosedFeePerawat, 0) = 1 THEN ISNULL(TPD.FeePerawat, 0) ELSE 0 END)) / TPD.NettBayar) * 100 ELSE 0 END AS [PctPendapatanBersih] " & _
                "FROM TransaksiDetail TD " & _
                "INNER JOIN MasterPasien MP ON TD.IDInternet = MP.NoRekamMedis " & _
                "INNER JOIN TransaksiPoliDetail TPD ON TD.NoTransaksi = TPD.TransPoliID " & _
                "LEFT JOIN LogPemakaianAlat LPA ON TPD.TransPoliIDDetail = LPA.TransaksiDetailID " & _
                "LEFT JOIN MasterAlat MA ON LPA.AlatID = MA.AlatID " & _
                "LEFT JOIN TransaksiCancel TC ON TD.NoUrut = TC.NoUrut " & _
                "WHERE TPD.DeletedBy IS NULL " & _
                "  AND TD.DeletedBy IS NULL " & _
                "  AND TC.NoUrut IS NULL " & _
                "  AND TPD.JenisTindakan = 'Tindakan' " & _
                "  AND TPD.StatusPembayaran = 'PAID' " & _
                "  AND TD.TanggalTransaksi BETWEEN '{0}' AND '{1}' " & _
                "ORDER BY TD.TanggalTransaksi ASC", strAwal, strAkhir)

            Dim dtPendapatan As DataTable = ReadSQL(sqlQueryPendapatan)

            ' Menyusun JSON Batch Detail Pendapatan
            Dim sbJsonPendapatan As New StringBuilder()
            sbJsonPendapatan.Append("{")
            sbJsonPendapatan.Append("""user"": """ & userInput & """,")
            sbJsonPendapatan.Append("""laporan"": [")

            Dim isFirstItemPendapatan As Boolean = True

            If dtPendapatan IsNot Nothing AndAlso dtPendapatan.Rows.Count > 0 Then
                For Each row As DataRow In dtPendapatan.Rows
                    Dim tglKunjungan As Date = CDate(row("TanggalKunjungan"))
                    Dim strTglKunjungan As String = tglKunjungan.ToString("yyyy-MM-dd")

                    Dim namaPoli As String = row("NamaPoli").ToString().Trim()
                    Dim noTrans As String = row("NoTransaksi").ToString().Trim()
                    Dim transPoliIDDetail As String = row("TransPoliIDDetail").ToString().Trim() ' <--- AMBIL NILAI BARU
                    Dim namaPasien As String = row("NamaPasien").ToString().Trim()
                    Dim namaTindakan As String = row("NamaTindakan").ToString().Trim()
                    Dim jenisBayar As String = row("JenisBayar").ToString().Trim()

                    ' === MAPPING KODE CABANG KONSISTEN BERSADARKAN JENIS POLI ===
                    Dim kodeCabang As String = ""
                    Select Case namaPoli.Trim().ToUpper()
                        Case "POLI KECANTIKAN"
                            kodeCabang = "LMIE"
                        Case "POLI HBOT"
                            kodeCabang = "HBOT"
                        Case "POLI FISIOTERAPI"
                            kodeCabang = "FISIOTERAPI"
                        Case "POLI REHABILITASI MEDIK"
                            kodeCabang = "RHBMDK"
                        Case "POLI EYELASH & NAIL"
                            kodeCabang = "EYELASHNAIL"
                        Case Else
                            kodeCabang = namaPoli.Trim()
                    End Select

                    ' Nilai Nominal & Persentase
                    Dim hargaRil As Decimal = If(IsDBNull(row("HargaRil")), 0, CDec(row("HargaRil")))
                    Dim diskonNominal As Decimal = If(IsDBNull(row("Diskon")), 0, CDec(row("Diskon")))
                    Dim diskonPersen As Decimal = If(IsDBNull(row("PctDiskon")), 0, CDec(row("PctDiskon")))
                    Dim pembayaran As Decimal = If(IsDBNull(row("Pembayaran")), 0, CDec(row("Pembayaran")))

                    Dim namaAlat As String = If(IsDBNull(row("NamaAlat")), "", row("NamaAlat").ToString().Trim())

                    Dim bhpAlatNominal As Decimal = If(IsDBNull(row("BHPAlat")), 0, CDec(row("BHPAlat")))
                    Dim bhpAlatPersen As Decimal = If(IsDBNull(row("PctBHPAlat")), 0, CDec(row("PctBHPAlat")))
                    Dim bhPApotekNominal As Decimal = If(IsDBNull(row("BHPApotek")), 0, CDec(row("BHPApotek")))
                    Dim bhPApotekPersen As Decimal = If(IsDBNull(row("PctBHPApotek")), 0, CDec(row("PctBHPApotek")))

                    Dim feeDokterNominal As Decimal = If(IsDBNull(row("FeeDokter")), 0, CDec(row("FeeDokter")))
                    Dim feeDokterPersen As Decimal = If(IsDBNull(row("PctFeeDokter")), 0, CDec(row("PctFeeDokter")))
                    Dim feePerawatNominal As Decimal = If(IsDBNull(row("FeePerawat")), 0, CDec(row("FeePerawat")))
                    Dim feePerawatPersen As Decimal = If(IsDBNull(row("PctFeePerawat")), 0, CDec(row("PctFeePerawat")))

                    Dim feeAdminBank As Decimal = If(IsDBNull(row("FeeAdminBank")), 0, CDec(row("FeeAdminBank")))
                    Dim overheadNominal As Decimal = If(IsDBNull(row("BiayaOverhead")), 0, CDec(row("BiayaOverhead")))
                    Dim overheadPersen As Decimal = If(IsDBNull(row("PctOverhead")), 0, CDec(row("PctOverhead")))

                    Dim pendapatanBersihNominal As Decimal = If(IsDBNull(row("PendapatanBersih")), 0, CDec(row("PendapatanBersih")))
                    Dim pendapatanBersihPersen As Decimal = If(IsDBNull(row("PctPendapatanBersih")), 0, CDec(row("PctPendapatanBersih")))

                    If Not isFirstItemPendapatan Then sbJsonPendapatan.Append(",")

                    sbJsonPendapatan.Append("{")
                    sbJsonPendapatan.Append("""cabang"": """ & kodeCabang & """,")
                    sbJsonPendapatan.Append("""no_trans"": """ & noTrans & """,")
                    ' === PARAMETER BARU DISISIPKAN TEPAT SETELAH no_trans ===
                    sbJsonPendapatan.Append("""transpoli_id_detail"": """ & transPoliIDDetail & """,")
                    sbJsonPendapatan.Append("""tgl_kunjungan"": """ & strTglKunjungan & """,")
                    sbJsonPendapatan.Append("""nama_poli"": """ & namaPoli & """,")
                    sbJsonPendapatan.Append("""nama_pasien"": """ & namaPasien.Replace("""", "\""") & """,")
                    sbJsonPendapatan.Append("""tindakan"": """ & namaTindakan.Replace("""", "\""") & """,")
                    sbJsonPendapatan.Append("""metode_bayar"": """ & jenisBayar.Replace("""", "\""") & """,")
                    sbJsonPendapatan.Append("""harga_ril"": " & hargaRil.ToString(System.Globalization.CultureInfo.InvariantCulture) & ",")
                    sbJsonPendapatan.Append("""diskon_nominal"": " & diskonNominal.ToString(System.Globalization.CultureInfo.InvariantCulture) & ",")
                    sbJsonPendapatan.Append("""diskon_persentase"": " & diskonPersen.ToString(System.Globalization.CultureInfo.InvariantCulture) & ",")
                    sbJsonPendapatan.Append("""pembayaran"": " & pembayaran.ToString(System.Globalization.CultureInfo.InvariantCulture) & ",")

                    If String.IsNullOrEmpty(namaAlat) Then
                        sbJsonPendapatan.Append("""alat"": null,")
                    Else
                        sbJsonPendapatan.Append("""alat"": """ & namaAlat.Replace("""", "\""") & """,")
                    End If

                    sbJsonPendapatan.Append("""bhp_alat_nominal"": " & bhpAlatNominal.ToString(System.Globalization.CultureInfo.InvariantCulture) & ",")
                    sbJsonPendapatan.Append("""bhp_alat_persentase"": " & bhpAlatPersen.ToString(System.Globalization.CultureInfo.InvariantCulture) & ",")
                    sbJsonPendapatan.Append("""bhp_apotek_nominal"": " & bhPApotekNominal.ToString(System.Globalization.CultureInfo.InvariantCulture) & ",")
                    sbJsonPendapatan.Append("""bhp_apotek_persentase"": " & bhPApotekPersen.ToString(System.Globalization.CultureInfo.InvariantCulture) & ",")
                    sbJsonPendapatan.Append("""fee_dokter_nominal"": " & feeDokterNominal.ToString(System.Globalization.CultureInfo.InvariantCulture) & ",")
                    sbJsonPendapatan.Append("""fee_dokter_persentase"": " & feeDokterPersen.ToString(System.Globalization.CultureInfo.InvariantCulture) & ",")
                    sbJsonPendapatan.Append("""fee_perawat_nominal"": " & feePerawatNominal.ToString(System.Globalization.CultureInfo.InvariantCulture) & ",")
                    sbJsonPendapatan.Append("""fee_perawat_persentase"": " & feePerawatPersen.ToString(System.Globalization.CultureInfo.InvariantCulture) & ",")
                    sbJsonPendapatan.Append("""fee_admin"": " & feeAdminBank.ToString(System.Globalization.CultureInfo.InvariantCulture) & ",")
                    sbJsonPendapatan.Append("""biaya_overhead_nominal"": " & overheadNominal.ToString(System.Globalization.CultureInfo.InvariantCulture) & ",")
                    sbJsonPendapatan.Append("""biaya_overhead_persentase"": " & overheadPersen.ToString(System.Globalization.CultureInfo.InvariantCulture) & ",")
                    sbJsonPendapatan.Append("""pendapatan_bersih_nominal"": " & pendapatanBersihNominal.ToString(System.Globalization.CultureInfo.InvariantCulture) & ",")
                    sbJsonPendapatan.Append("""pendapatan_bersih_persentase"": " & pendapatanBersihPersen.ToString(System.Globalization.CultureInfo.InvariantCulture) & "")
                    sbJsonPendapatan.Append("}")

                    isFirstItemPendapatan = False
                Next
            End If

            sbJsonPendapatan.Append("]")
            sbJsonPendapatan.Append("}")

            ' =========================================================================
            ' PROSES 3: EKSEKUSI PENGIRIMAN KE DUA ENDPOINT API LARAVEL
            ' =========================================================================
            Dim baseURL As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'ws_RM'")
            If Not baseURL.EndsWith("/") Then baseURL &= "/"

            ' 1. Kirim API Analisa Selling Revenue
            lblStatusProgress.Text = "Mengirim data Analisa Selling Revenue..."
            Application.DoEvents()

            Dim apiUrlAnalisa As String = baseURL & "api/analisa/insert-batch"
            Dim responseApiAnalisa As String = InsertDataAPIRMOld(sbJsonAnalisa.ToString(), apiUrlAnalisa)

            ' 2. Kirim API Detail Pendapatan
            lblStatusProgress.Text = "Mengirim data Detail Pendapatan Transaksi..."
            Application.DoEvents()

            Dim apiUrlPendapatan As String = baseURL & "api/pendapatan/insert-batch"
            Dim responseApiPendapatan As String = InsertDataAPIRMOld(sbJsonPendapatan.ToString(), apiUrlPendapatan)

            ProgressBar1.Value = ProgressBar1.Maximum
            lblStatusProgress.Text = "Selesai!"
            Application.DoEvents()

            ' =========================================================================
            ' PROSES 4: EVALUASI HASIL RESPONSE SERVER
            ' =========================================================================
            Dim isSuccessAnalisa As Boolean = responseApiAnalisa.ToLower().Contains("""success"":true") OrElse responseApiAnalisa.ToLower().Contains("""status"":""success""")
            Dim isSuccessPendapatan As Boolean = responseApiPendapatan.ToLower().Contains("""success"":true") OrElse responseApiPendapatan.ToLower().Contains("""status"":""success""")

            If isSuccessAnalisa AndAlso isSuccessPendapatan Then
                MessageBox.Show("Berhasil mengirim data Analisa Selling Revenue & " & dtPendapatan.Rows.Count & " data Detail Pendapatan ke Server!", "Sukses Batch API", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Dim msgError As String = "Pengiriman selesai dengan catatan:" & vbCrLf
                msgError &= "1. Status Analisa API: " & If(isSuccessAnalisa, "SUKSES", "GAGAL -> " & responseApiAnalisa) & vbCrLf
                msgError &= "2. Status Pendapatan API: " & If(isSuccessPendapatan, "SUKSES", "GAGAL -> " & responseApiPendapatan)

                MessageBox.Show(msgError, "Hasil Pengiriman API", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat pengiriman otomatis ke server: " & ex.Message, "Error Application", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Reset & Sembunyikan UI Progress
            ProgressBar1.Visible = False
            lblStatusProgress.Visible = False
            lblStatusProgress.Text = ""
        End Try
    End Sub
End Class