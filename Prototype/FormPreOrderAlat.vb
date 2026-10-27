Imports Newtonsoft.Json
Imports Newtonsoft
Imports System.Security.Cryptography
Imports System.Collections.Generic
Imports Newtonsoft.Json.Linq
Imports System.Drawing.Imaging
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Drawing
Imports PoliKlinik_Net2.ShapeDrawingFunctions
Imports System.Text
Imports System.Globalization
Imports System.Net
Public Class FormPreOrderAlat

    Private Sub ButtonGetSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonGetSupplier.Click
        FormSupplier.ShowDialog()
    End Sub

    Private Sub ButtonCariAlat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCariAlat.Click
        FormPencarianAlatSarpras.ShowDialog()
    End Sub

    Private Sub TextBoxQty_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TextBoxQty.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBoxHarga_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TextBoxHarga.KeyPress
        ' Mengizinkan angka, Backspace, dan tanda koma (atau titik sesuai format sistem Anda)
        ' Jika menggunakan koma sebagai pemisah desimal:
        Dim separator As Char = ","

        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> separator Then
            e.Handled = True
        End If

        ' Mencegah user memasukkan koma lebih dari satu kali
        If e.KeyChar = separator AndAlso DirectCast(sender, TextBox).Text.Contains(separator) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBoxHarga_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles TextBoxHarga.LostFocus
        Dim nilai As Decimal
        If Decimal.TryParse(TextBoxHarga.Text, nilai) Then
            TextBoxHarga.Text = nilai.ToString("N0") ' Format tanpa desimal, ribuan dipisah titik
        End If
    End Sub

    Private Sub TextBoxQty_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextBoxQty.TextChanged
        HitungTotal()
    End Sub

    Private Sub TextBoxHarga_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextBoxHarga.TextChanged
        HitungTotal()
    End Sub

    Private Sub HitungTotal()
        Dim qty As Decimal
        Dim harga As Decimal

        Decimal.TryParse(TextBoxQty.Text.Replace(".", "").Replace(",", ""), qty)
        Decimal.TryParse(TextBoxHarga.Text.Replace(".", "").Replace(",", ""), harga)

        ' Melakukan perkalian
        Dim total As Decimal = qty * harga

        TextBoxTotalHarga.Text = total.ToString("N0")
    End Sub

    Private Sub ButtonSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSubmit.Click
        ' Validasi sederhana sebelum kirim
        If TextBoxKodeItem.Text = "" Then
            MessageBox.Show("Silakan pilih alat/item terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Ubah kursor dan matikan tombol sementara
        ButtonSubmit.Enabled = False
        ButtonSubmit.Text = "Mengirim..."
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        Try
            ' 1. SUSUN JSON UTAMA (Header PR)
            Dim jsonPayload As New JObject()

            ' Format tanggal disesuaikan yyyy-MM-dd
            jsonPayload("pr_date") = DateTime.Now.ToString("yyyy-MM-dd")
            jsonPayload("pr_require_date") = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd")

            jsonPayload("pr_type") = ComboBoxJenisPermintaan.Text
            jsonPayload("payment_type") = ComboBoxJenisPembayaran.Text
            jsonPayload("assign_loc") = "LLMR"

            ' Data Rekomendasi Supplier
            jsonPayload("supp_recom_name") = TextBoxNamaSupp.Text
            jsonPayload("supp_recom_address") = TextBoxAlamatSupp.Text
            jsonPayload("supp_recom_payment_method") = TextBoxPay.Text
            jsonPayload("supp_recom_rek_bank") = TextBoxBankCode.Text
            jsonPayload("supp_recom_rek_no") = TextBoxNoRek.Text
            jsonPayload("supp_recom_rek_name") = TextBoxNamaRek.Text

            ' Asumsi ada TextBox untuk Catatan, jika tidak ada, kosongi saja ("")
            jsonPayload("pr_note") = ""

            ' 2. SUSUN ARRAY DETAIL PR (detail_pr)
            Dim detailArray As New JArray()

            Dim detailItem As New JObject()
            detailItem("room_id") = Convert.ToInt32(Val(TextBoxId.Text))
            detailItem("item_code") = TextBoxKodeItem.Text.Trim()
            detailItem("qty") = Convert.ToInt32(Val(TextBoxQty.Text))
            detailItem("satuan") = TextBoxSatuan.Text.Trim()
            detailItem("item_desc") = TextBoxDeskripsi.Text.Trim()

            Dim hargaClean As String = TextBoxHarga.Text.Replace(".", "").Replace(",", "").Trim()
            Dim totalClean As String = TextBoxTotalHarga.Text.Replace(".", "").Replace(",", "").Trim()

            detailItem("estimated_price") = If(IsNumeric(hargaClean), Convert.ToInt64(hargaClean), 0)
            detailItem("estimated_total") = If(IsNumeric(totalClean), Convert.ToInt64(totalClean), 0)

            ' Masukkan objek detail ke dalam Array, lalu Array ke JSON Utama
            detailArray.Add(detailItem)
            jsonPayload("detail_pr") = detailArray

            ' Ubah struktur JObject menjadi String JSON utuh
            Dim finalJsonString As String = jsonPayload.ToString()
            'MsgBox(finalJsonString)

            ' ==========================================
            ' START DEBUG (Hapus komentar jika ingin cek format JSON sebelum dikirim)
            ' MessageBox.Show("Payload yang dikirim:" & vbCrLf & finalJsonString, "Cek Payload JSON", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' ==========================================

            ' 3. TEMBAK API MENGGUNAKAN FUNGSI InsertDataAPIRMOld
            Dim baseUrl As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'ws_RM'")
            Dim apiUrl As String = baseUrl & "api/bridging/post-pr"

            ' INGAT: Urutan parameter fungsi Anda adalah (JSON, URL)
            Dim responseString As String = InsertDataAPIRMOld(finalJsonString, apiUrl)

            ' 4. EVALUASI RESPONSE JSON
            Dim jsonResult As JObject = JObject.Parse(responseString)

            ' Mengecek response status code dari API
            If jsonResult("code") IsNot Nothing AndAlso jsonResult("code").ToString() = "200" Then
                MessageBox.Show("Purchase Request (PR) berhasil dikirim!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Opsional: Tutup form atau bersihkan isian form
                Me.Close()
            Else
                ' Tangkap pesan error jika code bukan 200
                Dim errorMsg As String = If(jsonResult("message") IsNot Nothing, jsonResult("message").ToString(), "Terjadi kegagalan saat mengirim data.")
                MessageBox.Show("Gagal: " & errorMsg, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                ' Opsional Debug: Tampilkan detail error dari Laravel/Sarpras
                ' MessageBox.Show(responseString, "Debug Error")
            End If

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan sistem saat memproses data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Kembalikan kondisi tombol dan kursor
            ButtonSubmit.Enabled = True
            ButtonSubmit.Text = "Submit PR"
            Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class