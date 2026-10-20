<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormLaporanPasien
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.RBTindakanPasien = New System.Windows.Forms.RadioButton
        Me.RBPasienBaru = New System.Windows.Forms.RadioButton
        Me.RBTotalKunjungan = New System.Windows.Forms.RadioButton
        Me.RBPerPoli = New System.Windows.Forms.RadioButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.ComboBoxPoli = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.ComboBoxTahun = New System.Windows.Forms.ComboBox
        Me.ComboBoxBulan = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.ButtonTampilkan = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.TextBoxTindakan = New System.Windows.Forms.TextBox
        Me.ButtonCari = New System.Windows.Forms.Button
        Me.TextBoxIdTindakan = New System.Windows.Forms.TextBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RBTindakanPasien)
        Me.GroupBox1.Controls.Add(Me.RBPasienBaru)
        Me.GroupBox1.Controls.Add(Me.RBTotalKunjungan)
        Me.GroupBox1.Controls.Add(Me.RBPerPoli)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(590, 49)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Mode"
        '
        'RBTindakanPasien
        '
        Me.RBTindakanPasien.AutoSize = True
        Me.RBTindakanPasien.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBTindakanPasien.Location = New System.Drawing.Point(434, 18)
        Me.RBTindakanPasien.Name = "RBTindakanPasien"
        Me.RBTindakanPasien.Size = New System.Drawing.Size(153, 17)
        Me.RBTindakanPasien.TabIndex = 3
        Me.RBTindakanPasien.TabStop = True
        Me.RBTindakanPasien.Text = "Laporan Tindakan Pasien"
        Me.RBTindakanPasien.UseVisualStyleBackColor = True
        '
        'RBPasienBaru
        '
        Me.RBPasienBaru.AutoSize = True
        Me.RBPasienBaru.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBPasienBaru.Location = New System.Drawing.Point(274, 18)
        Me.RBPasienBaru.Name = "RBPasienBaru"
        Me.RBPasienBaru.Size = New System.Drawing.Size(157, 17)
        Me.RBPasienBaru.TabIndex = 2
        Me.RBPasienBaru.TabStop = True
        Me.RBPasienBaru.Text = "Laporan Data Pasien Baru"
        Me.RBPasienBaru.UseVisualStyleBackColor = True
        '
        'RBTotalKunjungan
        '
        Me.RBTotalKunjungan.AutoSize = True
        Me.RBTotalKunjungan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBTotalKunjungan.Location = New System.Drawing.Point(119, 18)
        Me.RBTotalKunjungan.Name = "RBTotalKunjungan"
        Me.RBTotalKunjungan.Size = New System.Drawing.Size(154, 17)
        Me.RBTotalKunjungan.TabIndex = 1
        Me.RBTotalKunjungan.TabStop = True
        Me.RBTotalKunjungan.Text = "Laporan Total Kunjungan"
        Me.RBTotalKunjungan.UseVisualStyleBackColor = True
        '
        'RBPerPoli
        '
        Me.RBPerPoli.AutoSize = True
        Me.RBPerPoli.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBPerPoli.Location = New System.Drawing.Point(10, 18)
        Me.RBPerPoli.Name = "RBPerPoli"
        Me.RBPerPoli.Size = New System.Drawing.Size(109, 17)
        Me.RBPerPoli.TabIndex = 0
        Me.RBPerPoli.TabStop = True
        Me.RBPerPoli.Text = "Laporan Per-Poli"
        Me.RBPerPoli.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Controls.Add(Me.ComboBoxPoli)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.ComboBoxTahun)
        Me.GroupBox2.Controls.Add(Me.ComboBoxBulan)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 67)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(590, 130)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Filter"
        '
        'ComboBoxPoli
        '
        Me.ComboBoxPoli.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxPoli.FormattingEnabled = True
        Me.ComboBoxPoli.Location = New System.Drawing.Point(287, 24)
        Me.ComboBoxPoli.Name = "ComboBoxPoli"
        Me.ComboBoxPoli.Size = New System.Drawing.Size(194, 25)
        Me.ComboBoxPoli.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(255, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Poli"
        '
        'ComboBoxTahun
        '
        Me.ComboBoxTahun.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxTahun.FormattingEnabled = True
        Me.ComboBoxTahun.Location = New System.Drawing.Point(144, 24)
        Me.ComboBoxTahun.Name = "ComboBoxTahun"
        Me.ComboBoxTahun.Size = New System.Drawing.Size(88, 25)
        Me.ComboBoxTahun.TabIndex = 2
        '
        'ComboBoxBulan
        '
        Me.ComboBoxBulan.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxBulan.FormattingEnabled = True
        Me.ComboBoxBulan.Location = New System.Drawing.Point(50, 24)
        Me.ComboBoxBulan.Name = "ComboBoxBulan"
        Me.ComboBoxBulan.Size = New System.Drawing.Size(88, 25)
        Me.ComboBoxBulan.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(7, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Bulan"
        '
        'ButtonTampilkan
        '
        Me.ButtonTampilkan.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonTampilkan.FlatAppearance.BorderSize = 0
        Me.ButtonTampilkan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonTampilkan.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonTampilkan.ForeColor = System.Drawing.Color.White
        Me.ButtonTampilkan.Location = New System.Drawing.Point(505, 203)
        Me.ButtonTampilkan.Name = "ButtonTampilkan"
        Me.ButtonTampilkan.Size = New System.Drawing.Size(94, 31)
        Me.ButtonTampilkan.TabIndex = 5
        Me.ButtonTampilkan.Text = "Tampilkan"
        Me.ButtonTampilkan.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.TextBoxIdTindakan)
        Me.GroupBox3.Controls.Add(Me.ButtonCari)
        Me.GroupBox3.Controls.Add(Me.TextBoxTindakan)
        Me.GroupBox3.Location = New System.Drawing.Point(10, 58)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(332, 61)
        Me.GroupBox3.TabIndex = 5
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Tindakan"
        '
        'TextBoxTindakan
        '
        Me.TextBoxTindakan.Location = New System.Drawing.Point(59, 21)
        Me.TextBoxTindakan.Multiline = True
        Me.TextBoxTindakan.Name = "TextBoxTindakan"
        Me.TextBoxTindakan.ReadOnly = True
        Me.TextBoxTindakan.Size = New System.Drawing.Size(213, 25)
        Me.TextBoxTindakan.TabIndex = 0
        '
        'ButtonCari
        '
        Me.ButtonCari.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonCari.FlatAppearance.BorderSize = 0
        Me.ButtonCari.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonCari.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCari.ForeColor = System.Drawing.Color.White
        Me.ButtonCari.Location = New System.Drawing.Point(12, 21)
        Me.ButtonCari.Name = "ButtonCari"
        Me.ButtonCari.Size = New System.Drawing.Size(41, 25)
        Me.ButtonCari.TabIndex = 6
        Me.ButtonCari.Text = "Cari"
        Me.ButtonCari.UseVisualStyleBackColor = False
        '
        'TextBoxIdTindakan
        '
        Me.TextBoxIdTindakan.Location = New System.Drawing.Point(278, 21)
        Me.TextBoxIdTindakan.Multiline = True
        Me.TextBoxIdTindakan.Name = "TextBoxIdTindakan"
        Me.TextBoxIdTindakan.ReadOnly = True
        Me.TextBoxIdTindakan.Size = New System.Drawing.Size(35, 25)
        Me.TextBoxIdTindakan.TabIndex = 7
        '
        'FormLaporanPasien
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(620, 257)
        Me.Controls.Add(Me.ButtonTampilkan)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximumSize = New System.Drawing.Size(636, 296)
        Me.MinimumSize = New System.Drawing.Size(636, 296)
        Me.Name = "FormLaporanPasien"
        Me.Text = "Form Laporan Pasien"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RBPerPoli As System.Windows.Forms.RadioButton
    Friend WithEvents RBTindakanPasien As System.Windows.Forms.RadioButton
    Friend WithEvents RBPasienBaru As System.Windows.Forms.RadioButton
    Friend WithEvents RBTotalKunjungan As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ComboBoxBulan As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxTahun As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxPoli As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ButtonTampilkan As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonCari As System.Windows.Forms.Button
    Friend WithEvents TextBoxTindakan As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxIdTindakan As System.Windows.Forms.TextBox
End Class
