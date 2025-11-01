<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormKodeVoucher
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormKodeVoucher))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.ButtonGenerateVoc = New System.Windows.Forms.Button
        Me.ButtonFind = New System.Windows.Forms.Button
        Me.Label10 = New System.Windows.Forms.Label
        Me.TextBoxTindakan = New System.Windows.Forms.TextBox
        Me.ButtonSimpanVoc = New System.Windows.Forms.Button
        Me.Label9 = New System.Windows.Forms.Label
        Me.TextBoxNominalVoc = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.ComboBoxNominalVoc = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.ButtonGenerate = New System.Windows.Forms.Button
        Me.ComboBoxJenisVoc = New System.Windows.Forms.ComboBox
        Me.TextBoxKuotaVoc = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TextBoxMinVoc = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.dtAkhir = New System.Windows.Forms.DateTimePicker
        Me.Label4 = New System.Windows.Forms.Label
        Me.dtAwal = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.TextBoxKodeVoc = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TextBoxNamaVoc = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.Color.White
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ButtonGenerateVoc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ButtonFind)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label10)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxTindakan)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ButtonSimpanVoc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label9)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxNominalVoc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ComboBoxNominalVoc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ButtonGenerate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ComboBoxJenisVoc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxKuotaVoc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxMinVoc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtAkhir)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtAwal)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxKodeVoc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxNamaVoc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridView1)
        Me.SplitContainer1.Size = New System.Drawing.Size(950, 455)
        Me.SplitContainer1.SplitterDistance = 180
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 0
        '
        'ButtonGenerateVoc
        '
        Me.ButtonGenerateVoc.BackColor = System.Drawing.Color.White
        Me.ButtonGenerateVoc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonGenerateVoc.FlatAppearance.BorderSize = 2
        Me.ButtonGenerateVoc.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.ButtonGenerateVoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonGenerateVoc.ForeColor = System.Drawing.Color.Black
        Me.ButtonGenerateVoc.Location = New System.Drawing.Point(129, 136)
        Me.ButtonGenerateVoc.Name = "ButtonGenerateVoc"
        Me.ButtonGenerateVoc.Size = New System.Drawing.Size(107, 35)
        Me.ButtonGenerateVoc.TabIndex = 520
        Me.ButtonGenerateVoc.Text = "GENERATE"
        Me.ButtonGenerateVoc.UseVisualStyleBackColor = False
        '
        'ButtonFind
        '
        Me.ButtonFind.Enabled = False
        Me.ButtonFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonFind.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonFind.Location = New System.Drawing.Point(588, 98)
        Me.ButtonFind.Name = "ButtonFind"
        Me.ButtonFind.Size = New System.Drawing.Size(35, 25)
        Me.ButtonFind.TabIndex = 519
        Me.ButtonFind.Text = "..."
        Me.ToolTip1.SetToolTip(Me.ButtonFind, "Cari Tindakan")
        Me.ButtonFind.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(454, 78)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(60, 17)
        Me.Label10.TabIndex = 518
        Me.Label10.Text = "Tindakan"
        '
        'TextBoxTindakan
        '
        Me.TextBoxTindakan.Enabled = False
        Me.TextBoxTindakan.Location = New System.Drawing.Point(457, 98)
        Me.TextBoxTindakan.Name = "TextBoxTindakan"
        Me.TextBoxTindakan.Size = New System.Drawing.Size(125, 22)
        Me.TextBoxTindakan.TabIndex = 517
        Me.TextBoxTindakan.WordWrap = False
        '
        'ButtonSimpanVoc
        '
        Me.ButtonSimpanVoc.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonSimpanVoc.FlatAppearance.BorderSize = 0
        Me.ButtonSimpanVoc.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.ButtonSimpanVoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonSimpanVoc.ForeColor = System.Drawing.Color.White
        Me.ButtonSimpanVoc.Location = New System.Drawing.Point(16, 136)
        Me.ButtonSimpanVoc.Name = "ButtonSimpanVoc"
        Me.ButtonSimpanVoc.Size = New System.Drawing.Size(107, 35)
        Me.ButtonSimpanVoc.TabIndex = 515
        Me.ButtonSimpanVoc.Text = "SIMPAN"
        Me.ButtonSimpanVoc.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(779, 78)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(85, 17)
        Me.Label9.TabIndex = 514
        Me.Label9.Text = "Nominal Disc"
        '
        'TextBoxNominalVoc
        '
        Me.TextBoxNominalVoc.Location = New System.Drawing.Point(782, 98)
        Me.TextBoxNominalVoc.Multiline = True
        Me.TextBoxNominalVoc.Name = "TextBoxNominalVoc"
        Me.TextBoxNominalVoc.Size = New System.Drawing.Size(138, 25)
        Me.TextBoxNominalVoc.TabIndex = 513
        Me.TextBoxNominalVoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(630, 78)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(89, 17)
        Me.Label8.TabIndex = 512
        Me.Label8.Text = "Jenis Nominal"
        '
        'ComboBoxNominalVoc
        '
        Me.ComboBoxNominalVoc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.ComboBoxNominalVoc.BackColor = System.Drawing.SystemColors.Window
        Me.ComboBoxNominalVoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxNominalVoc.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.ComboBoxNominalVoc.FormattingEnabled = True
        Me.ComboBoxNominalVoc.Items.AddRange(New Object() {"NOMINAL", "PERCENT"})
        Me.ComboBoxNominalVoc.Location = New System.Drawing.Point(633, 98)
        Me.ComboBoxNominalVoc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ComboBoxNominalVoc.Name = "ComboBoxNominalVoc"
        Me.ComboBoxNominalVoc.Size = New System.Drawing.Size(143, 25)
        Me.ComboBoxNominalVoc.TabIndex = 511
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(305, 78)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(84, 17)
        Me.Label7.TabIndex = 510
        Me.Label7.Text = "Tipe Voucher"
        '
        'ButtonGenerate
        '
        Me.ButtonGenerate.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ButtonGenerate.BackgroundImage = CType(resources.GetObject("ButtonGenerate.BackgroundImage"), System.Drawing.Image)
        Me.ButtonGenerate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ButtonGenerate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonGenerate.Location = New System.Drawing.Point(392, 32)
        Me.ButtonGenerate.Name = "ButtonGenerate"
        Me.ButtonGenerate.Size = New System.Drawing.Size(32, 25)
        Me.ButtonGenerate.TabIndex = 509
        Me.ButtonGenerate.Tag = ""
        Me.ToolTip1.SetToolTip(Me.ButtonGenerate, "Generate Kode Voucher")
        Me.ButtonGenerate.UseVisualStyleBackColor = False
        '
        'ComboBoxJenisVoc
        '
        Me.ComboBoxJenisVoc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.ComboBoxJenisVoc.BackColor = System.Drawing.SystemColors.Window
        Me.ComboBoxJenisVoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxJenisVoc.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.ComboBoxJenisVoc.FormattingEnabled = True
        Me.ComboBoxJenisVoc.Items.AddRange(New Object() {"Voucher Nominal", "Voucher Tindakan"})
        Me.ComboBoxJenisVoc.Location = New System.Drawing.Point(308, 98)
        Me.ComboBoxJenisVoc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ComboBoxJenisVoc.Name = "ComboBoxJenisVoc"
        Me.ComboBoxJenisVoc.Size = New System.Drawing.Size(143, 25)
        Me.ComboBoxJenisVoc.TabIndex = 508
        '
        'TextBoxKuotaVoc
        '
        Me.TextBoxKuotaVoc.Location = New System.Drawing.Point(164, 98)
        Me.TextBoxKuotaVoc.Multiline = True
        Me.TextBoxKuotaVoc.Name = "TextBoxKuotaVoc"
        Me.TextBoxKuotaVoc.Size = New System.Drawing.Size(138, 25)
        Me.TextBoxKuotaVoc.TabIndex = 455
        Me.TextBoxKuotaVoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(161, 78)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(108, 17)
        Me.Label6.TabIndex = 454
        Me.Label6.Text = "Kuota Pemakaian"
        '
        'TextBoxMinVoc
        '
        Me.TextBoxMinVoc.Location = New System.Drawing.Point(16, 98)
        Me.TextBoxMinVoc.Multiline = True
        Me.TextBoxMinVoc.Name = "TextBoxMinVoc"
        Me.TextBoxMinVoc.Size = New System.Drawing.Size(138, 25)
        Me.TextBoxMinVoc.TabIndex = 453
        Me.TextBoxMinVoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(13, 78)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(126, 17)
        Me.Label5.TabIndex = 452
        Me.Label5.Text = "Minimum Pembelian"
        '
        'dtAkhir
        '
        Me.dtAkhir.CalendarFont = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtAkhir.CustomFormat = "dd-MMM-yyyy"
        Me.dtAkhir.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtAkhir.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtAkhir.Location = New System.Drawing.Point(621, 32)
        Me.dtAkhir.Name = "dtAkhir"
        Me.dtAkhir.Size = New System.Drawing.Size(138, 25)
        Me.dtAkhir.TabIndex = 451
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(618, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(141, 17)
        Me.Label4.TabIndex = 450
        Me.Label4.Text = "Tanggal Berlaku (Akhir)"
        '
        'dtAwal
        '
        Me.dtAwal.CalendarFont = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtAwal.CustomFormat = "dd-MMM-yyyy"
        Me.dtAwal.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtAwal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtAwal.Location = New System.Drawing.Point(436, 32)
        Me.dtAwal.Name = "dtAwal"
        Me.dtAwal.Size = New System.Drawing.Size(170, 25)
        Me.dtAwal.TabIndex = 449
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(433, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(139, 17)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Tanggal Berlaku (Awal)"
        '
        'TextBoxKodeVoc
        '
        Me.TextBoxKodeVoc.Location = New System.Drawing.Point(192, 32)
        Me.TextBoxKodeVoc.Multiline = True
        Me.TextBoxKodeVoc.Name = "TextBoxKodeVoc"
        Me.TextBoxKodeVoc.Size = New System.Drawing.Size(188, 25)
        Me.TextBoxKodeVoc.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(189, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Kode Voucher"
        '
        'TextBoxNamaVoc
        '
        Me.TextBoxNamaVoc.Location = New System.Drawing.Point(16, 32)
        Me.TextBoxNamaVoc.Multiline = True
        Me.TextBoxNamaVoc.Name = "TextBoxNamaVoc"
        Me.TextBoxNamaVoc.Size = New System.Drawing.Size(170, 25)
        Me.TextBoxNamaVoc.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nama Voucher"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(950, 270)
        Me.DataGridView1.TabIndex = 0
        '
        'FormKodeVoucher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(950, 455)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FormKodeVoucher"
        Me.Text = "FormKodeVoucher"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TextBoxKodeVoc As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNamaVoc As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtAwal As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtAkhir As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxMinVoc As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxKuotaVoc As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxJenisVoc As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonGenerate As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxNominalVoc As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonSimpanVoc As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNominalVoc As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTindakan As System.Windows.Forms.TextBox
    Friend WithEvents ButtonFind As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ButtonGenerateVoc As System.Windows.Forms.Button
End Class
