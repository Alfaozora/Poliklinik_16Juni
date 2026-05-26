Public Class FormAdjustmentAlat

    Private userInput As String = PoliKlinik.Lbluser.Text

    Private isLoadingCombo As Boolean = False

    Private Sub FormAdjustmentAlat_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadComboAlat()
    End Sub

    Private Sub LoadComboAlat()
        isLoadingCombo = True

        Dim sql As String = "SELECT AlatID, NamaAlat FROM MasterAlat WHERE IsAktif = 1 ORDER BY NamaAlat"
        Dim dt As DataTable = ReadSQL(sql)

        ComboBoxAlat.DataSource = Nothing
        ComboBoxAlat.DisplayMember = "NamaAlat"
        ComboBoxAlat.ValueMember = "AlatID"
        ComboBoxAlat.DataSource = dt

        ' Set ke state kosong (belum ada yang dipilih)
        ComboBoxAlat.SelectedIndex = -1

        ' Kosongkan juga combo cartridge karena belum ada alat yang dipilih
        ComboBoxCartridge.DataSource = Nothing

        isLoadingCombo = False
    End Sub

    Private Sub ComboBoxAlat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxAlat.SelectedIndexChanged
        ' Validasi agar tidak error saat binding atau saat di-reset ke -1
        If isLoadingCombo Then Exit Sub
        If ComboBoxAlat.SelectedIndex = -1 Then Exit Sub
        If ComboBoxAlat.SelectedValue Is Nothing Then Exit Sub
        If TypeOf ComboBoxAlat.SelectedValue Is DataRowView Then Exit Sub
        If Not IsNumeric(ComboBoxAlat.SelectedValue) Then Exit Sub

        ' Ambil ID Alat yang dipilih
        Dim alatID As Integer = CInt(ComboBoxAlat.SelectedValue)

        ' Load Cartridge yang sesuai dengan Alat tersebut
        LoadComboCartridge(alatID)
    End Sub

    Private Sub LoadComboCartridge(ByVal alatID As Integer)
        ' Query diubah agar mengambil total aktual pemakaian dari LogPemakaianAlat
        Dim sql As String = _
            "SELECT CartridgeID, NamaCartridge, ISNULL(KuotaTotal, 0) AS KuotaTotal, " & _
            "ISNULL((SELECT SUM(ShootDipakai) FROM LogPemakaianAlat " & _
            "WHERE CartridgeID = MasterCartridge.CartridgeID AND Status = 1 AND IsVoid = 0), 0) AS KuotaTerpakai " & _
            "FROM MasterCartridge " & _
            "WHERE AlatID = " & alatID & " " & _
            "AND IsAktif = 1 " & _
            "ORDER BY NamaCartridge"

        Dim dt As DataTable = ReadSQL(sql)

        ComboBoxCartridge.DataSource = Nothing
        ComboBoxCartridge.DisplayMember = "NamaCartridge"
        ComboBoxCartridge.ValueMember = "CartridgeID"
        ComboBoxCartridge.DataSource = dt

        ComboBoxCartridge.SelectedIndex = -1
    End Sub

    Private Sub ComboBoxCartridge_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxCartridge.SelectedIndexChanged
        ' Validasi agar tidak error saat load awal atau form di-reset
        If isLoadingCombo Then Exit Sub

        ' Jika pilihan kosong, nol-kan textbox
        If ComboBoxCartridge.SelectedIndex = -1 Then
            TextBoxSaldoSistem.Text = "0"
            Exit Sub
        End If

        If ComboBoxCartridge.SelectedValue Is Nothing Then Exit Sub
        If TypeOf ComboBoxCartridge.SelectedValue Is DataRowView Then Exit Sub

        Try
            ' Mengambil objek baris (row) yang sedang dipilih di ComboBox
            Dim drv As DataRowView = DirectCast(ComboBoxCartridge.SelectedItem, DataRowView)

            ' Mengambil nilai kolom dari baris tersebut
            Dim kuotaTotal As Integer = CInt(drv("KuotaTotal"))
            Dim kuotaTerpakai As Integer = CInt(drv("KuotaTerpakai"))

            ' Menghitung saldo sistem
            Dim saldoSistem As Integer = kuotaTotal - kuotaTerpakai

            ' Menampilkan hasil perhitungan ke TextBox
            TextBoxSaldoSistem.Text = saldoSistem.ToString()

        Catch ex As Exception
            ' Biarkan kosong agar program tidak crash jika terjadi error 
            ' saat proses pemuatan awal (binding) data dari database.
        End Try
    End Sub

    Private Sub ButtonKalkulasi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKalkulasi.Click
        ' 1. Validasi Input
        If ComboBoxCartridge.SelectedIndex = -1 Then
            MsgBox("Pilih cartridge terlebih dahulu.", MsgBoxStyle.Exclamation, "Peringatan")
            Exit Sub
        End If

        If TextBoxSaldoFisik.Text.Trim() = "" Then
            MsgBox("Masukkan saldo fisik terlebih dahulu.", MsgBoxStyle.Exclamation, "Peringatan")
            TextBoxSaldoFisik.Focus()
            Exit Sub
        End If

        ' 2. Deklarasi Variabel
        Dim saldoSistem As Integer = 0
        Dim saldoFisik As Integer = 0

        ' 3. Konversi dan Validasi Angka (Cegah error jika diisi huruf)
        If Not Integer.TryParse(TextBoxSaldoSistem.Text, saldoSistem) Then
            saldoSistem = 0
        End If

        If Not Integer.TryParse(TextBoxSaldoFisik.Text, saldoFisik) Then
            MsgBox("Saldo fisik harus berupa angka bulat.", MsgBoxStyle.Exclamation, "Peringatan")
            TextBoxSaldoFisik.Focus()
            Exit Sub
        End If

        ' 4. Proses Kalkulasi Selisih
        Dim selisih As Integer = saldoFisik - saldoSistem

        ' 5. Tampilkan Hasil (Asumsi Anda menggunakan TextBoxSelisih)
        ' Jika selisih minus (-), berarti ada pemakaian yang belum tercatat (saldo berkurang)
        ' Jika selisih plus (+), berarti saldo fisik lebih banyak dari sistem
        TextBoxSelisih.Text = selisih.ToString()

        ' Opsional: Memberi warna teks agar user lebih awas
        If selisih < 0 Then
            TextBoxSelisih.ForeColor = Color.Red
        ElseIf selisih > 0 Then
            TextBoxSelisih.ForeColor = Color.Green
        Else
            TextBoxSelisih.ForeColor = Color.Black
            MsgBox("Saldo fisik dan sistem sudah sesuai (Selisih 0).", MsgBoxStyle.Information, "Info")
        End If
    End Sub

    Private Sub ButtonSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSimpan.Click
        ' ==========================================
        ' 1. VALIDASI INPUT
        ' ==========================================
        If ComboBoxCartridge.SelectedIndex = -1 Then
            MsgBox("Pilih cartridge terlebih dahulu.", MsgBoxStyle.Exclamation, "Peringatan")
            Exit Sub
        End If

        If TextBoxSelisih.Text.Trim() = "" Then
            MsgBox("Lakukan kalkulasi terlebih dahulu dengan menekan tombol Kalkulasi.", MsgBoxStyle.Exclamation, "Peringatan")
            Exit Sub
        End If

        If TextBoxKeterangan.Text.Trim() = "" Then
            MsgBox("Keterangan wajib diisi sebagai alasan penyesuaian (misal: kalibrasi, alat error, dsb).", MsgBoxStyle.Exclamation, "Peringatan")
            TextBoxKeterangan.Focus()
            Exit Sub
        End If

        ' ==========================================
        ' 2. AMBIL VARIABEL DARI FORM
        ' ==========================================
        Dim cartridgeID As Integer = CInt(ComboBoxCartridge.SelectedValue)
        Dim selisih As Integer = CInt(TextBoxSelisih.Text)

        ' Replace tanda petik tunggal untuk mencegah error SQL Injection
        Dim keterangan As String = TextBoxKeterangan.Text.Replace("'", "''")

        ' Jika tidak ada selisih, cegah proses simpan karena percuma
        If selisih = 0 Then
            MsgBox("Tidak ada selisih. Saldo fisik sudah sesuai dengan sistem.", MsgBoxStyle.Information, "Info")
            Exit Sub
        End If

        ' Konfirmasi sebelum menyimpan
        Dim tanya As DialogResult = MsgBox("Apakah Anda yakin ingin menyimpan penyesuaian saldo ini?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Konfirmasi")
        If tanya = DialogResult.No Then Exit Sub

        ' ==========================================
        ' 3. EKSEKUSI DATABASE
        ' ==========================================
        Dim dbTran As New DBTransaction

        Try
            dbTran.BeginTransaction()

            ' A. Simpan history ke tabel Log Adjustment
            dbTran.Execute( _
                "INSERT INTO LogAdjustmentCartridge (CartridgeID, TglAdjustment, QtyAdjustment, Keterangan, UserInput, CreatedAt) " & _
                "VALUES (" & cartridgeID & ", GETDATE(), " & selisih & ", '" & keterangan & "', '" & userInput & "', GETDATE())")

            ' B. Update Kuota Terpakai di tabel Master Cartridge
            ' Logika: 
            ' - Jika fisik kurang dari sistem (Selisih MINUS), artinya Kuota Terpakai HARUS NAIK.
            ' - Pengurangan (-) ketemu angka minus (-) akan menjadi plus (+).
            dbTran.Execute( _
                "UPDATE MasterCartridge SET " & _
                "KuotaTerpakai = KuotaTerpakai - (" & selisih & ") " & _
                "WHERE CartridgeID = " & cartridgeID)

            dbTran.Commit()

            MsgBox("Data penyesuaian (adjustment) berhasil disimpan.", MsgBoxStyle.Information, "Sukses")

            ' ==========================================
            ' 4. RESET FORM SETELAH SIMPAN
            ' ==========================================
            TextBoxSaldoSistem.Clear()
            TextBoxSaldoFisik.Clear()
            TextBoxSelisih.Clear()
            TextBoxKeterangan.Clear()

            ' Kosongkan Pilihan Combo
            ComboBoxCartridge.SelectedIndex = -1
            ComboBoxAlat.SelectedIndex = -1
            ComboBoxAlat.Focus()

        Catch ex As Exception
            dbTran.Rollback()
            MsgBox("Terjadi kesalahan saat menyimpan data: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
End Class