<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPreOrderAlat
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
        Me.DtPermintaan = New System.Windows.Forms.DateTimePicker
        Me.DtDibutuhkan = New System.Windows.Forms.DateTimePicker
        Me.ComboBoxJenisPermintaan = New System.Windows.Forms.ComboBox
        Me.ComboBoxJenisPembayaran = New System.Windows.Forms.ComboBox
        Me.ButtonGetSupplier = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.TextBoxPay = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.TextBoxAlamatSupp = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TextBoxNamaSupp = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.TextBoxBank = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.TextBoxNamaRek = New System.Windows.Forms.TextBox
        Me.TextBoxNoRek = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.TextBoxTotalHarga = New System.Windows.Forms.TextBox
        Me.TextBoxHarga = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.TextBoxSatuan = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.TextBoxQty = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.TextBoxKodeItem = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.TextBoxId = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.ButtonCariAlat = New System.Windows.Forms.Button
        Me.Label17 = New System.Windows.Forms.Label
        Me.TextBoxDeskripsi = New System.Windows.Forms.TextBox
        Me.ButtonSubmit = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'DtPermintaan
        '
        Me.DtPermintaan.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtPermintaan.Location = New System.Drawing.Point(147, 20)
        Me.DtPermintaan.Name = "DtPermintaan"
        Me.DtPermintaan.Size = New System.Drawing.Size(121, 20)
        Me.DtPermintaan.TabIndex = 641
        '
        'DtDibutuhkan
        '
        Me.DtDibutuhkan.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtDibutuhkan.Location = New System.Drawing.Point(147, 64)
        Me.DtDibutuhkan.Name = "DtDibutuhkan"
        Me.DtDibutuhkan.Size = New System.Drawing.Size(120, 20)
        Me.DtDibutuhkan.TabIndex = 643
        '
        'ComboBoxJenisPermintaan
        '
        Me.ComboBoxJenisPermintaan.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxJenisPermintaan.FormattingEnabled = True
        Me.ComboBoxJenisPermintaan.Items.AddRange(New Object() {"BARU", "PENGGANTI"})
        Me.ComboBoxJenisPermintaan.Location = New System.Drawing.Point(401, 20)
        Me.ComboBoxJenisPermintaan.Name = "ComboBoxJenisPermintaan"
        Me.ComboBoxJenisPermintaan.Size = New System.Drawing.Size(121, 25)
        Me.ComboBoxJenisPermintaan.TabIndex = 653
        '
        'ComboBoxJenisPembayaran
        '
        Me.ComboBoxJenisPembayaran.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxJenisPembayaran.FormattingEnabled = True
        Me.ComboBoxJenisPembayaran.Items.AddRange(New Object() {"CASH", "CREDIT", "INSTALLMENT"})
        Me.ComboBoxJenisPembayaran.Location = New System.Drawing.Point(401, 59)
        Me.ComboBoxJenisPembayaran.Name = "ComboBoxJenisPembayaran"
        Me.ComboBoxJenisPembayaran.Size = New System.Drawing.Size(121, 25)
        Me.ComboBoxJenisPembayaran.TabIndex = 655
        '
        'ButtonGetSupplier
        '
        Me.ButtonGetSupplier.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonGetSupplier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonGetSupplier.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonGetSupplier.FlatAppearance.BorderSize = 0
        Me.ButtonGetSupplier.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonGetSupplier.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonGetSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonGetSupplier.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonGetSupplier.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.ButtonGetSupplier.Location = New System.Drawing.Point(10, 19)
        Me.ButtonGetSupplier.Name = "ButtonGetSupplier"
        Me.ButtonGetSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonGetSupplier.Size = New System.Drawing.Size(93, 25)
        Me.ButtonGetSupplier.TabIndex = 664
        Me.ButtonGetSupplier.Text = "Cari Supplier"
        Me.ButtonGetSupplier.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonGetSupplier.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.TextBoxNoRek)
        Me.GroupBox1.Controls.Add(Me.TextBoxNamaRek)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.TextBoxBank)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.TextBoxPay)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.TextBoxAlamatSupp)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.TextBoxNamaSupp)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.ButtonGetSupplier)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 113)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(586, 195)
        Me.GroupBox1.TabIndex = 665
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Supplier"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(160, 126)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(35, 15)
        Me.Label8.TabIndex = 674
        Me.Label8.Text = "Bank"
        '
        'TextBoxPay
        '
        Me.TextBoxPay.BackColor = System.Drawing.Color.PaleGreen
        Me.TextBoxPay.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxPay.ForeColor = System.Drawing.Color.Black
        Me.TextBoxPay.Location = New System.Drawing.Point(58, 125)
        Me.TextBoxPay.Multiline = True
        Me.TextBoxPay.Name = "TextBoxPay"
        Me.TextBoxPay.ReadOnly = True
        Me.TextBoxPay.Size = New System.Drawing.Size(99, 24)
        Me.TextBoxPay.TabIndex = 673
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(13, 126)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(27, 15)
        Me.Label7.TabIndex = 672
        Me.Label7.Text = "Pay"
        '
        'TextBoxAlamatSupp
        '
        Me.TextBoxAlamatSupp.BackColor = System.Drawing.Color.PaleGreen
        Me.TextBoxAlamatSupp.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxAlamatSupp.Location = New System.Drawing.Point(344, 56)
        Me.TextBoxAlamatSupp.Multiline = True
        Me.TextBoxAlamatSupp.Name = "TextBoxAlamatSupp"
        Me.TextBoxAlamatSupp.ReadOnly = True
        Me.TextBoxAlamatSupp.Size = New System.Drawing.Size(229, 55)
        Me.TextBoxAlamatSupp.TabIndex = 671
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(293, 57)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 15)
        Me.Label6.TabIndex = 670
        Me.Label6.Text = "Alamat"
        '
        'TextBoxNamaSupp
        '
        Me.TextBoxNamaSupp.BackColor = System.Drawing.Color.PaleGreen
        Me.TextBoxNamaSupp.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxNamaSupp.Location = New System.Drawing.Point(58, 56)
        Me.TextBoxNamaSupp.Multiline = True
        Me.TextBoxNamaSupp.Name = "TextBoxNamaSupp"
        Me.TextBoxNamaSupp.ReadOnly = True
        Me.TextBoxNamaSupp.Size = New System.Drawing.Size(229, 55)
        Me.TextBoxNamaSupp.TabIndex = 669
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(11, 57)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 15)
        Me.Label5.TabIndex = 668
        Me.Label5.Text = "Nama"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 15)
        Me.Label1.TabIndex = 666
        Me.Label1.Text = "Tanggal Permintaan"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 65)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(118, 15)
        Me.Label2.TabIndex = 667
        Me.Label2.Text = "Tanggal Dibutuhkan"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(274, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(103, 15)
        Me.Label3.TabIndex = 668
        Me.Label3.Text = "Jenis Permintaan"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(274, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(109, 15)
        Me.Label4.TabIndex = 669
        Me.Label4.Text = "Jenis Pembayaran"
        '
        'TextBoxBank
        '
        Me.TextBoxBank.BackColor = System.Drawing.Color.PaleGreen
        Me.TextBoxBank.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxBank.ForeColor = System.Drawing.Color.Black
        Me.TextBoxBank.Location = New System.Drawing.Point(197, 126)
        Me.TextBoxBank.Multiline = True
        Me.TextBoxBank.Name = "TextBoxBank"
        Me.TextBoxBank.ReadOnly = True
        Me.TextBoxBank.Size = New System.Drawing.Size(90, 24)
        Me.TextBoxBank.TabIndex = 675
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(301, 128)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 15)
        Me.Label9.TabIndex = 676
        Me.Label9.Text = "Name"
        '
        'TextBoxNamaRek
        '
        Me.TextBoxNamaRek.BackColor = System.Drawing.Color.PaleGreen
        Me.TextBoxNamaRek.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxNamaRek.ForeColor = System.Drawing.Color.Black
        Me.TextBoxNamaRek.Location = New System.Drawing.Point(344, 126)
        Me.TextBoxNamaRek.Multiline = True
        Me.TextBoxNamaRek.Name = "TextBoxNamaRek"
        Me.TextBoxNamaRek.ReadOnly = True
        Me.TextBoxNamaRek.Size = New System.Drawing.Size(229, 24)
        Me.TextBoxNamaRek.TabIndex = 677
        '
        'TextBoxNoRek
        '
        Me.TextBoxNoRek.BackColor = System.Drawing.Color.PaleGreen
        Me.TextBoxNoRek.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxNoRek.ForeColor = System.Drawing.Color.Black
        Me.TextBoxNoRek.Location = New System.Drawing.Point(58, 161)
        Me.TextBoxNoRek.Multiline = True
        Me.TextBoxNoRek.Name = "TextBoxNoRek"
        Me.TextBoxNoRek.ReadOnly = True
        Me.TextBoxNoRek.Size = New System.Drawing.Size(229, 24)
        Me.TextBoxNoRek.TabIndex = 678
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(17, 165)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(23, 15)
        Me.Label10.TabIndex = 679
        Me.Label10.Text = "No"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TextBoxDeskripsi)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.TextBoxTotalHarga)
        Me.GroupBox2.Controls.Add(Me.TextBoxHarga)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.TextBoxSatuan)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.TextBoxQty)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.TextBoxKodeItem)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.TextBoxId)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.ButtonCariAlat)
        Me.GroupBox2.Location = New System.Drawing.Point(15, 314)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(586, 171)
        Me.GroupBox2.TabIndex = 670
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Item"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(199, 97)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(71, 15)
        Me.Label11.TabIndex = 679
        Me.Label11.Text = "Total Harga"
        '
        'TextBoxTotalHarga
        '
        Me.TextBoxTotalHarga.Location = New System.Drawing.Point(276, 97)
        Me.TextBoxTotalHarga.Multiline = True
        Me.TextBoxTotalHarga.Name = "TextBoxTotalHarga"
        Me.TextBoxTotalHarga.Size = New System.Drawing.Size(144, 24)
        Me.TextBoxTotalHarga.TabIndex = 678
        '
        'TextBoxHarga
        '
        Me.TextBoxHarga.Location = New System.Drawing.Point(58, 95)
        Me.TextBoxHarga.Multiline = True
        Me.TextBoxHarga.Name = "TextBoxHarga"
        Me.TextBoxHarga.Size = New System.Drawing.Size(135, 24)
        Me.TextBoxHarga.TabIndex = 677
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(11, 96)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(41, 15)
        Me.Label12.TabIndex = 676
        Me.Label12.Text = "Harga"
        '
        'TextBoxSatuan
        '
        Me.TextBoxSatuan.Location = New System.Drawing.Point(478, 57)
        Me.TextBoxSatuan.Multiline = True
        Me.TextBoxSatuan.Name = "TextBoxSatuan"
        Me.TextBoxSatuan.Size = New System.Drawing.Size(90, 24)
        Me.TextBoxSatuan.TabIndex = 675
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(426, 57)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(46, 15)
        Me.Label13.TabIndex = 674
        Me.Label13.Text = "Satuan"
        '
        'TextBoxQty
        '
        Me.TextBoxQty.Location = New System.Drawing.Point(323, 56)
        Me.TextBoxQty.Multiline = True
        Me.TextBoxQty.Name = "TextBoxQty"
        Me.TextBoxQty.Size = New System.Drawing.Size(97, 24)
        Me.TextBoxQty.TabIndex = 673
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(293, 57)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(24, 15)
        Me.Label14.TabIndex = 672
        Me.Label14.Text = "Qty"
        '
        'TextBoxKodeItem
        '
        Me.TextBoxKodeItem.Location = New System.Drawing.Point(191, 56)
        Me.TextBoxKodeItem.Multiline = True
        Me.TextBoxKodeItem.Name = "TextBoxKodeItem"
        Me.TextBoxKodeItem.Size = New System.Drawing.Size(96, 24)
        Me.TextBoxKodeItem.TabIndex = 671
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(122, 57)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(63, 15)
        Me.Label15.TabIndex = 670
        Me.Label15.Text = "Kode Item"
        '
        'TextBoxId
        '
        Me.TextBoxId.Location = New System.Drawing.Point(58, 56)
        Me.TextBoxId.Multiline = True
        Me.TextBoxId.Name = "TextBoxId"
        Me.TextBoxId.Size = New System.Drawing.Size(58, 24)
        Me.TextBoxId.TabIndex = 669
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(11, 57)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(17, 15)
        Me.Label16.TabIndex = 668
        Me.Label16.Text = "Id"
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
        Me.ButtonCariAlat.Location = New System.Drawing.Point(10, 19)
        Me.ButtonCariAlat.Name = "ButtonCariAlat"
        Me.ButtonCariAlat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonCariAlat.Size = New System.Drawing.Size(93, 25)
        Me.ButtonCariAlat.TabIndex = 664
        Me.ButtonCariAlat.Text = "Cari Item"
        Me.ButtonCariAlat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonCariAlat.UseVisualStyleBackColor = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(11, 133)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(58, 15)
        Me.Label17.TabIndex = 680
        Me.Label17.Text = "Deskripsi"
        '
        'TextBoxDeskripsi
        '
        Me.TextBoxDeskripsi.Location = New System.Drawing.Point(75, 132)
        Me.TextBoxDeskripsi.Multiline = True
        Me.TextBoxDeskripsi.Name = "TextBoxDeskripsi"
        Me.TextBoxDeskripsi.Size = New System.Drawing.Size(345, 24)
        Me.TextBoxDeskripsi.TabIndex = 681
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
        Me.ButtonSubmit.Location = New System.Drawing.Point(484, 497)
        Me.ButtonSubmit.Name = "ButtonSubmit"
        Me.ButtonSubmit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonSubmit.Size = New System.Drawing.Size(114, 28)
        Me.ButtonSubmit.TabIndex = 671
        Me.ButtonSubmit.Text = "Submit"
        Me.ButtonSubmit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonSubmit.UseVisualStyleBackColor = False
        '
        'FormPreOrderAlat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(610, 534)
        Me.Controls.Add(Me.ButtonSubmit)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ComboBoxJenisPembayaran)
        Me.Controls.Add(Me.ComboBoxJenisPermintaan)
        Me.Controls.Add(Me.DtDibutuhkan)
        Me.Controls.Add(Me.DtPermintaan)
        Me.Name = "FormPreOrderAlat"
        Me.Text = "FormPreOrderAlat"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DtPermintaan As System.Windows.Forms.DateTimePicker
    Friend WithEvents DtDibutuhkan As System.Windows.Forms.DateTimePicker
    Friend WithEvents ComboBoxJenisPermintaan As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxJenisPembayaran As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonGetSupplier As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBoxNamaSupp As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxAlamatSupp As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxPay As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNoRek As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxNamaRek As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBoxBank As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTotalHarga As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxHarga As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TextBoxSatuan As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TextBoxQty As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TextBoxKodeItem As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents TextBoxId As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents ButtonCariAlat As System.Windows.Forms.Button
    Friend WithEvents TextBoxDeskripsi As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents ButtonSubmit As System.Windows.Forms.Button
End Class
