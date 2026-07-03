<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MasterTindakanKlinik
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MasterTindakanKlinik))
        Me.PanelTop = New System.Windows.Forms.Panel
        Me.ButtonClose = New System.Windows.Forms.Button
        Me.PanelMasterTindakanKlinik = New System.Windows.Forms.Panel
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.NavBarControl1 = New DevExpress.XtraNavBar.NavBarControl
        Me.NavBarGroup1 = New DevExpress.XtraNavBar.NavBarGroup
        Me.NavBarItemTindakanPoliUmum = New DevExpress.XtraNavBar.NavBarItem
        Me.NavBarItemMasterPaketTindakan = New DevExpress.XtraNavBar.NavBarItem
        Me.NavBarItemMasterAlat = New DevExpress.XtraNavBar.NavBarItem
        Me.NavBarItem2 = New DevExpress.XtraNavBar.NavBarItem
        Me.TabControlMenu = New System.Windows.Forms.TabControl
        Me.TabPageMasterTindakan = New System.Windows.Forms.TabPage
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel
        Me.ButtonSimpanMasterTindakan = New System.Windows.Forms.Button
        Me.ButtonClearMasterTindakan = New System.Windows.Forms.Button
        Me.ButtonExportMasterTindakan = New System.Windows.Forms.Button
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
        Me.PanelMasterTindakan = New System.Windows.Forms.Panel
        Me.DataGridViewAlatTindakan = New System.Windows.Forms.DataGridView
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.ButtonTambahAlat = New System.Windows.Forms.Button
        Me.ComboBoxAlatTindakan = New System.Windows.Forms.ComboBox
        Me.Label37 = New System.Windows.Forms.Label
        Me.ComboBoxCartridgeTindakan = New System.Windows.Forms.ComboBox
        Me.Label36 = New System.Windows.Forms.Label
        Me.Label41 = New System.Windows.Forms.Label
        Me.NumericShootPerTindakan = New System.Windows.Forms.NumericUpDown
        Me.CheckBoxPakaiAlat = New System.Windows.Forms.CheckBox
        Me.ComboBoxKategori = New System.Windows.Forms.ComboBox
        Me.Label34 = New System.Windows.Forms.Label
        Me.CheckedListBoxPoli = New System.Windows.Forms.CheckedListBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.TextBoxHariReminder = New System.Windows.Forms.TextBox
        Me.CheckBoxReminder = New System.Windows.Forms.CheckBox
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.RadioButtonPelaksanaDokter = New System.Windows.Forms.RadioButton
        Me.RadioButtonPelaksanaPerawat = New System.Windows.Forms.RadioButton
        Me.CheckBoxStatusAktifTindakan = New System.Windows.Forms.CheckBox
        Me.SplitContainerFeeTindakan = New System.Windows.Forms.SplitContainer
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.RadioButtonAktifFee = New System.Windows.Forms.RadioButton
        Me.RadioButtonNonAktifFee = New System.Windows.Forms.RadioButton
        Me.Label12 = New System.Windows.Forms.Label
        Me.TextBoxPersentaseFeePerawat = New System.Windows.Forms.TextBox
        Me.Label42 = New System.Windows.Forms.Label
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.TextBox10 = New System.Windows.Forms.TextBox
        Me.TextBox11 = New System.Windows.Forms.TextBox
        Me.TextBox13 = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.RadioButton3 = New System.Windows.Forms.RadioButton
        Me.RadioButton4 = New System.Windows.Forms.RadioButton
        Me.Label31 = New System.Windows.Forms.Label
        Me.Label47 = New System.Windows.Forms.Label
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.TextBox4 = New System.Windows.Forms.TextBox
        Me.TextBox6 = New System.Windows.Forms.TextBox
        Me.TextBox8 = New System.Windows.Forms.TextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.RadioButton2 = New System.Windows.Forms.RadioButton
        Me.Label28 = New System.Windows.Forms.Label
        Me.TextBoxPersentaseFeeKaryawan = New System.Windows.Forms.TextBox
        Me.TextBoxPersentaseFeePerusahaan = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.TextBox14 = New System.Windows.Forms.TextBox
        Me.TextBoxFeePerawatTambahan = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.TextBox9 = New System.Windows.Forms.TextBox
        Me.TextBoxFeeDokterTambahan = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.TextBoxNominalFee = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.RadioButtonOpsiPersentase = New System.Windows.Forms.RadioButton
        Me.RadioButtonOpsiNominal = New System.Windows.Forms.RadioButton
        Me.Label13 = New System.Windows.Forms.Label
        Me.ComboBoxInformedConsent = New System.Windows.Forms.ComboBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.CheckBoxInformedConsent = New System.Windows.Forms.CheckBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.RadioButtonBHP = New System.Windows.Forms.RadioButton
        Me.RadioButtonNonBHP = New System.Windows.Forms.RadioButton
        Me.TextBoxTindakanID = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.ButtonFindTindakan = New System.Windows.Forms.Button
        Me.TextBoxNamaTindakan = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.TextBoxBiayaTindakan = New System.Windows.Forms.TextBox
        Me.ComboBoxGroupJenisTindakan = New System.Windows.Forms.ComboBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.RadioButtonPoliUmum = New System.Windows.Forms.RadioButton
        Me.RadioButtonPoliGigi = New System.Windows.Forms.RadioButton
        Me.TextBox7 = New System.Windows.Forms.TextBox
        Me.TextBox5 = New System.Windows.Forms.TextBox
        Me.TextBoxMaksimalBHP_Tindakan = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.TabPageMasterPaket = New System.Windows.Forms.TabPage
        Me.SplitContainerPendaftaran = New System.Windows.Forms.SplitContainer
        Me.FlowLayoutPanel4 = New System.Windows.Forms.FlowLayoutPanel
        Me.ButtonSimpanMasterPaket = New System.Windows.Forms.Button
        Me.ButtonClearMasterPaket = New System.Windows.Forms.Button
        Me.ButtonExportMasterPaket = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.SplitContainerMasterPaket = New System.Windows.Forms.SplitContainer
        Me.ComboBoxICPaket = New System.Windows.Forms.ComboBox
        Me.CheckBoxICPaket = New System.Windows.Forms.CheckBox
        Me.Label35 = New System.Windows.Forms.Label
        Me.CheckedListBoxPoli2 = New System.Windows.Forms.CheckedListBox
        Me.TextBox15 = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TextBoxMaksimalBHP_Paket = New System.Windows.Forms.TextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.RadioButtonPaketReguler = New System.Windows.Forms.RadioButton
        Me.RadioButtonPaketPerTindakan = New System.Windows.Forms.RadioButton
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.TextBoxHargaNett = New System.Windows.Forms.TextBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.TextBoxTotalDiskon = New System.Windows.Forms.TextBox
        Me.ButtonHapusDetailTindakan = New System.Windows.Forms.Button
        Me.TextBox12 = New System.Windows.Forms.TextBox
        Me.TextBoxDurasiAktifPaket = New System.Windows.Forms.NumericUpDown
        Me.TextBoxKuotaPaket = New System.Windows.Forms.NumericUpDown
        Me.ButtonDetailTindakanPaket = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label54 = New System.Windows.Forms.Label
        Me.ButtonKalkulasiPaket = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.TglAkhirPaket = New System.Windows.Forms.DateTimePicker
        Me.TglAwalPaket = New System.Windows.Forms.DateTimePicker
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.LabelKuotaPaket = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBoxHargaAwal = New System.Windows.Forms.TextBox
        Me.TextBoxKeteranganPaket = New System.Windows.Forms.TextBox
        Me.TextBoxNamaPaket = New System.Windows.Forms.TextBox
        Me.Label157 = New System.Windows.Forms.Label
        Me.ButtonFindPaket = New System.Windows.Forms.Button
        Me.Label203 = New System.Windows.Forms.Label
        Me.TextBoxReffID = New System.Windows.Forms.TextBox
        Me.GridControlTindakan = New DevExpress.XtraGrid.GridControl
        Me.GridViewTindakan = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.TabPageMasterAlat = New System.Windows.Forms.TabPage
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.TextBoxIdRuangan = New System.Windows.Forms.TextBox
        Me.Label59 = New System.Windows.Forms.Label
        Me.TextBoxNamaRuangan = New System.Windows.Forms.TextBox
        Me.ButtonGetAlatSarpras = New System.Windows.Forms.Button
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
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.ButtonGetAlatCartridge = New System.Windows.Forms.Button
        Me.Label49 = New System.Windows.Forms.Label
        Me.TextBox17 = New System.Windows.Forms.TextBox
        Me.Label48 = New System.Windows.Forms.Label
        Me.TextBoxHargaBeli = New System.Windows.Forms.TextBox
        Me.NumericKuotaMinimumCartridge = New System.Windows.Forms.TextBox
        Me.NumericKuotaAwalCartridge = New System.Windows.Forms.TextBox
        Me.ButtonPreOrder = New System.Windows.Forms.Button
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label58 = New System.Windows.Forms.Label
        Me.ComboBoxNamaAlat = New System.Windows.Forms.ComboBox
        Me.Label50 = New System.Windows.Forms.Label
        Me.TextBoxKuotaTotalCartridge = New System.Windows.Forms.TextBox
        Me.Label51 = New System.Windows.Forms.Label
        Me.TextBoxKodeCartridge = New System.Windows.Forms.TextBox
        Me.Label52 = New System.Windows.Forms.Label
        Me.TextBoxKuotaSisaCartridge = New System.Windows.Forms.TextBox
        Me.Label53 = New System.Windows.Forms.Label
        Me.TextBoxKuotaTerpakaiCartridge = New System.Windows.Forms.TextBox
        Me.Label55 = New System.Windows.Forms.Label
        Me.CheckBoxCartridge = New System.Windows.Forms.CheckBox
        Me.TextBoxIdCartridge = New System.Windows.Forms.TextBox
        Me.Label56 = New System.Windows.Forms.Label
        Me.ButtonCariCartridge = New System.Windows.Forms.Button
        Me.TextBoxNamaCartridge = New System.Windows.Forms.TextBox
        Me.Label57 = New System.Windows.Forms.Label
        Me.DataGridViewAlat = New System.Windows.Forms.DataGridView
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.ButtonSimpanAlat = New System.Windows.Forms.Button
        Me.ButtonClearAlat = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.NavBarItem3 = New DevExpress.XtraNavBar.NavBarItem
        Me.NavBarItem4 = New DevExpress.XtraNavBar.NavBarItem
        Me.PanelTop.SuspendLayout()
        Me.PanelMasterTindakanKlinik.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.NavBarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControlMenu.SuspendLayout()
        Me.TabPageMasterTindakan.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.PanelMasterTindakan.SuspendLayout()
        CType(Me.DataGridViewAlatTindakan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.NumericShootPerTindakan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SplitContainerFeeTindakan.Panel1.SuspendLayout()
        Me.SplitContainerFeeTindakan.Panel2.SuspendLayout()
        Me.SplitContainerFeeTindakan.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TabPageMasterPaket.SuspendLayout()
        Me.SplitContainerPendaftaran.Panel1.SuspendLayout()
        Me.SplitContainerPendaftaran.Panel2.SuspendLayout()
        Me.SplitContainerPendaftaran.SuspendLayout()
        Me.FlowLayoutPanel4.SuspendLayout()
        Me.SplitContainerMasterPaket.Panel1.SuspendLayout()
        Me.SplitContainerMasterPaket.Panel2.SuspendLayout()
        Me.SplitContainerMasterPaket.SuspendLayout()
        Me.Panel8.SuspendLayout()
        CType(Me.TextBoxDurasiAktifPaket, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBoxKuotaPaket, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControlTindakan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewTindakan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageMasterAlat.SuspendLayout()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.DataGridViewAlat, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel9.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelTop
        '
        Me.PanelTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.PanelTop.Controls.Add(Me.ButtonClose)
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(1000, 35)
        Me.PanelTop.TabIndex = 464
        '
        'ButtonClose
        '
        Me.ButtonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ButtonClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.ButtonClose.FlatAppearance.BorderSize = 0
        Me.ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonClose.Image = CType(resources.GetObject("ButtonClose.Image"), System.Drawing.Image)
        Me.ButtonClose.Location = New System.Drawing.Point(960, 0)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(40, 35)
        Me.ButtonClose.TabIndex = 281
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'PanelMasterTindakanKlinik
        '
        Me.PanelMasterTindakanKlinik.Controls.Add(Me.SplitContainer1)
        Me.PanelMasterTindakanKlinik.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelMasterTindakanKlinik.Location = New System.Drawing.Point(0, 35)
        Me.PanelMasterTindakanKlinik.Name = "PanelMasterTindakanKlinik"
        Me.PanelMasterTindakanKlinik.Size = New System.Drawing.Size(1000, 665)
        Me.PanelMasterTindakanKlinik.TabIndex = 465
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.NavBarControl1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TabControlMenu)
        Me.SplitContainer1.Size = New System.Drawing.Size(1000, 665)
        Me.SplitContainer1.SplitterDistance = 225
        Me.SplitContainer1.TabIndex = 0
        '
        'NavBarControl1
        '
        Me.NavBarControl1.ActiveGroup = Me.NavBarGroup1
        Me.NavBarControl1.BackColor = System.Drawing.SystemColors.Control
        Me.NavBarControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NavBarControl1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NavBarControl1.Groups.AddRange(New DevExpress.XtraNavBar.NavBarGroup() {Me.NavBarGroup1})
        Me.NavBarControl1.Items.AddRange(New DevExpress.XtraNavBar.NavBarItem() {Me.NavBarItemTindakanPoliUmum, Me.NavBarItem2, Me.NavBarItemMasterPaketTindakan, Me.NavBarItemMasterAlat})
        Me.NavBarControl1.Location = New System.Drawing.Point(0, 0)
        Me.NavBarControl1.Name = "NavBarControl1"
        Me.NavBarControl1.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.ExplorerBar
        Me.NavBarControl1.Size = New System.Drawing.Size(225, 665)
        Me.NavBarControl1.TabIndex = 1
        Me.NavBarControl1.Text = "NavBarItemMasterAlat"
        Me.NavBarControl1.View = New DevExpress.XtraNavBar.ViewInfo.StandardSkinExplorerBarViewInfoRegistrator("The Asphalt World")
        '
        'NavBarGroup1
        '
        Me.NavBarGroup1.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.NavBarGroup1.Appearance.Options.UseFont = True
        Me.NavBarGroup1.Caption = ""
        Me.NavBarGroup1.Expanded = True
        Me.NavBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.SmallIconsList
        Me.NavBarGroup1.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.NavBarItemTindakanPoliUmum), New DevExpress.XtraNavBar.NavBarItemLink(Me.NavBarItemMasterPaketTindakan), New DevExpress.XtraNavBar.NavBarItemLink(Me.NavBarItemMasterAlat)})
        Me.NavBarGroup1.Name = "NavBarGroup1"
        '
        'NavBarItemTindakanPoliUmum
        '
        Me.NavBarItemTindakanPoliUmum.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NavBarItemTindakanPoliUmum.Appearance.Options.UseFont = True
        Me.NavBarItemTindakanPoliUmum.AppearanceHotTracked.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.NavBarItemTindakanPoliUmum.AppearanceHotTracked.Options.UseFont = True
        Me.NavBarItemTindakanPoliUmum.AppearancePressed.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.NavBarItemTindakanPoliUmum.AppearancePressed.Options.UseFont = True
        Me.NavBarItemTindakanPoliUmum.Caption = "MASTER TINDAKAN"
        Me.NavBarItemTindakanPoliUmum.LargeImage = CType(resources.GetObject("NavBarItemTindakanPoliUmum.LargeImage"), System.Drawing.Image)
        Me.NavBarItemTindakanPoliUmum.Name = "NavBarItemTindakanPoliUmum"
        Me.NavBarItemTindakanPoliUmum.SmallImage = CType(resources.GetObject("NavBarItemTindakanPoliUmum.SmallImage"), System.Drawing.Image)
        '
        'NavBarItemMasterPaketTindakan
        '
        Me.NavBarItemMasterPaketTindakan.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.NavBarItemMasterPaketTindakan.Appearance.Options.UseFont = True
        Me.NavBarItemMasterPaketTindakan.AppearanceHotTracked.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.NavBarItemMasterPaketTindakan.AppearanceHotTracked.Options.UseFont = True
        Me.NavBarItemMasterPaketTindakan.AppearancePressed.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.NavBarItemMasterPaketTindakan.AppearancePressed.Options.UseFont = True
        Me.NavBarItemMasterPaketTindakan.Caption = "MASTER PAKET TINDAKAN"
        Me.NavBarItemMasterPaketTindakan.LargeImage = CType(resources.GetObject("NavBarItemMasterPaketTindakan.LargeImage"), System.Drawing.Image)
        Me.NavBarItemMasterPaketTindakan.Name = "NavBarItemMasterPaketTindakan"
        Me.NavBarItemMasterPaketTindakan.SmallImage = CType(resources.GetObject("NavBarItemMasterPaketTindakan.SmallImage"), System.Drawing.Image)
        '
        'NavBarItemMasterAlat
        '
        Me.NavBarItemMasterAlat.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NavBarItemMasterAlat.Appearance.Options.UseFont = True
        Me.NavBarItemMasterAlat.AppearanceHotTracked.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NavBarItemMasterAlat.AppearanceHotTracked.Options.UseFont = True
        Me.NavBarItemMasterAlat.AppearancePressed.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NavBarItemMasterAlat.AppearancePressed.Options.UseFont = True
        Me.NavBarItemMasterAlat.Caption = "MASTER ALAT"
        Me.NavBarItemMasterAlat.Name = "NavBarItemMasterAlat"
        Me.NavBarItemMasterAlat.SmallImage = CType(resources.GetObject("NavBarItemMasterAlat.SmallImage"), System.Drawing.Image)
        '
        'NavBarItem2
        '
        Me.NavBarItem2.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.NavBarItem2.Appearance.Options.UseFont = True
        Me.NavBarItem2.AppearanceHotTracked.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.NavBarItem2.AppearanceHotTracked.Options.UseFont = True
        Me.NavBarItem2.AppearancePressed.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.NavBarItem2.AppearancePressed.Options.UseFont = True
        Me.NavBarItem2.Caption = "DAFTAR PAKET TINDAKAN"
        Me.NavBarItem2.Name = "NavBarItem2"
        '
        'TabControlMenu
        '
        Me.TabControlMenu.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TabControlMenu.Controls.Add(Me.TabPageMasterTindakan)
        Me.TabControlMenu.Controls.Add(Me.TabPageMasterPaket)
        Me.TabControlMenu.Controls.Add(Me.TabPageMasterAlat)
        Me.TabControlMenu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlMenu.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TabControlMenu.Location = New System.Drawing.Point(0, 0)
        Me.TabControlMenu.Name = "TabControlMenu"
        Me.TabControlMenu.SelectedIndex = 0
        Me.TabControlMenu.Size = New System.Drawing.Size(771, 665)
        Me.TabControlMenu.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TabControlMenu.TabIndex = 0
        '
        'TabPageMasterTindakan
        '
        Me.TabPageMasterTindakan.BackColor = System.Drawing.Color.White
        Me.TabPageMasterTindakan.Controls.Add(Me.SplitContainer2)
        Me.TabPageMasterTindakan.Location = New System.Drawing.Point(4, 27)
        Me.TabPageMasterTindakan.Name = "TabPageMasterTindakan"
        Me.TabPageMasterTindakan.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageMasterTindakan.Size = New System.Drawing.Size(763, 634)
        Me.TabPageMasterTindakan.TabIndex = 1
        Me.TabPageMasterTindakan.Text = "Master Tindakan"
        Me.TabPageMasterTindakan.UseVisualStyleBackColor = True
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.FlowLayoutPanel1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.AutoScroll = True
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Size = New System.Drawing.Size(757, 628)
        Me.SplitContainer2.SplitterDistance = 46
        Me.SplitContainer2.SplitterWidth = 1
        Me.SplitContainer2.TabIndex = 462
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.FlowLayoutPanel1.Controls.Add(Me.ButtonSimpanMasterTindakan)
        Me.FlowLayoutPanel1.Controls.Add(Me.ButtonClearMasterTindakan)
        Me.FlowLayoutPanel1.Controls.Add(Me.ButtonExportMasterTindakan)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(757, 46)
        Me.FlowLayoutPanel1.TabIndex = 538
        '
        'ButtonSimpanMasterTindakan
        '
        Me.ButtonSimpanMasterTindakan.BackColor = System.Drawing.Color.White
        Me.ButtonSimpanMasterTindakan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonSimpanMasterTindakan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonSimpanMasterTindakan.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonSimpanMasterTindakan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonSimpanMasterTindakan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonSimpanMasterTindakan.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSimpanMasterTindakan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonSimpanMasterTindakan.Image = CType(resources.GetObject("ButtonSimpanMasterTindakan.Image"), System.Drawing.Image)
        Me.ButtonSimpanMasterTindakan.Location = New System.Drawing.Point(3, 3)
        Me.ButtonSimpanMasterTindakan.Name = "ButtonSimpanMasterTindakan"
        Me.ButtonSimpanMasterTindakan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonSimpanMasterTindakan.Size = New System.Drawing.Size(175, 40)
        Me.ButtonSimpanMasterTindakan.TabIndex = 582
        Me.ButtonSimpanMasterTindakan.Text = " Simpan Tindakan"
        Me.ButtonSimpanMasterTindakan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonSimpanMasterTindakan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonSimpanMasterTindakan.UseVisualStyleBackColor = False
        '
        'ButtonClearMasterTindakan
        '
        Me.ButtonClearMasterTindakan.BackColor = System.Drawing.Color.White
        Me.ButtonClearMasterTindakan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonClearMasterTindakan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonClearMasterTindakan.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonClearMasterTindakan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonClearMasterTindakan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonClearMasterTindakan.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonClearMasterTindakan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonClearMasterTindakan.Image = CType(resources.GetObject("ButtonClearMasterTindakan.Image"), System.Drawing.Image)
        Me.ButtonClearMasterTindakan.Location = New System.Drawing.Point(184, 3)
        Me.ButtonClearMasterTindakan.Name = "ButtonClearMasterTindakan"
        Me.ButtonClearMasterTindakan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonClearMasterTindakan.Size = New System.Drawing.Size(150, 40)
        Me.ButtonClearMasterTindakan.TabIndex = 583
        Me.ButtonClearMasterTindakan.Text = " Clear Data"
        Me.ButtonClearMasterTindakan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonClearMasterTindakan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonClearMasterTindakan.UseVisualStyleBackColor = False
        '
        'ButtonExportMasterTindakan
        '
        Me.ButtonExportMasterTindakan.BackColor = System.Drawing.Color.White
        Me.ButtonExportMasterTindakan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonExportMasterTindakan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonExportMasterTindakan.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonExportMasterTindakan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonExportMasterTindakan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonExportMasterTindakan.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonExportMasterTindakan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonExportMasterTindakan.Image = CType(resources.GetObject("ButtonExportMasterTindakan.Image"), System.Drawing.Image)
        Me.ButtonExportMasterTindakan.Location = New System.Drawing.Point(340, 3)
        Me.ButtonExportMasterTindakan.Name = "ButtonExportMasterTindakan"
        Me.ButtonExportMasterTindakan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonExportMasterTindakan.Size = New System.Drawing.Size(150, 40)
        Me.ButtonExportMasterTindakan.TabIndex = 585
        Me.ButtonExportMasterTindakan.Text = " Export To CSV"
        Me.ButtonExportMasterTindakan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonExportMasterTindakan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonExportMasterTindakan.UseVisualStyleBackColor = False
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.AutoScroll = True
        Me.SplitContainer3.Panel1.BackColor = System.Drawing.Color.White
        Me.SplitContainer3.Panel1.Controls.Add(Me.PanelMasterTindakan)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.AutoScroll = True
        Me.SplitContainer3.Size = New System.Drawing.Size(757, 581)
        Me.SplitContainer3.SplitterDistance = 552
        Me.SplitContainer3.SplitterWidth = 1
        Me.SplitContainer3.TabIndex = 462
        '
        'PanelMasterTindakan
        '
        Me.PanelMasterTindakan.AutoScroll = True
        Me.PanelMasterTindakan.Controls.Add(Me.DataGridViewAlatTindakan)
        Me.PanelMasterTindakan.Controls.Add(Me.GroupBox2)
        Me.PanelMasterTindakan.Controls.Add(Me.ComboBoxKategori)
        Me.PanelMasterTindakan.Controls.Add(Me.Label34)
        Me.PanelMasterTindakan.Controls.Add(Me.CheckedListBoxPoli)
        Me.PanelMasterTindakan.Controls.Add(Me.GroupBox1)
        Me.PanelMasterTindakan.Controls.Add(Me.Panel5)
        Me.PanelMasterTindakan.Controls.Add(Me.CheckBoxStatusAktifTindakan)
        Me.PanelMasterTindakan.Controls.Add(Me.SplitContainerFeeTindakan)
        Me.PanelMasterTindakan.Controls.Add(Me.ComboBoxInformedConsent)
        Me.PanelMasterTindakan.Controls.Add(Me.Label11)
        Me.PanelMasterTindakan.Controls.Add(Me.CheckBoxInformedConsent)
        Me.PanelMasterTindakan.Controls.Add(Me.Label18)
        Me.PanelMasterTindakan.Controls.Add(Me.Panel2)
        Me.PanelMasterTindakan.Controls.Add(Me.TextBoxTindakanID)
        Me.PanelMasterTindakan.Controls.Add(Me.Label14)
        Me.PanelMasterTindakan.Controls.Add(Me.Label19)
        Me.PanelMasterTindakan.Controls.Add(Me.Label15)
        Me.PanelMasterTindakan.Controls.Add(Me.ButtonFindTindakan)
        Me.PanelMasterTindakan.Controls.Add(Me.TextBoxNamaTindakan)
        Me.PanelMasterTindakan.Controls.Add(Me.Label20)
        Me.PanelMasterTindakan.Controls.Add(Me.TextBoxBiayaTindakan)
        Me.PanelMasterTindakan.Controls.Add(Me.ComboBoxGroupJenisTindakan)
        Me.PanelMasterTindakan.Controls.Add(Me.Label17)
        Me.PanelMasterTindakan.Controls.Add(Me.Label9)
        Me.PanelMasterTindakan.Controls.Add(Me.Panel1)
        Me.PanelMasterTindakan.Controls.Add(Me.TextBox7)
        Me.PanelMasterTindakan.Controls.Add(Me.TextBox5)
        Me.PanelMasterTindakan.Controls.Add(Me.TextBoxMaksimalBHP_Tindakan)
        Me.PanelMasterTindakan.Controls.Add(Me.Label10)
        Me.PanelMasterTindakan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelMasterTindakan.Location = New System.Drawing.Point(0, 0)
        Me.PanelMasterTindakan.Name = "PanelMasterTindakan"
        Me.PanelMasterTindakan.Size = New System.Drawing.Size(757, 552)
        Me.PanelMasterTindakan.TabIndex = 563
        '
        'DataGridViewAlatTindakan
        '
        Me.DataGridViewAlatTindakan.AllowUserToAddRows = False
        Me.DataGridViewAlatTindakan.AllowUserToDeleteRows = False
        Me.DataGridViewAlatTindakan.BackgroundColor = System.Drawing.Color.White
        Me.DataGridViewAlatTindakan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewAlatTindakan.Location = New System.Drawing.Point(808, 301)
        Me.DataGridViewAlatTindakan.Name = "DataGridViewAlatTindakan"
        Me.DataGridViewAlatTindakan.ReadOnly = True
        Me.DataGridViewAlatTindakan.Size = New System.Drawing.Size(428, 130)
        Me.DataGridViewAlatTindakan.TabIndex = 579
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ButtonTambahAlat)
        Me.GroupBox2.Controls.Add(Me.ComboBoxAlatTindakan)
        Me.GroupBox2.Controls.Add(Me.Label37)
        Me.GroupBox2.Controls.Add(Me.ComboBoxCartridgeTindakan)
        Me.GroupBox2.Controls.Add(Me.Label36)
        Me.GroupBox2.Controls.Add(Me.Label41)
        Me.GroupBox2.Controls.Add(Me.NumericShootPerTindakan)
        Me.GroupBox2.Controls.Add(Me.CheckBoxPakaiAlat)
        Me.GroupBox2.Location = New System.Drawing.Point(629, 293)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(174, 215)
        Me.GroupBox2.TabIndex = 578
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Pakai Alat ?"
        '
        'ButtonTambahAlat
        '
        Me.ButtonTambahAlat.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonTambahAlat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonTambahAlat.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonTambahAlat.FlatAppearance.BorderSize = 0
        Me.ButtonTambahAlat.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonTambahAlat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonTambahAlat.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonTambahAlat.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonTambahAlat.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.ButtonTambahAlat.Location = New System.Drawing.Point(92, 180)
        Me.ButtonTambahAlat.Name = "ButtonTambahAlat"
        Me.ButtonTambahAlat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonTambahAlat.Size = New System.Drawing.Size(73, 25)
        Me.ButtonTambahAlat.TabIndex = 587
        Me.ButtonTambahAlat.Text = " + Tambah"
        Me.ButtonTambahAlat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonTambahAlat.UseVisualStyleBackColor = False
        '
        'ComboBoxAlatTindakan
        '
        Me.ComboBoxAlatTindakan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxAlatTindakan.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxAlatTindakan.FormattingEnabled = True
        Me.ComboBoxAlatTindakan.Location = New System.Drawing.Point(11, 66)
        Me.ComboBoxAlatTindakan.Name = "ComboBoxAlatTindakan"
        Me.ComboBoxAlatTindakan.Size = New System.Drawing.Size(154, 25)
        Me.ComboBoxAlatTindakan.TabIndex = 586
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(7, 47)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(28, 15)
        Me.Label37.TabIndex = 585
        Me.Label37.Text = "Alat"
        '
        'ComboBoxCartridgeTindakan
        '
        Me.ComboBoxCartridgeTindakan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxCartridgeTindakan.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxCartridgeTindakan.FormattingEnabled = True
        Me.ComboBoxCartridgeTindakan.Location = New System.Drawing.Point(12, 115)
        Me.ComboBoxCartridgeTindakan.Name = "ComboBoxCartridgeTindakan"
        Me.ComboBoxCartridgeTindakan.Size = New System.Drawing.Size(154, 25)
        Me.ComboBoxCartridgeTindakan.TabIndex = 584
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(8, 96)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(56, 15)
        Me.Label36.TabIndex = 583
        Me.Label36.Text = "Cartridge"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label41.Location = New System.Drawing.Point(76, 154)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(78, 17)
        Me.Label41.TabIndex = 582
        Me.Label41.Text = "Shoot / Kali"
        '
        'NumericShootPerTindakan
        '
        Me.NumericShootPerTindakan.Location = New System.Drawing.Point(12, 152)
        Me.NumericShootPerTindakan.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.NumericShootPerTindakan.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericShootPerTindakan.Name = "NumericShootPerTindakan"
        Me.NumericShootPerTindakan.Size = New System.Drawing.Size(58, 23)
        Me.NumericShootPerTindakan.TabIndex = 580
        Me.NumericShootPerTindakan.ThousandsSeparator = True
        Me.NumericShootPerTindakan.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'CheckBoxPakaiAlat
        '
        Me.CheckBoxPakaiAlat.AutoSize = True
        Me.CheckBoxPakaiAlat.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxPakaiAlat.Location = New System.Drawing.Point(10, 22)
        Me.CheckBoxPakaiAlat.Name = "CheckBoxPakaiAlat"
        Me.CheckBoxPakaiAlat.Size = New System.Drawing.Size(42, 19)
        Me.CheckBoxPakaiAlat.TabIndex = 577
        Me.CheckBoxPakaiAlat.Text = "Iya"
        Me.CheckBoxPakaiAlat.UseVisualStyleBackColor = True
        '
        'ComboBoxKategori
        '
        Me.ComboBoxKategori.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxKategori.FormattingEnabled = True
        Me.ComboBoxKategori.Items.AddRange(New Object() {"Face", "Hair", "Body", "IV"})
        Me.ComboBoxKategori.Location = New System.Drawing.Point(629, 259)
        Me.ComboBoxKategori.Name = "ComboBoxKategori"
        Me.ComboBoxKategori.Size = New System.Drawing.Size(174, 25)
        Me.ComboBoxKategori.TabIndex = 576
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label34.Location = New System.Drawing.Point(626, 238)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(118, 17)
        Me.Label34.TabIndex = 575
        Me.Label34.Text = "Kategori Tindakan"
        '
        'CheckedListBoxPoli
        '
        Me.CheckedListBoxPoli.FormattingEnabled = True
        Me.CheckedListBoxPoli.Location = New System.Drawing.Point(629, 139)
        Me.CheckedListBoxPoli.Name = "CheckedListBoxPoli"
        Me.CheckedListBoxPoli.ScrollAlwaysVisible = True
        Me.CheckedListBoxPoli.Size = New System.Drawing.Size(174, 94)
        Me.CheckedListBoxPoli.TabIndex = 574
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label33)
        Me.GroupBox1.Controls.Add(Me.TextBoxHariReminder)
        Me.GroupBox1.Controls.Add(Me.CheckBoxReminder)
        Me.GroupBox1.Location = New System.Drawing.Point(629, 17)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(174, 116)
        Me.GroupBox1.TabIndex = 573
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Reminder Pasien ?"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label33.Location = New System.Drawing.Point(15, 51)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(85, 17)
        Me.Label33.TabIndex = 574
        Me.Label33.Text = "Berapa Hari?"
        '
        'TextBoxHariReminder
        '
        Me.TextBoxHariReminder.BackColor = System.Drawing.Color.White
        Me.TextBoxHariReminder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxHariReminder.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBoxHariReminder.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxHariReminder.Location = New System.Drawing.Point(18, 71)
        Me.TextBoxHariReminder.Name = "TextBoxHariReminder"
        Me.TextBoxHariReminder.Size = New System.Drawing.Size(109, 23)
        Me.TextBoxHariReminder.TabIndex = 573
        '
        'CheckBoxReminder
        '
        Me.CheckBoxReminder.AutoSize = True
        Me.CheckBoxReminder.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxReminder.Location = New System.Drawing.Point(18, 22)
        Me.CheckBoxReminder.Name = "CheckBoxReminder"
        Me.CheckBoxReminder.Size = New System.Drawing.Size(42, 19)
        Me.CheckBoxReminder.TabIndex = 572
        Me.CheckBoxReminder.Text = "Iya"
        Me.CheckBoxReminder.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Transparent
        Me.Panel5.Controls.Add(Me.RadioButtonPelaksanaDokter)
        Me.Panel5.Controls.Add(Me.RadioButtonPelaksanaPerawat)
        Me.Panel5.Location = New System.Drawing.Point(195, 168)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(327, 25)
        Me.Panel5.TabIndex = 571
        '
        'RadioButtonPelaksanaDokter
        '
        Me.RadioButtonPelaksanaDokter.AutoSize = True
        Me.RadioButtonPelaksanaDokter.Checked = True
        Me.RadioButtonPelaksanaDokter.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioButtonPelaksanaDokter.Location = New System.Drawing.Point(5, 3)
        Me.RadioButtonPelaksanaDokter.Name = "RadioButtonPelaksanaDokter"
        Me.RadioButtonPelaksanaDokter.Size = New System.Drawing.Size(60, 19)
        Me.RadioButtonPelaksanaDokter.TabIndex = 245
        Me.RadioButtonPelaksanaDokter.TabStop = True
        Me.RadioButtonPelaksanaDokter.Text = "Dokter"
        Me.RadioButtonPelaksanaDokter.UseVisualStyleBackColor = True
        '
        'RadioButtonPelaksanaPerawat
        '
        Me.RadioButtonPelaksanaPerawat.AutoSize = True
        Me.RadioButtonPelaksanaPerawat.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioButtonPelaksanaPerawat.Location = New System.Drawing.Point(71, 3)
        Me.RadioButtonPelaksanaPerawat.Name = "RadioButtonPelaksanaPerawat"
        Me.RadioButtonPelaksanaPerawat.Size = New System.Drawing.Size(67, 19)
        Me.RadioButtonPelaksanaPerawat.TabIndex = 246
        Me.RadioButtonPelaksanaPerawat.Text = "Perawat"
        Me.RadioButtonPelaksanaPerawat.UseVisualStyleBackColor = True
        '
        'CheckBoxStatusAktifTindakan
        '
        Me.CheckBoxStatusAktifTindakan.AutoSize = True
        Me.CheckBoxStatusAktifTindakan.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxStatusAktifTindakan.Location = New System.Drawing.Point(351, 51)
        Me.CheckBoxStatusAktifTindakan.Name = "CheckBoxStatusAktifTindakan"
        Me.CheckBoxStatusAktifTindakan.Size = New System.Drawing.Size(51, 19)
        Me.CheckBoxStatusAktifTindakan.TabIndex = 570
        Me.CheckBoxStatusAktifTindakan.Text = "Aktif"
        Me.CheckBoxStatusAktifTindakan.UseVisualStyleBackColor = True
        '
        'SplitContainerFeeTindakan
        '
        Me.SplitContainerFeeTindakan.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerFeeTindakan.Location = New System.Drawing.Point(12, 290)
        Me.SplitContainerFeeTindakan.Name = "SplitContainerFeeTindakan"
        Me.SplitContainerFeeTindakan.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerFeeTindakan.Panel1
        '
        Me.SplitContainerFeeTindakan.Panel1.Controls.Add(Me.Panel3)
        Me.SplitContainerFeeTindakan.Panel1.Controls.Add(Me.Label12)
        '
        'SplitContainerFeeTindakan.Panel2
        '
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.TextBoxPersentaseFeePerawat)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.Label42)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.Panel7)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.Label31)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.Label47)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.Panel6)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.Label28)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.TextBoxPersentaseFeeKaryawan)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.TextBoxPersentaseFeePerusahaan)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.Label27)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.TextBox14)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.TextBoxFeePerawatTambahan)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.Label21)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.TextBox9)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.TextBoxFeeDokterTambahan)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.Label16)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.TextBox2)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.TextBoxNominalFee)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.Label26)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.Label25)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.Label24)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.Label23)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.Label22)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.Panel4)
        Me.SplitContainerFeeTindakan.Panel2.Controls.Add(Me.Label13)
        Me.SplitContainerFeeTindakan.Size = New System.Drawing.Size(604, 320)
        Me.SplitContainerFeeTindakan.SplitterDistance = 31
        Me.SplitContainerFeeTindakan.SplitterWidth = 1
        Me.SplitContainerFeeTindakan.TabIndex = 567
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.Controls.Add(Me.RadioButtonAktifFee)
        Me.Panel3.Controls.Add(Me.RadioButtonNonAktifFee)
        Me.Panel3.Location = New System.Drawing.Point(183, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(327, 25)
        Me.Panel3.TabIndex = 564
        '
        'RadioButtonAktifFee
        '
        Me.RadioButtonAktifFee.AutoSize = True
        Me.RadioButtonAktifFee.Checked = True
        Me.RadioButtonAktifFee.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioButtonAktifFee.Location = New System.Drawing.Point(5, 3)
        Me.RadioButtonAktifFee.Name = "RadioButtonAktifFee"
        Me.RadioButtonAktifFee.Size = New System.Drawing.Size(37, 19)
        Me.RadioButtonAktifFee.TabIndex = 245
        Me.RadioButtonAktifFee.TabStop = True
        Me.RadioButtonAktifFee.Text = "Ya"
        Me.RadioButtonAktifFee.UseVisualStyleBackColor = True
        '
        'RadioButtonNonAktifFee
        '
        Me.RadioButtonNonAktifFee.AutoSize = True
        Me.RadioButtonNonAktifFee.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioButtonNonAktifFee.Location = New System.Drawing.Point(48, 3)
        Me.RadioButtonNonAktifFee.Name = "RadioButtonNonAktifFee"
        Me.RadioButtonNonAktifFee.Size = New System.Drawing.Size(54, 19)
        Me.RadioButtonNonAktifFee.TabIndex = 246
        Me.RadioButtonNonAktifFee.Text = "Tidak"
        Me.RadioButtonNonAktifFee.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label12.Location = New System.Drawing.Point(16, 6)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(148, 17)
        Me.Label12.TabIndex = 563
        Me.Label12.Text = "Aktifkan Fee Tindakan?"
        '
        'TextBoxPersentaseFeePerawat
        '
        Me.TextBoxPersentaseFeePerawat.BackColor = System.Drawing.Color.White
        Me.TextBoxPersentaseFeePerawat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxPersentaseFeePerawat.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxPersentaseFeePerawat.Location = New System.Drawing.Point(525, 34)
        Me.TextBoxPersentaseFeePerawat.Name = "TextBoxPersentaseFeePerawat"
        Me.TextBoxPersentaseFeePerawat.Size = New System.Drawing.Size(50, 23)
        Me.TextBoxPersentaseFeePerawat.TabIndex = 594
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.BackColor = System.Drawing.Color.Transparent
        Me.Label42.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label42.Location = New System.Drawing.Point(581, 37)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(17, 15)
        Me.Label42.TabIndex = 593
        Me.Label42.Text = "%"
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Transparent
        Me.Panel7.Controls.Add(Me.TextBox10)
        Me.Panel7.Controls.Add(Me.TextBox11)
        Me.Panel7.Controls.Add(Me.TextBox13)
        Me.Panel7.Controls.Add(Me.Label30)
        Me.Panel7.Controls.Add(Me.RadioButton3)
        Me.Panel7.Controls.Add(Me.RadioButton4)
        Me.Panel7.Location = New System.Drawing.Point(183, 216)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(327, 60)
        Me.Panel7.TabIndex = 591
        Me.Panel7.Visible = False
        '
        'TextBox10
        '
        Me.TextBox10.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBox10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox10.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox10.Location = New System.Drawing.Point(92, 32)
        Me.TextBox10.Name = "TextBox10"
        Me.TextBox10.Size = New System.Drawing.Size(35, 23)
        Me.TextBox10.TabIndex = 591
        Me.TextBox10.Text = "Rp"
        Me.TextBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox11
        '
        Me.TextBox11.BackColor = System.Drawing.Color.White
        Me.TextBox11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox11.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBox11.Location = New System.Drawing.Point(129, 32)
        Me.TextBox11.Name = "TextBox11"
        Me.TextBox11.Size = New System.Drawing.Size(129, 23)
        Me.TextBox11.TabIndex = 590
        '
        'TextBox13
        '
        Me.TextBox13.BackColor = System.Drawing.Color.White
        Me.TextBox13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox13.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBox13.Location = New System.Drawing.Point(92, 3)
        Me.TextBox13.Name = "TextBox13"
        Me.TextBox13.Size = New System.Drawing.Size(35, 23)
        Me.TextBox13.TabIndex = 593
        Me.TextBox13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label30.Location = New System.Drawing.Point(133, 5)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(17, 15)
        Me.Label30.TabIndex = 592
        Me.Label30.Text = "%"
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Checked = True
        Me.RadioButton3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioButton3.Location = New System.Drawing.Point(5, 3)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(81, 19)
        Me.RadioButton3.TabIndex = 245
        Me.RadioButton3.TabStop = True
        Me.RadioButton3.Text = "Persentase"
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'RadioButton4
        '
        Me.RadioButton4.AutoSize = True
        Me.RadioButton4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioButton4.Location = New System.Drawing.Point(5, 28)
        Me.RadioButton4.Name = "RadioButton4"
        Me.RadioButton4.Size = New System.Drawing.Size(71, 19)
        Me.RadioButton4.TabIndex = 246
        Me.RadioButton4.Text = "Nominal"
        Me.RadioButton4.UseVisualStyleBackColor = True
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label31.Location = New System.Drawing.Point(17, 219)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(82, 17)
        Me.Label31.TabIndex = 590
        Me.Label31.Text = "Fee Perawat"
        Me.Label31.Visible = False
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label47.Location = New System.Drawing.Point(472, 37)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(49, 15)
        Me.Label47.TabIndex = 592
        Me.Label47.Text = "Perawat"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Transparent
        Me.Panel6.Controls.Add(Me.TextBox4)
        Me.Panel6.Controls.Add(Me.TextBox6)
        Me.Panel6.Controls.Add(Me.TextBox8)
        Me.Panel6.Controls.Add(Me.Label29)
        Me.Panel6.Controls.Add(Me.RadioButton1)
        Me.Panel6.Controls.Add(Me.RadioButton2)
        Me.Panel6.Location = New System.Drawing.Point(183, 150)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(327, 60)
        Me.Panel6.TabIndex = 589
        Me.Panel6.Visible = False
        '
        'TextBox4
        '
        Me.TextBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox4.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox4.Location = New System.Drawing.Point(92, 32)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(35, 23)
        Me.TextBox4.TabIndex = 591
        Me.TextBox4.Text = "Rp"
        Me.TextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox6
        '
        Me.TextBox6.BackColor = System.Drawing.Color.White
        Me.TextBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBox6.Location = New System.Drawing.Point(129, 32)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(129, 23)
        Me.TextBox6.TabIndex = 590
        '
        'TextBox8
        '
        Me.TextBox8.BackColor = System.Drawing.Color.White
        Me.TextBox8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox8.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBox8.Location = New System.Drawing.Point(92, 3)
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New System.Drawing.Size(35, 23)
        Me.TextBox8.TabIndex = 593
        Me.TextBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label29.Location = New System.Drawing.Point(133, 5)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(17, 15)
        Me.Label29.TabIndex = 592
        Me.Label29.Text = "%"
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioButton1.Location = New System.Drawing.Point(5, 3)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(81, 19)
        Me.RadioButton1.TabIndex = 245
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Persentase"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioButton2.Location = New System.Drawing.Point(5, 28)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(71, 19)
        Me.RadioButton2.TabIndex = 246
        Me.RadioButton2.Text = "Nominal"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label28.Location = New System.Drawing.Point(17, 153)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(74, 17)
        Me.Label28.TabIndex = 588
        Me.Label28.Text = "Fee Dokter"
        Me.Label28.Visible = False
        '
        'TextBoxPersentaseFeeKaryawan
        '
        Me.TextBoxPersentaseFeeKaryawan.BackColor = System.Drawing.Color.White
        Me.TextBoxPersentaseFeeKaryawan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxPersentaseFeeKaryawan.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxPersentaseFeeKaryawan.Location = New System.Drawing.Point(393, 34)
        Me.TextBoxPersentaseFeeKaryawan.Name = "TextBoxPersentaseFeeKaryawan"
        Me.TextBoxPersentaseFeeKaryawan.Size = New System.Drawing.Size(50, 23)
        Me.TextBoxPersentaseFeeKaryawan.TabIndex = 587
        '
        'TextBoxPersentaseFeePerusahaan
        '
        Me.TextBoxPersentaseFeePerusahaan.BackColor = System.Drawing.Color.White
        Me.TextBoxPersentaseFeePerusahaan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxPersentaseFeePerusahaan.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxPersentaseFeePerusahaan.Location = New System.Drawing.Point(254, 34)
        Me.TextBoxPersentaseFeePerusahaan.Name = "TextBoxPersentaseFeePerusahaan"
        Me.TextBoxPersentaseFeePerusahaan.Size = New System.Drawing.Size(50, 23)
        Me.TextBoxPersentaseFeePerusahaan.TabIndex = 586
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label27.Location = New System.Drawing.Point(17, 121)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(157, 17)
        Me.Label27.TabIndex = 585
        Me.Label27.Text = "Fee Perawat (Tambahan)"
        '
        'TextBox14
        '
        Me.TextBox14.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBox14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox14.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox14.Location = New System.Drawing.Point(183, 121)
        Me.TextBox14.Name = "TextBox14"
        Me.TextBox14.Size = New System.Drawing.Size(35, 23)
        Me.TextBox14.TabIndex = 584
        Me.TextBox14.Text = "Rp"
        Me.TextBox14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBoxFeePerawatTambahan
        '
        Me.TextBoxFeePerawatTambahan.BackColor = System.Drawing.Color.White
        Me.TextBoxFeePerawatTambahan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxFeePerawatTambahan.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxFeePerawatTambahan.Location = New System.Drawing.Point(220, 121)
        Me.TextBoxFeePerawatTambahan.Name = "TextBoxFeePerawatTambahan"
        Me.TextBoxFeePerawatTambahan.Size = New System.Drawing.Size(129, 23)
        Me.TextBoxFeePerawatTambahan.TabIndex = 583
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label21.Location = New System.Drawing.Point(17, 92)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(149, 17)
        Me.Label21.TabIndex = 582
        Me.Label21.Text = "Fee Dokter (Tambahan)"
        '
        'TextBox9
        '
        Me.TextBox9.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBox9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox9.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox9.Location = New System.Drawing.Point(183, 92)
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.Size = New System.Drawing.Size(35, 23)
        Me.TextBox9.TabIndex = 581
        Me.TextBox9.Text = "Rp"
        Me.TextBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBoxFeeDokterTambahan
        '
        Me.TextBoxFeeDokterTambahan.BackColor = System.Drawing.Color.White
        Me.TextBoxFeeDokterTambahan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxFeeDokterTambahan.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxFeeDokterTambahan.Location = New System.Drawing.Point(220, 92)
        Me.TextBoxFeeDokterTambahan.Name = "TextBoxFeeDokterTambahan"
        Me.TextBoxFeeDokterTambahan.Size = New System.Drawing.Size(129, 23)
        Me.TextBoxFeeDokterTambahan.TabIndex = 580
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label16.Location = New System.Drawing.Point(16, 63)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(84, 17)
        Me.Label16.TabIndex = 579
        Me.Label16.Text = "Nominal Fee"
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox2.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox2.Location = New System.Drawing.Point(183, 63)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(35, 23)
        Me.TextBox2.TabIndex = 578
        Me.TextBox2.Text = "Rp"
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBoxNominalFee
        '
        Me.TextBoxNominalFee.BackColor = System.Drawing.Color.White
        Me.TextBoxNominalFee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxNominalFee.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxNominalFee.Location = New System.Drawing.Point(220, 63)
        Me.TextBoxNominalFee.Name = "TextBoxNominalFee"
        Me.TextBoxNominalFee.Size = New System.Drawing.Size(129, 23)
        Me.TextBoxNominalFee.TabIndex = 577
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label26.Location = New System.Drawing.Point(449, 36)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(17, 15)
        Me.Label26.TabIndex = 576
        Me.Label26.Text = "%"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(329, 36)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(58, 15)
        Me.Label25.TabIndex = 574
        Me.Label25.Text = "Karyawan"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label24.Location = New System.Drawing.Point(310, 36)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(17, 15)
        Me.Label24.TabIndex = 573
        Me.Label24.Text = "%"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label23.Location = New System.Drawing.Point(180, 36)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(68, 15)
        Me.Label23.TabIndex = 572
        Me.Label23.Text = "Perusahaan"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label22.Location = New System.Drawing.Point(16, 34)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(99, 17)
        Me.Label22.TabIndex = 567
        Me.Label22.Text = "Persentase Fee"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.Controls.Add(Me.RadioButtonOpsiPersentase)
        Me.Panel4.Controls.Add(Me.RadioButtonOpsiNominal)
        Me.Panel4.Location = New System.Drawing.Point(183, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(327, 25)
        Me.Panel4.TabIndex = 566
        '
        'RadioButtonOpsiPersentase
        '
        Me.RadioButtonOpsiPersentase.AutoSize = True
        Me.RadioButtonOpsiPersentase.Checked = True
        Me.RadioButtonOpsiPersentase.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioButtonOpsiPersentase.Location = New System.Drawing.Point(5, 3)
        Me.RadioButtonOpsiPersentase.Name = "RadioButtonOpsiPersentase"
        Me.RadioButtonOpsiPersentase.Size = New System.Drawing.Size(81, 19)
        Me.RadioButtonOpsiPersentase.TabIndex = 245
        Me.RadioButtonOpsiPersentase.TabStop = True
        Me.RadioButtonOpsiPersentase.Text = "Persentase"
        Me.RadioButtonOpsiPersentase.UseVisualStyleBackColor = True
        '
        'RadioButtonOpsiNominal
        '
        Me.RadioButtonOpsiNominal.AutoSize = True
        Me.RadioButtonOpsiNominal.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioButtonOpsiNominal.Location = New System.Drawing.Point(92, 3)
        Me.RadioButtonOpsiNominal.Name = "RadioButtonOpsiNominal"
        Me.RadioButtonOpsiNominal.Size = New System.Drawing.Size(71, 19)
        Me.RadioButtonOpsiNominal.TabIndex = 246
        Me.RadioButtonOpsiNominal.Text = "Nominal"
        Me.RadioButtonOpsiNominal.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label13.Location = New System.Drawing.Point(16, 6)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(62, 17)
        Me.Label13.TabIndex = 565
        Me.Label13.Text = "Jenis Fee"
        '
        'ComboBoxInformedConsent
        '
        Me.ComboBoxInformedConsent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxInformedConsent.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxInformedConsent.FormattingEnabled = True
        Me.ComboBoxInformedConsent.Location = New System.Drawing.Point(239, 199)
        Me.ComboBoxInformedConsent.Name = "ComboBoxInformedConsent"
        Me.ComboBoxInformedConsent.Size = New System.Drawing.Size(283, 25)
        Me.ComboBoxInformedConsent.TabIndex = 565
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label11.Location = New System.Drawing.Point(28, 202)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(118, 17)
        Me.Label11.TabIndex = 564
        Me.Label11.Text = "Informed Consent"
        '
        'CheckBoxInformedConsent
        '
        Me.CheckBoxInformedConsent.AutoSize = True
        Me.CheckBoxInformedConsent.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxInformedConsent.Location = New System.Drawing.Point(195, 203)
        Me.CheckBoxInformedConsent.Name = "CheckBoxInformedConsent"
        Me.CheckBoxInformedConsent.Size = New System.Drawing.Size(38, 19)
        Me.CheckBoxInformedConsent.TabIndex = 563
        Me.CheckBoxInformedConsent.Text = "Ya"
        Me.CheckBoxInformedConsent.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label18.Location = New System.Drawing.Point(28, 20)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(63, 17)
        Me.Label18.TabIndex = 528
        Me.Label18.Text = "Jenis Poli"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.RadioButtonBHP)
        Me.Panel2.Controls.Add(Me.RadioButtonNonBHP)
        Me.Panel2.Location = New System.Drawing.Point(195, 230)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(327, 25)
        Me.Panel2.TabIndex = 562
        '
        'RadioButtonBHP
        '
        Me.RadioButtonBHP.AutoSize = True
        Me.RadioButtonBHP.Checked = True
        Me.RadioButtonBHP.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioButtonBHP.Location = New System.Drawing.Point(5, 3)
        Me.RadioButtonBHP.Name = "RadioButtonBHP"
        Me.RadioButtonBHP.Size = New System.Drawing.Size(48, 19)
        Me.RadioButtonBHP.TabIndex = 245
        Me.RadioButtonBHP.TabStop = True
        Me.RadioButtonBHP.Text = "BHP"
        Me.RadioButtonBHP.UseVisualStyleBackColor = True
        '
        'RadioButtonNonBHP
        '
        Me.RadioButtonNonBHP.AutoSize = True
        Me.RadioButtonNonBHP.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioButtonNonBHP.Location = New System.Drawing.Point(59, 3)
        Me.RadioButtonNonBHP.Name = "RadioButtonNonBHP"
        Me.RadioButtonNonBHP.Size = New System.Drawing.Size(74, 19)
        Me.RadioButtonNonBHP.TabIndex = 246
        Me.RadioButtonNonBHP.Text = "Non BHP"
        Me.RadioButtonNonBHP.UseVisualStyleBackColor = True
        '
        'TextBoxTindakanID
        '
        Me.TextBoxTindakanID.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBoxTindakanID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxTindakanID.Cursor = System.Windows.Forms.Cursors.No
        Me.TextBoxTindakanID.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxTindakanID.Location = New System.Drawing.Point(261, 50)
        Me.TextBoxTindakanID.Name = "TextBoxTindakanID"
        Me.TextBoxTindakanID.ReadOnly = True
        Me.TextBoxTindakanID.Size = New System.Drawing.Size(84, 23)
        Me.TextBoxTindakanID.TabIndex = 526
        Me.TextBoxTindakanID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label14.Location = New System.Drawing.Point(28, 233)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(76, 17)
        Me.Label14.TabIndex = 561
        Me.Label14.Text = "Status BHP"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label19.Location = New System.Drawing.Point(28, 79)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(103, 17)
        Me.Label19.TabIndex = 525
        Me.Label19.Text = "Nama Tindakan"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label15.Location = New System.Drawing.Point(28, 171)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(68, 17)
        Me.Label15.TabIndex = 560
        Me.Label15.Text = "Pelaksana"
        '
        'ButtonFindTindakan
        '
        Me.ButtonFindTindakan.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonFindTindakan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonFindTindakan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonFindTindakan.FlatAppearance.BorderSize = 0
        Me.ButtonFindTindakan.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonFindTindakan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonFindTindakan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonFindTindakan.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonFindTindakan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.ButtonFindTindakan.Location = New System.Drawing.Point(195, 48)
        Me.ButtonFindTindakan.Name = "ButtonFindTindakan"
        Me.ButtonFindTindakan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonFindTindakan.Size = New System.Drawing.Size(60, 25)
        Me.ButtonFindTindakan.TabIndex = 527
        Me.ButtonFindTindakan.Text = " Cari"
        Me.ButtonFindTindakan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonFindTindakan.UseVisualStyleBackColor = False
        '
        'TextBoxNamaTindakan
        '
        Me.TextBoxNamaTindakan.BackColor = System.Drawing.Color.White
        Me.TextBoxNamaTindakan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxNamaTindakan.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBoxNamaTindakan.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxNamaTindakan.Location = New System.Drawing.Point(195, 79)
        Me.TextBoxNamaTindakan.Name = "TextBoxNamaTindakan"
        Me.TextBoxNamaTindakan.Size = New System.Drawing.Size(327, 23)
        Me.TextBoxNamaTindakan.TabIndex = 531
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label20.Location = New System.Drawing.Point(28, 111)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(79, 17)
        Me.Label20.TabIndex = 558
        Me.Label20.Text = "Group Jenis"
        '
        'TextBoxBiayaTindakan
        '
        Me.TextBoxBiayaTindakan.BackColor = System.Drawing.Color.White
        Me.TextBoxBiayaTindakan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxBiayaTindakan.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxBiayaTindakan.Location = New System.Drawing.Point(232, 139)
        Me.TextBoxBiayaTindakan.Name = "TextBoxBiayaTindakan"
        Me.TextBoxBiayaTindakan.Size = New System.Drawing.Size(129, 23)
        Me.TextBoxBiayaTindakan.TabIndex = 534
        '
        'ComboBoxGroupJenisTindakan
        '
        Me.ComboBoxGroupJenisTindakan.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxGroupJenisTindakan.FormattingEnabled = True
        Me.ComboBoxGroupJenisTindakan.Location = New System.Drawing.Point(195, 108)
        Me.ComboBoxGroupJenisTindakan.Name = "ComboBoxGroupJenisTindakan"
        Me.ComboBoxGroupJenisTindakan.Size = New System.Drawing.Size(327, 25)
        Me.ComboBoxGroupJenisTindakan.TabIndex = 557
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label17.Location = New System.Drawing.Point(28, 139)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(99, 17)
        Me.Label17.TabIndex = 535
        Me.Label17.Text = "Biaya Tindakan"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label9.Location = New System.Drawing.Point(28, 51)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 17)
        Me.Label9.TabIndex = 556
        Me.Label9.Text = "Tindakan ID"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.RadioButtonPoliUmum)
        Me.Panel1.Controls.Add(Me.RadioButtonPoliGigi)
        Me.Panel1.Location = New System.Drawing.Point(195, 17)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(327, 25)
        Me.Panel1.TabIndex = 555
        '
        'RadioButtonPoliUmum
        '
        Me.RadioButtonPoliUmum.AutoSize = True
        Me.RadioButtonPoliUmum.Checked = True
        Me.RadioButtonPoliUmum.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioButtonPoliUmum.Location = New System.Drawing.Point(5, 3)
        Me.RadioButtonPoliUmum.Name = "RadioButtonPoliUmum"
        Me.RadioButtonPoliUmum.Size = New System.Drawing.Size(85, 19)
        Me.RadioButtonPoliUmum.TabIndex = 245
        Me.RadioButtonPoliUmum.TabStop = True
        Me.RadioButtonPoliUmum.Text = "Poli Umum"
        Me.RadioButtonPoliUmum.UseVisualStyleBackColor = True
        '
        'RadioButtonPoliGigi
        '
        Me.RadioButtonPoliGigi.AutoSize = True
        Me.RadioButtonPoliGigi.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioButtonPoliGigi.Location = New System.Drawing.Point(96, 3)
        Me.RadioButtonPoliGigi.Name = "RadioButtonPoliGigi"
        Me.RadioButtonPoliGigi.Size = New System.Drawing.Size(69, 19)
        Me.RadioButtonPoliGigi.TabIndex = 246
        Me.RadioButtonPoliGigi.Text = "Poli Gigi"
        Me.RadioButtonPoliGigi.UseVisualStyleBackColor = True
        '
        'TextBox7
        '
        Me.TextBox7.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox7.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox7.Location = New System.Drawing.Point(195, 139)
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New System.Drawing.Size(35, 23)
        Me.TextBox7.TabIndex = 549
        Me.TextBox7.Text = "Rp"
        Me.TextBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox5
        '
        Me.TextBox5.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox5.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox5.Location = New System.Drawing.Point(195, 261)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(35, 23)
        Me.TextBox5.TabIndex = 554
        Me.TextBox5.Text = "Rp"
        Me.TextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBoxMaksimalBHP_Tindakan
        '
        Me.TextBoxMaksimalBHP_Tindakan.BackColor = System.Drawing.Color.White
        Me.TextBoxMaksimalBHP_Tindakan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxMaksimalBHP_Tindakan.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxMaksimalBHP_Tindakan.Location = New System.Drawing.Point(232, 261)
        Me.TextBoxMaksimalBHP_Tindakan.Name = "TextBoxMaksimalBHP_Tindakan"
        Me.TextBoxMaksimalBHP_Tindakan.Size = New System.Drawing.Size(129, 23)
        Me.TextBoxMaksimalBHP_Tindakan.TabIndex = 552
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label10.Location = New System.Drawing.Point(28, 261)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(95, 17)
        Me.Label10.TabIndex = 553
        Me.Label10.Text = "Maksimal BHP"
        '
        'TabPageMasterPaket
        '
        Me.TabPageMasterPaket.AutoScroll = True
        Me.TabPageMasterPaket.BackColor = System.Drawing.Color.White
        Me.TabPageMasterPaket.Controls.Add(Me.SplitContainerPendaftaran)
        Me.TabPageMasterPaket.Location = New System.Drawing.Point(4, 27)
        Me.TabPageMasterPaket.Name = "TabPageMasterPaket"
        Me.TabPageMasterPaket.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageMasterPaket.Size = New System.Drawing.Size(763, 634)
        Me.TabPageMasterPaket.TabIndex = 0
        Me.TabPageMasterPaket.Text = "Master Paket"
        Me.TabPageMasterPaket.UseVisualStyleBackColor = True
        '
        'SplitContainerPendaftaran
        '
        Me.SplitContainerPendaftaran.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerPendaftaran.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerPendaftaran.IsSplitterFixed = True
        Me.SplitContainerPendaftaran.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainerPendaftaran.Name = "SplitContainerPendaftaran"
        Me.SplitContainerPendaftaran.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerPendaftaran.Panel1
        '
        Me.SplitContainerPendaftaran.Panel1.Controls.Add(Me.FlowLayoutPanel4)
        '
        'SplitContainerPendaftaran.Panel2
        '
        Me.SplitContainerPendaftaran.Panel2.AutoScroll = True
        Me.SplitContainerPendaftaran.Panel2.Controls.Add(Me.SplitContainerMasterPaket)
        Me.SplitContainerPendaftaran.Size = New System.Drawing.Size(757, 628)
        Me.SplitContainerPendaftaran.SplitterDistance = 46
        Me.SplitContainerPendaftaran.SplitterWidth = 1
        Me.SplitContainerPendaftaran.TabIndex = 461
        '
        'FlowLayoutPanel4
        '
        Me.FlowLayoutPanel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.FlowLayoutPanel4.Controls.Add(Me.ButtonSimpanMasterPaket)
        Me.FlowLayoutPanel4.Controls.Add(Me.ButtonClearMasterPaket)
        Me.FlowLayoutPanel4.Controls.Add(Me.ButtonExportMasterPaket)
        Me.FlowLayoutPanel4.Controls.Add(Me.Button1)
        Me.FlowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel4.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel4.Name = "FlowLayoutPanel4"
        Me.FlowLayoutPanel4.Size = New System.Drawing.Size(757, 46)
        Me.FlowLayoutPanel4.TabIndex = 538
        '
        'ButtonSimpanMasterPaket
        '
        Me.ButtonSimpanMasterPaket.BackColor = System.Drawing.Color.White
        Me.ButtonSimpanMasterPaket.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonSimpanMasterPaket.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonSimpanMasterPaket.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonSimpanMasterPaket.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonSimpanMasterPaket.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonSimpanMasterPaket.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSimpanMasterPaket.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonSimpanMasterPaket.Image = CType(resources.GetObject("ButtonSimpanMasterPaket.Image"), System.Drawing.Image)
        Me.ButtonSimpanMasterPaket.Location = New System.Drawing.Point(3, 3)
        Me.ButtonSimpanMasterPaket.Name = "ButtonSimpanMasterPaket"
        Me.ButtonSimpanMasterPaket.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonSimpanMasterPaket.Size = New System.Drawing.Size(175, 40)
        Me.ButtonSimpanMasterPaket.TabIndex = 586
        Me.ButtonSimpanMasterPaket.Text = " Simpan Paket"
        Me.ButtonSimpanMasterPaket.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonSimpanMasterPaket.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonSimpanMasterPaket.UseVisualStyleBackColor = False
        '
        'ButtonClearMasterPaket
        '
        Me.ButtonClearMasterPaket.BackColor = System.Drawing.Color.White
        Me.ButtonClearMasterPaket.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonClearMasterPaket.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonClearMasterPaket.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonClearMasterPaket.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonClearMasterPaket.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonClearMasterPaket.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonClearMasterPaket.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonClearMasterPaket.Image = CType(resources.GetObject("ButtonClearMasterPaket.Image"), System.Drawing.Image)
        Me.ButtonClearMasterPaket.Location = New System.Drawing.Point(184, 3)
        Me.ButtonClearMasterPaket.Name = "ButtonClearMasterPaket"
        Me.ButtonClearMasterPaket.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonClearMasterPaket.Size = New System.Drawing.Size(150, 40)
        Me.ButtonClearMasterPaket.TabIndex = 587
        Me.ButtonClearMasterPaket.Text = " Clear Data"
        Me.ButtonClearMasterPaket.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonClearMasterPaket.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonClearMasterPaket.UseVisualStyleBackColor = False
        '
        'ButtonExportMasterPaket
        '
        Me.ButtonExportMasterPaket.BackColor = System.Drawing.Color.White
        Me.ButtonExportMasterPaket.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonExportMasterPaket.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonExportMasterPaket.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonExportMasterPaket.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonExportMasterPaket.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonExportMasterPaket.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonExportMasterPaket.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonExportMasterPaket.Image = CType(resources.GetObject("ButtonExportMasterPaket.Image"), System.Drawing.Image)
        Me.ButtonExportMasterPaket.Location = New System.Drawing.Point(340, 3)
        Me.ButtonExportMasterPaket.Name = "ButtonExportMasterPaket"
        Me.ButtonExportMasterPaket.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonExportMasterPaket.Size = New System.Drawing.Size(150, 40)
        Me.ButtonExportMasterPaket.TabIndex = 588
        Me.ButtonExportMasterPaket.Text = " Export To CSV"
        Me.ButtonExportMasterPaket.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonExportMasterPaket.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonExportMasterPaket.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(496, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Button1.Size = New System.Drawing.Size(120, 35)
        Me.Button1.TabIndex = 464
        Me.Button1.Text = " Generate"
        Me.Button1.UseVisualStyleBackColor = False
        Me.Button1.Visible = False
        '
        'SplitContainerMasterPaket
        '
        Me.SplitContainerMasterPaket.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerMasterPaket.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerMasterPaket.IsSplitterFixed = True
        Me.SplitContainerMasterPaket.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerMasterPaket.Name = "SplitContainerMasterPaket"
        Me.SplitContainerMasterPaket.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerMasterPaket.Panel1
        '
        Me.SplitContainerMasterPaket.Panel1.BackColor = System.Drawing.Color.White
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.ComboBoxICPaket)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.CheckBoxICPaket)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.Label35)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.CheckedListBoxPoli2)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.TextBox15)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.TextBoxMaksimalBHP_Paket)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.Label32)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.Panel8)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.TextBox3)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.Label8)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.TextBoxHargaNett)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.TextBox1)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.Label7)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.TextBoxTotalDiskon)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.ButtonHapusDetailTindakan)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.TextBox12)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.TextBoxDurasiAktifPaket)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.TextBoxKuotaPaket)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.ButtonDetailTindakanPaket)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.Label54)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.ButtonKalkulasiPaket)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.TglAkhirPaket)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.TglAwalPaket)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.LabelKuotaPaket)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.TextBoxHargaAwal)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.TextBoxKeteranganPaket)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.TextBoxNamaPaket)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.Label157)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.ButtonFindPaket)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.Label203)
        Me.SplitContainerMasterPaket.Panel1.Controls.Add(Me.TextBoxReffID)
        '
        'SplitContainerMasterPaket.Panel2
        '
        Me.SplitContainerMasterPaket.Panel2.AutoScroll = True
        Me.SplitContainerMasterPaket.Panel2.Controls.Add(Me.GridControlTindakan)
        Me.SplitContainerMasterPaket.Size = New System.Drawing.Size(757, 581)
        Me.SplitContainerMasterPaket.SplitterDistance = 380
        Me.SplitContainerMasterPaket.SplitterWidth = 1
        Me.SplitContainerMasterPaket.TabIndex = 462
        '
        'ComboBoxICPaket
        '
        Me.ComboBoxICPaket.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxICPaket.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxICPaket.FormattingEnabled = True
        Me.ComboBoxICPaket.Location = New System.Drawing.Point(203, 199)
        Me.ComboBoxICPaket.Name = "ComboBoxICPaket"
        Me.ComboBoxICPaket.Size = New System.Drawing.Size(283, 25)
        Me.ComboBoxICPaket.TabIndex = 580
        '
        'CheckBoxICPaket
        '
        Me.CheckBoxICPaket.AutoSize = True
        Me.CheckBoxICPaket.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxICPaket.Location = New System.Drawing.Point(159, 203)
        Me.CheckBoxICPaket.Name = "CheckBoxICPaket"
        Me.CheckBoxICPaket.Size = New System.Drawing.Size(38, 19)
        Me.CheckBoxICPaket.TabIndex = 579
        Me.CheckBoxICPaket.Text = "Ya"
        Me.CheckBoxICPaket.UseVisualStyleBackColor = True
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label35.Location = New System.Drawing.Point(28, 202)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(118, 17)
        Me.Label35.TabIndex = 578
        Me.Label35.Text = "Informed Consent"
        '
        'CheckedListBoxPoli2
        '
        Me.CheckedListBoxPoli2.FormattingEnabled = True
        Me.CheckedListBoxPoli2.Location = New System.Drawing.Point(496, 20)
        Me.CheckedListBoxPoli2.Name = "CheckedListBoxPoli2"
        Me.CheckedListBoxPoli2.ScrollAlwaysVisible = True
        Me.CheckedListBoxPoli2.Size = New System.Drawing.Size(174, 94)
        Me.CheckedListBoxPoli2.TabIndex = 577
        '
        'TextBox15
        '
        Me.TextBox15.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBox15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox15.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox15.Location = New System.Drawing.Point(159, 232)
        Me.TextBox15.Name = "TextBox15"
        Me.TextBox15.Size = New System.Drawing.Size(35, 23)
        Me.TextBox15.TabIndex = 576
        Me.TextBox15.Text = "Rp"
        Me.TextBox15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(28, 232)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 17)
        Me.Label2.TabIndex = 575
        Me.Label2.Text = "Maksimal BHP"
        '
        'TextBoxMaksimalBHP_Paket
        '
        Me.TextBoxMaksimalBHP_Paket.BackColor = System.Drawing.Color.White
        Me.TextBoxMaksimalBHP_Paket.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxMaksimalBHP_Paket.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxMaksimalBHP_Paket.Location = New System.Drawing.Point(196, 232)
        Me.TextBoxMaksimalBHP_Paket.Name = "TextBoxMaksimalBHP_Paket"
        Me.TextBoxMaksimalBHP_Paket.Size = New System.Drawing.Size(129, 23)
        Me.TextBoxMaksimalBHP_Paket.TabIndex = 574
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label32.Location = New System.Drawing.Point(28, 141)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(75, 17)
        Me.Label32.TabIndex = 573
        Me.Label32.Text = "Jenis Paket"
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Transparent
        Me.Panel8.Controls.Add(Me.RadioButtonPaketReguler)
        Me.Panel8.Controls.Add(Me.RadioButtonPaketPerTindakan)
        Me.Panel8.Location = New System.Drawing.Point(159, 138)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(327, 25)
        Me.Panel8.TabIndex = 572
        '
        'RadioButtonPaketReguler
        '
        Me.RadioButtonPaketReguler.AutoSize = True
        Me.RadioButtonPaketReguler.Checked = True
        Me.RadioButtonPaketReguler.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioButtonPaketReguler.Location = New System.Drawing.Point(5, 3)
        Me.RadioButtonPaketReguler.Name = "RadioButtonPaketReguler"
        Me.RadioButtonPaketReguler.Size = New System.Drawing.Size(97, 19)
        Me.RadioButtonPaketReguler.TabIndex = 245
        Me.RadioButtonPaketReguler.TabStop = True
        Me.RadioButtonPaketReguler.Text = "Paket Reguler"
        Me.RadioButtonPaketReguler.UseVisualStyleBackColor = True
        '
        'RadioButtonPaketPerTindakan
        '
        Me.RadioButtonPaketPerTindakan.AutoSize = True
        Me.RadioButtonPaketPerTindakan.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.RadioButtonPaketPerTindakan.Location = New System.Drawing.Point(108, 3)
        Me.RadioButtonPaketPerTindakan.Name = "RadioButtonPaketPerTindakan"
        Me.RadioButtonPaketPerTindakan.Size = New System.Drawing.Size(128, 19)
        Me.RadioButtonPaketPerTindakan.TabIndex = 246
        Me.RadioButtonPaketPerTindakan.Text = "Paket Per-Tindakan"
        Me.RadioButtonPaketPerTindakan.UseVisualStyleBackColor = True
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox3.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox3.Location = New System.Drawing.Point(159, 319)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(35, 23)
        Me.TextBox3.TabIndex = 557
        Me.TextBox3.Text = "Rp"
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label8.Location = New System.Drawing.Point(28, 319)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(76, 17)
        Me.Label8.TabIndex = 556
        Me.Label8.Text = "Harga Nett"
        '
        'TextBoxHargaNett
        '
        Me.TextBoxHargaNett.BackColor = System.Drawing.Color.White
        Me.TextBoxHargaNett.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxHargaNett.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxHargaNett.Location = New System.Drawing.Point(196, 319)
        Me.TextBoxHargaNett.Name = "TextBoxHargaNett"
        Me.TextBoxHargaNett.Size = New System.Drawing.Size(129, 23)
        Me.TextBoxHargaNett.TabIndex = 555
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox1.Location = New System.Drawing.Point(159, 290)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(35, 23)
        Me.TextBox1.TabIndex = 554
        Me.TextBox1.Text = "Rp"
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(28, 290)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 17)
        Me.Label7.TabIndex = 553
        Me.Label7.Text = "Diskon"
        '
        'TextBoxTotalDiskon
        '
        Me.TextBoxTotalDiskon.BackColor = System.Drawing.Color.White
        Me.TextBoxTotalDiskon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxTotalDiskon.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxTotalDiskon.Location = New System.Drawing.Point(196, 290)
        Me.TextBoxTotalDiskon.Name = "TextBoxTotalDiskon"
        Me.TextBoxTotalDiskon.Size = New System.Drawing.Size(129, 23)
        Me.TextBoxTotalDiskon.TabIndex = 552
        '
        'ButtonHapusDetailTindakan
        '
        Me.ButtonHapusDetailTindakan.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.ButtonHapusDetailTindakan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ButtonHapusDetailTindakan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonHapusDetailTindakan.FlatAppearance.BorderSize = 0
        Me.ButtonHapusDetailTindakan.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.ButtonHapusDetailTindakan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonHapusDetailTindakan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonHapusDetailTindakan.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonHapusDetailTindakan.ForeColor = System.Drawing.Color.White
        Me.ButtonHapusDetailTindakan.Location = New System.Drawing.Point(159, 348)
        Me.ButtonHapusDetailTindakan.Name = "ButtonHapusDetailTindakan"
        Me.ButtonHapusDetailTindakan.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ButtonHapusDetailTindakan.Size = New System.Drawing.Size(166, 25)
        Me.ButtonHapusDetailTindakan.TabIndex = 551
        Me.ButtonHapusDetailTindakan.Text = "Hapus Detail"
        Me.ButtonHapusDetailTindakan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonHapusDetailTindakan.UseVisualStyleBackColor = False
        '
        'TextBox12
        '
        Me.TextBox12.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBox12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox12.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox12.Location = New System.Drawing.Point(159, 261)
        Me.TextBox12.Name = "TextBox12"
        Me.TextBox12.Size = New System.Drawing.Size(35, 23)
        Me.TextBox12.TabIndex = 549
        Me.TextBox12.Text = "Rp"
        Me.TextBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBoxDurasiAktifPaket
        '
        Me.TextBoxDurasiAktifPaket.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxDurasiAktifPaket.Location = New System.Drawing.Point(159, 169)
        Me.TextBoxDurasiAktifPaket.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.TextBoxDurasiAktifPaket.Name = "TextBoxDurasiAktifPaket"
        Me.TextBoxDurasiAktifPaket.Size = New System.Drawing.Size(50, 23)
        Me.TextBoxDurasiAktifPaket.TabIndex = 548
        '
        'TextBoxKuotaPaket
        '
        Me.TextBoxKuotaPaket.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxKuotaPaket.Location = New System.Drawing.Point(436, 169)
        Me.TextBoxKuotaPaket.Name = "TextBoxKuotaPaket"
        Me.TextBoxKuotaPaket.Size = New System.Drawing.Size(50, 23)
        Me.TextBoxKuotaPaket.TabIndex = 547
        '
        'ButtonDetailTindakanPaket
        '
        Me.ButtonDetailTindakanPaket.BackColor = System.Drawing.Color.White
        Me.ButtonDetailTindakanPaket.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonDetailTindakanPaket.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonDetailTindakanPaket.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonDetailTindakanPaket.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonDetailTindakanPaket.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonDetailTindakanPaket.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonDetailTindakanPaket.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ButtonDetailTindakanPaket.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonDetailTindakanPaket.Location = New System.Drawing.Point(331, 348)
        Me.ButtonDetailTindakanPaket.Name = "ButtonDetailTindakanPaket"
        Me.ButtonDetailTindakanPaket.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonDetailTindakanPaket.Size = New System.Drawing.Size(155, 25)
        Me.ButtonDetailTindakanPaket.TabIndex = 546
        Me.ButtonDetailTindakanPaket.Text = " Tambah Detail Tindakan"
        Me.ButtonDetailTindakanPaket.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonDetailTindakanPaket.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(222, 173)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 15)
        Me.Label6.TabIndex = 545
        Me.Label6.Text = "Hari"
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.BackColor = System.Drawing.Color.Transparent
        Me.Label54.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.Location = New System.Drawing.Point(311, 113)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(24, 15)
        Me.Label54.TabIndex = 543
        Me.Label54.Text = "s/d"
        '
        'ButtonKalkulasiPaket
        '
        Me.ButtonKalkulasiPaket.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonKalkulasiPaket.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonKalkulasiPaket.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonKalkulasiPaket.FlatAppearance.BorderSize = 0
        Me.ButtonKalkulasiPaket.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonKalkulasiPaket.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonKalkulasiPaket.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonKalkulasiPaket.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonKalkulasiPaket.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.ButtonKalkulasiPaket.Location = New System.Drawing.Point(331, 317)
        Me.ButtonKalkulasiPaket.Name = "ButtonKalkulasiPaket"
        Me.ButtonKalkulasiPaket.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonKalkulasiPaket.Size = New System.Drawing.Size(155, 25)
        Me.ButtonKalkulasiPaket.TabIndex = 542
        Me.ButtonKalkulasiPaket.Text = " Kalkulasi"
        Me.ButtonKalkulasiPaket.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonKalkulasiPaket.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(28, 169)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 17)
        Me.Label5.TabIndex = 541
        Me.Label5.Text = "Durasi Aktif"
        '
        'TglAkhirPaket
        '
        Me.TglAkhirPaket.CalendarFont = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TglAkhirPaket.CustomFormat = "dd-MMM-yyyy"
        Me.TglAkhirPaket.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TglAkhirPaket.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TglAkhirPaket.Location = New System.Drawing.Point(336, 107)
        Me.TglAkhirPaket.Name = "TglAkhirPaket"
        Me.TglAkhirPaket.Size = New System.Drawing.Size(150, 25)
        Me.TglAkhirPaket.TabIndex = 540
        '
        'TglAwalPaket
        '
        Me.TglAwalPaket.CalendarFont = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TglAwalPaket.CustomFormat = "dd-MMM-yyyy"
        Me.TglAwalPaket.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TglAwalPaket.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TglAwalPaket.Location = New System.Drawing.Point(159, 107)
        Me.TglAwalPaket.Name = "TglAwalPaket"
        Me.TglAwalPaket.Size = New System.Drawing.Size(150, 25)
        Me.TglAwalPaket.TabIndex = 539
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(28, 111)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 17)
        Me.Label4.TabIndex = 538
        Me.Label4.Text = "Tgl Aktif"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(28, 78)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 17)
        Me.Label3.TabIndex = 537
        Me.Label3.Text = "Ket. Paket"
        '
        'LabelKuotaPaket
        '
        Me.LabelKuotaPaket.AutoSize = True
        Me.LabelKuotaPaket.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelKuotaPaket.Location = New System.Drawing.Point(323, 169)
        Me.LabelKuotaPaket.Name = "LabelKuotaPaket"
        Me.LabelKuotaPaket.Size = New System.Drawing.Size(82, 17)
        Me.LabelKuotaPaket.TabIndex = 536
        Me.LabelKuotaPaket.Text = "Kuota Paket"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(28, 261)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 17)
        Me.Label1.TabIndex = 535
        Me.Label1.Text = "Harga Awal"
        '
        'TextBoxHargaAwal
        '
        Me.TextBoxHargaAwal.BackColor = System.Drawing.Color.White
        Me.TextBoxHargaAwal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxHargaAwal.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxHargaAwal.Location = New System.Drawing.Point(196, 261)
        Me.TextBoxHargaAwal.Name = "TextBoxHargaAwal"
        Me.TextBoxHargaAwal.Size = New System.Drawing.Size(129, 23)
        Me.TextBoxHargaAwal.TabIndex = 534
        '
        'TextBoxKeteranganPaket
        '
        Me.TextBoxKeteranganPaket.BackColor = System.Drawing.Color.White
        Me.TextBoxKeteranganPaket.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxKeteranganPaket.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxKeteranganPaket.Location = New System.Drawing.Point(159, 78)
        Me.TextBoxKeteranganPaket.Name = "TextBoxKeteranganPaket"
        Me.TextBoxKeteranganPaket.Size = New System.Drawing.Size(327, 23)
        Me.TextBoxKeteranganPaket.TabIndex = 532
        '
        'TextBoxNamaPaket
        '
        Me.TextBoxNamaPaket.BackColor = System.Drawing.Color.White
        Me.TextBoxNamaPaket.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxNamaPaket.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBoxNamaPaket.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxNamaPaket.Location = New System.Drawing.Point(159, 49)
        Me.TextBoxNamaPaket.Name = "TextBoxNamaPaket"
        Me.TextBoxNamaPaket.Size = New System.Drawing.Size(327, 23)
        Me.TextBoxNamaPaket.TabIndex = 531
        '
        'Label157
        '
        Me.Label157.AutoSize = True
        Me.Label157.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label157.Location = New System.Drawing.Point(28, 20)
        Me.Label157.Name = "Label157"
        Me.Label157.Size = New System.Drawing.Size(59, 17)
        Me.Label157.TabIndex = 528
        Me.Label157.Text = "Paket ID"
        '
        'ButtonFindPaket
        '
        Me.ButtonFindPaket.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonFindPaket.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonFindPaket.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonFindPaket.FlatAppearance.BorderSize = 0
        Me.ButtonFindPaket.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonFindPaket.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonFindPaket.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonFindPaket.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonFindPaket.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.ButtonFindPaket.Location = New System.Drawing.Point(159, 17)
        Me.ButtonFindPaket.Name = "ButtonFindPaket"
        Me.ButtonFindPaket.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonFindPaket.Size = New System.Drawing.Size(60, 25)
        Me.ButtonFindPaket.TabIndex = 527
        Me.ButtonFindPaket.Text = " Cari"
        Me.ButtonFindPaket.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonFindPaket.UseVisualStyleBackColor = False
        '
        'Label203
        '
        Me.Label203.AutoSize = True
        Me.Label203.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label203.Location = New System.Drawing.Point(28, 49)
        Me.Label203.Name = "Label203"
        Me.Label203.Size = New System.Drawing.Size(82, 17)
        Me.Label203.TabIndex = 525
        Me.Label203.Text = "Nama Paket"
        '
        'TextBoxReffID
        '
        Me.TextBoxReffID.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBoxReffID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxReffID.Cursor = System.Windows.Forms.Cursors.No
        Me.TextBoxReffID.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxReffID.Location = New System.Drawing.Point(225, 20)
        Me.TextBoxReffID.Name = "TextBoxReffID"
        Me.TextBoxReffID.ReadOnly = True
        Me.TextBoxReffID.Size = New System.Drawing.Size(261, 23)
        Me.TextBoxReffID.TabIndex = 526
        Me.TextBoxReffID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GridControlTindakan
        '
        Me.GridControlTindakan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridControlTindakan.EmbeddedNavigator.Name = ""
        Me.GridControlTindakan.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.GridControlTindakan.Location = New System.Drawing.Point(0, 0)
        Me.GridControlTindakan.MainView = Me.GridViewTindakan
        Me.GridControlTindakan.Name = "GridControlTindakan"
        Me.GridControlTindakan.Size = New System.Drawing.Size(757, 200)
        Me.GridControlTindakan.TabIndex = 186
        Me.GridControlTindakan.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewTindakan})
        '
        'GridViewTindakan
        '
        Me.GridViewTindakan.GridControl = Me.GridControlTindakan
        Me.GridViewTindakan.Name = "GridViewTindakan"
        Me.GridViewTindakan.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridViewTindakan.OptionsView.ShowGroupPanel = False
        Me.GridViewTindakan.OptionsView.ShowIndicator = False
        Me.GridViewTindakan.PaintStyleName = "Skin"
        '
        'TabPageMasterAlat
        '
        Me.TabPageMasterAlat.AutoScroll = True
        Me.TabPageMasterAlat.BackColor = System.Drawing.Color.White
        Me.TabPageMasterAlat.Controls.Add(Me.SplitContainer4)
        Me.TabPageMasterAlat.Controls.Add(Me.Panel9)
        Me.TabPageMasterAlat.Location = New System.Drawing.Point(4, 27)
        Me.TabPageMasterAlat.Name = "TabPageMasterAlat"
        Me.TabPageMasterAlat.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageMasterAlat.Size = New System.Drawing.Size(763, 634)
        Me.TabPageMasterAlat.TabIndex = 2
        Me.TabPageMasterAlat.Text = "Master Alat"
        Me.TabPageMasterAlat.UseVisualStyleBackColor = True
        '
        'SplitContainer4
        '
        Me.SplitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer4.IsSplitterFixed = True
        Me.SplitContainer4.Location = New System.Drawing.Point(3, 58)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.TabControl1)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.DataGridViewAlat)
        Me.SplitContainer4.Size = New System.Drawing.Size(757, 568)
        Me.SplitContainer4.SplitterDistance = 250
        Me.SplitContainer4.SplitterWidth = 1
        Me.SplitContainer4.TabIndex = 2
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(757, 250)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.White
        Me.TabPage1.Controls.Add(Me.TextBoxIdRuangan)
        Me.TabPage1.Controls.Add(Me.Label59)
        Me.TabPage1.Controls.Add(Me.TextBoxNamaRuangan)
        Me.TabPage1.Controls.Add(Me.ButtonGetAlatSarpras)
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Controls.Add(Me.Label40)
        Me.TabPage1.Controls.Add(Me.TextBoxKodeAlat)
        Me.TabPage1.Controls.Add(Me.CheckBoxAlatAktif)
        Me.TabPage1.Controls.Add(Me.TextBoxAlatId)
        Me.TabPage1.Controls.Add(Me.Label39)
        Me.TabPage1.Controls.Add(Me.ButtonCariAlat)
        Me.TabPage1.Controls.Add(Me.TextBoxNamaAlat)
        Me.TabPage1.Controls.Add(Me.Label43)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(749, 222)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Alat"
        '
        'TextBoxIdRuangan
        '
        Me.TextBoxIdRuangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBoxIdRuangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxIdRuangan.Cursor = System.Windows.Forms.Cursors.No
        Me.TextBoxIdRuangan.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxIdRuangan.Location = New System.Drawing.Point(364, 125)
        Me.TextBoxIdRuangan.Name = "TextBoxIdRuangan"
        Me.TextBoxIdRuangan.Size = New System.Drawing.Size(37, 23)
        Me.TextBoxIdRuangan.TabIndex = 638
        Me.TextBoxIdRuangan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label59.Location = New System.Drawing.Point(27, 131)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(102, 17)
        Me.Label59.TabIndex = 637
        Me.Label59.Text = "Nama Ruangan"
        '
        'TextBoxNamaRuangan
        '
        Me.TextBoxNamaRuangan.BackColor = System.Drawing.Color.White
        Me.TextBoxNamaRuangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxNamaRuangan.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBoxNamaRuangan.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxNamaRuangan.Location = New System.Drawing.Point(194, 125)
        Me.TextBoxNamaRuangan.Name = "TextBoxNamaRuangan"
        Me.TextBoxNamaRuangan.Size = New System.Drawing.Size(167, 23)
        Me.TextBoxNamaRuangan.TabIndex = 636
        '
        'ButtonGetAlatSarpras
        '
        Me.ButtonGetAlatSarpras.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonGetAlatSarpras.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonGetAlatSarpras.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonGetAlatSarpras.FlatAppearance.BorderSize = 0
        Me.ButtonGetAlatSarpras.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonGetAlatSarpras.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonGetAlatSarpras.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonGetAlatSarpras.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonGetAlatSarpras.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.ButtonGetAlatSarpras.Location = New System.Drawing.Point(260, 22)
        Me.ButtonGetAlatSarpras.Name = "ButtonGetAlatSarpras"
        Me.ButtonGetAlatSarpras.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonGetAlatSarpras.Size = New System.Drawing.Size(67, 25)
        Me.ButtonGetAlatSarpras.TabIndex = 635
        Me.ButtonGetAlatSarpras.Text = "Get Alat"
        Me.ButtonGetAlatSarpras.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonGetAlatSarpras.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.ButtonCetakRiwayat)
        Me.GroupBox3.Controls.Add(Me.Label46)
        Me.GroupBox3.Controls.Add(Me.Label45)
        Me.GroupBox3.Controls.Add(Me.Label44)
        Me.GroupBox3.Controls.Add(Me.dtAkhir)
        Me.GroupBox3.Controls.Add(Me.dtAwal)
        Me.GroupBox3.Location = New System.Drawing.Point(407, 53)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(301, 113)
        Me.GroupBox3.TabIndex = 634
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Riwayat Penggunaan Alat"
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
        Me.Label40.Location = New System.Drawing.Point(27, 56)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(70, 17)
        Me.Label40.TabIndex = 630
        Me.Label40.Text = "Kode Aset"
        '
        'TextBoxKodeAlat
        '
        Me.TextBoxKodeAlat.BackColor = System.Drawing.Color.White
        Me.TextBoxKodeAlat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxKodeAlat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBoxKodeAlat.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxKodeAlat.Location = New System.Drawing.Point(194, 56)
        Me.TextBoxKodeAlat.Name = "TextBoxKodeAlat"
        Me.TextBoxKodeAlat.Size = New System.Drawing.Size(207, 23)
        Me.TextBoxKodeAlat.TabIndex = 631
        '
        'CheckBoxAlatAktif
        '
        Me.CheckBoxAlatAktif.AutoSize = True
        Me.CheckBoxAlatAktif.Checked = True
        Me.CheckBoxAlatAktif.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxAlatAktif.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxAlatAktif.Location = New System.Drawing.Point(425, 25)
        Me.CheckBoxAlatAktif.Name = "CheckBoxAlatAktif"
        Me.CheckBoxAlatAktif.Size = New System.Drawing.Size(51, 19)
        Me.CheckBoxAlatAktif.TabIndex = 623
        Me.CheckBoxAlatAktif.Text = "Aktif"
        Me.CheckBoxAlatAktif.UseVisualStyleBackColor = True
        '
        'TextBoxAlatId
        '
        Me.TextBoxAlatId.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBoxAlatId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxAlatId.Cursor = System.Windows.Forms.Cursors.No
        Me.TextBoxAlatId.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxAlatId.Location = New System.Drawing.Point(335, 24)
        Me.TextBoxAlatId.Name = "TextBoxAlatId"
        Me.TextBoxAlatId.ReadOnly = True
        Me.TextBoxAlatId.Size = New System.Drawing.Size(84, 23)
        Me.TextBoxAlatId.TabIndex = 619
        Me.TextBoxAlatId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label39.Location = New System.Drawing.Point(27, 92)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(72, 17)
        Me.Label39.TabIndex = 618
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
        Me.ButtonCariAlat.Location = New System.Drawing.Point(194, 22)
        Me.ButtonCariAlat.Name = "ButtonCariAlat"
        Me.ButtonCariAlat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonCariAlat.Size = New System.Drawing.Size(60, 25)
        Me.ButtonCariAlat.TabIndex = 620
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
        Me.TextBoxNamaAlat.Location = New System.Drawing.Point(194, 92)
        Me.TextBoxNamaAlat.Name = "TextBoxNamaAlat"
        Me.TextBoxNamaAlat.Size = New System.Drawing.Size(207, 23)
        Me.TextBoxNamaAlat.TabIndex = 621
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label43.Location = New System.Drawing.Point(27, 25)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(49, 17)
        Me.Label43.TabIndex = 622
        Me.Label43.Text = "Alat ID"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.White
        Me.TabPage2.Controls.Add(Me.ButtonGetAlatCartridge)
        Me.TabPage2.Controls.Add(Me.Label49)
        Me.TabPage2.Controls.Add(Me.TextBox17)
        Me.TabPage2.Controls.Add(Me.Label48)
        Me.TabPage2.Controls.Add(Me.TextBoxHargaBeli)
        Me.TabPage2.Controls.Add(Me.NumericKuotaMinimumCartridge)
        Me.TabPage2.Controls.Add(Me.NumericKuotaAwalCartridge)
        Me.TabPage2.Controls.Add(Me.ButtonPreOrder)
        Me.TabPage2.Controls.Add(Me.Label38)
        Me.TabPage2.Controls.Add(Me.Label58)
        Me.TabPage2.Controls.Add(Me.ComboBoxNamaAlat)
        Me.TabPage2.Controls.Add(Me.Label50)
        Me.TabPage2.Controls.Add(Me.TextBoxKuotaTotalCartridge)
        Me.TabPage2.Controls.Add(Me.Label51)
        Me.TabPage2.Controls.Add(Me.TextBoxKodeCartridge)
        Me.TabPage2.Controls.Add(Me.Label52)
        Me.TabPage2.Controls.Add(Me.TextBoxKuotaSisaCartridge)
        Me.TabPage2.Controls.Add(Me.Label53)
        Me.TabPage2.Controls.Add(Me.TextBoxKuotaTerpakaiCartridge)
        Me.TabPage2.Controls.Add(Me.Label55)
        Me.TabPage2.Controls.Add(Me.CheckBoxCartridge)
        Me.TabPage2.Controls.Add(Me.TextBoxIdCartridge)
        Me.TabPage2.Controls.Add(Me.Label56)
        Me.TabPage2.Controls.Add(Me.ButtonCariCartridge)
        Me.TabPage2.Controls.Add(Me.TextBoxNamaCartridge)
        Me.TabPage2.Controls.Add(Me.Label57)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(749, 222)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Cardtridge"
        '
        'ButtonGetAlatCartridge
        '
        Me.ButtonGetAlatCartridge.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonGetAlatCartridge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonGetAlatCartridge.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonGetAlatCartridge.FlatAppearance.BorderSize = 0
        Me.ButtonGetAlatCartridge.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonGetAlatCartridge.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonGetAlatCartridge.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonGetAlatCartridge.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonGetAlatCartridge.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.ButtonGetAlatCartridge.Location = New System.Drawing.Point(260, 9)
        Me.ButtonGetAlatCartridge.Name = "ButtonGetAlatCartridge"
        Me.ButtonGetAlatCartridge.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonGetAlatCartridge.Size = New System.Drawing.Size(67, 25)
        Me.ButtonGetAlatCartridge.TabIndex = 663
        Me.ButtonGetAlatCartridge.Text = "Get Alat"
        Me.ButtonGetAlatCartridge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonGetAlatCartridge.UseVisualStyleBackColor = False
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label49.Location = New System.Drawing.Point(636, 45)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(53, 17)
        Me.Label49.TabIndex = 662
        Me.Label49.Text = "/ Shoot"
        '
        'TextBox17
        '
        Me.TextBox17.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBox17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox17.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox17.Location = New System.Drawing.Point(501, 45)
        Me.TextBox17.Name = "TextBox17"
        Me.TextBox17.Size = New System.Drawing.Size(35, 23)
        Me.TextBox17.TabIndex = 661
        Me.TextBox17.Text = "Rp"
        Me.TextBox17.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label48.Location = New System.Drawing.Point(425, 45)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(70, 17)
        Me.Label48.TabIndex = 660
        Me.Label48.Text = "Harga Beli"
        '
        'TextBoxHargaBeli
        '
        Me.TextBoxHargaBeli.BackColor = System.Drawing.Color.White
        Me.TextBoxHargaBeli.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxHargaBeli.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBoxHargaBeli.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxHargaBeli.Location = New System.Drawing.Point(539, 45)
        Me.TextBoxHargaBeli.Name = "TextBoxHargaBeli"
        Me.TextBoxHargaBeli.Size = New System.Drawing.Size(91, 23)
        Me.TextBoxHargaBeli.TabIndex = 659
        Me.TextBoxHargaBeli.Text = "0"
        Me.TextBoxHargaBeli.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'NumericKuotaMinimumCartridge
        '
        Me.NumericKuotaMinimumCartridge.BackColor = System.Drawing.Color.White
        Me.NumericKuotaMinimumCartridge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumericKuotaMinimumCartridge.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.NumericKuotaMinimumCartridge.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.NumericKuotaMinimumCartridge.Location = New System.Drawing.Point(194, 178)
        Me.NumericKuotaMinimumCartridge.MaxLength = 5000000
        Me.NumericKuotaMinimumCartridge.Name = "NumericKuotaMinimumCartridge"
        Me.NumericKuotaMinimumCartridge.Size = New System.Drawing.Size(93, 23)
        Me.NumericKuotaMinimumCartridge.TabIndex = 658
        '
        'NumericKuotaAwalCartridge
        '
        Me.NumericKuotaAwalCartridge.BackColor = System.Drawing.Color.White
        Me.NumericKuotaAwalCartridge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumericKuotaAwalCartridge.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.NumericKuotaAwalCartridge.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.NumericKuotaAwalCartridge.Location = New System.Drawing.Point(194, 144)
        Me.NumericKuotaAwalCartridge.MaxLength = 50000000
        Me.NumericKuotaAwalCartridge.Name = "NumericKuotaAwalCartridge"
        Me.NumericKuotaAwalCartridge.Size = New System.Drawing.Size(93, 23)
        Me.NumericKuotaAwalCartridge.TabIndex = 657
        '
        'ButtonPreOrder
        '
        Me.ButtonPreOrder.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonPreOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonPreOrder.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonPreOrder.FlatAppearance.BorderSize = 0
        Me.ButtonPreOrder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonPreOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonPreOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonPreOrder.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonPreOrder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.ButtonPreOrder.Image = CType(resources.GetObject("ButtonPreOrder.Image"), System.Drawing.Image)
        Me.ButtonPreOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonPreOrder.Location = New System.Drawing.Point(528, 175)
        Me.ButtonPreOrder.Name = "ButtonPreOrder"
        Me.ButtonPreOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonPreOrder.Size = New System.Drawing.Size(119, 25)
        Me.ButtonPreOrder.TabIndex = 656
        Me.ButtonPreOrder.Text = "Pre-Order"
        Me.ButtonPreOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonPreOrder.UseVisualStyleBackColor = False
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label38.Location = New System.Drawing.Point(27, 178)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(106, 17)
        Me.Label38.TabIndex = 655
        Me.Label38.Text = "Kuota Minimum"
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label58.Location = New System.Drawing.Point(27, 45)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(72, 17)
        Me.Label58.TabIndex = 653
        Me.Label58.Text = "Nama Alat"
        '
        'ComboBoxNamaAlat
        '
        Me.ComboBoxNamaAlat.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxNamaAlat.FormattingEnabled = True
        Me.ComboBoxNamaAlat.Location = New System.Drawing.Point(194, 45)
        Me.ComboBoxNamaAlat.Name = "ComboBoxNamaAlat"
        Me.ComboBoxNamaAlat.Size = New System.Drawing.Size(207, 25)
        Me.ComboBoxNamaAlat.TabIndex = 652
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label50.Location = New System.Drawing.Point(293, 144)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(77, 17)
        Me.Label50.TabIndex = 649
        Me.Label50.Text = "Kuota Total"
        '
        'TextBoxKuotaTotalCartridge
        '
        Me.TextBoxKuotaTotalCartridge.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBoxKuotaTotalCartridge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxKuotaTotalCartridge.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBoxKuotaTotalCartridge.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxKuotaTotalCartridge.Location = New System.Drawing.Point(383, 144)
        Me.TextBoxKuotaTotalCartridge.MaxLength = 50000000
        Me.TextBoxKuotaTotalCartridge.Name = "TextBoxKuotaTotalCartridge"
        Me.TextBoxKuotaTotalCartridge.ReadOnly = True
        Me.TextBoxKuotaTotalCartridge.Size = New System.Drawing.Size(77, 23)
        Me.TextBoxKuotaTotalCartridge.TabIndex = 650
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label51.Location = New System.Drawing.Point(27, 80)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(71, 17)
        Me.Label51.TabIndex = 647
        Me.Label51.Text = "Kode Item"
        '
        'TextBoxKodeCartridge
        '
        Me.TextBoxKodeCartridge.BackColor = System.Drawing.Color.White
        Me.TextBoxKodeCartridge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxKodeCartridge.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBoxKodeCartridge.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxKodeCartridge.Location = New System.Drawing.Point(194, 80)
        Me.TextBoxKodeCartridge.Name = "TextBoxKodeCartridge"
        Me.TextBoxKodeCartridge.Size = New System.Drawing.Size(207, 23)
        Me.TextBoxKodeCartridge.TabIndex = 648
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label52.Location = New System.Drawing.Point(293, 178)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(71, 17)
        Me.Label52.TabIndex = 644
        Me.Label52.Text = "Kuota Sisa"
        '
        'TextBoxKuotaSisaCartridge
        '
        Me.TextBoxKuotaSisaCartridge.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBoxKuotaSisaCartridge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxKuotaSisaCartridge.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBoxKuotaSisaCartridge.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxKuotaSisaCartridge.Location = New System.Drawing.Point(383, 178)
        Me.TextBoxKuotaSisaCartridge.MaxLength = 50000000
        Me.TextBoxKuotaSisaCartridge.Name = "TextBoxKuotaSisaCartridge"
        Me.TextBoxKuotaSisaCartridge.ReadOnly = True
        Me.TextBoxKuotaSisaCartridge.Size = New System.Drawing.Size(77, 23)
        Me.TextBoxKuotaSisaCartridge.TabIndex = 645
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label53.Location = New System.Drawing.Point(466, 144)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(98, 17)
        Me.Label53.TabIndex = 642
        Me.Label53.Text = "Kuota Terpakai"
        '
        'TextBoxKuotaTerpakaiCartridge
        '
        Me.TextBoxKuotaTerpakaiCartridge.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBoxKuotaTerpakaiCartridge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxKuotaTerpakaiCartridge.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBoxKuotaTerpakaiCartridge.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxKuotaTerpakaiCartridge.Location = New System.Drawing.Point(570, 144)
        Me.TextBoxKuotaTerpakaiCartridge.MaxLength = 50000000
        Me.TextBoxKuotaTerpakaiCartridge.Name = "TextBoxKuotaTerpakaiCartridge"
        Me.TextBoxKuotaTerpakaiCartridge.ReadOnly = True
        Me.TextBoxKuotaTerpakaiCartridge.Size = New System.Drawing.Size(77, 23)
        Me.TextBoxKuotaTerpakaiCartridge.TabIndex = 643
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label55.Location = New System.Drawing.Point(27, 144)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(77, 17)
        Me.Label55.TabIndex = 641
        Me.Label55.Text = "Kuota Awal"
        '
        'CheckBoxCartridge
        '
        Me.CheckBoxCartridge.AutoSize = True
        Me.CheckBoxCartridge.Checked = True
        Me.CheckBoxCartridge.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxCartridge.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxCartridge.Location = New System.Drawing.Point(425, 11)
        Me.CheckBoxCartridge.Name = "CheckBoxCartridge"
        Me.CheckBoxCartridge.Size = New System.Drawing.Size(51, 19)
        Me.CheckBoxCartridge.TabIndex = 640
        Me.CheckBoxCartridge.Text = "Aktif"
        Me.CheckBoxCartridge.UseVisualStyleBackColor = True
        '
        'TextBoxIdCartridge
        '
        Me.TextBoxIdCartridge.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TextBoxIdCartridge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxIdCartridge.Cursor = System.Windows.Forms.Cursors.No
        Me.TextBoxIdCartridge.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxIdCartridge.Location = New System.Drawing.Point(335, 10)
        Me.TextBoxIdCartridge.Name = "TextBoxIdCartridge"
        Me.TextBoxIdCartridge.ReadOnly = True
        Me.TextBoxIdCartridge.Size = New System.Drawing.Size(84, 23)
        Me.TextBoxIdCartridge.TabIndex = 636
        Me.TextBoxIdCartridge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label56.Location = New System.Drawing.Point(27, 113)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(104, 17)
        Me.Label56.TabIndex = 635
        Me.Label56.Text = "Nama Cartridge"
        '
        'ButtonCariCartridge
        '
        Me.ButtonCariCartridge.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonCariCartridge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonCariCartridge.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonCariCartridge.FlatAppearance.BorderSize = 0
        Me.ButtonCariCartridge.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonCariCartridge.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonCariCartridge.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonCariCartridge.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonCariCartridge.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.ButtonCariCartridge.Location = New System.Drawing.Point(194, 9)
        Me.ButtonCariCartridge.Name = "ButtonCariCartridge"
        Me.ButtonCariCartridge.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonCariCartridge.Size = New System.Drawing.Size(60, 25)
        Me.ButtonCariCartridge.TabIndex = 637
        Me.ButtonCariCartridge.Text = " Cari"
        Me.ButtonCariCartridge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonCariCartridge.UseVisualStyleBackColor = False
        '
        'TextBoxNamaCartridge
        '
        Me.TextBoxNamaCartridge.BackColor = System.Drawing.Color.White
        Me.TextBoxNamaCartridge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxNamaCartridge.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBoxNamaCartridge.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TextBoxNamaCartridge.Location = New System.Drawing.Point(194, 113)
        Me.TextBoxNamaCartridge.Name = "TextBoxNamaCartridge"
        Me.TextBoxNamaCartridge.Size = New System.Drawing.Size(207, 23)
        Me.TextBoxNamaCartridge.TabIndex = 638
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label57.Location = New System.Drawing.Point(27, 12)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(81, 17)
        Me.Label57.TabIndex = 639
        Me.Label57.Text = "Cartridge ID"
        '
        'DataGridViewAlat
        '
        Me.DataGridViewAlat.AllowUserToAddRows = False
        Me.DataGridViewAlat.AllowUserToDeleteRows = False
        Me.DataGridViewAlat.BackgroundColor = System.Drawing.Color.White
        Me.DataGridViewAlat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewAlat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewAlat.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewAlat.Name = "DataGridViewAlat"
        Me.DataGridViewAlat.ReadOnly = True
        Me.DataGridViewAlat.Size = New System.Drawing.Size(757, 317)
        Me.DataGridViewAlat.TabIndex = 0
        '
        'Panel9
        '
        Me.Panel9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel9.AutoSize = True
        Me.Panel9.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Panel9.Controls.Add(Me.ButtonSimpanAlat)
        Me.Panel9.Controls.Add(Me.ButtonClearAlat)
        Me.Panel9.Controls.Add(Me.Button4)
        Me.Panel9.Location = New System.Drawing.Point(2, 1)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(760, 53)
        Me.Panel9.TabIndex = 0
        '
        'ButtonSimpanAlat
        '
        Me.ButtonSimpanAlat.BackColor = System.Drawing.Color.White
        Me.ButtonSimpanAlat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonSimpanAlat.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonSimpanAlat.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonSimpanAlat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonSimpanAlat.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonSimpanAlat.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSimpanAlat.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonSimpanAlat.Image = CType(resources.GetObject("ButtonSimpanAlat.Image"), System.Drawing.Image)
        Me.ButtonSimpanAlat.Location = New System.Drawing.Point(8, 7)
        Me.ButtonSimpanAlat.Name = "ButtonSimpanAlat"
        Me.ButtonSimpanAlat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonSimpanAlat.Size = New System.Drawing.Size(175, 40)
        Me.ButtonSimpanAlat.TabIndex = 586
        Me.ButtonSimpanAlat.Text = " Simpan Alat"
        Me.ButtonSimpanAlat.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonSimpanAlat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonSimpanAlat.UseVisualStyleBackColor = False
        '
        'ButtonClearAlat
        '
        Me.ButtonClearAlat.BackColor = System.Drawing.Color.White
        Me.ButtonClearAlat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonClearAlat.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonClearAlat.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonClearAlat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonClearAlat.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonClearAlat.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonClearAlat.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ButtonClearAlat.Image = CType(resources.GetObject("ButtonClearAlat.Image"), System.Drawing.Image)
        Me.ButtonClearAlat.Location = New System.Drawing.Point(189, 7)
        Me.ButtonClearAlat.Name = "ButtonClearAlat"
        Me.ButtonClearAlat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonClearAlat.Size = New System.Drawing.Size(150, 40)
        Me.ButtonClearAlat.TabIndex = 587
        Me.ButtonClearAlat.Text = " Clear Data"
        Me.ButtonClearAlat.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonClearAlat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonClearAlat.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.White
        Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.Button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.Button4.Image = CType(resources.GetObject("Button4.Image"), System.Drawing.Image)
        Me.Button4.Location = New System.Drawing.Point(345, 7)
        Me.Button4.Name = "Button4"
        Me.Button4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Button4.Size = New System.Drawing.Size(150, 40)
        Me.Button4.TabIndex = 588
        Me.Button4.Text = " Export To CSV"
        Me.Button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button4.UseVisualStyleBackColor = False
        Me.Button4.Visible = False
        '
        'NavBarItem3
        '
        Me.NavBarItem3.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.NavBarItem3.Appearance.Options.UseFont = True
        Me.NavBarItem3.AppearanceHotTracked.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.NavBarItem3.AppearanceHotTracked.Options.UseFont = True
        Me.NavBarItem3.AppearancePressed.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.NavBarItem3.AppearancePressed.Options.UseFont = True
        Me.NavBarItem3.Caption = "MASTER PAKET TINDAKAN"
        Me.NavBarItem3.LargeImage = CType(resources.GetObject("NavBarItem3.LargeImage"), System.Drawing.Image)
        Me.NavBarItem3.Name = "NavBarItem3"
        Me.NavBarItem3.SmallImage = CType(resources.GetObject("NavBarItem3.SmallImage"), System.Drawing.Image)
        '
        'NavBarItem4
        '
        Me.NavBarItem4.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.NavBarItem4.Appearance.Options.UseFont = True
        Me.NavBarItem4.AppearanceHotTracked.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.NavBarItem4.AppearanceHotTracked.Options.UseFont = True
        Me.NavBarItem4.AppearancePressed.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.NavBarItem4.AppearancePressed.Options.UseFont = True
        Me.NavBarItem4.Caption = "MASTER PAKET TINDAKAN"
        Me.NavBarItem4.LargeImage = CType(resources.GetObject("NavBarItem4.LargeImage"), System.Drawing.Image)
        Me.NavBarItem4.Name = "NavBarItem4"
        Me.NavBarItem4.SmallImage = CType(resources.GetObject("NavBarItem4.SmallImage"), System.Drawing.Image)
        '
        'MasterTindakanKlinik
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1000, 700)
        Me.ControlBox = False
        Me.Controls.Add(Me.PanelMasterTindakanKlinik)
        Me.Controls.Add(Me.PanelTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MasterTindakanKlinik"
        Me.PanelTop.ResumeLayout(False)
        Me.PanelMasterTindakanKlinik.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.NavBarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControlMenu.ResumeLayout(False)
        Me.TabPageMasterTindakan.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        Me.PanelMasterTindakan.ResumeLayout(False)
        Me.PanelMasterTindakan.PerformLayout()
        CType(Me.DataGridViewAlatTindakan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.NumericShootPerTindakan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.SplitContainerFeeTindakan.Panel1.ResumeLayout(False)
        Me.SplitContainerFeeTindakan.Panel1.PerformLayout()
        Me.SplitContainerFeeTindakan.Panel2.ResumeLayout(False)
        Me.SplitContainerFeeTindakan.Panel2.PerformLayout()
        Me.SplitContainerFeeTindakan.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabPageMasterPaket.ResumeLayout(False)
        Me.SplitContainerPendaftaran.Panel1.ResumeLayout(False)
        Me.SplitContainerPendaftaran.Panel2.ResumeLayout(False)
        Me.SplitContainerPendaftaran.ResumeLayout(False)
        Me.FlowLayoutPanel4.ResumeLayout(False)
        Me.SplitContainerMasterPaket.Panel1.ResumeLayout(False)
        Me.SplitContainerMasterPaket.Panel1.PerformLayout()
        Me.SplitContainerMasterPaket.Panel2.ResumeLayout(False)
        Me.SplitContainerMasterPaket.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        CType(Me.TextBoxDurasiAktifPaket, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBoxKuotaPaket, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControlTindakan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewTindakan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageMasterAlat.ResumeLayout(False)
        Me.TabPageMasterAlat.PerformLayout()
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.DataGridViewAlat, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel9.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelTop As System.Windows.Forms.Panel
    Friend WithEvents ButtonClose As System.Windows.Forms.Button
    Friend WithEvents PanelMasterTindakanKlinik As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents NavBarControl1 As DevExpress.XtraNavBar.NavBarControl
    Friend WithEvents NavBarGroup1 As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents NavBarItemTindakanPoliUmum As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents NavBarItem2 As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents TabControlMenu As System.Windows.Forms.TabControl
    Friend WithEvents TabPageMasterPaket As System.Windows.Forms.TabPage
    Friend WithEvents TabPageMasterTindakan As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainerPendaftaran As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainerMasterPaket As System.Windows.Forms.SplitContainer
    Friend WithEvents Label157 As System.Windows.Forms.Label
    Friend WithEvents ButtonFindPaket As System.Windows.Forms.Button
    Friend WithEvents Label203 As System.Windows.Forms.Label
    Friend WithEvents TextBoxReffID As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LabelKuotaPaket As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxHargaAwal As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxKeteranganPaket As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxNamaPaket As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TglAkhirPaket As System.Windows.Forms.DateTimePicker
    Friend WithEvents TglAwalPaket As System.Windows.Forms.DateTimePicker
    Friend WithEvents ButtonKalkulasiPaket As System.Windows.Forms.Button
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ButtonDetailTindakanPaket As System.Windows.Forms.Button
    Friend WithEvents TextBoxKuotaPaket As System.Windows.Forms.NumericUpDown
    Friend WithEvents TextBoxDurasiAktifPaket As System.Windows.Forms.NumericUpDown
    Friend WithEvents TextBox12 As System.Windows.Forms.TextBox
    Friend WithEvents ButtonHapusDetailTindakan As System.Windows.Forms.Button
    Friend WithEvents NavBarItemMasterPaketTindakan As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTotalDiskon As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBoxHargaNett As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents FlowLayoutPanel4 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Private WithEvents GridControlTindakan As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewTindakan As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents RadioButtonPaketReguler As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonPaketPerTindakan As System.Windows.Forms.RadioButton
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents TextBox15 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxMaksimalBHP_Paket As System.Windows.Forms.TextBox
    Friend WithEvents ButtonSimpanMasterTindakan As System.Windows.Forms.Button
    Friend WithEvents ButtonClearMasterTindakan As System.Windows.Forms.Button
    Friend WithEvents ButtonExportMasterTindakan As System.Windows.Forms.Button
    Friend WithEvents ButtonSimpanMasterPaket As System.Windows.Forms.Button
    Friend WithEvents ButtonClearMasterPaket As System.Windows.Forms.Button
    Friend WithEvents ButtonExportMasterPaket As System.Windows.Forms.Button
    Friend WithEvents CheckedListBoxPoli2 As System.Windows.Forms.CheckedListBox
    Friend WithEvents ComboBoxICPaket As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBoxICPaket As System.Windows.Forms.CheckBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents NavBarItemMasterAlat As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents NavBarItem3 As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents NavBarItem4 As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents TabPageMasterAlat As System.Windows.Forms.TabPage
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents ButtonSimpanAlat As System.Windows.Forms.Button
    Friend WithEvents ButtonClearAlat As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridViewAlat As System.Windows.Forms.DataGridView
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
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
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents TextBoxKuotaTotalCartridge As System.Windows.Forms.TextBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents TextBoxKodeCartridge As System.Windows.Forms.TextBox
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents TextBoxKuotaSisaCartridge As System.Windows.Forms.TextBox
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents TextBoxKuotaTerpakaiCartridge As System.Windows.Forms.TextBox
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxCartridge As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxIdCartridge As System.Windows.Forms.TextBox
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents ButtonCariCartridge As System.Windows.Forms.Button
    Friend WithEvents TextBoxNamaCartridge As System.Windows.Forms.TextBox
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxNamaAlat As System.Windows.Forms.ComboBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents ButtonPreOrder As System.Windows.Forms.Button
    Friend WithEvents NumericKuotaMinimumCartridge As System.Windows.Forms.TextBox
    Friend WithEvents NumericKuotaAwalCartridge As System.Windows.Forms.TextBox
    Friend WithEvents TextBox17 As System.Windows.Forms.TextBox
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents TextBoxHargaBeli As System.Windows.Forms.TextBox
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents ButtonGetAlatSarpras As System.Windows.Forms.Button
    Friend WithEvents ButtonGetAlatCartridge As System.Windows.Forms.Button
    Friend WithEvents TextBoxIdRuangan As System.Windows.Forms.TextBox
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNamaRuangan As System.Windows.Forms.TextBox
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents PanelMasterTindakan As System.Windows.Forms.Panel
    Friend WithEvents DataGridViewAlatTindakan As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonTambahAlat As System.Windows.Forms.Button
    Friend WithEvents ComboBoxAlatTindakan As System.Windows.Forms.ComboBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxCartridgeTindakan As System.Windows.Forms.ComboBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents NumericShootPerTindakan As System.Windows.Forms.NumericUpDown
    Friend WithEvents CheckBoxPakaiAlat As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBoxKategori As System.Windows.Forms.ComboBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents CheckedListBoxPoli As System.Windows.Forms.CheckedListBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents TextBoxHariReminder As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxReminder As System.Windows.Forms.CheckBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents RadioButtonPelaksanaDokter As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonPelaksanaPerawat As System.Windows.Forms.RadioButton
    Friend WithEvents CheckBoxStatusAktifTindakan As System.Windows.Forms.CheckBox
    Friend WithEvents SplitContainerFeeTindakan As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents RadioButtonAktifFee As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonNonAktifFee As System.Windows.Forms.RadioButton
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TextBoxPersentaseFeePerawat As System.Windows.Forms.TextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents TextBox10 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox11 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox13 As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton4 As System.Windows.Forms.RadioButton
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox8 As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents TextBoxPersentaseFeeKaryawan As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxPersentaseFeePerusahaan As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents TextBox14 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxFeePerawatTambahan As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents TextBox9 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxFeeDokterTambahan As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxNominalFee As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents RadioButtonOpsiPersentase As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonOpsiNominal As System.Windows.Forms.RadioButton
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxInformedConsent As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxInformedConsent As System.Windows.Forms.CheckBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents RadioButtonBHP As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonNonBHP As System.Windows.Forms.RadioButton
    Friend WithEvents TextBoxTindakanID As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents ButtonFindTindakan As System.Windows.Forms.Button
    Friend WithEvents TextBoxNamaTindakan As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents TextBoxBiayaTindakan As System.Windows.Forms.TextBox
    Friend WithEvents ComboBoxGroupJenisTindakan As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadioButtonPoliUmum As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonPoliGigi As System.Windows.Forms.RadioButton
    Friend WithEvents TextBox7 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxMaksimalBHP_Tindakan As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
