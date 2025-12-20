Public Class FormKodeVoucher
    Private userInput As String = PoliKlinik.Lbluser.Text

    Private Sub FormKodeVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = PoliKlinik
        Me.Dock = DockStyle.Fill
        Me.WindowState = FormWindowState.Maximized
        LoadVoucher()
    End Sub

    Private Sub TextBoxMinVoc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxMinVoc.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBoxNominalVoc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxNominalVoc.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub ButtonGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonGenerate.Click
        ' Generate kode voucher berupa UUID
        'Dim kodeVoucher As String = Guid.NewGuid().ToString().ToUpper()

        ' (Opsional) Kalau mau kode lebih pendek, ambil sebagian
        Dim UUID As String = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()
        Dim kodeVoucher As String = "LMRLZL" & UUID

        ' Tampilkan hasil ke TextBox
        TextBoxKodeVoc.Text = kodeVoucher
    End Sub

    Private Sub ComboBoxJenisVoc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxJenisVoc.SelectedIndexChanged
        ' Pastikan ComboBox sudah terisi
        If ComboBoxJenisVoc.SelectedItem Is Nothing Then Exit Sub

        ' Ambil teks yang dipilih
        Dim jenis As String = ComboBoxJenisVoc.Text.Trim()

        ' Jika yang dipilih adalah "Voucher Tindakan"
        If jenis = "Voucher Tindakan" Then
            TextBoxTindakan.Enabled = True
            ButtonFind.Enabled = True
        Else
            TextBoxTindakan.Enabled = False
            ButtonFind.Enabled = False
            TextBoxTindakan.Clear() ' Opsional: hapus isi textbox saat dinonaktifkan
        End If
    End Sub

    Private Sub ButtonFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFind.Click
        Find.PickMode = Find.FindPickMode.AppendText
        Find.Show()
        Find.Query("SELECT TindakanID, NamaTindakan, BiayaTindakan FROM MasterTindakan WHERE ClosedDate IS NULL", " ORDER BY NamaTindakan ASC", "FindMasterTindakanVoc", 1)
    End Sub

    Private Sub ResetForm()
        TextBoxKodeVoc.Clear()
        TextBoxNamaVoc.Clear()
        ComboBoxNominalVoc.SelectedIndex = -1
        TextBoxNominalVoc.Clear()
        TextBoxMinVoc.Clear()
        ComboBoxJenisPemakaian.SelectedIndex = -1
        dtAwal.Value = DateTime.Now
        dtAkhir.Value = DateTime.Now
        TextBoxKuotaVoc.Clear()
        ComboBoxJenisVoc.SelectedIndex = -1
        TextBoxTindakan.Clear()
    End Sub

    Private Sub ButtonSimpanVoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSimpanVoc.Click
        ' --- Ambil nilai dari form ---
        Dim kode As String = TextBoxKodeVoc.Text.Trim()
        Dim nama As String = TextBoxNamaVoc.Text.Trim()
        Dim tipe As String = ComboBoxNominalVoc.Text.Trim()
        Dim pemakaian As String = ComboBoxJenisPemakaian.Text.Trim()
        Dim nilai As Integer = If(TextBoxNominalVoc.Text = "", 0, CInt(TextBoxNominalVoc.Text))
        Dim minTransaksi As Integer = If(TextBoxMinVoc.Text = "", 0, CInt(TextBoxMinVoc.Text))
        Dim tglMulai As DateTime = dtAwal.Value
        Dim tglAkhir As DateTime = dtAkhir.Value
        Dim maks As Integer = If(TextBoxKuotaVoc.Text = "", 0, CInt(TextBoxKuotaVoc.Text))
        Dim jenis As Integer

        If ComboBoxJenisVoc.Text.Trim().ToLower() = "voucher tindakan" Then
            jenis = 1
        Else
            jenis = 0
        End If

        Dim status As Integer = 1
        Dim createdBy As String = userInput

        Dim tindakanID As String = ""
        Dim listID As New List(Of String)

        If TextBoxTindakan.Text.Trim() <> "" Then
            Dim items() As String = TextBoxTindakan.Text.Split(","c)

            For i As Integer = 0 To items.Length - 1
                Dim part() As String = items(i).Split("-"c)
                If part.Length > 0 Then
                    listID.Add(part(0).Trim())
                End If
            Next
        End If

        tindakanID = String.Join(",", listID.ToArray())

        ' --- Validasi ---
        If kode = "" Or nama = "" Then
            MsgBox("Kode dan nama voucher harus diisi.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        ' === VALIDASI JENIS PEMAKAIAN VOUCHER ===
        If ComboBoxJenisPemakaian.SelectedIndex = -1 OrElse _
           String.IsNullOrEmpty(ComboBoxJenisPemakaian.Text) Then

            MsgBox("Jenis pemakaian voucher wajib dipilih.", _
                   MsgBoxStyle.Exclamation, "Validasi")
            ComboBoxJenisPemakaian.Focus()
            Exit Sub
        End If

        ' ✅ Validasi kode voucher tidak boleh sama
        Dim cekSql As String = "SELECT COUNT(*) FROM MasterVoucher WHERE KodeVoucher = '" & kode.Replace("'", "''") & "'"
        Dim dtCek As DataTable = ReadSQL(cekSql)
        If dtCek.Rows.Count > 0 AndAlso CInt(dtCek.Rows(0)(0)) > 0 Then
            MsgBox("Kode voucher sudah digunakan. Gunakan kode lain.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If tglAkhir < tglMulai Then
            MsgBox("Tanggal akhir tidak boleh lebih kecil dari tanggal mulai.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If nilai <= 0 Then
            MsgBox("Nilai voucher tidak boleh 0.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        ' ✅ Validasi minimal pembelian harus diisi (>0)
        If minTransaksi <= 0 Then
            MsgBox("Minimal pembelian harus diisi dan lebih dari 0.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        ' --- Simpan ke database ---
        Dim sql As String = "INSERT INTO MasterVoucher " & _
            "(NamaVoucher, KodeVoucher, Tipe, Nilai, MinTransaksi, TanggalMulai, TanggalAkhir, JumlahMaks, JumlahTerpakai, Status, Tindakan, TipePemakaian, KodeTindakan, CreatedDate, CreatedBy) VALUES (" & _
            "'" & nama.Replace("'", "''") & "', " & _
            "'" & kode.Replace("'", "''") & "', " & _
            "'" & tipe.Replace("'", "''") & "', " & _
            nilai & ", " & _
            minTransaksi & ", " & _
            "'" & tglMulai.ToString("yyyy-MM-dd HH:mm:ss") & "', " & _
            "'" & tglAkhir.ToString("yyyy-MM-dd HH:mm:ss") & "', " & _
            maks & ", " & _
            "0, " & _
            status & ", " & _
            jenis & ", " & _
            "'" & pemakaian.Replace("'", "''") & "', " & _
            "'" & tindakanID.Replace("'", "''") & "', " & _
            "GETDATE(), " & _
            "'" & createdBy.Replace("'", "''") & "'" & _
            ")"

        WriteSQL(sql, 4)

        MsgBox("Data voucher berhasil disimpan.", MsgBoxStyle.Information)
        ResetForm()
        LoadVoucher()
    End Sub

    Private Sub LoadVoucher()
        Dim sql As String = "" & _
            "SELECT " & _
                "MV.NamaVoucher, " & _
                "MV.KodeVoucher, " & _
                "MV.Tipe, " & _
                "MV.Nilai, " & _
                "MV.MinTransaksi, " & _
                "MV.TanggalMulai, " & _
                "MV.TanggalAkhir, " & _
                "MV.JumlahMaks, " & _
                "MV.JumlahTerpakai, " & _
                "MV.Status, " & _
                "MV.KodeTindakan, " & _
                "MV.CreatedDate, " & _
                "MV.CreatedBy, " & _
                "MV.TipePemakaian " & _
            "FROM MasterVoucher MV " & _
            "WHERE(MV.DeletedBy Is NULL) " & _
            "ORDER BY MV.CreatedDate DESC"

        Dim dt As DataTable = ReadSQL(sql)

        Dim dtTindakan As DataTable = ReadSQL("SELECT TindakanID, NamaTindakan FROM MasterTindakan")

        ' --- Tambahkan kolom tampilan untuk user ---
        dt.Columns.Add("Nama Voucher", GetType(String))
        dt.Columns.Add("Kode Voucher", GetType(String))
        dt.Columns.Add("Jenis Voucher", GetType(String))
        dt.Columns.Add("Nominal", GetType(String))
        dt.Columns.Add("Minimal Transaksi", GetType(String))
        dt.Columns.Add("Tanggal Berlaku", GetType(String))
        dt.Columns.Add("Kuota", GetType(String))
        dt.Columns.Add("Terpakai", GetType(String))
        dt.Columns.Add("Status Voucher", GetType(String))
        dt.Columns.Add("Nama Tindakan", GetType(String))
        dt.Columns.Add("Tipe Pemakaian", GetType(String))

        For Each row As DataRow In dt.Rows
            Dim nama As String = If(IsDBNull(row("NamaVoucher")), "", row("NamaVoucher").ToString())
            Dim kode As String = If(IsDBNull(row("KodeVoucher")), "", row("KodeVoucher").ToString())
            Dim tipe As String = If(IsDBNull(row("Tipe")), "", row("Tipe").ToString())
            Dim nilai As Decimal = If(IsDBNull(row("Nilai")), 0D, CDec(row("Nilai")))
            Dim minTransaksi As Decimal = If(IsDBNull(row("MinTransaksi")), 0D, CDec(row("MinTransaksi")))
            Dim tglMulai As DateTime = If(IsDBNull(row("TanggalMulai")), DateTime.MinValue, CDate(row("TanggalMulai")))
            Dim tglAkhir As DateTime = If(IsDBNull(row("TanggalAkhir")), DateTime.MinValue, CDate(row("TanggalAkhir")))
            Dim kuota As Integer = If(IsDBNull(row("JumlahMaks")), 0, CInt(row("JumlahMaks")))
            Dim terpakai As Integer = If(IsDBNull(row("JumlahTerpakai")), 0, CInt(row("JumlahTerpakai")))
            Dim status As Integer = 0
            If Not IsDBNull(row("Status")) Then
                Dim statusVal = row("Status")
                If TypeOf statusVal Is Boolean Then
                    status = If(CBool(statusVal), 1, 0)
                Else
                    status = CInt(statusVal)
                End If
            End If

            Dim tipePemakaian As String = ""
            If Not IsDBNull(row("TipePemakaian")) Then
                tipePemakaian = CStr(row("TipePemakaian"))
            End If

            Select Case tipePemakaian
                Case "ONCE"
                    row("Tipe Pemakaian") = "Sekali Pakai (Per Pasien)"
                Case "UNLIMITED"
                    row("Tipe Pemakaian") = "Bisa Berkali-kali"
                Case "DAILY"
                    row("Tipe Pemakaian") = "1x Per Hari (Per Pasien)"
                Case Else
                    row("Tipe Pemakaian") = "-"
            End Select

            'Dim tindakan As String = If(IsDBNull(row("NamaTindakan")), "-", row("NamaTindakan").ToString())

            row("Nama Voucher") = nama
            row("Kode Voucher") = kode
            row("Jenis Voucher") = If(tipe.ToUpper() = "PERCENT", "PERCENT", "NOMINAL")
            If tipe.ToUpper() = "PERCENT" Then
                row("Nominal") = nilai.ToString("N0") & "%"
            Else
                row("Nominal") = "Rp " & nilai.ToString("N0")
            End If
            row("Minimal Transaksi") = "Rp " & minTransaksi.ToString("N0")
            row("Tanggal Berlaku") = If(tglMulai = DateTime.MinValue, "", tglMulai.ToString("dd MMM yyyy")) & _
                                 " - " & If(tglAkhir = DateTime.MinValue, "", tglAkhir.ToString("dd MMM yyyy"))
            row("Kuota") = kuota & "x"
            row("Terpakai") = terpakai
            row("Status Voucher") = If(status = 1, "Aktif", "Nonaktif")

            Dim namaTindakanGabung As String = "-"

            If Not IsDBNull(row("KodeTindakan")) Then
                Dim ids() As String = row("KodeTindakan").ToString().Split(","c)
                Dim namaList As New List(Of String)

                For i As Integer = 0 To ids.Length - 1
                    Dim idVal As Integer

                    If Integer.TryParse(ids(i).Trim(), idVal) Then
                        Dim found() As DataRow = dtTindakan.Select("TindakanID = " & idVal)

                        If found.Length > 0 Then
                            namaList.Add(found(0)("NamaTindakan").ToString())
                        End If
                    End If
                Next

                If namaList.Count > 0 Then
                    namaTindakanGabung = String.Join(", ", namaList.ToArray())
                End If
            End If

            row("Nama Tindakan") = namaTindakanGabung
        Next

        DataGridView1.DataSource = dt

        ' --- Tambahkan tombol hapus ---
        If Not DataGridView1.Columns.Contains("Hapus") Then
            Dim btnHapus As New DataGridViewButtonColumn()
            btnHapus.Name = "Hapus"
            btnHapus.HeaderText = ""
            btnHapus.Text = "Hapus"
            btnHapus.UseColumnTextForButtonValue = True
            btnHapus.Width = 60
            btnHapus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            btnHapus.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView1.Columns.Add(btnHapus)
            DataGridView1.RowTemplate.Height = 35
            btnHapus.DisplayIndex = DataGridView1.Columns.Count - 1
        End If

        ' --- Sembunyikan kolom mentah ---
        Dim hiddenCols() As String = {"ID", "NamaVoucher", "KodeVoucher", "Tipe", "Nilai", "MinTransaksi", "TanggalMulai", "TanggalAkhir", "JumlahMaks", "JumlahTerpakai", "Status", "KodeTindakan", "CreatedDate", "CreatedBy", "TipePemakaian"}
        For Each colName In hiddenCols
            If DataGridView1.Columns.Contains(colName) Then
                DataGridView1.Columns(colName).Visible = False
            End If
        Next

        ' --- Warna status ---
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.IsNewRow Then Continue For
            Dim cell = row.Cells("Status Voucher")
            If cell IsNot Nothing AndAlso cell.Value IsNot Nothing Then
                If cell.Value.ToString().Trim().ToUpper() = "NONAKTIF" Then
                    row.DefaultCellStyle.ForeColor = Color.Gray
                End If
            End If
        Next

        ' --- Atur tampilan grid agar full dan proporsional ---
        With DataGridView1
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .ReadOnly = True
            .AllowUserToAddRows = False
            .AllowUserToResizeRows = False
            .RowHeadersVisible = False
            .BackgroundColor = Color.White
            .DefaultCellStyle.BackColor = Color.White
            .DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue
            .DefaultCellStyle.SelectionForeColor = Color.Black
            .BorderStyle = BorderStyle.None
        End With
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Pastikan kolom yang diklik adalah tombol "Hapus"
        If e.RowIndex >= 0 AndAlso DataGridView1.Columns(e.ColumnIndex).Name = "Hapus" Then
            Dim kodeVoucher As String = DataGridView1.Rows(e.RowIndex).Cells("Kode Voucher").Value.ToString()
            Dim namaVoucher As String = DataGridView1.Rows(e.RowIndex).Cells("Nama Voucher").Value.ToString()

            Dim konfirmasi As DialogResult = MessageBox.Show("Apakah Anda yakin ingin menghapus voucher: " & namaVoucher & "?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If konfirmasi = DialogResult.Yes Then
                Try
                    ' Soft delete (update DeletedBy dan DeletedDate)
                    Dim deletedBy As String = userInput
                    Dim sql As String = "UPDATE MasterVoucher SET DeletedBy='" & deletedBy.Replace("'", "''") & "', " & _
                                    "DeletedDate=GETDATE() WHERE KodeVoucher='" & kodeVoucher.Replace("'", "''") & "'"

                    WriteSQL(sql, 4)

                    MessageBox.Show("Data voucher berhasil dihapus.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' Refresh DataGridView
                    LoadVoucher()
                Catch ex As Exception
                    MessageBox.Show("Terjadi kesalahan saat menghapus data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End If
    End Sub

    Private Sub ButtonGenerateVoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonGenerateVoc.Click
        Dim dlg As New DialogGenerateVoucher()
        If dlg.ShowDialog() = DialogResult.OK Then
            Dim jumlahGenerate As Integer
            If Integer.TryParse(dlg.TextBoxCopy.Text.Trim(), jumlahGenerate) = False OrElse jumlahGenerate <= 0 Then
                MsgBox("Masukkan jumlah voucher yang valid.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            ' --- Ambil data dari form utama (seperti ButtonSimpanVoc_Click) ---
            Dim nama As String = TextBoxNamaVoc.Text.Trim()
            Dim tipe As String = ComboBoxNominalVoc.Text.Trim()
            Dim nilai As Integer = If(TextBoxNominalVoc.Text = "", 0, CInt(TextBoxNominalVoc.Text))
            Dim minTransaksi As Integer = If(TextBoxMinVoc.Text = "", 0, CInt(TextBoxMinVoc.Text))
            Dim tglMulai As DateTime = dtAwal.Value
            Dim tglAkhir As DateTime = dtAkhir.Value
            Dim maks As Integer = If(TextBoxKuotaVoc.Text = "", 0, CInt(TextBoxKuotaVoc.Text))
            Dim jenis As Integer = If(ComboBoxJenisVoc.Text.Trim().ToLower() = "voucher tindakan", 1, 0)
            Dim status As Integer = 1
            Dim createdBy As String = userInput

            Dim listID As New List(Of String)

            If TextBoxTindakan.Text.Trim() <> "" Then
                Dim items() As String = TextBoxTindakan.Text.Split(","c)

                For i As Integer = 0 To items.Length - 1
                    Dim part() As String = items(i).Split("-"c)
                    If part.Length > 0 Then
                        listID.Add(part(0).Trim())
                    End If
                Next
            End If

            Dim tindakanID As String = String.Join(",", listID.ToArray())


            ' --- Validasi umum ---
            If tindakanID = "" Then
                MsgBox("Pilih minimal satu tindakan.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            If nama = "" Or tipe = "" Then
                MsgBox("Nama dan tipe voucher harus diisi.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            If nilai <= 0 Then
                MsgBox("Nilai voucher tidak boleh 0.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            ' --- Generate beberapa voucher ---
            'Dim tahun As String = DateTime.Now.Year.ToString()
            Dim count As Integer = 0

            For i As Integer = 1 To jumlahGenerate
                ' Buat UUID unik
                Dim uuid As String = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()

                ' Format kode voucher
                Dim kodeVoucher As String = "LMRLZL" & uuid

                ' Query insert voucher
                Dim sql As String = "INSERT INTO MasterVoucher " & _
                    "(NamaVoucher, KodeVoucher, Tipe, Nilai, MinTransaksi, TanggalMulai, TanggalAkhir, JumlahMaks, JumlahTerpakai, Status, Tindakan, KodeTindakan, CreatedDate, CreatedBy) VALUES (" & _
                    "'" & nama.Replace("'", "''") & "', " & _
                    "'" & kodeVoucher.Replace("'", "''") & "', " & _
                    "'" & tipe.Replace("'", "''") & "', " & _
                    nilai & ", " & _
                    minTransaksi & ", " & _
                    "'" & tglMulai.ToString("yyyy-MM-dd HH:mm:ss") & "', " & _
                    "'" & tglAkhir.ToString("yyyy-MM-dd HH:mm:ss") & "', " & _
                    maks & ", 0, " & _
                    status & ", " & _
                    jenis & ", " & _
                    "'" & tindakanID.Replace("'", "''") & "', " & _
                    "GETDATE(), '" & createdBy.Replace("'", "''") & "')"

                WriteSQL(sql, 4)
                count += 1
            Next

            MsgBox(count & " voucher berhasil digenerate.", MsgBoxStyle.Information)
            ResetForm()
            LoadVoucher()
        End If
    End Sub
End Class