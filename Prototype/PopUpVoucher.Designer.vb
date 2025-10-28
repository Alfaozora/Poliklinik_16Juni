<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PopUpVoucher
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
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.DataGridVoucher = New System.Windows.Forms.DataGridView
        Me.DataGridRiwayat = New System.Windows.Forms.DataGridView
        Me.TextBoxKodeVoc = New System.Windows.Forms.TextBox
        Me.ButtonCariVoc = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ButtonSubmit = New System.Windows.Forms.Button
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.DataGridVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridRiwayat, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel2)
        Me.SplitContainer1.Size = New System.Drawing.Size(801, 435)
        Me.SplitContainer1.SplitterDistance = 380
        Me.SplitContainer1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel2.Controls.Add(Me.ButtonSubmit)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(801, 51)
        Me.Panel2.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 60)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.DataGridRiwayat)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.DataGridVoucher)
        Me.SplitContainer2.Size = New System.Drawing.Size(801, 320)
        Me.SplitContainer2.SplitterDistance = 267
        Me.SplitContainer2.TabIndex = 1
        '
        'DataGridVoucher
        '
        Me.DataGridVoucher.AllowUserToAddRows = False
        Me.DataGridVoucher.AllowUserToDeleteRows = False
        Me.DataGridVoucher.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        Me.DataGridVoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridVoucher.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridVoucher.Location = New System.Drawing.Point(0, 0)
        Me.DataGridVoucher.Name = "DataGridVoucher"
        Me.DataGridVoucher.Size = New System.Drawing.Size(530, 320)
        Me.DataGridVoucher.TabIndex = 0
        '
        'DataGridRiwayat
        '
        Me.DataGridRiwayat.AllowUserToAddRows = False
        Me.DataGridRiwayat.AllowUserToDeleteRows = False
        Me.DataGridRiwayat.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        Me.DataGridRiwayat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridRiwayat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridRiwayat.Location = New System.Drawing.Point(0, 0)
        Me.DataGridRiwayat.Name = "DataGridRiwayat"
        Me.DataGridRiwayat.ReadOnly = True
        Me.DataGridRiwayat.Size = New System.Drawing.Size(267, 320)
        Me.DataGridRiwayat.TabIndex = 0
        '
        'TextBoxKodeVoc
        '
        Me.TextBoxKodeVoc.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxKodeVoc.Location = New System.Drawing.Point(271, 18)
        Me.TextBoxKodeVoc.Multiline = True
        Me.TextBoxKodeVoc.Name = "TextBoxKodeVoc"
        Me.TextBoxKodeVoc.Size = New System.Drawing.Size(405, 25)
        Me.TextBoxKodeVoc.TabIndex = 2
        '
        'ButtonCariVoc
        '
        Me.ButtonCariVoc.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonCariVoc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonCariVoc.FlatAppearance.BorderSize = 2
        Me.ButtonCariVoc.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.ButtonCariVoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonCariVoc.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCariVoc.ForeColor = System.Drawing.Color.White
        Me.ButtonCariVoc.Location = New System.Drawing.Point(682, 18)
        Me.ButtonCariVoc.Name = "ButtonCariVoc"
        Me.ButtonCariVoc.Size = New System.Drawing.Size(107, 25)
        Me.ButtonCariVoc.TabIndex = 516
        Me.ButtonCariVoc.Text = "CARI"
        Me.ButtonCariVoc.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel1.Controls.Add(Me.ButtonCariVoc)
        Me.Panel1.Controls.Add(Me.TextBoxKodeVoc)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(801, 60)
        Me.Panel1.TabIndex = 0
        '
        'ButtonSubmit
        '
        Me.ButtonSubmit.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonSubmit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonSubmit.FlatAppearance.BorderSize = 2
        Me.ButtonSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.ButtonSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonSubmit.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSubmit.ForeColor = System.Drawing.Color.White
        Me.ButtonSubmit.Location = New System.Drawing.Point(682, 8)
        Me.ButtonSubmit.Name = "ButtonSubmit"
        Me.ButtonSubmit.Size = New System.Drawing.Size(107, 35)
        Me.ButtonSubmit.TabIndex = 517
        Me.ButtonSubmit.Text = "SUBMIT"
        Me.ButtonSubmit.UseVisualStyleBackColor = False
        '
        'PopUpVoucher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(801, 435)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "PopUpVoucher"
        Me.Text = "PopUpVoucher"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.DataGridVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridRiwayat, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridRiwayat As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridVoucher As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCariVoc As System.Windows.Forms.Button
    Friend WithEvents TextBoxKodeVoc As System.Windows.Forms.TextBox
    Friend WithEvents ButtonSubmit As System.Windows.Forms.Button
End Class
