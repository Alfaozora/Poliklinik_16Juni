<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormLaporanRiwayatAlat
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
        Me.TextBoxIdRuangan = New System.Windows.Forms.TextBox
        Me.Label59 = New System.Windows.Forms.Label
        Me.TextBoxNamaRuangan = New System.Windows.Forms.TextBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.ButtonCetakRiwayat = New System.Windows.Forms.Button
        Me.Label46 = New System.Windows.Forms.Label
        Me.Label45 = New System.Windows.Forms.Label
        Me.Label44 = New System.Windows.Forms.Label
        Me.dtAkhir = New System.Windows.Forms.DateTimePicker
        Me.dtAwal = New System.Windows.Forms.DateTimePicker
        Me.Label40 = New System.Windows.Forms.Label
        Me.TextBoxKodeAlat = New System.Windows.Forms.TextBox
        Me.CheckBoxAlatAktif = New System.Windows.Forms.CheckBox
        Me.TextBoxAlatId = New System.Windows.Forms.TextBox
        Me.Label39 = New System.Windows.Forms.Label
        Me.ButtonCariAlat = New System.Windows.Forms.Button
        Me.TextBoxNamaAlat = New System.Windows.Forms.TextBox
        Me.Label43 = New System.Windows.Forms.Label
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxIdRuangan
        '
        Me.TextBoxIdRuangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBoxIdRuangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxIdRuangan.Cursor = System.Windows.Forms.Cursors.No
        Me.TextBoxIdRuangan.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxIdRuangan.Location = New System.Drawing.Point(309, 131)
        Me.TextBoxIdRuangan.Name = "TextBoxIdRuangan"
        Me.TextBoxIdRuangan.ReadOnly = True
        Me.TextBoxIdRuangan.Size = New System.Drawing.Size(37, 23)
        Me.TextBoxIdRuangan.TabIndex = 651
        Me.TextBoxIdRuangan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label59.Location = New System.Drawing.Point(20, 136)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(102, 17)
        Me.Label59.TabIndex = 650
        Me.Label59.Text = "Nama Ruangan"
        '
        'TextBoxNamaRuangan
        '
        Me.TextBoxNamaRuangan.BackColor = System.Drawing.Color.White
        Me.TextBoxNamaRuangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxNamaRuangan.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBoxNamaRuangan.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxNamaRuangan.Location = New System.Drawing.Point(139, 131)
        Me.TextBoxNamaRuangan.Name = "TextBoxNamaRuangan"
        Me.TextBoxNamaRuangan.ReadOnly = True
        Me.TextBoxNamaRuangan.Size = New System.Drawing.Size(167, 23)
        Me.TextBoxNamaRuangan.TabIndex = 649
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.ButtonCetakRiwayat)
        Me.GroupBox3.Controls.Add(Me.Label46)
        Me.GroupBox3.Controls.Add(Me.Label45)
        Me.GroupBox3.Controls.Add(Me.Label44)
        Me.GroupBox3.Controls.Add(Me.dtAkhir)
        Me.GroupBox3.Controls.Add(Me.dtAwal)
        Me.GroupBox3.Location = New System.Drawing.Point(352, 48)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(301, 113)
        Me.GroupBox3.TabIndex = 647
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Periode Penggunaan Alat"
        '
        'ButtonCetakRiwayat
        '
        Me.ButtonCetakRiwayat.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonCetakRiwayat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonCetakRiwayat.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonCetakRiwayat.FlatAppearance.BorderSize = 0
        Me.ButtonCetakRiwayat.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonCetakRiwayat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonCetakRiwayat.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonCetakRiwayat.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonCetakRiwayat.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.ButtonCetakRiwayat.Location = New System.Drawing.Point(232, 78)
        Me.ButtonCetakRiwayat.Name = "ButtonCetakRiwayat"
        Me.ButtonCetakRiwayat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonCetakRiwayat.Size = New System.Drawing.Size(60, 25)
        Me.ButtonCetakRiwayat.TabIndex = 604
        Me.ButtonCetakRiwayat.Text = " Cetak"
        Me.ButtonCetakRiwayat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonCetakRiwayat.UseVisualStyleBackColor = False
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.Color.Transparent
        Me.Label46.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(164, 27)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(33, 13)
        Me.Label46.TabIndex = 457
        Me.Label46.Text = "From"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.Color.Transparent
        Me.Label45.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(6, 27)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(18, 13)
        Me.Label45.TabIndex = 456
        Me.Label45.Text = "To"
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.Color.Transparent
        Me.Label44.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(138, 48)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(23, 13)
        Me.Label44.TabIndex = 455
        Me.Label44.Text = "s/d"
        '
        'dtAkhir
        '
        Me.dtAkhir.CalendarFont = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtAkhir.CustomFormat = "dd-MMM-yyyy"
        Me.dtAkhir.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtAkhir.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtAkhir.Location = New System.Drawing.Point(167, 43)
        Me.dtAkhir.Name = "dtAkhir"
        Me.dtAkhir.Size = New System.Drawing.Size(125, 25)
        Me.dtAkhir.TabIndex = 454
        '
        'dtAwal
        '
        Me.dtAwal.CalendarFont = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtAwal.CustomFormat = "dd-MMM-yyyy"
        Me.dtAwal.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtAwal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtAwal.Location = New System.Drawing.Point(7, 43)
        Me.dtAwal.Name = "dtAwal"
        Me.dtAwal.Size = New System.Drawing.Size(125, 25)
        Me.dtAwal.TabIndex = 453
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label40.Location = New System.Drawing.Point(20, 61)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(70, 17)
        Me.Label40.TabIndex = 645
        Me.Label40.Text = "Kode Aset"
        '
        'TextBoxKodeAlat
        '
        Me.TextBoxKodeAlat.BackColor = System.Drawing.Color.White
        Me.TextBoxKodeAlat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxKodeAlat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBoxKodeAlat.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxKodeAlat.Location = New System.Drawing.Point(139, 62)
        Me.TextBoxKodeAlat.Name = "TextBoxKodeAlat"
        Me.TextBoxKodeAlat.ReadOnly = True
        Me.TextBoxKodeAlat.Size = New System.Drawing.Size(207, 23)
        Me.TextBoxKodeAlat.TabIndex = 646
        '
        'CheckBoxAlatAktif
        '
        Me.CheckBoxAlatAktif.AutoSize = True
        Me.CheckBoxAlatAktif.Checked = True
        Me.CheckBoxAlatAktif.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxAlatAktif.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxAlatAktif.Location = New System.Drawing.Point(298, 33)
        Me.CheckBoxAlatAktif.Name = "CheckBoxAlatAktif"
        Me.CheckBoxAlatAktif.Size = New System.Drawing.Size(51, 19)
        Me.CheckBoxAlatAktif.TabIndex = 644
        Me.CheckBoxAlatAktif.Text = "Aktif"
        Me.CheckBoxAlatAktif.UseVisualStyleBackColor = True
        '
        'TextBoxAlatId
        '
        Me.TextBoxAlatId.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBoxAlatId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxAlatId.Cursor = System.Windows.Forms.Cursors.No
        Me.TextBoxAlatId.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxAlatId.Location = New System.Drawing.Point(207, 30)
        Me.TextBoxAlatId.Name = "TextBoxAlatId"
        Me.TextBoxAlatId.ReadOnly = True
        Me.TextBoxAlatId.Size = New System.Drawing.Size(84, 23)
        Me.TextBoxAlatId.TabIndex = 640
        Me.TextBoxAlatId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label39.Location = New System.Drawing.Point(20, 97)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(72, 17)
        Me.Label39.TabIndex = 639
        Me.Label39.Text = "Nama Alat"
        '
        'ButtonCariAlat
        '
        Me.ButtonCariAlat.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonCariAlat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonCariAlat.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonCariAlat.FlatAppearance.BorderSize = 0
        Me.ButtonCariAlat.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonCariAlat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonCariAlat.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonCariAlat.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonCariAlat.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.ButtonCariAlat.Location = New System.Drawing.Point(139, 28)
        Me.ButtonCariAlat.Name = "ButtonCariAlat"
        Me.ButtonCariAlat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonCariAlat.Size = New System.Drawing.Size(60, 25)
        Me.ButtonCariAlat.TabIndex = 641
        Me.ButtonCariAlat.Text = " Cari"
        Me.ButtonCariAlat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonCariAlat.UseVisualStyleBackColor = False
        '
        'TextBoxNamaAlat
        '
        Me.TextBoxNamaAlat.BackColor = System.Drawing.Color.White
        Me.TextBoxNamaAlat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxNamaAlat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBoxNamaAlat.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxNamaAlat.Location = New System.Drawing.Point(139, 98)
        Me.TextBoxNamaAlat.Name = "TextBoxNamaAlat"
        Me.TextBoxNamaAlat.ReadOnly = True
        Me.TextBoxNamaAlat.Size = New System.Drawing.Size(207, 23)
        Me.TextBoxNamaAlat.TabIndex = 642
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label43.Location = New System.Drawing.Point(20, 30)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(49, 17)
        Me.Label43.TabIndex = 643
        Me.Label43.Text = "Alat ID"
        '
        'FormLaporanRiwayatAlat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(669, 203)
        Me.Controls.Add(Me.TextBoxIdRuangan)
        Me.Controls.Add(Me.Label59)
        Me.Controls.Add(Me.TextBoxNamaRuangan)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Label40)
        Me.Controls.Add(Me.TextBoxKodeAlat)
        Me.Controls.Add(Me.CheckBoxAlatAktif)
        Me.Controls.Add(Me.TextBoxAlatId)
        Me.Controls.Add(Me.Label39)
        Me.Controls.Add(Me.ButtonCariAlat)
        Me.Controls.Add(Me.TextBoxNamaAlat)
        Me.Controls.Add(Me.Label43)
        Me.MaximumSize = New System.Drawing.Size(685, 242)
        Me.MinimumSize = New System.Drawing.Size(685, 242)
        Me.Name = "FormLaporanRiwayatAlat"
        Me.Text = "FormLaporanRiwayatAlat"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxIdRuangan As System.Windows.Forms.TextBox
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNamaRuangan As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonCetakRiwayat As System.Windows.Forms.Button
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents dtAkhir As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtAwal As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents TextBoxKodeAlat As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxAlatAktif As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxAlatId As System.Windows.Forms.TextBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents ButtonCariAlat As System.Windows.Forms.Button
    Friend WithEvents TextBoxNamaAlat As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
End Class
