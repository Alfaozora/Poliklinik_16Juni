Imports Newtonsoft.Json
Imports System.Data.SqlClient

Public Class FormBrigdingPosWeb
    Private _nomorUrutTransaksi As String = ""
    Private listDataPOS As List(Of Transaksi)

    Private Sub ButtonCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCari.Click
        Try
            ' Set cursor ke loading
            Me.Cursor = Cursors.WaitCursor

            ' Format tanggal dari DateTimePicker ke Y-m-d
            Dim startDate As String = dtAwal.Value.ToString("yyyy-MM-dd")
            Dim endDate As String = dtAkhir.Value.ToString("yyyy-MM-dd")

            ' Sesuaikan URL dengan server Laravel Anda
            Dim baseUrl As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'ws_RM' ")
            If Not baseUrl.EndsWith("/") Then baseUrl &= "/"
            Dim fullUri As String = String.Format("{0}api/pos/action-packages?start_date={1}&end_date={2}", baseUrl, startDate, endDate)

            ' Panggil Helper GET milik Anda
            Dim jsonResult As String = GetDataAPIRM(fullUri)


            ' Parse response JSON
            Dim apiRes As ApiResponse = JsonConvert.DeserializeObject(Of ApiResponse)(jsonResult)

            If apiRes IsNot Nothing AndAlso apiRes.code = 200 Then
                listDataPOS = apiRes.data

                ' Tampilkan data ke DataGridView
                TampilkanKeGrid(listDataPOS)

                If listDataPOS Is Nothing OrElse listDataPOS.Count = 0 Then
                    MessageBox.Show("Tidak ada data transaksi pada rentang tanggal tersebut.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                Dim msg As String = If(apiRes IsNot Nothing, apiRes.message, "Response kosong atau server error")
                MessageBox.Show("Gagal mengambil data: " & msg, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TampilkanKeGrid(ByVal data As List(Of Transaksi))
        DataGridView.DataSource = Nothing
        DataGridView.Columns.Clear()

        If data Is Nothing Then Exit Sub

        Dim chkCol As New DataGridViewCheckBoxColumn()
        chkCol.Name = "colSelect"
        chkCol.HeaderText = "Pilih"
        chkCol.Width = 50
        chkCol.ReadOnly = False
        DataGridView.Columns.Add(chkCol)

        ' Buat DataTable ringkas untuk konsumsi UI
        Dim dt As New DataTable()
        dt.Columns.Add("No. RM", GetType(String))
        dt.Columns.Add("Nama Pasien", GetType(String))
        dt.Columns.Add("No. Telepon", GetType(String))
        dt.Columns.Add("Tgl Transaksi", GetType(String))
        dt.Columns.Add("Jenis Bayar", GetType(String))
        dt.Columns.Add("Total Nett", GetType(Decimal))
        dt.Columns.Add("Jumlah Paket", GetType(Integer))

        For Each item In data
            Dim jmlPaket As Integer = If(item.PaketTindakan IsNot Nothing, item.PaketTindakan.Count, 0)
            dt.Rows.Add( _
                item.NoRekamMedis, _
                item.NamaLengkap, _
                item.NoTelepon, _
                item.TanggalTransaksi, _
                item.JenisPembayaran, _
                item.TotalNettPembayaran, _
                jmlPaket _
            )
        Next

        DataGridView.DataSource = dt

        For Each col As DataGridViewColumn In DataGridView.Columns
            If col.Name <> "colSelect" Then
                col.ReadOnly = True
            End If
        Next

        ' Format angka Rupiah untuk kolom Total Nett jika ada datanya
        If DataGridView.Columns("Total Nett") IsNot Nothing Then
            DataGridView.Columns("Total Nett").DefaultCellStyle.Format = "N0"
        End If
    End Sub

    Private Sub ButtonSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSubmit.Click
        If listDataPOS Is Nothing OrElse DataGridView.Rows.Count = 0 Then
            MessageBox.Show("Tidak ada data untuk diproses!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Pastikan user mengakhiri edit pada cell DataGridView agar status CheckBox ter-update
        DataGridView.EndEdit()

        ' Kumpulkan index data yang dicentang oleh user
        Dim listSelectedItems As New List(Of Transaksi)()

        For Each row As DataGridViewRow In DataGridView.Rows
            ' Cek nilai CheckBox (Kolom colSelect)
            Dim isChecked As Boolean = Convert.ToBoolean(row.Cells("colSelect").Value)

            If isChecked Then
                ' Ambil object Transaksi berdasarkan Index barisnya
                Dim itemData As Transaksi = listDataPOS(row.Index)
                listSelectedItems.Add(itemData)
            End If
        Next

        ' Validasi jika tidak ada baris yang dicentang
        If listSelectedItems.Count = 0 Then
            MessageBox.Show("Silakan centang/pilih minimal satu transaksi yang akan disimpan!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Konfirmasi ke user
        Dim confirm As DialogResult = MessageBox.Show("Apakah Anda yakin ingin menyimpan " & listSelectedItems.Count & " transaksi terpilih ke Database Lokal?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm = DialogResult.Yes Then
            Dim sb As New System.Text.StringBuilder()
            sb.AppendLine("=== HASIL MAPPING PASIEN & TRANSAKSI TERPILIH ===")
            sb.AppendLine("Total Transaksi: " & listSelectedItems.Count)
            sb.AppendLine("------------------------------------")

            Try
                For i As Integer = 0 To listSelectedItems.Count - 1
                    Dim item As Transaksi = listSelectedItems(i)

                    ' 1. Formatting Nomor Telepon (Tambah 0 jika belum ada)
                    Dim noHpFormatted As String = FormatNoTelepon(item.NoTelepon)

                    ' 2. Cek ke MasterPasien menggunakan fungsi ReadSQL milik Anda
                    Dim statusPasien As String = ""
                    Dim queryCek As String = "SELECT * FROM MasterPasien WHERE NoHandphone = '" & noHpFormatted & "'"

                    Dim tblPasien As DataTable = ReadSQL(queryCek)

                    ' Cek apakah ada baris data yang ditemukan
                    If tblPasien IsNot Nothing AndAlso tblPasien.Rows.Count > 0 Then
                        Dim rowPasien As DataRow = tblPasien.Rows(0)

                        ' Dapatkan data pasien lokal (Sesuaikan nama kolom dengan DB Anda, misal: NoRekamMedis / KodePasien)
                        Dim rmLokal As String = rowPasien("NoRekamMedis").ToString()
                        Dim namaLokal As String = rowPasien("Nama").ToString()

                        statusPasien = "[DITEMUKAN IN DB] No RM Lokal: " & rmLokal & " (" & namaLokal & ")"
                    Else
                        statusPasien = "[PASIEN BARU] Tidak ditemukan, akan di-INSERT ke MasterPasien"
                    End If

                    ' 3. Susun Tampilan Preview ke StringBuilder
                    sb.AppendLine(String.Format("[{0}] No. RM Web : {1}", i + 1, item.NoRekamMedis))
                    sb.AppendLine("    Nama Pasien : " & item.NamaLengkap)
                    sb.AppendLine("    No. Telepon : " & item.NoTelepon & " -> [Formatted: " & noHpFormatted & "]")
                    sb.AppendLine("    Status DB   : " & statusPasien)
                    sb.AppendLine("    Tgl Transaksi: " & item.TanggalTransaksi)
                    sb.AppendLine("    Jenis Bayar : " & item.JenisPembayaran)
                    sb.AppendLine("    Total Nett  : Rp " & item.TotalNettPembayaran.ToString("N0"))

                    ' Loop Paket Tindakan
                    If item.PaketTindakan IsNot Nothing AndAlso item.PaketTindakan.Count > 0 Then
                        sb.AppendLine("    > Paket Purchased (" & item.PaketTindakan.Count & "):")
                        For Each pkt In item.PaketTindakan
                            sb.AppendLine("      - " & pkt.NamaPaket & " (Rp " & pkt.HargaAkhir.ToString("N0") & ")")

                            ' Loop Detail Tindakan
                            If pkt.Tindakan IsNot Nothing AndAlso pkt.Tindakan.Count > 0 Then
                                For Each tnd In pkt.Tindakan
                                    sb.AppendLine("        * " & tnd.NamaTindakan & " (" & tnd.JenisTindakan & " - Kuota: " & tnd.KuotaTindakan & ")")
                                Next
                            End If
                        Next
                    Else
                        sb.AppendLine("    > Paket Purchased: Tidak ada paket")
                    End If

                    sb.AppendLine("------------------------------------")
                Next

            Catch ex As Exception
                sb.AppendLine("Error saat pengecekan DB: " & ex.Message)
            End Try

            ' Tampilkan Preview
            MessageBox.Show(sb.ToString(), "Preview Mapping MasterPasien", MessageBoxButtons.OK, MessageBoxIcon.Information)
            SimpanBulkKeDatabaseLokal(listSelectedItems)
        End If
    End Sub

    Private Function FormatNoTelepon(ByVal rawPhone As String) As String
        If String.IsNullOrEmpty(rawPhone) Then Return ""

        Dim phone As String = rawPhone.Trim()

        ' Jika tidak diawali angka 0, tambahkan 0 di depan
        If Not phone.StartsWith("0") Then
            phone = "0" & phone
        End If

        Return phone
    End Function

    Private Sub SimpanBulkKeDatabaseLokal(ByVal items As List(Of Transaksi))
        Dim totalBerhasil As Integer = 0
        Dim totalPasienBaru As Integer = 0
        Dim totalTransaksiPoli As Integer = 0
        Dim totalTransaksiDetail As Integer = 0
        Dim totalBayarAwal As Integer = 0

        Try
            For Each item In items
                ' 1. Formatting Nomor Telepon (Tambah 0 di depan jika belum ada)
                Dim noHpFormatted As String = FormatNoTelepon(item.NoTelepon)

                ' 2. Cek ke MasterPasien berdasarkan NoHandphone
                Dim queryCek As String = "SELECT * FROM MasterPasien WHERE NoHandphone = '" & noHpFormatted & "'"
                Dim tblPasien As DataTable = ReadSQL(queryCek)

                Dim noRekamMedisLokal As String = ""

                If tblPasien IsNot Nothing AndAlso tblPasien.Rows.Count > 0 Then
                    ' A. PASIEN DITEMUKAN -> Ambil NoRekamMedis yang sudah ada di lokal
                    Dim rowPasien As DataRow = tblPasien.Rows(0)
                    noRekamMedisLokal = rowPasien("NoRekamMedis").ToString()
                Else
                    ' B. PASIEN TIDAK DITEMUKAN -> Generate NoRM & NoNIK Baru, lalu INSERT
                    noRekamMedisLokal = generateNomorRekamMedis()
                    Dim noNIKBaru As String = generateNomorNIK()
                    Dim userInput As String = If(String.IsNullOrEmpty(item.Admin), "BRIDGING_POS_WEB", item.Admin)
                    Dim tglLahir As String = If(String.IsNullOrEmpty(item.TanggalLahir), "1900-01-01", item.TanggalLahir)
                    Dim jenisKelamin As String = If(String.IsNullOrEmpty(item.JenisKelamin), "-", item.JenisKelamin)
                    Dim alamat As String = If(String.IsNullOrEmpty(item.Alamat), "-", item.Alamat)

                    Dim queryInsertPasien As String = _
                        "INSERT INTO MasterPasien (NoRekamMedis, NoNIK, Nama, NoHandphone, PasienTanpaNIK, TglLahir, JenisKelamin, DomisiliAlamat, CreatedBy, CreatedDate) " & _
                        "VALUES (@noRekamMedis, @noNIK, @namaPasien, @noHandphone, 1, @tglLahir, @jenisKelamin, @alamat, @userInput, GETDATE())"

                    WriteParameterizedLokalSQL(queryInsertPasien, 8, _
                         New SqlParameter("@noRekamMedis", noRekamMedisLokal), _
                         New SqlParameter("@noNIK", noNIKBaru), _
                         New SqlParameter("@namaPasien", item.NamaLengkap), _
                         New SqlParameter("@noHandphone", noHpFormatted), _
                         New SqlParameter("@tglLahir", tglLahir), _
                         New SqlParameter("@jenisKelamin", jenisKelamin), _
                         New SqlParameter("@alamat", alamat), _
                         New SqlParameter("@userInput", userInput))

                    totalPasienBaru += 1
                End If

                ' 3. INSERT KE TABEL TransaksiPoli PER PAKET TINDAKAN
                If item.PaketTindakan IsNot Nothing AndAlso item.PaketTindakan.Count > 0 Then
                    For Each pkt In item.PaketTindakan

                        ' A. Generate NoUrut Transaksi
                        Dim kodeTransaksi As String = "U"
                        Dim jenisPoli As String = "UMUM"
                        Dim tglFormat As String = DateTime.Now.ToString("yyMMdd")
                        Dim isMCU As Boolean = False

                        Dim noUrutBaru As String = generateNoTransaksi(kodeTransaksi, jenisPoli, tglFormat, isMCU)

                        ' B. User Input / Admin
                        Dim userInput As String = If(String.IsNullOrEmpty(item.Admin), "BRIDGING_POS_WEB", item.Admin)
                        Dim dokterDefault As String = ""
                        Dim perawatDefault As String = ""

                        ' C. Query INSERT ke TransaksiPoli (Sesuaikan NoRM / NoRekamMedis dengan nama kolom tabel Anda)
                        Dim queryInsertPoli As String = _
                            "INSERT INTO TransaksiPoli (" & _
                            "   IDInternet, DokterPoli, PerawatPoli, TglPoli, TotalTagihan, TotalDisc, Pembulatan, " & _
                            "   NettBayar, TotalBayar, Pengembalian, ClosedBayar, ClosedTransaksi, " & _
                            "   CreatedBy, CreatedDate, NoUrut, StatusKeterangan, StatusTransaksi, ArrivedDate" & _
                            ") VALUES (" & _
                            "   @idInternet, @dokterPoli, @perawatPoli, GETDATE(), @totalTagihan, @totalDisc, 0, " & _
                            "   @nettBayar, @totalBayar, 0, '9998-12-31', 0, " & _
                            "   @createdBy, GETDATE(), @noUrut, 'Belum Diperiksa', 'arrived', GETDATE()" & _
                            ")"

                        ' C. Eksekusi dengan Parameterized SQL (Ubah jumlah parameter dari 7 ke 8)
                        WriteParameterizedLokalSQL(queryInsertPoli, 8, _
                             New SqlParameter("@idInternet", noRekamMedisLokal), _
                             New SqlParameter("@dokterPoli", dokterDefault), _
                             New SqlParameter("@perawatPoli", perawatDefault), _
                             New SqlParameter("@totalTagihan", pkt.HargaAwal), _
                             New SqlParameter("@totalDisc", pkt.Diskon), _
                             New SqlParameter("@nettBayar", pkt.HargaAkhir), _
                             New SqlParameter("@totalBayar", pkt.HargaAkhir), _
                             New SqlParameter("@createdBy", userInput), _
                             New SqlParameter("@noUrut", noUrutBaru))

                        totalTransaksiPoli += 1

                        Dim transPoliID As String = ReadSQLNIK("SELECT TOP 1 TransPoliID FROM TransaksiPoli WHERE NoUrut = '" & noUrutBaru & "' ORDER BY TransPoliID DESC")

                        Dim valJenisPoli As String = If(String.IsNullOrEmpty(pkt.JenisPoli), "POLI FISIOTERAPI", pkt.JenisPoli)

                        ' E. INSERT ke TransaksiDetail (Termasuk JenisPoli & NoUrut)
                        If Not String.IsNullOrEmpty(transPoliID) Then
                            Dim queryInsertDetail As String = _
                                "INSERT INTO TransaksiDetail (" & _
                                "   StatusBPJS, NoUrutBPJS, NoKunjunganBPJS, JenisKunjungan, JenisRawat, StatusPulang, " & _
                                "   NoTransaksi, JenisTransaksi, JenisPenagihan, TanggalTransaksi, IDInternet, " & _
                                "   KeluhanUtama, KeluhanTambahan, CreatedBy, CreatedDate, AntreanOnline, TransaksiPaket, " & _
                                "   Sumber, JenisPoli, NoUrut" & _
                                ") VALUES (" & _
                                "   0, '', '', 'Kunjungan Sakit', 'Rawat Jalan', 'Berobat Jalan', " & _
                                "   @noTransaksi, 'POLI', 'UMUM', GETDATE(), @idInternet, " & _
                                "   '', '', @createdBy, GETDATE(), 0, 1, " & _
                                "   'Bridging_POS', @jenisPoli, @noUrut" & _
                                ")"

                            ' Parameter bertambah menjadi 5 Parameter
                            WriteParameterizedLokalSQL(queryInsertDetail, 5, _
                                 New SqlParameter("@noTransaksi", transPoliID), _
                                 New SqlParameter("@idInternet", noRekamMedisLokal), _
                                 New SqlParameter("@createdBy", userInput), _
                                 New SqlParameter("@jenisPoli", valJenisPoli), _
                                 New SqlParameter("@noUrut", noUrutBaru))

                            totalTransaksiDetail += 1

                            Dim valJenisBayar As String = If(String.IsNullOrEmpty(item.JenisPembayaran), "TUNAI", item.JenisPembayaran)
                            Dim valNoKartu As String = If(String.IsNullOrEmpty(item.NoKartu), "-", item.NoKartu)
                            Dim valMesinEDC As String = If(String.IsNullOrEmpty(item.MesinEDC), "-", item.MesinEDC)
                            Dim valPenerbitKartu As String = If(String.IsNullOrEmpty(item.PenerbitKartu), "-", item.PenerbitKartu)

                            Dim queryInsertBayarAwal As String = _
                                "INSERT INTO TransaksiPoliBayarAwal (" & _
                                "   TransPoliID, JenisBayar, NoJenisBayar, JumlahBayar, admin, " & _
                                "   TransaksiTime, Kembali, TransJenis, mesin, kartu" & _
                                ") VALUES (" & _
                                "   @transPoliID, @jenisBayar, @noJenisBayar, @jumlahBayar, @admin, " & _
                                "   GETDATE(), @kembali, 'POLI', @mesin, @kartu" & _
                                ")"

                            WriteParameterizedLokalSQL(queryInsertBayarAwal, 9, _
                                 New SqlParameter("@transPoliID", transPoliID), _
                                 New SqlParameter("@jenisBayar", valJenisBayar), _
                                 New SqlParameter("@noJenisBayar", valNoKartu), _
                                 New SqlParameter("@jumlahBayar", item.TotalPembayaranAkhir), _
                                 New SqlParameter("@admin", userInput), _
                                 New SqlParameter("@kembali", item.TotalKembalian), _
                                 New SqlParameter("@mesin", valMesinEDC), _
                                 New SqlParameter("@kartu", valPenerbitKartu))

                            totalBayarAwal += 1
                        End If
                    Next
                End If

                totalBerhasil += 1
            Next

            MessageBox.Show("Proses simpan berhasil!" & vbCrLf & _
                        "- Total Transaksi Diproses   : " & totalBerhasil & vbCrLf & _
                        "- Pasien Baru Dibuat          : " & totalPasienBaru & vbCrLf & _
                        "- TransaksiPoli Terbuat       : " & totalTransaksiPoli & vbCrLf & _
                        "- TransaksiDetail Terbuat     : " & totalTransaksiDetail & vbCrLf & _
                        "- TransaksiPoliBayarAwal      : " & totalBayarAwal, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Refresh Tampilan Grid
            ButtonCari_Click(Nothing, Nothing)

        Catch ex As Exception
            MessageBox.Show("Gagal menyimpan data ke Database: " & ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class