Imports System.Windows.Forms

Public Class DialogGenerateVoucher

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If TextBoxCopy.Text.Trim() = "" Then
            MsgBox("Masukkan jumlah voucher yang akan digenerate.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
