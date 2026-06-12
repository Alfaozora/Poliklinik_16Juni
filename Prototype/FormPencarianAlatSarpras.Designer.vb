<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPencarianAlatSarpras
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
        Me.TextBoxCariAlat = New System.Windows.Forms.TextBox
        Me.ButtonCariAlat = New System.Windows.Forms.Button
        Me.DataGridViewAlatSarpras = New System.Windows.Forms.DataGridView
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridViewAlatSarpras, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxCariAlat)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ButtonCariAlat)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridViewAlatSarpras)
        Me.SplitContainer1.Size = New System.Drawing.Size(824, 466)
        Me.SplitContainer1.SplitterDistance = 90
        Me.SplitContainer1.TabIndex = 0
        '
        'TextBoxCariAlat
        '
        Me.TextBoxCariAlat.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxCariAlat.Location = New System.Drawing.Point(93, 28)
        Me.TextBoxCariAlat.Multiline = True
        Me.TextBoxCariAlat.Name = "TextBoxCariAlat"
        Me.TextBoxCariAlat.Size = New System.Drawing.Size(380, 32)
        Me.TextBoxCariAlat.TabIndex = 1
        '
        'ButtonCariAlat
        '
        Me.ButtonCariAlat.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonCariAlat.FlatAppearance.BorderSize = 0
        Me.ButtonCariAlat.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonCariAlat.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCariAlat.ForeColor = System.Drawing.Color.White
        Me.ButtonCariAlat.Location = New System.Drawing.Point(22, 28)
        Me.ButtonCariAlat.Name = "ButtonCariAlat"
        Me.ButtonCariAlat.Size = New System.Drawing.Size(65, 32)
        Me.ButtonCariAlat.TabIndex = 0
        Me.ButtonCariAlat.Text = "Cari"
        Me.ButtonCariAlat.UseVisualStyleBackColor = False
        '
        'DataGridViewAlatSarpras
        '
        Me.DataGridViewAlatSarpras.AllowUserToAddRows = False
        Me.DataGridViewAlatSarpras.BackgroundColor = System.Drawing.Color.White
        Me.DataGridViewAlatSarpras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewAlatSarpras.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewAlatSarpras.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewAlatSarpras.Name = "DataGridViewAlatSarpras"
        Me.DataGridViewAlatSarpras.Size = New System.Drawing.Size(824, 372)
        Me.DataGridViewAlatSarpras.TabIndex = 0
        '
        'FormPencarianAlatSarpras
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(824, 466)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FormPencarianAlatSarpras"
        Me.Text = "FormPencarianAlatSarpras"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridViewAlatSarpras, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ButtonCariAlat As System.Windows.Forms.Button
    Friend WithEvents TextBoxCariAlat As System.Windows.Forms.TextBox
    Friend WithEvents DataGridViewAlatSarpras As System.Windows.Forms.DataGridView
End Class
