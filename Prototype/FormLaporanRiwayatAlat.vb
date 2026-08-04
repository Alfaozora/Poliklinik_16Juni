Imports System.Net
Imports System.IO
Imports System.Text

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
        TextBoxAlatId.Text = row("AlatID").ToString()
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
        ' 1. QUERY SQL (Memperhitungkan Saldo Awal sebelum @TglAwal)
        ' ========================================================
        Dim sql As String = ""
        sql = "DECLARE @AlatID INT = " & alatID & " " & _
              "DECLARE @TglAwal DATETIME = '" & tglAwal.ToString("yyyy-MM-dd HH:mm:ss") & "' " & _
              "DECLARE @TglAkhir DATETIME = '" & tglAkhir.ToString("yyyy-MM-dd HH:mm:ss") & "' " & _
              " ;WITH HistoryBefore AS ( " & _
              "     /* 1. Ambil Kuota Awal Master Cartridge */ " & _
              "     SELECT MC.CartridgeID, MC.CreatedAt AS Tanggal, MC.KuotaAwal AS Debet, 0 AS Kredit " & _
              "     FROM MasterCartridge MC WHERE MC.AlatID = @AlatID " & _
              "     UNION ALL " & _
              "     /* 2. Pemakaian sebelum TglAwal */ " & _
              "     SELECT LPA.CartridgeID, LPA.TglPemakaian, 0, LPA.ShootDipakai " & _
              "     FROM LogPemakaianAlat LPA " & _
              "     INNER JOIN MasterCartridge MC ON MC.CartridgeID = LPA.CartridgeID " & _
              "     WHERE MC.AlatID = @AlatID AND LPA.IsVoid = 0 AND LPA.Status = 1 AND LPA.TglPemakaian < @TglAwal " & _
              "     UNION ALL " & _
              "     /* 3. Adjustment sebelum TglAwal */ " & _
              "     SELECT MC.CartridgeID, LA.TglAdjustment, " & _
              "     CASE WHEN LA.QtyAdjustment > 0 THEN LA.QtyAdjustment ELSE 0 END, " & _
              "     CASE WHEN LA.QtyAdjustment < 0 THEN ABS(LA.QtyAdjustment) ELSE 0 END " & _
              "     FROM LogAdjustmentCartridge LA " & _
              "     INNER JOIN MasterCartridge MC ON MC.CartridgeID = LA.CartridgeID " & _
              "     WHERE MC.AlatID = @AlatID AND LA.TglAdjustment < @TglAwal " & _
              " ), " & _
              " SaldoAwalPeriode AS ( " & _
              "     /* Menghitung akumulasi sisa saldo sebelum TglAwal */ " & _
              "     SELECT CartridgeID, ISNULL(SUM(Debet - Kredit), 0) AS SaldoAwal " & _
              "     FROM HistoryBefore GROUP BY CartridgeID " & _
              " ), " & _
              " Ledger AS ( " & _
              "     /* Baris SALDO AWAL untuk Laporan Periode Ini */ " & _
              "     SELECT " & _
              "     MC.CartridgeID, " & _
              "     @TglAwal AS Tanggal, " & _
              "     'SALDO AWAL' AS JenisTransaksi, " & _
              "     MA.NamaAlat + ' - ' + MC.NamaCartridge AS Keterangan, " & _
              "     ISNULL(SA.SaldoAwal, 0) AS Debet, " & _
              "     0 AS Kredit, " & _
              "     CAST(0 AS DECIMAL(18,2)) AS HargaBeliTampil, " & _
              "     CAST(0 AS DECIMAL(18,2)) AS HargaBeliTotal " & _
              "     FROM MasterCartridge MC " & _
              "     INNER JOIN MasterAlat MA ON MA.AlatID = MC.AlatID " & _
              "     LEFT JOIN SaldoAwalPeriode SA ON SA.CartridgeID = MC.CartridgeID " & _
              "     WHERE MC.AlatID = @AlatID " & _
              "     UNION ALL " & _
              "     /* Transaksi Pemakaian dalam Periode (@TglAwal s/d @TglAkhir) */ " & _
              "     SELECT " & _
              "     MC.CartridgeID, " & _
              "     LPA.TglPemakaian, " & _
              "     CAST(TP.NoUrut AS VARCHAR(50)), " & _
              "     TPD.NamaTindakan + ' - ' + MP.Nama + ' | ' + MC.NamaCartridge, " & _
              "     0, " & _
              "     LPA.ShootDipakai, " & _
              "     ISNULL(CAST(LPA.HargaBeli AS DECIMAL(18,2)), 0) * LPA.ShootDipakai, " & _
              "     ISNULL(CAST(LPA.HargaBeli AS DECIMAL(18,2)), 0) * LPA.ShootDipakai " & _
              "     FROM LogPemakaianAlat LPA " & _
              "     INNER JOIN MasterCartridge MC ON MC.CartridgeID = LPA.CartridgeID " & _
              "     INNER JOIN MasterAlat MA ON MA.AlatID = MC.AlatID " & _
              "     INNER JOIN TransaksiPoliDetail TPD ON TPD.TransPoliIDDetail = LPA.TransaksiDetailID " & _
              "     INNER JOIN TransaksiPoli TP ON TP.TransPoliID = TPD.TransPoliID " & _
              "     INNER JOIN MasterPasien MP ON MP.NoRekamMedis = TP.IDInternet " & _
              "     WHERE MA.AlatID = @AlatID AND LPA.IsVoid = 0 AND LPA.Status = 1 " & _
              "     AND LPA.TglPemakaian BETWEEN @TglAwal AND @TglAkhir " & _
              "     UNION ALL " & _
              "     /* Adjustment dalam Periode (@TglAwal s/d @TglAkhir) */ " & _
              "     SELECT " & _
              "     MC.CartridgeID, " & _
              "     LA.TglAdjustment, " & _
              "     'ADJUSTMENT', " & _
              "     'Penyesuaian - ' + ISNULL(LA.Keterangan, ''), " & _
              "     CASE WHEN LA.QtyAdjustment > 0 THEN LA.QtyAdjustment ELSE 0 END, " & _
              "     CASE WHEN LA.QtyAdjustment < 0 THEN ABS(LA.QtyAdjustment) ELSE 0 END, " & _
              "     CAST(0 AS DECIMAL(18,2)), " & _
              "     CAST(0 AS DECIMAL(18,2)) " & _
              "     FROM LogAdjustmentCartridge LA " & _
              "     INNER JOIN MasterCartridge MC ON MC.CartridgeID = LA.CartridgeID " & _
              "     WHERE MC.AlatID = @AlatID " & _
              "     AND LA.TglAdjustment BETWEEN @TglAwal AND @TglAkhir " & _
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
        Dim totalHargaBeli As Decimal = If(IsDBNull(dt.Compute("SUM(HargaBeliTotal)", "")), 0, CDec(dt.Compute("SUM(HargaBeliTotal)", "")))
        Dim totalSaldo As Decimal = totalDebet - totalKredit

        ' ========================================================
        ' 3. QUERY SUMMARY HEADER ALAT (BERDAPATKAN RENTANG TANGGAL)
        ' ========================================================
        Dim sqlSummary As String = _
            "DECLARE @AlatID INT = " & alatID & " " & _
            "DECLARE @TglAwal DATETIME = '" & tglAwal.ToString("yyyy-MM-dd HH:mm:ss") & "' " & _
            "DECLARE @TglAkhir DATETIME = '" & tglAkhir.ToString("yyyy-MM-dd HH:mm:ss") & "' " & _
            "SELECT " & _
            "/* Saldo Awal sebelum TglAwal */ " & _
            "( " & _
            "  (SELECT ISNULL(SUM(KuotaAwal),0) FROM MasterCartridge WHERE AlatID=@AlatID) " & _
            "  - (SELECT ISNULL(SUM(LPA.ShootDipakai),0) FROM LogPemakaianAlat LPA INNER JOIN MasterCartridge MC ON MC.CartridgeID=LPA.CartridgeID WHERE MC.AlatID=@AlatID AND LPA.Status=1 AND LPA.IsVoid=0 AND LPA.TglPemakaian < @TglAwal) " & _
            "  + (SELECT ISNULL(SUM(LA.QtyAdjustment),0) FROM LogAdjustmentCartridge LA INNER JOIN MasterCartridge MC ON MC.CartridgeID=LA.CartridgeID WHERE MC.AlatID=@AlatID AND LA.TglAdjustment < @TglAwal) " & _
            ") AS KuotaAwalPeriode, " & _
            "/* TopUp pada Periode */ " & _
            "0 AS TotalTopUp, " & _
            "/* Pemakaian pada Periode */ " & _
            "(SELECT ISNULL(SUM(LPA.ShootDipakai),0) FROM LogPemakaianAlat LPA INNER JOIN MasterCartridge MC ON MC.CartridgeID = LPA.CartridgeID WHERE MC.AlatID=@AlatID AND LPA.Status=1 AND LPA.IsVoid=0 AND LPA.TglPemakaian BETWEEN @TglAwal AND @TglAkhir) AS TotalPemakaian, " & _
            "/* Saldo Akhir s/d TglAkhir */ " & _
            "( " & _
            "  (SELECT ISNULL(SUM(KuotaAwal),0) FROM MasterCartridge WHERE AlatID=@AlatID) " & _
            "  - (SELECT ISNULL(SUM(LPA.ShootDipakai),0) FROM LogPemakaianAlat LPA INNER JOIN MasterCartridge MC ON MC.CartridgeID=LPA.CartridgeID WHERE MC.AlatID=@AlatID AND LPA.Status=1 AND LPA.IsVoid=0 AND LPA.TglPemakaian <= @TglAkhir) " & _
            "  + (SELECT ISNULL(SUM(LA.QtyAdjustment),0) FROM LogAdjustmentCartridge LA INNER JOIN MasterCartridge MC ON MC.CartridgeID=LA.CartridgeID WHERE MC.AlatID=@AlatID AND LA.TglAdjustment <= @TglAkhir) " & _
            ") AS SaldoAkhirPeriode"

        Dim dtSummary As DataTable = ReadSQL(sqlSummary)
        Dim kuotaAwal As Integer = 0
        Dim totalTopUp As Integer = 0
        Dim totalPakai As Integer = 0
        Dim saldoAkhir As Decimal = 0

        If dtSummary.Rows.Count > 0 Then
            kuotaAwal = CInt(dtSummary.Rows(0)("KuotaAwalPeriode"))
            totalTopUp = CInt(dtSummary.Rows(0)("TotalTopUp"))
            totalPakai = CInt(dtSummary.Rows(0)("TotalPemakaian"))
            saldoAkhir = CDec(dtSummary.Rows(0)("SaldoAkhirPeriode"))
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

    Private Sub ButtonKirimJurnal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKirimJurnal.Click
        Try
            Dim confirmResult As DialogResult = MessageBox.Show( _
            "Apakah Anda yakin ingin mengirim data jurnal penggunaan alat ini?", _
            "Konfirmasi Pengiriman", _
            MessageBoxButtons.YesNo, _
            MessageBoxIcon.Question, _
            MessageBoxDefaultButton.Button2)

            If confirmResult = DialogResult.No Then
                Exit Sub
            End If

            ' 1. Ambil Rentang Tanggal dari Form Picker
            Dim tglAwal As Date = dtAwal.Value.Date
            Dim tglAkhir As Date = dtAkhir.Value.Date

            Dim strAwal As String = tglAwal.ToString("yyyy-MM-dd 00:00:00")
            Dim strAkhir As String = tglAkhir.ToString("yyyy-MM-dd 23:59:59")

            ' 2. Query Data Penggunaan Alat Baru (Termasuk NoUrut)
            Dim sqlGetLog As String = String.Format( _
                "SELECT " & _
                "   LPA.LogID, " & _
                "   TD.NoUrut, " & _
                "   LPA.TglPemakaian, " & _
                "   TD.JenisPoli AS [Poli], " & _
                "   MA.NamaAlat AS [NamaAlat], " & _
                "   MC.NamaCartridge AS [NamaCartridge], " & _
                "   LPA.ShootDipakai AS [Shoot], " & _
                "   LPA.HargaBeli AS [BiayaShoot] " & _
                "FROM LogPemakaianAlat LPA " & _
                "INNER JOIN MasterCartridge MC ON LPA.CartridgeID = MC.CartridgeID " & _
                "INNER JOIN MasterAlat MA ON MC.AlatID = MA.AlatID " & _
                "INNER JOIN TransaksiPoliDetail TPD ON LPA.TransaksiDetailID = TPD.TransPoliIDDetail " & _
                "INNER JOIN TransaksiDetail TD ON TPD.TransPoliID = TD.NoTransaksi " & _
                "WHERE LPA.IsVoid = '0' " & _
                "  AND LPA.VoidAt IS NULL " & _
                "  AND LPA.Upload = '0' " & _
                "  AND LPA.TglPemakaian BETWEEN '{0}' AND '{1}' " & _
                "ORDER BY LPA.TglPemakaian ASC", strAwal, strAkhir)

            Dim dtLog As DataTable = ReadSQL(sqlGetLog)

            If dtLog Is Nothing OrElse dtLog.Rows.Count = 0 Then
                MessageBox.Show("Tidak ada data penggunaan alat yang perlu dikirim pada periode tersebut.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            ' 3. Inisialisasi UI Progress Bar
            ProgressBar1.Minimum = 0
            ProgressBar1.Maximum = dtLog.Rows.Count + 2
            ProgressBar1.Value = 0
            ProgressBar1.Visible = True

            lblStatusProgress.Text = "Menyiapkan data penggunaan alat..."
            lblStatusProgress.Visible = True
            Application.DoEvents()

            ' 4. Menyusun Payload JSON Batch & List LogID
            Dim sbJson As New StringBuilder()
            sbJson.Append("{")
            sbJson.Append("""user_input"": """ & userInput & """,")
            sbJson.Append("""laporan"": [")

            Dim listLogID As New List(Of String)()
            Dim isFirst As Boolean = True
            Dim progressStep As Integer = 0

            For Each row As DataRow In dtLog.Rows
                Dim logID As String = row("LogID").ToString()
                Dim noTransaksi As String = row("NoUrut").ToString().Trim()
                Dim tglPemakaian As Date = CDate(row("TglPemakaian"))
                Dim strTanggalFormatYmd As String = tglPemakaian.ToString("yyyyMMdd")

                Dim namaPoliDB As String = row("Poli").ToString().Trim().ToUpper()
                Dim namaAlat As String = row("NamaAlat").ToString().Trim()
                Dim namaCartridge As String = row("NamaCartridge").ToString().Trim()
                Dim shoot As Decimal = If(IsDBNull(row("Shoot")), 0, CDec(row("Shoot")))
                Dim biayaShoot As Decimal = If(IsDBNull(row("BiayaShoot")), 0, CDec(row("BiayaShoot")))

                ' Total Nominal Pemakaian
                Dim totalNominal As Decimal = shoot * biayaShoot

                ' Mapping Poli
                Dim poliKey As String = ""
                Select Case namaPoliDB
                    Case "POLI KECANTIKAN"
                        poliKey = "kecantikan"
                    Case "POLI HBOT"
                        poliKey = "hbot"
                    Case "POLI FISIOTERAPI"
                        poliKey = "fisio"
                    Case Else
                        poliKey = namaPoliDB.ToLower()
                End Select

                Dim keterangan As String = String.Format("Pemakaian BHP Alat: {0} [{1}] - No. Transaksi: {2}", namaAlat, namaCartridge, noTransaksi)

                If totalNominal > 0 Then
                    If Not isFirst Then sbJson.Append(",")

                    sbJson.Append("{")
                    sbJson.Append("""poli"": """ & poliKey & """,")
                    sbJson.Append("""nominal"": " & totalNominal.ToString(System.Globalization.CultureInfo.InvariantCulture) & ",")
                    sbJson.Append("""keterangan"": """ & keterangan.Replace("""", "\""") & """,")
                    sbJson.Append("""tanggal"": """ & strTanggalFormatYmd & """")
                    sbJson.Append("}")

                    listLogID.Add(logID)
                    isFirst = False
                End If

                progressStep += 1
                If progressStep <= ProgressBar1.Maximum Then ProgressBar1.Value = progressStep
                Application.DoEvents()
            Next

            sbJson.Append("]")
            sbJson.Append("}")

            If listLogID.Count = 0 Then
                MessageBox.Show("Tidak ada nominal pemakaian alat yang valid untuk dikirim.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' 5. Ambil Config URL & Exec API
            lblStatusProgress.Text = "Menghubungkan ke API Jurnal..."
            ProgressBar1.Value = ProgressBar1.Maximum - 1
            Application.DoEvents()

            Dim baseURL As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'ws_RM'")
            If Not baseURL.EndsWith("/") Then baseURL &= "/"

            Dim StatusUrl As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'ws_jurnal_status'")
            Dim urlApiJurnal As String = baseURL & "api/jurnal/transaksi/penggunaanAlat" & If(StatusUrl = "Dev", "/Stg", "")
            Dim jsonPayload As String = sbJson.ToString()

            lblStatusProgress.Text = "Mengirim jurnal penggunaan alat ke server..."
            Application.DoEvents()

            Dim responseApi As String = InsertDataAPIRMOld(jsonPayload, urlApiJurnal)

            ProgressBar1.Value = ProgressBar1.Maximum
            lblStatusProgress.Text = "Selesai!"
            Application.DoEvents()

            ' 6. Handling Response & Update LogPemakaianAlat
            If responseApi.ToLower().Contains("""success"":true") Then
                Try
                    ' Gabungkan seluruh LogID menjadi format list (misal: '1','2','3')
                    Dim strInLogID As String = String.Join("','", listLogID.ToArray())

                    ' Buat query update batch
                    Dim sqlUpdate As String = "UPDATE LogPemakaianAlat SET Upload = '1' WHERE LogID IN ('" & strInLogID & "')"

                    ' Eksekusi menggunakan WriteSQL milik Anda (parameter kedua disesuaikan jika perlu index connection tertentu, misal 4)
                    WriteSQL(sqlUpdate, 4)

                    MessageBox.Show("Berhasil mengirim " & listLogID.Count & " data penggunaan alat ke Jurnal dan status Upload telah diperbarui!", "Sukses API", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Catch exUpdate As Exception
                    MessageBox.Show("API Berhasil terkirim, namun gagal memperbarui status Upload di database lokal: " & exUpdate.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Else
                MessageBox.Show("Gagal mengirim data penggunaan alat. Response Server:" & vbCrLf & responseApi, "Gagal Kirim API", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat memproses penggunaan alat: " & ex.Message, "Error Application", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ProgressBar1.Visible = False
            lblStatusProgress.Visible = False
            lblStatusProgress.Text = ""
        End Try
    End Sub
End Class