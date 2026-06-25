Public Class FormSimulasiBHP
    Dim dbApotek As New TLcon

    ' Variabel untuk menampung cache data BHP di memory (RAM)
    Private dtCacheBHP As New DataTable()

    Private Sub ConnectionApotek()
        ' Mengambil konfigurasi database dari database utama/lokal saat ini
        Dim Tbls As DataTable = ReadSQL("SELECT ConfigName, StrValue FROM MsConfig WHERE ConfigName IN ('NameDatabaseObat', 'AddressDatabaseObat', 'PasswordDatabaseObat')")
        dbApotek.Tid = "root"

        For Each Trow As DataRow In Tbls.Rows
            If Trow("ConfigName") = "NameDatabaseObat" Then
                dbApotek.Tdb = Trow("StrValue")
            ElseIf Trow("ConfigName") = "AddressDatabaseObat" Then
                dbApotek.Tip = Trow("StrValue")
            ElseIf Trow("ConfigName") = "PasswordDatabaseObat" Then
                dbApotek.Tpass = Trow("StrValue")
            End If
        Next
    End Sub

    Private Sub LoadCacheBHP()
        Try
            Dim query As String = "CALL Sp_GetListBHP('\%','')"

            dtCacheBHP = ReadThemysql(query, dbApotek)

            ' Cek apakah query mengembalikan data atau kosong
            If dtCacheBHP IsNot Nothing Then
                ' Munculkan pop-up info jumlah data yang berhasil ditarik
                MessageBox.Show("Berhasil menarik " & dtCacheBHP.Rows.Count & " data BHP dari database Apotek.", "Debug Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Objek DataTable bernilai Null (Tidak Terinisialisasi).", "Debug Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal memuat cache BHP dari database Apotek: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub FormSimulasiBHP_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ConnectionApotek()

        LoadCacheBHP()
        txtNama.Multiline = False
        txtQty.Multiline = False

        txtNama.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txtNama.AutoCompleteSource = AutoCompleteSource.CustomSource

        Dim kolNamaObat As New AutoCompleteStringCollection()

        If dtCacheBHP IsNot Nothing AndAlso dtCacheBHP.Rows.Count > 0 Then
            For Each row As DataRow In dtCacheBHP.Rows
                Dim nama As String = row("namaobat").ToString().Trim()
                If Not String.IsNullOrEmpty(nama) Then
                    kolNamaObat.Add(nama)
                End If
            Next
        End If

        txtNama.AutoCompleteCustomSource = kolNamaObat

        With dgvBHP
            .Columns.Clear()
            .Columns.Add("Nama", "Nama BHP")
            .Columns.Add("Harga", "Harga Satuan")
            .Columns.Add("Jumlah", "Jumlah")
            .Columns.Add("Subtotal", "Subtotal")
            .Dock = DockStyle.Fill
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .Columns("Nama").FillWeight = 40
            .Columns("Harga").FillWeight = 20
            .Columns("Jumlah").FillWeight = 20
            .Columns("Subtotal").FillWeight = 20
        End With
    End Sub

    Private Sub dgvBHP_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles dgvBHP.KeyDown
        If e.KeyCode = Keys.Delete Then
            If dgvBHP.CurrentRow IsNot Nothing AndAlso Not dgvBHP.CurrentRow.IsNewRow Then

                Dim konfirmasi As DialogResult = MessageBox.Show("Hapus item BHP yang dipilih?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If konfirmasi = DialogResult.Yes Then
                    dgvBHP.Rows.Remove(dgvBHP.CurrentRow)
                    HitungTotalBiaya()
                End If

            End If
        End If
    End Sub

    Private Sub txtNama_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNama.TextChanged
        If dtCacheBHP IsNot Nothing AndAlso dtCacheBHP.Rows.Count > 0 Then

            Dim namaDicari As String = txtNama.Text.Replace("'", "''")

            Dim foundRows() As DataRow = dtCacheBHP.Select("namaobat = '" & namaDicari & "'")

            If foundRows.Length > 0 Then
                Dim formatHarga As Decimal
                Dim hargaObat As String = foundRows(0)("valjual").ToString()

                If Decimal.TryParse(hargaObat, formatHarga) Then
                    txtHarga.Text = formatHarga.ToString("N2")
                Else
                    txtHarga.Text = "0.00"
                End If
            Else
                txtHarga.Text = ""
            End If
        End If
    End Sub

    Private Sub ButtonTambah_click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonTambah.Click
        If String.IsNullOrEmpty(txtNama.Text) OrElse String.IsNullOrEmpty(txtHarga.Text) Then
            MessageBox.Show("Pilih obat BHP yang valid terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim jumlah As Integer
        If Not Integer.TryParse(txtQty.Text, jumlah) OrElse jumlah <= 0 Then
            MessageBox.Show("Masukkan jumlah pemakaian yang valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim harga As Decimal = Convert.ToDecimal(txtHarga.Text)
        Dim subtotal As Decimal = harga * jumlah

        dgvBHP.Rows.Add(txtNama.Text, harga.ToString("N2"), jumlah, subtotal.ToString("N2"))

        HitungTotalBiaya()

        txtNama.Clear()
        txtQty.Clear()
        txtNama.Focus()
    End Sub

    Private Sub HitungTotalBiaya()
        Dim total As Decimal = 0

        For Each row As DataGridViewRow In dgvBHP.Rows
            If Not row.IsNewRow Then
                If row.Cells("Subtotal").Value IsNot Nothing Then
                    total += Convert.ToDecimal(row.Cells("Subtotal").Value)
                End If
            End If
        Next

        txtTotalBiaya.Text = total.ToString("N2")
    End Sub
End Class