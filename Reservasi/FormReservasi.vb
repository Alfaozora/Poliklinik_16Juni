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
Imports DevExpress.XtraReports.UI
Imports System.Data.SqlClient
Imports System.Drawing.Imaging
Imports DevExpress.XtraPrinting

Public Class FormReservasi
    Private formName As String = "RESERVASI"
    Private userInput As String = PoliKlinik.Lbluser.Text
    'Private kdProviderLokal As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'KodePPK'")
    'Private urlPcare As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName='pcare_base_url'")

    Private Sub PanelTop_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PanelTop.Paint
        Try
            e.Graphics.DrawString(formName, New Font("Century Gothic", 16, FontStyle.Bold), Brushes.White, New Point(10, 3))
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Warning!")
        End Try
    End Sub

    Private Sub LoadReservasi()
        Dim query As String = "SELECT JR.JadwalID, R.NamaRuangan, JR.TglMulai, JR.TglAkhir, JR.NamaPasien, JR.NoHP, JR.Catatan, JR.Status " & _
                              "FROM JadwalRuangan JR " & _
                              "INNER JOIN MasterRuangan R ON JR.RuanganID = R.RuanganID " & _
                              "WHERE JR.TglAkhir > GETDATE() " & _
                              "ORDER BY JR.TglMulai DESC"

        Dim dt As DataTable = ReadSQL(query)

        ' Tambahkan kolom format Indonesia\
        dt.Columns.Add("Nama Ruangan", GetType(String))
        dt.Columns.Add("Nama Pasien", GetType(String))
        dt.Columns.Add("No HP", GetType(String))
        dt.Columns.Add("Tgl Reservasi", GetType(String))
        dt.Columns.Add("Sampai", GetType(String))
        dt.Columns.Add("Keterangan", GetType(String))
        dt.Columns.Add("Status Pasien", GetType(String))

        ' Konversi setiap baris ke format Indonesia
        For Each row As DataRow In dt.Rows
            Dim ruangan As String = If(IsDBNull(row("NamaRuangan")), "", row("NamaRuangan").ToString())
            Dim namaPasien As String = If(IsDBNull(row("NamaPasien")), "", row("NamaPasien").ToString())
            Dim noHp As String = If(IsDBNull(row("NoHP")), "", row("NoHP").ToString())
            Dim catatan As String = If(IsDBNull(row("Catatan")), "", row("Catatan").ToString())
            Dim status As String = If(IsDBNull(row("Status")), "", row("Status").ToString())

            Dim tgl As DateTime = If(IsDBNull(row("TglMulai")), DateTime.MinValue, CDate(row("TglMulai")))
            Dim tglAkhir As DateTime = If(IsDBNull(row("TglAkhir")), DateTime.MinValue, CDate(row("TglAkhir")))

            row("Tgl Reservasi") = If(tgl = DateTime.MinValue, "", tgl.ToString("dddd, dd MMMM yyyy HH:mm", New Globalization.CultureInfo("id-ID")))
            row("Sampai") = If(tglAkhir = DateTime.MinValue, "", tglAkhir.ToString("dddd, dd MMM yyyy HH:mm", New Globalization.CultureInfo("id-ID")))
            row("Nama Pasien") = namaPasien
            row("Nama Ruangan") = ruangan
            row("No HP") = noHp
            row("Keterangan") = catatan
            row("Status Pasien") = status
        Next

        For Each row As DataGridViewRow In DataGridViewReservasi.Rows
            If row.Cells("Status Pasien").Value.ToString().ToUpper() = "CANCEL" Then
                row.Cells("Reminder").Style.ForeColor = Color.Gray
                row.Cells("UbahStatus").Style.ForeColor = Color.Gray
                row.Cells("Reminder").Style.SelectionForeColor = Color.Gray
                row.Cells("UbahStatus").Style.SelectionForeColor = Color.Gray
            End If
        Next

        DataGridViewReservasi.DataSource = dt

        If DataGridViewReservasi.Columns.Contains("JadwalID") Then
            DataGridViewReservasi.Columns("JadwalID").Visible = False
            DataGridViewReservasi.Columns("NamaPasien").Visible = False
            DataGridViewReservasi.Columns("TglMulai").Visible = False
            DataGridViewReservasi.Columns("TglAkhir").Visible = False
            DataGridViewReservasi.Columns("NamaRuangan").Visible = False
            DataGridViewReservasi.Columns("NoHP").Visible = False
            DataGridViewReservasi.Columns("Catatan").Visible = False
            DataGridViewReservasi.Columns("Status").Visible = False
        End If

        TambahKolomAksi()
    End Sub

    Private Sub DataGridViewReservasi_RowPrePaint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridViewReservasi.RowPrePaint
        Dim row As DataGridViewRow = DataGridViewReservasi.Rows(e.RowIndex)
        Dim statusCell As DataGridViewCell = row.Cells("Status") ' Ganti "Status" dengan nama kolom yang sesuai

        If statusCell.Value IsNot Nothing AndAlso statusCell.Value.ToString() = "CANCEL" Then
            row.DefaultCellStyle.BackColor = Color.FromArgb(242, 222, 222)
            row.DefaultCellStyle.ForeColor = Color.Black
        ElseIf statusCell.Value IsNot Nothing AndAlso statusCell.Value.ToString() = "RESCHEDULE" Then
            row.DefaultCellStyle.BackColor = Color.FromArgb(184, 232, 252)
            row.DefaultCellStyle.ForeColor = Color.Black
        ElseIf statusCell.Value IsNot Nothing AndAlso statusCell.Value.ToString() = "TERJADWAL" Then
            row.DefaultCellStyle.BackColor = Color.FromArgb(195, 237, 192)
            row.DefaultCellStyle.ForeColor = Color.Black
        End If
    End Sub

    Public Function SetParameterWhatsappText(ByVal nomor As String, ByVal pesan As String) As String
        ' Bersihkan format pesan: ganti newline jadi \n, escape kutip dua
        Dim cleanPesan As String = pesan.Replace(vbCrLf, "\n").Replace("""", "\""")

        ' Buat JSON manual
        Dim json As String = "{" & _
            """number"": """ & nomor & """," & _
            """message"": """ & cleanPesan & """" & _
        "}"

        Return json
    End Function

    Public Sub KirimReminderReservasiWhatsApp(ByVal namaPasien As String, ByVal noHP As String, ByVal tglReservasi As DateTime, ByVal ruangan As String)
        Try
            If Strings.Left(noHP, 2) = "08" Then
                noHP = "62" & noHP.Substring(1)
            End If

            Dim waStatus As String = ReadSQLNIK("SELECT StrValue FROM msconfig WHERE ConfigName = 'whatsapp_klinik_status'")
            Dim waIP As String = ReadSQLNIK("SELECT StrValue FROM msconfig WHERE ConfigName = 'whatsapp_klinik_ip'")
            Dim lokasiKlinik As String = ReadSQLNIK("SELECT StrValue FROM msconfig WHERE ConfigName = 'InvoiceTempat_Klinik'")

            If waStatus <> "Yes" Then
                MsgBox("WhatsApp belum disetting. Hubungi IT.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            Dim tanggalIndo As String = tglReservasi.ToString("dddd, dd MMMM yyyy", New Globalization.CultureInfo("id-ID"))

            Dim pesan As String = "Halo " & namaPasien & "," & vbCrLf & vbCrLf & _
                                  "Pengingat reservasi Anda di " & lokasiKlinik & "." & vbCrLf & _
                                  "Tanggal: " & tanggalIndo & vbCrLf & _
                                  "Jam: " & tglReservasi.ToString("HH:mm") & " WIB" & vbCrLf & _
                                  "Ruangan: " & ruangan & vbCrLf & vbCrLf & _
                                  "Mohon datang tepat waktu." & vbCrLf & _
                                  "Terima kasih" & vbCrLf & vbCrLf & _
                                  "----- Pesan otomatis, mohon tidak dibalas -----"

            Dim parameter As String = SetParameterWhatsappText(noHP, pesan)
            Dim urlSend As String = "http://" & waIP & ":4444/send"
            Dim statusKirim As String = SendDataWhatsapp(parameter, urlSend)

            If statusKirim = "Success" Then
                MsgBox("Reminder berhasil dikirim ke " & noHP, MsgBoxStyle.Information)
            Else
                MsgBox("Gagal mengirim WhatsApp. Periksa koneksi atau server WhatsApp.", MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception
            MsgBox("Terjadi kesalahan saat kirim reminder: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private selectedJadwalID As Integer = 0

    Private Sub DataGridViewReservasi_CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DataGridViewReservasi.CellClick
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub

        Dim statusPasien As String = DataGridViewReservasi.Rows(e.RowIndex).Cells("Status Pasien").Value.ToString().ToUpper()
        Dim columnName As String = DataGridViewReservasi.Columns(e.ColumnIndex).Name

        If statusPasien = "CANCEL" AndAlso (columnName = "Reminder" Or columnName = "UbahStatus") Then Exit Sub

        If columnName = "Reminder" Then
            Dim namaPasien As String = DataGridViewReservasi.Rows(e.RowIndex).Cells("NamaPasien").Value.ToString()
            Dim noHP As String = DataGridViewReservasi.Rows(e.RowIndex).Cells("NoHP").Value.ToString()
            Dim tglMulai As DateTime = CDate(DataGridViewReservasi.Rows(e.RowIndex).Cells("TglMulai").Value)

            ' Ambil JadwalID dari kolom tersembunyi
            Dim idJadwal As Integer = CInt(DataGridViewReservasi.Rows(e.RowIndex).Cells("JadwalID").Value)

            ' Ambil Nama Ruangan dari JadwalID
            Dim namaRuangan As String = ReadSQLNIK("SELECT MR.NamaRuangan FROM JadwalRuangan JR " & _
                                               "INNER JOIN MasterRuangan MR ON JR.RuanganID = MR.RuanganID " & _
                                               "WHERE JR.JadwalID = " & idJadwal)

            ' Kirim WA reminder
            KirimReminderReservasiWhatsApp(namaPasien, noHP, tglMulai, namaRuangan)
        End If

        If columnName = "UbahStatus" Then
            ' Ambil nilai JadwalID dari baris yang diklik
            selectedJadwalID = CInt(DataGridViewReservasi.Rows(e.RowIndex).Cells("JadwalID").Value)

            ' Tampilkan ContextMenu di posisi mouse
            ContextMenuStatus.Show(Cursor.Position)
        End If
    End Sub

    Private Sub CancelToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CancelToolStripMenuItem.Click
        Try
            Dim query As String = "UPDATE JadwalRuangan SET " & _
                              "Status = 'CANCEL', " & _
                              "DeletedBy = '" & userInput & "', " & _
                              "DeletedDate = GETDATE() " & _
                              "WHERE JadwalID = " & selectedJadwalID
            WriteSQL(query, 4)

            MsgBox("Status diubah menjadi CANCEL", MsgBoxStyle.Information)
            LoadReservasi()
        Catch ex As Exception
            MsgBox("Status gagal diubah", MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub RescheduleToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles RescheduleToolStripMenuItem.Click
        Try
            Dim dlg As New Dialog1()

            If dlg.ShowDialog() = DialogResult.OK Then
                Dim mulaiBaru As DateTime = dlg.TglMulaiBaru
                Dim akhirBaru As DateTime = dlg.TglAkhirBaru

                ' Update status, TglMulai, TglAkhir, ChangedBy dan ChangedDate
                Dim query As String = "UPDATE JadwalRuangan SET " & _
                                      "Status = 'RESCHEDULE', " & _
                                      "TglMulai = '" & mulaiBaru.ToString("yyyy-MM-dd HH:mm") & "', " & _
                                      "TglAkhir = '" & akhirBaru.ToString("yyyy-MM-dd HH:mm") & "', " & _
                                      "ChangedBy = '" & userInput & "', " & _
                                      "ChangedDate = GETDATE() " & _
                                      "WHERE JadwalID = " & selectedJadwalID

                WriteSQL(query, 4)

                MsgBox("Status diubah menjadi RESCHEDULE dan jadwal diperbarui.", MsgBoxStyle.Information)
                LoadReservasi()
            End If
        Catch ex As Exception
            MsgBox("Status gagal diubah", MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub TambahKolomAksi()
        ' Bersihkan kolom button yang sudah ada
        For i As Integer = DataGridViewReservasi.Columns.Count - 1 To 0 Step -1
            If TypeOf DataGridViewReservasi.Columns(i) Is DataGridViewButtonColumn Then
                DataGridViewReservasi.Columns.RemoveAt(i)
            End If
        Next

        ' Tambah tombol Reminder
        Dim btnReminder As New DataGridViewButtonColumn()
        btnReminder.Name = "Reminder"
        btnReminder.HeaderText = "Reminder"
        btnReminder.Text = "Kirim"
        btnReminder.UseColumnTextForButtonValue = True
        btnReminder.Width = 100
        btnReminder.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        btnReminder.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewReservasi.Columns.Add(btnReminder)

        ' Tambah tombol Status
        Dim btnStatus As New DataGridViewButtonColumn()
        btnStatus.Name = "UbahStatus"
        btnStatus.HeaderText = "Status"
        btnStatus.Text = "Ubah"
        btnStatus.UseColumnTextForButtonValue = True
        btnStatus.Width = 100
        btnStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        btnStatus.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewReservasi.Columns.Add(btnStatus)

        ' Besarkan tinggi baris agar tombol terlihat lebih besar
        DataGridViewReservasi.RowTemplate.Height = 35
    End Sub

    Private Sub LoadRuangan()
        Dim dt As DataTable = ReadSQL("SELECT RuanganID, NamaRuangan FROM MasterRuangan")
        ComboBoxRuangan.DataSource = dt
        ComboBoxRuangan.DisplayMember = "NamaRuangan"
        ComboBoxRuangan.ValueMember = "RuanganID"
    End Sub

    Private Sub FormReservasi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load 
        Try
            Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
            Me.MdiParent = PoliKlinik
            Me.Dock = DockStyle.Fill

            ' Hide tab header button
            'TabControl1.Appearance = TabAppearance.FlatButtons
            TabControlMenu.ItemSize = New Size(0, 1)
            'TabControl1.SizeMode = TabSizeMode.Fixed

            LoadRuangan()
            LoadReservasi()
            TambahKolomAksi()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Warning!")
        End Try
    End Sub

    Private Sub btnMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMenu4.Click, btnMenu3.Click, btnMenu2.Click, btnMenu1.Click
        Try
            Dim myButton = DirectCast(sender, Button)

            Select Case myButton.Name
                Case "btnMenu1"
                    TabControlMenu.SelectedTab = TabPage1
                Case "btnMenu2"
                    TabControlMenu.SelectedTab = TabPage2
                Case "btnMenu3"
                    TabControlMenu.SelectedTab = TabPage3
                Case "btnMenu4"
                    TabControlMenu.SelectedTab = TabPage4
            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Warning!")
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Public Function ReadSQLValue(ByVal sql As String) As Object
        Dim dt As DataTable = ReadSQL(sql)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)(0)
        Else
            Return Nothing
        End If
    End Function

    Private Function IsJadwalTersedia(ByVal ruanganId As Integer, ByVal mulai As DateTime, ByVal selesai As DateTime) As Boolean
        Dim namaRuangan As Object = ReadSQLNIK("SELECT NamaRuangan FROM MasterRuangan WHERE RuanganID = " & ruanganId)

        ' Cek jika hasil NULL
        If namaRuangan Is Nothing OrElse IsDBNull(namaRuangan) Then
            MsgBox("Nama ruangan tidak ditemukan.", MsgBoxStyle.Exclamation)
            Return False
        End If

        ' Jika HBOT, boleh overlap
        If namaRuangan.ToString().ToUpper().Contains("HBOT") Then
            Return True
        End If

        ' Cek bentrok waktu
        Dim query As String = "SELECT COUNT(*) FROM JadwalRuangan WHERE RuanganID = " & ruanganId & " AND " & _
                              "('" & mulai.ToString("yyyy-MM-dd HH:mm") & "' < TglAkhir AND '" & selesai.ToString("yyyy-MM-dd HH:mm") & "' > TglMulai)"

        ' Gunakan fungsi scalar yang benar
        Dim count As Integer = Val(ReadSQLValue(query))
        Return count = 0
    End Function

    Private Sub ButtonSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSimpan.Click
        Dim mulai As DateTime = dtReservasi.Value
        Dim selesai As DateTime = dtSelesai.Value
        Dim ruanganId As Integer = ComboBoxRuangan.SelectedValue
        Dim namaPasien As String = TextBoxNamaPasien.Text
        Dim keterangan As String = TextBoxCatatan.Text
        Dim NoHp As String = TextBoxNoHP.Text
        Dim sekarang As DateTime = DateTime.Now

        If namaPasien = "" Then
            MsgBox("Nama pasien tidak boleh kosong.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If NoHp = "" Then
            MsgBox("Nomor HP tidak boleh kosong.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If mulai.ToString("yyyy-MM-dd HH:mm") = sekarang.ToString("yyyy-MM-dd HH:mm") Then
            MsgBox("Waktu reservasi tidak boleh sama dengan waktu saat ini.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If selesai.ToString("yyyy-MM-dd HH:mm") = sekarang.ToString("yyyy-MM-dd HH:mm") Then
            MsgBox("Waktu selesai tidak boleh sama dengan waktu saat ini.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If Not IsJadwalTersedia(ruanganId, mulai, selesai) Then
            MsgBox("Waktu sudah terisi. Silakan pilih waktu lain.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If mulai < DateTime.Now Then
            MsgBox("Waktu reservasi tidak boleh di masa lalu.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If selesai <= mulai Then
            MsgBox("Waktu selesai harus lebih besar dari waktu mulai.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        namaPasien = namaPasien.Replace("'", "''")
        NoHp = NoHp.Replace("'", "''")
        keterangan = keterangan.Replace("'", "''")

        WriteSQL("INSERT INTO JadwalRuangan (RuanganID, Tanggal, TglMulai, TglAkhir, NamaPasien, NoHP, Catatan, Status, CreatedBy, CreatedDate) " & _
            "VALUES (" & ruanganId & ", " & _
             "GETDATE(), " & _
             "'" & mulai.ToString("yyyy-MM-dd HH:mm") & "', " & _
             "'" & selesai.ToString("yyyy-MM-dd HH:mm") & "', " & _
             "'" & namaPasien & "', " & _
             "'" & NoHp & "', " & _
             "'" & keterangan & "', " & _
             "'TERJADWAL', " & _
             "'" & userInput & "', " & _
             "GETDATE())", 4)

        MsgBox("Reservasi berhasil disimpan.", MsgBoxStyle.Information)

        ' Reset form
        TextBoxNamaPasien.Clear()
        TextBoxNoHP.Clear()
        TextBoxCatatan.Clear()
        dtReservasi.Value = DateTime.Now
        dtSelesai.Value = DateTime.Now.AddMinutes(30)
        LoadReservasi()
    End Sub

    Private Sub ButtonCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCari.Click
        Dim mulai As DateTime = dtReservasi.Value
        Dim selesai As DateTime = dtSelesai.Value
        Dim tersedia As Boolean = IsJadwalTersedia(ComboBoxRuangan.SelectedValue, mulai, selesai)

        If tersedia Then
            MsgBox("Jadwal Tersedia.", MsgBoxStyle.Information)
        Else
            MsgBox("Jadwal Sudah Terisi.", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Public Sub LoadPasien(ByVal Trow As DataRowView)
        'Set Data From Pencarian
        TextBoxNamaPasien.Text = Trow("Nama").ToString
        TextBoxNoHP.Text = Trow("No. HP").ToString
    End Sub

    Private Sub ButtonFindPasien_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFindPasien.Click
        FindPasien.SetForm("FormReservasi")
        FindPasien.ShowDialog()
    End Sub
End Class