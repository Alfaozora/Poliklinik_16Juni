Public Class PopUpVoucher
    Public Event VoucherDipilih(ByVal kode As String, ByVal jenis As String, ByVal nilai As Integer)

    Private userInput As String = PoliKlinik.Lbluser.Text

    Private Sub ButtonCariVoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCariVoc.Click
        Dim kodeVoucher As String = Trim(TextBoxKodeVoc.Text)

        If kodeVoucher = "" Then
            MessageBox.Show("Silakan masukkan kode voucher terlebih dahulu.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Query cari voucher
        Dim sql As String = "" & _
            "SELECT TOP 1 " & _
                "NamaVoucher, " & _
                "KodeVoucher, " & _
                "Tipe, " & _
                "Nilai, " & _
                "MinTransaksi, " & _
                "TanggalMulai, " & _
                "TanggalAkhir, " & _
                "JumlahMaks, " & _
                "JumlahTerpakai, " & _
                "Status, " & _
                "ISNULL(MT.NamaTindakan, '-') AS NamaTindakan " & _
            "FROM MasterVoucher MV " & _
            "LEFT JOIN MasterTindakan MT ON MT.TindakanID = MV.KodeTindakan " & _
            "WHERE MV.KodeVoucher = '" & kodeVoucher.Replace("'", "''") & "' " & _
            "AND (MV.DeletedBy IS NULL OR MV.DeletedBy = '')"

        Dim dt As DataTable = ReadSQL(sql)

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

        Dim tipe As String = row("Tipe").ToString().ToUpper()
        Dim nilai As Decimal = CDec(row("Nilai"))

        Dim nominal As String = If(tipe = "PERCENT", nilai.ToString("N0") & "%", "Rp " & nilai.ToString("N0"))
        Dim tglAkhirStr As String = If(tglAkhir = DateTime.MinValue, "-", tglAkhir.ToString("dd MMM yyyy"))

        dtDisplay.Rows.Add(row("KodeVoucher"), row("NamaVoucher"), tipe, nominal, row("NamaTindakan"), tglAkhirStr)

        DataGridVoucher.DataSource = dtDisplay
        DataGridVoucher.AllowUserToAddRows = False
        DataGridVoucher.AllowUserToDeleteRows = False

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

    Private Sub ButtonSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSubmit.Click
        For Each row As DataGridViewRow In DataGridVoucher.Rows
            If Convert.ToBoolean(row.Cells("Pilih").Value) = True Then
                Dim kode As String = row.Cells("Kode Voucher").Value.ToString()
                Dim jenis As String = row.Cells("Jenis Voucher").Value.ToString() ' NOMINAL atau PERCENT
                Dim nilai As Decimal = CDec(row.Cells("NOMINAL").Value.ToString().Replace("Rp", "").Replace("%", "").Trim())
                RaiseEvent VoucherDipilih(kode, jenis, nilai)
                Exit For
            End If
        Next
        Me.Close()
    End Sub
End Class