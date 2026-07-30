<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormRevenueSelling
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
        Me.RadioButtonHarian = New System.Windows.Forms.RadioButton
        Me.RadioButtonBulanan = New System.Windows.Forms.RadioButton
        Me.RadioButtonTahunan = New System.Windows.Forms.RadioButton
        Me.dtAwal = New System.Windows.Forms.DateTimePicker
        Me.dtAkhir = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.ComboBoxBulan = New System.Windows.Forms.ComboBox
        Me.ComboBoxTahun = New System.Windows.Forms.ComboBox
        Me.ButtonCetakLaporan = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.ComboBoxPoli = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.TextBoxHariKerja = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.ButtonSimpanTarget = New System.Windows.Forms.Button
        Me.TextBoxTargetBulanan = New System.Windows.Forms.TextBox
        Me.ComboBoxPoliTarget = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.ButtonCetakCostumer = New System.Windows.Forms.Button
        Me.ButtonKirimLaporan = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lblStatusProgress = New System.Windows.Forms.Label
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'RadioButtonHarian
        '
        Me.RadioButtonHarian.AutoSize = True
        Me.RadioButtonHarian.Location = New System.Drawing.Point(22, 24)
        Me.RadioButtonHarian.Name = "RadioButtonHarian"
        Me.RadioButtonHarian.Size = New System.Drawing.Size(56, 17)
        Me.RadioButtonHarian.TabIndex = 0
        Me.RadioButtonHarian.TabStop = True
        Me.RadioButtonHarian.Text = "Harian"
        Me.RadioButtonHarian.UseVisualStyleBackColor = True
        '
        'RadioButtonBulanan
        '
        Me.RadioButtonBulanan.AutoSize = True
        Me.RadioButtonBulanan.Location = New System.Drawing.Point(22, 62)
        Me.RadioButtonBulanan.Name = "RadioButtonBulanan"
        Me.RadioButtonBulanan.Size = New System.Drawing.Size(64, 17)
        Me.RadioButtonBulanan.TabIndex = 1
        Me.RadioButtonBulanan.TabStop = True
        Me.RadioButtonBulanan.Text = "Bulanan"
        Me.RadioButtonBulanan.UseVisualStyleBackColor = True
        '
        'RadioButtonTahunan
        '
        Me.RadioButtonTahunan.AutoSize = True
        Me.RadioButtonTahunan.Location = New System.Drawing.Point(22, 99)
        Me.RadioButtonTahunan.Name = "RadioButtonTahunan"
        Me.RadioButtonTahunan.Size = New System.Drawing.Size(68, 17)
        Me.RadioButtonTahunan.TabIndex = 2
        Me.RadioButtonTahunan.TabStop = True
        Me.RadioButtonTahunan.Text = "Tahunan"
        Me.RadioButtonTahunan.UseVisualStyleBackColor = True
        '
        'dtAwal
        '
        Me.dtAwal.CalendarFont = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtAwal.CustomFormat = "dd-MMM-yyyy"
        Me.dtAwal.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtAwal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtAwal.Location = New System.Drawing.Point(99, 21)
        Me.dtAwal.Name = "dtAwal"
        Me.dtAwal.Size = New System.Drawing.Size(125, 25)
        Me.dtAwal.TabIndex = 449
        '
        'dtAkhir
        '
        Me.dtAkhir.CalendarFont = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtAkhir.CustomFormat = "dd-MMM-yyyy"
        Me.dtAkhir.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtAkhir.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtAkhir.Location = New System.Drawing.Point(256, 21)
        Me.dtAkhir.Name = "dtAkhir"
        Me.dtAkhir.Size = New System.Drawing.Size(125, 25)
        Me.dtAkhir.TabIndex = 450
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(228, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(23, 13)
        Me.Label1.TabIndex = 451
        Me.Label1.Text = "s/d"
        '
        'ComboBoxBulan
        '
        Me.ComboBoxBulan.FormattingEnabled = True
        Me.ComboBoxBulan.Location = New System.Drawing.Point(99, 62)
        Me.ComboBoxBulan.Name = "ComboBoxBulan"
        Me.ComboBoxBulan.Size = New System.Drawing.Size(125, 21)
        Me.ComboBoxBulan.TabIndex = 452
        '
        'ComboBoxTahun
        '
        Me.ComboBoxTahun.FormattingEnabled = True
        Me.ComboBoxTahun.Location = New System.Drawing.Point(99, 99)
        Me.ComboBoxTahun.Name = "ComboBoxTahun"
        Me.ComboBoxTahun.Size = New System.Drawing.Size(125, 21)
        Me.ComboBoxTahun.TabIndex = 453
        '
        'ButtonCetakLaporan
        '
        Me.ButtonCetakLaporan.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonCetakLaporan.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonCetakLaporan.FlatAppearance.BorderSize = 0
        Me.ButtonCetakLaporan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonCetakLaporan.ForeColor = System.Drawing.Color.White
        Me.ButtonCetakLaporan.Location = New System.Drawing.Point(479, 170)
        Me.ButtonCetakLaporan.Name = "ButtonCetakLaporan"
        Me.ButtonCetakLaporan.Size = New System.Drawing.Size(142, 23)
        Me.ButtonCetakLaporan.TabIndex = 454
        Me.ButtonCetakLaporan.Text = "Cetak Laporan Keuangan"
        Me.ButtonCetakLaporan.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 136)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(24, 13)
        Me.Label2.TabIndex = 455
        Me.Label2.Text = "Poli"
        '
        'ComboBoxPoli
        '
        Me.ComboBoxPoli.FormattingEnabled = True
        Me.ComboBoxPoli.Items.AddRange(New Object() {"POLI EYELASH & NAIL", "POLI FISIOTERAPI", "POLI HBOT", "POLI KECANTIKAN", "POLI REHABILITASI MEDIK"})
        Me.ComboBoxPoli.Location = New System.Drawing.Point(49, 133)
        Me.ComboBoxPoli.Name = "ComboBoxPoli"
        Me.ComboBoxPoli.Size = New System.Drawing.Size(175, 21)
        Me.ComboBoxPoli.TabIndex = 456
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBoxHariKerja)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.ButtonSimpanTarget)
        Me.GroupBox1.Controls.Add(Me.TextBoxTargetBulanan)
        Me.GroupBox1.Controls.Add(Me.ComboBoxPoliTarget)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(256, 62)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(365, 92)
        Me.GroupBox1.TabIndex = 457
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Set Target Bulanan"
        '
        'TextBoxHariKerja
        '
        Me.TextBoxHariKerja.Location = New System.Drawing.Point(100, 62)
        Me.TextBoxHariKerja.Name = "TextBoxHariKerja"
        Me.TextBoxHariKerja.Size = New System.Drawing.Size(49, 20)
        Me.TextBoxHariKerja.TabIndex = 460
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 65)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 13)
        Me.Label4.TabIndex = 459
        Me.Label4.Text = "Total Hari kerja"
        '
        'ButtonSimpanTarget
        '
        Me.ButtonSimpanTarget.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonSimpanTarget.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonSimpanTarget.FlatAppearance.BorderSize = 0
        Me.ButtonSimpanTarget.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonSimpanTarget.ForeColor = System.Drawing.Color.White
        Me.ButtonSimpanTarget.Location = New System.Drawing.Point(273, 60)
        Me.ButtonSimpanTarget.Name = "ButtonSimpanTarget"
        Me.ButtonSimpanTarget.Size = New System.Drawing.Size(86, 23)
        Me.ButtonSimpanTarget.TabIndex = 458
        Me.ButtonSimpanTarget.Text = "Simpan"
        Me.ButtonSimpanTarget.UseVisualStyleBackColor = False
        '
        'TextBoxTargetBulanan
        '
        Me.TextBoxTargetBulanan.Location = New System.Drawing.Point(210, 30)
        Me.TextBoxTargetBulanan.Name = "TextBoxTargetBulanan"
        Me.TextBoxTargetBulanan.Size = New System.Drawing.Size(149, 20)
        Me.TextBoxTargetBulanan.TabIndex = 0
        '
        'ComboBoxPoliTarget
        '
        Me.ComboBoxPoliTarget.FormattingEnabled = True
        Me.ComboBoxPoliTarget.Items.AddRange(New Object() {"POLI EYELASH & NAIL", "POLI FISIOTERAPI", "POLI HBOT", "POLI KECANTIKAN", "POLI REHABILITASI MEDIK"})
        Me.ComboBoxPoliTarget.Location = New System.Drawing.Point(45, 30)
        Me.ComboBoxPoliTarget.Name = "ComboBoxPoliTarget"
        Me.ComboBoxPoliTarget.Size = New System.Drawing.Size(159, 21)
        Me.ComboBoxPoliTarget.TabIndex = 458
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(24, 13)
        Me.Label3.TabIndex = 457
        Me.Label3.Text = "Poli"
        '
        'ButtonCetakCostumer
        '
        Me.ButtonCetakCostumer.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ButtonCetakCostumer.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonCetakCostumer.FlatAppearance.BorderSize = 0
        Me.ButtonCetakCostumer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonCetakCostumer.ForeColor = System.Drawing.Color.Black
        Me.ButtonCetakCostumer.Location = New System.Drawing.Point(327, 170)
        Me.ButtonCetakCostumer.Name = "ButtonCetakCostumer"
        Me.ButtonCetakCostumer.Size = New System.Drawing.Size(146, 23)
        Me.ButtonCetakCostumer.TabIndex = 458
        Me.ButtonCetakCostumer.Text = "Cetak Laporan Costumer"
        Me.ButtonCetakCostumer.UseVisualStyleBackColor = False
        '
        'ButtonKirimLaporan
        '
        Me.ButtonKirimLaporan.BackColor = System.Drawing.Color.Goldenrod
        Me.ButtonKirimLaporan.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonKirimLaporan.FlatAppearance.BorderSize = 0
        Me.ButtonKirimLaporan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonKirimLaporan.ForeColor = System.Drawing.Color.Black
        Me.ButtonKirimLaporan.Location = New System.Drawing.Point(217, 60)
        Me.ButtonKirimLaporan.Name = "ButtonKirimLaporan"
        Me.ButtonKirimLaporan.Size = New System.Drawing.Size(142, 23)
        Me.ButtonKirimLaporan.TabIndex = 459
        Me.ButtonKirimLaporan.Text = "Kirim Data"
        Me.ButtonKirimLaporan.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblStatusProgress)
        Me.GroupBox2.Controls.Add(Me.ProgressBar1)
        Me.GroupBox2.Controls.Add(Me.ButtonKirimLaporan)
        Me.GroupBox2.Location = New System.Drawing.Point(256, 218)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(365, 93)
        Me.GroupBox2.TabIndex = 460
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Kirim Web Dashboard"
        '
        'lblStatusProgress
        '
        Me.lblStatusProgress.AutoSize = True
        Me.lblStatusProgress.BackColor = System.Drawing.Color.Transparent
        Me.lblStatusProgress.Location = New System.Drawing.Point(6, 22)
        Me.lblStatusProgress.Name = "lblStatusProgress"
        Me.lblStatusProgress.Size = New System.Drawing.Size(10, 13)
        Me.lblStatusProgress.TabIndex = 461
        Me.lblStatusProgress.Text = "-"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(7, 38)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(352, 14)
        Me.ProgressBar1.TabIndex = 460
        '
        'FormRevenueSelling
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(633, 334)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.ButtonCetakCostumer)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ComboBoxPoli)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ButtonCetakLaporan)
        Me.Controls.Add(Me.ComboBoxTahun)
        Me.Controls.Add(Me.ComboBoxBulan)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtAkhir)
        Me.Controls.Add(Me.dtAwal)
        Me.Controls.Add(Me.RadioButtonTahunan)
        Me.Controls.Add(Me.RadioButtonBulanan)
        Me.Controls.Add(Me.RadioButtonHarian)
        Me.MaximumSize = New System.Drawing.Size(649, 373)
        Me.MinimumSize = New System.Drawing.Size(649, 373)
        Me.Name = "FormRevenueSelling"
        Me.Text = "FormRevenueSelling"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadioButtonHarian As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonBulanan As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonTahunan As System.Windows.Forms.RadioButton
    Friend WithEvents dtAwal As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtAkhir As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxBulan As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxTahun As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonCetakLaporan As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxPoli As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonSimpanTarget As System.Windows.Forms.Button
    Friend WithEvents TextBoxTargetBulanan As System.Windows.Forms.TextBox
    Friend WithEvents ComboBoxPoliTarget As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxHariKerja As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ButtonCetakCostumer As System.Windows.Forms.Button
    Friend WithEvents ButtonKirimLaporan As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents lblStatusProgress As System.Windows.Forms.Label
End Class
