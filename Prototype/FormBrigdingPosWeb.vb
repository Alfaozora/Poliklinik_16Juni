Imports Newtonsoft.Json
Imports System.Data.SqlClient
Imports System.Globalization

Public Class FormBrigdingPosWeb
    Private _nomorUrutTransaksi As String = ""
    Private listDataPOS As List(Of Transaksi)

    Private Sub ButtonCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCari.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim startDate As String = dtAwal.Value.ToString("yyyy-MM-dd")
            Dim endDate As String = dtAkhir.Value.ToString("yyyy-MM-dd")

            Dim baseUrl As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'ws_RM' ")
            If Not baseUrl.EndsWith("/") Then baseUrl &= "/"
            Dim fullUri As String = String.Format("{0}api/pos/action-packages?start_date={1}&end_date={2}", baseUrl, startDate, endDate)

            Dim jsonResult As String = GetDataAPIRM(fullUri)

            Dim apiRes As ApiResponse = JsonConvert.DeserializeObject(Of ApiResponse)(jsonResult)

            If apiRes IsNot Nothing AndAlso apiRes.code = 200 Then
                Dim rawDataFromAPI As List(Of Transaksi) = apiRes.data

                Dim filteredList As New List(Of Transaksi)()

                If rawDataFromAPI IsNot Nothing AndAlso rawDataFromAPI.Count > 0 Then
                    For Each item In rawDataFromAPI
                        Dim sudahDisimpan As Boolean = False

                        If item.PaketTindakan IsNot Nothing AndAlso item.PaketTindakan.Count > 0 Then
                            For Each pkt In item.PaketTindakan
                                If IsTransaksiSudahAdaLokal(item.NoTelepon, pkt.KodeVoucher) Then
                                    sudahDisimpan = True
                                    Exit For
                                End If
                            Next
                        End If

                        If Not sudahDisimpan Then
                            filteredList.Add(item)
                        End If
                    Next
                End If

                listDataPOS = filteredList

                TampilkanKeGrid(listDataPOS)

                If listDataPOS Is Nothing OrElse listDataPOS.Count = 0 Then
                    MessageBox.Show("Tidak ada data transaksi baru (atau semua data pada rentang tanggal tersebut sudah tersimpan di database lokal).", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            'DEBUG

            'Dim sb As New System.Text.StringBuilder()
            'sb.AppendLine("=== HASIL MAPPING PASIEN & TRANSAKSI TERPILIH ===")
            'sb.AppendLine("Total Transaksi: " & listSelectedItems.Count)
            'sb.AppendLine("------------------------------------")

            'Try
            '    For i As Integer = 0 To listSelectedItems.Count - 1
            '        Dim item As Transaksi = listSelectedItems(i)

            '        ' 1. Formatting Nomor Telepon (Tambah 0 jika belum ada)
            '        Dim noHpFormatted As String = FormatNoTelepon(item.NoTelepon)

            '        ' 2. Cek ke MasterPasien menggunakan fungsi ReadSQL milik Anda
            '        Dim statusPasien As String = ""
            '        Dim queryCek As String = "SELECT * FROM MasterPasien WHERE NoHandphone = '" & noHpFormatted & "'"

            '        Dim tblPasien As DataTable = ReadSQL(queryCek)

            '        ' Cek apakah ada baris data yang ditemukan
            '        If tblPasien IsNot Nothing AndAlso tblPasien.Rows.Count > 0 Then
            '            Dim rowPasien As DataRow = tblPasien.Rows(0)

            '            ' Dapatkan data pasien lokal (Sesuaikan nama kolom dengan DB Anda, misal: NoRekamMedis / KodePasien)
            '            Dim rmLokal As String = rowPasien("NoRekamMedis").ToString()
            '            Dim namaLokal As String = rowPasien("Nama").ToString()

            '            statusPasien = "[DITEMUKAN IN DB] No RM Lokal: " & rmLokal & " (" & namaLokal & ")"
            '        Else
            '            statusPasien = "[PASIEN BARU] Tidak ditemukan, akan di-INSERT ke MasterPasien"
            '        End If

            '        ' 3. Susun Tampilan Preview ke StringBuilder
            '        sb.AppendLine(String.Format("[{0}] No. RM Web : {1}", i + 1, item.NoRekamMedis))
            '        sb.AppendLine("    Nama Pasien : " & item.NamaLengkap)
            '        sb.AppendLine("    No. Telepon : " & item.NoTelepon & " -> [Formatted: " & noHpFormatted & "]")
            '        sb.AppendLine("    Status DB   : " & statusPasien)
            '        sb.AppendLine("    Tgl Transaksi: " & item.TanggalTransaksi)
            '        sb.AppendLine("    Jenis Bayar : " & item.JenisPembayaran)
            '        sb.AppendLine("    Total Nett  : Rp " & item.TotalNettPembayaran.ToString("N0"))

            '        ' Loop Paket Tindakan
            '        If item.PaketTindakan IsNot Nothing AndAlso item.PaketTindakan.Count > 0 Then
            '            sb.AppendLine("    > Paket Purchased (" & item.PaketTindakan.Count & "):")
            '            For Each pkt In item.PaketTindakan
            '                sb.AppendLine("      - " & pkt.NamaPaket & " (Rp " & pkt.HargaAkhir.ToString("N0") & ")")

            '                ' Loop Detail Tindakan
            '                If pkt.Tindakan IsNot Nothing AndAlso pkt.Tindakan.Count > 0 Then
            '                    For Each tnd In pkt.Tindakan
            '                        sb.AppendLine("        * " & tnd.NamaTindakan & " (" & tnd.JenisTindakan & " - Kuota: " & tnd.KuotaTindakan & ")")
            '                    Next
            '                End If
            '            Next
            '        Else
            '            sb.AppendLine("    > Paket Purchased: Tidak ada paket")
            '        End If

            '        sb.AppendLine("------------------------------------")
            '    Next

            'Catch ex As Exception
            '    sb.AppendLine("Error saat pengecekan DB: " & ex.Message)
            'End Try
            'MessageBox.Show(sb.ToString(), "Preview Mapping MasterPasien", MessageBoxButtons.OK, MessageBoxIcon.Information)

            'End Debug

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
        Dim totalPoliDetail As Integer = 0
        Dim totalPaketTindakan As Integer = 0
        Dim totalPaketTindakanDetail As Integer = 0

        Try
            For Each item In items
                ' 1. Formatting Nomor Telepon (Tambah 0 di depan jika belum ada)
                Dim noHpFormatted As String = FormatNoTelepon(item.NoTelepon)

                ' Ambil TanggalTransaksi dari API (dengan fallback DateTime.Now jika null/kosong)
                Dim valTglTransaksi As String = If(String.IsNullOrEmpty(item.TanggalTransaksi), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), item.TanggalTransaksi)
                Dim dtTx As DateTime = DateTime.Now
                DateTime.TryParse(valTglTransaksi, dtTx)

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
                    Dim userInputPasien As String = If(String.IsNullOrEmpty(item.Admin), "BRIDGING_POS_WEB", item.Admin)
                    Dim tglLahir As String = If(String.IsNullOrEmpty(item.TanggalLahir), "1900-01-01", item.TanggalLahir)
                    Dim jenisKelamin As String = If(String.IsNullOrEmpty(item.JenisKelamin), "-", item.JenisKelamin)
                    Dim alamat As String = If(String.IsNullOrEmpty(item.Alamat), "-", item.Alamat)

                    Dim queryInsertPasien As String = _
                        "INSERT INTO MasterPasien (NoRekamMedis, NoNIK, Nama, NoHandphone, PasienTanpaNIK, TglLahir, JenisKelamin, DomisiliAlamat, CreatedBy, CreatedDate) " & _
                        "VALUES (@noRekamMedis, @noNIK, @namaPasien, @noHandphone, 1, @tglLahir, @jenisKelamin, @alamat, @userInput, @tglTransaksi)"

                    WriteParameterizedLokalSQL(queryInsertPasien, 4, _
                         New SqlParameter("@noRekamMedis", noRekamMedisLokal), _
                         New SqlParameter("@noNIK", noNIKBaru), _
                         New SqlParameter("@namaPasien", item.NamaLengkap), _
                         New SqlParameter("@noHandphone", noHpFormatted), _
                         New SqlParameter("@tglLahir", tglLahir), _
                         New SqlParameter("@jenisKelamin", jenisKelamin), _
                         New SqlParameter("@alamat", alamat), _
                         New SqlParameter("@userInput", userInputPasien), _
                         New SqlParameter("@tglTransaksi", valTglTransaksi))

                    totalPasienBaru += 1
                End If

                ' 3. INSERT KE TABEL TransaksiPoli PER PAKET TINDAKAN
                If item.PaketTindakan IsNot Nothing AndAlso item.PaketTindakan.Count > 0 Then
                    For Each pkt In item.PaketTindakan

                        ' A. Generate NoUrut Transaksi
                        Dim kodeTransaksi As String = "U"
                        Dim jenisPoli As String = "UMUM"

                        ' Parse TanggalTransaksi dari API ke format yyMMdd / yyyyMMdd untuk NoUrut
                        Dim tglFormat As String = dtTx.ToString("yyyyMMdd")
                        Dim isMCU As Boolean = False

                        Dim noUrutBaru As String = generateNoTransaksi(kodeTransaksi, jenisPoli, tglFormat, isMCU)

                        ' B. User Input / Admin
                        Dim userInput As String = If(String.IsNullOrEmpty(item.Admin), "BRIDGING_POS_WEB", item.Admin)
                        Dim dokterDefault As String = ""
                        Dim perawatDefault As String = ""

                        ' C. Query INSERT ke TransaksiPoli (TglPoli, CreatedDate, ArrivedDate menggantikan GETDATE)
                        Dim queryInsertPoli As String = _
                            "INSERT INTO TransaksiPoli (" & _
                            "   IDInternet, DokterPoli, PerawatPoli, TglPoli, TotalTagihan, TotalDisc, Pembulatan, " & _
                            "   NettBayar, TotalBayar, Pengembalian, ClosedBayar, ClosedTransaksi, " & _
                            "   CreatedBy, CreatedDate, NoUrut, StatusKeterangan, StatusTransaksi, ArrivedDate" & _
                            ") VALUES (" & _
                            "   @idInternet, @dokterPoli, @perawatPoli, @tglTransaksi, @totalTagihan, @totalDisc, 0, " & _
                            "   @nettBayar, @totalBayar, 0, '9998-12-31', 0, " & _
                            "   @createdBy, @tglTransaksi, @noUrut, 'Belum Diperiksa', 'arrived', @tglTransaksi" & _
                            ")"

                        WriteParameterizedLokalSQL(queryInsertPoli, 4, _
                             New SqlParameter("@idInternet", noRekamMedisLokal), _
                             New SqlParameter("@dokterPoli", dokterDefault), _
                             New SqlParameter("@perawatPoli", perawatDefault), _
                             New SqlParameter("@totalTagihan", pkt.HargaAwal), _
                             New SqlParameter("@totalDisc", pkt.Diskon), _
                             New SqlParameter("@nettBayar", pkt.HargaAkhir), _
                             New SqlParameter("@totalBayar", pkt.HargaAkhir), _
                             New SqlParameter("@createdBy", userInput), _
                             New SqlParameter("@noUrut", noUrutBaru), _
                             New SqlParameter("@tglTransaksi", valTglTransaksi))

                        totalTransaksiPoli += 1

                        Dim transPoliID As String = ReadSQLNIK("SELECT TOP 1 TransPoliID FROM TransaksiPoli WHERE NoUrut = '" & noUrutBaru & "' ORDER BY TransPoliID DESC")

                        Dim valJenisPoli As String = If(String.IsNullOrEmpty(pkt.JenisPoli), "POLI FISIOTERAPI", pkt.JenisPoli)

                        If Not String.IsNullOrEmpty(transPoliID) Then
                            ' E. INSERT ke TransaksiDetail (TanggalTransaksi & CreatedDate menggantikan GETDATE)
                            Dim queryInsertDetail As String = _
                                "INSERT INTO TransaksiDetail (" & _
                                "   StatusBPJS, NoUrutBPJS, NoKunjunganBPJS, JenisKunjungan, JenisRawat, StatusPulang, " & _
                                "   NoTransaksi, JenisTransaksi, JenisPenagihan, TanggalTransaksi, IDInternet, " & _
                                "   KeluhanUtama, KeluhanTambahan, CreatedBy, CreatedDate, AntreanOnline, TransaksiPaket, " & _
                                "   Sumber, JenisPoli, NoUrut" & _
                                ") VALUES (" & _
                                "   0, '', '', 'Kunjungan Sakit', 'Rawat Jalan', 'Berobat Jalan', " & _
                                "   @noTransaksi, 'POLI', 'UMUM', @tglTransaksi, @idInternet, " & _
                                "   '', '', @createdBy, @tglTransaksi, 0, 1, " & _
                                "   'Bridging_POS', @jenisPoli, @noUrut" & _
                                ")"

                            WriteParameterizedLokalSQL(queryInsertDetail, 4, _
                                 New SqlParameter("@noTransaksi", transPoliID), _
                                 New SqlParameter("@idInternet", noRekamMedisLokal), _
                                 New SqlParameter("@createdBy", userInput), _
                                 New SqlParameter("@jenisPoli", valJenisPoli), _
                                 New SqlParameter("@noUrut", noUrutBaru), _
                                 New SqlParameter("@tglTransaksi", valTglTransaksi))

                            totalTransaksiDetail += 1

                            ' F. INSERT ke TransaksiPoliBayarAwal (TransaksiTime menggantikan GETDATE)
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
                                "   @tglTransaksi, @kembali, 'POLI', @mesin, @kartu" & _
                                ")"

                            WriteParameterizedLokalSQL(queryInsertBayarAwal, 4, _
                                 New SqlParameter("@transPoliID", transPoliID), _
                                 New SqlParameter("@jenisBayar", valJenisBayar), _
                                 New SqlParameter("@noJenisBayar", valNoKartu), _
                                 New SqlParameter("@jumlahBayar", item.TotalPembayaranAkhir), _
                                 New SqlParameter("@admin", userInput), _
                                 New SqlParameter("@kembali", item.TotalKembalian), _
                                 New SqlParameter("@mesin", valMesinEDC), _
                                 New SqlParameter("@kartu", valPenerbitKartu), _
                                 New SqlParameter("@tglTransaksi", valTglTransaksi))

                            totalBayarAwal += 1

                            ' G. MASTER PAKET TINDAKAN
                            Dim paketIDLokal As String = "-"
                            Dim namaPaketMaster As String = pkt.NamaPaket
                            Dim keteranganPaketMaster As String = "-"
                            Dim detailTindakanMaster As String = "-"

                            Dim tblMasterPaket As DataTable = ReadSQL("SELECT * FROM MasterPaketTindakan WHERE NamaPaket = '" & pkt.NamaPaket.Replace("'", "''") & "'")
                            If tblMasterPaket IsNot Nothing AndAlso tblMasterPaket.Rows.Count > 0 Then
                                Dim rPaket As DataRow = tblMasterPaket.Rows(0)
                                paketIDLokal = rPaket("PaketID").ToString().ToLower()

                                If tblMasterPaket.Columns.Contains("NamaPaket") Then
                                    namaPaketMaster = rPaket("NamaPaket").ToString()
                                End If

                                If tblMasterPaket.Columns.Contains("KeteranganPaket") Then
                                    keteranganPaketMaster = rPaket("KeteranganPaket").ToString()
                                ElseIf tblMasterPaket.Columns.Contains("Keterangan") Then
                                    keteranganPaketMaster = rPaket("Keterangan").ToString()
                                End If

                                If tblMasterPaket.Columns.Contains("DetailTindakan") Then
                                    detailTindakanMaster = rPaket("DetailTindakan").ToString()
                                End If
                            End If

                            Dim valKodeVoucher As String = If(String.IsNullOrEmpty(pkt.KodeVoucher), "-", pkt.KodeVoucher)

                            ' H. INSERT ke TransaksiPoliDetail (transaksitime menggantikan GETDATE)
                            Dim queryInsertPoliDetail As String = _
                                "INSERT INTO TransaksiPoliDetail (" & _
                                "   TransPoliID, TindakanID, PaketID, NamaTindakan, JenisTindakan, " & _
                                "   BiayaTindakan, DiscTindakan, NettBayar, Penjamin, Hasil, " & _
                                "   admin, transaksitime, DiscPersenTindakan, Awal, Upload, " & _
                                "   KodeVoucher, StatusPembayaran" & _
                                ") VALUES (" & _
                                "   @transPoliID, 0, @paketID, @namaTindakan, '-', " & _
                                "   @biayaTindakan, @discTindakan, @nettBayar, 'Tanpa Asuransi / Umum ', '-', " & _
                                "   @admin, @tglTransaksi, 0, '1', '0', " & _
                                "   @kodeVoucher, 'PAID'" & _
                                ")"

                            WriteParameterizedLokalSQL(queryInsertPoliDetail, 4, _
                                 New SqlParameter("@transPoliID", transPoliID), _
                                 New SqlParameter("@paketID", paketIDLokal), _
                                 New SqlParameter("@namaTindakan", namaPaketMaster), _
                                 New SqlParameter("@biayaTindakan", pkt.HargaAwal), _
                                 New SqlParameter("@discTindakan", pkt.Diskon), _
                                 New SqlParameter("@nettBayar", pkt.HargaAkhir), _
                                 New SqlParameter("@admin", userInput), _
                                 New SqlParameter("@kodeVoucher", valKodeVoucher), _
                                 New SqlParameter("@tglTransaksi", valTglTransaksi))

                            totalPoliDetail += 1

                            ' I. INSERT ke TransaksiPaketTindakan (CreatedDate menggantikan GETDATE)
                            Dim rawJenisPaket As String = If(String.IsNullOrEmpty(pkt.JenisPaket), "-", pkt.JenisPaket)
                            Dim valJenisPaket As String = ToTitleCase(rawJenisPaket)
                            Dim valTglMulai As String = If(String.IsNullOrEmpty(pkt.TanggalMulai), "1900-01-01", pkt.TanggalMulai)
                            Dim valTglAkhir As String = If(String.IsNullOrEmpty(pkt.TanggalAkhir), "1900-01-01", pkt.TanggalAkhir)

                            Dim queryInsertPaketTindakan As String = _
                                "INSERT INTO TransaksiPaketTindakan (" & _
                                "   NoRekamMedis, NoTransaksi, PaketID, NamaPaket, KeteranganPaket, " & _
                                "   JenisPaket, HargaAwal, DiskonNominal, HargaNett, Kuota, " & _
                                "   DetailTindakan, StatusPembayaran, Penjamin, KodeVoucher, " & _
                                "   TanggalMulaiPaket, TanggalAkhirPaket, CreatedBy, CreatedDate" & _
                                ") VALUES (" & _
                                "   @noRekamMedis, @noTransaksi, @paketID, @namaPaket, @keteranganPaket, " & _
                                "   @jenisPaket, @hargaAwal, @diskonNominal, @hargaNett, @kuota, " & _
                                "   @detailTindakan, 'PAID', 'Tanpa Asuransi / Umum', @kodeVoucher, " & _
                                "   @tglMulai, @tglAkhir, @createdBy, @tglTransaksi" & _
                                ")"

                            WriteParameterizedLokalSQL(queryInsertPaketTindakan, 4, _
                                 New SqlParameter("@noRekamMedis", noRekamMedisLokal), _
                                 New SqlParameter("@noTransaksi", noUrutBaru), _
                                 New SqlParameter("@paketID", paketIDLokal), _
                                 New SqlParameter("@namaPaket", namaPaketMaster), _
                                 New SqlParameter("@keteranganPaket", keteranganPaketMaster), _
                                 New SqlParameter("@jenisPaket", valJenisPaket), _
                                 New SqlParameter("@hargaAwal", pkt.HargaAwal), _
                                 New SqlParameter("@diskonNominal", pkt.Diskon), _
                                 New SqlParameter("@hargaNett", pkt.HargaAkhir), _
                                 New SqlParameter("@kuota", pkt.Kuota), _
                                 New SqlParameter("@detailTindakan", detailTindakanMaster), _
                                 New SqlParameter("@kodeVoucher", valKodeVoucher), _
                                 New SqlParameter("@tglMulai", valTglMulai), _
                                 New SqlParameter("@tglAkhir", valTglAkhir), _
                                 New SqlParameter("@createdBy", userInput), _
                                 New SqlParameter("@tglTransaksi", valTglTransaksi))

                            totalPaketTindakan += 1

                            ' J. INSERT ke TransaksiPaketTindakanDetail (CreatedDate menggantikan GETDATE)
                            If Not String.IsNullOrEmpty(detailTindakanMaster) AndAlso detailTindakanMaster <> "-" Then
                                Try
                                    Dim listDetailTindakan As List(Of DetailTindakanItem) = _
                                        Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of DetailTindakanItem))(detailTindakanMaster)

                                    If listDetailTindakan IsNot Nothing AndAlso listDetailTindakan.Count > 0 Then
                                        For Each tndInPaket In listDetailTindakan
                                            Dim queryInsertPaketDetail As String = _
                                                "INSERT INTO TransaksiPaketTindakanDetail (" & _
                                                "   PaketID, KodeVoucher, TindakanID, NamaTindakan, JenisTindakan, " & _
                                                "   BiayaTindakan, DiskonTindakan, NettBayar, Jumlah, GrandTotal, " & _
                                                "   KuotaTindakan, CreatedBy, CreatedDate" & _
                                                ") VALUES (" & _
                                                "   @paketID, @kodeVoucher, @tindakanID, @namaTindakan, @jenisTindakan, " & _
                                                "   @biayaTindakan, @diskonTindakan, @nettBayar, 1, @grandTotal, " & _
                                                "   @kuotaTindakan, @createdBy, @tglTransaksi" & _
                                                ")"

                                            WriteParameterizedLokalSQL(queryInsertPaketDetail, 4, _
                                                 New SqlParameter("@paketID", paketIDLokal), _
                                                 New SqlParameter("@kodeVoucher", valKodeVoucher), _
                                                 New SqlParameter("@tindakanID", tndInPaket.TindakanID), _
                                                 New SqlParameter("@namaTindakan", If(String.IsNullOrEmpty(tndInPaket.NamaTindakan), "-", tndInPaket.NamaTindakan)), _
                                                 New SqlParameter("@jenisTindakan", If(String.IsNullOrEmpty(tndInPaket.JenisTindakan), "-", tndInPaket.JenisTindakan)), _
                                                 New SqlParameter("@biayaTindakan", tndInPaket.BiayaTindakan), _
                                                 New SqlParameter("@diskonTindakan", tndInPaket.DiskonTindakan), _
                                                 New SqlParameter("@nettBayar", tndInPaket.NettBayar), _
                                                 New SqlParameter("@grandTotal", tndInPaket.GrandTotal), _
                                                 New SqlParameter("@kuotaTindakan", tndInPaket.KuotaTindakan), _
                                                 New SqlParameter("@createdBy", userInput), _
                                                 New SqlParameter("@tglTransaksi", valTglTransaksi))

                                            totalPaketTindakanDetail += 1
                                        Next
                                    End If
                                Catch exJson As Exception
                                    Console.WriteLine("Gagal parse DetailTindakan: " & exJson.Message)
                                End Try
                            End If
                        End If
                    Next
                End If

                totalBerhasil += 1
            Next

            MessageBox.Show("Berhasil menyimpan data " & totalBerhasil & " pasien!" & vbCrLf & _
                            "(Pasien Baru: " & totalPasienBaru & ")", _
                            "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Refresh Tampilan Grid
            ButtonCari_Click(Nothing, Nothing)

        Catch ex As Exception
            MessageBox.Show("Gagal menyimpan data ke Database: " & ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ToTitleCase(ByVal text As String) As String
        If String.IsNullOrEmpty(text) Then Return ""
        Return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower())
    End Function

    Private Function IsTransaksiSudahAdaLokal(ByVal noHandphone As String, ByVal kodeVoucher As String) As Boolean
        Try
            Dim noHpFormatted As String = FormatNoTelepon(noHandphone)

            ' Cek ke TransaksiPaketTindakan berdasarkan KodeVoucher dan NoHandphone
            Dim queryCek As String = _
                "SELECT TOP 1 1 " & _
                "FROM TransaksiPaketTindakan TPT " & _
                "INNER JOIN MasterPasien MP ON TPT.NoRekamMedis = MP.NoRekamMedis " & _
                "WHERE MP.NoHandphone = '" & noHpFormatted & "' " & _
                "AND TPT.KodeVoucher = '" & kodeVoucher.Replace("'", "''") & "'"

            Dim dt As DataTable = ReadSQL(queryCek)
            Return (dt IsNot Nothing AndAlso dt.Rows.Count > 0)
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class