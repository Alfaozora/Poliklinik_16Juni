Public Class FormLaporanPasien

    Private Sub FormLaporanPasien_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

        ButtonCari.Enabled = RBTindakanPasien.Checked

        LoadDataPoli()
    End Sub

    Private Sub LoadDataPoli()
        Try
            Dim sqlPoli As String = "SELECT NamaPoli FROM MasterPoliTujuan ORDER BY NamaPoli ASC"
            Dim dtPoli As DataTable = ReadSQL(sqlPoli)

            ComboBoxPoli.Items.Clear()

            ComboBoxPoli.Items.Add("SEMUA POLI")

            If dtPoli IsNot Nothing AndAlso dtPoli.Rows.Count > 0 Then
                For Each row As DataRow In dtPoli.Rows
                    ComboBoxPoli.Items.Add(row("NamaPoli").ToString())
                Next
            End If

            ComboBoxPoli.SelectedIndex = 0
        Catch ex As Exception
            MsgBox("Gagal memuat data poli: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ButtonTampilkan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTampilkan.Click
        If ComboBoxPoli.SelectedIndex = -1 Then
            MsgBox("Silahkan pilih Jenis Poli terlebih dahulu.", MsgBoxStyle.Exclamation, "Peringatan")
            Exit Sub
        End If

        If ComboBoxBulan.SelectedIndex = -1 OrElse ComboBoxTahun.SelectedIndex = -1 Then
            MsgBox("Silahkan pilih Bulan dan Tahun terlebih dahulu.", MsgBoxStyle.Exclamation, "Peringatan")
            Exit Sub
        End If

        If RBTindakanPasien.Checked AndAlso String.IsNullOrEmpty(TextBoxIdTindakan.Text.Trim()) Then
            MsgBox("Silahkan pilih Tindakan terlebih dahulu melalui tombol pencarian.", MsgBoxStyle.Exclamation, "Peringatan")
            Exit Sub
        End If

        Dim jenisPoli As String = ComboBoxPoli.Text.Trim()
        Dim bulan As Integer = ComboBoxBulan.SelectedIndex + 1
        Dim tahun As Integer = Convert.ToInt32(ComboBoxTahun.Text)

        Dim tglAwal As New Date(tahun, bulan, 1)
        Dim tglAkhir As New Date(tahun, bulan, Date.DaysInMonth(tahun, bulan))

        Dim strAwal As String = tglAwal.ToString("yyyy-MM-dd 00:00:00")
        Dim strAkhir As String = tglAkhir.ToString("yyyy-MM-dd 23:59:59")

        Dim sqlReport As String = ""
        Dim repxFileName As String = ""

        If RBPerPoli.Checked Then
            repxFileName = "\reports\LaporanPasienPoli.repx"
            sqlReport = String.Format( _
                "SELECT DISTINCT MP.Nama, MP.NoRekamMedis, MP.TglLahir, MP.NoHandphone " & _
                "FROM TransaksiDetail TD " & _
                "INNER JOIN MasterPasien MP ON TD.IDInternet = MP.NoRekamMedis " & _
                "WHERE TD.DeletedBy IS NULL " & _
                "  AND TD.CreatedDate BETWEEN '{0}' AND '{1}' " & _
                "  AND ('{2}' = 'SEMUA POLI' OR TD.JenisPoli = '{2}') " & _
                "ORDER BY MP.Nama ASC", strAwal, strAkhir, jenisPoli)

        ElseIf RBTotalKunjungan.Checked Then
            repxFileName = "\reports\LaporanTotalKunjunganPasien.repx"
            sqlReport = String.Format( _
                "SELECT " & _
                "    MP.NoRekamMedis, " & _
                "    MP.Nama, " & _
                "    COUNT(TD.NoTransaksi) AS TotalKunjungan, " & _
                "    MAX(TD.CreatedDate) AS TglTerakhirKunjungan " & _
                "FROM TransaksiDetail TD " & _
                "INNER JOIN MasterPasien MP ON TD.IDInternet = MP.NoRekamMedis " & _
                "WHERE TD.DeletedBy IS NULL " & _
                "  AND TD.CreatedDate BETWEEN '{0}' AND '{1}' " & _
                "  AND ('{2}' = 'SEMUA POLI' OR TD.JenisPoli = '{2}') " & _
                "GROUP BY MP.NoRekamMedis, MP.Nama " & _
                "ORDER BY COUNT(TD.NoTransaksi) DESC, MP.Nama ASC", strAwal, strAkhir, jenisPoli)
        ElseIf RBPasienBaru.Checked Then
            repxFileName = "\reports\LaporanPasienBaru.repx"
            sqlReport = String.Format( _
                "WITH PasienPertama AS ( " & _
                "    SELECT IDInternet, MIN(CreatedDate) AS TransaksiPertama " & _
                "    FROM TransaksiDetail " & _
                "    WHERE DeletedBy IS NULL " & _
                "    GROUP BY IDInternet " & _
                ") " & _
                "SELECT " & _
                "    MP.NoRekamMedis, " & _
                "    MP.Nama, " & _
                "    MP.TglLahir, " & _
                "    MP.NoHandphone, " & _
                "    PP.TransaksiPertama AS TglKunjunganPertama, " & _
                "    ISNULL(SUM(TPD.NettBayar), 0) AS TotalTransaksi " & _
                "FROM PasienPertama PP " & _
                "INNER JOIN MasterPasien MP ON PP.IDInternet = MP.NoRekamMedis " & _
                "INNER JOIN TransaksiDetail TD ON MP.NoRekamMedis = TD.IDInternet AND TD.CreatedDate = PP.TransaksiPertama " & _
                "INNER JOIN TransaksiPoliDetail TPD ON TD.NoTransaksi = TPD.TransPoliID " & _
                "WHERE PP.TransaksiPertama BETWEEN '{0}' AND '{1}' " & _
                "  AND TD.DeletedBy IS NULL " & _
                "  AND TPD.DeletedBy IS NULL " & _
                "  AND TPD.StatusPembayaran = 'PAID' " & _
                "  AND ('{2}' = 'SEMUA POLI' OR TD.JenisPoli = '{2}') " & _
                "GROUP BY MP.NoRekamMedis, MP.Nama, MP.TglLahir, MP.NoHandphone, PP.TransaksiPertama " & _
                "ORDER BY MP.Nama ASC", strAwal, strAkhir, jenisPoli)

        ElseIf RBTindakanPasien.Checked Then
            repxFileName = "\reports\LaporanTindakanPasien.repx"
            Dim idTindakan As String = TextBoxIdTindakan.Text.Trim().Replace("'", "''")

            sqlReport = String.Format( _
                "SELECT " & _
                "    MP.NoRekamMedis, " & _
                "    MP.Nama, " & _
                "    MT.NamaTindakan, " & _
                "    COUNT(TPD.TransPoliIDDetail) AS TotalPakaiTindakan, " & _
                "    MAX(TD.TanggalTransaksi) AS TglTerakhirKunjungan " & _
                "FROM TransaksiDetail TD " & _
                "INNER JOIN MasterPasien MP ON TD.IDInternet = MP.NoRekamMedis " & _
                "INNER JOIN TransaksiPoliDetail TPD ON TD.NoTransaksi = TPD.TransPoliID " & _
                "INNER JOIN MasterTindakan MT ON TPD.TindakanID = MT.TindakanID " & _
                "WHERE TD.DeletedBy IS NULL " & _
                "  AND TPD.DeletedBy IS NULL " & _
                "  AND TPD.StatusPembayaran = 'PAID' " & _
                "  AND TD.TanggalTransaksi BETWEEN '{0}' AND '{1}' " & _
                "  AND TPD.TindakanID = '{2}' " & _
                "GROUP BY MP.NoRekamMedis, MP.Nama, MT.NamaTindakan " & _
                "ORDER BY COUNT(TPD.TransPoliIDDetail) DESC, MP.Nama ASC", strAwal, strAkhir, idTindakan)

        Else
            MsgBox("Silahkan pilih salah satu jenis laporan terlebih dahulu.", MsgBoxStyle.Exclamation, "Peringatan")
            Exit Sub
        End If

        Try
            Dim dtData As DataTable = ReadSQL(sqlReport)

            If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                Dim report As New DevExpress.XtraReports.UI.XtraReport
                report = DevExpress.XtraReports.UI.XtraReport.FromFile(Application.StartupPath & repxFileName, True)

                ' Tambahkan kolom pendukung Header & Nomor
                dtData.Columns.Add("No", GetType(Integer))
                dtData.Columns.Add("PoliHeader", GetType(String))
                dtData.Columns.Add("PeriodeHeader", GetType(String))

                Dim noUrut As Integer = 1
                For Each row As DataRow In dtData.Rows
                    row("No") = noUrut
                    row("PoliHeader") = jenisPoli
                    row("PeriodeHeader") = ComboBoxBulan.Text & " " & tahun.ToString()
                    noUrut += 1
                Next

                report.DataSource = dtData
                report.ShowPreview()
                'report.RunDesigner()
            Else
                MsgBox("Tidak ada data pasien untuk " & jenisPoli & " pada periode " & ComboBoxBulan.Text & " " & tahun.ToString() & ".", MsgBoxStyle.Information, "Informasi")
            End If
        Catch ex As Exception
            MsgBox("Gagal menampilkan laporan: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ButtonCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCari.Click
        Try
            Dim jenisPoli As String = ""
            If ComboBoxPoli.SelectedIndex <> -1 Then
                jenisPoli = ComboBoxPoli.Text.Trim()
            End If

            Dim queryString As String = "SELECT TindakanID, NamaTindakan, JenisTindakan, " & _
                                        "PARSENAME(CONVERT(varchar, CONVERT(money, BiayaTindakan), 1), 2) AS [BiayaTindakan] " & _
                                        "FROM MasterTindakan " & _
                                        "WHERE Closed = 0 AND JenisTindakan NOT LIKE '%Konsultasi%' AND JenisTindakan NOT LIKE '%Administrasi%'"

            If jenisPoli <> "" AndAlso jenisPoli <> "SEMUA POLI" Then
                queryString &= " AND PoliTujuan LIKE '%" & jenisPoli.Replace("'", "''") & "%'"
            End If

            Dim queryOrderString As String = " ORDER BY JenisTindakan, NamaTindakan"

            Find.Show()
            Find.Query(queryString, queryOrderString, "FindTindakanLaporanPasien", 1)

        Catch ex As Exception
            MsgBox("Gagal membuka pencarian tindakan: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Public Sub LoadTindakanPoli(ByRef Trow As DataRowView)
        Try
            If Trow IsNot Nothing Then
                If Val(Trow("TindakanID")) > 0 Then
                    TextBoxIdTindakan.Text = Trow("TindakanID").ToString()
                    TextBoxTindakan.Text = Trow("NamaTindakan").ToString()
                End If
            End If
        Catch ex As Exception
            MsgBox("Gagal memuat data tindakan: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Public Sub SetTindakanPilihan(ByVal idTindakan As String, ByVal namaTindakan As String)
        TextBoxTindakan.Text = namaTindakan
        TextBoxTindakan.Tag = idTindakan
    End Sub

    Private Sub RBTindakanPasien_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBTindakanPasien.CheckedChanged
        ComboBoxPoli.Enabled = Not RBTindakanPasien.Checked
        ButtonCari.Enabled = RBTindakanPasien.Checked

        If RBTindakanPasien.Checked Then
            ComboBoxPoli.Text = "SEMUA POLI"
        Else
            TextBoxIdTindakan.Clear()
            TextBoxTindakan.Clear()
        End If
    End Sub
End Class