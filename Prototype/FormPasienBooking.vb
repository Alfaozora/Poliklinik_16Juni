Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient
Imports System.Globalization

Public Class FormPasienBooking
    Private listDataCheckin As New List(Of PasienCheckinItem)()

    Private userInput As String = PoliKlinik.Lbluser.Text

    Private Sub FormPasienBooking_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.DesignMode Then Exit Sub
        SetupDataGridView()
        LoadDataBookingAuto()
    End Sub

    Private Sub LoadDataBookingAuto()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim baseUrl As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'ws_RM' ")
            If String.IsNullOrEmpty(baseUrl) Then
                MessageBox.Show("Base URL 'ws_RM' belum dikonfigurasi di MsConfig.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If Not baseUrl.EndsWith("/") Then baseUrl &= "/"

            Dim fullUri As String = String.Format("{0}api/clinic-management/today-checkins", baseUrl)
            Dim jsonResult As String = GetDataAPIRM(fullUri)

            If String.IsNullOrEmpty(jsonResult) Then Exit Sub

            Dim apiRes As CheckinApiResponse = JsonConvert.DeserializeObject(Of CheckinApiResponse)(jsonResult)

            If apiRes IsNot Nothing AndAlso apiRes.status = "success" Then
                Dim rawDataFromAPI As List(Of PasienCheckinItem) = apiRes.data
                TampilkanKeGrid(If(rawDataFromAPI, New List(Of PasienCheckinItem)()))
            Else
                Dim msg As String = If(apiRes IsNot Nothing, apiRes.message, "Gagal mengambil data dari server.")
                MessageBox.Show("Gagal mengambil data: " & msg, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat memuat data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TampilkanKeGrid(ByVal listData As List(Of PasienCheckinItem))
        DataGridView.DataSource = Nothing
        DataGridView.Rows.Clear()

        If listData IsNot Nothing AndAlso listData.Count > 0 Then
            For Each item In listData
                Dim idx As Integer = DataGridView.Rows.Add()
                Dim row As DataGridViewRow = DataGridView.Rows(idx)

                row.Cells("colSelect").Value = False
                row.Cells("booking_id").Value = item.booking_id
                row.Cells("patient_name").Value = item.patient_name
                row.Cells("patient_phone").Value = item.patient_phone
                row.Cells("clinic_name").Value = item.clinic_name
                row.Cells("room_name").Value = item.room_name
                row.Cells("doctor_name").Value = item.doctor_name
                row.Cells("nurse_name").Value = item.nurse_name
                row.Cells("nurse_code").Value = item.nurse_code
                row.Cells("treatment_name").Value = item.treatment_name
                row.Cells("start_time").Value = item.start_time
                row.Cells("checked_in_at").Value = item.checked_in_at

                row.Tag = item
            Next
        End If
    End Sub

    Private Sub SetupDataGridView()
        With DataGridView
            .Columns.Clear()
            .AutoGenerateColumns = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ReadOnly = False ' Di-set False agar Checkbox bisa di-centang oleh user
            .AllowUserToAddRows = False
            .MultiSelect = False
            .RowHeadersVisible = False

            ' 1. Kolom Checkbox untuk Seleksi Pasien
            Dim chkCol As New DataGridViewCheckBoxColumn()
            chkCol.Name = "colSelect"
            chkCol.HeaderText = "Pilih"
            chkCol.Width = 45
            chkCol.ReadOnly = False
            .Columns.Add(chkCol)

            ' 2. Kolom ID Booking (Hidden / Tersembunyi)
            .Columns.Add("booking_id", "ID")
            .Columns("booking_id").DataPropertyName = "booking_id"
            .Columns("booking_id").Visible = False ' Disembunyikan dari layar
            .Columns("booking_id").ReadOnly = True

            ' 3. Kolom Informasi Pasien & Pelayanan (Read Only)
            .Columns.Add("patient_name", "Nama Pasien")
            .Columns("patient_name").DataPropertyName = "patient_name"
            .Columns("patient_name").Width = 150
            .Columns("patient_name").ReadOnly = True

            .Columns.Add("patient_phone", "No. HP")
            .Columns("patient_phone").DataPropertyName = "patient_phone"
            .Columns("patient_phone").Width = 110
            .Columns("patient_phone").ReadOnly = True

            .Columns.Add("clinic_name", "Poli / Klinik")
            .Columns("clinic_name").DataPropertyName = "clinic_name"
            .Columns("clinic_name").Width = 130
            .Columns("clinic_name").ReadOnly = True

            .Columns.Add("room_name", "Ruangan")
            .Columns("room_name").DataPropertyName = "room_name"
            .Columns("room_name").Width = 90
            .Columns("room_name").ReadOnly = True

            .Columns.Add("doctor_name", "Nama Dokter")
            .Columns("doctor_name").DataPropertyName = "doctor_name"
            .Columns("doctor_name").Width = 180
            .Columns("doctor_name").ReadOnly = True

            .Columns.Add("nurse_name", "Perawat")
            .Columns("nurse_name").DataPropertyName = "nurse_name"
            .Columns("nurse_name").Width = 130
            .Columns("nurse_name").ReadOnly = True

            .Columns.Add("nurse_code", "Kode Perawat")
            .Columns("nurse_code").DataPropertyName = "nurse_code"
            .Columns("nurse_code").Width = 90
            .Columns("nurse_code").ReadOnly = True

            .Columns.Add("treatment_name", "Nama Tindakan")
            .Columns("treatment_name").DataPropertyName = "treatment_name"
            .Columns("treatment_name").Width = 150
            .Columns("treatment_name").ReadOnly = True

            .Columns.Add("start_time", "Jam Sesi")
            .Columns("start_time").DataPropertyName = "start_time"
            .Columns("start_time").Width = 80
            .Columns("start_time").ReadOnly = True

            .Columns.Add("checked_in_at", "Jam Check-In")
            .Columns("checked_in_at").DataPropertyName = "checked_in_at"
            .Columns("checked_in_at").Width = 90
            .Columns("checked_in_at").ReadOnly = True
        End With
    End Sub

    Private Sub ButtonSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSubmit.Click
        If DataGridView Is Nothing OrElse DataGridView.Rows.Count = 0 Then
            MessageBox.Show("Tidak ada data booking untuk diproses!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Akhiri mode edit pada DataGridView
        DataGridView.EndEdit()

        Dim listSelectedItems As New List(Of PasienCheckinItem)()

        For Each row As DataGridViewRow In DataGridView.Rows
            If Not row.IsNewRow Then
                Dim isChecked As Boolean = False
                Dim valCheck As Object = row.Cells("colSelect").Value

                If valCheck IsNot Nothing AndAlso Not IsDBNull(valCheck) Then
                    Boolean.TryParse(valCheck.ToString(), isChecked)
                End If

                If isChecked Then
                    ' Ambil objek pasien dari Tag
                    If row.Tag IsNot Nothing AndAlso TypeOf row.Tag Is PasienCheckinItem Then
                        Dim itemData As PasienCheckinItem = CType(row.Tag, PasienCheckinItem)
                        listSelectedItems.Add(itemData)
                    End If
                End If
            End If
        Next

        If listSelectedItems.Count = 0 Then
            MessageBox.Show("Silakan centang/pilih minimal satu pasien check-in yang akan diproses!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim confirm As DialogResult = MessageBox.Show("Apakah Anda yakin ingin memproses " & listSelectedItems.Count & " pasien terpilih ke Database Lokal?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm = DialogResult.Yes Then
            SimpanBulkKeDatabaseLokal(listSelectedItems)
        End If
    End Sub

    Private Sub DataGridView_CellContentClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DataGridView.CellContentClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = DataGridView.Columns("colSelect").Index Then
            DataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub SimpanBulkKeDatabaseLokal(ByVal items As List(Of PasienCheckinItem))
        Dim totalBerhasil As Integer = 0
        Dim totalPasienBaru As Integer = 0
        Dim totalStatusWebUpdated As Integer = 0

        Try
            ' Mengambil user login aktif secara aman
            Dim userCurrent As String = "BRIDGING_API_OKUPANSI"
            If PoliKlinik IsNot Nothing AndAlso PoliKlinik.Lbluser IsNot Nothing AndAlso Not String.IsNullOrEmpty(PoliKlinik.Lbluser.Text) Then
                userCurrent = PoliKlinik.Lbluser.Text.Trim()
            End If

            For Each item In items
                ' 1. Formatting Nomor Telepon
                Dim noHpFormatted As String = FormatNoTelepon(item.patient_phone)
                Dim valTglTransaksi As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                Dim dtTx As DateTime = DateTime.Now

                ' Sanitasi string nama
                Dim namaSanitized As String = If(Not String.IsNullOrEmpty(item.patient_name), item.patient_name.Trim().Replace("'", "''"), "")

                ' 2. Cek ke MasterPasien (NoHandphone AND Nama LIKE)
                Dim queryCek As String = "SELECT TOP 1 NoRekamMedis FROM MasterPasien " & _
                                         "WHERE NoHandphone = '" & noHpFormatted.Replace("'", "''") & "' " & _
                                         "AND Nama LIKE '%" & namaSanitized & "%'"
                Dim tblPasien As DataTable = ReadSQL(queryCek)

                Dim noRekamMedisLokal As String = ""

                If tblPasien IsNot Nothing AndAlso tblPasien.Rows.Count > 0 Then
                    Dim rowPasien As DataRow = tblPasien.Rows(0)
                    noRekamMedisLokal = rowPasien("NoRekamMedis").ToString()
                Else
                    noRekamMedisLokal = generateNomorRekamMedis()
                    Dim noNIKBaru As String = generateNomorNIK()
                    Dim userInputPasien As String = "Sistem_API"

                    Dim queryInsertPasien As String = _
                        "INSERT INTO MasterPasien (NoRekamMedis, NoNIK, Nama, NoHandphone, PasienTanpaNIK, TglLahir, JenisKelamin, DomisiliAlamat, CreatedBy, CreatedDate) " & _
                        "VALUES (@noRekamMedis, @noNIK, @namaPasien, @noHandphone, 1, '1900-01-01', '-', '-', @userInput, @tglTransaksi)"

                    WriteParameterizedLokalSQL(queryInsertPasien, 4, _
                         New SqlParameter("@noRekamMedis", noRekamMedisLokal), _
                         New SqlParameter("@noNIK", noNIKBaru), _
                         New SqlParameter("@namaPasien", item.patient_name), _
                         New SqlParameter("@noHandphone", noHpFormatted), _
                         New SqlParameter("@userInput", userInputPasien), _
                         New SqlParameter("@tglTransaksi", valTglTransaksi))

                    totalPasienBaru += 1
                End If

                ' 3. INSERT TransaksiPoli
                Dim kodeTransaksi As String = "U"
                Dim jenisPoli As String = If(String.IsNullOrEmpty(item.clinic_name), "POLI UMUM", item.clinic_name)
                Dim tglFormat As String = dtTx.ToString("yyyyMMdd")
                Dim isMCU As Boolean = False

                Dim kodeDokterLokal As String = "-"
                If Not String.IsNullOrEmpty(item.doctor_name) Then
                    Dim queryCekDokter As String = "SELECT TOP 1 ID FROM MasterDokter WHERE Nama = '" & item.doctor_name.Replace("'", "''") & "'"
                    Dim tblDokter As DataTable = ReadSQL(queryCekDokter)

                    If tblDokter IsNot Nothing AndAlso tblDokter.Rows.Count > 0 Then
                        kodeDokterLokal = tblDokter.Rows(0)("ID").ToString()
                    End If
                End If

                Dim noUrutBaru As String = generateNoTransaksi(kodeTransaksi, jenisPoli, tglFormat, isMCU)

                Dim queryInsertPoli As String = _
                    "INSERT INTO TransaksiPoli (" & _
                    "   IDInternet, DokterPoli, KodeDokter, PerawatPoli, TglPoli, TotalTagihan, TotalDisc, Pembulatan, " & _
                    "   NettBayar, TotalBayar, Pengembalian, ClosedBayar, ClosedTransaksi, " & _
                    "   CreatedBy, CreatedDate, NoUrut, Status, StatusKeterangan, StatusTransaksi, ArrivedDate" & _
                    ") VALUES (" & _
                    "   @idInternet, @dokterPoli, @kodeDokter, @perawatPoli, @tglTransaksi, 0, 0, 0, " & _
                    "   0, 0, 0, '9998-12-31', 0, " & _
                    "   @createdBy, @tglTransaksi, @noUrut, 'OPEN', 'Belum Diperiksa', 'arrived', @tglTransaksi" & _
                    ")"

                WriteParameterizedLokalSQL(queryInsertPoli, 4, _
                     New SqlParameter("@idInternet", noRekamMedisLokal), _
                     New SqlParameter("@dokterPoli", item.doctor_name), _
                     New SqlParameter("@kodeDokter", kodeDokterLokal), _
                     New SqlParameter("@perawatPoli", item.nurse_name), _
                     New SqlParameter("@createdBy", userCurrent), _
                     New SqlParameter("@noUrut", noUrutBaru), _
                     New SqlParameter("@tglTransaksi", valTglTransaksi))

                ' 4. GET TransPoliID
                Dim transPoliID As String = ReadSQLNIK("SELECT TOP 1 TransPoliID FROM TransaksiPoli WHERE NoUrut = '" & noUrutBaru & "' ORDER BY TransPoliID DESC")

                If Not String.IsNullOrEmpty(transPoliID) Then
                    Dim valJenisPoli As String = If(Not String.IsNullOrEmpty(item.clinic_name), item.clinic_name.Trim().ToUpper(), "-")
                    Dim penjaminLokal As String = "Tanpa Asuransi / Umum"
                    Dim jenisPenagihanLokal As String = "UMUM"
                    Dim noJenisBayarLokal As String = "UMUM-C0001"

                    If valJenisPoli.Contains("KECANTIKAN") Then
                        penjaminLokal = "La Lumiere Wellness"
                        jenisPenagihanLokal = "Asuransi Lain"
                        noJenisBayarLokal = "La Lumiere Wellness-C1488"
                    ElseIf valJenisPoli.Contains("FISIOTERAPI") Then
                        penjaminLokal = "FISIOTERAPI"
                        jenisPenagihanLokal = "Asuransi Lain"
                        noJenisBayarLokal = "FISIOTERAPI-C1492"
                    ElseIf valJenisPoli.Contains("HBOT") OrElse valJenisPoli.Contains("HIPERBARIK") Then
                        penjaminLokal = "HIPERBARIK"
                        jenisPenagihanLokal = "Asuransi Lain"
                        noJenisBayarLokal = "HIPERBARIK-C1257"
                    End If

                    ' TransaksiDetail
                    Dim queryInsertDetail As String = _
                        "INSERT INTO TransaksiDetail (" & _
                        "   StatusBPJS, NoUrutBPJS, NoKunjunganBPJS, JenisKunjungan, JenisRawat, StatusPulang, " & _
                        "   NoTransaksi, JenisTransaksi, JenisPenagihan, TanggalTransaksi, IDInternet, " & _
                        "   KeluhanUtama, KeluhanTambahan, CreatedBy, CreatedDate, AntreanOnline, TransaksiPaket, " & _
                        "   Sumber, JenisPoli, NoUrut" & _
                        ") VALUES (" & _
                        "   0, '', '', 'Kunjungan Sakit', 'Rawat Jalan', 'Berobat Jalan', " & _
                        "   @noTransaksi, 'POLI', @jenisPenagihan, @tglTransaksi, @idInternet, " & _
                        "   'KONSULTASI', 'TIDAK ADA KELUHAN TAMBAHAN', @createdBy, @tglTransaksi, 0, 0, " & _
                        "   'Bridging_Clinic_Web', @jenisPoli, @noUrut" & _
                        ")"

                    WriteParameterizedLokalSQL(queryInsertDetail, 4, _
                         New SqlParameter("@noTransaksi", transPoliID), _
                         New SqlParameter("@idInternet", noRekamMedisLokal), _
                         New SqlParameter("@createdBy", userCurrent), _
                         New SqlParameter("@jenisPenagihan", jenisPenagihanLokal), _
                         New SqlParameter("@jenisPoli", valJenisPoli), _
                         New SqlParameter("@noUrut", noUrutBaru), _
                         New SqlParameter("@tglTransaksi", valTglTransaksi))

                    ' TransaksiPoliDetail
                    Dim queryInsertPoliDetail As String = _
                         "INSERT INTO TransaksiPoliDetail (" & _
                         "   TransPoliID, TindakanID, PaketID, NamaTindakan, JenisTindakan, " & _
                         "   BiayaTindakan, DiscTindakan, NettBayar, Penjamin, Hasil, " & _
                         "   admin, transaksitime, DiscPersenTindakan, Awal, Upload, " & _
                         "   KodeVoucher, StatusPembayaran" & _
                         ") VALUES (" & _
                         "   @transPoliID, '256', '0', 'KONSULTASI FREE', 'Konsultasi Dokter Jaminan', " & _
                         "   0, 0, 0, @penjamin, '-', " & _
                         "   @admin, @tglTransaksi, 0, '1', '0', " & _
                         "   '-', 'PAID'" & _
                         ")"

                    WriteParameterizedLokalSQL(queryInsertPoliDetail, 4, _
                         New SqlParameter("@transPoliID", transPoliID), _
                         New SqlParameter("@penjamin", penjaminLokal), _
                         New SqlParameter("@admin", userCurrent), _
                         New SqlParameter("@tglTransaksi", valTglTransaksi))

                    ' 5. TransaksiTindakanPerencanaan
                    If Not String.IsNullOrEmpty(item.treatment_name) Then
                        Dim tindakanIDLokal As String = "0"
                        Dim biayaTindakan As Decimal = 0
                        Dim discTindakan As Decimal = 0
                        Dim nettBayar As Decimal = 0

                        Dim queryCekTindakan As String = "SELECT TOP 1 TindakanID, BiayaTindakan, DiscTindakan, NettBayar " & _
                                                         "FROM MasterTindakan " & _
                                                         "WHERE NamaTindakan = '" & item.treatment_name.Replace("'", "''") & "'"
                        Dim tblTindakan As DataTable = ReadSQL(queryCekTindakan)

                        If tblTindakan IsNot Nothing AndAlso tblTindakan.Rows.Count > 0 Then
                            Dim rTnd As DataRow = tblTindakan.Rows(0)
                            tindakanIDLokal = rTnd("TindakanID").ToString()
                            Decimal.TryParse(rTnd("BiayaTindakan").ToString(), biayaTindakan)
                            Decimal.TryParse(rTnd("DiscTindakan").ToString(), discTindakan)
                            Decimal.TryParse(rTnd("NettBayar").ToString(), nettBayar)
                        End If

                        Dim queryInsertPerencanaan As String = _
                            "INSERT INTO TransaksiTindakanPerencanaan (" & _
                            "   NoRM, NoTransaksi, NamaTindakan, TindakanID, JenisTindakan, " & _
                            "   BiayaTindakan, DiscTindakan, NettBayar, Penjamin, StatusPembayaran, " & _
                            "   StatusDipakai, CreatedBy, CreatedDate" & _
                            ") VALUES (" & _
                            "   @noRM, @noTransaksi, @namaTindakan, @tindakanID, 'Tindakan', " & _
                            "   @biayaTindakan, @discTindakan, @nettBayar, @penjamin, 'PENDING', " & _
                            "   0, @createdBy, GETDATE()" & _
                            ")"

                        WriteParameterizedLokalSQL(queryInsertPerencanaan, 4, _
                             New SqlParameter("@noRM", noRekamMedisLokal), _
                             New SqlParameter("@noTransaksi", noUrutBaru), _
                             New SqlParameter("@namaTindakan", item.treatment_name), _
                             New SqlParameter("@tindakanID", tindakanIDLokal), _
                             New SqlParameter("@biayaTindakan", biayaTindakan), _
                             New SqlParameter("@discTindakan", discTindakan), _
                             New SqlParameter("@nettBayar", nettBayar), _
                             New SqlParameter("@penjamin", penjaminLokal), _
                             New SqlParameter("@createdBy", userCurrent))
                    End If

                    ' 6. TransaksiPoliBayarAwal
                    Dim queryInsertBayarAwal As String = _
                        "INSERT INTO TransaksiPoliBayarAwal (" & _
                        "   TransPoliID, JenisBayar, NoJenisBayar, JumlahBayar, admin, " & _
                        "   transaksitime, kembali, TransJenis" & _
                        ") VALUES (" & _
                        "   @transPoliID, 'TAGIHAN', @noJenisBayar, 0, @createdBy, " & _
                        "   @tglTransaksi, 0, 'POLI'" & _
                        ")"

                    WriteParameterizedLokalSQL(queryInsertBayarAwal, 4, _
                         New SqlParameter("@transPoliID", transPoliID), _
                         New SqlParameter("@noJenisBayar", noJenisBayarLokal), _
                         New SqlParameter("@tglTransaksi", valTglTransaksi), _
                         New SqlParameter("@createdBy", userCurrent))
                End If

                ' =========================================================================
                ' 7. UPDATE STATUS ATTENDANCE KE WEB (SETelah LOKAL SUKSES TERSIMPAN)
                ' =========================================================================
                Dim isWebUpdated As Boolean = UpdateStatusBookingKeWeb(item.booking_id)
                If isWebUpdated Then
                    totalStatusWebUpdated += 1
                End If

                totalBerhasil += 1
            Next

            MessageBox.Show("Berhasil memproses " & totalBerhasil & " data kunjungan pasien!" & vbCrLf & _
                            "(Pasien Baru Didaftarkan: " & totalPasienBaru & ")" & vbCrLf & _
                            "(Status Web Diubah Ke Completed: " & totalStatusWebUpdated & ")", _
                            "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Refresh Tampilan Grid
            LoadDataBookingAuto()

        Catch ex As Exception
            MessageBox.Show("Gagal menyimpan data ke Database Lokal: " & ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function UpdateStatusBookingKeWeb(ByVal bookingId As Integer) As Boolean
        Try
            ' Ambil Base URL dari MsConfig
            Dim baseUrl As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'ws_RM' ")
            If String.IsNullOrEmpty(baseUrl) Then Return False

            If Not baseUrl.EndsWith("/") Then baseUrl &= "/"

            ' Endpoint API pembaruan status
            Dim fullUri As String = String.Format("{0}api/clinic-management/update-attendance-status", baseUrl)

            ' CARA AMAN VB.NET: Gunakan JObject untuk membuat JSON Payload
            Dim payloadObj As New JObject()
            payloadObj("booking_id") = bookingId
            payloadObj("attendance_status") = "completed"

            ' Convert ke string JSON
            Dim jsonPayload As String = payloadObj.ToString(Formatting.None)

            ' Eksekusi POST menggunakan fungsi yang sudah Anda miliki
            Dim jsonResponse As String = InsertDataAPIRMOld(jsonPayload, fullUri)

            If String.IsNullOrEmpty(jsonResponse) Then Return False

            ' Parse respon
            Dim jObj As JObject = JObject.Parse(jsonResponse)
            If jObj("status") IsNot Nothing AndAlso jObj("status").ToString().ToLower() = "success" Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function FormatNoTelepon(ByVal rawPhone As String) As String
        If String.IsNullOrEmpty(rawPhone) Then Return ""

        Dim phone As String = rawPhone.Trim()

        ' Jika tidak diawali angka 0, tambahkan 0 di depan
        If Not phone.StartsWith("0") Then
            phone = "0" & phone
        End If

        Return phone
    End Function
End Class