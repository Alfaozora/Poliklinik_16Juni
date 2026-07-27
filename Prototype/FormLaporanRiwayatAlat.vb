Public Class FormLaporanRiwayatAlat

    Private userInput As String = PoliKlinik.Lbluser.Text

    Private Sub ButtonCariAlat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCariAlat.Click
        Find.Show()
        Find.Query("SELECT AlatID, KodeAlat, NamaAlat, IsAktif FROM MasterAlat ", "ORDER BY CreatedAt DESC", "FindMasterAlatPreview", 2)
    End Sub

    Public Sub LoadDetailAlat(ByRef AlatID As String)

        Dim sql As String = ""
        sql = "SELECT * FROM MasterAlat WHERE AlatID = '" & AlatID & "'"

        Dim dt As DataTable = ReadSQL(sql)

        If dt.Rows.Count = 0 Then
            MsgBox("Data alat tidak ditemukan.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim row As DataRow = dt.Rows(0)

        ' Isi form
        TextBoxAlatID.Text = row("AlatID").ToString()
        TextBoxKodeAlat.Text = row("KodeAlat").ToString()
        TextBoxNamaAlat.Text = row("NamaAlat").ToString()
        TextBoxNamaRuangan.Text = row("NamaRuangan").ToString()
        TextBoxIdRuangan.Text = row("IdRuangan").ToString()


        CheckBoxAlatAktif.Checked = (row("IsAktif") = True)

    End Sub

    Private Sub ButtonCetakRiwayat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCetakRiwayat.Click
        If TextBoxAlatId.Text.Trim() = "" Then
            MsgBox("Pilih alat terlebih dahulu.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim alatID As String = TextBoxAlatId.Text.Trim()
        Dim tglAwal As Date = dtAwal.Value
        Dim tglAkhir As Date = dtAkhir.Value
        Dim namaAlat As String = TextBoxNamaAlat.Text
        Dim stringTglTransaksiAwal As String = tglAwal.Day & " " & BulanIndonesia(tglAwal.Month) & " " & tglAwal.Year
        Dim stringTglTransaksiAkhir As String = tglAkhir.Day & " " & BulanIndonesia(tglAkhir.Month) & " " & tglAkhir.Year

        ' ========================================================
        ' 1. QUERY SQL (Memisahkan HargaBeliTampil dan HargaBeliTotal)
        ' ========================================================
        Dim sql As String = ""
        sql = "DECLARE @AlatID INT = " & alatID & " " & _
              "DECLARE @TglAwal DATETIME = '" & tglAwal.ToString("yyyy-MM-dd HH:mm:ss") & "' " & _
              "DECLARE @TglAkhir DATETIME = '" & tglAkhir.ToString("yyyy-MM-dd HH:mm:ss") & "' " & _
              " ;WITH Ledger AS ( " & _
              " SELECT " & _
              " MC.CreatedAt AS Tanggal, " & _
              " 'SALDO AWAL' AS JenisTransaksi, " & _
              " MA.NamaAlat + ' - ' + MC.NamaCartridge AS Keterangan, " & _
              " MC.KuotaAwal AS Debet, " & _
              " 0 AS Kredit, " & _
              " MC.CartridgeID, " & _
              " ISNULL(CAST(MC.HargaBeli AS DECIMAL(18,2)), 0) AS HargaBeliTampil, " & _
              " CAST(0 AS DECIMAL(18,2)) AS HargaBeliTotal " & _
              " FROM MasterCartridge MC " & _
              " INNER JOIN MasterAlat MA ON MA.AlatID = MC.AlatID " & _
              " WHERE MA.AlatID = @AlatID " & _
              " UNION ALL " & _
              " SELECT " & _
              " LPA.TglPemakaian, " & _
              " CAST(TP.NoUrut AS VARCHAR(50)), " & _
              " TPD.NamaTindakan + ' - ' + MP.Nama + ' | ' + MC.NamaCartridge, " & _
              " 0, " & _
              " LPA.ShootDipakai, " & _
              " MC.CartridgeID, " & _
              " ISNULL(CAST(LPA.HargaBeli AS DECIMAL(18,2)), 0) * LPA.ShootDipakai AS HargaBeliTampil, " & _
              " ISNULL(CAST(LPA.HargaBeli AS DECIMAL(18,2)), 0) * LPA.ShootDipakai AS HargaBeliTotal " & _
              " FROM LogPemakaianAlat LPA " & _
              " INNER JOIN MasterCartridge MC ON MC.CartridgeID = LPA.CartridgeID " & _
              " INNER JOIN MasterAlat MA ON MA.AlatID = MC.AlatID " & _
              " INNER JOIN TransaksiPoliDetail TPD ON TPD.TransPoliIDDetail = LPA.TransaksiDetailID " & _
              " INNER JOIN TransaksiPoli TP ON TP.TransPoliID = TPD.TransPoliID " & _
              " INNER JOIN MasterPasien MP ON MP.NoRekamMedis = TP.IDInternet " & _
              " WHERE MA.AlatID = @AlatID " & _
              " AND LPA.IsVoid = 0 " & _
              " AND LPA.Status = 1 " & _
              " AND LPA.TglPemakaian BETWEEN @TglAwal AND @TglAkhir " & _
              " UNION ALL " & _
              " SELECT " & _
              " LA.TglAdjustment, " & _
              " 'ADJUSTMENT', " & _
              " 'Penyesuaian - ' + ISNULL(LA.Keterangan, ''), " & _
              " CASE WHEN LA.QtyAdjustment > 0 THEN LA.QtyAdjustment ELSE 0 END, " & _
              " CASE WHEN LA.QtyAdjustment < 0 THEN ABS(LA.QtyAdjustment) ELSE 0 END, " & _
              " MC.CartridgeID, " & _
              " CAST(0 AS DECIMAL(18,2)) AS HargaBeliTampil, " & _
              " CAST(0 AS DECIMAL(18,2)) AS HargaBeliTotal " & _
              " FROM LogAdjustmentCartridge LA " & _
              " INNER JOIN MasterCartridge MC ON MC.CartridgeID = LA.CartridgeID " & _
              " WHERE MC.AlatID = @AlatID " & _
              " AND LA.TglAdjustment BETWEEN @TglAwal AND @TglAkhir " & _
              " ) " & _
              " SELECT " & _
              " L1.CartridgeID, " & _
              " MC.NamaCartridge, " & _
              " L1.Tanggal, " & _
              " L1.JenisTransaksi, " & _
              " L1.Keterangan, " & _
              " L1.Debet, " & _
              " L1.Kredit, " & _
              " (SELECT ISNULL(SUM(L2.Debet - L2.Kredit), 0) " & _
              "  FROM Ledger L2 " & _
              "  WHERE L2.CartridgeID = L1.CartridgeID " & _
              "  AND L2.Tanggal <= L1.Tanggal) AS Saldo, " & _
              " L1.HargaBeliTampil, " & _
              " L1.HargaBeliTotal " & _
              " FROM Ledger L1 " & _
              " INNER JOIN MasterCartridge MC ON MC.CartridgeID = L1.CartridgeID " & _
              " ORDER BY L1.CartridgeID ASC, L1.Tanggal ASC"

        Dim dt As DataTable = ReadSQL(sql)

        If dt.Rows.Count = 0 Then
            MsgBox("Tidak ada data riwayat alat.", MsgBoxStyle.Information)
            Exit Sub
        End If

        ' ========================================================
        ' 2. PERHITUNGAN TOTAL DATATABLE
        ' ========================================================
        Dim totalDebet As Decimal = If(IsDBNull(dt.Compute("SUM(Debet)", "")), 0, CDec(dt.Compute("SUM(Debet)", "")))
        Dim totalKredit As Decimal = If(IsDBNull(dt.Compute("SUM(Kredit)", "")), 0, CDec(dt.Compute("SUM(Kredit)", "")))

        ' Menghitung Total dari HargaBeliTotal (Saldo Awal nilainya 0 di kolom ini)
        Dim totalHargaBeli As Decimal = If(IsDBNull(dt.Compute("SUM(HargaBeliTotal)", "")), 0, CDec(dt.Compute("SUM(HargaBeliTotal)", "")))

        Dim totalSaldo As Decimal = totalDebet - totalKredit

        ' ========================================================
        ' 3. QUERY SUMMARY HEADER ALAT
        ' ========================================================
        Dim sqlSummary As String = _
            "DECLARE @AlatID INT = " & alatID & " " & _
            "SELECT " & _
            "(SELECT ISNULL(SUM(KuotaAwal),0) FROM MasterCartridge WHERE AlatID=@AlatID) AS KuotaAwal, " & _
            "(SELECT ISNULL(SUM(JumlahTopUp),0) FROM LogTopUpAlat WHERE AlatID=@AlatID) AS TotalTopUp, " & _
            "(SELECT ISNULL(SUM(LPA.ShootDipakai),0) FROM LogPemakaianAlat LPA INNER JOIN MasterCartridge MC ON MC.CartridgeID = LPA.CartridgeID WHERE MC.AlatID=@AlatID AND LPA.Status=1 AND LPA.IsVoid=0) AS TotalPemakaian, " & _
            "( " & _
            " (SELECT ISNULL(SUM(KuotaAwal),0) FROM MasterCartridge WHERE AlatID=@AlatID) " & _
            " + (SELECT ISNULL(SUM(JumlahTopUp),0) FROM LogTopUpAlat WHERE AlatID=@AlatID) " & _
            " - (SELECT ISNULL(SUM(LPA.ShootDipakai),0) FROM LogPemakaianAlat LPA INNER JOIN MasterCartridge MC ON MC.CartridgeID=LPA.CartridgeID WHERE MC.AlatID=@AlatID AND LPA.Status=1 AND LPA.IsVoid=0) " & _
            " + (SELECT ISNULL(SUM(LA.QtyAdjustment),0) FROM LogAdjustmentCartridge LA INNER JOIN MasterCartridge MC ON MC.CartridgeID=LA.CartridgeID WHERE MC.AlatID=@AlatID) " & _
            ") AS SaldoAkhir"

        Dim dtSummary As DataTable = ReadSQL(sqlSummary)
        Dim kuotaAwal As Integer = 0
        Dim totalTopUp As Integer = 0
        Dim totalPakai As Integer = 0
        Dim saldoAkhir As Decimal = 0

        If dtSummary.Rows.Count > 0 Then
            kuotaAwal = CInt(dtSummary.Rows(0)("KuotaAwal"))
            totalTopUp = CInt(dtSummary.Rows(0)("TotalTopUp"))
            totalPakai = CInt(dtSummary.Rows(0)("TotalPemakaian"))
            saldoAkhir = CDec(dtSummary.Rows(0)("SaldoAkhir"))
        End If

        ' ========================================================
        ' 4. LOAD REPORT & BINDING
        ' ========================================================
        Dim report As DevExpress.XtraReports.UI.XtraReport = DevExpress.XtraReports.UI.XtraReport.FromFile(Application.StartupPath & "\reports\RiwayatTransaksiAlat.repx", True)

        ' Paksa Grouping Cartridge
        Dim grpHeader As GroupHeaderBand = CType(report.Bands("GroupHeader1"), GroupHeaderBand)
        If grpHeader IsNot Nothing Then
            grpHeader.GroupFields.Clear()
            grpHeader.GroupFields.Add(New GroupField("CartridgeID", XRColumnSortOrder.Ascending))
        End If

        ' Header Report
        Dim bandHeader As ReportHeaderBand = CType(report.Bands(BandKind.ReportHeader), ReportHeaderBand)
        If bandHeader IsNot Nothing Then
            bandHeader.Controls("lblNamaAlat").Text = namaAlat
            bandHeader.Controls("lblKodeAlat").Text = TextBoxKodeAlat.Text
            bandHeader.Controls("lbltanggal").Text = stringTglTransaksiAwal & " - " & stringTglTransaksiAkhir
        End If

        ' Summary Top Header
        Dim bandGroup As GroupHeaderBand = CType(report.Bands("GroupHeader3"), GroupHeaderBand)
        If bandGroup IsNot Nothing Then
            bandGroup.Controls("saldoAwal").Text = Format(kuotaAwal, "#,##0")
            bandGroup.Controls("totalTopUp").Text = Format(totalTopUp, "#,##0")
            bandGroup.Controls("totalPemakaian").Text = Format(totalPakai, "#,##0")
            bandGroup.Controls("saldoAkhir").Text = Format(saldoAkhir, "#,##0")
        End If

        ' Page Footer
        Dim bandPageFooter As PageFooterBand = CType(report.Bands(BandKind.PageFooter), PageFooterBand)
        If bandPageFooter IsNot Nothing Then
            bandPageFooter.Controls("lblprinted").Text = Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") & " By " & userInput
        End If

        ' Report Footer (Grand Total)
        Dim bandReportFooter As ReportFooterBand = CType(report.Bands(BandKind.ReportFooter), ReportFooterBand)
        If bandReportFooter IsNot Nothing Then
            If bandReportFooter.Controls("lblTotalDebet") IsNot Nothing Then bandReportFooter.Controls("lblTotalDebet").Text = Format(totalDebet, "#,##0")
            If bandReportFooter.Controls("lblTotalKredit") IsNot Nothing Then bandReportFooter.Controls("lblTotalKredit").Text = Format(totalKredit, "#,##0")
            If bandReportFooter.Controls("lblTotalHargaBeli") IsNot Nothing Then
                bandReportFooter.Controls("lblTotalHargaBeli").Text = totalHargaBeli.ToString("#,##0.##")
            End If
            If bandReportFooter.Controls("lblTotalSaldo") IsNot Nothing Then bandReportFooter.Controls("lblTotalSaldo").Text = Format(totalSaldo, "#,##0")
        End If

        report.DataSource = dt
        'report.RunDesigner()
        report.ShowPreview()
    End Sub
End Class