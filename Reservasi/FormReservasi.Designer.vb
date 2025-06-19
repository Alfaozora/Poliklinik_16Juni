<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormReservasi
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormReservasi))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.PanelTop = New System.Windows.Forms.Panel
        Me.btnClose = New System.Windows.Forms.Button
        Me.PanelSide = New System.Windows.Forms.Panel
        Me.btnMenu4 = New System.Windows.Forms.Button
        Me.btnMenu3 = New System.Windows.Forms.Button
        Me.btnMenu2 = New System.Windows.Forms.Button
        Me.btnMenu1 = New System.Windows.Forms.Button
        Me.ToolTipBtnMenu = New System.Windows.Forms.ToolTip(Me.components)
        Me.TabControlMenu = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.PanelLayoutMenu1TopMain = New System.Windows.Forms.Panel
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.ButtonCari = New System.Windows.Forms.Button
        Me.ButtonSimpan = New System.Windows.Forms.Button
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.PanelLayoutMenu2TopMain = New System.Windows.Forms.Panel
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.PanelLayoutMenu3TopMain = New System.Windows.Forms.Panel
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.PanelLayoutMenu4TopMain = New System.Windows.Forms.Panel
        Me.SplitContainer9 = New System.Windows.Forms.SplitContainer
        Me.PanelButton = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.ComboBoxRuangan = New System.Windows.Forms.ComboBox
        Me.dtReservasi = New System.Windows.Forms.DateTimePicker
        Me.dtSelesai = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.TextBoxNamaPasien = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TextBoxCatatan = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.TextBoxNoHP = New System.Windows.Forms.TextBox
        Me.DataGridViewReservasi = New System.Windows.Forms.DataGridView
        Me.PanelTop.SuspendLayout()
        Me.PanelSide.SuspendLayout()
        Me.TabControlMenu.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.PanelLayoutMenu1TopMain.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.SplitContainer9.Panel1.SuspendLayout()
        Me.SplitContainer9.Panel2.SuspendLayout()
        Me.SplitContainer9.SuspendLayout()
        Me.PanelButton.SuspendLayout()
        CType(Me.DataGridViewReservasi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelTop
        '
        Me.PanelTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.PanelTop.Controls.Add(Me.btnClose)
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(1100, 40)
        Me.PanelTop.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(1060, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(40, 40)
        Me.btnClose.TabIndex = 281
        Me.ToolTipBtnMenu.SetToolTip(Me.btnClose, "Tutup Form")
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'PanelSide
        '
        Me.PanelSide.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.PanelSide.Controls.Add(Me.btnMenu4)
        Me.PanelSide.Controls.Add(Me.btnMenu3)
        Me.PanelSide.Controls.Add(Me.btnMenu2)
        Me.PanelSide.Controls.Add(Me.btnMenu1)
        Me.PanelSide.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelSide.Location = New System.Drawing.Point(0, 40)
        Me.PanelSide.Name = "PanelSide"
        Me.PanelSide.Size = New System.Drawing.Size(150, 644)
        Me.PanelSide.TabIndex = 1
        '
        'btnMenu4
        '
        Me.btnMenu4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnMenu4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMenu4.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnMenu4.FlatAppearance.BorderSize = 0
        Me.btnMenu4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnMenu4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnMenu4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMenu4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnMenu4.ForeColor = System.Drawing.Color.White
        Me.btnMenu4.Location = New System.Drawing.Point(0, 135)
        Me.btnMenu4.Name = "btnMenu4"
        Me.btnMenu4.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.btnMenu4.Size = New System.Drawing.Size(150, 45)
        Me.btnMenu4.TabIndex = 5
        Me.btnMenu4.Text = "Asuransi"
        Me.ToolTipBtnMenu.SetToolTip(Me.btnMenu4, "Asuransi")
        Me.btnMenu4.UseVisualStyleBackColor = True
        Me.btnMenu4.Visible = False
        '
        'btnMenu3
        '
        Me.btnMenu3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnMenu3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMenu3.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnMenu3.FlatAppearance.BorderSize = 0
        Me.btnMenu3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnMenu3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnMenu3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMenu3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnMenu3.ForeColor = System.Drawing.Color.White
        Me.btnMenu3.Location = New System.Drawing.Point(0, 90)
        Me.btnMenu3.Name = "btnMenu3"
        Me.btnMenu3.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.btnMenu3.Size = New System.Drawing.Size(150, 45)
        Me.btnMenu3.TabIndex = 4
        Me.btnMenu3.Text = "KECANTIKAN"
        Me.ToolTipBtnMenu.SetToolTip(Me.btnMenu3, "Kontak Pekerjaan")
        Me.btnMenu3.UseVisualStyleBackColor = True
        Me.btnMenu3.Visible = False
        '
        'btnMenu2
        '
        Me.btnMenu2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnMenu2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMenu2.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnMenu2.FlatAppearance.BorderSize = 0
        Me.btnMenu2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnMenu2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnMenu2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMenu2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnMenu2.ForeColor = System.Drawing.Color.White
        Me.btnMenu2.Location = New System.Drawing.Point(0, 45)
        Me.btnMenu2.Name = "btnMenu2"
        Me.btnMenu2.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.btnMenu2.Size = New System.Drawing.Size(150, 45)
        Me.btnMenu2.TabIndex = 3
        Me.btnMenu2.Text = "FISIOTERAPI"
        Me.ToolTipBtnMenu.SetToolTip(Me.btnMenu2, "Kontak Darurat")
        Me.btnMenu2.UseVisualStyleBackColor = True
        Me.btnMenu2.Visible = False
        '
        'btnMenu1
        '
        Me.btnMenu1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnMenu1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMenu1.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnMenu1.FlatAppearance.BorderSize = 0
        Me.btnMenu1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnMenu1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnMenu1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMenu1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnMenu1.ForeColor = System.Drawing.Color.White
        Me.btnMenu1.Location = New System.Drawing.Point(0, 0)
        Me.btnMenu1.Name = "btnMenu1"
        Me.btnMenu1.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.btnMenu1.Size = New System.Drawing.Size(150, 45)
        Me.btnMenu1.TabIndex = 0
        Me.btnMenu1.Text = "Menu Booking"
        Me.btnMenu1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTipBtnMenu.SetToolTip(Me.btnMenu1, "Identitas Pasien")
        Me.btnMenu1.UseVisualStyleBackColor = False
        '
        'TabControlMenu
        '
        Me.TabControlMenu.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TabControlMenu.Controls.Add(Me.TabPage1)
        Me.TabControlMenu.Controls.Add(Me.TabPage2)
        Me.TabControlMenu.Controls.Add(Me.TabPage3)
        Me.TabControlMenu.Controls.Add(Me.TabPage4)
        Me.TabControlMenu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlMenu.Location = New System.Drawing.Point(0, 0)
        Me.TabControlMenu.Name = "TabControlMenu"
        Me.TabControlMenu.SelectedIndex = 0
        Me.TabControlMenu.Size = New System.Drawing.Size(950, 594)
        Me.TabControlMenu.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TabControlMenu.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.White
        Me.TabPage1.Controls.Add(Me.PanelLayoutMenu1TopMain)
        Me.TabPage1.Location = New System.Drawing.Point(4, 27)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(942, 563)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Tab Menu 1"
        '
        'PanelLayoutMenu1TopMain
        '
        Me.PanelLayoutMenu1TopMain.AutoScroll = True
        Me.PanelLayoutMenu1TopMain.BackColor = System.Drawing.Color.White
        Me.PanelLayoutMenu1TopMain.Controls.Add(Me.SplitContainer1)
        Me.PanelLayoutMenu1TopMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelLayoutMenu1TopMain.Location = New System.Drawing.Point(3, 3)
        Me.PanelLayoutMenu1TopMain.Name = "PanelLayoutMenu1TopMain"
        Me.PanelLayoutMenu1TopMain.Size = New System.Drawing.Size(936, 557)
        Me.PanelLayoutMenu1TopMain.TabIndex = 6
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxNoHP)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxCatatan)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxNamaPasien)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtSelesai)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtReservasi)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ComboBoxRuangan)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridViewReservasi)
        Me.SplitContainer1.Size = New System.Drawing.Size(936, 557)
        Me.SplitContainer1.SplitterDistance = 200
        Me.SplitContainer1.TabIndex = 0
        '
        'ButtonCari
        '
        Me.ButtonCari.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonCari.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonCari.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonCari.FlatAppearance.BorderSize = 0
        Me.ButtonCari.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonCari.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonCari.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonCari.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCari.ForeColor = System.Drawing.Color.White
        Me.ButtonCari.Image = Global.PoliKlinik_Net2.My.Resources.Resources.icons8_search_24
        Me.ButtonCari.Location = New System.Drawing.Point(133, 3)
        Me.ButtonCari.Name = "ButtonCari"
        Me.ButtonCari.Size = New System.Drawing.Size(147, 35)
        Me.ButtonCari.TabIndex = 502
        Me.ButtonCari.Text = "Cek Ketersediaan"
        Me.ButtonCari.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonCari.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonCari.UseVisualStyleBackColor = False
        '
        'ButtonSimpan
        '
        Me.ButtonSimpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonSimpan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonSimpan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonSimpan.FlatAppearance.BorderSize = 0
        Me.ButtonSimpan.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonSimpan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonSimpan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonSimpan.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSimpan.ForeColor = System.Drawing.Color.White
        Me.ButtonSimpan.Image = Global.PoliKlinik_Net2.My.Resources.Resources.icons8_save_24
        Me.ButtonSimpan.Location = New System.Drawing.Point(7, 3)
        Me.ButtonSimpan.Name = "ButtonSimpan"
        Me.ButtonSimpan.Size = New System.Drawing.Size(120, 35)
        Me.ButtonSimpan.TabIndex = 499
        Me.ButtonSimpan.Text = " Simpan Data"
        Me.ButtonSimpan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonSimpan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonSimpan.UseVisualStyleBackColor = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.PanelLayoutMenu2TopMain)
        Me.TabPage2.Location = New System.Drawing.Point(4, 27)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(942, 563)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Tab Menu 2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'PanelLayoutMenu2TopMain
        '
        Me.PanelLayoutMenu2TopMain.AutoScroll = True
        Me.PanelLayoutMenu2TopMain.BackColor = System.Drawing.Color.White
        Me.PanelLayoutMenu2TopMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelLayoutMenu2TopMain.Location = New System.Drawing.Point(3, 3)
        Me.PanelLayoutMenu2TopMain.Name = "PanelLayoutMenu2TopMain"
        Me.PanelLayoutMenu2TopMain.Size = New System.Drawing.Size(936, 557)
        Me.PanelLayoutMenu2TopMain.TabIndex = 3
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.PanelLayoutMenu3TopMain)
        Me.TabPage3.Location = New System.Drawing.Point(4, 25)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(942, 565)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Tab Menu 3"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'PanelLayoutMenu3TopMain
        '
        Me.PanelLayoutMenu3TopMain.AutoScroll = True
        Me.PanelLayoutMenu3TopMain.BackColor = System.Drawing.Color.White
        Me.PanelLayoutMenu3TopMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelLayoutMenu3TopMain.Location = New System.Drawing.Point(3, 3)
        Me.PanelLayoutMenu3TopMain.Name = "PanelLayoutMenu3TopMain"
        Me.PanelLayoutMenu3TopMain.Size = New System.Drawing.Size(936, 559)
        Me.PanelLayoutMenu3TopMain.TabIndex = 5
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.PanelLayoutMenu4TopMain)
        Me.TabPage4.Location = New System.Drawing.Point(4, 25)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(942, 565)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Tab Menu 4"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'PanelLayoutMenu4TopMain
        '
        Me.PanelLayoutMenu4TopMain.AutoScroll = True
        Me.PanelLayoutMenu4TopMain.BackColor = System.Drawing.Color.White
        Me.PanelLayoutMenu4TopMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelLayoutMenu4TopMain.Location = New System.Drawing.Point(3, 3)
        Me.PanelLayoutMenu4TopMain.Name = "PanelLayoutMenu4TopMain"
        Me.PanelLayoutMenu4TopMain.Size = New System.Drawing.Size(936, 559)
        Me.PanelLayoutMenu4TopMain.TabIndex = 7
        '
        'SplitContainer9
        '
        Me.SplitContainer9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer9.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer9.IsSplitterFixed = True
        Me.SplitContainer9.Location = New System.Drawing.Point(150, 40)
        Me.SplitContainer9.Name = "SplitContainer9"
        Me.SplitContainer9.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer9.Panel1
        '
        Me.SplitContainer9.Panel1.AutoScroll = True
        Me.SplitContainer9.Panel1.Controls.Add(Me.PanelButton)
        '
        'SplitContainer9.Panel2
        '
        Me.SplitContainer9.Panel2.AutoScroll = True
        Me.SplitContainer9.Panel2.Controls.Add(Me.TabControlMenu)
        Me.SplitContainer9.Size = New System.Drawing.Size(950, 644)
        Me.SplitContainer9.SplitterDistance = 45
        Me.SplitContainer9.SplitterWidth = 5
        Me.SplitContainer9.TabIndex = 461
        '
        'PanelButton
        '
        Me.PanelButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.PanelButton.Controls.Add(Me.ButtonCari)
        Me.PanelButton.Controls.Add(Me.ButtonSimpan)
        Me.PanelButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelButton.Location = New System.Drawing.Point(0, 0)
        Me.PanelButton.Name = "PanelButton"
        Me.PanelButton.Size = New System.Drawing.Size(950, 45)
        Me.PanelButton.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 17)
        Me.Label1.TabIndex = 509
        Me.Label1.Text = "Poli"
        '
        'ComboBoxRuangan
        '
        Me.ComboBoxRuangan.AutoCompleteCustomSource.AddRange(New String() {"SURAT KETERANGAN DOKTER", "SURAT KETERANGAN ISTIRAHAT", "SURAT KETERANGAN KEMATIAN", "SURAT KETERANGAN SUNAT", "SURAT KETERANGAN LAYAK TERBANG", "SURAT KETERANGAN KONTROL"})
        Me.ComboBoxRuangan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.ComboBoxRuangan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxRuangan.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxRuangan.FormattingEnabled = True
        Me.ComboBoxRuangan.Items.AddRange(New Object() {"SURAT KETERANGAN DOKTER", "SURAT KETERANGAN ISTIRAHAT", "SURAT KETERANGAN BEROBAT", "SURAT KETERANGAN CUTI HAMIL", "SURAT KETERANGAN KEMATIAN", "SURAT KETERANGAN KHITAN", "SURAT KETERANGAN LAYAK TERBANG", "SURAT KETERANGAN KONTROL", "SURAT KETERANGAN PERSETUJUAN RUJUKAN"})
        Me.ComboBoxRuangan.Location = New System.Drawing.Point(111, 15)
        Me.ComboBoxRuangan.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ComboBoxRuangan.Name = "ComboBoxRuangan"
        Me.ComboBoxRuangan.Size = New System.Drawing.Size(277, 25)
        Me.ComboBoxRuangan.TabIndex = 510
        '
        'dtReservasi
        '
        Me.dtReservasi.CalendarFont = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtReservasi.CustomFormat = "dd-MMM-yyyy HH:mm"
        Me.dtReservasi.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtReservasi.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtReservasi.Location = New System.Drawing.Point(111, 55)
        Me.dtReservasi.Name = "dtReservasi"
        Me.dtReservasi.Size = New System.Drawing.Size(151, 25)
        Me.dtReservasi.TabIndex = 519
        '
        'dtSelesai
        '
        Me.dtSelesai.CalendarFont = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtSelesai.CustomFormat = "dd-MMM-yyyy HH:mm"
        Me.dtSelesai.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtSelesai.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtSelesai.Location = New System.Drawing.Point(111, 95)
        Me.dtSelesai.Name = "dtSelesai"
        Me.dtSelesai.Size = New System.Drawing.Size(151, 25)
        Me.dtSelesai.TabIndex = 520
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 17)
        Me.Label2.TabIndex = 521
        Me.Label2.Text = "Tgl Reservasi"
        '
        'TextBoxNamaPasien
        '
        Me.TextBoxNamaPasien.Location = New System.Drawing.Point(111, 139)
        Me.TextBoxNamaPasien.Name = "TextBoxNamaPasien"
        Me.TextBoxNamaPasien.Size = New System.Drawing.Size(277, 23)
        Me.TextBoxNamaPasien.TabIndex = 522
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(18, 145)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 17)
        Me.Label3.TabIndex = 523
        Me.Label3.Text = "Nama Pasien"
        '
        'TextBoxCatatan
        '
        Me.TextBoxCatatan.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.TextBoxCatatan.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxCatatan.Location = New System.Drawing.Point(494, 58)
        Me.TextBoxCatatan.Multiline = True
        Me.TextBoxCatatan.Name = "TextBoxCatatan"
        Me.TextBoxCatatan.Size = New System.Drawing.Size(194, 70)
        Me.TextBoxCatatan.TabIndex = 524
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(421, 66)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 17)
        Me.Label4.TabIndex = 525
        Me.Label4.Text = "Catatan"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(18, 101)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 17)
        Me.Label5.TabIndex = 526
        Me.Label5.Text = "Sampai"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(421, 18)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 17)
        Me.Label6.TabIndex = 527
        Me.Label6.Text = "No HP"
        '
        'TextBoxNoHP
        '
        Me.TextBoxNoHP.Location = New System.Drawing.Point(494, 15)
        Me.TextBoxNoHP.Name = "TextBoxNoHP"
        Me.TextBoxNoHP.Size = New System.Drawing.Size(194, 23)
        Me.TextBoxNoHP.TabIndex = 528
        '
        'DataGridViewReservasi
        '
        Me.DataGridViewReservasi.AllowUserToAddRows = False
        Me.DataGridViewReservasi.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(231, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.Padding = New System.Windows.Forms.Padding(3)
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewReservasi.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewReservasi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridViewReservasi.BackgroundColor = System.Drawing.Color.White
        Me.DataGridViewReservasi.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridViewReservasi.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.DataGridViewReservasi.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(218, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Padding = New System.Windows.Forms.Padding(3)
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewReservasi.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewReservasi.ColumnHeadersHeight = 30
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.Padding = New System.Windows.Forms.Padding(3)
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(186, Byte), Integer), CType(CType(191, Byte), Integer), CType(CType(149, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewReservasi.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewReservasi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewReservasi.EnableHeadersVisualStyles = False
        Me.DataGridViewReservasi.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewReservasi.Name = "DataGridViewReservasi"
        Me.DataGridViewReservasi.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle4.Padding = New System.Windows.Forms.Padding(3)
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewReservasi.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewReservasi.RowHeadersVisible = False
        Me.DataGridViewReservasi.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.DataGridViewReservasi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridViewReservasi.Size = New System.Drawing.Size(936, 353)
        Me.DataGridViewReservasi.StandardTab = True
        Me.DataGridViewReservasi.TabIndex = 520
        '
        'FormReservasi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1100, 684)
        Me.ControlBox = False
        Me.Controls.Add(Me.SplitContainer9)
        Me.Controls.Add(Me.PanelSide)
        Me.Controls.Add(Me.PanelTop)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormReservasi"
        Me.PanelTop.ResumeLayout(False)
        Me.PanelSide.ResumeLayout(False)
        Me.TabControlMenu.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.PanelLayoutMenu1TopMain.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.SplitContainer9.Panel1.ResumeLayout(False)
        Me.SplitContainer9.Panel2.ResumeLayout(False)
        Me.SplitContainer9.ResumeLayout(False)
        Me.PanelButton.ResumeLayout(False)
        CType(Me.DataGridViewReservasi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelTop As System.Windows.Forms.Panel
    Friend WithEvents PanelSide As System.Windows.Forms.Panel
    Friend WithEvents ToolTipBtnMenu As System.Windows.Forms.ToolTip
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btnMenu3 As System.Windows.Forms.Button
    Friend WithEvents btnMenu2 As System.Windows.Forms.Button
    Friend WithEvents btnMenu1 As System.Windows.Forms.Button
    Friend WithEvents btnMenu4 As System.Windows.Forms.Button
    Friend WithEvents TabControlMenu As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents PanelLayoutMenu1TopMain As System.Windows.Forms.Panel
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents PanelLayoutMenu2TopMain As System.Windows.Forms.Panel
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents PanelLayoutMenu3TopMain As System.Windows.Forms.Panel
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents PanelLayoutMenu4TopMain As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer9 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents PanelButton As System.Windows.Forms.Panel
    Friend WithEvents ButtonCari As System.Windows.Forms.Button
    Friend WithEvents ButtonSimpan As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxRuangan As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtSelesai As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtReservasi As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBoxNamaPasien As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCatatan As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNoHP As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DataGridViewReservasi As System.Windows.Forms.DataGridView
End Class
