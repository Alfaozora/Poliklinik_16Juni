<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormAdjustmentAlat
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.ComboBoxAlat = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.ComboBoxCartridge = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TextBoxSaldoSistem = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.TextBoxSaldoFisik = New System.Windows.Forms.TextBox
        Me.ButtonKalkulasi = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.ButtonSimpan = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Pilih Alat"
        '
        'ComboBoxAlat
        '
        Me.ComboBoxAlat.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxAlat.FormattingEnabled = True
        Me.ComboBoxAlat.Location = New System.Drawing.Point(108, 15)
        Me.ComboBoxAlat.Name = "ComboBoxAlat"
        Me.ComboBoxAlat.Size = New System.Drawing.Size(207, 25)
        Me.ComboBoxAlat.TabIndex = 653
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 15)
        Me.Label2.TabIndex = 654
        Me.Label2.Text = "Pilih Cartridge"
        '
        'ComboBoxCartridge
        '
        Me.ComboBoxCartridge.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxCartridge.FormattingEnabled = True
        Me.ComboBoxCartridge.Location = New System.Drawing.Point(108, 57)
        Me.ComboBoxCartridge.Name = "ComboBoxCartridge"
        Me.ComboBoxCartridge.Size = New System.Drawing.Size(207, 25)
        Me.ComboBoxCartridge.TabIndex = 655
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 103)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 15)
        Me.Label3.TabIndex = 656
        Me.Label3.Text = "Saldo Sistem"
        '
        'TextBoxSaldoSistem
        '
        Me.TextBoxSaldoSistem.Location = New System.Drawing.Point(108, 101)
        Me.TextBoxSaldoSistem.Multiline = True
        Me.TextBoxSaldoSistem.Name = "TextBoxSaldoSistem"
        Me.TextBoxSaldoSistem.Size = New System.Drawing.Size(207, 25)
        Me.TextBoxSaldoSistem.TabIndex = 657
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 147)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 15)
        Me.Label4.TabIndex = 658
        Me.Label4.Text = "Saldo FIsik"
        '
        'TextBoxSaldoFisik
        '
        Me.TextBoxSaldoFisik.Location = New System.Drawing.Point(108, 145)
        Me.TextBoxSaldoFisik.Multiline = True
        Me.TextBoxSaldoFisik.Name = "TextBoxSaldoFisik"
        Me.TextBoxSaldoFisik.Size = New System.Drawing.Size(207, 25)
        Me.TextBoxSaldoFisik.TabIndex = 659
        '
        'ButtonKalkulasi
        '
        Me.ButtonKalkulasi.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonKalkulasi.FlatAppearance.BorderSize = 0
        Me.ButtonKalkulasi.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonKalkulasi.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonKalkulasi.ForeColor = System.Drawing.Color.White
        Me.ButtonKalkulasi.Location = New System.Drawing.Point(239, 188)
        Me.ButtonKalkulasi.Name = "ButtonKalkulasi"
        Me.ButtonKalkulasi.Size = New System.Drawing.Size(75, 23)
        Me.ButtonKalkulasi.TabIndex = 660
        Me.ButtonKalkulasi.Text = "Kalkulasi"
        Me.ButtonKalkulasi.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 219)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 15)
        Me.Label5.TabIndex = 661
        Me.Label5.Text = "Keterangan"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(107, 217)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(207, 86)
        Me.TextBox1.TabIndex = 662
        '
        'ButtonSimpan
        '
        Me.ButtonSimpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonSimpan.FlatAppearance.BorderSize = 0
        Me.ButtonSimpan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonSimpan.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSimpan.ForeColor = System.Drawing.Color.White
        Me.ButtonSimpan.Location = New System.Drawing.Point(107, 321)
        Me.ButtonSimpan.Name = "ButtonSimpan"
        Me.ButtonSimpan.Size = New System.Drawing.Size(208, 23)
        Me.ButtonSimpan.TabIndex = 663
        Me.ButtonSimpan.Text = "Simpan"
        Me.ButtonSimpan.UseVisualStyleBackColor = False
        '
        'FormAdjustmentAlat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(327, 385)
        Me.Controls.Add(Me.ButtonSimpan)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ButtonKalkulasi)
        Me.Controls.Add(Me.TextBoxSaldoFisik)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxSaldoSistem)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ComboBoxCartridge)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ComboBoxAlat)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FormAdjustmentAlat"
        Me.Text = "FormAdjustmentAlat"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxAlat As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxCartridge As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxSaldoSistem As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxSaldoFisik As System.Windows.Forms.TextBox
    Friend WithEvents ButtonKalkulasi As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ButtonSimpan As System.Windows.Forms.Button
End Class
