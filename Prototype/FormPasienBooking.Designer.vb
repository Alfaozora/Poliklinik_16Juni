<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPasienBooking
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
        Me.ButtonSubmit = New System.Windows.Forms.Button
        Me.DataGridView = New System.Windows.Forms.DataGridView
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridView)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.ButtonSubmit)
        Me.SplitContainer1.Size = New System.Drawing.Size(861, 368)
        Me.SplitContainer1.SplitterDistance = 300
        Me.SplitContainer1.TabIndex = 0
        '
        'ButtonSubmit
        '
        Me.ButtonSubmit.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonSubmit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonSubmit.FlatAppearance.BorderSize = 0
        Me.ButtonSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonSubmit.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonSubmit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.ButtonSubmit.Location = New System.Drawing.Point(12, 20)
        Me.ButtonSubmit.Name = "ButtonSubmit"
        Me.ButtonSubmit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonSubmit.Size = New System.Drawing.Size(77, 25)
        Me.ButtonSubmit.TabIndex = 607
        Me.ButtonSubmit.Text = "Submit"
        Me.ButtonSubmit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonSubmit.UseVisualStyleBackColor = False
        '
        'DataGridView
        '
        Me.DataGridView.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView.Name = "DataGridView"
        Me.DataGridView.Size = New System.Drawing.Size(861, 300)
        Me.DataGridView.TabIndex = 0
        '
        'FormPasienBooking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(861, 368)
        Me.Controls.Add(Me.SplitContainer1)
        Me.MaximumSize = New System.Drawing.Size(877, 407)
        Me.MinimumSize = New System.Drawing.Size(877, 407)
        Me.Name = "FormPasienBooking"
        Me.Text = "FormPasienBooking"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents ButtonSubmit As System.Windows.Forms.Button
End Class
