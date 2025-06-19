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
        Dim query As String = "SELECT R.NamaRuangan AS [Nama Ruangan], JR.TglMulai AS [Tgl Mulai], JR.TglAkhir AS [Tgl Akhir], JR.NamaPasien AS [Nama Pasien], JR.NoHP AS [No HP], JR.Catatan, JR.Status " & _
                              "FROM JadwalRuangan JR " & _
                              "INNER JOIN MasterRuangan R ON JR.RuanganID = R.RuanganID " & _
                              "ORDER BY JR.TglMulai DESC"

        Dim dt As DataTable = ReadSQL(query)
        DataGridViewReservasi.DataSource = dt
    End Sub

    Private Sub TambahKolomAksi()
        ' Bersihkan dulu kolom tombol jika sudah ada
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
        DataGridViewReservasi.Columns.Add(btnReminder)

        ' Tambah tombol Status
        Dim btnStatus As New DataGridViewButtonColumn()
        btnStatus.Name = "UbahStatus"
        btnStatus.HeaderText = "Status"
        btnStatus.Text = "Ubah"
        btnStatus.UseColumnTextForButtonValue = True
        DataGridViewReservasi.Columns.Add(btnStatus)
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

    Private Function IsJadwalTersedia(ByVal ruanganId As Integer, ByVal mulai As DateTime, ByVal selesai As DateTime) As Boolean
        Dim query As String = "SELECT COUNT(*) AS Jumlah FROM JadwalRuangan " & _
                              "WHERE RuanganID = " & ruanganId & _
                              " AND ('" & mulai.ToString("yyyy-MM-dd HH:mm") & "' < TglAkhir AND '" & selesai.ToString("yyyy-MM-dd HH:mm") & "' > TglMulai)"

        Dim tbl As DataTable = ReadSQL(query)
        Dim count As Integer = 0

        If tbl.Rows.Count > 0 Then
            count = Val(tbl.Rows(0)("Jumlah").ToString())
        End If

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
End Class