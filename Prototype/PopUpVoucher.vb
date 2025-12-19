Public Class PopUpVoucher
    Private _NoRekamMedis As String

    Public Property NoRekamMedis() As String
        Get
            Return _NoRekamMedis
        End Get
        Set(ByVal value As String)
            _NoRekamMedis = value
        End Set
    End Property

    Public Event VoucherDipilih(ByVal kode As String, ByVal jenis As String, ByVal nilai As Decimal, ByVal isTindakan As Boolean, ByVal namaTindakan As String)

    Private userInput As String = PoliKlinik.Lbluser.Text

    Private Sub PopUpVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadRiwayatVoucher()
    End Sub

    Private Sub LoadRiwayatVoucher()
        Try
            Dim sql As String = "SELECT " & _
                "TPD.KodeVoucher, " & _
                "MV.NamaVoucher, " & _
                "MV.Tipe, " & _
                "MV.Nilai, " & _
                "CONVERT(VARCHAR(10), MV.TanggalAkhir, 105) AS TanggalAkhir " & _
                "FROM TransaksiPoli TP " & _
                "INNER JOIN TransaksiPoliDetail TPD ON TP.TransPoliID = TPD.TransPoliID " & _
                "INNER JOIN MasterVoucher MV ON TPD.KodeVoucher = MV.KodeVoucher " & _
                "WHERE TP.IDInternet = '" & _NoRekamMedis.Replace("'", "''") & "' " & _
                "AND TPD.DeletedBy IS NULL "

            Dim dt As DataTable = ReadSQL(sql)
            DataGridRiwayat.DataSource = dt
            DataGridVoucher.AllowUserToAddRows = False
            DataGridVoucher.AllowUserToDeleteRows = False
            DataGridVoucher.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        Catch ex As Exception
            MsgBox("Gagal memuat riwayat voucher: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub ButtonCariVoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCariVoc.Click
        Dim kodeVoucher As String = Trim(TextBoxKodeVoc.Text)

        If kodeVoucher = "" Then
            MessageBox.Show("Silakan masukkan kode voucher terlebih dahulu.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Query cari voucher
        Dim sql As String = "" & _
            "SELECT TOP 1 " & _
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
                "MV.KodeTindakan " & _
            "FROM MasterVoucher MV " & _
            "WHERE MV.KodeVoucher = '" & kodeVoucher.Replace("'", "''") & "' " & _
            "AND (MV.DeletedBy IS NULL OR MV.DeletedBy = '') " & _
            "AND (MV.JumlahTerpakai < MV.JumlahMaks OR MV.JumlahMaks = 0)"


        Dim dt As DataTable = ReadSQL(sql)

        Dim dtTindakan As DataTable = ReadSQL("SELECT TindakanID, NamaTindakan FROM MasterTindakan")

        If dt.Rows.Count = 0 Then
            MessageBox.Show("Kode voucher tidak ditemukan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim row As DataRow = dt.Rows(0)

        ' --- Validasi status dan tanggal ---
        Dim status As Integer = If(IsDBNull(row("Status")), 0, CInt(row("Status")))
        Dim tglMulai As DateTime = If(IsDBNull(row("TanggalMulai")), DateTime.MinValue, CDate(row("TanggalMulai")))
        Dim tglAkhir As DateTime = If(IsDBNull(row("TanggalAkhir")), DateTime.MinValue, CDate(row("TanggalAkhir")))
        Dim sekarang As DateTime = DateTime.Now

        If status = 0 Then
            MessageBox.Show("Voucher sudah tidak aktif.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If sekarang < tglMulai Or sekarang > tglAkhir Then
            MessageBox.Show("Voucher tidak berlaku untuk tanggal hari ini.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' --- Tampilkan di DataGridVoucher ---
        Dim dtDisplay As New DataTable()
        dtDisplay.Columns.Add("Kode Voucher")
        dtDisplay.Columns.Add("Nama Voucher")
        dtDisplay.Columns.Add("Jenis Voucher")
        dtDisplay.Columns.Add("Nominal")
        dtDisplay.Columns.Add("Nama Tindakan")
        dtDisplay.Columns.Add("Berakhir Tanggal")
        dtDisplay.Columns.Add("KodeTindakan")

        Dim tipe As String = row("Tipe").ToString().ToUpper()
        Dim nilai As Decimal = CDec(row("Nilai"))

        Dim nominal As String = If(tipe = "PERCENT", nilai.ToString("N0") & "%", "Rp " & nilai.ToString("N0"))
        Dim tglAkhirStr As String = If(tglAkhir = DateTime.MinValue, "-", tglAkhir.ToString("dd MMM yyyy"))

        Dim namaTindakan As String = "-"

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
                namaTindakan = String.Join(", ", namaList.ToArray())
            End If
        End If

        dtDisplay.Rows.Add(row("KodeVoucher"), row("NamaVoucher"), tipe, nominal, namaTindakan, tglAkhirStr, row("KodeTindakan").ToString())

        DataGridVoucher.DataSource = dtDisplay
        DataGridVoucher.AllowUserToAddRows = False
        DataGridVoucher.AllowUserToDeleteRows = False
        DataGridVoucher.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridVoucher.Columns("KodeTindakan").Visible = False

        If Not DataGridVoucher.Columns.Contains("Pilih") Then
            Dim chk As New DataGridViewCheckBoxColumn()
            chk.HeaderText = "Pilih"
            chk.Name = "Pilih"
            chk.Width = 50
            DataGridVoucher.Columns.Insert(0, chk)
        End If

        For Each col As DataGridViewColumn In DataGridVoucher.Columns
            If col.Name = "Pilih" Then
                col.ReadOnly = False
            Else
                col.ReadOnly = True
            End If
        Next

        MessageBox.Show("Voucher ditemukan dan ditambahkan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    'Private Sub ButtonSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSubmit.Click
    '    For Each row As DataGridViewRow In DataGridVoucher.Rows
    '        If Convert.ToBoolean(row.Cells("Pilih").Value) = True Then
    '            Dim kode As String = row.Cells("Kode Voucher").Value.ToString()
    '            Dim jenis As String = row.Cells("Jenis Voucher").Value.ToString() ' NOMINAL atau PERCENT
    '            Dim nilai As Decimal = CDec(row.Cells("NOMINAL").Value.ToString().Replace("Rp", "").Replace("%", "").Trim())

    '            ' Tambahkan nama tindakan (jika voucher jenis tindakan)
    '            Dim namaTindakan As String = ""
    '            If DataGridVoucher.Columns.Contains("Nama Tindakan") Then
    '                namaTindakan = row.Cells("Nama Tindakan").Value.ToString()
    '            End If

    '            ' Tentukan apakah voucher ini untuk tindakan tertentu
    '            Dim isTindakan As Boolean = Not String.IsNullOrEmpty(namaTindakan)

    '            ' 🟡 Validasi: cek apakah voucher sudah pernah digunakan pasien ini
    '            Dim sudahDipakai As Boolean = False

    '            For Each r As DataGridViewRow In DataGridRiwayat.Rows
    '                If r.Cells("KodeVoucher").Value.ToString().Trim().ToUpper() = kode.Trim().ToUpper() Then
    '                    sudahDipakai = True
    '                    Exit For
    '                End If
    '            Next

    '            If sudahDipakai Then
    '                MsgBox("Voucher ini sudah pernah digunakan oleh pasien ini dan tidak dapat digunakan lagi.", MsgBoxStyle.Exclamation, "Voucher Sudah Digunakan")
    '                Exit Sub
    '            End If

    '            ' Kirim semua parameter ke FormTransaksiPoli
    '            RaiseEvent VoucherDipilih(kode, jenis, nilai, isTindakan, namaTindakan)

    '            Exit For
    '        End If
    '    Next

    '    Me.Close()
    'End Sub

    Private Sub ButtonSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSubmit.Click
        For Each row As DataGridViewRow In DataGridVoucher.Rows
            If Convert.ToBoolean(row.Cells("Pilih").Value) = True Then

                Dim kode As String = row.Cells("Kode Voucher").Value.ToString()
                Dim jenis As String = row.Cells("Jenis Voucher").Value.ToString()

                Dim nilai As Decimal = CDec(row.Cells("Nominal").Value.ToString().Replace("Rp", "").Replace("%", "").Replace(",", "").Trim())

                ' 🔑 AMBIL KODE TINDAKAN (CSV ID)
                Dim kodeTindakan As String = ""
                If DataGridVoucher.Columns.Contains("KodeTindakan") Then
                    kodeTindakan = row.Cells("KodeTindakan").Value.ToString()
                End If

                ' Voucher khusus tindakan jika KodeTindakan tidak kosong
                Dim isTindakan As Boolean = (kodeTindakan.Trim() <> "")

                ' 🟡 Validasi: cek apakah voucher sudah pernah digunakan pasien ini
                Dim sudahDipakai As Boolean = False
                For Each r As DataGridViewRow In DataGridRiwayat.Rows
                    If r.Cells("KodeVoucher").Value.ToString().Trim().ToUpper() = kode.Trim().ToUpper() Then
                        sudahDipakai = True
                        Exit For
                    End If
                Next

                If sudahDipakai Then
                    MsgBox("Voucher ini sudah pernah digunakan oleh pasien ini dan tidak dapat digunakan lagi.", MsgBoxStyle.Exclamation, "Voucher Sudah Digunakan")
                    Exit Sub
                End If

                ' ✅ KIRIM KE FORM TUJUAN (KASIR / FORM TINDAKAN)
                RaiseEvent VoucherDipilih(kode, jenis, nilai, isTindakan, kodeTindakan)

                Exit For
            End If
        Next

        Me.Close()
    End Sub
End Class