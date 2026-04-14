Imports System.Security.Cryptography
Imports System.Text

Public Class TripleDES

    Private Sub TripleDES_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        txtKey.Text = "albert"
    End Sub

    Private Function EncryptTripleDES(ByVal plainText As String, ByVal key As String) As String
        Try
            Dim data As Byte() = Encoding.UTF8.GetBytes(plainText)

            ' Hash key menggunakan MD5
            Dim md5 As New MD5CryptoServiceProvider()
            Dim keyBytes As Byte() = md5.ComputeHash(Encoding.UTF8.GetBytes(key))
            md5.Clear()

            ' Konfigurasi TripleDES
            Dim tripleDES As New TripleDESCryptoServiceProvider()
            tripleDES.Key = keyBytes
            tripleDES.Mode = CipherMode.ECB
            tripleDES.Padding = PaddingMode.PKCS7

            Dim encryptor As ICryptoTransform = tripleDES.CreateEncryptor()
            Dim results As Byte() = encryptor.TransformFinalBlock(data, 0, data.Length)

            tripleDES.Clear()

            Return Convert.ToBase64String(results)
        Catch ex As Exception
            MessageBox.Show("Error Enkripsi: " & ex.Message)
            Return ""
        End Try
    End Function

    Private Function DecryptTripleDES(ByVal cipherText As String, ByVal key As String) As String
        Try
            Dim data As Byte() = Convert.FromBase64String(cipherText)

            ' Hash key menggunakan MD5
            Dim md5 As New MD5CryptoServiceProvider()
            Dim keyBytes As Byte() = md5.ComputeHash(Encoding.UTF8.GetBytes(key))
            md5.Clear()

            ' Konfigurasi TripleDES
            Dim tripleDES As New TripleDESCryptoServiceProvider()
            tripleDES.Key = keyBytes
            tripleDES.Mode = CipherMode.ECB
            tripleDES.Padding = PaddingMode.PKCS7

            Dim decryptor As ICryptoTransform = tripleDES.CreateDecryptor()
            Dim results As Byte() = decryptor.TransformFinalBlock(data, 0, data.Length)

            tripleDES.Clear()

            Return Encoding.UTF8.GetString(results)
        Catch ex As Exception
            MessageBox.Show("Error Dekripsi: " & ex.Message)
            Return ""
        End Try
    End Function


    Private Sub btnEncrypt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEncrypt.Click
        If txtPlainText.Text = "" Then
            MessageBox.Show("Masukkan teks yang akan dienkripsi!", "Peringatan")
            Exit Sub
        End If

        txtCipherText.Text = EncryptTripleDES(txtPlainText.Text, txtKey.Text)
    End Sub


    Private Sub btnDecrypt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDecrypt.Click
        If txtCipherText.Text = "" Then
            MessageBox.Show("Masukkan teks yang akan didekripsi!", "Peringatan")
            Exit Sub
        End If

        txtPlainText.Text = DecryptTripleDES(txtCipherText.Text, txtKey.Text)
    End Sub


    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtPlainText.Clear()
        txtCipherText.Clear()
        txtKey.Text = "albert"
        txtPlainText.Focus()
    End Sub
End Class