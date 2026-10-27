<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPenerimaanBarang
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.ButtonAmbilData = New System.Windows.Forms.Button
        Me.TextBoxIdRuangan = New System.Windows.Forms.TextBox
        Me.ComboBoxNamaAlat = New System.Windows.Forms.ComboBox
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.DataGridViewBarang = New System.Windows.Forms.DataGridView
        Me.CheckPersetujuan = New System.Windows.Forms.CheckBox
        Me.LabelSaldoAlat = New System.Windows.Forms.Label
        Me.LabelDipilih = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.ButtonSubmit = New System.Windows.Forms.Button
        Me.LabelSelisih = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.DataGridViewBarang, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ButtonAmbilData)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxIdRuangan)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ComboBoxNamaAlat)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(786, 462)
        Me.SplitContainer1.SplitterDistance = 56
        Me.SplitContainer1.TabIndex = 0
        '
        'ButtonAmbilData
        '
        Me.ButtonAmbilData.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ButtonAmbilData.FlatAppearance.BorderSize = 0
        Me.ButtonAmbilData.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonAmbilData.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonAmbilData.Location = New System.Drawing.Point(268, 16)
        Me.ButtonAmbilData.Name = "ButtonAmbilData"
        Me.ButtonAmbilData.Size = New System.Drawing.Size(101, 25)
        Me.ButtonAmbilData.TabIndex = 655
        Me.ButtonAmbilData.Text = "Ambil Data"
        Me.ButtonAmbilData.UseVisualStyleBackColor = False
        '
        'TextBoxIdRuangan
        '
        Me.TextBoxIdRuangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBoxIdRuangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxIdRuangan.Cursor = System.Windows.Forms.Cursors.No
        Me.TextBoxIdRuangan.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxIdRuangan.Location = New System.Drawing.Point(225, 16)
        Me.TextBoxIdRuangan.Multiline = True
        Me.TextBoxIdRuangan.Name = "TextBoxIdRuangan"
        Me.TextBoxIdRuangan.ReadOnly = True
        Me.TextBoxIdRuangan.Size = New System.Drawing.Size(37, 25)
        Me.TextBoxIdRuangan.TabIndex = 654
        Me.TextBoxIdRuangan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ComboBoxNamaAlat
        '
        Me.ComboBoxNamaAlat.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxNamaAlat.FormattingEnabled = True
        Me.ComboBoxNamaAlat.Location = New System.Drawing.Point(12, 16)
        Me.ComboBoxNamaAlat.Name = "ComboBoxNamaAlat"
        Me.ComboBoxNamaAlat.Size = New System.Drawing.Size(207, 25)
        Me.ComboBoxNamaAlat.TabIndex = 653
        Me.ComboBoxNamaAlat.TabStop = False
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.DataGridViewBarang)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.LabelSelisih)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label6)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label7)
        Me.SplitContainer2.Panel2.Controls.Add(Me.CheckPersetujuan)
        Me.SplitContainer2.Panel2.Controls.Add(Me.LabelSaldoAlat)
        Me.SplitContainer2.Panel2.Controls.Add(Me.LabelDipilih)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label4)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label3)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer2.Panel2.Controls.Add(Me.ButtonSubmit)
        Me.SplitContainer2.Size = New System.Drawing.Size(786, 402)
        Me.SplitContainer2.SplitterDistance = 298
        Me.SplitContainer2.TabIndex = 0
        '
        'DataGridViewBarang
        '
        Me.DataGridViewBarang.AllowUserToAddRows = False
        Me.DataGridViewBarang.AllowUserToDeleteRows = False
        Me.DataGridViewBarang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridViewBarang.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.DataGridViewBarang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewBarang.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewBarang.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewBarang.Name = "DataGridViewBarang"
        Me.DataGridViewBarang.Size = New System.Drawing.Size(786, 298)
        Me.DataGridViewBarang.TabIndex = 0
        '
        'CheckPersetujuan
        '
        Me.CheckPersetujuan.AutoSize = True
        Me.CheckPersetujuan.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckPersetujuan.Location = New System.Drawing.Point(25, 67)
        Me.CheckPersetujuan.Name = "CheckPersetujuan"
        Me.CheckPersetujuan.Size = New System.Drawing.Size(242, 19)
        Me.CheckPersetujuan.TabIndex = 8
        Me.CheckPersetujuan.Text = "Saya menyetujui penambahan kuota alat"
        Me.CheckPersetujuan.UseVisualStyleBackColor = True
        '
        'LabelSaldoAlat
        '
        Me.LabelSaldoAlat.AutoSize = True
        Me.LabelSaldoAlat.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelSaldoAlat.Location = New System.Drawing.Point(106, 42)
        Me.LabelSaldoAlat.Name = "LabelSaldoAlat"
        Me.LabelSaldoAlat.Size = New System.Drawing.Size(13, 15)
        Me.LabelSaldoAlat.TabIndex = 7
        Me.LabelSaldoAlat.Text = "1"
        '
        'LabelDipilih
        '
        Me.LabelDipilih.AutoSize = True
        Me.LabelDipilih.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDipilih.Location = New System.Drawing.Point(106, 16)
        Me.LabelDipilih.Name = "LabelDipilih"
        Me.LabelDipilih.Size = New System.Drawing.Size(13, 15)
        Me.LabelDipilih.TabIndex = 6
        Me.LabelDipilih.Text = "1"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(98, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 15)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = ":"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(98, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 15)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = ":"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(22, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Saldo Fisik"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(22, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 15)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Total Dipilih"
        '
        'ButtonSubmit
        '
        Me.ButtonSubmit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ButtonSubmit.FlatAppearance.BorderSize = 0
        Me.ButtonSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonSubmit.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSubmit.Location = New System.Drawing.Point(632, 57)
        Me.ButtonSubmit.Name = "ButtonSubmit"
        Me.ButtonSubmit.Size = New System.Drawing.Size(142, 29)
        Me.ButtonSubmit.TabIndex = 1
        Me.ButtonSubmit.Text = "Setujui dan Proses"
        Me.ButtonSubmit.UseVisualStyleBackColor = False
        '
        'LabelSelisih
        '
        Me.LabelSelisih.AutoSize = True
        Me.LabelSelisih.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelSelisih.Location = New System.Drawing.Point(242, 16)
        Me.LabelSelisih.Name = "LabelSelisih"
        Me.LabelSelisih.Size = New System.Drawing.Size(13, 15)
        Me.LabelSelisih.TabIndex = 11
        Me.LabelSelisih.Text = "1"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(234, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(10, 15)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = ":"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(195, 15)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(40, 15)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Selisih"
        '
        'FormPenerimaanBarang
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(786, 462)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FormPenerimaanBarang"
        Me.Text = "FormPenerimaanBarang"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.DataGridViewBarang, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridViewBarang As System.Windows.Forms.DataGridView
    Friend WithEvents ButtonSubmit As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CheckPersetujuan As System.Windows.Forms.CheckBox
    Friend WithEvents LabelSaldoAlat As System.Windows.Forms.Label
    Friend WithEvents LabelDipilih As System.Windows.Forms.Label
    Friend WithEvents ComboBoxNamaAlat As System.Windows.Forms.ComboBox
    Friend WithEvents TextBoxIdRuangan As System.Windows.Forms.TextBox
    Friend WithEvents ButtonAmbilData As System.Windows.Forms.Button
    Friend WithEvents LabelSelisih As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
End Class
