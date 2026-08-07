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
        Dim strAkhir As String = tglAkhir.ToString("yyyy-MM-dd 23:59:59")

        ' 2. QUERY SELLING & REVENUE
        Dim sqlSellingRevenue As String = String.Format( _
            "SELECT " & _
            "    ISNULL(S.Tgl, R.Tgl) AS Tgl, " & _
            "    ISNULL(S.TotalSelling, 0) AS SELLING, " & _
            "    ISNULL(R.TotalRevenue, 0) AS REVENUE " & _
            "FROM ( " & _
            "    SELECT " & _
            "        Tgl, " & _
            "        SUM(TotalHarga) AS TotalSelling " & _
            "    FROM ( " & _
            "        SELECT " & _
            "            CAST(TPT.CreatedDate AS DATE) AS Tgl, " & _
            "            TPT.HargaNett AS TotalHarga " & _
            "        FROM TransaksiPaketTindakan TPT " & _
            "        INNER JOIN TransaksiDetail TD ON TPT.NoTransaksi = TD.NoUrut " & _
            "        WHERE TPT.DeletedDate IS NULL " & _
            "          AND TD.JenisPoli = '{0}' " & _
            "          AND TD.DeletedBy IS NULL " & _
            "          AND TPT.CreatedDate BETWEEN '{1}' AND '{2}' " & _
            " " & _
            "        UNION ALL " & _
            " " & _
            "        SELECT " & _
            "            CAST(TPD.CreatedDate AS DATE) AS Tgl, " & _
            "            TPD.NettBayar AS TotalHarga " & _
            "        FROM TransaksiPoliDetail TPD " & _
            "        INNER JOIN TransaksiPoli TP ON TPD.TransPoliID = TP.TransPoliID " & _
            "        INNER JOIN TransaksiDetail TD ON TP.NoUrut = TD.NoUrut " & _
            "        WHERE TPD.DeletedDate IS NULL " & _
            "          AND TD.JenisPoli = '{0}' " & _
            "          AND TD.DeletedBy IS NULL " & _
            "          AND TPD.CreatedDate BETWEEN '{1}' AND '{2}' " & _
            "    ) CombinedSelling " & _
            "    GROUP BY Tgl " & _
            ") S " & _
            "FULL OUTER JOIN ( " & _
            "    SELECT " & _
            "        CAST(TD.TanggalTransaksi AS DATE) AS Tgl, " & _
            "        SUM(TPD.NettBayar) AS TotalRevenue " & _
            "    FROM TransaksiDetail TD " & _
            "    INNER JOIN TransaksiPoliDetail TPD ON TD.NoTransaksi = TPD.TransPoliID " & _
            "    WHERE TPD.DeletedBy IS NULL " & _
            "      AND TD.DeletedBy IS NULL " & _
            "      AND TPD.JenisTindakan = 'Tindakan' " & _
            "      AND TPD.StatusPembayaran = 'PAID' " & _
            "      AND TD.JenisPoli = '{0}' " & _
            "      AND TD.TanggalTransaksi BETWEEN '{1}' AND '{2}' " & _
            "      AND EXISTS ( " & _
            "          SELECT 1 FROM ( " & _
            "              SELECT TransPoliID FROM TransaksiPoliBayar WHERE ISNULL(JumlahBayar, 0) > 0 " & _
            "              UNION ALL " & _
            "              SELECT TransPoliID FROM TransaksiPoliBayarAwal WHERE ISNULL(JumlahBayar, 0) > 0 " & _
            "          ) PB WHERE PB.TransPoliID = TPD.TransPoliID " & _
            "      ) " & _
            "    GROUP BY CAST(TD.TanggalTransaksi AS DATE) " & _
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
        dtRaw.Columns.Add("Today_Collection", GetType(Decimal))
        dtRaw.Columns.Add("ActualCollectionMTD", GetType(Decimal))
        dtRaw.Columns.Add("ActualAchievement", GetType(Decimal))
        dtRaw.Columns.Add("Gap_To_Target", GetType(Decimal))
        dtRaw.Columns.Add("Projection", GetType(Decimal))
        dtRaw.Columns.Add("DailyNeeds", GetType(Decimal))
        dtRaw.Columns.Add("TargetBulanan", GetType(Decimal))
        dtRaw.Columns.Add("Persentase_Selling_Revenue", GetType(Decimal))

        ' Kolom Informasi Hari
        dtRaw.Columns.Add("TotalHariKalender", GetType(Integer))
        dtRaw.Columns.Add("TotalHariKerja", GetType(Integer))
        dtRaw.Columns.Add("HariBerjalan", GetType(Integer))
        dtRaw.Columns.Add("SisaHariKerja", GetType(Integer))

        ' Get Target Bulanan
        Dim targetBulan As Integer = tglAwal.Month
        Dim targetTahun As Integer = tglAwal.Year
        Dim sqlTarget As String = "SELECT ISNULL(TargetBulanan, 0) FROM MasterTargetPoli WHERE JenisPoli = '" & jenisPoli & "' AND Bulan = " & targetBulan & " AND Tahun = " & targetTahun
        Dim dtTarget As DataTable = ReadSQL(sqlTarget)
        Dim nominalTarget As Decimal = If(dtTarget.Rows.Count > 0, CDec(dtTarget.Rows(0)(0)), 0)

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

        ' HITUNG YESTERDAY & TODAY COLLECTION BERDASARKAN TANGGAL EXAK
        Dim totalYesterdayCollection As Decimal = 0
        Dim totalTodayCollection As Decimal = 0

        For Each rowItem As DataRow In dtRaw.Rows
            Dim tglRow As Date = CDate(rowItem("Tgl")).Date
            Dim sellingVal As Decimal = If(IsDBNull(rowItem("SELLING")), 0, CDec(rowItem("SELLING")))

            If tglRow < tglAkhir.Date Then
                totalYesterdayCollection += sellingVal
            ElseIf tglRow = tglAkhir.Date Then
                totalTodayCollection = sellingVal
            End If
        Next

        ' === REVISI: JUMLAH HARI DALAM 1 BULAN KALENDER ===
        Dim totalHariKalender As Integer = Date.DaysInMonth(tglAwal.Year, tglAwal.Month)

        ' HITUNG HARI KERJA
        Dim sqlHariKerja As String = "SELECT ISNULL(HariKerja, 0) FROM MasterTargetPoli WHERE JenisPoli = '" & jenisPoli & "' AND Bulan = " & targetBulan & " AND Tahun = " & targetTahun
        Dim dtHariKerja As DataTable = ReadSQL(sqlHariKerja)
        Dim totalHariKerja As Integer = If(dtHariKerja.Rows.Count > 0, Convert.ToInt32(dtHariKerja.Rows(0)(0)), 1)
        If totalHariKerja = 0 Then totalHariKerja = 1

        Dim hariBerjalan As Integer = HitungHariKerjaBerjalan(tglAwal, tglAkhir)
        Dim sisaHariKerja As Integer = totalHariKerja - hariBerjalan
        If sisaHariKerja < 0 Then sisaHariKerja = 0

        ' POPULASI SELURUH KOLOM
        For Each row As DataRow In dtRaw.Rows
            Dim sellingHariIni As Decimal = If(IsDBNull(row("SELLING")), 0, CDec(row("SELLING")))
            Dim revenueHariIni As Decimal = If(IsDBNull(row("REVENUE")), 0, CDec(row("REVENUE")))

            row("TargetBulanan") = Convert.ToDecimal(nominalTarget)
            row("Yesterday_Collection") = Convert.ToDecimal(totalYesterdayCollection)
            row("Today_Collection") = Convert.ToDecimal(totalTodayCollection)

            row("TotalHariKalender") = totalHariKalender
            row("TotalHariKerja") = totalHariKerja
            row("HariBerjalan") = hariBerjalan
            row("SisaHariKerja") = sisaHariKerja

            ' PERBAIKAN 1: Cek sellingHariIni > 0 sebelum membagi
            If sellingHariIni > 0 Then
                row("Persentase_Selling_Revenue") = Convert.ToDecimal(revenueHariIni / sellingHariIni)
            Else
                row("Persentase_Selling_Revenue") = Convert.ToDecimal(0)
            End If

            If sellingHariIni = 0 Then row("SELLING") = DBNull.Value
            If revenueHariIni = 0 Then row("REVENUE") = DBNull.Value

            Dim mtdVal As Decimal = totalYesterdayCollection + totalTodayCollection
            row("ActualCollectionMTD") = Convert.ToDecimal(mtdVal)

            Dim currentAch As Decimal = 0
            If nominalTarget > 0 Then
                currentAch = Convert.ToDecimal(mtdVal / nominalTarget)
            End If
            row("ActualAchievement") = currentAch

            row("Gap_To_Target") = Convert.ToDecimal(mtdVal - nominalTarget)

            ' PERBAIKAN 2: Cek hariBerjalan > 0 sebelum membagi Projection
            Dim projeksiDesimal As Decimal = 0
            If hariBerjalan > 0 Then
                projeksiDesimal = (currentAch / Convert.ToDecimal(hariBerjalan)) * Convert.ToDecimal(totalHariKerja)
            End If
            row("Projection") = Convert.ToDecimal(projeksiDesimal)

            ' PERBAIKAN 3: Hitung DailyNeeds
            Dim sisaTarget As Decimal = nominalTarget - mtdVal
            If sisaTarget > 0 Then
                If sisaHariKerja > 0 Then
                    row("DailyNeeds") = Convert.ToDecimal(sisaTarget / Convert.ToDecimal(sisaHariKerja))
                Else
                    row("DailyNeeds") = Convert.ToDecimal(sisaTarget)
                End If
            Else
                row("DailyNeeds") = Convert.ToDecimal(0)
            End If
        Next

        ' BUKA REPORT & RUN DESIGNER
        Dim report As New DevExpress.XtraReports.UI.XtraReport
        report = XtraReport.FromFile(Application.StartupPath & "\reports\LaporanRevenueSelling.repx", True)

        Dim bandHeader As ReportHeaderBand = CType(report.Bands(BandKind.ReportHeader), ReportHeaderBand)
        If bandHeader IsNot Nothing Then
            If bandHeader.Controls("lblPoli") IsNot Nothing Then bandHeader.Controls("lblPoli").Text = jenisPoli
            If bandHeader.Controls("lblPeriode") IsNot Nothing Then bandHeader.Controls("lblPeriode").Text = tglAwal.ToString("dd MMM yyyy") & " - " & tglAkhir.ToString("dd MMM yyyy")
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
            Dim confirmResult As DialogResult = MessageBox.Show( _
                "Apakah Anda yakin ingin mengirim data Selling Revenue periode ini?", _
                "Konfirmasi Pengiriman", _
                MessageBoxButtons.YesNo, _
                MessageBoxIcon.Question, _
                MessageBoxDefaultButton.Button2)

            If confirmResult = DialogResult.No Then
                Exit Sub
            End If

            Dim tglAwal As Date = dtAwal.Value.Date
            Dim tglAkhir As Date = dtAkhir.Value.Date

            Dim strAwal As String = tglAwal.ToString("yyyy-MM-dd 00:00:00")
            Dim strAkhir As String = tglAkhir.ToString("yyyy-MM-dd 23:59:59")

            Dim listPoli As New Dictionary(Of String, String)()
            listPoli.Add("POLI KECANTIKAN", "LMIE")
            listPoli.Add("POLI HBOT", "HBOT")
            listPoli.Add("POLI FISIOTERAPI", "FISIOTERAPI")

            Dim totalHari As Integer = (tglAkhir - tglAwal).Days + 1
            Dim totalStep As Integer = listPoli.Count * totalHari

            ProgressBar1.Minimum = 0
            ProgressBar1.Maximum = totalStep + 2
            ProgressBar1.Value = 0
            ProgressBar1.Visible = True

            lblStatusProgress.Text = "Menyiapkan data laporan..."
            lblStatusProgress.Visible = True
            Application.DoEvents()

            Dim sbJson As New StringBuilder()
            sbJson.Append("{")
            sbJson.Append("""user"": """ & userInput & """,")
            sbJson.Append("""laporan"": [")

            Dim isFirstItem As Boolean = True
            Dim currentProgressVal As Integer = 0

            For Each kvp As KeyValuePair(Of String, String) In listPoli
                Dim namaPoliDB As String = kvp.Key
                Dim kodePoliAPI As String = kvp.Value

                lblStatusProgress.Text = "Mengambil data " & namaPoliDB & "..."
                Application.DoEvents()

                ' Query Selling & Revenue Terbaru per Poli
                Dim sqlQuery As String = String.Format( _
                    "SELECT " & _
                    "    ISNULL(S.Tgl, R.Tgl) AS Tgl, " & _
                    "    ISNULL(S.TotalSelling, 0) AS SELLING, " & _
                    "    ISNULL(R.TotalRevenue, 0) AS REVENUE " & _
                    "FROM ( " & _
                    "    SELECT Tgl, SUM(TotalHarga) AS TotalSelling " & _
                    "    FROM ( " & _
                    "        SELECT CAST(TPT.CreatedDate AS DATE) AS Tgl, TPT.HargaNett AS TotalHarga " & _
                    "        FROM TransaksiPaketTindakan TPT " & _
                    "        INNER JOIN TransaksiDetail TD ON TPT.NoTransaksi = TD.NoUrut " & _
                    "        WHERE TPT.DeletedDate IS NULL AND TD.JenisPoli = '{0}' AND TD.DeletedBy IS NULL AND TPT.CreatedDate BETWEEN '{1}' AND '{2}' " & _
                    "        UNION ALL " & _
                    "        SELECT CAST(TPD.CreatedDate AS DATE) AS Tgl, TPD.NettBayar AS TotalHarga " & _
                    "        FROM TransaksiPoliDetail TPD " & _
                    "        INNER JOIN TransaksiPoli TP ON TPD.TransPoliID = TP.TransPoliID " & _
                    "        INNER JOIN TransaksiDetail TD ON TP.NoUrut = TD.NoUrut " & _
                    "        WHERE TPD.DeletedDate IS NULL AND TD.JenisPoli = '{0}' AND TD.DeletedBy IS NULL AND TPD.CreatedDate BETWEEN '{1}' AND '{2}' " & _
                    "    ) CombinedSelling " & _
                    "    GROUP BY Tgl " & _
                    ") S " & _
                    "FULL OUTER JOIN ( " & _
                    "    SELECT " & _
                    "        CAST(TD.TanggalTransaksi AS DATE) AS Tgl, " & _
                    "        SUM(TPD.NettBayar) AS TotalRevenue " & _
                    "    FROM TransaksiDetail TD " & _
                    "    INNER JOIN TransaksiPoliDetail TPD ON TD.NoTransaksi = TPD.TransPoliID " & _
                    "    WHERE TPD.DeletedBy IS NULL " & _
                    "      AND TD.DeletedBy IS NULL " & _
                    "      AND TPD.JenisTindakan = 'Tindakan' " & _
                    "      AND TPD.StatusPembayaran = 'PAID' " & _
                    "      AND TD.JenisPoli = '{0}' " & _
                    "      AND TD.TanggalTransaksi BETWEEN '{1}' AND '{2}' " & _
                    "      AND EXISTS ( " & _
                    "          SELECT 1 FROM ( " & _
                    "              SELECT TransPoliID FROM TransaksiPoliBayar WHERE ISNULL(JumlahBayar, 0) > 0 " & _
                    "              UNION ALL " & _
                    "              SELECT TransPoliID FROM TransaksiPoliBayarAwal WHERE ISNULL(JumlahBayar, 0) > 0 " & _
                    "          ) PB WHERE PB.TransPoliID = TPD.TransPoliID " & _
                    "      ) " & _
                    "    GROUP BY CAST(TD.TanggalTransaksi AS DATE) " & _
                    ") R ON S.Tgl = R.Tgl " & _
                    "ORDER BY Tgl ASC", namaPoliDB, strAwal, strAkhir)

                Dim dtPoli As DataTable = ReadSQL(sqlQuery)

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

                    If Not isFirstItem Then sbJson.Append(",")

                    sbJson.Append("{")
                    sbJson.Append("""tanggal"": """ & strTglCurrent & """,")
                    sbJson.Append("""kode_poli"": """ & kodePoliAPI & """,")
                    sbJson.Append("""selling"": " & sellingVal.ToString(System.Globalization.CultureInfo.InvariantCulture) & ",")
                    sbJson.Append("""revenue"": " & revenueVal.ToString(System.Globalization.CultureInfo.InvariantCulture) & "")
                    sbJson.Append("}")

                    isFirstItem = False
                    currentTgl = currentTgl.AddDays(1)

                    currentProgressVal += 1
                    If currentProgressVal <= ProgressBar1.Maximum Then
                        ProgressBar1.Value = currentProgressVal
                    End If
                    Application.DoEvents()
                End While
            Next

            sbJson.Append("]")
            sbJson.Append("}")

            lblStatusProgress.Text = "Menghubungkan ke API Server..."
            ProgressBar1.Value = ProgressBar1.Maximum - 1
            Application.DoEvents()

            Dim jsonPayload As String = sbJson.ToString()
            Dim baseURL As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'ws_RM'")

            If Not baseURL.EndsWith("/") Then baseURL &= "/"
            Dim apiUrl As String = baseURL & "api/analisa/insert-batch"

            lblStatusProgress.Text = "Mengirim data ke server..."
            Application.DoEvents()

            Dim responseApi As String = InsertDataAPIRMOld(jsonPayload, apiUrl)

            ProgressBar1.Value = ProgressBar1.Maximum
            lblStatusProgress.Text = "Selesai!"
            Application.DoEvents()

            If responseApi.ToLower().Contains("""success"":true") OrElse responseApi.ToLower().Contains("""status"":""success""") Then
                MessageBox.Show("Berhasil mengirim data laporan harian untuk SELURUH POLI (Kecantikan, HBOT, Fisio) periode " & tglAwal.ToString("dd/MM/yyyy") & " s/d " & tglAkhir.ToString("dd/MM/yyyy") & "!", "Sukses Batch API", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Gagal mengirim data. Response Server:" & vbCrLf & responseApi, "Gagal Kirim API", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat pengiriman otomatis seluruh poli: " & ex.Message, "Error Application", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class