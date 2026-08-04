Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Drawing

Imports Excel = Microsoft.Office.Interop.Excel
Imports Microsoft.Office
Imports Microsoft.Office.Interop
Imports System.Text

Imports Newtonsoft.Json
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class ReportPendapatanKlinik
    ' Set Variabel Private
    Private formName As String = "LAPORAN PENDAPATAN KLINIK"
    Private tipeJurnal As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'tipe_jurnal'")
    Private kodeLokasiKlinik As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'NoPoli'")
    Private autoJurnal As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'AutoJurnal'")
    Private userInput As String = PoliKlinik.Lbluser.Text

    ' Label kategori paket. Literalnya SENGAJA sama persis dengan FormUploadDataOmset supaya
    ' laporan ini bisa ditaruh berdampingan dengan omset yang dikirim tanpa menerjemahkan istilah.
    Private Const KATEGORI_BELI_PAKET As String = "PEMBELIAN PAKET"
    Private Const KATEGORI_PAKAI_PAKET As String = "PEMAKAIAN PAKET"

    Private DB_Keuangan As TLcon
    Private DB_Omset As TLcon
    Private DB_Jurnal As TLcon
    Private Find_Poliklinik As Find.TLconnect
    Private Find_Omset As Find.TLconnect
    Private Find_Keuangan As Find.TLconnect

    ' Deklarasi variabel untuk menyimpan link terakhir yang diklik
    Private lastClickedItem As DevExpress.XtraNavBar.NavBarItem = Nothing

    Private Sub PanelTop_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PanelTop.Paint
        Try
            e.Graphics.DrawString(formName, New Font("Arial", 12, FontStyle.Bold), Brushes.White, New Point(10, 10))
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SetConnectionFromConfig()
        ' Set Parameter Server Online
        Dim serverOnline_IP As String = Utility.GetConfig("ServerOnline_IP")
        Dim serverOnline_UserID As String = Utility.GetConfig("ServerOnline_UserID")
        Dim serverOnline_Password As String = KASLibrary.Utility.DecryptTripleDES(Utility.GetConfig("ServerOnline_Password"), "albert")

        ' Set Find Keuangan 1 (mitramed_keu)
        Find_Keuangan.Tip = serverOnline_IP
        Find_Keuangan.Tid = serverOnline_UserID
        Find_Keuangan.Tpass = serverOnline_Password
        Find_Keuangan.Tdb = Utility.GetConfig("ServerOnlineKeuangan_DB")

        ' Set DB Keuangan 1 (mitramed_keu)
        DB_Keuangan.Tip = serverOnline_IP
        DB_Keuangan.Tid = serverOnline_UserID
        DB_Keuangan.Tpass = serverOnline_Password
        DB_Keuangan.Tdb = Utility.GetConfig("ServerOnlineKeuangan_DB")

        ' Set Find Poliklinik
        Find_Poliklinik.Tip = serverOnline_IP
        Find_Poliklinik.Tid = serverOnline_UserID
        Find_Poliklinik.Tpass = serverOnline_Password
        Find_Poliklinik.Tdb = Utility.GetConfig("ServerOnlinePoli_DB")

        ' Set Find Omset (mitramedicare_omset_keuangan)
        Find_Omset.Tip = serverOnline_IP
        Find_Omset.Tid = serverOnline_UserID
        Find_Omset.Tpass = serverOnline_Password
        Find_Omset.Tdb = Utility.GetConfig("ServerOnlineOmset_DB")

        ' Set DB Omset (mitramedicare_omset_keuangan)
        DB_Omset.Tip = serverOnline_IP
        DB_Omset.Tid = serverOnline_UserID
        DB_Omset.Tpass = serverOnline_Password
        DB_Omset.Tdb = Utility.GetConfig("ServerOnlineOmset_DB")

        ' Set DB Jurnal
        DB_Jurnal.Tip = Utility.GetConfig("ServerJurnal_IP")
        DB_Jurnal.Tid = Utility.GetConfig("ServerJurnal_UserID")
        DB_Jurnal.Tpass = KASLibrary.Utility.DecryptTripleDES(Utility.GetConfig("ServerJurnal_Password"), "albert")
        DB_Jurnal.Tdb = Utility.GetConfig("ServerJurnal_DB")
    End Sub

    ' ==========================================================================================
    ' PENGIRIMAN JURNAL — SALINAN dari FormUploadDataOmset (BuildMySqlConnString / BuatJurnalUMH /
    ' CariNomorJurnal). Kalau perilakunya diubah di sana, ubah di sini juga.
    '
    ' Kenapa tidak memakai WriteThemysql seperti sebelumnya: `F_MakeUMH` adalah FUNCTION yang
    ' melaporkan kegagalan dengan MENGEMBALIKAN STRING ('Periode Clossed' saat periode akuntansi
    ' sudah ditutup, 'DuplicateNumbe1' saat referensi kembar) — bukan dengan melempar error.
    ' WriteThemysql menjalankannya lewat ExecuteNonQuery dan menelan hasil maupun errornya, sehingga
    ' jurnal yang TIDAK terbentuk tetap dianggap sukses: flag dan LogJurnalKeuangan ikut ditulis dan
    ' jurnalnya hilang permanen tanpa jejak. Sudah dibuktikan live di server jurnal untuk jalur omset.
    ' ==========================================================================================
    Private Function BuildMySqlConnString(ByVal conn As TLcon) As String
        Return "datasource=" & conn.Tip & ";username=" & conn.Tid & ";password=" & conn.Tpass & ";database=" & conn.Tdb
    End Function

    ' Melempar bila server jurnal TIDAK mengembalikan nomor UMH. Nilai kembaliannya = nomor jurnal.
    Private Function BuatJurnalUMH(ByVal sql As String, ByVal conn As TLcon) As String
        Dim hasil As String = ""

        Using mysqlConn As New MySqlConnection(BuildMySqlConnString(conn))
            mysqlConn.Open()
            Using cmd As New MySqlCommand(sql, mysqlConn)
                cmd.CommandTimeout = 120
                Dim nilai As Object = cmd.ExecuteScalar()
                If nilai IsNot Nothing AndAlso Not Convert.IsDBNull(nilai) Then hasil = nilai.ToString().Trim()
            End Using
        End Using

        If hasil.StartsWith("UMH-") = False Then
            Throw New Exception("Server jurnal menolak, jurnal TIDAK dibuat. Jawaban server: '" & hasil & "'")
        End If

        Return hasil
    End Function

    ' Baca satu nilai dari MySQL yang MELEMPAR saat koneksi/kueri gagal, dan mengembalikan "" saat
    ' datanya memang tidak ada. ReadThemysqlNIK tidak bisa dipakai untuk keputusan penting: ia
    ' mengembalikan "-" untuk KEDUA keadaan itu, padahal di penomoran invoice bedanya menentukan —
    ' "gagal koneksi" yang dikira "belum ada invoice bulan ini" membuat penomoran mengulang dari 0001.
    Private Function BacaSatuNilaiMySqlChecked(ByVal sql As String, ByVal conn As TLcon) As String
        Using mysqlConn As New MySqlConnection(BuildMySqlConnString(conn) & ";Connection Timeout=15")
            mysqlConn.Open()
            Using cmd As New MySqlCommand(sql, mysqlConn)
                cmd.CommandTimeout = 60
                Dim nilai As Object = cmd.ExecuteScalar()
                If nilai Is Nothing OrElse Convert.IsDBNull(nilai) Then Return ""
                Return nilai.ToString().Trim()
            End Using
        End Using
    End Function

    ' Gerbang koneksi sebelum rangkaian tulis. SALINAN dari FormUploadDataOmset.
    '
    ' Bukan sekadar "internet nyala": yang diuji adalah membuka koneksi ke server tujuan yang
    ' sesungguhnya. `CheckInternetConnection` di DataConnect hanya ping ICMP ke google — lolos walau
    ' server MySQL mati, dan gagal palsu kalau firewall memblokir ICMP.
    Private Function CekKoneksiServer(ByVal conn As TLcon) As Boolean
        Try
            Using mysqlConn As New MySqlConnection(BuildMySqlConnString(conn) & ";Connection Timeout=8")
                mysqlConn.Open()
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ' DB_Omset tidak ikut diuji terpisah: SetConnectionFromConfig menaruhnya di host yang sama dengan
    ' DB_Keuangan (ServerOnline_IP), jadi sudah terwakili.
    Private Function CekKoneksiServerKirim() As Boolean
        Dim serverBermasalah As New StringBuilder()

        If CekKoneksiServer(DB_Keuangan) = False Then
            serverBermasalah.AppendLine("- Server Keuangan/Omset (" & DB_Keuangan.Tip & ")")
        End If

        If CekKoneksiServer(DB_Jurnal) = False Then
            serverBermasalah.AppendLine("- Server Jurnal (" & DB_Jurnal.Tip & ")")
        End If

        If serverBermasalah.Length > 0 Then
            MsgBox("Proses dibatalkan: server tujuan berikut tidak dapat dihubungi." & vbCrLf & vbCrLf & _
                   serverBermasalah.ToString() & vbCrLf & _
                   "Periksa koneksi internet/VPN dan pastikan server aktif, lalu coba lagi." & vbCrLf & _
                   "Belum ada data yang diubah.", _
                   MsgBoxStyle.Critical, "Koneksi Server Gagal")
            Return False
        End If

        Return True
    End Function

    ' Dipakai saat pembuatan jurnal melempar: error di sisi klien TIDAK membuktikan server tidak
    ' memproses (koneksi bisa putus sesudah server menyimpan). Kalau nomornya ternyata ada, jurnalnya
    ' memang sudah jadi dan tidak boleh dibuat ulang.
    Private Function CariNomorJurnal(ByVal refferenceID As String) As String
        Using mysqlConn As New MySqlConnection(BuildMySqlConnString(DB_Jurnal) & ";Connection Timeout=15")
            mysqlConn.Open()
            Using cmd As New MySqlCommand("SELECT umh FROM umh WHERE oms = '" & refferenceID & "' LIMIT 1", mysqlConn)
                cmd.CommandTimeout = 60
                Dim nilai As Object = cmd.ExecuteScalar()
                If nilai Is Nothing OrElse Convert.IsDBNull(nilai) Then Return ""
                Return nilai.ToString().Trim()
            End Using
        End Using
    End Function

    ' Satu panggilan F_MakeUMH yang aman: melempar kalau jurnal benar-benar tidak terbentuk, dan
    ' memulihkan diri kalau koneksi putus padahal server sudah menyimpannya.
    Private Function KirimJurnalAman(ByVal sqlJurnal As String, ByVal refferenceID As String) As String
        Try
            Return BuatJurnalUMH(sqlJurnal, DB_Jurnal)
        Catch exBuat As Exception
            Dim nomorJurnal As String = ""
            Try
                nomorJurnal = CariNomorJurnal(refferenceID)
            Catch exCek As Exception
                Throw exBuat
            End Try

            If nomorJurnal = "" Then Throw exBuat

            Return nomorJurnal
        End Try
    End Function

    Private Sub ReportPendapatanKlinik_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Form ini sendiri tidak punya worker thread, jadi baris ini sebenarnya tidak diperlukan di
        ' sini. TETAP DIBIARKAN: nilainya statis milik seluruh aplikasi dan 62 form lain menyetelnya
        ' juga, sehingga menghapusnya di sini tidak mengembalikan penjagaan apa pun — hanya menambah
        ' satu perbedaan tanpa manfaat. Membersihkannya harus serentak, di luar cakupan file ini.
        Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        Me.MdiParent = PoliKlinik
        Me.Dock = DockStyle.Fill

        ' Hide tab header button TabControl
        TabControlMenu.ItemSize = New Size(0, 1)

        ' Set Clear Form
        SetClearLaporanPendapatan()
        SetClearRekapTagihan()
        SetClearRekapInvoice()

        ' Set Connection Database
        SetConnectionFromConfig()

        ' Panggil metode untuk NavBarItemTindakanPoliUmum
        TriggerNavBarItemClick(NavBarItemLaporanPendapatan)
    End Sub

    ' Akses event LinkClicked dari NavBarItem secara manual
    Public Sub TriggerNavBarItemClick(ByVal navBarItem As DevExpress.XtraNavBar.NavBarItem)
        ' Buat event args dengan item NavBar yang diinginkan
        Dim link As New DevExpress.XtraNavBar.NavBarItemLink(navBarItem)
        Dim eventArgs As New DevExpress.XtraNavBar.NavBarLinkEventArgs(link)

        ' Panggil event handler NavBarItem_LinkClicked dengan argumen yang sesuai
        NavBarItem_LinkClicked(navBarItem, eventArgs)
    End Sub

    Private Sub ButtonClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClose.Click
        Me.Close()
    End Sub

    Private Sub NavBarItem_LinkClicked(ByVal sender As System.Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavBarItemLaporanPendapatan.LinkClicked, NavBarItemRekapTagihan.LinkClicked, NavBarItemRekapInvoice.LinkClicked
        ' Reset gaya dari item sebelumnya jika ada
        If lastClickedItem IsNot Nothing Then lastClickedItem.Appearance.ForeColor = Color.Empty

        ' Simpan item yang baru diklik dan setel warnanya
        lastClickedItem = e.Link.Item
        lastClickedItem.Appearance.ForeColor = Color.Gray

        ' Dapatkan caption dari link yang diklik
        Dim clickedLink As String = e.Link.Item.Caption

        ' Lakukan navigasi berdasarkan link yang diklik
        Select Case clickedLink
            Case "LAPORAN PENDAPATAN"
                TabControlMenu.SelectedTab = TabPageLaporanPendapatan
            Case "REKAP TAGIHAN"
                TabControlMenu.SelectedTab = TabPageRekapTagihan
            Case "REKAP INVOICE"
                TabControlMenu.SelectedTab = TabPageRekapInvoice
            Case Else
                MessageBox.Show("Link tidak dikenali!")
        End Select
    End Sub

    Private Sub SetClearLaporanPendapatan()
        ' Set ComboBox Poli Tujuan
        ComboBoxPoliTujuanLaporanPendapatan.Items.Clear()
        ComboBoxPoliTujuanLaporanPendapatan.Items.Add("SEMUA TRANSAKSI")
        Dim tblsPoliTujuan As DataTable = ReadSQL("SELECT * FROM MasterPoliTujuan WHERE PoliSakit = 'True'")
        For Each TrowPoliTujuan As DataRow In tblsPoliTujuan.Rows
            ComboBoxPoliTujuanLaporanPendapatan.Items.Add(TrowPoliTujuan("NamaPoli"))
        Next
        ComboBoxPoliTujuanLaporanPendapatan.SelectedIndex = 0

        ' Set Grid Control
        SetGridLaporanPendapatan()

        ' Clear Variabel
        TextBoxNamaPerusahaanLaporanPendapatan.ResetText()
        TextBoxNamaPerusahaanRekapTagihan.ResetText()

        ' Enable Control
        TglAwalTransaksiLaporanPendapatan.Enabled = True
        TglAkhirTransaksiLaporanPendapatan.Enabled = True
        ComboBoxPoliTujuanLaporanPendapatan.Enabled = True
        ButtonFindPerusahaanLaporanPendapatan.Enabled = True
        ButtonViewDataPendapatan.Enabled = True
        CheckBoxTagihan.Enabled = True
        CheckBoxTunai.Enabled = True
        CheckBoxCard.Enabled = True
        CheckBoxDebet.Enabled = True
        CheckBoxTransaksiNolRupiah.Enabled = True
        ' Kedua mode laporan ini di-disable saat data tampil, jadi Clear harus mengembalikannya
        ' (CheckBoxVoucher sebelumnya tidak ikut di-enable lagi sehingga mati permanen setelah dipakai).
        CheckBoxVoucher.Enabled = True
        CheckBoxPaket.Enabled = True
    End Sub

    Private Sub SetGridLaporanPendapatan()
        Dim tblOmset As DataTable = CreateTableLaporanPendapatan()

        GridControlLaporanPendapatan.DataSource = tblOmset

        GridViewLaporanPendapatan.Columns("No").Visible = False
        GridViewLaporanPendapatan.Columns("NoJenisBayar").Visible = False
        GridViewLaporanPendapatan.Columns("NIP").Visible = False
        GridViewLaporanPendapatan.Columns("Tgl Lahir").Visible = False
        GridViewLaporanPendapatan.Columns("Alamat").Visible = False
        GridViewLaporanPendapatan.Columns("Translabid").Visible = False
        GridViewLaporanPendapatan.Columns("mesin").Visible = False
        GridViewLaporanPendapatan.Columns("kartu").Visible = False
        GridViewLaporanPendapatan.Columns("JenisTransaksi").Visible = False
        GridViewLaporanPendapatan.Columns("Jenis Bayar").Visible = False

        ' Buang pengelompokan sisa mode Paket (lihat ViewDataLaporanPendapatanPaket)
        GridViewLaporanPendapatan.Columns("Detail Pembayaran").GroupIndex = -1

        ' Berlaku untuk mode biasa DAN mode Paket — keduanya memakai CreateTableLaporanPendapatan
        ' sehingga GridColumn-nya dipakai ulang saat DataSource diganti. Mode Voucher punya struktur
        ' kolom sendiri (lihat GetDataLaporanPendapatanVoucher) jadi ia memanggil sendiri.
        ' "pemeriksaan" dipilih sebagai tempat keterangan: kolom teks terlebar yang tampil DAN persis
        ' di sebelah kiri "Tagihan", jadi keterangan dan angkanya terbaca sebagai satu kalimat.
        PasangTotalLaporanPendapatan("pemeriksaan", "Tagihan")
    End Sub

    ' Sub total (footer tiap grup) + grand total (footer paling bawah) untuk grid Laporan Pendapatan.
    '
    ' Sub total hanya punya arti kalau gridnya DIKELOMPOKKAN: mode Paket sudah otomatis dikelompokkan
    ' per kategori (lihat ViewDataLaporanPendapatanPaket), dan untuk mode lain panel grup dinyalakan
    ' supaya petugas bisa menarik kolom "Jenis Poli"/"Detail Pembayaran" ke sana sesuai kebutuhan —
    ' pengelompokan dipaksa dari kode akan mengunci satu bentuk laporan saja.
    '
    ' Angka sub total ditaruh di FOOTER GRUP (argumen ke-3 Add), bukan menempel di baris grup, supaya
    ' sejajar lurus dengan angka barisnya dan dengan grand total di bawah.
    '
    ' Dipanggil ulang setiap kali DataSource berganti STRUKTUR: kalau nama kolomnya berbeda
    ' (mode Voucher), DevExpress membuang GridColumn lama dan membuat yang baru sehingga SummaryItem
    ' yang sudah dipasang ikut hilang.
    '
    ' `kolomKeterangan` adalah kolom TEKS tempat tulisan "SUB TOTAL"/"GRAND TOTAL" ditaruh — angkanya
    ' tetap di kolom nominalnya masing-masing, jadi satu baris footer terbaca menyamping:
    '     GRAND TOTAL (128 baris)   ...   Rp 47,850,000
    ' Keterangan JANGAN ditempel ke kolom nominal: teksnya lebih panjang daripada lebar kolom dan yang
    ' terpotong justru ANGKANYA — persis keluhan yang dulu muncul di footer Rekap Tagihan.
    Private Sub PasangTotalLaporanPendapatan(ByVal kolomKeterangan As String, ByVal ParamArray namaKolom() As String)
        GridViewLaporanPendapatan.OptionsView.ShowFooter = True
        GridViewLaporanPendapatan.OptionsView.ShowGroupPanel = True
        ' Di XtraGrid 3.2.3 properti ini ada LANGSUNG di GridView, bukan di OptionsView seperti
        ' versi DevExpress yang lebih baru — jangan "dirapikan" ke OptionsView, tidak akan kompilasi.
        GridViewLaporanPendapatan.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        GridViewLaporanPendapatan.GroupPanelText = "Tarik judul kolom ke sini untuk mengelompokkan — tiap kelompok dapat sub total"

        ' Dibersihkan dulu: sub ini dipanggil lagi tiap Clear Data, tanpa Clear item lama menumpuk
        ' sehingga satu kolom menampilkan angka yang sama berkali-kali.
        GridViewLaporanPendapatan.GroupSummary.Clear()

        For Each nama As String In namaKolom
            Dim kolom As DevExpress.XtraGrid.Columns.GridColumn = GridViewLaporanPendapatan.Columns(nama)
            If kolom Is Nothing Then Continue For

            SetKolomNominal(kolom)

            With kolom.SummaryItem
                .FieldName = nama
                .SummaryType = DevExpress.Data.SummaryItemType.Sum
                .DisplayFormat = "Rp {0:N0}"
            End With

            GridViewLaporanPendapatan.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, nama, kolom, "Rp {0:N0}")
        Next

        ' Keterangannya sendiri. SummaryType Count, bukan None, karena DisplayFormat baru dipakai kalau
        ' ada nilai yang dihitung — sekalian jumlah barisnya ikut terbaca. Sengaja "baris", bukan "trx":
        ' di mode Paket satu baris adalah satu TINDAKAN, bukan satu transaksi.
        Dim kolomLabel As DevExpress.XtraGrid.Columns.GridColumn = GridViewLaporanPendapatan.Columns(kolomKeterangan)
        If kolomLabel Is Nothing Then Exit Sub

        With kolomLabel.SummaryItem
            .FieldName = kolomKeterangan
            .SummaryType = DevExpress.Data.SummaryItemType.Count
            .DisplayFormat = "GRAND TOTAL ({0} baris)"
        End With

        GridViewLaporanPendapatan.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, kolomKeterangan, kolomLabel, "SUB TOTAL ({0} baris)")
    End Sub

    Function CreateTableLaporanPendapatan() As DataTable
        Dim Thasil As New DataTable

        insertRowToTbls(Thasil, "tyinterger", "No")
        insertRowToTbls(Thasil, "Typstring", "NoJenisBayar")
        insertRowToTbls(Thasil, "Typstring", "No Transaksi")
        insertRowToTbls(Thasil, "typdate", "Tgl Transaksi")
        insertRowToTbls(Thasil, "Typstring", "NIP")
        insertRowToTbls(Thasil, "Typstring", "Nama")
        insertRowToTbls(Thasil, "typdate", "Tgl Lahir")
        insertRowToTbls(Thasil, "Typstring", "Alamat") '
        insertRowToTbls(Thasil, "Typstring", "pemeriksaan")
        insertRowToTbls(Thasil, "tyinterger", "Tagihan")
        insertRowToTbls(Thasil, "tyinterger", "Translabid")
        insertRowToTbls(Thasil, "Typstring", "mesin")
        insertRowToTbls(Thasil, "Typstring", "kartu")
        insertRowToTbls(Thasil, "Typstring", "JenisTransaksi")
        insertRowToTbls(Thasil, "Typstring", "Jenis Poli")
        insertRowToTbls(Thasil, "Typstring", "Jenis Bayar")
        insertRowToTbls(Thasil, "Typstring", "Detail Pembayaran")

        For Each Tcol As DataColumn In Thasil.Columns
            Tcol.ReadOnly = True
        Next

        Return Thasil
    End Function

    Private Sub SetClearRekapTagihan()
        ' Set ComboBox Poli Tujuan
        ComboBoxJenisPoliOrTransaksi.Items.Clear()
        If tipeJurnal = "Poli" Then
            Dim tblsJenisPoli As New DataTable
            tblsJenisPoli = ReadSQL("SELECT * FROM MasterPoliTujuan WHERE PoliSakit = 'True' AND JenisPoli NOT IN ('LABORATORIUM')")
            For Each rowPoli As DataRow In tblsJenisPoli.Rows
                ComboBoxJenisPoliOrTransaksi.Items.Add(rowPoli("NamaPoli"))
            Next
        ElseIf tipeJurnal = "Transaksi" Then
            ComboBoxJenisPoliOrTransaksi.Items.Add("POLI")
            ComboBoxJenisPoliOrTransaksi.Items.Add("GIGI")
        End If
        ComboBoxJenisPoliOrTransaksi.SelectedIndex = -1

        ' Set Grid Control
        SetGridRekapTagihan()

        ' Clear Variabel
        TextBoxNamaPerusahaanRekapTagihan.ResetText()

        ' Enable Control
        TglAwalTransaksiRekapTagihan.Enabled = True
        TglAkhirTransaksiRekapTagihan.Enabled = True
        ComboBoxJenisPoliOrTransaksi.Enabled = True
        ButtonFindPerusahaanRekapTagihan.Enabled = True
        TextBoxNamaPerusahaanRekapTagihan.Enabled = True
        ButtonViewDataRekapTagihan.Enabled = True

        ' Change Color Button
        ButtonFindPerusahaanRekapTagihan.BackColor = Color.FromArgb(0, 100, 200)

        ' Disable Control
        PanelCreateInvoice.Visible = False

        ' Set Control Penagihan
        CheckBoxSemuaTransaksiRekapTagihan.Checked = False
        TextBoxBankTransferCoa.ResetText()
        TextBoxBankTransferKeterangan.ResetText()
    End Sub

    Private Sub SetGridRekapTagihan()
        Dim tblOmset As DataTable = CreateTableRekapTagihan()

        GridControlRekapTagihan.DataSource = tblOmset

        GridViewRekapTagihan.Columns("No").Visible = False
        'GridViewRekapTagihan.Columns("ID").Visible = False
        GridViewRekapTagihan.Columns("NoJenisBayar").Visible = False
        GridViewRekapTagihan.Columns("Tgl Lahir").Visible = False
        GridViewRekapTagihan.Columns("Alamat").Visible = False
        GridViewRekapTagihan.Columns("Translabid").Visible = False
        GridViewRekapTagihan.Columns("mesin").Visible = False
        GridViewRekapTagihan.Columns("kartu").Visible = False
        GridViewRekapTagihan.Columns("JenisTransaksi").Visible = False
        GridViewRekapTagihan.Columns("Jenis Bayar").Visible = False

        ' Footer kalkulasi: petugas closing perlu melihat berapa yang akan ditagihkan SAMBIL
        ' mencentang, bukan setelah invoice terlanjur terbentuk — angka itulah yang dicocokkan dengan
        ' tagihan perusahaan. SummaryType Custom, bukan Sum biasa, karena yang boleh ikut hanya baris
        ' yang dicentang DAN belum CLOSED (lihat GridViewRekapTagihan_CustomSummaryCalculate) —
        ' persis himpunan yang nanti masuk invoice, bukan seluruh isi grid.
        '
        ' Ditaruh di sini, bukan di ViewDataRekapTagihan, karena DataSource yang baru memakai nama
        ' kolom yang sama sehingga GridColumn-nya dipakai ulang — sama seperti Visible di atas yang
        ' juga tetap bertahan sesudah data ditarik.
        GridViewRekapTagihan.OptionsView.ShowFooter = True

        ' Footer dibesarkan: angka inilah yang dicocokkan petugas dengan tagihan perusahaan, jadi harus
        ' terbaca sekilas dari jarak duduk normal. UseFont WAJIB di-set — tanpa itu DevExpress
        ' mengabaikan Font yang diberikan dan tetap memakai gaya bawaan skin. FooterPanelHeight tidak
        ' ikut menyesuaikan diri terhadap font, jadi tingginya ditentukan sendiri; kalau tidak,
        ' hurufnya terpotong di atas-bawah.
        GridViewRekapTagihan.Appearance.FooterPanel.Font = New Font("Tahoma", 11, FontStyle.Bold)
        GridViewRekapTagihan.Appearance.FooterPanel.Options.UseFont = True
        GridViewRekapTagihan.FooterPanelHeight = 30

        ' Keterangannya ditaruh di kolom "No Transaksi" yang lebar, dan kolom "Tagihan" cukup memuat
        ' angkanya saja — footer dibaca menyamping jadi satu kalimat:
        '     AKAN DITAGIH : 12 trx  ...  Rp 3,250,000
        ' Kalau keterangan itu ikut menempel di kolom Tagihan, teksnya lebih panjang daripada lebar
        ' kolom dan justru ANGKANYA yang terpotong — itu yang terjadi sebelum ini.
        With GridViewRekapTagihan.Columns("No Transaksi").SummaryItem
            .FieldName = "No Transaksi"
            .SummaryType = DevExpress.Data.SummaryItemType.Custom
            .DisplayFormat = "AKAN DITAGIH : {0} trx"
        End With

        With GridViewRekapTagihan.Columns("Tagihan").SummaryItem
            .FieldName = "Tagihan"
            .SummaryType = DevExpress.Data.SummaryItemType.Custom
            .DisplayFormat = "Rp {0:N0}"
        End With

        SetKolomNominal(GridViewRekapTagihan.Columns("Tagihan"))

        ' Lebih lebar daripada patokan umum di SetKolomNominal: khusus grid ini footernya memakai huruf
        ' 11pt tebal, jadi "Rp 17,500,000" butuh ruang lebih. Tanpa ini angkanya terpotong lagi —
        ' persis keluhan yang dulu muncul saat teks footer lebih panjang daripada kolomnya.
        GridViewRekapTagihan.Columns("Tagihan").Width = 175
        GridViewRekapTagihan.Columns("Tagihan").MinWidth = 175

        PasangEditorCentang()
    End Sub

    ' Editor centang yang dipasang SENDIRI ke kolom "Checked", supaya event perubahannya bisa dikait.
    '
    ' Kenapa perlu: di versi DevExpress ini `CellValueChanged` baru menyala ketika editor sel DITUTUP,
    ' yaitu saat kursor pindah baris. Jadi menghitung ulang footer dari sana selalu terlambat satu
    ' baris — mencentang 4 transaksi @100rb tetap menampilkan 300rb sampai kursor digeser. Event
    ' `EditValueChanged` milik editornya menyala tepat saat diklik, dan `PostEditor` mendorong nilainya
    ' ke sumber data sehingga CustomSummaryCalculate (yang membaca lewat GetRowCellValue) melihat
    ' nilai yang baru.
    '
    ' Dibuat SEKALI saja: SetGridRekapTagihan dipanggil ulang tiap Clear Data, dan menambah repository
    ' item baru tiap kali akan menumpuk item sekaligus menggandakan handler.
    Private editorCentangRekap As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit = Nothing

    Private Sub PasangEditorCentang()
        If editorCentangRekap Is Nothing Then
            editorCentangRekap = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
            GridControlRekapTagihan.RepositoryItems.Add(editorCentangRekap)
            AddHandler editorCentangRekap.EditValueChanged, AddressOf EditorCentangRekap_EditValueChanged
        End If

        GridViewRekapTagihan.Columns("Checked").ColumnEdit = editorCentangRekap
    End Sub

    Private Sub EditorCentangRekap_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If sedangPostCentang Then Exit Sub

        sedangPostCentang = True
        Try
            GridViewRekapTagihan.PostEditor()
            GridViewRekapTagihan.UpdateSummary()
        Finally
            sedangPostCentang = False
        End Try
    End Sub

    ' Tampilan kolom nominal, dipakai BERSAMA grid Laporan Pendapatan dan Rekap Tagihan supaya
    ' keduanya seragam:
    '   - dikelompokkan ribuan, 6000000 -> "Rp 6,000,000", karena angka mentah sulit dibaca sekilas
    '     dan petugas harus mencocokkannya dengan tagihan perusahaan;
    '   - rata kanan supaya digit satuan sejajar antar baris, jadi besar-kecilnya langsung terlihat;
    '   - lebar minimum dijaga supaya tidak terpotong saat ColumnAutoWidth memampatkan kolom
    '     ("Rp 17,500,000" nominal terpanjang yang wajar di sini).
    '
    ' Pemisah ribuannya KOMA, mengikuti Regional Setting en-US yang memang diwajibkan aplikasi ini
    ' saat start (PoliKlinik.vb) dan yang dipakai seluruh hasil cetak — jangan dipaksa jadi titik di
    ' layar saja, nanti layar dan cetakan tidak seragam.
    Private Sub SetKolomNominal(ByVal kolom As DevExpress.XtraGrid.Columns.GridColumn)
        kolom.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        kolom.DisplayFormat.FormatString = "Rp #,##0"
        kolom.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        kolom.Width = 130
        kolom.MinWidth = 130
    End Sub

    ' Baris yang benar-benar ikut ke invoice: dicentang DAN belum pernah ditutup. SATU tempat supaya
    ' footer, konfirmasi simpan, dan CreateInvoiceTagihan tidak mungkin berbeda pendapat — kalau
    ' berbeda, angka yang dilihat petugas bukan angka yang tersimpan.
    Private Function BarisIkutDitagih(ByVal nilaiChecked As Object, ByVal nilaiStatus As Object) As Boolean
        If nilaiChecked Is Nothing OrElse IsDBNull(nilaiChecked) Then Return False

        Return CBool(nilaiChecked) AndAlso Convert.ToString(nilaiStatus) <> "CLOSED"
    End Function

    Private Sub GridViewRekapTagihan_CustomSummaryCalculate(ByVal sender As System.Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles GridViewRekapTagihan.CustomSummaryCalculate
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Start Then
            e.TotalValue = 0
            Exit Sub
        End If

        If e.SummaryProcess <> DevExpress.Data.CustomSummaryProcess.Calculate Then Exit Sub

        If BarisIkutDitagih(GridViewRekapTagihan.GetRowCellValue(e.RowHandle, "Checked"), _
                            GridViewRekapTagihan.GetRowCellValue(e.RowHandle, "Status")) = False Then Exit Sub

        ' Dua summary memakai handler yang sama, dibedakan lewat FieldName yang di-set di
        ' SetGridRekapTagihan: kolom Tagihan menjumlah rupiah, yang lain mencacah baris.
        If e.Item.FieldName = "Tagihan" Then
            e.TotalValue = CDbl(e.TotalValue) + CDbl(GridViewRekapTagihan.GetRowCellValue(e.RowHandle, "Tagihan"))
        Else
            e.TotalValue = CDbl(e.TotalValue) + 1
        End If
    End Sub

    ' Penjaga masuk-ulang: PostEditor bisa memicu ulang event yang memanggilnya.
    Private sedangPostCentang As Boolean = False

    ' Jaring pengaman KEDUA, sesudah EditorCentangRekap_EditValueChanged.
    '
    ' Di versi DevExpress ini event ini baru menyala saat editor sel DITUTUP (kursor pindah baris),
    ' jadi ia terlalu lambat untuk menyegarkan footer — itu tugas EditValueChanged milik editornya.
    ' Yang tetap dikerjakan di sini: UpdateCurrentRow, supaya perubahan benar-benar mengendap di
    ' DataTable, dan hitung ulang sekali lagi untuk jalur perubahan yang tidak lewat editor.
    Private Sub GridViewRekapTagihan_CellValueChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridViewRekapTagihan.CellValueChanged
        If e.Column Is Nothing OrElse e.Column.FieldName <> "Checked" Then Exit Sub
        If sedangPostCentang Then Exit Sub

        sedangPostCentang = True
        Try
            GridViewRekapTagihan.PostEditor()
            GridViewRekapTagihan.UpdateCurrentRow()
            GridViewRekapTagihan.UpdateSummary()
        Finally
            sedangPostCentang = False
        End Try
    End Sub

    Function CreateTableRekapTagihan() As DataTable
        Dim Thasil As New DataTable

        insertRowToTbls(Thasil, "tyinterger", "No")
        insertRowToTbls(Thasil, "tyinterger", "ID")
        insertRowToTbls(Thasil, "Typstring", "NoJenisBayar")
        insertRowToTbls(Thasil, "Typstring", "No Transaksi")
        insertRowToTbls(Thasil, "typdate", "Tgl Transaksi")
        insertRowToTbls(Thasil, "Typstring", "Nama")
        insertRowToTbls(Thasil, "typdate", "Tgl Lahir")
        insertRowToTbls(Thasil, "Typstring", "Alamat") '
        insertRowToTbls(Thasil, "Typstring", "pemeriksaan")
        insertRowToTbls(Thasil, "tyinterger", "Tagihan")
        insertRowToTbls(Thasil, "tyinterger", "Translabid")
        insertRowToTbls(Thasil, "Typstring", "mesin")
        insertRowToTbls(Thasil, "Typstring", "kartu")
        insertRowToTbls(Thasil, "Typstring", "JenisTransaksi")
        insertRowToTbls(Thasil, "Typstring", "Jenis Poli")
        insertRowToTbls(Thasil, "Typstring", "Jenis Bayar")
        insertRowToTbls(Thasil, "Typstring", "Detail Pembayaran")
        insertRowToTbls(Thasil, "Typstring", "Jenis Kasir")

        insertRowToTbls(Thasil, "Typstring", "Status")
        insertRowToTbls(Thasil, "typchecked", "Checked")

        For Each Tcol As DataColumn In Thasil.Columns
            Tcol.ReadOnly = True
        Next

        Thasil.Columns("Checked").ReadOnly = False

        Return Thasil
    End Function

    Private Function GetDataLaporanPendapatanVoucher() As DataTable
        ' Ambil tanggal dari DateTimePicker
        Dim tglAwal As String = Format(TglAwalTransaksiLaporanPendapatan.Value, "yyyy-MM-dd")
        Dim tglAkhir As String = Format(TglAkhirTransaksiLaporanPendapatan.Value, "yyyy-MM-dd")

        ' Filter Poli
        Dim filterPoli As String = ""
        If ComboBoxPoliTujuanLaporanPendapatan.Text <> "SEMUA TRANSAKSI" Then
            filterPoli = " AND TD.JenisPoli = '" & ComboBoxPoliTujuanLaporanPendapatan.Text & "'"
        End If

        ' Query utama.
        ' Yang digerakkan adalah TransaksiPoliDetail: voucher promo (MasterVoucher) menempel per
        ' TINDAKAN, bukan per kunjungan. TransaksiDetail hanya dipakai untuk mengambil JenisPoli,
        ' lewat APPLY TOP 1 karena satu NoUrut bisa punya lebih dari satu baris di sana —
        ' dengan JOIN biasa satu tindakan akan tampil berkali-kali.
        ' Penghubung TransaksiDetail <-> TransaksiPoli adalah NoUrut (nomor transaksi), BUKAN
        ' TransaksiDetail.NoTransaksi (itu ID kunjungan/pendaftaran, bukan TransPoliID).
        Dim query As String = "SELECT " & _
                "TP.NoUrut AS [Nomor Transaksi], " & _
                "ISNULL(TD.JenisPoli, '') AS [Poli], " & _
                "CONVERT(DATE, TP.TglPoli) AS [Tanggal], " & _
                "MP.Nama, " & _
                "TPD.NamaTindakan AS [Pemeriksaan], " & _
                "MV.NamaVoucher, " & _
                "MV.Nilai AS [Nominal], " & _
                "TPD.KodeVoucher, " & _
                "TPD.NettBayar AS [Sisa Bayar] " & _
            "FROM TransaksiPoliDetail TPD " & _
                "INNER JOIN TransaksiPoli TP ON TP.TransPoliID = TPD.TransPoliID AND TP.Deleted = 0 " & _
                "INNER JOIN MasterVoucher MV ON MV.KodeVoucher = TPD.KodeVoucher " & _
                "LEFT JOIN MasterPasien MP ON MP.NoRekamMedis = TP.IDInternet " & _
                "OUTER APPLY (SELECT TOP 1 TD2.JenisPoli FROM TransaksiDetail TD2 WHERE TD2.NoUrut = TP.NoUrut) TD " & _
            "WHERE (CONVERT(DATE, TP.TglPoli) BETWEEN '" & tglAwal & "' AND '" & tglAkhir & "') " & _
                "AND TPD.DeletedBy IS NULL" & filterPoli & " " & _
            "ORDER BY TP.TglPoli, TP.NoUrut"

        Return ReadSQL(query)
    End Function

    Private Sub ViewDataLaporanPendapatanVoucher()
        Try
            Dim dt As DataTable = GetDataLaporanPendapatanVoucher()

            If dt.Rows.Count > 0 Then
                GridControlLaporanPendapatan.DataSource = dt

                ' HARUS sesudah DataSource: struktur kolom mode ini berbeda dari mode lain, jadi
                ' GridColumn-nya baru dibuat di sini — sekaligus menutup kolom nominal mode Voucher
                ' yang sebelumnya tidak pernah diformat.
                PasangTotalLaporanPendapatan("NamaVoucher", "Nominal", "Sisa Bayar")

                ' Disable control (samakan dengan versi normal)
                TglAwalTransaksiLaporanPendapatan.Enabled = False
                TglAkhirTransaksiLaporanPendapatan.Enabled = False
                ComboBoxPoliTujuanLaporanPendapatan.Enabled = False
                ButtonFindPerusahaanLaporanPendapatan.Enabled = False
                ButtonViewDataPendapatan.Enabled = False
                CheckBoxTagihan.Enabled = False
                CheckBoxTunai.Enabled = False
                CheckBoxCard.Enabled = False
                CheckBoxDebet.Enabled = False
                CheckBoxTransaksiNolRupiah.Enabled = False
                CheckBoxVoucher.Enabled = False

            Else
                MessageBox.Show("Tidak Ada Data Voucher.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error Voucher: " & ex.Message)
        End Try
    End Sub

    ' ==========================================================================================
    ' LAPORAN PAKET (checkbox "Paket")
    '
    ' Menjawab kebutuhan: melihat sisi paket dari pendapatan dengan istilah yang SAMA dengan
    ' pengiriman omset di FormUploadDataOmset, yaitu PEMBELIAN PAKET dan PEMAKAIAN PAKET.
    '
    ' Kenapa dibaca dari TransaksiPoliDetail / LogPenggunaanPaket, bukan dari tabel pembayaran
    ' seperti laporan biasa: paket hanya ada di tingkat DETAIL (TransaksiPoliDetail.PaketID).
    ' Satu pembayaran sering mencampur tindakan biasa dan pembelian paket sekaligus, sehingga
    ' di sisi omset nilainya harus dipecah proporsional. Untuk laporan, memakai detail langsung
    ' memberi nama paket dan nilai per barisnya apa adanya — jauh lebih berguna dibaca.
    ' KONSEKUENSI yang harus disadari: nilai di sini adalah yang DITAGIHKAN (NettBayar), sedangkan
    ' "PEMBELIAN PAKET" di omset adalah porsi dari uang yang BENAR-BENAR dibayar. Keduanya sama
    ' bila transaksi dibayar penuh, dan bisa berbeda bila ada pembayaran sebagian.
    '
    ' GIGI dan LAB tidak punya jalur paket, jadi tidak ada query untuk keduanya.
    ' ==========================================================================================
    Private Sub ButtonViewDataPendapatan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonViewDataPendapatan.Click
        Try
            ' Tanpa satu pun centang, `jenisBayar` di GetDataLaporanPendapatanPoliUmum/Gigi kosong
            ' sehingga query jadi "JenisBayar IN ()" — error sintaks SQL yang tampil ke petugas
            ' sebagai query mentah. Hadang di sini.
            If Not (CheckBoxPaket.Checked OrElse CheckBoxVoucher.Checked OrElse CheckBoxTagihan.Checked _
                    OrElse CheckBoxTunai.Checked OrElse CheckBoxDebet.Checked OrElse CheckBoxCard.Checked) Then
                MessageBox.Show("Pilih dulu data yang mau ditampilkan: centang minimal satu jenis pembayaran (Tagihan/Tunai/Debet/Card), atau centang Paket, atau centang Voucher.", _
                                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Exit Sub
            End If

            If CheckBoxPaket.Checked Then
                ViewDataLaporanPendapatanPaket()
            ElseIf CheckBoxVoucher.Checked Then
                ViewDataLaporanPendapatanVoucher()
            Else
                ViewDataLaporanPendapatan()
            End If

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ViewDataLaporanPendapatanPaket()
        ' Struktur tabelnya SAMA dengan laporan biasa supaya grid (kolom yang disembunyikan) dan
        ' kedua template cetak yang sudah ada tetap jalan tanpa diubah sama sekali.
        Dim rowTable As DataTable = CreateTableLaporanPendapatan()

        IsiBarisLaporanPaket(rowTable, GetDataLaporanPaketPembelian(), KATEGORI_BELI_PAKET)
        IsiBarisLaporanPaket(rowTable, GetDataLaporanPaketPemakaian(), KATEGORI_PAKAI_PAKET)

        If rowTable.Rows.Count = 0 Then
            MessageBox.Show("Tidak Ada Data Paket.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Exit Sub
        End If

        GridControlLaporanPendapatan.DataSource = rowTable

        ' Kelompokkan per kategori supaya pembelian dan pemakaian langsung terbaca terpisah,
        ' sama seperti tampilan grid di FormUploadDataOmset. Dikembalikan lagi oleh Clear Data
        ' (SetGridLaporanPendapatan), dan Clear Data memang satu-satunya jalan untuk tarik ulang.
        GridViewLaporanPendapatan.Columns("Detail Pembayaran").GroupIndex = 0
        GridViewLaporanPendapatan.ExpandAllGroups()

        ' Disable Control
        TglAwalTransaksiLaporanPendapatan.Enabled = False
        TglAkhirTransaksiLaporanPendapatan.Enabled = False
        ComboBoxPoliTujuanLaporanPendapatan.Enabled = False
        ButtonFindPerusahaanLaporanPendapatan.Enabled = False
        ButtonViewDataPendapatan.Enabled = False
        CheckBoxTagihan.Enabled = False
        CheckBoxTunai.Enabled = False
        CheckBoxCard.Enabled = False
        CheckBoxDebet.Enabled = False
        CheckBoxTransaksiNolRupiah.Enabled = False
        CheckBoxVoucher.Enabled = False
        CheckBoxPaket.Enabled = False
    End Sub

    Private Sub IsiBarisLaporanPaket(ByVal rowTable As DataTable, ByVal tblSumber As DataTable, ByVal kategori As String)
        ' Nomor urut dihitung ULANG per blok cetak, bukan memakai NomorUrut dari query. Versi1
        ' mengelompokkan per poli + keterangan dan TIDAK menomori ulang seperti Versi2, jadi nomor
        ' hasil ROW_NUMBER seluruh periode tampil berhamburan di dalam blok (14, 1, 7, 8, 11, ...).
        ' Kuncinya sama dengan yang dipakai mengelompokkan, dan urutan di dalam blok mengikuti
        ' ORDER BY query, jadi nomornya jadi 1..n rapi persis seperti blok laporan biasa.
        Dim nomorPerBlok As New Dictionary(Of String, Integer)

        For Each rowSumber As DataRow In tblSumber.Rows
            Dim kunciBlok As String = rowSumber("JenisPoli").ToString & "|" & rowSumber("JenisBayarPaket").ToString
            If nomorPerBlok.ContainsKey(kunciBlok) Then
                nomorPerBlok(kunciBlok) += 1
            Else
                nomorPerBlok(kunciBlok) = 1
            End If

            Dim newRow As DataRow = rowTable.NewRow
            newRow("No") = nomorPerBlok(kunciBlok)
            newRow("No Transaksi") = rowSumber("No Transaksi")
            newRow("Tgl Transaksi") = rowSumber("Tgl Transaksi")
            newRow("Nama") = rowSumber("Nama").ToString.ToUpper
            newRow("Tgl Lahir") = rowSumber("Tgl Lahir")
            newRow("pemeriksaan") = rowSumber("Pemeriksaan")
            newRow("Tagihan") = rowSumber("Nilai")
            newRow("Jenis Poli") = rowSumber("JenisPoli")

            ' "JenisBayarPaket" isinya tergantung sumbernya: jenis bayar untuk baris PEMBELIAN,
            ' penanda pendek "tidak dijurnal" untuk baris PEMAKAIAN (lihat kedua fungsi query).
            ' Kosong = pemakaian yang layak dijurnal, judulnya cukup kategori saja.
            '
            ' Ketiga kolom pembayaran diisi mengikuti pemakaian tiap kolom di template cetak, supaya
            ' tidak ada file .repx yang perlu disentuh (.repx hanya bisa didesain dari VS 2008):
            '   "Jenis Bayar"       -> Versi1 MENGELOMPOKKAN per kolom ini => tetap kategori saja,
            '                          kalau diisi jenis bayar, pembelian & pemakaian jadi tercampur.
            '   "NoJenisBayar"      -> Versi1 mengelompokkan + mencetaknya sebagai keterangan.
            '   "Detail Pembayaran" -> dicetak Versi2, sekaligus kolom grup di grid mode Paket
            '                          => kategori + keterangan, jadi grupnya sejalan dengan omset
            '                             yang juga memecah PEMBELIAN PAKET per jenis bayar, dan
            '                             pemakaian pra-cut-off terpisah sendiri dari yang dijurnal.
            Dim jenisBayarPaket As String = rowSumber("JenisBayarPaket").ToString.Trim

            newRow("Jenis Bayar") = kategori
            newRow("NoJenisBayar") = jenisBayarPaket
            If jenisBayarPaket.Length > 0 Then
                newRow("Detail Pembayaran") = kategori & " - " & jenisBayarPaket
            Else
                newRow("Detail Pembayaran") = kategori
            End If

            rowTable.Rows.Add(newRow)
        Next
    End Sub

    ' Keterangan SATU baris pembayaran, aturannya disamakan dengan ViewDataLaporanPendapatan supaya
    ' laporan paket bisa dicocokkan baris-per-baris dengan laporan pendapatan biasa dan dengan omset:
    '   DEBET / CARD -> "<jenis> <mesin>"       (omset juga dipecah per mesin)
    '   TAGIHAN      -> "<jenis> <perusahaan>"  (NoJenisBayar berbentuk "<NAMA>-<KODE>", diambil
    '                   potongan sebelum '-' pertama, sama seperti Split("-")(0) di laporan biasa)
    '   TUNAI        -> "<jenis>" saja
    ' NULLIF ganda membuang mesin sampah ('' dan '-'), dan CHARINDEX pada "NoJenisBayar + '-'"
    ' menjaga nama tetap utuh kalau kodenya tidak ada.
    ' Keterangan satu baris pembayaran untuk grid & cetak. Dulu blok yang sama disalin di TIGA tempat
    ' (laporan pendapatan, rekap tagihan, detail invoice) — kalau aturannya berubah di satu tempat,
    ' ketiga tampilan itu diam-diam jadi tidak sama.
    '
    ' KEMBARAN SQL-nya ada di SqlKeteranganBayar di bawah, yang dipakai laporan Paket karena di sana
    ' keterangannya harus dirakit di dalam query. Aturan keduanya WAJIB sama: mesin untuk DEBET/CARD,
    ' nama perusahaan (potongan sebelum '-' pertama) untuk TAGIHAN, dan kosong untuk TUNAI.
    '
    ' Convert.ToString dipakai supaya kolom yang DBNull menghasilkan "" — bukan melempar seperti
    ' penugasan langsung ke String pada versi lama.
    Private Function KeteranganJenisBayar(ByVal rowData As DataRow) As String
        Dim jenisBayar As String = Convert.ToString(rowData("JenisBayar"))

        If jenisBayar = "DEBET" OrElse jenisBayar = "CARD" Then
            Return Convert.ToString(rowData("JenisMesin"))
        ElseIf jenisBayar = "TAGIHAN" Then
            Return Convert.ToString(rowData("CompanyTagihan")).Split("-"c)(0).Trim().ToUpper()
        End If

        Return ""
    End Function

    ' Bentuk gabungannya: "DEBET BCA", "TAGIHAN IBU LUCIANA TANOYO", atau "TUNAI" saja.
    ' Beda tipis dari versi lama: dulu DEBET/CARD/TAGIHAN selalu ditempeli spasi walau keterangannya
    ' kosong sehingga menghasilkan "DEBET " berspasi ekor. Sekarang tidak.
    Private Function KeteranganDetailPembayaran(ByVal rowData As DataRow) As String
        Dim keterangan As String = KeteranganJenisBayar(rowData)

        If keterangan = "" Then Return Convert.ToString(rowData("JenisBayar"))

        Return Convert.ToString(rowData("JenisBayar")) & " " & keterangan
    End Function

    Private Function SqlKeteranganBayar(ByVal tbl As String) As String
        Return tbl & ".JenisBayar + CASE " & _
               "WHEN " & tbl & ".JenisBayar IN ('DEBET', 'CARD') THEN ISNULL(' ' + NULLIF(NULLIF(" & tbl & ".mesin, ''), '-'), '') " & _
               "WHEN " & tbl & ".JenisBayar = 'TAGIHAN' THEN ISNULL(' ' + NULLIF(UPPER(LTRIM(RTRIM(LEFT(ISNULL(" & tbl & ".NoJenisBayar, ''), CHARINDEX('-', ISNULL(" & tbl & ".NoJenisBayar, '') + '-') - 1)))), ''), '') " & _
               "ELSE '' END"
    End Function

    ' Baris PEMBELIAN paket: PaketID berisi GUID dan TindakanID = '0' (NamaTindakan = nama paket,
    ' NettBayar = harga paket). Pembeda ini wajib dua tingkat — baris pemakaian juga ber-PaketID
    ' GUID dan ber-NettBayar > 0, jadi NettBayar BUKAN pembeda.
    Private Function GetDataLaporanPaketPembelian() As DataTable
        Dim tglTransaksiAwal As Date = TglAwalTransaksiLaporanPendapatan.Value
        Dim tglTransaksiAkhir As Date = TglAkhirTransaksiLaporanPendapatan.Value

        Dim query As New StringBuilder()

        ' Tanpa ROW_NUMBER: nomor barisnya dihitung per blok cetak di IsiBarisLaporanPaket.
        query.AppendLine("SELECT t.NoUrut AS 'No Transaksi', CONVERT(DATE, t.TglPoli) AS 'Tgl Transaksi',")
        query.AppendLine("       ISNULL(p.Nama, '') AS 'Nama', CONVERT(DATE, p.TglLahir) AS 'Tgl Lahir',")
        query.AppendLine("       ISNULL(d.NamaTindakan, '') AS 'Pemeriksaan', ISNULL(dt.JenisPoli, '') AS 'JenisPoli',")
        query.AppendLine("       CAST(ROUND(ISNULL(d.NettBayar, 0), 0) AS bigint) AS 'Nilai',")
        ' Jenis bayar transaksi tempat paket ini DIBELI — tanpa ini baris paket tampil tanpa jenis
        ' bayar sama sekali (TransaksiPoliDetail memang tidak menyimpan pembayaran).
        ' Tabel pembayarannya ditentukan `d.Awal`: '1' -> TransaksiPoliBayarAwal, '0' ->
        ' TransaksiPoliBayar. Pasangan yang SAMA dipakai laporan pendapatan biasa, jadi jangan
        ' digabung tanpa menyaring Awal — satu transaksi bisa punya keduanya untuk tindakan berbeda
        ' sehingga paket akan tertulis dibayar dengan cara yang sebenarnya milik tindakan lain.
        ' DISTINCT karena satu jenis bayar bisa terpecah beberapa baris (mis. TAGIHAN dicicil), dan
        ' keterangannya dibentuk SqlKeteranganBayar supaya sama dengan laporan pendapatan biasa.
        ' Hanya keempat jenis bayar yang membawa UANG yang dihitung — sama seperti query omset. Baris
        ' 'VOUCHER' pada transaksi campur adalah pemakaian jatah paket LAIN, bukan alat bayar paket
        ' ini; kalau ikut, transaksi tanpa pembayaran tunai akan salah tertulis "dibayar VOUCHER".
        ' Hasil kosong = memang tidak ada pembayaran tunai/kartu/tagihan tercatat di transaksi itu.
        query.AppendLine("       ISNULL(STUFF((SELECT DISTINCT ', ' + x.Ket FROM (")
        query.AppendLine("           SELECT " & SqlKeteranganBayar("ba") & " AS Ket")
        query.AppendLine("           FROM TransaksiPoliBayarAwal ba")
        query.AppendLine("           WHERE ba.TransPoliID = d.TransPoliID AND ba.JumlahBayar > 0 AND d.Awal = '1'")
        query.AppendLine("             AND ba.JenisBayar IN ('TAGIHAN', 'TUNAI', 'DEBET', 'CARD')")
        query.AppendLine("           UNION ALL")
        query.AppendLine("           SELECT " & SqlKeteranganBayar("b"))
        query.AppendLine("           FROM TransaksiPoliBayar b")
        query.AppendLine("           WHERE b.TransPoliID = d.TransPoliID AND b.JumlahBayar > 0 AND d.Awal = '0'")
        query.AppendLine("             AND b.JenisBayar IN ('TAGIHAN', 'TUNAI', 'DEBET', 'CARD')")
        ' Kosong = tak satu pun pembayaran tunai/kartu/tagihan tercatat di transaksi itu (terukur: 14
        ' baris, semuanya lama). Diberi teks tegas, bukan dibiarkan kosong, karena kolom ini menjadi
        ' judul grup di hasil cetak Versi1.
        query.AppendLine("       ) x FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, ''), 'TANPA PEMBAYARAN TERCATAT') AS 'JenisBayarPaket'")
        query.AppendLine("FROM TransaksiPoliDetail d")
        query.AppendLine("INNER JOIN TransaksiPoli t ON t.TransPoliID = d.TransPoliID AND t.Deleted = 0")
        query.AppendLine("LEFT JOIN MasterPasien p ON p.NoRekamMedis = t.IDInternet")
        ' TransaksiDetail memang 1:1 per NoUrut (terukur 10.322 baris / 10.319 NoUrut; yang banyak baris
        ' adalah TransaksiPoliDetail, 2,26 per transaksi). APPLY TOP 1 dipakai sebagai pengaman saja
        ' terhadap baris kembar hasil double-save — di seluruh DB baru ada SATU (20250414U00011,
        ' dibuat berjarak 29 detik). JOIN biasa di query lain karena itu BUKAN kesalahan.
        query.AppendLine("OUTER APPLY (SELECT TOP 1 d2.JenisPoli FROM TransaksiDetail d2 WHERE d2.NoUrut = t.NoUrut) dt")
        query.AppendLine("WHERE d.DeletedDate IS NULL")
        query.AppendLine("  AND ISNULL(d.PaketID, '0') <> '0' AND ISNULL(d.TindakanID, '0') = '0'")
        query.AppendLine("  AND (CONVERT(DATE, t.TglPoli) BETWEEN '" & Format(tglTransaksiAwal.Date, "yyyyMMdd") & "' AND '" & Format(tglTransaksiAkhir.Date, "yyyyMMdd") & "')")

        If ComboBoxPoliTujuanLaporanPendapatan.Text <> "SEMUA TRANSAKSI" Then
            query.AppendLine("  AND dt.JenisPoli = '" & ComboBoxPoliTujuanLaporanPendapatan.Text & "'")
        End If

        If CheckBoxTransaksiNolRupiah.Checked = False Then
            query.AppendLine("  AND ISNULL(d.NettBayar, 0) > 0")
        End If

        query.AppendLine("ORDER BY t.TglPoli, t.NoUrut")

        Return ReadSQL(query.ToString())
    End Function

    ' Nilai satu tindakan yang ditarik dari paket. SALINAN dari FormUploadDataOmset
    ' (SqlNilaiPemakaianPaket) — kalau rumusnya berubah di sana, ubah di sini juga, kalau tidak
    ' laporan ini tidak akan cocok lagi dengan jurnal pengakuan pendapatan paket.
    '
    ' Nilainya SENGAJA TIDAK diambil dari baris bayar VOUCHER di TransaksiPoliBayarAwal: nilai memo
    ' di sana dihitung sebagai HargaNett/Kuota dari HEADER master, padahal untuk Paket Per-Tindakan
    ' kolom Kuota header selalu 0 sehingga pembaginya jatuh ke 1 dan angkanya membengkak sebesar
    ' satu paket penuh per tindakan.
    Private Function SqlNilaiPemakaianPaket() As String
        ' Paket Per-Tindakan : harga per tindakan dari SNAPSHOT saat paket dibeli.
        ' Paket Reguler      : harga paket dibagi kuota ASLI di master = harga per kunjungan
        '                      (bukan kuota sisa — kuota sisa menyusut dan menggelembungkan harga).
        Return "CASE WHEN mp.JenisPaket = 'Paket Per-Tindakan' THEN ISNULL(td.NettBayar, 0) " & _
               "     WHEN ISNULL(mp.Kuota, 0) > 0 THEN mp.HargaNett / mp.Kuota " & _
               "     ELSE 0 END"
    End Function

    ' Baris PEMAKAIAN paket, satu baris per tindakan yang ditarik dari paket.
    ' Sengaja TANPA saringan FlagJurnal maupun cut-off TglMulaiJurnalPaket: itu urusan penjurnalan,
    ' sedangkan laporan harus menampilkan seluruh pemakaian pada periode yang diminta. Yang TIDAK
    ' akan dijurnal diberi penanda pendek (lihat kolom JenisBayarPaket), kalau tidak total pemakaian
    ' di laporan ini selalu lebih besar daripada jurnal tanpa terlihat sebabnya — contoh 17 Juli 2026
    ' (cut-off 1 Juli): POLI HBOT laporan 3.707.273 vs jurnal 2.325.455, karena 6 dari 17 pemakaian
    ' berasal dari paket yang dibeli Feb/Apr dan pendapatannya sudah diakui saat pembelian.
    '
    ' Yang LAYAK dijurnal sengaja TIDAK diberi teks apa pun (permintaan user): kolom ini menempel di
    ' judul grup hasil cetak, dan memberi label pada kedua sisi membuat judulnya kepanjangan.
    Private Function GetDataLaporanPaketPemakaian() As DataTable
        Dim tglTransaksiAwal As Date = TglAwalTransaksiLaporanPendapatan.Value
        Dim tglTransaksiAkhir As Date = TglAkhirTransaksiLaporanPendapatan.Value

        ' Cut-off pengakuan pendapatan paket — patokannya TANGGAL PEMBELIAN paket, bukan tanggal
        ' pemakaian. Sumber & fail-safe-nya disamakan dengan TglCutOffPaket() di FormUploadDataOmset:
        ' key hilang/tidak sah => Date.MaxValue = fitur mati, sehingga SELURUH pemakaian bertanda
        ' pra cut-off — persis seperti yang akan dilakukan pengiriman jurnalnya.
        ' Date.TryParse menimpa variabelnya dengan Date.MinValue saat gagal, jadi wajib di-set ulang.
        Dim tglCutOffPaket As Date = Date.MaxValue
        If Date.TryParse(ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'TglMulaiJurnalPaket'"), tglCutOffPaket) = False Then
            tglCutOffPaket = Date.MaxValue
        End If

        Dim query As New StringBuilder()

        ' Tanpa ROW_NUMBER: nomor barisnya dihitung per blok cetak di IsiBarisLaporanPaket.
        query.AppendLine("SELECT l.NoTransaksi AS 'No Transaksi', CONVERT(DATE, t.TglPoli) AS 'Tgl Transaksi',")
        query.AppendLine("       ISNULL(p.Nama, '') AS 'Nama', CONVERT(DATE, p.TglLahir) AS 'Tgl Lahir',")
        ' Nama tindakan hanya ada untuk Paket Per-Tindakan. Pada Paket Reguler `l.TindakanID` = 0
        ' sehingga JOIN ke TransaksiPaketTindakanDetail tidak ketemu dan NamaTindakan NULL — bentuk
        ' "NamaTindakan [NamaPaket]" tanpa penjagaan mencetak " [PAKET SWD 10X]": kurung yatim
        ' berspasi di depan, dan itulah mayoritas baris (paket reguler jauh lebih banyak).
        query.AppendLine("       CASE WHEN ISNULL(td.NamaTindakan, '') = '' THEN ISNULL(mp.NamaPaket, '')")
        query.AppendLine("            ELSE td.NamaTindakan + ' [' + ISNULL(mp.NamaPaket, '') + ']' END AS 'Pemeriksaan',")
        query.AppendLine("       ISNULL(dt.JenisPoli, '') AS 'JenisPoli',")
        query.AppendLine("       CAST(ROUND(" & SqlNilaiPemakaianPaket() & ", 0) AS bigint) AS 'Nilai',")
        ' Pemakaian tidak dibayar dengan apa pun (ditarik dari jatah paket), jadi kolom yang di
        ' pembelian berisi jenis bayar dipakai ulang di sini sebagai PENANDA baris yang tidak akan
        ' masuk jurnal pengakuan pendapatan. Dengan begitu penandanya ikut sendiri ke tiga tempat yang
        ' sama seperti jenis bayar (grup grid, grup + label Versi1, kolom Versi2) tanpa plumbing baru.
        ' Teksnya dijaga PENDEK karena menempel di judul grup. Kosong = layak dijurnal.
        '
        ' Dua sebab tidak dijurnal, keduanya sama dengan saringan SqlSumberPemakaianPaket di
        ' FormUploadDataOmset:
        '   paket dibeli SEBELUM cut-off -> pendapatannya sudah diakui penuh saat pembelian (aturan
        '                                   lama), mengakui lagi = dobel.
        '   data pembelian tak ditemukan -> tak bisa dibuktikan pernah ditangguhkan. Nol baris di DB
        '                                   sekarang, tapi tanpa penanda ia akan menyamar sebagai
        '                                   baris yang dijurnal.
        query.AppendLine("       CASE WHEN beli.TglPoli IS NULL THEN 'TANPA DATA BELI'")
        query.AppendLine("            WHEN CONVERT(DATE, beli.TglPoli) < '" & Format(tglCutOffPaket, "yyyyMMdd") & "' THEN 'PRA CUT-OFF'")
        query.AppendLine("            ELSE '' END AS 'JenisBayarPaket'")
        query.AppendLine("FROM LogPenggunaanPaket l")
        ' CROSS APPLY: transaksi yang sudah dihapus ikut tersaring keluar, sama seperti laporan lain.
        query.AppendLine("CROSS APPLY (SELECT TOP 1 t2.TglPoli FROM TransaksiPoli t2 WHERE t2.NoUrut = l.NoTransaksi AND t2.Deleted = 0) t")
        query.AppendLine("OUTER APPLY (SELECT TOP 1 d2.JenisPoli FROM TransaksiDetail d2 WHERE d2.NoUrut = l.NoTransaksi) dt")
        ' Tanggal transaksi tempat paket ini DIBELI. SALINAN dari SqlSumberPemakaianPaket di
        ' FormUploadDataOmset — sengaja TglPoli transaksi pembeliannya, bukan CreatedDate paket,
        ' supaya penanda di laporan ini memakai patokan yang sama persis dengan jurnalnya.
        query.AppendLine("OUTER APPLY (SELECT TOP 1 tp.TglPoli FROM TransaksiPaketTindakan pk")
        query.AppendLine("             INNER JOIN TransaksiPoli tp ON tp.NoUrut = pk.NoTransaksi AND tp.Deleted = 0")
        query.AppendLine("             WHERE pk.KodeVoucher = l.KodeVoucher AND pk.PaketID = l.PaketID AND pk.DeletedDate IS NULL) beli")
        query.AppendLine("LEFT JOIN MasterPasien p ON p.NoRekamMedis = l.NoRekamMedis")
        query.AppendLine("LEFT JOIN MasterPaketTindakan mp ON mp.PaketID = l.PaketID")
        query.AppendLine("LEFT JOIN TransaksiPaketTindakanDetail td ON td.KodeVoucher = l.KodeVoucher AND td.PaketID = l.PaketID AND td.TindakanID = l.TindakanID")
        query.AppendLine("WHERE l.DeletedDate IS NULL")
        query.AppendLine("  AND (CONVERT(DATE, t.TglPoli) BETWEEN '" & Format(tglTransaksiAwal.Date, "yyyyMMdd") & "' AND '" & Format(tglTransaksiAkhir.Date, "yyyyMMdd") & "')")

        If ComboBoxPoliTujuanLaporanPendapatan.Text <> "SEMUA TRANSAKSI" Then
            query.AppendLine("  AND dt.JenisPoli = '" & ComboBoxPoliTujuanLaporanPendapatan.Text & "'")
        End If

        ' Nilai 0 = harga paket tak bisa ditentukan (master/snapshot hilang), bukan tindakan gratis.
        If CheckBoxTransaksiNolRupiah.Checked = False Then
            query.AppendLine("  AND " & SqlNilaiPemakaianPaket() & " > 0")
        End If

        query.AppendLine("ORDER BY t.TglPoli, l.NoTransaksi")

        Return ReadSQL(query.ToString())
    End Function

    Private Sub ViewDataLaporanPendapatan()
        Dim tblPoliUmum As DataTable = GetDataLaporanPendapatanPoliUmum()
        Dim tblPoliGigi As DataTable = GetDataLaporanPendapatanPoliGigi()

        ' Buat tabel gabungan dengan struktur yang sama
        Dim tblOmset As DataTable = tblPoliUmum.Clone()

        ' Gabungkan data dari kedua tabel
        tblOmset.Merge(tblPoliUmum)
        tblOmset.Merge(tblPoliGigi)

        If tblOmset.Rows.Count > 0 Then
            Dim rowTable As DataTable = CreateTableLaporanPendapatan()
            rowTable.Rows.Clear()

            For Each rowData As DataRow In tblOmset.Rows
                Dim newRow As DataRow = rowTable.NewRow
                newRow("No") = rowData("NomorUrutTransaksi")
                newRow("No Transaksi") = rowData("No Transaksi")
                newRow("Tgl Transaksi") = rowData("Tgl Transaksi")
                newRow("Nama") = rowData("Nama").ToString.ToUpper
                newRow("Tgl Lahir") = rowData("Tgl Lahir")
                newRow("pemeriksaan") = rowData("DataListTindakan")
                newRow("Tagihan") = rowData("TotalBayar")
                newRow("JenisTransaksi") = rowData("JenisTransaksi")
                newRow("Jenis Poli") = rowData("JenisPoli")
                newRow("Jenis Bayar") = rowData("JenisBayar")
                newRow("NoJenisBayar") = KeteranganJenisBayar(rowData)
                newRow("Detail Pembayaran") = KeteranganDetailPembayaran(rowData)
                rowTable.Rows.Add(newRow)
            Next

            GridControlLaporanPendapatan.DataSource = rowTable

            ' Disable Control
            TglAwalTransaksiLaporanPendapatan.Enabled = False
            TglAkhirTransaksiLaporanPendapatan.Enabled = False
            ComboBoxPoliTujuanLaporanPendapatan.Enabled = False
            ButtonFindPerusahaanLaporanPendapatan.Enabled = False
            ButtonViewDataPendapatan.Enabled = False
            CheckBoxTagihan.Enabled = False
            CheckBoxTunai.Enabled = False
            CheckBoxCard.Enabled = False
            CheckBoxDebet.Enabled = False
            CheckBoxTransaksiNolRupiah.Enabled = False
        Else
            MessageBox.Show("Tidak Ada Data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Function GetDataLaporanPendapatanPoliUmum() As DataTable
        ' Set Tanggal Transaksi
        Dim tglTransaksiAwal As Date = TglAwalTransaksiLaporanPendapatan.Value
        Dim tglTransaksiAkhir As Date = TglAkhirTransaksiLaporanPendapatan.Value

        ' Set Jenis Pembayaran
        Dim listJenisBayar As New List(Of String)
        If CheckBoxTagihan.Checked Then listJenisBayar.Add("'Tagihan'")
        If CheckBoxTunai.Checked Then listJenisBayar.Add("'Tunai'")
        If CheckBoxDebet.Checked Then listJenisBayar.Add("'Debet'")
        If CheckBoxCard.Checked Then listJenisBayar.Add("'Card'")
        Dim jenisBayar As String = String.Join(", ", listJenisBayar.ToArray())

        ' Set Jenis Poli
        Dim poliTujuan As String = ""
        If ComboBoxPoliTujuanLaporanPendapatan.Text <> "SEMUA TRANSAKSI" Then
            poliTujuan = "WHERE JenisPoli = '" & ComboBoxPoliTujuanLaporanPendapatan.Text & "'"
        End If

        ' Set Tabel Result
        Dim tblResult As New DataTable

        Dim queryBuilder As New StringBuilder()

        ' WITH clause for FilteredTransPoli
        queryBuilder.AppendLine("WITH FilteredTransPoli AS (")
        queryBuilder.AppendLine("    SELECT TransPoliID, RekapID")
        queryBuilder.AppendLine("    FROM TransaksiPoli")
        queryBuilder.AppendLine("    WHERE (CONVERT(DATE, TglPoli) BETWEEN '" & Format(tglTransaksiAwal.Date, "yyyyMMdd") & "' AND '" & Format(tglTransaksiAkhir.Date, "yyyyMMdd") & "') AND Deleted = 0")
        queryBuilder.AppendLine(")")

        ' Main SELECT statement
        queryBuilder.AppendLine("SELECT ROW_NUMBER() OVER (PARTITION BY tblDetail.JenisPoli, tblBayar.JenisBayar, tblBayar.CompanyTagihan, tblBayar.mesin ORDER BY tblPoli.TglPoli, tblPoli.NoUrut) AS NomorUrutTransaksi, tblBayar.TransPoliIDBayar AS 'ID',")
        queryBuilder.AppendLine("       tblPoli.NoUrut AS 'No Transaksi', CONVERT(DATE, tblPoli.TglPoli) AS 'Tgl Transaksi',")
        queryBuilder.AppendLine("       tblDetail.JenisPoli, tblBayar.DataListTindakan, tblPasien.Nama, CONVERT(DATE, tblPasien.TglLahir) AS 'Tgl Lahir', tblPasien.NoRekamMedis,")
        queryBuilder.AppendLine("       tblBayar.JenisBayar, tblBayar.CompanyTagihan, tblBayar.kartu AS 'JenisKartu',")
        queryBuilder.AppendLine("       tblBayar.mesin AS 'JenisMesin', tblDetail.JenisTransaksi,")
        queryBuilder.AppendLine("       tblBayar.JumlahBayar - tblBayar.Kembali AS TotalBayar, tblPoli.RekapID, tblBayar.NoInvoicePembayaran")
        queryBuilder.AppendLine("FROM (")
        queryBuilder.AppendLine("    SELECT tb.TransPoliIDBayar, tb.TransPoliID,")
        queryBuilder.AppendLine("           STUFF((SELECT ', ' + ISNULL(tblTindakan.NamaTindakan, '')")
        queryBuilder.AppendLine("                  FROM TransaksiPoliDetail tblTindakan")
        queryBuilder.AppendLine("                  WHERE tblTindakan.Awal = '0' AND tblTindakan.TransPoliID = tb.TransPoliID AND tblTindakan.DeletedBy IS NULL")
        queryBuilder.AppendLine("                  FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS DataListTindakan,")
        queryBuilder.AppendLine("           tb.JenisBayar, CASE WHEN tb.JenisBayar = 'TAGIHAN' THEN tb.NoJenisBayar ELSE '' END AS 'CompanyTagihan', tb.JumlahBayar, tb.Kembali, tb.kartu, tb.mesin, tb.NoInvoicePembayaran")
        queryBuilder.AppendLine("    FROM TransaksiPoliBayar tb")
        queryBuilder.AppendLine("         INNER JOIN FilteredTransPoli ft ON tb.TransPoliID = ft.TransPoliID")
        queryBuilder.AppendLine("    WHERE tb.JenisBayar IN (" & jenisBayar & ")")

        If CheckBoxTransaksiNolRupiah.Checked = False Then
            queryBuilder.AppendLine("    AND tb.JumlahBayar > 0")
        End If

        If CheckBoxTagihan.Checked = True And TextBoxNamaPerusahaanLaporanPendapatan.Text.Length > 0 Then
            queryBuilder.AppendLine("    AND tb.NoJenisBayar = '" & TextBoxNamaPerusahaanLaporanPendapatan.Text & "'")
        End If

        queryBuilder.AppendLine("    UNION ALL")
        queryBuilder.AppendLine("    SELECT tba.TransPoliIDBayar, tba.TransPoliID,")
        queryBuilder.AppendLine("           STUFF((SELECT ', ' + ISNULL(tblTindakan.NamaTindakan, '')")
        queryBuilder.AppendLine("                  FROM TransaksiPoliDetail tblTindakan")
        queryBuilder.AppendLine("                  WHERE tblTindakan.Awal = '1' AND tblTindakan.TransPoliID = tba.TransPoliID AND tblTindakan.DeletedBy IS NULL")
        queryBuilder.AppendLine("                  FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS DataListTindakan,")
        queryBuilder.AppendLine("           tba.JenisBayar, CASE WHEN tba.JenisBayar = 'TAGIHAN' THEN tba.NoJenisBayar ELSE '' END AS 'CompanyTagihan', tba.JumlahBayar, tba.Kembali, tba.kartu, tba.mesin, tba.NoInvoicePembayaran")
        queryBuilder.AppendLine("    FROM TransaksiPoliBayarAwal tba")
        queryBuilder.AppendLine("         INNER JOIN FilteredTransPoli ft ON tba.TransPoliID = ft.TransPoliID")
        queryBuilder.AppendLine("    WHERE tba.JenisBayar IN (" & jenisBayar & ")")

        If CheckBoxTransaksiNolRupiah.Checked = False Then
            queryBuilder.AppendLine("    AND tba.JumlahBayar > 0")
        End If

        If CheckBoxTagihan.Checked = True And TextBoxNamaPerusahaanLaporanPendapatan.Text.Length > 0 Then
            queryBuilder.AppendLine("    AND tba.NoJenisBayar = '" & TextBoxNamaPerusahaanLaporanPendapatan.Text & "'")
        End If

        queryBuilder.AppendLine(") AS tblBayar")
        queryBuilder.AppendLine("LEFT JOIN TransaksiPoli tblPoli ON tblBayar.TransPoliID = tblPoli.TransPoliID")
        queryBuilder.AppendLine("LEFT JOIN TransaksiDetail tblDetail ON tblPoli.NoUrut = tblDetail.NoUrut")
        queryBuilder.AppendLine("LEFT JOIN MasterPasien tblPasien ON tblPoli.IDInternet = tblPasien.NoRekamMedis")
        If ComboBoxPoliTujuanLaporanPendapatan.Text <> "SEMUA TRANSAKSI" Then
            queryBuilder.AppendLine("WHERE JenisPoli = '" & ComboBoxPoliTujuanLaporanPendapatan.Text & "'")
        End If
        queryBuilder.AppendLine("ORDER BY tblBayar.JenisBayar, tblDetail.JenisPoli")

        tblResult = ReadSQL(queryBuilder.ToString())

        Return tblResult
    End Function

    Function GetDataLaporanPendapatanPoliGigi() As DataTable
        ' Set Tanggal Transaksi
        Dim tglTransaksiAwal As Date = TglAwalTransaksiLaporanPendapatan.Value
        Dim tglTransaksiAkhir As Date = TglAkhirTransaksiLaporanPendapatan.Value

        ' Set Jenis Pembayaran
        Dim listJenisBayar As New List(Of String)
        If CheckBoxTagihan.Checked Then listJenisBayar.Add("'Tagihan'")
        If CheckBoxTunai.Checked Then listJenisBayar.Add("'Tunai'")
        If CheckBoxDebet.Checked Then listJenisBayar.Add("'Debet'")
        If CheckBoxCard.Checked Then listJenisBayar.Add("'Card'")
        Dim jenisBayar As String = String.Join(", ", listJenisBayar.ToArray())

        ' Set Jenis Poli
        Dim poliTujuan As String = ""
        If ComboBoxPoliTujuanLaporanPendapatan.Text <> "SEMUA TRANSAKSI" Then
            poliTujuan = "WHERE JenisPoli = '" & ComboBoxPoliTujuanLaporanPendapatan.Text & "'"
        End If

        ' Set Tabel Result
        Dim tblResult As New DataTable

        Dim queryBuilder As New StringBuilder()

        ' WITH clause for FilteredTransPoli
        queryBuilder.AppendLine("WITH FilteredTransGigi AS (")
        queryBuilder.AppendLine("    SELECT TransGigiID, RekapID")
        queryBuilder.AppendLine("    FROM TransaksiGigi")
        queryBuilder.AppendLine("    WHERE (CONVERT(DATE, TglGigi) BETWEEN '" & Format(tglTransaksiAwal.Date, "yyyyMMdd") & "' AND '" & Format(tglTransaksiAkhir.Date, "yyyyMMdd") & "') AND Deleted = 0")
        queryBuilder.AppendLine(")")

        ' Main SELECT statement
        queryBuilder.AppendLine("SELECT ROW_NUMBER() OVER (PARTITION BY tblDetail.JenisPoli, tblBayar.JenisBayar, tblBayar.CompanyTagihan, tblBayar.mesin ORDER BY tblGigi.TglGigi, tblGigi.NoUrut) AS NomorUrutTransaksi, tblBayar.TransGigiIDBayar AS 'ID',")
        queryBuilder.AppendLine("       tblGigi.NoUrut AS 'No Transaksi', CONVERT(DATE, tblGigi.TglGigi) AS 'Tgl Transaksi',")
        queryBuilder.AppendLine("       tblDetail.JenisPoli, tblBayar.DataListTindakan, tblPasien.Nama, CONVERT(DATE, tblPasien.TglLahir) AS 'Tgl Lahir', tblPasien.NoRekamMedis,")
        queryBuilder.AppendLine("       tblBayar.JenisBayar, tblBayar.CompanyTagihan, tblBayar.kartu AS 'JenisKartu',")
        queryBuilder.AppendLine("       tblBayar.mesin AS 'JenisMesin', tblDetail.JenisTransaksi,")
        queryBuilder.AppendLine("       tblBayar.JumlahBayar - tblBayar.Kembali AS TotalBayar, tblGigi.RekapID, tblBayar.NoInvoicePembayaran")
        queryBuilder.AppendLine("FROM (")
        queryBuilder.AppendLine("    SELECT tb.TransGigiIDBayar, tb.TransGigiID,")
        queryBuilder.AppendLine("           STUFF((SELECT ', ' + ISNULL(tblTindakan.NamaTindakan, '')")
        queryBuilder.AppendLine("                  FROM TransaksiGigiDetail tblTindakan")
        queryBuilder.AppendLine("                  WHERE tblTindakan.Awal = '0' AND tblTindakan.TransGigiID = tb.TransGigiID")
        queryBuilder.AppendLine("                  FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS DataListTindakan,")
        queryBuilder.AppendLine("           tb.JenisBayar, CASE WHEN tb.JenisBayar = 'TAGIHAN' THEN tb.NoJenisBayar ELSE '' END AS 'CompanyTagihan', tb.JumlahBayar, tb.Kembali, tb.kartu, tb.mesin, tb.NoInvoicePembayaran")
        queryBuilder.AppendLine("    FROM TransaksiGigiBayar tb")
        queryBuilder.AppendLine("         INNER JOIN FilteredTransGigi ft ON tb.TransGigiID = ft.TransGigiID")
        queryBuilder.AppendLine("    WHERE tb.JenisBayar IN (" & jenisBayar & ")")

        If CheckBoxTransaksiNolRupiah.Checked = False Then
            queryBuilder.AppendLine("    AND tb.JumlahBayar > 0")
        End If

        If CheckBoxTagihan.Checked = True And TextBoxNamaPerusahaanLaporanPendapatan.Text.Length > 0 Then
            queryBuilder.AppendLine("    AND tb.NoJenisBayar = '" & TextBoxNamaPerusahaanLaporanPendapatan.Text & "'")
        End If

        queryBuilder.AppendLine("    UNION ALL")
        queryBuilder.AppendLine("    SELECT tba.TransGigiIDBayar, tba.TransGigiID,")
        queryBuilder.AppendLine("           STUFF((SELECT ', ' + ISNULL(tblTindakan.NamaTindakan, '')")
        queryBuilder.AppendLine("                  FROM TransaksiGigiDetail tblTindakan")
        queryBuilder.AppendLine("                  WHERE tblTindakan.Awal = '1' AND tblTindakan.TransGigiID = tba.TransGigiID")
        queryBuilder.AppendLine("                  FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS DataListTindakan,")
        queryBuilder.AppendLine("           tba.JenisBayar, CASE WHEN tba.JenisBayar = 'TAGIHAN' THEN tba.NoJenisBayar ELSE '' END AS 'CompanyTagihan', tba.JumlahBayar, tba.Kembali, tba.kartu, tba.mesin, tba.NoInvoicePembayaran")
        queryBuilder.AppendLine("    FROM TransaksiGigiBayarAwal tba")
        queryBuilder.AppendLine("         INNER JOIN FilteredTransGigi ft ON tba.TransGigiID = ft.TransGigiID")
        queryBuilder.AppendLine("    WHERE tba.JenisBayar IN (" & jenisBayar & ")")

        If CheckBoxTransaksiNolRupiah.Checked = False Then
            queryBuilder.AppendLine("    AND tba.JumlahBayar > 0")
        End If

        If CheckBoxTagihan.Checked = True And TextBoxNamaPerusahaanLaporanPendapatan.Text.Length > 0 Then
            queryBuilder.AppendLine("    AND tba.NoJenisBayar = '" & TextBoxNamaPerusahaanLaporanPendapatan.Text & "'")
        End If

        queryBuilder.AppendLine(") AS tblBayar")
        queryBuilder.AppendLine("LEFT JOIN TransaksiGigi tblGigi ON tblBayar.TransGigiID = tblGigi.TransGigiID")
        queryBuilder.AppendLine("LEFT JOIN TransaksiDetail tblDetail ON tblGigi.NoUrut = tblDetail.NoUrut")
        queryBuilder.AppendLine("LEFT JOIN MasterPasien tblPasien ON tblGigi.IDInternet = tblPasien.NoRekamMedis")
        If ComboBoxPoliTujuanLaporanPendapatan.Text <> "SEMUA TRANSAKSI" Then
            queryBuilder.AppendLine("WHERE JenisPoli = '" & ComboBoxPoliTujuanLaporanPendapatan.Text & "'")
        End If
        queryBuilder.AppendLine("ORDER BY tblBayar.JenisBayar, tblDetail.JenisPoli")

        tblResult = ReadSQL(queryBuilder.ToString())

        Return tblResult
    End Function

    Private Sub ButtonFindPerusahaan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFindPerusahaanLaporanPendapatan.Click
        Find.Show()
        Find.Query("SELECT NoCompany AS 'ID', CompanyCode AS 'Kode Perusahaan', CompanyNama AS 'Keterangan' FROM mastercompany WHERE GETDATE() BETWEEN StartDate AND EndDate", " ORDER BY NoCompany ASC", "FindPerusahaanReportPendapatanKlinik_LaporanPendapatan", 2)
    End Sub

    Private Sub ButtonCetakLaporanPendapatan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCetakLaporanPendapatan.Click
        Dim tblOmset As DataTable = GridControlLaporanPendapatan.DataSource

        ' Tampilan Voucher memakai struktur kolomnya sendiri (lebih kaya, supaya enak dibaca di grid),
        ' jadi dipetakan dulu ke struktur laporan biasa. Dikerjakan di sini — satu pintu untuk kedua
        ' versi cetak — karena tanpa ini Versi2 error mencari kolom "No".
        If tblOmset.Columns.Contains("No") = False Then tblOmset = TabelCetakLaporanVoucher(tblOmset)

        If tblOmset.Rows.Count > 0 Then
            If RadioButtonOptionLaporanPendapatan.Checked Then
                LaporanPendapatanKlinikVersi1(tblOmset)
            ElseIf RadioButtonOptionLaporanPendapatanTagihan.Checked Then
                LaporanPendapatanKlinikVersi2(tblOmset)
            End If
        Else
            MessageBox.Show("Tidak Ada Data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' Judul menyesuaikan mode yang sedang ditampilkan — kalau tetap "Laporan Pendapatan Klinik",
    ' hasil cetak voucher/paket akan mengaku sebagai laporan pendapatan.
    Private Function JudulLaporanPendapatan() As String
        If CheckBoxVoucher.Checked Then Return "Laporan Pemakaian Voucher"
        If CheckBoxPaket.Checked Then Return "Laporan Paket (Pembelian & Pemakaian)"

        Return "Laporan Pendapatan Klinik"
    End Function

    ' Tabel yang dicetak dikirim dari pemanggil, BUKAN dibaca ulang dari grid: tampilan Voucher
    ' perlu dipetakan dulu (lihat ButtonCetakLaporanPendapatan_Click) dan pemetaan itu akan hilang
    ' kalau fungsi ini mengambil sendiri dari DataSource.
    Private Sub LaporanPendapatanKlinikVersi1(ByVal tblOmset As DataTable)
        ' Set Tanggal Transaksi
        Dim tglTransaksiAwal As Date = TglAwalTransaksiLaporanPendapatan.Value
        Dim tglTransaksiAkhir As Date = TglAkhirTransaksiLaporanPendapatan.Value

        ' Convert Tanggal Transaksi
        Dim stringTglTransaksiAwal As String = Format(tglTransaksiAwal, "dd") & " " & BulanIndonesia(Format(tglTransaksiAwal, "MM")) & " " & Format(tglTransaksiAwal, "yyyy")
        Dim stringTglTransaksiAkhir As String = Format(tglTransaksiAkhir, "dd") & " " & BulanIndonesia(Format(tglTransaksiAkhir, "MM")) & " " & Format(tglTransaksiAkhir, "yyyy")

        tblOmset.AcceptChanges()

        Report = XtraReport.FromFile(Application.StartupPath + "\reports\Klinik\Laporan\LaporanPendapatanKlinikVersi1.repx", True)

        Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelJudulLaporan").Text = JudulLaporanPendapatan()
        Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelLokasiLaporan").Text = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'NamaPoli'")
        Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelTanggalLaporan").Text = stringTglTransaksiAwal & " - " & stringTglTransaksiAkhir
        Report.Bands.Item(BandKind.PageFooter).Controls("xrLabelInfoCetak").Text = Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") & " By " & userInput
        Report.DataSource = tblOmset

        'Report.RunDesigner()
        Report.ShowPreview()
        'Report.Print()
    End Sub

    Private Sub LaporanPendapatanKlinikVersi2(ByVal tblOmset As DataTable)
        ' Set Tanggal Transaksi
        Dim tglTransaksiAwal As Date = TglAwalTransaksiLaporanPendapatan.Value
        Dim tglTransaksiAkhir As Date = TglAkhirTransaksiLaporanPendapatan.Value

        ' Convert Tanggal Transaksi
        Dim stringTglTransaksiAwal As String = Format(tglTransaksiAwal, "dd") & " " & BulanIndonesia(Format(tglTransaksiAwal, "MM")) & " " & Format(tglTransaksiAwal, "yyyy")
        Dim stringTglTransaksiAkhir As String = Format(tglTransaksiAkhir, "dd") & " " & BulanIndonesia(Format(tglTransaksiAkhir, "MM")) & " " & Format(tglTransaksiAkhir, "yyyy")

        tblOmset.AcceptChanges()

        ' Ubah kolom "No" menjadi tidak readonly sementara
        tblOmset.Columns("No").ReadOnly = False

        Dim rowNumber As Integer = 0
        For Each Trow As DataRow In tblOmset.Rows
            rowNumber = rowNumber + 1
            Trow("No") = rowNumber
        Next

        Report = XtraReport.FromFile(Application.StartupPath + "\reports\Klinik\Laporan\LaporanPendapatanKlinikVersi2.repx", True)

        Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelJudulLaporan").Text = JudulLaporanPendapatan()
        Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelLokasiLaporan").Text = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'NamaPoli'")
        Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelTanggalLaporan").Text = stringTglTransaksiAwal & " - " & stringTglTransaksiAkhir
        Report.Bands.Item(BandKind.PageFooter).Controls("xrLabelInfoCetak").Text = Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") & " By " & userInput
        Report.DataSource = tblOmset

        'Report.RunDesigner()
        Report.ShowPreview()
        'Report.Print()
    End Sub

    ' Petakan hasil tampilan Voucher ke struktur laporan biasa supaya bisa dicetak dengan kedua
    ' template yang sudah ada (.repx hanya bisa didesain dari Visual Studio 2008, jadi sengaja
    ' TIDAK membuat template baru). Kolom generik dipakai ulang sesuai apa yang dicetak tiap versi:
    '   Versi1 -> mengelompokkan per "Jenis Bayar"      => diisi nama voucher
    '   Versi2 -> mencetak kolom "Detail Pembayaran"    => diisi nama + kode voucher
    ' "Tagihan" diisi NILAI POTONGAN voucher (angka yang memang dicari dari laporan ini); kolom
    ' "Sisa Bayar" tidak terangkut ke cetakan, tetap bisa dilihat di grid.
    Private Function TabelCetakLaporanVoucher(ByVal tblVoucher As DataTable) As DataTable
        Dim rowTable As DataTable = CreateTableLaporanPendapatan()
        Dim nomorUrut As Integer = 0

        For Each rowVoucher As DataRow In tblVoucher.Rows
            nomorUrut = nomorUrut + 1

            Dim newRow As DataRow = rowTable.NewRow
            newRow("No") = nomorUrut
            newRow("No Transaksi") = rowVoucher("Nomor Transaksi")
            newRow("Tgl Transaksi") = rowVoucher("Tanggal")
            newRow("Nama") = rowVoucher("Nama").ToString.ToUpper
            newRow("pemeriksaan") = rowVoucher("Pemeriksaan")
            newRow("Jenis Poli") = rowVoucher("Poli")
            newRow("Jenis Bayar") = rowVoucher("NamaVoucher")
            newRow("NoJenisBayar") = rowVoucher("KodeVoucher")
            newRow("Detail Pembayaran") = rowVoucher("NamaVoucher").ToString & " - " & rowVoucher("KodeVoucher").ToString
            If IsDBNull(rowVoucher("Nominal")) = False Then newRow("Tagihan") = rowVoucher("Nominal")
            rowTable.Rows.Add(newRow)
        Next

        Return rowTable
    End Function

    Private Sub ButtonClearDataLaporanPendapatan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClearDataLaporanPendapatan.Click
        SetClearLaporanPendapatan()
    End Sub

    Private Sub ButtonViewDataRekapTagihan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonViewDataRekapTagihan.Click
        Try
            If ValidasiViewDataRekapTagihan() = True Then
                ViewDataRekapTagihan()
            End If
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Function ValidasiViewDataRekapTagihan() As Boolean
        If TglAkhirTransaksiRekapTagihan.Value.Date < TglAwalTransaksiRekapTagihan.Value.Date Then
            MsgBox("Tanggal akhir tidak boleh melebihi tanggal mulai.", MsgBoxStyle.Exclamation, "Warning!")
            Return False
        End If

        If TextBoxNamaPerusahaanRekapTagihan.Text = "" Then
            MsgBox("Pilih perusahaan terlebih dahulu.", MsgBoxStyle.Exclamation, "Warning!")
            Return False
        End If

        Return True
    End Function

    ' ==========================================================================================
    ' SARINGAN PERUSAHAAN DI TAB INI TIDAK BOLEH BERGANTUNG PADA CheckBoxTagihan.
    '
    ' Checkbox itu milik tab Laporan Pendapatan, bukan tab ini. Dulu saringan `NoJenisBayar =
    ' <perusahaan>` hanya dipasang bila checkbox itu menyala, sehingga mematikannya membuat saringan
    ' perusahaan HILANG DIAM-DIAM: grid closing menampilkan tagihan SELURUH perusahaan pada periode
    ' itu (terukur Juli 2026: 6 perusahaan, Rp 193,7 juta), dan "pilih semua" + Simpan akan
    ' menagihkan transaksi perusahaan lain ke satu perusahaan.
    '
    ' Di tab ini jenis bayarnya memang sudah dikunci 'Tagihan' (lihat GetDataRekapTagihan*), jadi
    ' penjagaan checkbox itu tidak pernah ada gunanya — hanya menyimpan jebakan. Di tab Laporan
    ' Pendapatan penjagaannya TETAP dipakai dan memang perlu: di sana jenis bayarnya dipilih petugas,
    ' dan menyaring NoJenisBayar saat TAGIHAN tidak dicentang akan membuang baris TUNAI/DEBET/CARD.
    '
    ' ValidasiViewDataRekapTagihan sudah mewajibkan nama perusahaan diisi sebelum data ditarik.
    ' ==========================================================================================
    Private Sub ViewDataRekapTagihan()
        Dim tblPoliUmum As DataTable = GetDataRekapTagihanPoliUmum()
        Dim tblPoliGigi As DataTable = GetDataRekapTagihanPoliGigi()

        ' Buat tabel gabungan dengan struktur yang sama
        Dim tblOmset As DataTable = tblPoliUmum.Clone()

        ' Gabungkan data dari kedua tabel
        tblOmset.Merge(tblPoliUmum)
        tblOmset.Merge(tblPoliGigi)

        If tblOmset.Rows.Count > 0 Then
            Dim rowTable As DataTable = CreateTableRekapTagihan()
            rowTable.Rows.Clear()

            For Each rowData As DataRow In tblOmset.Rows
                Dim newRow As DataRow = rowTable.NewRow
                newRow("No") = rowData("NomorUrutTransaksi")
                newRow("ID") = rowData("ID")
                newRow("No Transaksi") = rowData("No Transaksi")
                newRow("Tgl Transaksi") = rowData("Tgl Transaksi")
                newRow("Nama") = rowData("Nama").ToString.ToUpper
                newRow("Tgl Lahir") = rowData("Tgl Lahir")
                newRow("pemeriksaan") = rowData("DataListTindakan")
                newRow("Tagihan") = rowData("TotalBayar")
                newRow("JenisTransaksi") = rowData("JenisTransaksi")
                newRow("Jenis Poli") = rowData("JenisPoli")
                newRow("Jenis Bayar") = rowData("JenisBayar")
                newRow("NoJenisBayar") = KeteranganJenisBayar(rowData)
                newRow("Detail Pembayaran") = KeteranganDetailPembayaran(rowData)
                newRow("Jenis Kasir") = rowData("JenisKasir")

                If IsDBNull(rowData("RekapID")) = False Or IsDBNull(rowData("NoInvoicePembayaran")) = False Then
                    newRow("Status") = "CLOSED"
                    newRow("Checked") = True
                Else
                    newRow("Status") = "-"
                    newRow("Checked") = False
                End If

                rowTable.Rows.Add(newRow)
            Next

            GridControlRekapTagihan.DataSource = rowTable

            ' Disable Control
            TglAwalTransaksiRekapTagihan.Enabled = False
            TglAkhirTransaksiRekapTagihan.Enabled = False
            ComboBoxJenisPoliOrTransaksi.Enabled = False
            ButtonFindPerusahaanRekapTagihan.Enabled = False
            TextBoxNamaPerusahaanRekapTagihan.Enabled = False
            ButtonViewDataRekapTagihan.Enabled = False

            ' Change Color Button
            ButtonFindPerusahaanRekapTagihan.BackColor = Color.FromArgb(225, 225, 225)

            ' Enable Control
            PanelCreateInvoice.Visible = True
        Else
            MessageBox.Show("Tidak Ada Data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Function GetDataRekapTagihanPoliUmum() As DataTable
        ' Set Tanggal Transaksi
        Dim tglTransaksiAwal As Date = TglAwalTransaksiRekapTagihan.Value
        Dim tglTransaksiAkhir As Date = TglAkhirTransaksiRekapTagihan.Value

        ' Set Jenis Pembayaran
        Dim jenisBayar As String = "'Tagihan'"

        ' Set Jenis Poli / Transaksi
        Dim jenisPoliOrTransaksi As String = ""
        If tipeJurnal = "Poli" Then
            If ComboBoxJenisPoliOrTransaksi.Text <> "SEMUA TRANSAKSI" Then
                jenisPoliOrTransaksi = "WHERE JenisPoli = '" & ComboBoxJenisPoliOrTransaksi.Text & "'"
            End If
        ElseIf tipeJurnal = "Transaksi" Then
            jenisPoliOrTransaksi = "WHERE JenisTransaksi = '" & ComboBoxJenisPoliOrTransaksi.Text & "'"
        End If

        ' Set Tabel Result
        Dim tblResult As New DataTable

        Dim queryBuilder As New StringBuilder()

        ' WITH clause for FilteredTransPoli
        queryBuilder.AppendLine("WITH FilteredTransPoli AS (")
        queryBuilder.AppendLine("    SELECT TransPoliID, RekapID")
        queryBuilder.AppendLine("    FROM TransaksiPoli")
        queryBuilder.AppendLine("    WHERE (CONVERT(DATE, TglPoli) BETWEEN '" & Format(tglTransaksiAwal.Date, "yyyyMMdd") & "' AND '" & Format(tglTransaksiAkhir.Date, "yyyyMMdd") & "') AND Deleted = 0")
        queryBuilder.AppendLine(")")

        ' Main SELECT statement
        queryBuilder.AppendLine("SELECT ROW_NUMBER() OVER (PARTITION BY tblDetail.JenisPoli, tblBayar.JenisBayar, tblBayar.CompanyTagihan, tblBayar.mesin ORDER BY tblPoli.TglPoli, tblPoli.NoUrut) AS NomorUrutTransaksi, tblBayar.TransPoliIDBayar AS 'ID',")
        queryBuilder.AppendLine("       tblPoli.NoUrut AS 'No Transaksi', CONVERT(DATE, tblPoli.TglPoli) AS 'Tgl Transaksi',")
        queryBuilder.AppendLine("       tblDetail.JenisPoli, tblBayar.DataListTindakan, tblPasien.Nama, CONVERT(DATE, tblPasien.TglLahir) AS 'Tgl Lahir', tblPasien.NoRekamMedis,")
        queryBuilder.AppendLine("       tblBayar.JenisBayar, tblBayar.CompanyTagihan, tblBayar.kartu AS 'JenisKartu',")
        queryBuilder.AppendLine("       tblBayar.mesin AS 'JenisMesin', tblDetail.JenisTransaksi,")
        queryBuilder.AppendLine("       tblBayar.JumlahBayar - tblBayar.Kembali AS TotalBayar, tblPoli.RekapID, tblBayar.NoInvoicePembayaran, tblBayar.JenisKasir")
        queryBuilder.AppendLine("FROM (")
        queryBuilder.AppendLine("    SELECT tb.TransPoliIDBayar, tb.TransPoliID,")
        queryBuilder.AppendLine("           STUFF((SELECT ', ' + ISNULL(tblTindakan.NamaTindakan, '')")
        queryBuilder.AppendLine("                  FROM TransaksiPoliDetail tblTindakan")
        queryBuilder.AppendLine("                  WHERE tblTindakan.Awal = '0' AND tblTindakan.TransPoliID = tb.TransPoliID")
        queryBuilder.AppendLine("                  FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS DataListTindakan,")
        queryBuilder.AppendLine("           tb.JenisBayar, CASE WHEN tb.JenisBayar = 'TAGIHAN' THEN tb.NoJenisBayar ELSE '' END AS 'CompanyTagihan', tb.JumlahBayar, tb.Kembali, tb.kartu, tb.mesin, tb.NoInvoicePembayaran, 'Transaksi Tindakan' AS JenisKasir")
        queryBuilder.AppendLine("    FROM TransaksiPoliBayar tb")
        queryBuilder.AppendLine("         INNER JOIN FilteredTransPoli ft ON tb.TransPoliID = ft.TransPoliID")
        queryBuilder.AppendLine("    WHERE tb.JumlahBayar > 0 AND tb.JenisBayar IN (" & jenisBayar & ")")
        If TextBoxNamaPerusahaanRekapTagihan.Text.Length > 0 Then   ' saringan perusahaan TIDAK boleh bergantung CheckBoxTagihan (lihat catatan di ViewDataRekapTagihan)
            queryBuilder.AppendLine("    AND tb.NoJenisBayar = '" & TextBoxNamaPerusahaanRekapTagihan.Text & "'")
        End If
        queryBuilder.AppendLine("    UNION ALL")
        queryBuilder.AppendLine("    SELECT tba.TransPoliIDBayar, tba.TransPoliID,")
        queryBuilder.AppendLine("           STUFF((SELECT ', ' + ISNULL(tblTindakan.NamaTindakan, '')")
        queryBuilder.AppendLine("                  FROM TransaksiPoliDetail tblTindakan")
        queryBuilder.AppendLine("                  WHERE tblTindakan.Awal = '1' AND tblTindakan.TransPoliID = tba.TransPoliID")
        queryBuilder.AppendLine("                  FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS DataListTindakan,")
        queryBuilder.AppendLine("           tba.JenisBayar, CASE WHEN tba.JenisBayar = 'TAGIHAN' THEN tba.NoJenisBayar ELSE '' END AS 'CompanyTagihan', tba.JumlahBayar, tba.Kembali, tba.kartu, tba.mesin, tba.NoInvoicePembayaran, 'Transaksi Awal' AS JenisKasir")
        queryBuilder.AppendLine("    FROM TransaksiPoliBayarAwal tba")
        queryBuilder.AppendLine("         INNER JOIN FilteredTransPoli ft ON tba.TransPoliID = ft.TransPoliID")
        queryBuilder.AppendLine("    WHERE tba.JumlahBayar > 0 AND tba.JenisBayar IN (" & jenisBayar & ")")
        If TextBoxNamaPerusahaanRekapTagihan.Text.Length > 0 Then   ' saringan perusahaan TIDAK boleh bergantung CheckBoxTagihan (lihat catatan di ViewDataRekapTagihan)
            queryBuilder.AppendLine("    AND tba.NoJenisBayar = '" & TextBoxNamaPerusahaanRekapTagihan.Text & "'")
        End If
        queryBuilder.AppendLine(") AS tblBayar")
        queryBuilder.AppendLine("LEFT JOIN TransaksiPoli tblPoli ON tblBayar.TransPoliID = tblPoli.TransPoliID")
        queryBuilder.AppendLine("LEFT JOIN TransaksiDetail tblDetail ON tblPoli.NoUrut = tblDetail.NoUrut")
        queryBuilder.AppendLine("LEFT JOIN MasterPasien tblPasien ON tblPoli.IDInternet = tblPasien.NoRekamMedis")
        queryBuilder.AppendLine(jenisPoliOrTransaksi)
        queryBuilder.AppendLine("ORDER BY tblBayar.JenisBayar, tblDetail.JenisPoli")

        tblResult = ReadSQL(queryBuilder.ToString())

        Return tblResult
    End Function

    Function GetDataRekapTagihanPoliGigi() As DataTable
        ' Set Tanggal Transaksi
        Dim tglTransaksiAwal As Date = TglAwalTransaksiRekapTagihan.Value
        Dim tglTransaksiAkhir As Date = TglAkhirTransaksiRekapTagihan.Value

        ' Set Jenis Pembayaran
        Dim jenisBayar As String = "'Tagihan'"

        ' Set Jenis Poli / Transaksi
        Dim jenisPoliOrTransaksi As String = ""
        If tipeJurnal = "Poli" Then
            If ComboBoxJenisPoliOrTransaksi.Text <> "SEMUA TRANSAKSI" Then
                jenisPoliOrTransaksi = "WHERE JenisPoli = '" & ComboBoxJenisPoliOrTransaksi.Text & "'"
            End If
        ElseIf tipeJurnal = "Transaksi" Then
            jenisPoliOrTransaksi = "WHERE JenisTransaksi = '" & ComboBoxJenisPoliOrTransaksi.Text & "'"
        End If

        ' Set Tabel Result
        Dim tblResult As New DataTable

        Dim queryBuilder As New StringBuilder()

        ' WITH clause for FilteredTransPoli
        queryBuilder.AppendLine("WITH FilteredTransGigi AS (")
        queryBuilder.AppendLine("    SELECT TransGigiID, RekapID")
        queryBuilder.AppendLine("    FROM TransaksiGigi")
        queryBuilder.AppendLine("    WHERE (CONVERT(DATE, TglGigi) BETWEEN '" & Format(tglTransaksiAwal.Date, "yyyyMMdd") & "' AND '" & Format(tglTransaksiAkhir.Date, "yyyyMMdd") & "') AND Deleted = 0")
        queryBuilder.AppendLine(")")

        ' Main SELECT statement
        queryBuilder.AppendLine("SELECT ROW_NUMBER() OVER (PARTITION BY tblDetail.JenisPoli, tblBayar.JenisBayar, tblBayar.CompanyTagihan, tblBayar.mesin ORDER BY tblGigi.TglGigi, tblGigi.NoUrut) AS NomorUrutTransaksi, tblBayar.TransGigiIDBayar AS 'ID',")
        queryBuilder.AppendLine("       tblGigi.NoUrut AS 'No Transaksi', CONVERT(DATE, tblGigi.TglGigi) AS 'Tgl Transaksi',")
        queryBuilder.AppendLine("       tblDetail.JenisPoli, tblBayar.DataListTindakan, tblPasien.Nama, CONVERT(DATE, tblPasien.TglLahir) AS 'Tgl Lahir', tblPasien.NoRekamMedis,")
        queryBuilder.AppendLine("       tblBayar.JenisBayar, tblBayar.CompanyTagihan, tblBayar.kartu AS 'JenisKartu',")
        queryBuilder.AppendLine("       tblBayar.mesin AS 'JenisMesin', tblDetail.JenisTransaksi,")
        queryBuilder.AppendLine("       tblBayar.JumlahBayar - tblBayar.Kembali AS TotalBayar, tblGigi.RekapID, tblBayar.NoInvoicePembayaran, tblBayar.JenisKasir")
        queryBuilder.AppendLine("FROM (")
        queryBuilder.AppendLine("    SELECT tb.TransGigiIDBayar, tb.TransGigiID,")
        queryBuilder.AppendLine("           STUFF((SELECT ', ' + ISNULL(tblTindakan.NamaTindakan, '')")
        queryBuilder.AppendLine("                  FROM TransaksiGigiDetail tblTindakan")
        queryBuilder.AppendLine("                  WHERE tblTindakan.Awal = '0' AND tblTindakan.TransGigiID = tb.TransGigiID")
        queryBuilder.AppendLine("                  FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS DataListTindakan,")
        queryBuilder.AppendLine("           tb.JenisBayar, CASE WHEN tb.JenisBayar = 'TAGIHAN' THEN tb.NoJenisBayar ELSE '' END AS 'CompanyTagihan', tb.JumlahBayar, tb.Kembali, tb.kartu, tb.mesin, tb.NoInvoicePembayaran, 'Transaksi Tindakan' AS JenisKasir")
        queryBuilder.AppendLine("    FROM TransaksiGigiBayar tb")
        queryBuilder.AppendLine("         INNER JOIN FilteredTransGigi ft ON tb.TransGigiID = ft.TransGigiID")
        queryBuilder.AppendLine("    WHERE tb.JumlahBayar > 0 AND tb.JenisBayar IN (" & jenisBayar & ")")
        If TextBoxNamaPerusahaanRekapTagihan.Text.Length > 0 Then   ' saringan perusahaan TIDAK boleh bergantung CheckBoxTagihan (lihat catatan di ViewDataRekapTagihan)
            queryBuilder.AppendLine("    AND tb.NoJenisBayar = '" & TextBoxNamaPerusahaanRekapTagihan.Text & "'")
        End If
        queryBuilder.AppendLine("    UNION ALL")
        queryBuilder.AppendLine("    SELECT tba.TransGigiIDBayar, tba.TransGigiID,")
        queryBuilder.AppendLine("           STUFF((SELECT ', ' + ISNULL(tblTindakan.NamaTindakan, '')")
        queryBuilder.AppendLine("                  FROM TransaksiGigiDetail tblTindakan")
        queryBuilder.AppendLine("                  WHERE tblTindakan.Awal = '1' AND tblTindakan.TransGigiID = tba.TransGigiID")
        queryBuilder.AppendLine("                  FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS DataListTindakan,")
        queryBuilder.AppendLine("           tba.JenisBayar, CASE WHEN tba.JenisBayar = 'TAGIHAN' THEN tba.NoJenisBayar ELSE '' END AS 'CompanyTagihan', tba.JumlahBayar, tba.Kembali, tba.kartu, tba.mesin, tba.NoInvoicePembayaran, 'Transaksi Awal' AS JenisKasir")
        queryBuilder.AppendLine("    FROM TransaksiGigiBayarAwal tba")
        queryBuilder.AppendLine("         INNER JOIN FilteredTransGigi ft ON tba.TransGigiID = ft.TransGigiID")
        queryBuilder.AppendLine("    WHERE tba.JumlahBayar > 0 AND tba.JenisBayar IN (" & jenisBayar & ")")
        If TextBoxNamaPerusahaanRekapTagihan.Text.Length > 0 Then   ' saringan perusahaan TIDAK boleh bergantung CheckBoxTagihan (lihat catatan di ViewDataRekapTagihan)
            queryBuilder.AppendLine("    AND tba.NoJenisBayar = '" & TextBoxNamaPerusahaanRekapTagihan.Text & "'")
        End If
        queryBuilder.AppendLine(") AS tblBayar")
        queryBuilder.AppendLine("LEFT JOIN TransaksiGigi tblGigi ON tblBayar.TransGigiID = tblGigi.TransGigiID")
        queryBuilder.AppendLine("LEFT JOIN TransaksiDetail tblDetail ON tblGigi.NoUrut = tblDetail.NoUrut")
        queryBuilder.AppendLine("LEFT JOIN MasterPasien tblPasien ON tblGigi.IDInternet = tblPasien.NoRekamMedis")
        queryBuilder.AppendLine(jenisPoliOrTransaksi)
        queryBuilder.AppendLine("ORDER BY tblBayar.JenisBayar, tblDetail.JenisPoli")

        tblResult = ReadSQL(queryBuilder.ToString())

        Return tblResult
    End Function

    Private Sub ButtonClearDataRekapTagihan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClearDataRekapTagihan.Click
        SetClearRekapTagihan()
    End Sub

    Private Sub CheckBoxSemuaTransaksiRekapTagihan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxSemuaTransaksiRekapTagihan.CheckedChanged
        ' Tutup dulu editor sel yang mungkin masih terbuka. Sub ini menulis LANGSUNG ke DataTable,
        ' sedangkan editor yang belum ditutup akan menimpakan nilai lamanya begitu ia menutup —
        ' hasilnya satu baris bisa membangkang dari "pilih semua"/"lepas semua".
        GridViewRekapTagihan.PostEditor()
        GridViewRekapTagihan.UpdateCurrentRow()

        Dim tblTagihan As DataTable = GridControlRekapTagihan.DataSource

        If tblTagihan IsNot Nothing AndAlso tblTagihan.Rows.Count > 0 Then
            For Each rowTransaksi As DataRow In tblTagihan.Rows
                If CheckBoxSemuaTransaksiRekapTagihan.CheckState = CheckState.Checked Then
                    rowTransaksi("Checked") = True
                Else
                    If rowTransaksi("Status").ToString() <> "CLOSED" Then rowTransaksi("Checked") = False
                End If
            Next

            ' Perubahan langsung ke DataTable tidak melewati CellValueChanged, jadi footer kalkulasinya
            ' harus diminta hitung ulang di sini.
            GridViewRekapTagihan.UpdateSummary()
        Else
            CheckBoxSemuaTransaksiRekapTagihan.Checked = False
        End If
    End Sub

    Private Sub ButtonFindBankTagihan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFindBankTagihan.Click
        Find.Show()
        Find.QueryTheMysql("SELECT a.NamaBank, a.NomorRekening, a.NamaRekening, a.CabangRekening, a.CoaBank FROM MasterBankTransfer a WHERE a.StatusAktif = 1 AND a.TransferTagihan = 1", " ", "FindBankTagihanReportPendapatanKlinik", 0, Find_Omset)
    End Sub

    Private Sub ButtonSimpanRekapTagihan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSimpanRekapTagihan.Click
        Try
            If ValidasiSimpanRekapTagihan() = True Then
                CreateInvoiceTagihan()
            End If
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Function CreateTableInvoice() As DataTable
        Dim Thasil As New DataTable

        insertRowToTbls(Thasil, "Typstring", "TglInvoice")
        insertRowToTbls(Thasil, "Typstring", "TglJatuhTempo")
        insertRowToTbls(Thasil, "Typstring", "NomorInvoice")
        insertRowToTbls(Thasil, "Typstring", "NamaPerusahaan")
        insertRowToTbls(Thasil, "Typstring", "TglTTD")
        insertRowToTbls(Thasil, "Typstring", "UserTTD")
        insertRowToTbls(Thasil, "Typstring", "KeteranganNominal")
        insertRowToTbls(Thasil, "Typstring", "NamaRekeningBank")
        insertRowToTbls(Thasil, "Typstring", "NomorRekeningBank")
        insertRowToTbls(Thasil, "Typstring", "NamaBank")
        insertRowToTbls(Thasil, "Typstring", "KeteranganEmail")

        Return Thasil
    End Function

    Function ValidasiSimpanRekapTagihan() As Boolean
        'Dim tblTransaksi As DataTable = GridControlTransaksi.DataSource

        'If tblTransaksi IsNot Nothing AndAlso tblTransaksi.Rows.Count > 0 Then
        '    For Each rowTransaksi As DataRow In tblTransaksi.Select("[Checked]=True")
        '        If rowTransaksi("Status") <> "CLOSED" Then
        '            ' Validasi apakah ada tindakan yang belum di BHP
        '            If rowTransaksi("Status BHP") = 1 And rowTransaksi("Nominal BHP") = 0 Then
        '                MsgBox("Tindakan " & rowTransaksi("Tindakan") & " atas nama " & rowTransaksi("Nama Pasien") & " (" & rowTransaksi("No Transaksi") & ") belum dilakukan BHP.", MsgBoxStyle.Exclamation, "Warning!")
        '                Return False
        '            End If
        '        End If
        '    Next
        'End If

        If TglJatuhTempoRekapTagihan.Value.Date < Date.Now.Date Then
            MsgBox("Tanggal jatuh tempo tidak boleh kurang dari tanggal hari ini.", MsgBoxStyle.Exclamation, "Warning!")
            Return False
        End If

        If TextBoxBankTransferKeterangan.Text = "" Then
            MsgBox("Pilih bank terlebih dahulu.", MsgBoxStyle.Exclamation, "Warning!")
            Return False
        End If

        ' Gerbang koneksi SEBELUM ringkasan ditampilkan: penyimpanan menulis ke tiga tempat berurutan
        ' (keuangan online, DB lokal, server jurnal) tanpa transaksi yang menaungi ketiganya, jadi
        ' putus di tengah meninggalkan keadaan separuh jadi. Menghadang di sini tidak membuatnya
        ' atomik — hanya memastikan proses tidak dimulai saat servernya sudah jelas tidak terjangkau,
        ' dan tidak membuang waktu petugas mengonfirmasi sesuatu yang pasti gagal.
        If CekKoneksiServerKirim() = False Then Return False

        ' Dorong centang yang masih tertahan di editor sel ke DataTable SEBELUM apa pun dibaca.
        ' Petugas biasa mencentang baris terakhir lalu langsung menekan Simpan tanpa memindahkan
        ' kursor; tanpa baris ini nilai centang itu belum ada di sumber data, sehingga transaksinya
        ' tidak ikut terhitung MAUPUN ikut ditagihkan — dan tidak ada tanda apa pun bahwa ia hilang.
        GridViewRekapTagihan.PostEditor()
        GridViewRekapTagihan.UpdateCurrentRow()

        ' Kalkulasi lengkap SEBELUM menyimpan. Sebelumnya konfirmasinya hanya "Yakin ingin menyimpan
        ' data tersebut?" tanpa satu angka pun, sehingga nilai invoice baru ketahuan setelah invoice
        ' terbentuk — padahal sesudah itu tidak bisa ditambah, hanya bisa dihapus lalu diulang.
        ' "Belum dicentang" sengaja ikut ditampilkan: itu penangkap kesalahan yang paling sering,
        ' yaitu transaksi yang seharusnya masuk tagihan bulan ini tapi luput dicentang.
        Dim jmlDipilih As Integer = 0
        Dim jmlTertinggal As Integer = 0
        Dim jmlClosed As Integer = 0
        Dim nilaiDipilih As Double = 0
        Dim nilaiTertinggal As Double = 0
        Dim nilaiClosed As Double = 0

        Dim tblTransaksi As DataTable = GridControlRekapTagihan.DataSource
        If tblTransaksi IsNot Nothing Then
            For Each rowTransaksi As DataRow In tblTransaksi.Rows
                Dim nilaiBaris As Double = CDbl(rowTransaksi("Tagihan"))

                If Convert.ToString(rowTransaksi("Status")) = "CLOSED" Then
                    jmlClosed += 1
                    nilaiClosed += nilaiBaris
                ElseIf BarisIkutDitagih(rowTransaksi("Checked"), rowTransaksi("Status")) Then
                    jmlDipilih += 1
                    nilaiDipilih += nilaiBaris
                Else
                    jmlTertinggal += 1
                    nilaiTertinggal += nilaiBaris
                End If
            Next
        End If

        ' Dihadang di sini, bukan di tengah CreateInvoiceTagihan: di sana pesannya baru muncul sesudah
        ' petugas melewati seluruh isian jatuh tempo dan bank.
        If jmlDipilih = 0 Then
            MsgBox("Belum ada transaksi yang dicentang, jadi tidak ada yang bisa ditagihkan.", MsgBoxStyle.Exclamation, "Warning!")
            Return False
        End If

        Dim ringkasan As String = _
            "AKAN DITAGIHKAN" & vbCrLf & _
            "     " & jmlDipilih & " transaksi" & vbCrLf & _
            "     Rp " & Format(nilaiDipilih, "#,##0") & vbCrLf & vbCrLf & _
            "Tidak ikut (belum dicentang) : " & jmlTertinggal & " transaksi, Rp " & Format(nilaiTertinggal, "#,##0") & vbCrLf & _
            "Sudah ditagih sebelumnya     : " & jmlClosed & " transaksi, Rp " & Format(nilaiClosed, "#,##0") & vbCrLf & vbCrLf & _
            "Perusahaan  : " & TextBoxNamaPerusahaanRekapTagihan.Text & vbCrLf & _
            "Periode     : " & Format(TglAwalTransaksiRekapTagihan.Value, "dd/MM/yyyy") & " s/d " & Format(TglAkhirTransaksiRekapTagihan.Value, "dd/MM/yyyy") & vbCrLf & _
            "Jatuh tempo : " & Format(TglJatuhTempoRekapTagihan.Value, "dd/MM/yyyy") & vbCrLf & _
            "Bank        : " & TextBoxBankTransferKeterangan.Text & vbCrLf & vbCrLf & _
            "Simpan dan buat invoice sekarang?"

        If MsgBox(ringkasan, MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Konfirmasi Closing Tagihan") = MsgBoxResult.No Then
            Return False
        End If

        Return True
    End Function

    ' Nomor invoice: <urut 4 digit>/<kode klinik>/<bulan romawi>/<tahun>, urutan dihitung per BULAN.
    '
    ' Serinya SENGAJA GLOBAL lintas cabang, JANGAN disaring per `cabang`. Terverifikasi di server
    ' keuangan (Juli 2026): deret 0001–0287 dipakai bergantian oleh 16 kombinasi cabang/kode klinik.
    ' Dan yang menentukan: KODE KLINIK TIDAK UNIK PER CABANG — `CDC-DHU` dipakai 4 cabang
    ' (L001/U005/U006/U007) dan `MMC-HBT` dipakai L003 DAN Z001. Kalau tiap cabang diberi serinya
    ' sendiri, dua cabang berkode sama akan menerbitkan nomor yang sama persis, justru tabrakan yang
    ' hendak dicegah. Seri global inilah yang menjaga keunikan.
    '
    ' Dua hal yang diperbaiki dari versi lama:
    '  1. Lookup-nya MELEMPAR saat koneksi gagal. Versi lama memakai ReadThemysqlNIK yang mengembalikan
    '     "-" untuk gagal MAUPUN kosong, lalu jatuh ke cabang "0001": server yang sedang tidak
    '     terjangkau membuat penomoran mengulang dari awal — dan 0001 bulan berjalan hampir pasti
    '     sudah dipakai cabang lain.
    '  2. Nomornya dipastikan BELUM terpakai, bukan sekadar MAX+1, sehingga dua petugas yang menyimpan
    '     hampir bersamaan tidak mendapat nomor yang sama. Baris yang sudah dihapus (deleted=1) tetap
    '     dihitung — nomornya tidak boleh dipakai ulang, karena semua pencarian invoice memakai
    '     notagihancompany dan dua baris bernomor sama membuat pencarian itu ambigu.
    '
    ' Sisa risiko: celah antara pemeriksaan dan INSERT. Penutup sesungguhnya adalah UNIQUE index pada
    ' notagihancompany di server keuangan — perlu ALTER tabel produksi (26.449 baris, MyISAM), dan
    ' saat ini datanya memang belum pernah kembar sehingga index itu bisa dipasang tanpa pembersihan.
    Function GetNomorInvoiceTagihan() As String
        Dim kodeInvoice As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName='KodeInvoicePoli'")
        Dim akhiran As String = "/" & kodeInvoice & "/" & GetRomes(DateAndTime.Now.Month) & "/" & Format(DateAndTime.Now, "yyyy")
        Dim tglSekarang As String = Format(DateAndTime.Now, "yyyy-MM-dd")

        Dim urutTerakhir As String = BacaSatuNilaiMySqlChecked( _
            "SELECT LEFT(notagihancompany, 4) FROM acctransrekaptagihan " & _
            "WHERE DATE(tgltagihan) BETWEEN DATE_FORMAT('" & tglSekarang & "', '%Y-%m-01') AND LAST_DAY('" & tglSekarang & "') " & _
            "ORDER BY notagihancompany DESC LIMIT 1", DB_Keuangan)

        Dim urut As Integer = 0
        If urutTerakhir <> "" Then Integer.TryParse(urutTerakhir, urut)

        For percobaan As Integer = 1 To 20
            urut += 1
            Dim calon As String = Format(urut, "0000") & akhiran

            ' Tanpa saringan cabang, sama seperti pencarian urutan di atas: yang harus unik adalah
            ' nomor lengkapnya, dan kode klinik di dalamnya dipakai bersama beberapa cabang.
            If BacaSatuNilaiMySqlChecked("SELECT COUNT(*) FROM acctransrekaptagihan WHERE notagihancompany = '" & calon & "'", DB_Keuangan) = "0" Then
                Return calon
            End If
        Next

        Throw New Exception("Tidak berhasil menemukan nomor invoice yang belum terpakai setelah 20 percobaan — periksa data acctransrekaptagihan untuk cabang " & kodeLokasiKlinik & ".")
    End Function

    Private Sub CreateInvoiceTagihan()
        Dim jenisTransaksi As String = ""
        Dim jenisPoli As String = ""

        Dim totalTransaksi As Integer = 0

        ' Long, bukan Integer: rupiah menembus batas Integer (±2,1 miliar) pada invoice besar dan
        ' penjumlahannya akan melempar OverflowException di tengah proses. Long juga dicetak ke SQL
        ' apa adanya ("3250000") tanpa risiko notasi ilmiah seperti Double.
        Dim totalTagihan As Long = 0

        ' Set Jenis Poli / Transaksi
        If tipeJurnal = "Transaksi" Then jenisTransaksi = ComboBoxJenisPoliOrTransaksi.Text
        If tipeJurnal = "Poli" Then jenisPoli = ComboBoxJenisPoliOrTransaksi.Text

        Dim sqlJenisTransaksi As String = If(jenisTransaksi = "", "NULL", "'" & jenisTransaksi & "'")
        Dim sqlJenisPoli As String = If(jenisPoli = "", "NULL", "'" & jenisPoli & "'")

        ' Set Nama Perusahaan
        Dim splitPerusahaan() As String = TextBoxNamaPerusahaanRekapTagihan.Text.Split("-")

        ' Set Data No Transaksi
        Dim idDetailTransaksiPoliAwal As String = ""
        Dim idDetailTransaksiGigiAwal As String = ""
        Dim idDetailTransaksiPoliAkhir As String = ""
        Dim idDetailTransaksiGigiAkhir As String = ""
        Dim nomorTransaksiPoli As String = ""
        Dim nomorTransaksiGigi As String = ""

        ' Set HashSet untuk setiap jenis transaksi
        Dim tempTransaksiPoli As New HashSet(Of String)()
        Dim tempTransaksiGigi As New HashSet(Of String)()

        Dim tblTransaksi As DataTable = GridControlRekapTagihan.DataSource
        If tblTransaksi IsNot Nothing AndAlso tblTransaksi.Rows.Count > 0 Then
            For Each rowTransaksi As DataRow In tblTransaksi.Rows
                ' Aturannya dipindah ke BarisIkutDitagih supaya sama persis dengan angka yang tampil
                ' di footer dan di konfirmasi simpan (dulu: Select("[Checked]=True") + cek Status).
                If BarisIkutDitagih(rowTransaksi("Checked"), rowTransaksi("Status")) Then
                    totalTagihan = totalTagihan + CLng(rowTransaksi("Tagihan"))

                    Select Case rowTransaksi("JenisTransaksi").ToString()
                        Case "POLI"
                            ' Set Nomor Transaksi
                            If tempTransaksiPoli.Add(rowTransaksi("No Transaksi").ToString()) Then
                                nomorTransaksiPoli &= "'" & rowTransaksi("No Transaksi").ToString() & "',"
                            End If

                            ' Set ID Detail (Untuk Update Flag)
                            If rowTransaksi("Jenis Kasir").ToString() = "Transaksi Awal" Then
                                idDetailTransaksiPoliAwal &= "'" & rowTransaksi("ID").ToString() & "',"
                            ElseIf rowTransaksi("Jenis Kasir").ToString() = "Transaksi Tindakan" Then
                                idDetailTransaksiPoliAkhir &= "'" & rowTransaksi("ID").ToString() & "',"
                            End If
                        Case "GIGI"
                            ' Set Nomor Transaksi
                            If tempTransaksiGigi.Add(rowTransaksi("No Transaksi").ToString()) Then
                                nomorTransaksiGigi &= "'" & rowTransaksi("No Transaksi").ToString() & "',"
                            End If

                            ' Set ID Detail (Untuk Update Flag)
                            If rowTransaksi("Jenis Kasir").ToString() = "Transaksi Awal" Then
                                idDetailTransaksiGigiAwal &= "'" & rowTransaksi("ID").ToString() & "',"
                            ElseIf rowTransaksi("Jenis Kasir").ToString() = "Transaksi Tindakan" Then
                                idDetailTransaksiGigiAkhir &= "'" & rowTransaksi("ID").ToString() & "',"
                            End If
                    End Select

                    ' Tambah Total Transaksi
                    totalTransaksi += 1
                End If
            Next

            If totalTransaksi > 0 Then
                Dim nomorInvoice As String = GetNomorInvoiceTagihan()
                Dim tglJatuhTempo As Date = TglJatuhTempoRekapTagihan.Value
                Dim tblBankTransfer As DataTable = ReadThemysql("SELECT * FROM MasterBankTransfer WHERE CoaBank = '" & TextBoxBankTransferCoa.Text & "'", DB_Omset)

                ' Dulu langsung Rows(0): bank yang tidak ketemu (atau server omset tidak terjangkau)
                ' membuat form crash. Dihadang SEBELUM baris pertama ditulis, jadi tidak ada sisa.
                If tblBankTransfer.Rows.Count = 0 Then
                    MessageBox.Show("Data bank dengan COA '" & TextBoxBankTransferCoa.Text & "' tidak ditemukan (atau server omset tidak terjangkau)." & vbCrLf & _
                                    "Invoice BELUM disimpan — pilih ulang banknya lalu coba lagi.", _
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Dim rowBankTransfer As DataRow = tblBankTransfer.Rows(0)

                ' Insert Database Rekap Tagihan Online
                WriteThemysql("INSERT INTO acctransrekaptagihan(company, tgltagihan, periode, duedate, totaltagihan, keterangan, createdate, createby, notagihancompany, cabang, JenisTransaksi, JenisPoli) " & _
                              "VALUES('" & TextBoxNamaPerusahaanRekapTagihan.Text & "', NOW(), '" & CInt(0) & "', '" & Format(tglJatuhTempo, "yyyy/MM/dd") & "', '" & totalTagihan & "', '-', NOW(), '" & userInput & "', '" & nomorInvoice & "', '" & kodeLokasiKlinik & "', " & sqlJenisTransaksi & ", " & sqlJenisPoli & ") ", DB_Keuangan)

                ' Ambil RekapID milik baris yang BARU SAJA disisipkan, dikenali dari nomor invoice +
                ' cabangnya. Versi lama memakai `ORDER BY RekapID DESC LIMIT 1` tanpa saringan apa pun:
                ' server keuangan itu dipakai banyak cabang, jadi kalau cabang lain menyisipkan pada
                ' saat yang hampir sama, yang terbaca RekapID MILIK MEREKA — lalu dipakai untuk
                ' mengunci acctranstagihandetail dan disimpan ke RekapTagihan lokal.
                ' (LAST_INSERT_ID() tidak bisa dipakai: WriteThemysql menutup koneksinya, sedangkan
                ' nilai itu hanya hidup di koneksi yang melakukan INSERT.)
                '
                ' Pembacaan ini SEKALIGUS memverifikasi INSERT di atas — WriteThemysql menelan error,
                ' jadi tanpa ini INSERT yang gagal akan diteruskan dengan RekapID asal-asalan.
                Dim rekapID As String = ""
                Try
                    rekapID = BacaSatuNilaiMySqlChecked("SELECT RekapID FROM acctransrekaptagihan WHERE notagihancompany = '" & nomorInvoice & "' AND cabang = '" & kodeLokasiKlinik & "' ORDER BY RekapID DESC LIMIT 1", DB_Keuangan)
                Catch exBaca As Exception
                    MessageBox.Show("Rekap tagihan sudah dikirim ke server keuangan tapi jawabannya tidak terbaca:" & vbCrLf & exBaca.Message & vbCrLf & vbCrLf & _
                                    "Belum ada data lokal yang diubah. SEBELUM mengulang, periksa apakah invoice " & nomorInvoice & " sudah ada di keuangan — kalau ada, hapus dulu supaya tidak menggantung.", _
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try

                If rekapID = "" Then
                    MessageBox.Show("Rekap tagihan GAGAL tersimpan di server keuangan (invoice " & nomorInvoice & " tidak ditemukan setelah disimpan)." & vbCrLf & vbCrLf & _
                                    "Invoice tidak jadi dibuat dan tidak ada data lokal yang diubah — periksa koneksi lalu ulangi.", _
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                ' Update Flag Tabel TransaksiPoliBayarAwal
                If idDetailTransaksiPoliAwal <> "" Then
                    idDetailTransaksiPoliAwal = Strings.Left(idDetailTransaksiPoliAwal, idDetailTransaksiPoliAwal.Length - 1)
                    WriteSQL("UPDATE TransaksiPoliBayarAwal SET NoInvoicePembayaran = '" & nomorInvoice & "', RekapBy = '" & userInput & "', RekapDate = GETDATE() WHERE TransPoliIDBayar IN (" & idDetailTransaksiPoliAwal & ")", 4)
                End If

                ' Update Flag Tabel TransaksiGigiBayarAwal
                If idDetailTransaksiGigiAwal <> "" Then
                    idDetailTransaksiGigiAwal = Strings.Left(idDetailTransaksiGigiAwal, idDetailTransaksiGigiAwal.Length - 1)
                    WriteSQL("UPDATE TransaksiGigiBayarAwal SET NoInvoicePembayaran = '" & nomorInvoice & "', RekapBy = '" & userInput & "', RekapDate = GETDATE() WHERE TransGigiIDBayar IN (" & idDetailTransaksiGigiAwal & ")", 4)
                End If

                ' Update Flag Tabel TransaksiPoliBayar
                If idDetailTransaksiPoliAkhir <> "" Then
                    idDetailTransaksiPoliAkhir = Strings.Left(idDetailTransaksiPoliAkhir, idDetailTransaksiPoliAkhir.Length - 1)
                    WriteSQL("UPDATE TransaksiPoliBayar SET NoInvoicePembayaran = '" & nomorInvoice & "', RekapBy = '" & userInput & "', RekapDate = GETDATE() WHERE TransPoliIDBayar IN (" & idDetailTransaksiPoliAkhir & ")", 4)
                End If

                ' Update Flag Tabel TransaksiGigiBayar
                If idDetailTransaksiGigiAkhir <> "" Then
                    idDetailTransaksiGigiAkhir = Strings.Left(idDetailTransaksiGigiAkhir, idDetailTransaksiGigiAkhir.Length - 1)
                    WriteSQL("UPDATE TransaksiGigiBayar SET NoInvoicePembayaran = '" & nomorInvoice & "', RekapBy = '" & userInput & "', RekapDate = GETDATE() WHERE TransGigiIDBayar IN (" & idDetailTransaksiGigiAkhir & ")", 4)
                End If

                ' Update Flag Tabel acctranstagihandetail (Online)
                If nomorTransaksiPoli.Length > 0 Then
                    nomorTransaksiPoli = Strings.Left(nomorTransaksiPoli, nomorTransaksiPoli.Length - 1)
                    WriteThemysql("UPDATE acctranstagihandetail SET lockeddate = NOW(), lockedby='" & userInput & "', RekapID='" & rekapID & "' WHERE no_transaksi IN (" & nomorTransaksiPoli & ") AND loc = '" & kodeLokasiKlinik & "' AND kartu = '" & splitPerusahaan(0).ToString.Trim.ToUpper() & "' AND mesin = '" & splitPerusahaan(1).ToString.Trim.ToUpper() & "'", DB_Keuangan)
                ElseIf nomorTransaksiGigi.Length > 0 Then
                    nomorTransaksiGigi = Strings.Left(nomorTransaksiGigi, nomorTransaksiGigi.Length - 1)
                    WriteThemysql("UPDATE acctranstagihandetail SET lockeddate = NOW(), lockedby='" & userInput & "', RekapID='" & rekapID & "' WHERE no_transaksi IN (" & nomorTransaksiGigi & ") AND loc = '" & kodeLokasiKlinik & "' AND kartu = '" & splitPerusahaan(0).ToString.Trim.ToUpper() & "' AND mesin = '" & splitPerusahaan(1).ToString.Trim.ToUpper() & "'", DB_Keuangan)
                End If

                ' Insert Tabel RekapTagihan (Lokal)
                WriteSQL("INSERT INTO RekapTagihan (RekapID, company, tgltagihan, periode, duedate, totaltagihan, keterangan, createdate, createby, notagihancompany, jenistransaksi, JenisPoli, CoaBank, NamaBank, NamaRekeningBank, NomorRekeningBank, CabangRekeningBank, JenisBank) " & _
                         "VALUES('" & rekapID & "', '" & TextBoxNamaPerusahaanRekapTagihan.Text & "', GETDATE(), '0', '" & tglJatuhTempo & "', '" & totalTagihan & "', '-', GETDATE() , '" & userInput & "', '" & nomorInvoice & "', " & sqlJenisTransaksi & ", " & sqlJenisPoli & ", '" & rowBankTransfer("CoaBank") & "', '" & rowBankTransfer("NamaBank") & "', '" & rowBankTransfer("NamaRekening") & "', '" & rowBankTransfer("NomorRekening") & "', '" & rowBankTransfer("CabangRekening") & "', '" & rowBankTransfer("LabelBank") & "')", 4)

                ' Create Jurnal. Ditangkap tersendiri: pada titik ini invoice + seluruh flag SUDAH
                ' tersimpan, jadi kegagalan jurnal tidak boleh membatalkan pesan sukses. Kalau
                ' errornya dibiarkan naik ke Catch di ButtonSimpanRekapTagihan, petugas hanya melihat
                ' "Terjadi kesalahan", mengira invoice gagal dibuat, lalu mengulang — dan invoicenya
                ' jadi dobel. Itu jauh lebih merusak daripada jurnal yang tertunda.
                Dim pesanJurnal As String = ""
                If autoJurnal.Trim = "Yes" Then
                    Try
                        Dim nomorJurnal As String = SendingJurnal(nomorInvoice)
                        If nomorJurnal <> "" Then pesanJurnal = vbCrLf & "Jurnal : " & nomorJurnal
                    Catch exJurnal As Exception
                        pesanJurnal = vbCrLf & vbCrLf & "PERHATIAN: invoice tersimpan tapi JURNALNYA GAGAL dibuat." & vbCrLf & _
                                      exJurnal.Message & vbCrLf & _
                                      "Perbaiki penyebabnya lalu pakai tombol 'Kirim Ulang Jurnal' di tab Rekap Invoice." & vbCrLf & _
                                      "JANGAN membuat invoice baru untuk transaksi yang sama."
                    End Try
                End If

                ' Cetak Invoice
                CetakInvoiceTagihan(nomorInvoice, True)

                SetClearRekapTagihan()
                MsgBox("Berhasil Menyimpan Data dengan No. Invoice : " & nomorInvoice & pesanJurnal, MsgBoxStyle.Information, "Success!")
            Else
                MessageBox.Show("Tidak ada data yang dipilih.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Tidak ada data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub CetakInvoiceTagihan(ByVal nomorInvoice As String, ByVal simpanLaporanPDF As Boolean)
        Dim Ttempat As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName='Kota'")
        Dim Temail As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName='InvoiceEmail'")
        Dim Tnamaklinik As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName='InvoiceTempat_Klinik'")

        Dim queryBuilder As New StringBuilder()

        ' WITH clause
        queryBuilder.AppendLine("WITH BaseData AS (")
        queryBuilder.AppendLine("    SELECT c.NoJenisBayar AS NamaPerusahaan,")
        queryBuilder.AppendLine("           a.NoUrut AS NoTransaksi,")
        queryBuilder.AppendLine("           CONVERT(DATE, a.TglPoli) AS TglTransaksi,")
        queryBuilder.AppendLine("           c.JumlahBayar - c.Kembali AS Tagihan")
        queryBuilder.AppendLine("    FROM TransaksiPoli a")
        queryBuilder.AppendLine("         LEFT JOIN TransaksiPoliBayar c ON a.TransPoliID = c.TransPoliID")
        queryBuilder.AppendLine("    WHERE c.NoInvoicePembayaran = '" & nomorInvoice & "'")
        queryBuilder.AppendLine("          AND a.deleted <> 1")
        queryBuilder.AppendLine("          AND c.JenisBayar = 'TAGIHAN'")
        queryBuilder.AppendLine("    UNION ALL")
        queryBuilder.AppendLine("    SELECT c.NoJenisBayar,")
        queryBuilder.AppendLine("           a.NoUrut AS NoTransaksi,")
        queryBuilder.AppendLine("           CONVERT(DATE, a.TglPoli) AS TglTransaksi,")
        queryBuilder.AppendLine("           c.JumlahBayar - c.Kembali AS Tagihan")
        queryBuilder.AppendLine("    FROM TransaksiPoli a")
        queryBuilder.AppendLine("         LEFT JOIN TransaksiPoliBayarAwal c ON a.TransPoliID = c.TransPoliID")
        queryBuilder.AppendLine("    WHERE c.NoInvoicePembayaran = '" & nomorInvoice & "'")
        queryBuilder.AppendLine("          AND a.deleted <> 1")
        queryBuilder.AppendLine("          AND c.JenisBayar = 'TAGIHAN'")
        queryBuilder.AppendLine("    UNION ALL")
        queryBuilder.AppendLine("    SELECT c.NoJenisBayar AS NamaPerusahaan,")
        queryBuilder.AppendLine("           a.NoUrut AS NoTransaksi,")
        queryBuilder.AppendLine("           CONVERT(DATE, a.TglGigi) AS TglTransaksi,")
        queryBuilder.AppendLine("           c.JumlahBayar - c.Kembali AS Tagihan")
        queryBuilder.AppendLine("    FROM TransaksiGigi a")
        queryBuilder.AppendLine("         LEFT JOIN TransaksiGigiBayar c ON a.TransGigiID = c.TransGigiID")
        queryBuilder.AppendLine("    WHERE c.NoInvoicePembayaran = '" & nomorInvoice & "'")
        queryBuilder.AppendLine("          AND a.deleted <> 1")
        queryBuilder.AppendLine("          AND c.JenisBayar = 'TAGIHAN'")
        queryBuilder.AppendLine("    UNION ALL")
        queryBuilder.AppendLine("    SELECT c.NoJenisBayar,")
        queryBuilder.AppendLine("           a.NoUrut AS NoTransaksi,")
        queryBuilder.AppendLine("           CONVERT(DATE, a.TglGigi) AS TglTransaksi,")
        queryBuilder.AppendLine("           c.JumlahBayar - c.Kembali AS Tagihan")
        queryBuilder.AppendLine("    FROM TransaksiGigi a")
        queryBuilder.AppendLine("         LEFT JOIN TransaksiGigiBayarAwal c ON a.TransGigiID = c.TransGigiID")
        queryBuilder.AppendLine("    WHERE c.NoInvoicePembayaran = '" & nomorInvoice & "'")
        queryBuilder.AppendLine("          AND a.deleted <> 1")
        queryBuilder.AppendLine("          AND c.JenisBayar = 'TAGIHAN'")
        queryBuilder.AppendLine(")")

        ' Main SELECT statement
        queryBuilder.AppendLine("SELECT ROW_NUMBER() OVER (ORDER BY NamaPerusahaan, TglTransaksi) AS NomorUrut,")
        queryBuilder.AppendLine("       NamaPerusahaan,")
        queryBuilder.AppendLine("       TglTransaksi,")
        queryBuilder.AppendLine("       COUNT(DISTINCT NoTransaksi) AS JumlahPasien,")
        queryBuilder.AppendLine("       'Sesuai Rekapan' AS Keterangan,")
        queryBuilder.AppendLine("       SUM(Tagihan) AS JumlahTagihan")
        queryBuilder.AppendLine("FROM BaseData")
        queryBuilder.AppendLine("GROUP BY NamaPerusahaan, TglTransaksi")
        queryBuilder.AppendLine("ORDER BY NamaPerusahaan, TglTransaksi ASC;")

        Dim tblDetailTransaksi As DataTable = ReadSQL(queryBuilder.ToString())

        Dim tblHeaderLaporan As DataTable = New DataTable
        tblHeaderLaporan = CreateTableInvoice()
        tblHeaderLaporan.Rows.Clear()

        Dim tblDataHeader As DataTable = ReadSQL("SELECT * FROM RekapTagihan WHERE notagihancompany = '" & nomorInvoice & "'")
        For Each rowHeader As DataRow In tblDataHeader.Rows
            Dim newRowHeader As DataRow = tblHeaderLaporan.NewRow
            Dim splitPerusahaan() As String = rowHeader("company").ToString().Split("-")

            newRowHeader("TglInvoice") = Format(rowHeader("tgltagihan"), "dd") & " " & BulanIndonesia(Format(rowHeader("tgltagihan"), "MM")) & " " & Format(rowHeader("tgltagihan"), "yyyy")
            newRowHeader("TglJatuhTempo") = Format(rowHeader("duedate"), "dd") & " " & BulanIndonesia(Format(rowHeader("duedate"), "MM")) & " " & Format(rowHeader("duedate"), "yyyy")
            newRowHeader("NomorInvoice") = rowHeader("notagihancompany").ToString()
            newRowHeader("NamaPerusahaan") = splitPerusahaan(0).ToString.Trim.ToUpper()
            newRowHeader("TglTTD") = Ttempat & ", " & Format(DateAndTime.Now, "dd") & " " & BulanIndonesia(Format(DateAndTime.Now, "MM")) & " " & Format(DateAndTime.Now, "yyyy")
            'newRowHeader("KeteranganNominal") = "Terbilang : " & Terbilang(rowHeader("totaltagihan").ToString()) & " Rupiah"
            newRowHeader("KeteranganNominal") = "Terbilang : " & Terbilang(CLng(rowHeader("totaltagihan"))) & " Rupiah"

            ' Get Nama User TTD
            Dim userTTD As String = ReadSQLNIK("SELECT UserNama FROM Users WHERE UserID = '" & rowHeader("createby").ToString() & "'")

            newRowHeader("UserTTD") = userTTD
            newRowHeader("KeteranganEmail") = "3. Bukti Pembayaran dapat dikirim melalui email " & Temail

            ' Set Data Bank
            newRowHeader("NamaRekeningBank") = rowHeader("NamaRekeningBank").ToString()
            newRowHeader("NomorRekeningBank") = rowHeader("NomorRekeningBank").ToString()
            If rowHeader("CabangRekeningBank").ToString() <> "-" Then
                newRowHeader("NamaBank") = rowHeader("JenisBank").ToString() & " - " & "CABANG " & rowHeader("CabangRekeningBank").ToString()
            Else
                newRowHeader("NamaBank") = rowHeader("JenisBank").ToString()
            End If

            tblHeaderLaporan.Rows.Add(newRowHeader)
        Next

        tblHeaderLaporan.TableName = "HeaderLaporan"
        tblDetailTransaksi.TableName = "DetailTransaksi"

        Dim dataSet As New DataSet()
        dataSet.Tables.Add(tblHeaderLaporan.Copy())
        dataSet.Tables.Add(tblDetailTransaksi.Copy())

        Report = XtraReport.FromFile(Application.StartupPath + "\reports\Klinik\Laporan\LaporanRekapInvoiceKlinik.repx", True)

        Report.DataSource = dataSet
        Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelJudulLaporan").Text = "INVOICE"
        Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelKalimatPembuka").Text = "Dengan hormat," & vbCrLf & _
                                                                                          "Kami mengucapkan terimakasih atas kepercayaan yang telah diberikan kepada " & Tnamaklinik & ". Bersama surat ini kami menginformasikan tagihan Pemeriksaan Klinik dengan rincian sebagai berikut:"
        Report.Bands.Item(BandKind.PageFooter).Controls("xrLabelInfoCetak").Text = Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") & " By " & userInput

        'Report.RunDesigner()
        Report.ShowPreview()
        'Report.Print()

        If simpanLaporanPDF = True Then
            ' SavePDF
            Dim NameFile As String = nomorInvoice.Replace("/", "-")
            Dim PathFolder As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName='PathAutoSaveInvoice'")
            PathFolder = PathFolder & "INVOICE_POLI" & "\" & Format(DateAndTime.Now, "yyyy") & "\" & Format(DateAndTime.Now, "MMMM") & "\" & Format(DateAndTime.Now, "dd") & ""

            If (Not System.IO.Directory.Exists(PathFolder)) Then
                System.IO.Directory.CreateDirectory(PathFolder)
            End If

            Dim filepath As String = PathFolder & "\" & NameFile & ".pdf"
            If File.Exists(filepath) Then
                My.Computer.FileSystem.DeleteFile(filepath)
            End If
            Report.CreatePdfDocument(filepath)

            ' CreateBase64
            'Dim Base64Inv As String = ""
            'Base64Inv = Convert.ToBase64String(System.IO.File.ReadAllBytes(filepath))

            ' Update Base64
            'WriteThemysql("UPDATE acctransrekaptagihan a SET a.FileBase64 = '" & Base64Inv & "' WHERE a.notagihancompany = '" & NoTagihan & "'", DB_Keuangan)

            MsgBox("Invoice Otomatis Tersimpan di drive : " & PathFolder & "", MsgBoxStyle.Information, "Success!")
        End If
    End Sub

    Private Sub ButtonFindPerusahaanRekapTagihan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFindPerusahaanRekapTagihan.Click
        Find.Show()
        Find.Query("SELECT NoCompany AS 'ID', CompanyCode AS 'Kode Perusahaan', CompanyNama AS 'Keterangan' FROM mastercompany WHERE GETDATE() BETWEEN StartDate AND EndDate", " ORDER BY NoCompany ASC", "FindPerusahaanReportPendapatanKlinik_RekapTagihan", 2)
    End Sub

    ' Melempar bila jurnal TIDAK terbentuk (dipulihkan otomatis lewat KirimJurnalAman bila hanya
    ' koneksi yang putus). Pemanggil WAJIB menangkapnya dan memberi tahu bahwa datanya sendiri sudah
    ' tersimpan — kalau tidak, petugas mengira penyimpanan gagal lalu mengulang dan invoice jadi dobel.
    Private Function SendingJurnal(ByVal nomorInvoice As String) As String
        ' Set Data Rekap Tagihan
        Dim tblInvoice As DataTable = ReadSQL("SELECT * FROM RekapTagihan WHERE notagihancompany = '" & nomorInvoice & "'")
        If tblInvoice.Rows.Count = 0 Then
            Throw New Exception("Invoice " & nomorInvoice & " tidak ditemukan di tabel RekapTagihan, jurnal tidak dibuat.")
        End If

        Dim rowInvoice As DataRow = tblInvoice.Rows(0)

        ' Jurnal piutang satu invoice hanya boleh SEKALI. Tombol "Kirim Ulang Jurnal" memanggil sub ini
        ' lagi dengan refferenceID BARU, jadi tanpa penjagaan ini dua klik = dua jurnal piutang untuk
        ' invoice yang sama. Flag di bawah baru ditulis SESUDAH jurnal benar-benar terbentuk, jadi flag
        ' terisi = jurnalnya memang sudah ada. Dibaca dari DB lokal (bukan server keuangan) karena
        ' ReadThemysqlNIK mengembalikan "-" saat koneksi gagal — tidak bisa dibedakan dari "belum
        ' pernah dijurnal", dan salah tebak di situ berarti jurnal dobel.
        ' SISA RISIKO: kalau jurnal terbentuk tapi UPDATE flag gagal (WriteSQL menelan error), flag
        ' tetap NULL dan kiriman ulang masih bisa menduplikasi. Perlu WriteSqlChecked seperti di
        ' FormUploadDataOmset untuk menutupnya.
        Dim flagJurnalLama As String = ReadSQLNIK("SELECT TOP 1 FlagJurnalCreateTagihan FROM (" & _
            "SELECT FlagJurnalCreateTagihan FROM TransaksiPoliBayar WHERE NoInvoicePembayaran = '" & nomorInvoice & "' AND JenisBayar = 'TAGIHAN' " & _
            "UNION ALL SELECT FlagJurnalCreateTagihan FROM TransaksiPoliBayarAwal WHERE NoInvoicePembayaran = '" & nomorInvoice & "' AND JenisBayar = 'TAGIHAN' " & _
            "UNION ALL SELECT FlagJurnalCreateTagihan FROM TransaksiGigiBayar WHERE NoInvoicePembayaran = '" & nomorInvoice & "' AND JenisBayar = 'TAGIHAN' " & _
            "UNION ALL SELECT FlagJurnalCreateTagihan FROM TransaksiGigiBayarAwal WHERE NoInvoicePembayaran = '" & nomorInvoice & "' AND JenisBayar = 'TAGIHAN') f " & _
            "WHERE FlagJurnalCreateTagihan IS NOT NULL")

        If flagJurnalLama <> "" AndAlso flagJurnalLama <> "-" Then
            MsgBox("Invoice " & nomorInvoice & " SUDAH pernah dijurnal (ref " & flagJurnalLama & ")." & vbCrLf & vbCrLf & _
                   "Pengiriman dibatalkan supaya tidak terbentuk jurnal piutang kedua untuk invoice yang sama." & vbCrLf & _
                   "Kalau jurnalnya memang tidak ada di buku besar, hapus invoice ini lalu buat ulang.", _
                   MsgBoxStyle.Exclamation, "Jurnal Sudah Ada")

            Return ""
        End If

        ' Set Variabel Jurnal
        Dim idLokasiJurnal As String = ""
        Dim keteranganLokasiJurnal As String = ""
        Dim coaPiutang As String = ""
        Dim coaPiutangPending As String = ""

        ' Set COA Piutang & Piutang Pending
        If IsDBNull(rowInvoice("JenisPoli")) = False Then ' Jika Kolom Jenis Poli Tidak Kosong (Jurnal Berdasarkan Jenis Poli)
            Dim tblMasterJurnal As DataTable = ReadThemysql("SELECT * FROM MasterLokasiJurnal a WHERE a.KodeLokasi = '" & kodeLokasiKlinik & "' AND a.KeteranganPoli = '" & rowInvoice("JenisPoli") & "' LIMIT 1", DB_Omset)
            ' Poli belum terdaftar di MasterLokasiJurnal (atau koneksi ke server omset gagal — kedua
            ' hal itu sama-sama menghasilkan tabel kosong). Dulu langsung Rows(0) dan form crash.
            If tblMasterJurnal.Rows.Count = 0 Then
                Throw New Exception("MasterLokasiJurnal untuk poli '" & rowInvoice("JenisPoli").ToString() & "' di lokasi " & kodeLokasiKlinik & " tidak ditemukan (atau server omset tidak terjangkau), jurnal tidak dibuat.")
            End If

            Dim rowDataJurnal As DataRow = tblMasterJurnal.Rows(0)
            idLokasiJurnal = rowDataJurnal("KodeLokasiJurnal")
            keteranganLokasiJurnal = rowDataJurnal("KeteranganLokasi") & " (" & rowDataJurnal("KeteranganPoli") & ") "
            coaPiutang = rowDataJurnal("KodeCoaPiutang")
            coaPiutangPending = rowDataJurnal("KodeCoaPiutangPending")
        Else ' Jika Kolom Jenis Poli Kosong (Jurnal Berdasarkan Jenis Transaksi)
            Dim tblMasterJurnal As DataTable = ReadThemysql("SELECT * FROM MasterLokasiJurnal a WHERE a.KodeLokasi = '" & kodeLokasiKlinik & "' AND a.JenisTransaksi = '" & rowInvoice("jenistransaksi") & "' LIMIT 1", DB_Omset)
            If tblMasterJurnal.Rows.Count = 0 Then
                Throw New Exception("MasterLokasiJurnal untuk jenis transaksi '" & rowInvoice("jenistransaksi").ToString() & "' di lokasi " & kodeLokasiKlinik & " tidak ditemukan (atau server omset tidak terjangkau), jurnal tidak dibuat.")
            End If

            Dim rowDataJurnal As DataRow = tblMasterJurnal.Rows(0)
            idLokasiJurnal = rowDataJurnal("KodeLokasiJurnal")
            keteranganLokasiJurnal = rowDataJurnal("KeteranganLokasi") & " (" & rowDataJurnal("JenisTransaksi") & ") "
            If rowInvoice("jenistransaksi").ToString() = "POLI" Then
                coaPiutang = ReadThemysqlNIK("SELECT KodeAkun FROM MasterCoaJurnal WHERE NamaAkun = 'Piutang Poli Umum'", DB_Omset)
                coaPiutangPending = ReadThemysqlNIK("SELECT KodeAkun FROM MasterCoaJurnal WHERE NamaAkun = 'Piutang Pending Poli Umum'", DB_Omset)
            ElseIf rowInvoice("jenistransaksi").ToString() = "GIGI" Then
                coaPiutang = ReadThemysqlNIK("SELECT KodeAkun FROM MasterCoaJurnal WHERE NamaAkun = 'Piutang Poli Gigi'", DB_Omset)
                coaPiutangPending = ReadThemysqlNIK("SELECT KodeAkun FROM MasterCoaJurnal WHERE NamaAkun = 'Piutang Pending Poli Gigi'", DB_Omset)
            End If
        End If

        ' Set Flag / Refference
        Dim refferenceID As String = GetUUID()

        ' Set Nama Perusahaan
        Dim splitPerusahaan() As String = rowInvoice("company").ToString().Split("-")

        ' Set Keterangan Jurnal
        Dim keteranganJurnal As String = "TERIMA - TAGIHAN " & splitPerusahaan(0).ToString.Trim.ToUpper() & " " & keteranganLokasiJurnal & " " & Format(rowInvoice("tgltagihan"), "dd/MM/yyyy") & " (NO. INVOICE : " & nomorInvoice & ")"

        ' Akun WAJIB sah. Lookup COA yang gagal mengembalikan "-" (ReadThemysqlNIK) atau kolom master
        ' yang belum diisi mengembalikan kosong — keduanya akan membentuk jurnal ke akun yang tidak
        ' ada di bagan akun. Lebih baik tertahan di sini dan dikirim ulang sesudah master dilengkapi.
        If coaPiutang.Trim() = "" OrElse coaPiutang.Trim() = "-" OrElse coaPiutangPending.Trim() = "" OrElse coaPiutangPending.Trim() = "-" Then
            Throw New Exception("COA piutang belum lengkap (D '" & coaPiutangPending & "' / K '" & coaPiutang & "'), jurnal tidak dibuat. Lengkapi MasterLokasiJurnal untuk divisi ini.")
        End If

        ' Kirim Jurnal Create Tagihan. Melempar bila server jurnal menolak (mis. periode sudah ditutup)
        ' — dulu lewat WriteThemysql sehingga penolakan itu tidak terlihat sama sekali.
        Dim nomorJurnal As String = KirimJurnalAman("SELECT F_MakeUMH('" & refferenceID & "', 'out', '', '" & Format(rowInvoice("tgltagihan"), "yyyy-MM-dd") & "', '" & CoaPiutangPending & "', '" & CoaPiutang & "', 'IDR', 1, " & CDbl(rowInvoice("totaltagihan")) & ", '" & keteranganJurnal.ToUpper() & "', '1', 1, '', '" & keteranganJurnal.ToUpper() & "', '', '', '" & idLokasiJurnal & "', 4, " & CDbl(rowInvoice("totaltagihan")) & ", 'K')", refferenceID)

        ' Update Flag di Tabel Bayar
        WriteSQL("UPDATE TransaksiPoliBayar SET FlagJurnalCreateTagihan = '" & refferenceID & "' WHERE NoInvoicePembayaran = '" & nomorInvoice & "' AND JenisBayar = 'TAGIHAN'", 4)
        WriteSQL("UPDATE TransaksiPoliBayarAwal SET FlagJurnalCreateTagihan = '" & refferenceID & "' WHERE NoInvoicePembayaran = '" & nomorInvoice & "' AND JenisBayar = 'TAGIHAN'", 4)
        WriteSQL("UPDATE TransaksiGigiBayar SET FlagJurnalCreateTagihan = '" & refferenceID & "' WHERE NoInvoicePembayaran = '" & nomorInvoice & "' AND JenisBayar = 'TAGIHAN'", 4)
        WriteSQL("UPDATE TransaksiGigiBayarAwal SET FlagJurnalCreateTagihan = '" & refferenceID & "' WHERE NoInvoicePembayaran = '" & nomorInvoice & "' AND JenisBayar = 'TAGIHAN'", 4)

        ' Update Tabel Rekap Tagihan (Online)
        WriteThemysql("UPDATE acctransrekaptagihan SET FlagJurnalCreateTagihan = '" & refferenceID & "' WHERE notagihancompany = '" & nomorInvoice & "'", DB_Keuangan)

        ' Insert Log Invoice Tagihan
        WriteSQL("INSERT INTO LogJurnalKeuangan (JurnalFlag, JurnalDate, JurnalBy, Status, JenisPoli, JenisTransaksi) VALUES ('" & refferenceID & "', GETDATE(), '" & userInput & "', '" & keteranganJurnal & "', '" & rowInvoice("JenisPoli").ToString & "', '" & rowInvoice("jenistransaksi").ToString() & "')", 4)

        Return nomorJurnal
    End Function

    Private Sub ButtonCariDataInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCariDataInvoice.Click
        ' Cek Koneksi
        If CekKoneksi("SHOW TABLES FROM " & DB_Keuangan.Tdb & "", DB_Keuangan) = False Then
            MsgBox("Proses gagal, cek koneksi internet atau server Database " & DB_Keuangan.Tdb & ".", MsgBoxStyle.Exclamation, "Warning!")
            Exit Sub
        End If

        Dim kodeInvoice As String = ReadSQLNIK("SELECT StrValue  FROM MsConfig WHERE ConfigName = 'KodeInvoicePoli'")
        Dim queryBuilder As New StringBuilder()

        queryBuilder.AppendLine("SELECT RekapID, company, notagihancompany AS 'No Tagihan',")
        queryBuilder.AppendLine("       tgltagihan AS 'Tgl Tagihan', totaltagihan,")
        queryBuilder.AppendLine("       duedate AS 'Tgl Jatuh Tempo',")
        queryBuilder.AppendLine("       CASE WHEN lunasid > 0 THEN 'LUNAS' ELSE '-' END AS 'Status'")
        queryBuilder.AppendLine("FROM acctransrekaptagihan")
        queryBuilder.AppendLine("WHERE notagihancompany LIKE '%" & kodeInvoice & "%'")
        queryBuilder.AppendLine("      AND cabang = '" & kodeLokasiKlinik & "'")
        queryBuilder.AppendLine("      AND (DATE(tgltagihan) BETWEEN '" & Format(TglAwalTransaksiRekapInvoice.Value, "yyyy-MM-dd") & "' AND '" & Format(TglAkhirTransaksiRekapInvoice.Value, "yyyy-MM-dd") & "')")
        queryBuilder.AppendLine("      AND deleted IS NULL")

        Dim queryString As String = queryBuilder.ToString()

        ' Set Nama Perusahaan
        If TextBoxNamaPerusahaanRekapInvoice.Text <> "" Then
            queryString = queryString & " AND (company = '" & TextBoxNamaPerusahaanRekapInvoice.Text & "')"
        End If

        Find.Show()
        Find.QueryTheMysql(queryString, " ORDER BY tgltagihan, RekapID ASC", "FindInvoiceKlinik_ReportPendapatanKlinik", 2, Find_Keuangan)
    End Sub

    Public Sub LoadInvoiceTagihan(ByRef NomorInvoice As String)
        ' Set Data Header
        SetHeaderRekapInvoice(NomorInvoice)

        Dim tblPoliUmum As DataTable = GetDataRekapTagihanPoliUmum(NomorInvoice)
        Dim tblPoliGigi As DataTable = GetDataRekapTagihanPoliGigi(NomorInvoice)

        ' Buat tabel gabungan dengan struktur yang sama
        Dim tblRekapTagihan As DataTable = tblPoliUmum.Clone()

        ' Gabungkan data dari kedua tabel
        tblRekapTagihan.Merge(tblPoliUmum)
        tblRekapTagihan.Merge(tblPoliGigi)

        If tblRekapTagihan.Rows.Count > 0 Then
            Dim rowTable As DataTable = CreateTableLaporanPendapatan()
            rowTable.Rows.Clear()

            For Each rowData As DataRow In tblRekapTagihan.Rows
                Dim newRow As DataRow = rowTable.NewRow
                newRow("No") = rowData("NomorUrutTransaksi")
                newRow("No Transaksi") = rowData("No Transaksi")
                newRow("Tgl Transaksi") = rowData("Tgl Transaksi")
                newRow("Nama") = rowData("Nama").ToString.ToUpper
                newRow("Tgl Lahir") = rowData("Tgl Lahir")
                newRow("pemeriksaan") = rowData("DataListTindakan")
                newRow("Tagihan") = rowData("TotalBayar")
                newRow("JenisTransaksi") = rowData("JenisTransaksi")
                newRow("Jenis Poli") = rowData("JenisPoli")
                newRow("Jenis Bayar") = rowData("JenisBayar")
                newRow("NoJenisBayar") = KeteranganJenisBayar(rowData)
                newRow("Detail Pembayaran") = KeteranganDetailPembayaran(rowData)
                rowTable.Rows.Add(newRow)
            Next

            GridControlRekapInvoice.DataSource = rowTable

            Dim view As DevExpress.XtraGrid.Views.Grid.GridView = GridViewRekapInvoice

            view.BeginUpdate()

            view.OptionsView.RowAutoHeight = True
            view.OptionsView.ShowAutoFilterRow = False
            view.OptionsView.ColumnAutoWidth = False

            view.Columns("pemeriksaan").AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
            view.Columns("pemeriksaan").AppearanceCell.Options.UseTextOptions = True

            view.Columns("pemeriksaan").ColumnEdit = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()

            view.EndUpdate()

            view.BestFitColumns()
        Else
            MessageBox.Show("Tidak Ada Data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub SetHeaderRekapInvoice(ByRef NomorInvoice As String)
        Dim tblDataHeader As DataTable = ReadSQL("SELECT * FROM RekapTagihan WHERE notagihancompany = '" & NomorInvoice & "'")

        If tblDataHeader.Rows.Count > 0 Then
            Dim rowHeader As DataRow = tblDataHeader.Rows(0)

            Dim splitPerusahaan() As String = rowHeader("company").ToString().Split("-")

            TextBoxNamaPerusahaanInvoice.Text = splitPerusahaan(0).ToString.Trim.ToUpper()
            TextBoxNomorInvoice.Text = rowHeader("notagihancompany").ToString()
            TextBoxBankInvoice.Text = rowHeader("NamaBank").ToString() & " – " & rowHeader("NomorRekeningBank").ToString() & " – a.n. " & rowHeader("NamaRekeningBank").ToString()
            TextBoxNominalInvoice.Text = "" & Format(Val(rowHeader("totaltagihan")), "Rp, ###,###.#0") & ""
            TglInvoice.Value = rowHeader("tgltagihan")
            TglJatuhTempoInvoice.Value = rowHeader("duedate")
        End If
    End Sub

    Function GetDataRekapTagihanPoliUmum(ByRef NomorInvoice As String) As DataTable

        ' Set Tabel Result
        Dim tblResult As New DataTable

        Dim queryBuilder As New StringBuilder()

        queryBuilder.AppendLine("DECLARE @NoInvoicePembayaran NVARCHAR(50) = '" & NomorInvoice & "'")

        queryBuilder.AppendLine("SELECT ROW_NUMBER() OVER (PARTITION BY tblDetail.JenisPoli, tblBayar.JenisBayar, tblBayar.CompanyTagihan, tblBayar.mesin ")
        queryBuilder.AppendLine("ORDER BY tblPoli.TglPoli, tblPoli.NoUrut) AS NomorUrutTransaksi, ")

        queryBuilder.AppendLine("tblBayar.TransPoliIDBayar AS 'ID', tblPoli.NoUrut AS 'No Transaksi', ")
        queryBuilder.AppendLine("CONVERT(DATE, tblPoli.TglPoli) AS 'Tgl Transaksi', ")
        queryBuilder.AppendLine("tblDetail.JenisPoli, tblBayar.DataListTindakan, ")

        queryBuilder.AppendLine("tblPasien.Nama, CONVERT(DATE, tblPasien.TglLahir) AS 'Tgl Lahir', ")
        queryBuilder.AppendLine("tblPasien.NoRekamMedis, ")

        queryBuilder.AppendLine("tblBayar.JenisBayar, tblBayar.CompanyTagihan, ")
        queryBuilder.AppendLine("tblBayar.kartu AS 'JenisKartu', tblBayar.mesin AS 'JenisMesin', ")

        queryBuilder.AppendLine("tblDetail.JenisTransaksi, ")
        queryBuilder.AppendLine("(tblBayar.JumlahBayar - tblBayar.Kembali) AS TotalBayar, ")

        queryBuilder.AppendLine("tblPoli.RekapID, tblBayar.NoInvoicePembayaran, tblBayar.JenisKasir ")

        queryBuilder.AppendLine("FROM ( ")

        ' =========================
        ' TRANSAKSI BAYAR
        ' =========================
        queryBuilder.AppendLine("    SELECT tb.TransPoliIDBayar, tb.TransPoliID, ")

        queryBuilder.AppendLine("    STUFF(( ")
        queryBuilder.AppendLine("        SELECT CHAR(13) + CHAR(10) + ")
        queryBuilder.AppendLine("               d.NamaTindakan + ' (' + CAST(d.Qty AS VARCHAR) + 'x) ' ")
        queryBuilder.AppendLine("        FROM ( ")
        queryBuilder.AppendLine("            SELECT tblTindakan.NamaTindakan, COUNT(*) AS Qty ")
        queryBuilder.AppendLine("            FROM TransaksiPoliDetail tblTindakan ")
        queryBuilder.AppendLine("            WHERE tblTindakan.Awal = '0' ")
        queryBuilder.AppendLine("              AND tblTindakan.TransPoliID = tb.TransPoliID ")
        queryBuilder.AppendLine("            GROUP BY tblTindakan.NamaTindakan ")
        queryBuilder.AppendLine("        ) d ")
        queryBuilder.AppendLine("        FOR XML PATH(''), TYPE ")
        queryBuilder.AppendLine("    ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS DataListTindakan, ")

        queryBuilder.AppendLine("    tb.JenisBayar, ")
        queryBuilder.AppendLine("    CASE WHEN tb.JenisBayar = 'TAGIHAN' THEN tb.NoJenisBayar ELSE '' END AS CompanyTagihan, ")
        queryBuilder.AppendLine("    tb.JumlahBayar, tb.Kembali, tb.kartu, tb.mesin, tb.NoInvoicePembayaran, ")
        queryBuilder.AppendLine("    'Transaksi Tindakan' AS JenisKasir ")

        queryBuilder.AppendLine("    FROM TransaksiPoliBayar tb ")
        queryBuilder.AppendLine("    WHERE tb.JumlahBayar > 0 ")
        queryBuilder.AppendLine("      AND tb.NoInvoicePembayaran = @NoInvoicePembayaran ")
        ' Hanya baris TAGIHAN yang benar-benar ditagihkan lewat form ini. Kolom NoInvoicePembayaran
        ' ternyata juga dipakai proses lain: terukur 5 baris DEBET/CARD (Rp 4.950.000) membawa nomor
        ' invoice tanpa RekapBy/RekapDate. Tanpa saringan ini grid menampilkan Rp 6.375.000 untuk
        ' invoice 0133/MMC-HBT/I/2025 yang headernya Rp 1.425.000 — cetakannya sudah menyaring, jadi
        ' grid dan kertas berbeda angka.
        queryBuilder.AppendLine("      AND tb.JenisBayar = 'TAGIHAN' ")

        queryBuilder.AppendLine("    UNION ALL ")

        ' =========================
        ' TRANSAKSI AWAL
        ' =========================
        queryBuilder.AppendLine("    SELECT tba.TransPoliIDBayar, tba.TransPoliID, ")

        queryBuilder.AppendLine("    STUFF(( ")
        queryBuilder.AppendLine("        SELECT CHAR(13) + CHAR(10) + ")
        queryBuilder.AppendLine("               d.NamaTindakan + ' (' + CAST(d.Qty AS VARCHAR) + 'x) ' ")
        queryBuilder.AppendLine("        FROM ( ")
        queryBuilder.AppendLine("            SELECT tblTindakan.NamaTindakan, COUNT(*) AS Qty ")
        queryBuilder.AppendLine("            FROM TransaksiPoliDetail tblTindakan ")
        queryBuilder.AppendLine("            WHERE tblTindakan.Awal = '1' ")
        queryBuilder.AppendLine("              AND tblTindakan.TransPoliID = tba.TransPoliID ")
        queryBuilder.AppendLine("            GROUP BY tblTindakan.NamaTindakan ")
        queryBuilder.AppendLine("        ) d ")
        queryBuilder.AppendLine("        FOR XML PATH(''), TYPE ")
        queryBuilder.AppendLine("    ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS DataListTindakan, ")

        queryBuilder.AppendLine("    tba.JenisBayar, ")
        queryBuilder.AppendLine("    CASE WHEN tba.JenisBayar = 'TAGIHAN' THEN tba.NoJenisBayar ELSE '' END AS CompanyTagihan, ")
        queryBuilder.AppendLine("    tba.JumlahBayar, tba.Kembali, tba.kartu, tba.mesin, tba.NoInvoicePembayaran, ")
        queryBuilder.AppendLine("    'Transaksi Awal' AS JenisKasir ")

        queryBuilder.AppendLine("    FROM TransaksiPoliBayarAwal tba ")
        queryBuilder.AppendLine("    WHERE tba.JumlahBayar > 0 ")
        queryBuilder.AppendLine("      AND tba.NoInvoicePembayaran = @NoInvoicePembayaran ")
        queryBuilder.AppendLine("      AND tba.JenisBayar = 'TAGIHAN' ")

        queryBuilder.AppendLine(") AS tblBayar ")

        queryBuilder.AppendLine("LEFT JOIN TransaksiPoli tblPoli ON tblBayar.TransPoliID = tblPoli.TransPoliID ")
        queryBuilder.AppendLine("LEFT JOIN TransaksiDetail tblDetail ON tblPoli.NoUrut = tblDetail.NoUrut ")
        queryBuilder.AppendLine("LEFT JOIN MasterPasien tblPasien ON tblPoli.IDInternet = tblPasien.NoRekamMedis ")

        queryBuilder.AppendLine("WHERE tblDetail.DeletedDate IS NULL ")

        queryBuilder.AppendLine("ORDER BY tblBayar.JenisBayar, tblDetail.JenisPoli;")

        tblResult = ReadSQL(queryBuilder.ToString())

        Return tblResult

    End Function

    'OLD
    'Function GetDataRekapTagihanPoliUmum(ByRef NomorInvoice As String) As DataTable
    '    ' Set Tabel Result
    '    Dim tblResult As New DataTable

    '    Dim queryBuilder As New StringBuilder()

    '    queryBuilder.AppendLine("DECLARE @NoInvoicePembayaran NVARCHAR(50) = '" & NomorInvoice & "'") ' Menggunakan parameter
    '    queryBuilder.AppendLine("SELECT ROW_NUMBER() OVER (PARTITION BY tblDetail.JenisPoli, tblBayar.JenisBayar, tblBayar.CompanyTagihan, tblBayar.mesin ")
    '    queryBuilder.AppendLine("ORDER BY tblPoli.TglPoli, tblPoli.NoUrut) AS NomorUrutTransaksi, ")
    '    queryBuilder.AppendLine("tblBayar.TransPoliIDBayar AS 'ID', tblPoli.NoUrut AS 'No Transaksi', ")
    '    queryBuilder.AppendLine("CONVERT(DATE, tblPoli.TglPoli) AS 'Tgl Transaksi', tblDetail.JenisPoli, tblBayar.DataListTindakan, ")
    '    queryBuilder.AppendLine("tblPasien.Nama, CONVERT(DATE, tblPasien.TglLahir) AS 'Tgl Lahir', ")
    '    queryBuilder.AppendLine("tblPasien.NoRekamMedis, tblBayar.JenisBayar, tblBayar.CompanyTagihan, ")
    '    queryBuilder.AppendLine("tblBayar.kartu AS 'JenisKartu', tblBayar.mesin AS 'JenisMesin', ")
    '    queryBuilder.AppendLine("tblDetail.JenisTransaksi, tblBayar.JumlahBayar - tblBayar.Kembali AS TotalBayar, ")
    '    queryBuilder.AppendLine("tblPoli.RekapID, tblBayar.NoInvoicePembayaran, tblBayar.JenisKasir ")
    '    queryBuilder.AppendLine("FROM ( ")
    '    queryBuilder.AppendLine("    SELECT tb.TransPoliIDBayar, tb.TransPoliID, ")
    '    queryBuilder.AppendLine("    STUFF((SELECT ', ' + ISNULL(tblTindakan.NamaTindakan, '') ")
    '    queryBuilder.AppendLine("           FROM TransaksiPoliDetail tblTindakan ")
    '    queryBuilder.AppendLine("           WHERE tblTindakan.Awal = '0' AND tblTindakan.TransPoliID = tb.TransPoliID ")
    '    queryBuilder.AppendLine("           FOR XML PATH (''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS DataListTindakan, ")
    '    queryBuilder.AppendLine("    tb.JenisBayar, CASE WHEN tb.JenisBayar = 'TAGIHAN' THEN tb.NoJenisBayar ELSE '' END AS 'CompanyTagihan', ")
    '    queryBuilder.AppendLine("    tb.JumlahBayar, tb.Kembali, tb.kartu, tb.mesin, tb.NoInvoicePembayaran, 'Transaksi Tindakan' AS JenisKasir ")
    '    queryBuilder.AppendLine("    FROM TransaksiPoliBayar tb ")
    '    queryBuilder.AppendLine("    WHERE tb.JumlahBayar > 0 ")
    '    queryBuilder.AppendLine("    AND tb.NoInvoicePembayaran = @NoInvoicePembayaran ")
    '    queryBuilder.AppendLine("    UNION ALL ")
    '    queryBuilder.AppendLine("    SELECT tba.TransPoliIDBayar, tba.TransPoliID, ")
    '    queryBuilder.AppendLine("    STUFF((SELECT ', ' + ISNULL(tblTindakan.NamaTindakan, '') ")
    '    queryBuilder.AppendLine("           FROM TransaksiPoliDetail tblTindakan ")
    '    queryBuilder.AppendLine("           WHERE tblTindakan.Awal = '1' AND tblTindakan.TransPoliID = tba.TransPoliID ")
    '    queryBuilder.AppendLine("           FOR XML PATH (''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS DataListTindakan, ")
    '    queryBuilder.AppendLine("    tba.JenisBayar, CASE WHEN tba.JenisBayar = 'TAGIHAN' THEN tba.NoJenisBayar ELSE '' END AS 'CompanyTagihan', ")
    '    queryBuilder.AppendLine("    tba.JumlahBayar, tba.Kembali, tba.kartu, tba.mesin, tba.NoInvoicePembayaran, 'Transaksi Awal' AS JenisKasir ")
    '    queryBuilder.AppendLine("    FROM TransaksiPoliBayarAwal tba ")
    '    queryBuilder.AppendLine("    WHERE tba.JumlahBayar > 0 ")
    '    queryBuilder.AppendLine("    AND tba.NoInvoicePembayaran = @NoInvoicePembayaran ")
    '    queryBuilder.AppendLine(") AS tblBayar ")
    '    queryBuilder.AppendLine("LEFT JOIN TransaksiPoli tblPoli ON tblBayar.TransPoliID = tblPoli.TransPoliID ")
    '    queryBuilder.AppendLine("LEFT JOIN TransaksiDetail tblDetail ON tblPoli.NoUrut = tblDetail.NoUrut ")
    '    queryBuilder.AppendLine("LEFT JOIN MasterPasien tblPasien ON tblPoli.IDInternet = tblPasien.NoRekamMedis ")
    '    queryBuilder.AppendLine("WHERE tblDetail.DeletedDate IS NULL ")
    '    queryBuilder.AppendLine("ORDER BY tblBayar.JenisBayar, tblDetail.JenisPoli;")

    '    tblResult = ReadSQL(queryBuilder.ToString())

    '    Return tblResult
    'End Function

    Function GetDataRekapTagihanPoliGigi(ByRef NomorInvoice As String) As DataTable
        ' Set Tabel Result
        Dim tblResult As New DataTable

        Dim queryBuilder As New StringBuilder()

        queryBuilder.AppendLine("DECLARE @NoInvoicePembayaran NVARCHAR(50) = '" & NomorInvoice & "'") ' Menggunakan parameter
        queryBuilder.AppendLine("SELECT ROW_NUMBER() OVER (PARTITION BY tblDetail.JenisPoli, tblBayar.JenisBayar, tblBayar.CompanyTagihan, tblBayar.mesin ")
        queryBuilder.AppendLine("ORDER BY tblGigi.TglGigi, tblGigi.NoUrut) AS NomorUrutTransaksi, ")
        queryBuilder.AppendLine("tblBayar.TransGigiIDBayar AS 'ID', tblGigi.NoUrut AS 'No Transaksi', ")
        queryBuilder.AppendLine("CONVERT(DATE, tblGigi.TglGigi) AS 'Tgl Transaksi', tblDetail.JenisPoli, tblBayar.DataListTindakan, ")
        queryBuilder.AppendLine("tblPasien.Nama, CONVERT(DATE, tblPasien.TglLahir) AS 'Tgl Lahir', ")
        queryBuilder.AppendLine("tblPasien.NoRekamMedis, tblBayar.JenisBayar, tblBayar.CompanyTagihan, ")
        queryBuilder.AppendLine("tblBayar.kartu AS 'JenisKartu', tblBayar.mesin AS 'JenisMesin', ")
        queryBuilder.AppendLine("tblDetail.JenisTransaksi, tblBayar.JumlahBayar - tblBayar.Kembali AS TotalBayar, ")
        queryBuilder.AppendLine("tblGigi.RekapID, tblBayar.NoInvoicePembayaran, tblBayar.JenisKasir ")
        queryBuilder.AppendLine("FROM ( ")
        queryBuilder.AppendLine("    SELECT tb.TransGigiIDBayar, tb.TransGigiID, ")
        queryBuilder.AppendLine("    STUFF((SELECT ', ' + ISNULL(tblTindakan.NamaTindakan, '') ")
        queryBuilder.AppendLine("           FROM TransaksiGigiDetail tblTindakan ")
        queryBuilder.AppendLine("           WHERE tblTindakan.Awal = '0' AND tblTindakan.TransGigiID = tb.TransGigiID ")
        queryBuilder.AppendLine("           FOR XML PATH (''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS DataListTindakan, ")
        queryBuilder.AppendLine("    tb.JenisBayar, CASE WHEN tb.JenisBayar = 'TAGIHAN' THEN tb.NoJenisBayar ELSE '' END AS 'CompanyTagihan', ")
        queryBuilder.AppendLine("    tb.JumlahBayar, tb.Kembali, tb.kartu, tb.mesin, tb.NoInvoicePembayaran, 'Transaksi Tindakan' AS JenisKasir ")
        queryBuilder.AppendLine("    FROM TransaksiGigiBayar tb ")
        queryBuilder.AppendLine("    WHERE tb.JumlahBayar > 0 ")
        queryBuilder.AppendLine("    AND tb.NoInvoicePembayaran = @NoInvoicePembayaran ")
        queryBuilder.AppendLine("    AND tb.JenisBayar = 'TAGIHAN' ")
        queryBuilder.AppendLine("    UNION ALL ")
        queryBuilder.AppendLine("    SELECT tba.TransGigiIDBayar, tba.TransGigiID, ")
        queryBuilder.AppendLine("    STUFF((SELECT ', ' + ISNULL(tblTindakan.NamaTindakan, '') ")
        queryBuilder.AppendLine("           FROM TransaksiGigiDetail tblTindakan ")
        queryBuilder.AppendLine("           WHERE tblTindakan.Awal = '1' AND tblTindakan.TransGigiID = tba.TransGigiID ")
        queryBuilder.AppendLine("           FOR XML PATH (''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS DataListTindakan, ")
        queryBuilder.AppendLine("    tba.JenisBayar, CASE WHEN tba.JenisBayar = 'TAGIHAN' THEN tba.NoJenisBayar ELSE '' END AS 'CompanyTagihan', ")
        queryBuilder.AppendLine("    tba.JumlahBayar, tba.Kembali, tba.kartu, tba.mesin, tba.NoInvoicePembayaran, 'Transaksi Awal' AS JenisKasir ")
        queryBuilder.AppendLine("    FROM TransaksiGigiBayarAwal tba ")
        queryBuilder.AppendLine("    WHERE tba.JumlahBayar > 0 ")
        queryBuilder.AppendLine("    AND tba.NoInvoicePembayaran = @NoInvoicePembayaran ")
        queryBuilder.AppendLine("    AND tba.JenisBayar = 'TAGIHAN' ")
        queryBuilder.AppendLine(") AS tblBayar ")
        queryBuilder.AppendLine("LEFT JOIN TransaksiGigi tblGigi ON tblBayar.TransGigiID = tblGigi.TransGigiID ")
        queryBuilder.AppendLine("LEFT JOIN TransaksiDetail tblDetail ON tblGigi.NoUrut = tblDetail.NoUrut ")
        queryBuilder.AppendLine("LEFT JOIN MasterPasien tblPasien ON tblGigi.IDInternet = tblPasien.NoRekamMedis ")
        queryBuilder.AppendLine("WHERE tblDetail.DeletedDate IS NULL ")
        queryBuilder.AppendLine("ORDER BY tblBayar.JenisBayar, tblDetail.JenisPoli;")

        tblResult = ReadSQL(queryBuilder.ToString())

        Return tblResult
    End Function

    Private Sub ButtonFindPerusahaanRekapInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFindPerusahaanRekapInvoice.Click
        Find.Show()
        Find.Query("SELECT NoCompany AS 'ID', CompanyCode AS 'Kode Perusahaan', CompanyNama AS 'Keterangan' FROM mastercompany WHERE GETDATE() BETWEEN StartDate AND EndDate", " ORDER BY NoCompany ASC", "FindPerusahaanReportPendapatanKlinik_RekapInvoice", 2)
    End Sub

    Private Sub ButtonCetakRekapTagihan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCetakRekapTagihan.Click
        Dim tblRekapInvoice As DataTable = GridControlRekapInvoice.DataSource

        If tblRekapInvoice.Rows.Count > 0 Then
            CetakRekapTagihan()
        Else
            MessageBox.Show("Tidak Ada Data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub CetakRekapTagihan()
        Dim tblRekapInvoice As DataTable = GridControlRekapInvoice.DataSource
        tblRekapInvoice.AcceptChanges()

        Report = XtraReport.FromFile(Application.StartupPath + "\reports\Klinik\Laporan\LaporanRekapTagihanKlinik.repx", True)

        Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelJudulLaporan").Text = "Rekap Tagihan Pemeriksaan Klinik"
        Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelLokasiLaporan").Text = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'NamaPoli'")
        Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelNamaPerusahaan").Text = TextBoxNamaPerusahaanInvoice.Text
        Report.Bands.Item(BandKind.ReportHeader).Controls("xrLabelNomorInvoice").Text = TextBoxNomorInvoice.Text
        Report.Bands.Item(BandKind.PageFooter).Controls("xrLabelInfoCetak").Text = Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") & " By " & userInput
        Report.DataSource = tblRekapInvoice

        'Report.RunDesigner()
        Report.ShowPreview()
        'Report.Print()
    End Sub

    Private Sub ButtonCetakInvoiceTagihan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCetakInvoiceTagihan.Click
        If TextBoxNomorInvoice.Text <> "" Then
            CetakInvoiceTagihan(TextBoxNomorInvoice.Text, False)
        Else
            MessageBox.Show("Tidak Ada Data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub ButtonHapusTagihan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHapusTagihan.Click
        If TextBoxNomorInvoice.Text = "" Then
            MessageBox.Show("Tidak Ada Data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If MsgBox("Apakah anda yakin ingin menghapus invoice " & TextBoxNomorInvoice.Text & " (" & TextBoxNamaPerusahaanInvoice.Text & ")?", MsgBoxStyle.YesNoCancel, "Question.") = MsgBoxResult.Yes Then
            ' Gerbang yang sama seperti saat menyimpan. Dulu hanya server keuangan yang diuji, padahal
            ' penghapusan juga WAJIB membuat jurnal pembalik — kalau server jurnal yang mati, invoice
            ' terlanjur hilang dari program sementara piutangnya tertinggal di buku besar.
            If CekKoneksiServerKirim() = False Then
                Exit Sub ' pesannya sudah ditampilkan CekKoneksiServerKirim
            Else
                If CheckThemysql("SELECT * FROM acctransrekaptagihan WHERE notagihancompany = '" & TextBoxNomorInvoice.Text & "' and lunasid > 0", DB_Keuangan) = True Then
                    MsgBox("Proses gagal, invoice " & TextBoxNomorInvoice.Text & " sudah terlunasi di keuangan.", MsgBoxStyle.Exclamation, "Warning!")
                Else
                    ' Update Tabel Rekap Online
                    WriteThemysql("UPDATE acctransrekaptagihan SET deleted=1, deletedby='" & userInput & "', deleteddate=NOW() WHERE notagihancompany = '" & TextBoxNomorInvoice.Text & "'", DB_Keuangan)
                    WriteThemysql("UPDATE acctranstagihandetail SET lockedby='', lockeddate='9998-12-31 00:00:00', rekapid=NULL WHERE rekapid IN (SELECT RekapID FROM acctransrekaptagihan WHERE notagihancompany = '" & TextBoxNomorInvoice.Text & "')", DB_Keuangan)

                    ' Update Tabel Rekap Lokal
                    WriteSQL("UPDATE RekapTagihan SET deleted=1, deletedby='" & userInput & "', deleteddate=GETDATE() WHERE notagihancompany = '" & TextBoxNomorInvoice.Text & "'", 4)

                    ' Update Tabel Pembayaran. `AND JenisBayar = 'TAGIHAN'` WAJIB: kolom
                    ' NoInvoicePembayaran ternyata juga dipakai proses lain — terukur 5 baris
                    ' DEBET/CARD (Rp 4.950.000) membawa nomor invoice tanpa RekapBy/RekapDate, jadi
                    ' bukan hasil closing form ini. Tanpa saringan itu, menghapus satu invoice ikut
                    ' mencabut tanda pada baris milik proses lain.
                    WriteSQL("UPDATE TransaksiPoliBayarAwal SET NoInvoicePembayaran=NULL, RekapBy=NULL, RekapDate=NULL WHERE NoInvoicePembayaran='" & TextBoxNomorInvoice.Text & "' AND JenisBayar='TAGIHAN'", 4)
                    WriteSQL("UPDATE TransaksiPoliBayar SET NoInvoicePembayaran=NULL, RekapBy=NULL, RekapDate=NULL WHERE NoInvoicePembayaran='" & TextBoxNomorInvoice.Text & "' AND JenisBayar='TAGIHAN'", 4)
                    WriteSQL("UPDATE TransaksiGigiBayarAwal SET NoInvoicePembayaran=NULL, RekapBy=NULL, RekapDate=NULL WHERE NoInvoicePembayaran='" & TextBoxNomorInvoice.Text & "' AND JenisBayar='TAGIHAN'", 4)
                    WriteSQL("UPDATE TransaksiGigiBayar SET NoInvoicePembayaran=NULL, RekapBy=NULL, RekapDate=NULL WHERE NoInvoicePembayaran='" & TextBoxNomorInvoice.Text & "' AND JenisBayar='TAGIHAN'", 4)

                    ' Create Jurnal. Sama seperti saat menyimpan: invoice sudah terlanjur dihapus di
                    ' sini, jadi kegagalan jurnal pembalik dilaporkan APA ADANYA — bukan ditelan dan
                    ' bukan pula membatalkan pesan bahwa invoicenya sudah hilang dari program.
                    Dim pesanJurnal As String = ""
                    Dim AutoJurnal As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName='AutoJurnal'")
                    If AutoJurnal.Trim = "Yes" Then
                        Try
                            SendingJurnalCancel(TextBoxNomorInvoice.Text)
                        Catch exJurnal As Exception
                            pesanJurnal = vbCrLf & vbCrLf & "PERHATIAN: invoice sudah dihapus tapi JURNAL PEMBALIKNYA GAGAL dibuat." & vbCrLf & _
                                          exJurnal.Message & vbCrLf & _
                                          "Piutangnya masih tercatat di buku besar — hubungi keuangan untuk jurnal manual."
                        End Try
                    End If

                    MsgBox("Invoice " & TextBoxNomorInvoice.Text & " berhasil dihapus dari program." & pesanJurnal, MsgBoxStyle.Information, "Success!")
                    SetClearRekapInvoice()
                End If
            End If
        End If
    End Sub

    Private Sub SetClearRekapInvoice()
        ' Clear Header
        TextBoxNamaPerusahaanRekapInvoice.ResetText()

        ' Clear Detail
        TextBoxNamaPerusahaanInvoice.ResetText()
        TextBoxNomorInvoice.ResetText()
        TextBoxBankInvoice.ResetText()
        TextBoxNominalInvoice.ResetText()

        GridControlRekapInvoice.DataSource = Nothing
    End Sub

    Private Sub ButtonClearDataRekapInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClearDataRekapInvoice.Click
        SetClearRekapInvoice()
    End Sub

    ' Sama seperti SendingJurnal: melempar bila jurnal pembalik TIDAK terbentuk, dan pemanggil wajib
    ' menangkapnya — invoice-nya sendiri sudah terlanjur dihapus saat sub ini dipanggil.
    Private Sub SendingJurnalCancel(ByVal nomorInvoice As String)
        ' Set Data Rekap Tagihan
        Dim tblInvoice As DataTable = ReadSQL("SELECT * FROM RekapTagihan WHERE notagihancompany = '" & nomorInvoice & "'")
        If tblInvoice.Rows.Count = 0 Then
            Throw New Exception("Invoice " & nomorInvoice & " tidak ditemukan di tabel RekapTagihan, jurnal pembalik tidak dibuat.")
        End If

        Dim rowInvoice As DataRow = tblInvoice.Rows(0)

        ' Set Variabel Jurnal
        Dim idLokasiJurnal As String = ""
        Dim keteranganLokasiJurnal As String = ""
        Dim coaPiutang As String = ""
        Dim coaPiutangPending As String = ""

        ' Set COA Piutang & Piutang Pending
        If IsDBNull(rowInvoice("JenisPoli")) = False Then ' Jika Kolom Jenis Poli Tidak Kosong (Jurnal Berdasarkan Jenis Poli)
            Dim tblMasterJurnal As DataTable = ReadThemysql("SELECT * FROM MasterLokasiJurnal a WHERE a.KodeLokasi = '" & kodeLokasiKlinik & "' AND a.KeteranganPoli = '" & rowInvoice("JenisPoli") & "' LIMIT 1", DB_Omset)
            If tblMasterJurnal.Rows.Count = 0 Then
                Throw New Exception("MasterLokasiJurnal untuk poli '" & rowInvoice("JenisPoli").ToString() & "' di lokasi " & kodeLokasiKlinik & " tidak ditemukan (atau server omset tidak terjangkau), jurnal pembalik tidak dibuat.")
            End If

            Dim rowDataJurnal As DataRow = tblMasterJurnal.Rows(0)
            idLokasiJurnal = rowDataJurnal("KodeLokasiJurnal")
            keteranganLokasiJurnal = rowDataJurnal("KeteranganLokasi") & " (" & rowDataJurnal("KeteranganPoli") & ") "
            coaPiutang = rowDataJurnal("KodeCoaPiutang")
            coaPiutangPending = rowDataJurnal("KodeCoaPiutangPending")
        Else ' Jika Kolom Jenis Poli Kosong (Jurnal Berdasarkan Jenis Transaksi)
            Dim tblMasterJurnal As DataTable = ReadThemysql("SELECT * FROM MasterLokasiJurnal a WHERE a.KodeLokasi = '" & kodeLokasiKlinik & "' AND a.JenisTransaksi = '" & rowInvoice("jenistransaksi") & "' LIMIT 1", DB_Omset)
            If tblMasterJurnal.Rows.Count = 0 Then
                Throw New Exception("MasterLokasiJurnal untuk jenis transaksi '" & rowInvoice("jenistransaksi").ToString() & "' di lokasi " & kodeLokasiKlinik & " tidak ditemukan (atau server omset tidak terjangkau), jurnal pembalik tidak dibuat.")
            End If

            Dim rowDataJurnal As DataRow = tblMasterJurnal.Rows(0)
            idLokasiJurnal = rowDataJurnal("KodeLokasiJurnal")
            keteranganLokasiJurnal = rowDataJurnal("KeteranganLokasi") & " (" & rowDataJurnal("JenisTransaksi") & ") "
            If rowInvoice("jenistransaksi").ToString() = "POLI" Then
                coaPiutang = ReadThemysqlNIK("SELECT KodeAkun FROM MasterCoaJurnal WHERE NamaAkun = 'Piutang Poli Umum'", DB_Omset)
                coaPiutangPending = ReadThemysqlNIK("SELECT KodeAkun FROM MasterCoaJurnal WHERE NamaAkun = 'Piutang Pending Poli Umum'", DB_Omset)
            ElseIf rowInvoice("jenistransaksi").ToString() = "GIGI" Then
                coaPiutang = ReadThemysqlNIK("SELECT KodeAkun FROM MasterCoaJurnal WHERE NamaAkun = 'Piutang Poli Gigi'", DB_Omset)
                coaPiutangPending = ReadThemysqlNIK("SELECT KodeAkun FROM MasterCoaJurnal WHERE NamaAkun = 'Piutang Pending Poli Gigi'", DB_Omset)
            End If
        End If

        ' Set Flag / Refference
        Dim refferenceID As String = GetUUID()

        ' Set Nama Perusahaan
        Dim splitPerusahaan() As String = rowInvoice("company").ToString().Split("-")

        ' Set Keterangan Jurnal
        Dim keteranganJurnal As String = "CANCEL - TAGIHAN " & splitPerusahaan(0).ToString.Trim.ToUpper() & " " & keteranganLokasiJurnal & " " & Format(rowInvoice("tgltagihan"), "dd/MM/yyyy") & " (NO. INVOICE : " & nomorInvoice & ")"

        ' Penjagaan akun yang sama seperti SendingJurnal — jurnal pembalik ke akun '-' sama merusaknya.
        If coaPiutang.Trim() = "" OrElse coaPiutang.Trim() = "-" OrElse coaPiutangPending.Trim() = "" OrElse coaPiutangPending.Trim() = "-" Then
            Throw New Exception("COA piutang belum lengkap (D '" & coaPiutang & "' / K '" & coaPiutangPending & "'), jurnal pembalik tidak dibuat. Lengkapi MasterLokasiJurnal untuk divisi ini.")
        End If

        ' Kirim Jurnal Cancel Tagihan. Melempar bila server jurnal menolak — dulu lewat WriteThemysql
        ' sehingga invoice bisa terhapus tanpa jurnal pembalik dan piutangnya tertinggal di buku besar.
        KirimJurnalAman("SELECT F_MakeUMH('" & refferenceID & "', 'out', '', '" & Format(rowInvoice("tgltagihan"), "yyyyMMdd") & "', '" & coaPiutang & "', '" & coaPiutangPending & "', 'IDR', 1, " & CDbl(rowInvoice("totaltagihan")) & ", '" & keteranganJurnal.ToUpper() & "', '1', 1, '', '" & keteranganJurnal.ToUpper() & "', '', '', '" & idLokasiJurnal & "', 4, " & CDbl(rowInvoice("totaltagihan")) & ", 'K')", refferenceID)

        ' Update Tabel Rekap Tagihan (Online)
        WriteThemysql("UPDATE acctransrekaptagihan SET FlagJurnalCancel = '" & refferenceID & "' WHERE notagihancompany = '" & nomorInvoice & "'", DB_Keuangan)

        ' Insert Log Invoice Tagihan
        WriteSQL("INSERT INTO LogJurnalKeuangan (JurnalFlag, JurnalDate, JurnalBy, Status, JenisPoli, JenisTransaksi) VALUES ('" & refferenceID & "', GETDATE(), '" & userInput & "', '" & keteranganJurnal & "', '" & rowInvoice("JenisPoli").ToString & "', '" & rowInvoice("jenistransaksi").ToString() & "')", 4)
    End Sub

    Private Sub ButtonKirimUlangJurnal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKirimUlangJurnal.Click
        If TextBoxNoTagihan.Text.Trim() = "" Then
            MessageBox.Show("Pilih invoicenya terlebih dahulu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' SendingJurnal kini melempar bila jurnal tidak terbentuk (dulu diam saja), dan menolak sendiri
        ' bila invoice itu sudah pernah dijurnal. Tanpa Try di sini, lemparannya jadi crash aplikasi.
        Try
            Dim nomorJurnal As String = SendingJurnal(TextBoxNoTagihan.Text)
            If nomorJurnal <> "" Then
                MsgBox("Jurnal untuk invoice " & TextBoxNoTagihan.Text & " berhasil dibuat, No. " & nomorJurnal & ".", MsgBoxStyle.Information, "Success!")
            End If
        Catch exJurnal As Exception
            MsgBox("Jurnal TIDAK dibuat." & vbCrLf & vbCrLf & exJurnal.Message & vbCrLf & vbCrLf & _
                   "Perbaiki penyebabnya lalu coba lagi — belum ada jurnal yang terbentuk, jadi aman diulang.", _
                   MsgBoxStyle.Exclamation, "Gagal Kirim Jurnal")
        End Try
    End Sub

    ' ==========================================================================================
    ' TIGA MODE LAPORAN YANG SALING MENIMPA GRID: jenis bayar (NON PAKET), VOUCHER, dan PAKET.
    ' ButtonViewDataPendapatan_Click memilih SATU mode menurut urutan prioritas Paket > Voucher >
    ' jenis bayar, jadi bila dua mode tercentang bersamaan petugas melihat data yang bukan ia minta
    ' (mis. centang Paket + Tunai -> yang muncul hanya data Paket). Ketiga handler di bawah menjaga
    ' hanya satu mode yang aktif, jadi apa yang tercentang = apa yang tampil.
    '
    ' Aman dari pemanggilan berantai walau CheckedChanged saling terpicu: setiap mutasi hanya
    ' men-set False dan setiap handler dijaga "If ... Checked" sehingga picuan balasannya berhenti.
    ' CheckBoxTransaksiNolRupiah SENGAJA tidak ikut — itu saringan tambahan, dipakai ketiga mode.
    ' ==========================================================================================
    Private Sub CheckBoxVoucher_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxVoucher.CheckedChanged
        If CheckBoxVoucher.Checked Then
            MatikanCentangJenisBayar()
            CheckBoxPaket.Checked = False
        End If
    End Sub

    Private Sub CheckBoxPaket_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxPaket.CheckedChanged
        If CheckBoxPaket.Checked Then
            MatikanCentangJenisBayar()
            CheckBoxVoucher.Checked = False
        End If
    End Sub

    ' Satu handler untuk keempat jenis bayar — isinya sama, dan keempatnya boleh tercentang bersama.
    Private Sub CheckBoxJenisBayar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles CheckBoxTagihan.CheckedChanged, CheckBoxTunai.CheckedChanged, _
                    CheckBoxDebet.CheckedChanged, CheckBoxCard.CheckedChanged
        If DirectCast(sender, System.Windows.Forms.CheckBox).Checked Then
            CheckBoxVoucher.Checked = False
            CheckBoxPaket.Checked = False
        End If
    End Sub

    Private Sub MatikanCentangJenisBayar()
        CheckBoxTagihan.Checked = False
        CheckBoxTunai.Checked = False
        CheckBoxDebet.Checked = False
        CheckBoxCard.Checked = False
    End Sub
End Class