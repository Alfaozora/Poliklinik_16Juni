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

Public Class MasterTindakanKlinik
    Private formName As String = "MASTER TINDAKAN KLINIK"

    Private Find_Poliklinik As Find.TLconnect
    Private DB_Poliklinik As TLcon

    Dim thread As System.Threading.Thread
    Dim conn As OleDbConnection
    Dim dtr As OleDbDataReader
    Dim dta As OleDbDataAdapter
    Dim cmd As OleDbCommand
    Dim dts As DataSet
    Dim excel As String
    Dim opFileDialog As New OpenFileDialog()
    Private userInput As String = PoliKlinik.Lbluser.Text

    ' Deklarasi variabel untuk menyimpan link terakhir yang diklik
    Private lastClickedItem As DevExpress.XtraNavBar.NavBarItem = Nothing

    Private Sub PanelTop_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PanelTop.Paint
        Try
            e.Graphics.DrawString(formName, New Font("Arial", 12, FontStyle.Bold), Brushes.White, New Point(10, 10))
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SetConn()
        ' Set Parameter Server Online
        Dim serverOnline_IP As String = Utility.GetConfig("ServerOnline_IP")
        Dim serverOnline_UserID As String = Utility.GetConfig("ServerOnline_UserID")
        Dim serverOnline_Password As String = KASLibrary.Utility.DecryptTripleDES(Utility.GetConfig("ServerOnline_Password"), "albert")

        ' Set DB Poliklinik
        DB_Poliklinik.Tip = serverOnline_IP
        DB_Poliklinik.Tid = serverOnline_UserID
        DB_Poliklinik.Tpass = serverOnline_Password
        DB_Poliklinik.Tdb = Utility.GetConfig("ServerOnlinePoli_DB")

        ' Set Find Poliklinik
        Find_Poliklinik.Tip = serverOnline_IP
        Find_Poliklinik.Tid = serverOnline_UserID
        Find_Poliklinik.Tpass = serverOnline_Password
        Find_Poliklinik.Tdb = Utility.GetConfig("ServerOnlinePoli_DB")
    End Sub

    Private Sub SetDataGrid()
        Dim tblTindakan As DataTable = CreateTableTindakan()

        If TextBoxReffID.Text <> "" Then
            Dim stringQuery As New StringBuilder()

            stringQuery.AppendLine("SELECT b.TindakanID, b.NamaTindakan, b.JenisTindakan, b.BiayaTindakan, b.DiskonTindakan, b.NettBayar, b.Jumlah AS 'KuotaTindakan', b.GrandTotal")
            stringQuery.AppendLine("FROM MasterPaketTindakan a")
            stringQuery.AppendLine("LEFT JOIN MasterPaketTindakanDetail b ON a.PaketID = b.PaketID")
            stringQuery.AppendLine("WHERE a.PaketID = '" & TextBoxReffID.Text & "' AND b.DeletedDate IS NULL")

            tblTindakan = ReadSQL(stringQuery.ToString())
        End If

        ' Set DataTable sebagai sumber data GridControl
        GridControlTindakan.DataSource = tblTindakan
        GridViewTindakan.BestFitColumns()
        GridViewTindakan.Columns("TindakanID").Visible = False

        ' Nonaktifkan edit untuk semua kolom
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In GridViewTindakan.Columns
            col.OptionsColumn.AllowEdit = False
        Next

        ' Aktifkan edit hanya untuk kolom tertentu
        GridViewTindakan.Columns("NettBayar").OptionsColumn.AllowEdit = True

        ' Tambahkan event handler untuk kalkulasi otomatis
        AddHandler GridViewTindakan.CellValueChanged, AddressOf GridViewTindakan_CellValueChanged
    End Sub

    ' Tambahkan variabel global untuk mencegah rekursi
    Private isProcessing As Boolean = False

    Private Sub GridViewTindakan_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs)
        ' Cegah rekursi dengan mengecek flag
        If isProcessing Then Exit Sub
        isProcessing = True

        ' Nonaktifkan event handler sementara untuk menghindari rekursi
        RemoveHandler GridViewTindakan.CellValueChanged, AddressOf GridViewTindakan_CellValueChanged

        Try
            ' Pastikan event hanya diproses untuk kolom "NettBayar" atau "KuotaTindakan"
            If e.Column.FieldName = "NettBayar" OrElse e.Column.FieldName = "KuotaTindakan" Then
                ' Nonaktifkan update tampilan sementara
                GridViewTindakan.BeginUpdate()

                ' Ambil nilai dari grid
                Dim biayaTindakan As Decimal = Convert.ToDecimal(GridViewTindakan.GetRowCellValue(e.RowHandle, "BiayaTindakan"))
                Dim nettBayar As Object = GridViewTindakan.GetRowCellValue(e.RowHandle, "NettBayar")
                Dim kuotaTindakan As Object = GridViewTindakan.GetRowCellValue(e.RowHandle, "KuotaTindakan")

                ' Validasi NettBayar
                Dim nettBayarValid As Decimal = 0
                If nettBayar IsNot Nothing AndAlso Not String.IsNullOrEmpty(nettBayar.ToString()) Then
                    If Decimal.TryParse(nettBayar.ToString(), nettBayarValid) Then
                        If nettBayarValid < 0 Then
                            MessageBox.Show("NettBayar tidak boleh kurang dari 0!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            nettBayarValid = 0
                        End If
                    Else
                        MessageBox.Show("NettBayar harus berupa angka!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        nettBayarValid = 0
                    End If
                End If

                ' Set nilai NettBayar yang valid ke grid
                GridViewTindakan.SetRowCellValue(e.RowHandle, "NettBayar", nettBayarValid)

                ' Validasi KuotaTindakan
                Dim kuotaTindakanValid As Decimal = 1
                If kuotaTindakan IsNot Nothing AndAlso Not String.IsNullOrEmpty(kuotaTindakan.ToString()) Then
                    If Decimal.TryParse(kuotaTindakan.ToString(), kuotaTindakanValid) Then
                        If kuotaTindakanValid < 1 Then
                            MessageBox.Show("KuotaTindakan tidak boleh kurang dari 1!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            kuotaTindakanValid = 1
                        End If
                    Else
                        MessageBox.Show("KuotaTindakan harus berupa angka!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        kuotaTindakanValid = 1
                    End If
                End If

                ' Set nilai KuotaTindakan yang valid ke grid
                GridViewTindakan.SetRowCellValue(e.RowHandle, "KuotaTindakan", kuotaTindakanValid)

                ' Validasi NettBayar tidak boleh melebihi BiayaTindakan
                If nettBayarValid > biayaTindakan Then
                    MessageBox.Show("NettBayar tidak boleh melebihi BiayaTindakan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    nettBayarValid = biayaTindakan
                    GridViewTindakan.SetRowCellValue(e.RowHandle, "NettBayar", biayaTindakan)
                End If

                ' Hitung DiskonTindakan
                Dim diskonTindakan As Decimal = Math.Round(biayaTindakan - nettBayarValid, 2)
                GridViewTindakan.SetRowCellValue(e.RowHandle, "DiskonTindakan", diskonTindakan)

                ' Hitung GrandTotal
                Dim grandTotal As Decimal = Math.Round(nettBayarValid * kuotaTindakanValid, 2)
                GridViewTindakan.SetRowCellValue(e.RowHandle, "GrandTotal", grandTotal)

                ' Aktifkan kembali event handler
                GridViewTindakan.EndUpdate()
            End If
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat menghitung nilai: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Aktifkan kembali event handler dan reset flag
            AddHandler GridViewTindakan.CellValueChanged, AddressOf GridViewTindakan_CellValueChanged
            isProcessing = False
        End Try
    End Sub

    Function CreateTableTindakan() As DataTable
        Dim Thasil As New DataTable

        insertRowToTbls(Thasil, "Typstring", "TindakanID")
        insertRowToTbls(Thasil, "Typstring", "NamaTindakan")
        insertRowToTbls(Thasil, "Typstring", "JenisTindakan")
        insertRowToTbls(Thasil, "Double", "BiayaTindakan")
        insertRowToTbls(Thasil, "Double", "DiskonTindakan")
        insertRowToTbls(Thasil, "Double", "NettBayar")
        insertRowToTbls(Thasil, "Double", "KuotaTindakan")
        insertRowToTbls(Thasil, "Double", "GrandTotal")

        Return Thasil
    End Function

    Private Sub MasterTindakanKlinik_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        Me.MdiParent = PoliKlinik
        Me.Dock = DockStyle.Fill

        ' Hide tab header button TabControl
        TabControlMenu.ItemSize = New Size(0, 1)
        TextBoxHariReminder.ReadOnly = True

        ClearDataMenuPaket()
        ClearDataMenuTindakan()
        LoadPoliData()
        LoadPoliDataPaket()

        SetConn()
        SetDataGrid()

        InitGridAlatTindakan()

        ' Panggil metode untuk NavBarItemTindakanPoliUmum
        TriggerNavBarItemClick(NavBarItemTindakanPoliUmum)
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

    Private Sub NavBarItem_LinkClicked(ByVal sender As System.Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavBarItemTindakanPoliUmum.LinkClicked, NavBarItemMasterPaketTindakan.LinkClicked, NavBarItemMasterAlat.LinkClicked
        ' Reset gaya dari item sebelumnya jika ada
        If lastClickedItem IsNot Nothing Then lastClickedItem.Appearance.ForeColor = Color.Empty

        ' Simpan item yang baru diklik dan setel warnanya
        lastClickedItem = e.Link.Item
        lastClickedItem.Appearance.ForeColor = Color.Gray

        ' Dapatkan caption dari link yang diklik
        Dim clickedLink As String = e.Link.Item.Caption

        ' Lakukan navigasi berdasarkan link yang diklik
        Select Case clickedLink
            Case "MASTER TINDAKAN"
                TabControlMenu.SelectedTab = TabPageMasterTindakan
            Case "MASTER PAKET TINDAKAN"
                TabControlMenu.SelectedTab = TabPageMasterPaket
            Case "MASTER ALAT"
                TabControlMenu.SelectedTab = TabPageMasterAlat
            Case Else
                MessageBox.Show("Link tidak dikenali!")
        End Select
    End Sub

    Private Function ValidasiDetailPaket() As Boolean
        Dim namaPaket As String = TextBoxNamaPaket.Text.Trim()
        Dim keteranganPaket As String = TextBoxKeteranganPaket.Text.Trim()
        Dim hargaPaket As String = TextBoxHargaAwal.Text.Trim()
        Dim maksimalBHP As String = TextBoxMaksimalBHP_Paket.Text.Trim()

        ' Validasi Nama Paket
        If namaPaket = "" Then
            MsgBox("Gagal. Silahkan isi nama paket terlebih dahulu.", MsgBoxStyle.Exclamation, "Warning!")
            Return False
        ElseIf namaPaket.Length <= 4 Then
            MsgBox("Gagal. Nama paket harus lebih dari 4 karakter.", MsgBoxStyle.Exclamation, "Warning!")
            Return False
        End If

        ' Validasi Nama Poli
        If RadioButtonPaketReguler.Checked = True AndAlso CheckedListBoxPoli2.CheckedItems.Count = 0 Then
            MsgBox("Silakan pilih minimal satu poli.", MsgBoxStyle.Exclamation, "Warning")
            Exit Function
        ElseIf RadioButtonPaketPerTindakan.Checked = True AndAlso CheckedListBoxPoli2.CheckedItems.Count = 0 Then
            MsgBox("Silakan pilih minimal satu poli.", MsgBoxStyle.Exclamation, "Warning")
            Exit Function
        End If

        ' Validasi Keterangan Paket
        If keteranganPaket = "" Then TextBoxKeteranganPaket.Text = "-"

        ' Validasi Kuota Paket
        If CDbl(TextBoxKuotaPaket.Value) <= 0 And RadioButtonPaketReguler.Checked = True Then
            MsgBox("Gagal. Kuota paket harus lebih dari 0.", MsgBoxStyle.Exclamation, "Warning!")
            Return False
        End If

        ' Validasi Harga Paket
        If hargaPaket = "" Then
            MessageBox.Show("Harga paket tidak boleh kosong!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBoxHargaAwal.Focus()
            Return False
        ElseIf Not IsNumeric(hargaPaket) Then
            MessageBox.Show("Harga paket harus berupa angka!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBoxHargaAwal.Focus()
            Return False
        ElseIf CDbl(hargaPaket) <= 0 Then
            MessageBox.Show("Harga paket harus lebih dari 0!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBoxHargaAwal.Focus()
            Return False
        End If

        ' Validasi Maksimal BHP
        If maksimalBHP.Trim() = "" Then
            TextBoxMaksimalBHP_Paket.Text = "0"
        ElseIf Not IsNumeric(maksimalBHP) Then
            MessageBox.Show("Maksimal BHP harus berupa angka!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBoxMaksimalBHP_Paket.Focus()
            Return False
        ElseIf CDbl(maksimalBHP) <= 0 Then
            MessageBox.Show("Maksimal BHP harus lebih dari 0!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBoxMaksimalBHP_Paket.Focus()
            Return False
        End If

        ' Validasi Tanggal Paket
        If TglAkhirPaket.Value.Date < TglAwalPaket.Value.Date Then
            MsgBox("Gagal. Tanggal awal tidak boleh melebihi tanggal akhir.", MsgBoxStyle.Exclamation, "Warning!")
            Return False
        End If

        ' Validasi Durasi Aktif Paket
        If CDbl(TextBoxDurasiAktifPaket.Value) <= 0 Then
            MsgBox("Gagal. Durasi aktif paket harus lebih dari 0.", MsgBoxStyle.Exclamation, "Warning!")
            Return False
        End If

        ' Kalkulasi Paket
        KalkulasiNominalPaket()

        ' Validasi Akhir
        Dim result As DialogResult = MessageBox.Show("Apakah data yang dimasukkan sudah sesuai?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.No Then
            Return False
        End If

        Return True
    End Function

    Private Sub SimpanDataPaket()
        Dim reffID As String = ""
        Dim statusInformedConsent As Boolean = False

        ' Get JSON Data Grid
        Dim jsonDataGrid As String = GenerateJsonFromGridControl(GridControlTindakan)

        Dim poliList As New List(Of String)
        For Each item As String In CheckedListBoxPoli2.CheckedItems
            poliList.Add(item)
        Next

        Dim poliGabung As String = String.Join(",", poliList.ToArray())

        ' Get Jenis Paket
        Dim jenisPaket As String = ""
        If RadioButtonPaketReguler.Checked = True Then
            jenisPaket = RadioButtonPaketReguler.Text
        ElseIf RadioButtonPaketPerTindakan.Checked = True Then
            jenisPaket = RadioButtonPaketPerTindakan.Text
        End If

        ' Set Status Informed Consent
        If CheckBoxICPaket.Checked = True Then statusInformedConsent = True

        If TextBoxReffID.Text = "" Then ' Insert Paket
            ' Save ke tabel MasterPaketTindakan
            WriteSQL("INSERT INTO MasterPaketTindakan (NamaPaket, KeteranganPaket, JenisPaket, HargaAwal, DiskonNominal, HargaNett, Kuota, MaksimalBHP, DetailTindakan, TanggalMulai, TanggalBerakhir, DurasiAktif, CreatedBy, CreatedDate, PoliTujuan, StatusInformedConsent, TindakanInformedConsent, IsSync) " & _
                     "VALUES('" & TextBoxNamaPaket.Text & "', '" & TextBoxKeteranganPaket.Text & "', '" & jenisPaket & "', " & CDbl(TextBoxHargaAwal.Text) & ", " & CDbl(TextBoxTotalDiskon.Text) & ", " & CDbl(TextBoxHargaNett.Text) & ", " & CDbl(TextBoxKuotaPaket.Text) & ", " & CDbl(TextBoxMaksimalBHP_Paket.Text) & ", '" & jsonDataGrid & "', '" & Format(TglAwalPaket.Value, "MM/dd/yyyy") & "', '" & Format(TglAkhirPaket.Value, "MM/dd/yyyy") & "', " & CDbl(TextBoxDurasiAktifPaket.Text) & ", '" & userInput & "', GETDATE(), '" & poliGabung & "', '" & statusInformedConsent & "', '" & ComboBoxICPaket.Text & "', 0)", 4)

            ' Get Reff ID
            reffID = ReadSQLNIK("SELECT TOP(1) PaketID FROM MasterPaketTindakan WHERE NamaPaket = '" & TextBoxNamaPaket.Text & "' ORDER BY CreatedDate DESC")

            ' Save Detail Grid Paket
            SimpanDetailPaket(reffID)

            ' =========================
            ' 🔥 KIRIM KE API (INSERT)
            ' =========================
            Dim payload As New Dictionary(Of String, Object)

            payload("PaketID") = reffID
            payload("NamaPaket") = TextBoxNamaPaket.Text
            payload("KeteranganPaket") = TextBoxKeteranganPaket.Text
            payload("JenisPaket") = jenisPaket
            payload("HargaAwal") = CDbl(TextBoxHargaAwal.Text)
            payload("DiskonNominal") = CDbl(TextBoxTotalDiskon.Text)
            payload("HargaNett") = CDbl(TextBoxHargaNett.Text)
            payload("Kuota") = CDbl(TextBoxKuotaPaket.Text)
            payload("MaksimalBHP") = CDbl(TextBoxMaksimalBHP_Paket.Text)
            payload("DetailTindakan") = jsonDataGrid
            payload("TanggalMulai") = Format(TglAwalPaket.Value, "yyyy-MM-dd")
            payload("TanggalBerakhir") = Format(TglAkhirPaket.Value, "yyyy-MM-dd")
            payload("DurasiAktif") = CDbl(TextBoxDurasiAktifPaket.Text)
            payload("CreatedBy") = userInput
            payload("PoliTujuan") = poliGabung
            payload("StatusInformedConsent") = If(statusInformedConsent, 1, 0)
            payload("TindakanInformedConsent") = ComboBoxICPaket.Text

            Dim jsonParam As String = JsonConvert.SerializeObject(payload)

            Dim baseURL As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'ws_RM'")
            Dim urlWebService As String = baseURL & "api/paket-tindakan"

            Dim responseApi As String = InsertDataAPIRMOld(jsonParam, urlWebService)

            If responseApi.Contains("success") Then
                WriteSQL("UPDATE MasterPaketTindakan SET IsSync = 1 WHERE PaketID = '" & reffID & "'", 4)
                MsgBox("Data Paket Berhasil Disimpan & Sync Server", MsgBoxStyle.Information, "Success")
            Else
                MsgBox("Data lokal tersimpan, tapi gagal sync ke server", MsgBoxStyle.Exclamation)
            End If

            'MsgBox("Data Paket Berhasil Disimpan.", MsgBoxStyle.Information, "Success")
        Else ' Update Paket
            reffID = TextBoxReffID.Text ' Set UUID

            Dim parameterUpdate As String = ""
            parameterUpdate = "NamaPaket = '" & TextBoxNamaPaket.Text & "'," & _
                              "KeteranganPaket = '" & TextBoxKeteranganPaket.Text & "'," & _
                              "HargaAwal = " & CDbl(TextBoxHargaAwal.Text) & "," & _
                              "DiskonNominal = " & CDbl(TextBoxTotalDiskon.Text) & "," & _
                              "HargaNett = " & CDbl(TextBoxHargaNett.Text) & "," & _
                              "Kuota = " & CDbl(TextBoxKuotaPaket.Text) & "," & _
                              "MaksimalBHP = " & CDbl(TextBoxMaksimalBHP_Paket.Text) & "," & _
                              "DetailTindakan = '" & jsonDataGrid & "'," & _
                              "TanggalMulai = '" & Format(TglAwalPaket.Value, "MM/dd/yyyy") & "'," & _
                              "TanggalBerakhir = '" & Format(TglAkhirPaket.Value, "MM/dd/yyyy") & "'," & _
                              "DurasiAktif = " & CDbl(TextBoxDurasiAktifPaket.Text) & "," & _
                              "UpdatedBy = '" & userInput & "'," & _
                              "PoliTujuan = '" & poliGabung & "'," & _
                              "StatusInformedConsent = '" & statusInformedConsent & "'," & _
                              "TindakanInformedConsent = '" & ComboBoxICPaket.Text & "'," & _
                              "UpdatedDate = GETDATE()," & _
                              "IsSync = 0"

            WriteSQL("UPDATE MasterPaketTindakan SET " & parameterUpdate & " WHERE PaketID = '" & reffID & "'", 4)

            ' Save Detail Grid Paket
            SimpanDetailPaket(reffID)

            ' =========================
            ' 🔥 KIRIM KE API (UPDATE)
            ' =========================
            Dim payload As New Dictionary(Of String, Object)

            payload("PaketID") = reffID
            payload("NamaPaket") = TextBoxNamaPaket.Text
            payload("KeteranganPaket") = TextBoxKeteranganPaket.Text
            payload("JenisPaket") = jenisPaket
            payload("HargaAwal") = CDbl(TextBoxHargaAwal.Text)
            payload("DiskonNominal") = CDbl(TextBoxTotalDiskon.Text)
            payload("HargaNett") = CDbl(TextBoxHargaNett.Text)
            payload("Kuota") = CDbl(TextBoxKuotaPaket.Text)
            payload("MaksimalBHP") = CDbl(TextBoxMaksimalBHP_Paket.Text)
            payload("DetailTindakan") = jsonDataGrid
            payload("TanggalMulai") = Format(TglAwalPaket.Value, "yyyy-MM-dd")
            payload("TanggalBerakhir") = Format(TglAkhirPaket.Value, "yyyy-MM-dd")
            payload("DurasiAktif") = CDbl(TextBoxDurasiAktifPaket.Text)
            payload("CreatedBy") = userInput
            payload("PoliTujuan") = poliGabung
            payload("StatusInformedConsent") = If(statusInformedConsent, 1, 0)
            payload("TindakanInformedConsent") = ComboBoxICPaket.Text

            Dim jsonParam As String = JsonConvert.SerializeObject(payload)

            Dim baseURL As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'ws_RM'")
            Dim urlWebService As String = baseURL & "api/paket-tindakan" ' bisa sama endpoint

            Dim responseApi As String = InsertDataAPIRMOld(jsonParam, urlWebService)

            If responseApi.Contains("success") Then
                WriteSQL("UPDATE MasterPaketTindakan SET IsSync = 1 WHERE PaketID = '" & reffID & "'", 4)
                MsgBox("Data Paket Berhasil Update & Sync Server", MsgBoxStyle.Information, "Success")
            Else
                MsgBox("Update lokal berhasil, tapi gagal sync ke server", MsgBoxStyle.Exclamation)
            End If

            'MsgBox("Data Paket Berhasil Diperbarui.", MsgBoxStyle.Information, "Success")
        End If
    End Sub

    Private Sub SimpanDetailPaket(ByVal PaketID As String)
        Dim tblsTindakan As DataTable = GridControlTindakan.DataSource
        tblsTindakan.AcceptChanges()

        For Each rowTindakan As DataRow In tblsTindakan.Rows
            If CheckSQL("SELECT * FROM MasterPaketTindakanDetail WHERE PaketID = '" & PaketID & "' AND TindakanID = '" & rowTindakan("TindakanID") & "' AND DeletedDate IS NULL") Then
                Dim parameterUpdate As String = ""
                parameterUpdate = "NamaTindakan = '" & rowTindakan("NamaTindakan") & "'," & _
                                  "JenisTindakan = '" & rowTindakan("JenisTindakan") & "'," & _
                                  "BiayaTindakan = " & CDbl(rowTindakan("BiayaTindakan")) & "," & _
                                  "DiskonTindakan = " & CDbl(rowTindakan("DiskonTindakan")) & "," & _
                                  "NettBayar = " & CDbl(rowTindakan("NettBayar")) & "," & _
                                  "Jumlah = " & CDbl(rowTindakan("KuotaTindakan")) & "," & _
                                  "GrandTotal = " & CDbl(rowTindakan("GrandTotal")) & "," & _
                                  "UpdatedBy = '" & userInput & "'," & _
                                  "UpdatedDate = GETDATE()"

                WriteSQL("UPDATE MasterPaketTindakanDetail SET " & parameterUpdate & " WHERE PaketID = '" & PaketID & "' AND TindakanID = '" & rowTindakan("TindakanID") & "' AND DeletedDate IS NULL", 4)
            Else
                ' Save ke tabel MasterPaketTindakanDetail
                WriteSQL("INSERT INTO MasterPaketTindakanDetail (PaketID, TindakanID, NamaTindakan, JenisTindakan, BiayaTindakan, DiskonTindakan, NettBayar, Jumlah, GrandTotal, CreatedBy, CreatedDate) " & _
                         "VALUES('" & PaketID & "', '" & rowTindakan("TindakanID") & "', '" & rowTindakan("NamaTindakan") & "', '" & rowTindakan("JenisTindakan") & "', " & CDbl(rowTindakan("BiayaTindakan")) & ", " & CDbl(rowTindakan("DiskonTindakan")) & ", " & CDbl(rowTindakan("NettBayar")) & ", " & CDbl(rowTindakan("KuotaTindakan")) & ", " & CDbl(rowTindakan("GrandTotal")) & ", '" & userInput & "', GETDATE())", 4)
            End If
        Next
    End Sub

    Private Sub ClearDataMenuPaket()
        TextBoxReffID.ResetText()
        TextBoxNamaPaket.ResetText()
        TextBoxKeteranganPaket.ResetText()
        TextBoxKuotaPaket.Value = 0
        TextBoxMaksimalBHP_Paket.ResetText()
        TextBoxHargaAwal.ResetText()
        TextBoxTotalDiskon.ResetText()
        TextBoxHargaNett.ResetText()
        TglAwalPaket.Value = Date.Now
        TglAkhirPaket.Value = Date.Now
        TextBoxDurasiAktifPaket.Value = 0

        For i As Integer = 0 To CheckedListBoxPoli2.Items.Count - 1
            CheckedListBoxPoli2.SetItemChecked(i, False)
        Next
        CheckedListBoxPoli2.Enabled = True

        InsertNewItemComboSQL("SELECT DISTINCT NamaTindakan FROM Klinik_MasterTindakanInformedConsent", ComboBoxICPaket)

        ComboBoxICPaket.SelectedIndex = -1
        ComboBoxICPaket.Enabled = False

        RadioButtonPaketReguler.Checked = True
        RadioButtonPaketReguler.Enabled = True
        RadioButtonPaketPerTindakan.Enabled = True

        SetDataGrid()
    End Sub

    Private Sub ButtonDetailTindakanPaket_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDetailTindakanPaket.Click
        FindMultiple.Show()
        FindMultiple.Query("SELECT TindakanID, NamaTindakan, JenisTindakan, JenisDokter, BiayaTindakan FROM MasterTindakan WHERE Closed = 0 ", " ORDER BY JenisTindakan, NamaTindakan", "FindPaketKlinikDetail", 1)
    End Sub

    Public Sub AddTindakanDetail(ByRef Trow() As DataRow)
        Dim Tbls As DataTable = GridControlTindakan.DataSource
        If Trow.Count > 0 Then
            For i As Integer = 0 To Trow.Length - 1
                ' Validasi: Cek apakah TindakanID sudah ada di DataTable
                Dim TindakanID As String = Trow(i)("TindakanID").ToString()
                Dim alreadyExists As Boolean = False

                ' Loop untuk cek apakah TindakanID sudah ada di DataTable
                For Each row As DataRow In Tbls.Rows
                    If row("TindakanID").ToString() = TindakanID Then
                        alreadyExists = True
                        Exit For
                    End If
                Next

                ' Jika sudah ada, berikan pesan peringatan, jika tidak, tambahkan
                If alreadyExists Then
                    MessageBox.Show("Tindakan dengan ID " & Trow(i)("NamaTindakan").ToString() & " sudah ada.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    ' Jika TindakanID belum ada, tambahkan row baru
                    Dim Tnew As DataRow = Tbls.NewRow
                    Tnew("TindakanID") = TindakanID
                    Tnew("NamaTindakan") = Trow(i)("NamaTindakan")
                    Tnew("JenisTindakan") = Trow(i)("JenisTindakan")
                    Tnew("BiayaTindakan") = Trow(i)("BiayaTindakan")
                    Tnew("DiskonTindakan") = "0"
                    Tnew("NettBayar") = Trow(i)("BiayaTindakan")
                    Tnew("KuotaTindakan") = "1"
                    Tnew("GrandTotal") = Trow(i)("BiayaTindakan")
                    Tbls.Rows.Add(Tnew)
                End If
            Next
            Tbls.AcceptChanges()
        End If
    End Sub

    Private Sub ButtonFindPaket_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFindPaket.Click
        Find.Show()
        Find.Query("SELECT NamaPaket, KeteranganPaket, HargaNett, TanggalMulai, TanggalBerakhir, PaketID FROM MasterPaketTindakan", " ORDER BY CreatedDate DESC", "FindPaketTindakanKlinik", 2)
    End Sub

    Public Sub LoadDetailPaket(ByRef PaketID As String)
        ClearDataMenuPaket()

        Dim Tbls As DataTable = ReadSQL("SELECT * FROM MasterPaketTindakan WHERE PaketID = '" & PaketID & "'")
        Dim rowPaket As DataRow = Tbls.Rows(0)

        TextBoxReffID.Text = PaketID
        TextBoxNamaPaket.Text = rowPaket("NamaPaket")
        TextBoxKeteranganPaket.Text = rowPaket("KeteranganPaket")
        TextBoxKuotaPaket.Text = rowPaket("Kuota")
        TextBoxMaksimalBHP_Paket.Text = String.Format("{0:N}", rowPaket("MaksimalBHP"))
        TextBoxHargaAwal.Text = String.Format("{0:N}", rowPaket("HargaAwal"))
        TextBoxTotalDiskon.Text = String.Format("{0:N}", rowPaket("DiskonNominal"))
        TextBoxHargaNett.Text = String.Format("{0:N}", rowPaket("HargaNett"))
        TglAwalPaket.Value = rowPaket("TanggalMulai")
        TglAkhirPaket.Value = rowPaket("TanggalBerakhir")
        TextBoxDurasiAktifPaket.Text = rowPaket("DurasiAktif")

        If Not IsDBNull(rowPaket("PoliTujuan")) Then
            Dim namaPoliGabungan As String = rowPaket("PoliTujuan").ToString()
            Dim daftarPoli() As String = namaPoliGabungan.Split(","c)

            For i As Integer = 0 To CheckedListBoxPoli2.Items.Count - 1
                Dim itemPoli As String = CheckedListBoxPoli2.Items(i).ToString()

                ' Tandai dicentang jika termasuk dalam data poli
                If daftarPoli.Contains(itemPoli) Then
                    CheckedListBoxPoli2.SetItemChecked(i, True)
                End If
            Next
        End If

        ' Set Informed Consent Paket
        If Convert.ToInt32(rowPaket("StatusInformedConsent")) = 1 Then CheckBoxICPaket.Checked = True
        If Convert.ToInt32(rowPaket("StatusInformedConsent")) = 0 Then CheckBoxICPaket.Checked = False
        If IsDBNull(rowPaket("TindakanInformedConsent")) = False Then ComboBoxICPaket.Text = rowPaket("TindakanInformedConsent")

        ' Set Jenis Paket
        If rowPaket("JenisPaket").ToString() = "Paket Reguler" Then
            RadioButtonPaketReguler.Checked = True
            RadioButtonPaketPerTindakan.Enabled = False
            GridViewTindakan.Columns("KuotaTindakan").OptionsColumn.AllowEdit = False
        ElseIf rowPaket("JenisPaket").ToString() = "Paket Per-Tindakan" Then
            RadioButtonPaketPerTindakan.Checked = True
            RadioButtonPaketReguler.Enabled = False
            GridViewTindakan.Columns("KuotaTindakan").OptionsColumn.AllowEdit = True
        End If

        SetDataGrid()
    End Sub

    Private Sub ButtonKalkulasiPaket_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKalkulasiPaket.Click
        KalkulasiNominalPaket()
    End Sub

    Function KalkulasiNominalPaket() As Boolean
        Dim totalHargaAwal As Double = 0
        Dim totalDiskon As Double = 0
        Dim totalHargaNett As Double = 0
        Dim tblTindakan As DataTable = GridControlTindakan.DataSource

        ' Validasi Kuota Paket
        If CDbl(TextBoxKuotaPaket.Value) <= 0 And RadioButtonPaketReguler.Checked = True Then
            MsgBox("Gagal. Kuota paket harus lebih dari 0.", MsgBoxStyle.Exclamation, "Warning!")
            Return False
        End If

        ' Validasi Inputan Tindakan Detail
        If tblTindakan IsNot Nothing AndAlso tblTindakan.Rows.Count > 0 Then
            tblTindakan.AcceptChanges()
            tblTindakan.Columns("DiskonTindakan").ReadOnly = False
            tblTindakan.Columns("GrandTotal").ReadOnly = False

            For Each rowTindakan As DataRow In tblTindakan.Rows
                If CDbl(rowTindakan("NettBayar")) <= 0 Then
                    MessageBox.Show("Nominal harga tindakan " & rowTindakan("NamaTindakan") & " harus lebih dari Rp. 0!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                ElseIf CDbl(rowTindakan("KuotaTindakan")) <= 0 Then
                    MessageBox.Show("KuotaTindakan " & rowTindakan("NamaTindakan") & " harus lebih dari 0!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                ElseIf CDbl(rowTindakan("BiayaTindakan")) < CDbl(rowTindakan("NettBayar")) Then
                    MessageBox.Show("nominal bayar tindakan " & rowTindakan("NamaTindakan") & " melebihi biaya tindakan!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                Else
                    ' Update Nominal Diskon di Grid
                    Dim nominalDiskon As Double = CDbl(rowTindakan("BiayaTindakan")) - CDbl(rowTindakan("NettBayar"))
                    rowTindakan("DiskonTindakan") = nominalDiskon

                    ' Update Nominal Grand Total di Grid
                    Dim grandTotal As Double = CDbl(rowTindakan("NettBayar")) * CDbl(rowTindakan("KuotaTindakan"))
                    rowTindakan("GrandTotal") = grandTotal

                    If RadioButtonPaketReguler.Checked = True Then
                        totalHargaAwal = totalHargaAwal + (rowTindakan("BiayaTindakan") * TextBoxKuotaPaket.Value)
                        totalDiskon = totalDiskon + (rowTindakan("DiskonTindakan") * TextBoxKuotaPaket.Value)
                        totalHargaNett = totalHargaNett + (rowTindakan("NettBayar") * TextBoxKuotaPaket.Value)
                    ElseIf RadioButtonPaketPerTindakan.Checked = True Then
                        totalHargaAwal = totalHargaAwal + (rowTindakan("BiayaTindakan") * rowTindakan("KuotaTindakan"))
                        totalDiskon = totalDiskon + (rowTindakan("DiskonTindakan") * rowTindakan("KuotaTindakan"))
                        totalHargaNett = totalHargaNett + (rowTindakan("NettBayar") * rowTindakan("KuotaTindakan"))
                    End If
                End If
            Next
        End If

        TextBoxHargaAwal.Text = String.Format("{0:N}", totalHargaAwal)
        TextBoxTotalDiskon.Text = String.Format("{0:N}", totalDiskon)
        TextBoxHargaNett.Text = String.Format("{0:N}", totalHargaNett)

        Return True
    End Function

    Private Sub ButtonHapusDetailTindakan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHapusDetailTindakan.Click
        ' Dapatkan baris yang dipilih
        Dim selectedRow As Integer = GridViewTindakan.FocusedRowHandle

        If selectedRow >= 0 Then
            ' Dapatkan nilai ID atau kolom utama dari baris yang dipilih
            Dim tindakanID As String = GridViewTindakan.GetRowCellValue(selectedRow, "TindakanID")

            If MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If TextBoxReffID.Text <> "" Then
                    Dim parameterUpdate As String = ""

                    ' Hapus Data MasterPaketTindakanDetail
                    parameterUpdate = "DeletedBy = '" & userInput & "'," & _
                                      "DeletedDate = GETDATE()"

                    WriteSQL("UPDATE MasterPaketTindakanDetail SET " & parameterUpdate & " WHERE PaketID = '" & TextBoxReffID.Text & "' AND TindakanID = '" & tindakanID & "' AND DeletedDate IS NULL", 4)
                End If

                GridViewTindakan.DeleteRow(selectedRow)
                MsgBox("Data Tindakan Berhasil Dihapus.", MsgBoxStyle.Information, "Success")
            End If
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim jsonData As String = GenerateJsonFromGridControl(GridControlTindakan)

        Dim tblTindakan As DataTable = JsonConvert.DeserializeObject(Of DataTable)(jsonData)

        '' Periksa jika DataTable berisi data
        'If tblTindakan.Rows.Count > 0 Then
        '    DataGridView.DataSource = tblTindakan
        'Else
        '    MessageBox.Show("Tidak ada data JSON yang tersedia.")
        'End If

        MsgBox(jsonData)
    End Sub

    Private Sub RadioButtonAktifFee_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonAktifFee.CheckedChanged
        If RadioButtonAktifFee.Checked = True Then
            SplitContainerFeeTindakan.Panel2Collapsed = False
        ElseIf RadioButtonNonAktifFee.Checked = True Then
            SplitContainerFeeTindakan.Panel2Collapsed = True
        End If
    End Sub

    Private Sub RadioButtonNonAktifFee_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonNonAktifFee.CheckedChanged
        If RadioButtonAktifFee.Checked = True Then
            SplitContainerFeeTindakan.Panel2Collapsed = False
        ElseIf RadioButtonNonAktifFee.Checked = True Then
            SplitContainerFeeTindakan.Panel2Collapsed = True

            ' Clear Field
            TextBoxPersentaseFeePerusahaan.Text = "0"
            TextBoxPersentaseFeeKaryawan.Text = "0"
            TextBoxPersentaseFeePerawat.Text = "0"
            TextBoxNominalFee.Text = "0"
            TextBoxFeeDokterTambahan.Text = "0"
            TextBoxFeePerawatTambahan.Text = "0"
        End If
    End Sub

    Private Sub ButtonFindTindakan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFindTindakan.Click
        Dim stringQuery As String = ""
        Dim stringQueryOrder As String = " ORDER BY TindakanID"

        If RadioButtonPoliUmum.Checked = True Then
            stringQuery = "SELECT TindakanID, NamaTindakan, JenisTindakan, BiayaTindakan, JenisDokter AS 'Pelaksana', CASE WHEN Closed = 0 THEN 'Aktif' WHEN Closed = 1 THEN 'Tidak Aktif' END AS StatusTindakan FROM MasterTindakan"
        ElseIf RadioButtonPoliGigi.Checked = True Then
            stringQuery = "SELECT TindakanID, NamaTindakan, JenisTindakan, BiayaTindakan, JenisDokter AS 'Pelaksana', CASE WHEN Closed = 0 THEN 'Aktif' WHEN Closed = 1 THEN 'Tidak Aktif' END AS StatusTindakan FROM MasterTindakanGigi"
        End If

        Find.Show()
        Find.Query(stringQuery, stringQueryOrder, "FindMasterTindakanKlinik", 2)
    End Sub

    Private Function GetNamaAlat(ByVal alatID As String) As String

        Dim sql As String = ""
        sql = "SELECT NamaAlat FROM MasterAlat WHERE AlatID = '" & alatID & "'"

        Dim dt As DataTable = ReadSQL(sql)

        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)("NamaAlat").ToString()
        Else
            Return ""
        End If

    End Function

    Private Sub LoadDetailAlatFromJSON(ByVal json As String)

        If json = "" Then Exit Sub

        ' Pastikan DataTable sudah ada
        If dtDetailAlat Is Nothing Then
            InitGridAlatTindakan()
        End If

        ' Bersihkan isi lama
        dtDetailAlat.Rows.Clear()

        Dim list As List(Of DetailAlatClass)

        list = JsonConvert.DeserializeObject( _
                Of List(Of DetailAlatClass))(json)

        If list Is Nothing Then Exit Sub

        ' Aktifkan checkbox alat
        CheckBoxPakaiAlat.Checked = True

        Dim item As DetailAlatClass

        For Each item In list

            dtDetailAlat.Rows.Add( _
                item.AlatID, _
                item.NamaAlat, _
                item.CartridgeID, _
                item.NamaCartridge, _
                item.Shoot, _
                item.HargaBeli)

        Next

    End Sub

    Public Sub LoadTindakanPoli(ByRef TindakanID As String)
        ClearDataMenuTindakan()

        Dim tblTindakan As DataTable = New DataTable

        If RadioButtonPoliUmum.Checked = True Then
            tblTindakan = ReadSQL("SELECT * FROM MasterTindakan WHERE TindakanID = '" & TindakanID & "'")
        ElseIf RadioButtonPoliGigi.Checked = True Then
            tblTindakan = ReadSQL("SELECT * FROM MasterTindakanGigi WHERE TindakanID = '" & TindakanID & "'")
        End If

        Dim rowTindakan As DataRow = tblTindakan.Rows(0)

        If Convert.ToInt32(rowTindakan("Closed")) = 1 Then CheckBoxStatusAktifTindakan.Checked = False
        If Convert.ToInt32(rowTindakan("Closed")) = 0 Then CheckBoxStatusAktifTindakan.Checked = True

        TextBoxTindakanID.Text = rowTindakan("TindakanID").ToString
        TextBoxNamaTindakan.Text = rowTindakan("NamaTindakan").ToString
        ComboBoxGroupJenisTindakan.Text = rowTindakan("JenisTindakan").ToString
        ComboBoxKategori.Text = rowTindakan("Kategori").ToString
        If rowTindakan("JenisDokter").ToString = "Dokter" Then RadioButtonPelaksanaDokter.Checked = True
        If rowTindakan("JenisDokter").ToString = "Perawat" Then RadioButtonPelaksanaPerawat.Checked = True
        TextBoxBiayaTindakan.Text = Splitmoney(rowTindakan("BiayaTindakan"))

        'Set Poli
        If Not IsDBNull(rowTindakan("PoliTujuan")) Then
            Dim namaPoliGabungan As String = rowTindakan("PoliTujuan").ToString()
            Dim daftarPoli() As String = namaPoliGabungan.Split(","c)

            For i As Integer = 0 To CheckedListBoxPoli.Items.Count - 1
                Dim itemPoli As String = CheckedListBoxPoli.Items(i).ToString()

                ' Tandai dicentang jika termasuk dalam data poli
                If daftarPoli.Contains(itemPoli) Then
                    CheckedListBoxPoli.SetItemChecked(i, True)
                End If
            Next
        End If

        'Set Alat
        'If Not IsDBNull(rowTindakan("AlatID")) Then
        '    CheckBoxPakaiAlat.Checked = True
        '    TextBoxAlatIdTindakan.Text = rowTindakan("AlatID").ToString()
        '    TextBoxNamaAlatTindakan.Text = GetNamaAlat(rowTindakan("AlatID").ToString())
        '    NumericShootPerTindakan.Value = CInt(rowTindakan("ShootPerTindakan"))
        'Else
        '    CheckBoxPakaiAlat.Checked = False
        'End If


        'Set Reminder
        If Convert.ToInt32(rowTindakan("Reminder")) = 1 Then CheckBoxReminder.Checked = True
        If Convert.ToInt32(rowTindakan("Reminder")) = 0 Then CheckBoxReminder.Checked = False
        If IsDBNull(rowTindakan("Hari")) = False Then TextBoxHariReminder.Text = rowTindakan("Hari")

        ' Set Informed Consent
        If Convert.ToInt32(rowTindakan("StatusInformedConsent")) = 1 Then CheckBoxInformedConsent.Checked = True
        If Convert.ToInt32(rowTindakan("StatusInformedConsent")) = 0 Then CheckBoxInformedConsent.Checked = False
        If IsDBNull(rowTindakan("TindakanInformedConsent")) = False Then ComboBoxInformedConsent.Text = rowTindakan("TindakanInformedConsent")

        ' Set Status Aktif Fee Tindakan
        If Convert.ToInt32(rowTindakan("StatusFeeTindakan")) = 1 Then RadioButtonAktifFee.Checked = True
        If Convert.ToInt32(rowTindakan("StatusFeeTindakan")) = 0 Then RadioButtonNonAktifFee.Checked = True

        ' Set Status Persentase / Nominal
        If Convert.ToInt32(rowTindakan("StatusPersentaseFee")) = 1 Then RadioButtonOpsiPersentase.Checked = True
        If Convert.ToInt32(rowTindakan("StatusNominalFee")) = 1 Then RadioButtonOpsiNominal.Checked = True

        ' Set Status BHP
        If Convert.ToInt32(rowTindakan("StatusBHP")) = 1 Then RadioButtonBHP.Checked = True
        If Convert.ToInt32(rowTindakan("StatusBHP")) = 0 Then RadioButtonNonBHP.Checked = True

        ' Set Value
        If IsDBNull(rowTindakan("PersentaseFeePerusahaan")) = False Then TextBoxPersentaseFeePerusahaan.Text = rowTindakan("PersentaseFeePerusahaan")
        If IsDBNull(rowTindakan("PersentaseFeeKaryawan")) = False Then TextBoxPersentaseFeeKaryawan.Text = rowTindakan("PersentaseFeeKaryawan")
        If IsDBNull(rowTindakan("PersentaseFeePerawat")) = False Then TextBoxPersentaseFeePerawat.Text = rowTindakan("PersentaseFeePerawat")
        If IsDBNull(rowTindakan("FeePerTindakan")) = False Then TextBoxNominalFee.Text = Splitmoney(rowTindakan("FeePerTindakan"))
        If IsDBNull(rowTindakan("MaksimalBHP")) = False Then TextBoxMaksimalBHP_Tindakan.Text = Splitmoney(rowTindakan("MaksimalBHP"))

        If IsDBNull(rowTindakan("FeeDokter")) = False Then TextBoxFeeDokterTambahan.Text = Splitmoney(rowTindakan("FeeDokter"))
        If IsDBNull(rowTindakan("FeePerawat")) = False Then TextBoxFeePerawatTambahan.Text = Splitmoney(rowTindakan("FeePerawat"))

        If Not IsDBNull(rowTindakan("DetailAlat")) Then
            Dim json As String = rowTindakan("DetailAlat").ToString()
            LoadDetailAlatFromJSON(json)
        End If
    End Sub

    Private Sub ClearDataMenuTindakan()
        SplitContainerFeeTindakan.Panel2Collapsed = True

        TextBoxTindakanID.ResetText()
        TextBoxNamaTindakan.ResetText()

        CheckBoxInformedConsent.Checked = False
        RadioButtonPelaksanaDokter.Checked = True
        RadioButtonNonAktifFee.Checked = True
        RadioButtonOpsiPersentase.Checked = True
        RadioButtonNonBHP.Checked = True
        CheckBoxStatusAktifTindakan.Checked = True

        TextBoxBiayaTindakan.Text = "0"
        TextBoxFeeDokterTambahan.Text = "0"
        TextBoxFeePerawatTambahan.Text = "0"
        TextBoxMaksimalBHP_Tindakan.Text = "0"
        TextBoxPersentaseFeePerusahaan.Text = "0"
        TextBoxPersentaseFeeKaryawan.Text = "0"
        TextBoxPersentaseFeePerawat.Text = "0"
        TextBoxNominalFee.Text = "0"

        For i As Integer = 0 To CheckedListBoxPoli.Items.Count - 1
            CheckedListBoxPoli.SetItemChecked(i, False)
        Next
        CheckedListBoxPoli.Enabled = True

        CheckBoxReminder.Checked = False
        CheckBoxAlatAktif.Checked = False
        TextBoxHariReminder.ResetText()
        TextBoxHariReminder.ReadOnly = True

        If RadioButtonPoliUmum.Checked = True Then
            InsertNewItemComboSQL("SELECT DISTINCT JenisTindakan FROM MasterTindakan", ComboBoxGroupJenisTindakan)
        ElseIf RadioButtonPoliGigi.Checked = True Then
            InsertNewItemComboSQL("SELECT DISTINCT JenisTindakan FROM MasterTindakanGigi", ComboBoxGroupJenisTindakan)
        End If

        InsertNewItemComboSQL("SELECT DISTINCT NamaTindakan FROM Klinik_MasterTindakanInformedConsent", ComboBoxInformedConsent)

        ComboBoxGroupJenisTindakan.SelectedIndex = -1
        ComboBoxInformedConsent.SelectedIndex = -1
        ComboBoxKategori.SelectedIndex = -1
        ComboBoxInformedConsent.Enabled = False

        If dtDetailAlat IsNot Nothing Then
            dtDetailAlat.Rows.Clear()
        End If

        CheckBoxPakaiAlat.Checked = False
        NumericShootPerTindakan.Value = 1
    End Sub

    Private Sub LoadPoliData()
        CheckedListBoxPoli.Items.Clear()
        Dim poli As DataTable = ReadSQL("SELECT * FROM MasterPoliTujuan WHERE PoliSakit = 'True'")
        If poli IsNot Nothing Then
            For Each TrowPoliTujuan As DataRow In poli.Rows
                CheckedListBoxPoli.Items.Add(TrowPoliTujuan("NamaPoli").ToString())
            Next
        End If
    End Sub

    Private Sub LoadPoliDataPaket()
        CheckedListBoxPoli2.Items.Clear()
        Dim poli As DataTable = ReadSQL("SELECT * FROM MasterPoliTujuan WHERE PoliSakit = 'True'")
        If poli IsNot Nothing Then
            For Each TrowPoliTujuan As DataRow In poli.Rows
                CheckedListBoxPoli2.Items.Add(TrowPoliTujuan("NamaPoli").ToString())
            Next
        End If
    End Sub

    Private Function GenerateDetailTindakanJSON() As String

        If dtDetailAlat Is Nothing Then Return "[]"
        If dtDetailAlat.Rows.Count = 0 Then Return "[]"

        Dim list As New List(Of Object)

        For Each row As DataRow In dtDetailAlat.Rows

            list.Add(New With { _
                .AlatID = row("AlatID"), _
                .NamaAlat = row("NamaAlat"), _
                .CartridgeID = row("CartridgeID"), _
                .NamaCartridge = row("NamaCartridge"), _
                .Shoot = row("Shoot"), _
                .HargaBeli = row("HargaBeli") _
            })

        Next

        Return JsonConvert.SerializeObject(list)

    End Function

    Private Sub SimpanDataTindakan()
        Dim statusFee As Boolean = False
        Dim statusPersentase As Boolean = False
        Dim statusNominal As Boolean = False
        Dim statusBHP As Boolean = False
        Dim statusFeeKonsultasi As Boolean = False
        Dim statusInformedConsent As Boolean = False
        Dim jenisPelaksana As String = ""
        Dim detailAlat As String = GenerateDetailTindakanJSON().Replace("'", "''")

        ' Set Jenis Pelaksana
        If RadioButtonPelaksanaDokter.Checked = True Then
            jenisPelaksana = "Dokter"
        ElseIf RadioButtonPelaksanaPerawat.Checked = True Then
            jenisPelaksana = "Perawat"
        End If

        'Reminder
        Dim statusReminder As Integer = If(CheckBoxReminder.Checked, 1, 0)
        Dim hari As String

        If TextBoxHariReminder.Text.Trim() = "" Then
            hari = "NULL"
        Else
            hari = "'" & TextBoxHariReminder.Text.Trim().Replace("'", "''") & "'"
        End If

        'Filter Poli
        'If RadioButtonPelaksanaDokter.Checked Then
        '    jenisPelaksana = "Dokter"
        'ElseIf RadioButtonPelaksanaPerawat.Checked Then
        '    jenisPelaksana = "Perawat"
        'Else
        '    MessageBox.Show("Silakan pilih jenis pelaksana (Dokter atau Perawat).", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If
        Dim poliList As New List(Of String)
        For Each item As String In CheckedListBoxPoli.CheckedItems
            poliList.Add(item)
        Next

        Dim poliGabung As String = String.Join(",", poliList.ToArray())

        ' Set Status BHP
        If RadioButtonBHP.Checked = True Then statusBHP = True

        ' Set Status Informed Consent
        If CheckBoxInformedConsent.Checked = True Then statusInformedConsent = True

        ' Set Status Aktif Fee Tindakan
        If RadioButtonAktifFee.Checked = True Then statusFee = True

        If statusFee = True And ComboBoxGroupJenisTindakan.Text.Contains("Konsultasi") = False Then
            ' Set Status Persentase / Nominal
            If RadioButtonOpsiPersentase.Checked = True Then statusPersentase = True
            If RadioButtonOpsiNominal.Checked = True Then statusNominal = True
        Else
            statusPersentase = False
            statusNominal = False
            statusFeeKonsultasi = True
        End If

        Dim kategori As String = ComboBoxKategori.SelectedItem.ToString()

        If TextBoxTindakanID.Text = "" Then
            ' Insert Data Baru 
            If RadioButtonPoliUmum.Checked = True Then
                WriteSQL("INSERT INTO MasterTindakan ([NamaTindakan], [JenisTindakan], [BiayaTindakan], [JenisDokter], [CreatedDate], [CreatedBy], [StatusFeeTindakan], [StatusBHP], [StatusPersentaseFee], [StatusNominalFee], [PersentaseFeePerusahaan], [PersentaseFeeKaryawan], [PersentaseFeePerawat], [FeePerTindakan], [FeeDokter], [FeePerawat], [StatusFeeKonsultasi], [MaksimalBHP], [StatusInformedConsent], [TindakanInformedConsent], [Hari], [Reminder], [PoliTujuan], [Kategori], [DetailAlat]) " & _
                         "VALUES('" & TextBoxNamaTindakan.Text & "','" & ComboBoxGroupJenisTindakan.Text & "'," & CInt(TextBoxBiayaTindakan.Text) & ", '" & jenisPelaksana & "', GETDATE(), '" & userInput & "', '" & statusFee & "', '" & statusBHP & "', '" & statusPersentase & "', " & _
                         "'" & statusNominal & "', '" & CDbl(TextBoxPersentaseFeePerusahaan.Text) & "', '" & CDbl(TextBoxPersentaseFeeKaryawan.Text) & "', '" & CDbl(TextBoxPersentaseFeePerawat.Text) & "', '" & CDbl(TextBoxNominalFee.Text) & "', '" & CDbl(TextBoxFeeDokterTambahan.Text) & "', '" & CDbl(TextBoxFeePerawatTambahan.Text) & "', '" & statusFeeKonsultasi & "', '" & CDbl(TextBoxMaksimalBHP_Tindakan.Text) & "', '" & statusInformedConsent & "', '" & ComboBoxInformedConsent.Text & "', '" & hari & "', " & statusReminder & ", '" & poliGabung & "', '" & kategori & "', '" & detailAlat & "')", 4)
            ElseIf RadioButtonPoliGigi.Checked = True Then
                WriteSQL("INSERT INTO MasterTindakanGigi ([NamaTindakan], [JenisTindakan], [BiayaTindakan], [JenisDokter], [CreatedDate], [CreatedBy], [StatusFeeTindakan], [StatusBHP], [StatusPersentaseFee], [StatusNominalFee], [PersentaseFeePerusahaan], [PersentaseFeeKaryawan], [FeePerTindakan], [FeeDokter], [FeePerawat], [StatusFeeKonsultasi], [MaksimalBHP], [StatusInformedConsent], [TindakanInformedConsent], [Hari], [Reminder], [PoliTujuan]) " & _
                     "VALUES('" & TextBoxNamaTindakan.Text & "', '" & ComboBoxGroupJenisTindakan.Text & "', " & CInt(TextBoxBiayaTindakan.Text) & ", '" & jenisPelaksana & "', GETDATE(), '" & userInput & "', '" & statusFee & "', '" & statusBHP & "', '" & statusPersentase & "', " & _
                     "'" & statusNominal & "', '" & CDbl(TextBoxPersentaseFeePerusahaan.Text) & "', '" & CDbl(TextBoxPersentaseFeeKaryawan.Text) & "', '" & CDbl(TextBoxNominalFee.Text) & "', '" & CDbl(TextBoxFeeDokterTambahan.Text) & "', '" & CDbl(TextBoxFeePerawatTambahan.Text) & "', '" & statusFeeKonsultasi & "', '" & CDbl(TextBoxMaksimalBHP_Tindakan.Text) & "', '" & statusInformedConsent & "', '" & ComboBoxInformedConsent.Text & "', '" & hari & "', " & statusReminder & ", '" & poliGabung & "')", 4)
            End If

            ClearDataMenuTindakan()
            MsgBox("Berhasil Menyimpan Data.", MsgBoxStyle.Information, "Success!")
        Else
            Dim parameterUpdate As String = ""
            parameterUpdate = "NamaTindakan = '" & TextBoxNamaTindakan.Text & "'," & _
                              "JenisTindakan = '" & ComboBoxGroupJenisTindakan.Text & "'," & _
                              "BiayaTindakan = " & CInt(TextBoxBiayaTindakan.Text) & "," & _
                              "JenisDokter = '" & jenisPelaksana & "'," & _
                              "StatusInformedConsent = '" & statusInformedConsent & "'," & _
                              "TindakanInformedConsent = '" & ComboBoxInformedConsent.Text & "'," & _
                              "StatusFeeTindakan = '" & statusFee & "'," & _
                              "StatusBHP = '" & statusBHP & "'," & _
                              "StatusPersentaseFee = '" & statusPersentase & "'," & _
                              "StatusNominalFee = '" & statusNominal & "'," & _
                              "PersentaseFeePerusahaan = '" & CDbl(TextBoxPersentaseFeePerusahaan.Text) & "'," & _
                              "PersentaseFeeKaryawan = '" & CDbl(TextBoxPersentaseFeeKaryawan.Text) & "'," & _
                              "PersentaseFeePerawat = '" & CDbl(TextBoxPersentaseFeePerawat.Text) & "'," & _
                              "FeePerTindakan = '" & CDbl(TextBoxNominalFee.Text) & "'," & _
                              "FeeDokter = '" & CDbl(TextBoxFeeDokterTambahan.Text) & "'," & _
                              "FeePerawat = '" & CDbl(TextBoxFeePerawatTambahan.Text) & "'," & _
                              "StatusFeeKonsultasi = '" & statusFeeKonsultasi & "'," & _
                              "MaksimalBHP = '" & CDbl(TextBoxMaksimalBHP_Tindakan.Text) & "'," & _
                              "Hari = " & hari & "," & _
                              "Reminder = '" & statusReminder & "'," & _
                              "PoliTujuan = '" & poliGabung & "'," & _
                              "UpdatedBy = '" & userInput & "'," & _
                              "Kategori = '" & kategori & "'," & _
                              "DetailAlat = '" & detailAlat & "'," & _
                              "UpdatedDate = GETDATE()"

            ' Insert Data Baru 
            If RadioButtonPoliUmum.Checked = True Then
                WriteSQL("UPDATE MasterTindakan SET " & parameterUpdate & " WHERE TindakanID = '" & TextBoxTindakanID.Text & "'", 4)
            ElseIf RadioButtonPoliGigi.Checked = True Then
                WriteSQL("UPDATE MasterTindakanGigi SET " & parameterUpdate & " WHERE TindakanID = '" & TextBoxTindakanID.Text & "'", 4)
            End If

            ClearDataMenuTindakan()
            MsgBox("Berhasil Menyimpan Data.", MsgBoxStyle.Information, "Success!")
        End If
    End Sub

    Function ValidasiTindakan() As Boolean
        ' Validasi Nama Tindakan
        If TextBoxNamaTindakan.Text.Trim = "" Then MsgBox("Nama tindakan harus diisi.", MsgBoxStyle.Exclamation, "Warning!") : Return False

        ' Validasi jenis tindakan
        If ComboBoxGroupJenisTindakan.FindStringExact(ComboBoxGroupJenisTindakan.Text) < 0 Then
            If MsgBox("Tindakan group menu baru ?", MsgBoxStyle.YesNoCancel) <> MsgBoxResult.Yes Then
                MsgBox("Pilih tindakan group yang sesuai.", MsgBoxStyle.Exclamation, "Warning!") : Return False
            End If
        End If

        If ComboBoxKategori.SelectedIndex = -1 Then
            MsgBox("Silakan pilih kategori terlebih dahulu!", _
                   MsgBoxStyle.Exclamation, "Peringatan")
            ComboBoxKategori.Focus()
            Exit Function
        End If

        ' Validasi Nama Poli
        If RadioButtonPoliUmum.Checked = True AndAlso CheckedListBoxPoli.CheckedItems.Count = 0 Then
            MsgBox("Silakan pilih minimal satu poli.", MsgBoxStyle.Exclamation, "Warning")
            Exit Function
        End If

        ' Validasi biaya tindakan kurang dari 0
        'If CDbl(TextBoxBiayaTindakan.Text) <= 0 Then MsgBox("Biaya harus lebih besar dari 0.", MsgBoxStyle.Exclamation, "Warning!") : Return False

        ' Validasi informed consent
        If CheckBoxInformedConsent.Checked = True Then
            If ComboBoxInformedConsent.FindStringExact(ComboBoxInformedConsent.Text) < 0 Then
                MsgBox("Pilih kelompok informed consent terlebih dahulu.", MsgBoxStyle.Exclamation, "Warning!") : Return False
            End If
        End If

        ' Validasi informed consent paket
        If CheckBoxICPaket.Checked = True Then
            If ComboBoxICPaket.FindStringExact(ComboBoxICPaket.Text) < 0 Then
                MsgBox("Pilih kelompok informed consent terlebih dahulu.", MsgBoxStyle.Exclamation, "Warning!") : Return False
            End If
        End If

        ' Validasi Maksimal BHP jika memilih BHP
        If RadioButtonBHP.Checked = True And CDbl(TextBoxMaksimalBHP_Tindakan.Text) = 0 And ComboBoxGroupJenisTindakan.Text.Contains("Konsultasi") = False Then
            MsgBox("Isi maksimal BHP terlebih dahulu.", MsgBoxStyle.Exclamation, "Warning!") : Return False
        End If

        ' Validasi Fee Tindakan
        If RadioButtonAktifFee.Checked = True And ComboBoxGroupJenisTindakan.Text.Contains("Konsultasi") = False Then
            ' Validasi Persentase & Nominal Tindakan
            If RadioButtonOpsiPersentase.Checked = True And (CDbl(TextBoxPersentaseFeeKaryawan.Text) = 0 Or CDbl(TextBoxPersentaseFeePerusahaan.Text) = 0) Then
                MsgBox("Lengkapi nilai persentase terlebih dahulu.", MsgBoxStyle.Exclamation, "Warning!") : Return False
            ElseIf RadioButtonOpsiNominal.Checked = True And CDbl(TextBoxNominalFee.Text) = 0 Then
                MsgBox("Lengkapi nilai fee tindakan terlebih dahulu.", MsgBoxStyle.Exclamation, "Warning!") : Return False
            End If
        End If

        ' Validasi nama tindakan sudah ada atau belum
        If TextBoxTindakanID.Text <> "" Then
            Dim jenisPelaksana As String = ""

            ' Set Jenis Pelaksana
            If RadioButtonPelaksanaDokter.Checked = True Then
                jenisPelaksana = "Dokter"
            ElseIf RadioButtonPelaksanaPerawat.Checked = True Then
                jenisPelaksana = "Perawat"
            End If

            If RadioButtonPoliUmum.Checked = True Then
                If CheckSQL("SELECT * FROM MasterTindakan WHERE Closed = 0 AND [NamaTindakan]='" & TextBoxNamaTindakan.Text & "' AND [JenisTindakan]='" & ComboBoxGroupJenisTindakan.Text & "' AND JenisDokter = '" & jenisPelaksana & "' AND TindakanID NOT IN ('" & TextBoxTindakanID.Text & "')") = True Then
                    MsgBox("Sudah ada 'Nama Tindakan' dan 'Jenis Tindakan' yang sama. Mohon check kembali.", MsgBoxStyle.Exclamation, "Warning!")
                    Return False
                End If
            ElseIf RadioButtonPoliGigi.Checked = True Then
                If CheckSQL("SELECT * FROM MasterTindakanGigi WHERE Closed = 0 AND [NamaTindakan]='" & TextBoxNamaTindakan.Text & "' AND [JenisTindakan]='" & ComboBoxGroupJenisTindakan.Text & "' AND JenisDokter = '" & jenisPelaksana & "' AND TindakanID NOT IN ('" & TextBoxTindakanID.Text & "')") = True Then
                    MsgBox("Sudah ada 'Nama Tindakan' dan 'Jenis Tindakan' yang sama. Mohon check kembali.", MsgBoxStyle.Exclamation, "Warning!")
                    Return False
                End If
            End If
        End If

        Return True
    End Function

    Private Sub CheckBoxStatusAktifTindakan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxStatusAktifTindakan.CheckedChanged
        If CheckBoxStatusAktifTindakan.CheckState = CheckState.Unchecked And TextBoxTindakanID.Text <> "" Then
            If RadioButtonPoliUmum.Checked = True Then
                If CheckSQL("SELECT * FROM MasterTindakan WHERE TindakanID = '" & TextBoxTindakanID.Text & "' AND Closed = 0") = True Then
                    If MsgBox("Yakin Tindakan diclosed ? ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        WriteSQL("UPDATE MasterTindakan SET Closed = 1, ClosedBy = '" & userInput & "', ClosedDate = GETDATE() WHERE TindakanID = '" & TextBoxTindakanID.Text & "'", 4)
                        MsgBox("Tindakan berhasil diclosed.", MsgBoxStyle.Information, "Berhasil")

                    Else
                        CheckBoxStatusAktifTindakan.Checked = True
                    End If
                End If
            ElseIf RadioButtonPoliGigi.Checked = True Then
                If CheckSQL("SELECT * FROM MasterTindakanGigi WHERE TindakanID = '" & TextBoxTindakanID.Text & "' AND Closed = 0") = True Then
                    If MsgBox("Yakin Tindakan diclosed ? ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        WriteSQL("UPDATE MasterTindakanGigi SET Closed = 1, ClosedBy = '" & userInput & "', ClosedDate = GETDATE() WHERE TindakanID = '" & TextBoxTindakanID.Text & "'", 4)
                        MsgBox("Tindakan berhasil diclosed.", MsgBoxStyle.Information, "Berhasil")
                    Else
                        CheckBoxStatusAktifTindakan.Checked = True
                    End If
                End If
            End If
        ElseIf CheckBoxStatusAktifTindakan.CheckState = CheckState.Checked And TextBoxTindakanID.Text <> "" Then
            If RadioButtonPoliUmum.Checked = True Then
                If CheckSQL("SELECT * FROM MasterTindakan WHERE TindakanID = '" & TextBoxTindakanID.Text & "' AND Closed = 1") = True Then
                    If MsgBox("Yakin Tindakan diunclosed ? ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        WriteSQL("UPDATE MasterTindakan SET Closed = 0, ClosedBy = NULL, ClosedDate = NULL, UpdatedBy = '" & userInput & "', UpdatedDate = GETDATE() WHERE TindakanID = '" & TextBoxTindakanID.Text & "'", 4)
                        MsgBox("Tindakan berhasil diunclosed.", MsgBoxStyle.Information, "Berhasil")
                    Else
                        CheckBoxStatusAktifTindakan.Checked = False
                    End If
                End If
            ElseIf RadioButtonPoliGigi.Checked = True Then
                If CheckSQL("SELECT * FROM MasterTindakanGigi WHERE TindakanID = '" & TextBoxTindakanID.Text & "' AND Closed = 1") = True Then
                    If MsgBox("Yakin Tindakan diunclosed ? ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        WriteSQL("UPDATE MasterTindakanGigi SET Closed = 0, ClosedBy = NULL, ClosedDate = NULL, UpdatedBy = '" & userInput & "', UpdatedDate = GETDATE() WHERE TindakanID = '" & TextBoxTindakanID.Text & "'", 4)
                        MsgBox("Tindakan berhasil diunclosed.", MsgBoxStyle.Information, "Berhasil")
                    Else
                        CheckBoxStatusAktifTindakan.Checked = False
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub CheckBoxInformedConsent_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxInformedConsent.CheckedChanged
        If CheckBoxInformedConsent.CheckState = CheckState.Checked Then
            ComboBoxInformedConsent.SelectedIndex = -1
            ComboBoxInformedConsent.Enabled = True
        ElseIf CheckBoxInformedConsent.CheckState = CheckState.Unchecked Then
            ComboBoxInformedConsent.SelectedIndex = -1
            ComboBoxInformedConsent.Enabled = False
        End If
    End Sub

    Private Sub CheckBoxICPaket_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxICPaket.CheckedChanged
        If CheckBoxICPaket.CheckState = CheckState.Checked Then
            ComboBoxICPaket.SelectedIndex = -1
            ComboBoxICPaket.Enabled = True
        ElseIf CheckBoxICPaket.CheckState = CheckState.Unchecked Then
            ComboBoxICPaket.SelectedIndex = -1
            ComboBoxICPaket.Enabled = False
        End If
    End Sub

    Private Sub RadioButtonOpsiPersentaseOrNominal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonOpsiPersentase.CheckedChanged, RadioButtonOpsiNominal.CheckedChanged
        If RadioButtonOpsiPersentase.Checked = True Then
            TextBoxPersentaseFeePerusahaan.Enabled = True
            TextBoxPersentaseFeeKaryawan.Enabled = True
            TextBoxPersentaseFeePerawat.Enabled = True
            TextBoxNominalFee.Enabled = False
            TextBoxNominalFee.Text = "0"
        ElseIf RadioButtonOpsiNominal.Checked = True Then
            TextBoxPersentaseFeePerusahaan.Enabled = False
            TextBoxPersentaseFeeKaryawan.Enabled = False
            TextBoxPersentaseFeePerawat.Enabled = False
            TextBoxNominalFee.Enabled = True
            TextBoxPersentaseFeeKaryawan.Text = "0"
            TextBoxPersentaseFeePerusahaan.Text = "0"
            TextBoxPersentaseFeePerawat.Text = "0"
        End If
    End Sub

    Private Sub RadioButtonBHP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonBHP.CheckedChanged, RadioButtonNonBHP.CheckedChanged
        If RadioButtonBHP.Checked = True Then
            TextBoxMaksimalBHP_Tindakan.Enabled = True
            TextBoxMaksimalBHP_Tindakan.Text = "0"
        ElseIf RadioButtonNonBHP.Checked = True Then
            TextBoxMaksimalBHP_Tindakan.Enabled = False
            TextBoxMaksimalBHP_Tindakan.Text = "0"
        End If
    End Sub

    Private Sub RadioButtonPaketReguler_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonPaketReguler.CheckedChanged
        ' Tampilkan Label dan TextBox untuk Kuota Paket
        LabelKuotaPaket.Visible = True
        TextBoxKuotaPaket.Visible = True
        TextBoxKuotaPaket.Value = 0

        ' Ambil DataTable dari GridControl
        Dim tblTindakan As DataTable = TryCast(GridControlTindakan.DataSource, DataTable)

        ' Pastikan DataTable tidak kosong
        If tblTindakan IsNot Nothing Then
            ' Nonaktifkan kolom KuotaTindakan di GridView
            GridViewTindakan.Columns("KuotaTindakan").OptionsColumn.AllowEdit = False

            If tblTindakan.Rows.Count > 0 Then
                ' Iterasi semua baris di DataTable
                For Each row As DataRow In tblTindakan.Rows
                    ' Set nilai KuotaTindakan menjadi 1
                    row("KuotaTindakan") = 1
                Next

                ' Refresh GridControl untuk menampilkan perubahan
                GridControlTindakan.RefreshDataSource()
            End If
        End If
    End Sub

    Private Sub RadioButtonPaketPerTindakan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonPaketPerTindakan.CheckedChanged
        ' Tampilkan Label dan TextBox untuk Kuota Paket
        LabelKuotaPaket.Visible = False
        TextBoxKuotaPaket.Visible = False
        TextBoxKuotaPaket.Value = 0

        ' Aktifkan Kolom KuotaTindakan
        GridViewTindakan.Columns("KuotaTindakan").OptionsColumn.AllowEdit = True
    End Sub

    Private Sub ButtonExportMasterTindakan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExportMasterTindakan.Click
        Dim queryStringTindakan As String = ""

        If RadioButtonPoliUmum.Checked = True Then
            queryStringTindakan = "SELECT TindakanID, NamaTindakan, JenisTindakan, BiayaTindakan, JenisDokter AS 'Pelaksana', StatusFeeTindakan, FeeDokter, FeePerawat, StatusBHP, MaksimalBHP, PersentaseFeePerusahaan, PersentaseFeeKaryawan, StatusPersentaseFee, StatusNominalFee, FeePerTindakan, StatusFeeKonsultasi, TindakanInformedConsent, Hari, Reminder, PoliTujuan, Kategori FROM MasterTindakan WHERE Closed = 0"
        ElseIf RadioButtonPoliGigi.Checked = True Then
            queryStringTindakan = "SELECT TindakanID, NamaTindakan, JenisTindakan, BiayaTindakan, JenisDokter AS 'Pelaksana', StatusFeeTindakan, FeeDokter, FeePerawat, StatusBHP, MaksimalBHP, PersentaseFeePerusahaan, PersentaseFeeKaryawan, StatusPersentaseFee, StatusNominalFee, FeePerTindakan, StatusFeeKonsultasi, TindakanInformedConsent, Hari, Reminder, PoliTujuan, Kategori FROM MasterTindakanGigi WHERE Closed = 0"
        End If

        Dim tblTindakan As DataTable = ReadSQL(queryStringTindakan)

        If tblTindakan.Rows.Count > 0 Then
            Dim saveFileDialog1 As New SaveFileDialog
            saveFileDialog1.Filter = "CSV Files|*.csv"
            saveFileDialog1.Title = "Save CSV File"
            If saveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Try
                    Using sw As New System.IO.StreamWriter(saveFileDialog1.FileName, False, System.Text.Encoding.UTF8)
                        ' Tulis header kolom
                        Dim columnNames As List(Of String) = New List(Of String)
                        For Each col As DataColumn In tblTindakan.Columns
                            columnNames.Add("""" & col.ColumnName & """")
                        Next
                        sw.WriteLine(String.Join(",", columnNames.ToArray()))

                        ' Tulis data baris
                        For Each row As DataRow In tblTindakan.Rows
                            Dim fields As New List(Of String)
                            For Each col As DataColumn In tblTindakan.Columns
                                fields.Add("""" & row(col).ToString().Replace("""", """""") & """")
                            Next
                            sw.WriteLine(String.Join(",", fields.ToArray()))
                        Next
                    End Using
                    MsgBox("CSV berhasil disimpan.", MsgBoxStyle.Information)
                Catch ex As Exception
                    MsgBox("Gagal menyimpan file: " & ex.Message, MsgBoxStyle.Critical)
                End Try
            End If
        Else
            MsgBox("Tidak ada data untuk diekspor.", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub ButtonSimpanMasterTindakan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSimpanMasterTindakan.Click
        Try
            If ValidasiTindakan() = True Then
                SimpanDataTindakan()
                ClearDataMenuTindakan()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Warning!")
        End Try
    End Sub

    Private dtDetailAlat As DataTable

    Private Sub InitGridAlatTindakan()

        dtDetailAlat = New DataTable

        dtDetailAlat.Columns.Add("AlatID", GetType(Integer))
        dtDetailAlat.Columns.Add("NamaAlat", GetType(String))
        dtDetailAlat.Columns.Add("CartridgeID", GetType(Integer))
        dtDetailAlat.Columns.Add("NamaCartridge", GetType(String))
        dtDetailAlat.Columns.Add("Shoot", GetType(Integer))
        dtDetailAlat.Columns.Add("HargaBeli", GetType(Decimal))

        DataGridViewAlatTindakan.DataSource = dtDetailAlat

    End Sub

    Private Sub ButtonTambahAlat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTambahAlat.Click
        If dtDetailAlat Is Nothing Then
            InitGridAlatTindakan()
        End If

        ' ===== VALIDASI =====
        If ComboBoxAlatTindakan.SelectedIndex = -1 Then
            MsgBox("Pilih alat terlebih dahulu")
            Exit Sub
        End If

        If ComboBoxCartridgeTindakan.SelectedIndex = -1 Then
            MsgBox("Pilih cartridge terlebih dahulu")
            Exit Sub
        End If

        Dim shoot As Integer = CInt(NumericShootPerTindakan.Value)

        If shoot <= 0 Then
            MsgBox("Shoot harus lebih dari 0")
            Exit Sub
        End If

        ' ===== AMBIL DATA =====
        Dim alatID As Integer = CInt(ComboBoxAlatTindakan.SelectedValue)
        Dim namaAlat As String = ComboBoxAlatTindakan.Text

        Dim cartridgeID As Integer = CInt(ComboBoxCartridgeTindakan.SelectedValue)
        Dim namaCartridge As String = ComboBoxCartridgeTindakan.Text

        ' --- MULAI PENGAMBILAN & PERHITUNGAN HARGA BELI ---
        ' Ambil row yang sedang dipilih di ComboBox Cartridge
        Dim drv As DataRowView = DirectCast(ComboBoxCartridgeTindakan.SelectedItem, DataRowView)
        Dim hargaBeliSatuan As Decimal = CDec(drv("HargaBeli"))

        ' Kalikan harga beli satuan dengan jumlah shoot
        Dim totalHargaBeli As Decimal = hargaBeliSatuan * shoot
        ' --- AKHIR PENGAMBILAN & PERHITUNGAN HARGA BELI ---

        ' ===== CEK DUPLIKAT =====
        For Each row As DataRow In dtDetailAlat.Rows
            If CInt(row("CartridgeID")) = cartridgeID Then
                MsgBox("Cartridge sudah ditambahkan.")
                Exit Sub
            End If
        Next

        ' ===== TAMBAH KE GRID =====
        dtDetailAlat.Rows.Add(alatID, namaAlat, cartridgeID, namaCartridge, shoot, totalHargaBeli)

        NumericShootPerTindakan.Value = 1
        ComboBoxCartridgeTindakan.SelectedIndex = -1
        ComboBoxCartridgeTindakan.Focus()
    End Sub

    Private Sub ButtonClearMasterTindakan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClearMasterTindakan.Click
        ClearDataMenuTindakan()
    End Sub

    Private Sub ButtonSimpanMasterPaket_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSimpanMasterPaket.Click
        Try
            If ValidasiDetailPaket() = True Then
                SimpanDataPaket()
                ClearDataMenuPaket()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Warning!")
        End Try
    End Sub

    Private Sub ButtonClearMasterPaket_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClearMasterPaket.Click
        ClearDataMenuPaket()
    End Sub

    Private Sub ButtonExportMasterPaket_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExportMasterPaket.Click
        Dim queryStringPaket As String = ""

        queryStringPaket = "SELECT PaketID, NamaPaket, KeteranganPaket, JenisPaket, HargaNett, Kuota, TanggalMulai, TanggalBerakhir, PoliTujuan FROM MasterPaketTindakan WHERE DeletedDate IS NULL"

        Dim tblPaket As DataTable = ReadSQL(queryStringPaket)

        If tblPaket.Rows.Count > 0 Then
            Dim saveFileDialog1 As New SaveFileDialog
            saveFileDialog1.Filter = "CSV Files|*.csv"
            saveFileDialog1.Title = "Save CSV File"
            If saveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Try
                    Using sw As New System.IO.StreamWriter(saveFileDialog1.FileName, False, System.Text.Encoding.UTF8)
                        ' Tulis header kolom
                        Dim columnNames As List(Of String) = New List(Of String)
                        For Each col As DataColumn In tblPaket.Columns
                            columnNames.Add("""" & col.ColumnName & """")
                        Next
                        sw.WriteLine(String.Join(",", columnNames.ToArray()))

                        ' Tulis data baris
                        For Each row As DataRow In tblPaket.Rows
                            Dim fields As New List(Of String)
                            For Each col As DataColumn In tblPaket.Columns
                                fields.Add("""" & row(col).ToString().Replace("""", """""") & """")
                            Next
                            sw.WriteLine(String.Join(",", fields.ToArray()))
                        Next
                    End Using
                    MsgBox("CSV berhasil disimpan.", MsgBoxStyle.Information)
                Catch ex As Exception
                    MsgBox("Gagal menyimpan file: " & ex.Message, MsgBoxStyle.Critical)
                End Try
            End If
        Else
            MsgBox("Tidak ada data untuk diekspor.", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub TextBoxHariReminder_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxHariReminder.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub CheckBoxReminder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxReminder.CheckedChanged
        ' Jika dicentang, TextBoxBulan bisa diisi
        If CheckBoxReminder.Checked Then
            TextBoxHariReminder.ReadOnly = False
        Else
            TextBoxHariReminder.ReadOnly = True
            TextBoxHariReminder.Text = ""
        End If
    End Sub

    Private Sub HitungKuotaSisa()
        Dim kuotaTotal As Integer = CInt(NumericKuotaAwalCartridge.Text)
        Dim kuotaTerpakai As Integer = 0

        Integer.TryParse(TextBoxKuotaTerpakaiCartridge.Text, kuotaTerpakai)

        TextBoxKuotaSisaCartridge.Text = (kuotaTotal - kuotaTerpakai).ToString()
    End Sub

    Private Sub NumericKuotaTotal_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        HitungKuotaSisa()
    End Sub

    Private Sub ClearFormMasterAlat()
        ' Reset ID (penanda insert / update)
        TextBoxAlatId.Text = ""

        ' Reset input utama
        TextBoxKodeAlat.Text = ""
        TextBoxNamaAlat.Text = ""
        TextBoxNamaRuangan.Text = ""
        TextBoxIdRuangan.Text = ""

        ' Status aktif default ON
        CheckBoxAlatAktif.Checked = True

        ' Fokus ke field pertama
        TextBoxKodeAlat.Focus()

        DataGridViewAlat.DataSource = Nothing
        DataGridViewAlat.Rows.Clear()
    End Sub

    Private Sub ClearFormMasterCartridge()
        ' Reset ID (penanda insert / update)
        TextBoxIdCartridge.Text = ""

        ' Reset input utama
        TextBoxKodeCartridge.Text = ""
        TextBoxNamaCartridge.Text = ""

        ' Reset kuota
        NumericKuotaAwalCartridge.Text = 1
        NumericKuotaMinimumCartridge.Text = 1
        TextBoxKuotaTerpakaiCartridge.Text = "0"
        TextBoxKuotaSisaCartridge.Text = "0"
        TextBoxKuotaTotalCartridge.Text = "0"
        TextBoxHargaBeli.Text = "0"

        ' Status aktif default ON
        CheckBoxCartridge.Checked = True

        ComboBoxNamaAlat.SelectedIndex = -1

        ' Fokus ke field pertama
        TextBoxKodeCartridge.Focus()

        DataGridViewAlat.DataSource = Nothing
        DataGridViewAlat.Rows.Clear()
    End Sub

    'Private Sub DataGridViewAlat_CellDoubleClick( _
    'ByVal sender As Object, _
    'ByVal e As DataGridViewCellEventArgs) _
    'Handles DataGridViewAlat.CellDoubleClick

    '    If e.RowIndex < 0 Then Exit Sub

    '    Dim row As DataGridViewRow = DataGridViewAlat.Rows(e.RowIndex)

    '    TextBoxAlatId.Text = row.Cells("AlatID").Value.ToString()
    '    TextBoxKodeAlat.Text = row.Cells("KodeAlat").Value.ToString()
    '    TextBoxNamaAlat.Text = row.Cells("NamaAlat").Value.ToString()

    '    CheckBoxAlatAktif.Checked = (row.Cells("IsAktif").Value = True)

    'End Sub

    Private Sub FormatGridMasterAlat()

        If DataGridViewAlat.DataSource Is Nothing Then Exit Sub
        If DataGridViewAlat.Columns.Count = 0 Then Exit Sub

        With DataGridViewAlat

            .ReadOnly = True
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            If .Columns.Contains("AlatID") Then
                .Columns("AlatID").Visible = False
            End If

            If .Columns.Contains("KodeAlat") Then
                .Columns("KodeAlat").HeaderText = "Kode"
            End If

            If .Columns.Contains("NamaAlat") Then
                .Columns("NamaAlat").HeaderText = "Nama Alat"
            End If

            If .Columns.Contains("IsAktif") Then
                .Columns("IsAktif").HeaderText = "Aktif"
            End If

        End With

    End Sub

    Private Sub FormatGridMasterCartridge()

        With DataGridViewAlat

            .ReadOnly = True
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            .Columns("CartridgeID").Visible = False

            .Columns("KodeCartridge").HeaderText = "Kode"
            .Columns("NamaCartridge").HeaderText = "Nama Cartridge"
            .Columns("KuotaAwal").HeaderText = "Kuota Awal"
            .Columns("KuotaTotal").HeaderText = "Kuota Total"
            .Columns("KuotaTerpakai").HeaderText = "Terpakai"
            .Columns("KuotaSisa").HeaderText = "Sisa"
            .Columns("IsAktif").HeaderText = "Aktif"

        End With

        HighlightKuotaGrid()

    End Sub

    Private Sub HighlightKuotaGrid()

        For Each row As DataGridViewRow In DataGridViewAlat.Rows

            Dim sisa As Integer = CInt(row.Cells("KuotaSisa").Value)

            If sisa <= 0 Then
                row.DefaultCellStyle.BackColor = Color.LightCoral
            ElseIf sisa <= 100 Then
                row.DefaultCellStyle.BackColor = Color.LightYellow
            End If

        Next

    End Sub

    Private Sub LoadGridMasterAlat(Optional ByVal alatID As String = "")

        Dim sql As String = _
            "SELECT AlatID, KodeAlat, NamaAlat, IsAktif " & _
            "FROM MasterAlat "

        If alatID <> "" Then
            sql &= "WHERE AlatID = '" & alatID & "' "
        End If

        sql &= "ORDER BY NamaAlat"

        Dim dt As DataTable = ReadSQL(sql)

        DataGridViewAlat.AutoGenerateColumns = True
        DataGridViewAlat.DataSource = dt

        FormatGridMasterAlat()

    End Sub

    Private Sub LoadGridMasterCartridge( _
        Optional ByVal alatID As String = "", _
        Optional ByVal cartridgeID As String = "")

        Dim sql As String = ""

        sql = "SELECT " & _
              "c.CartridgeID, " & _
              "c.KodeCartridge, " & _
              "c.NamaCartridge, " & _
              "a.NamaAlat, " & _
              "c.KuotaAwal, " & _
              "c.KuotaTotal, " & _
              "c.KuotaTerpakai, " & _
              "(c.KuotaTotal - c.KuotaTerpakai) AS KuotaSisa, " & _
              "c.IsAktif " & _
              "FROM MasterCartridge c " & _
              "INNER JOIN MasterAlat a ON a.AlatID = c.AlatID "

        If cartridgeID <> "" Then
            sql &= "WHERE c.CartridgeID = '" & cartridgeID & "' "
        ElseIf alatID <> "" Then
            sql &= "WHERE c.AlatID = '" & alatID & "' "
        End If

        sql &= "ORDER BY c.NamaCartridge"

        Dim dt As DataTable = ReadSQL(sql)

        DataGridViewAlat.AutoGenerateColumns = True
        DataGridViewAlat.DataSource = dt

        FormatGridMasterCartridge()

    End Sub

    Private Sub InsertMasterAlat()

        Dim statusAktif As Integer = If(CheckBoxAlatAktif.Checked, 1, 0)

        WriteSQL("INSERT INTO MasterAlat " & _
                 "(KodeAlat, NamaAlat, IsAktif, IdRuangan, NamaRuangan, CreatedAt, CreatedBy) " & _
                 "VALUES (" & _
                 "'" & TextBoxKodeAlat.Text & "', " & _
                 "'" & TextBoxNamaAlat.Text & "', " & _
                 "'" & TextBoxNamaRuangan.Text & "', " & _
                 "'" & TextBoxIdRuangan.Text & "', " & _
                 statusAktif & ", " & _
                 "GETDATE(), " & _
                 "'" & userInput & "')", 4)

            MsgBox("Berhasil menyimpan data alat.", MsgBoxStyle.Information, "Success!")

    End Sub

    Private Sub InsertMasterCartridge()

        If ComboBoxNamaAlat.SelectedIndex = -1 Then
            MsgBox("Pilih alat terlebih dahulu")
            Exit Sub
        End If

        Dim statusAktif As Integer = If(CheckBoxCartridge.Checked, 1, 0)

        Dim kuotaAwal As Integer = CInt(NumericKuotaAwalCartridge.Text)
        Dim kuotaMinimum As Integer = CInt(NumericKuotaMinimumCartridge.Text)

        ' Mengambil nilai harga beli dari textbox. Jika kosong/bukan angka, default ke 0
        Dim hargaBeli As Decimal = 0
        If Not Decimal.TryParse(TextBoxHargaBeli.Text, hargaBeli) Then
            hargaBeli = 0
            ' Anda bisa mengganti "hargaBeli = 0" dengan MsgBox peringatan 
            ' dan "Exit Sub" jika harga beli wajib diisi.
        End If

        Dim alatID As Integer = CInt(ComboBoxNamaAlat.SelectedValue)

        If TextBoxKodeCartridge.Text = "" Or TextBoxNamaCartridge.Text = "" Then
            MsgBox("Kode dan Nama cartridge wajib diisi")
            Exit Sub
        End If

        WriteSQL("INSERT INTO MasterCartridge " & _
                 "(KodeCartridge, NamaCartridge, KuotaTotal, KuotaTerpakai, IsAktif, CreatedAt, CreatedBy, KuotaAwal, KuotaMinimum, AlatID, HargaBeli) " & _
                 "VALUES (" & _
                 "'" & TextBoxKodeCartridge.Text.Replace("'", "''") & "', " & _
                 "'" & TextBoxNamaCartridge.Text.Replace("'", "''") & "', " & _
                 kuotaAwal & ", " & _
                 "0, " & _
                 statusAktif & ", " & _
                 "GETDATE(), " & _
                 "'" & userInput & "', " & _
                 kuotaAwal & ", " & _
                 kuotaMinimum & ", " & _
                 alatID & ", " & _
                 hargaBeli & ")", 4)

        MsgBox("Berhasil menyimpan data cartridge.", MsgBoxStyle.Information, "Success!")

    End Sub

    Private Sub UpdateMasterAlat()

        Dim statusAktif As Integer = If(CheckBoxAlatAktif.Checked = True, 1, 0)

        Dim parameterUpdate As String = ""
        parameterUpdate = "KodeAlat = '" & TextBoxKodeAlat.Text.Replace("'", "''") & "', " & _
                          "NamaAlat = '" & TextBoxNamaAlat.Text.Replace("'", "''") & "', " & _
                          "IsAktif = " & statusAktif & ", " & _
                          "IdRuangan = '" & TextBoxIdRuangan.Text & "', " & _
                          "NamaRuangan = '" & TextBoxNamaRuangan.Text.Replace("'", "''") & "', " & _
                          "UpdatedAt = GETDATE(), " & _
                          "UpdatedBy = '" & userInput & "'"

        WriteSQL("UPDATE MasterAlat SET " & _
                 parameterUpdate & _
                 " WHERE AlatID = '" & TextBoxAlatId.Text & "'", 4)

        MsgBox("Berhasil mengubah data alat.", MsgBoxStyle.Information, "Success!")

    End Sub

    Private Sub UpdateMasterCartridge()

        Dim statusAktif As Integer = If(CheckBoxCartridge.Checked = True, 1, 0)

        ' Ambil kuota terpakai lama
        Dim kuotaTerpakai As Integer = CInt(TextBoxKuotaTerpakaiCartridge.Text)
        Dim kuotaAwalBaru As Integer = CInt(NumericKuotaAwalCartridge.Text)
        Dim kuotaMinimum As Integer = CInt(NumericKuotaMinimumCartridge.Text)

        Dim alatID As Integer = CInt(ComboBoxNamaAlat.SelectedValue)

        Dim hargaBeli As Decimal = 0
        If Not Decimal.TryParse(TextBoxHargaBeli.Text, hargaBeli) Then
            hargaBeli = 0
        End If

        If TextBoxKodeCartridge.Text = "" Or TextBoxNamaCartridge.Text = "" Then
            MsgBox("Kode dan Nama cartridge wajib diisi")
            Exit Sub
        End If

        ' Validasi kuota
        If kuotaAwalBaru < kuotaTerpakai Then
            MsgBox("Kuota awal tidak boleh lebih kecil dari kuota terpakai.", _
                   MsgBoxStyle.Critical, "Validasi")
            Exit Sub
        End If

        If ComboBoxNamaAlat.SelectedIndex = -1 Then
            MsgBox("Pilih alat terlebih dahulu")
            Exit Sub
        End If

        Dim parameterUpdate As String = ""

        parameterUpdate = "KodeCartridge = '" & TextBoxKodeCartridge.Text.Replace("'", "''") & "', " & _
                          "NamaCartridge = '" & TextBoxNamaCartridge.Text.Replace("'", "''") & "', " & _
                          "AlatID = " & alatID & ", " & _
                          "HargaBeli = " & hargaBeli & ", " & _
                          "KuotaMinimum = " & kuotaMinimum & ", " & _
                          "IsAktif = " & statusAktif & ", " & _
                          "UpdatedAt = GETDATE(), " & _
                          "UpdatedBy = '" & userInput & "'"

        ' Jika belum ada pemakaian → boleh ubah kuota awal
        Dim kuotaAwalLama As Integer = CInt(NumericKuotaAwalCartridge.Text)

        If kuotaTerpakai = 0 Then
            parameterUpdate &= ", KuotaAwal = " & kuotaAwalBaru & _
                               ", KuotaTotal = " & kuotaAwalBaru
        Else
            If kuotaAwalBaru <> kuotaAwalLama Then
                MsgBox("Kuota awal tidak dapat diubah karena sudah ada pemakaian." & vbCrLf & _
                       "Gunakan fitur Top Up untuk menambah kuota.", _
                       MsgBoxStyle.Exclamation, "Peringatan")
                Exit Sub
            End If
        End If

        WriteSQL("UPDATE MasterCartridge SET " & _
                 parameterUpdate & _
                 " WHERE CartridgeID = '" & TextBoxIdCartridge.Text & "'", 4)

        MsgBox("Berhasil mengubah data cartridge.", MsgBoxStyle.Information, "Success!")

    End Sub

    Private Sub LoadComboAlat()

        Dim sql As String = _
            "SELECT AlatID, NamaAlat " & _
            "FROM MasterAlat " & _
            "WHERE IsAktif = 1 " & _
            "ORDER BY NamaAlat"

        Dim dt As DataTable = ReadSQL(sql)

        ComboBoxNamaAlat.DataSource = dt
        ComboBoxNamaAlat.DisplayMember = "NamaAlat"
        ComboBoxNamaAlat.ValueMember = "AlatID"

        ComboBoxNamaAlat.SelectedIndex = -1

    End Sub

    Private Sub TabControl1_SelectedIndexChanged() _
        Handles TabControl1.SelectedIndexChanged

        If TabControl1.SelectedTab Is TabPage2 Then
            LoadComboAlat()
        End If

    End Sub

    Private Sub SimpanCartridge()

        If TextBoxKodeCartridge.Text = "" Or TextBoxNamaCartridge.Text = "" Then
            MsgBox("Kode dan Nama cartridge wajib diisi")
            Exit Sub
        End If

        If TextBoxIdCartridge.Text = "" Then
            InsertMasterCartridge()
        Else
            UpdateMasterCartridge()
        End If

        ClearFormMasterCartridge()
        LoadGridMasterCartridge()

    End Sub

    Private Sub SimpanAlat()

        If TextBoxKodeAlat.Text = "" Or TextBoxNamaAlat.Text = "" Then
            MsgBox("Kode dan Nama alat wajib diisi")
            Exit Sub
        End If

        If TextBoxAlatId.Text = "" Then
            InsertMasterAlat()
        Else
            UpdateMasterAlat()
        End If

        ClearFormMasterAlat()
        LoadGridMasterAlat()

    End Sub

    Private Sub ButtonSimpanAlat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSimpanAlat.Click
        Select Case TabControl1.SelectedTab.Name

            Case "TabPage1"
                SimpanAlat()

            Case "TabPage2"
                SimpanCartridge()

        End Select
    End Sub

    Private Sub ButtonClearAlat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClearAlat.Click
        Select Case TabControl1.SelectedTab.Name

            Case "TabPage1"
                ClearFormMasterAlat()

            Case "TabPage2"
                ClearFormMasterCartridge()
        End Select
    End Sub

    Public Sub LoadDetailAlat(ByRef AlatID As String)

        Dim sql As String = ""
        sql = "SELECT * FROM MasterAlat WHERE AlatID = '" & AlatID & "'"

        Dim dt As DataTable = ReadSQL(sql)

        If dt.Rows.Count = 0 Then
            MsgBox("Data alat tidak ditemukan.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim row As DataRow = dt.Rows(0)

        ' Isi form
        TextBoxAlatID.Text = row("AlatID").ToString()
        TextBoxKodeAlat.Text = row("KodeAlat").ToString()
        TextBoxNamaAlat.Text = row("NamaAlat").ToString()
        TextBoxNamaRuangan.Text = row("NamaRuangan").ToString()
        TextBoxIdRuangan.Text = row("IdRuangan").ToString()


        CheckBoxAlatAktif.Checked = (row("IsAktif") = True)

        LoadGridMasterAlat(AlatID)

    End Sub

    Public Sub LoadDetailCartridge(ByRef CartridgeID As String)

        Dim sql As String = "SELECT * FROM MasterCartridge WHERE CartridgeID = '" & CartridgeID & "'"

        Dim dt As DataTable = ReadSQL(sql)

        If dt.Rows.Count = 0 Then
            MsgBox("Data cartridge tidak ditemukan.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim row As DataRow = dt.Rows(0)

        Dim kuotaAwal As Integer = If(IsDBNull(row("KuotaAwal")), 0, CInt(row("KuotaAwal")))
        Dim kuotaTotal As Integer = If(IsDBNull(row("KuotaTotal")), 0, CInt(row("KuotaTotal")))
        Dim kuotaTerpakai As Integer = If(IsDBNull(row("KuotaTerpakai")), 0, CInt(row("KuotaTerpakai")))
        Dim kuotaMinimum As Integer = If(IsDBNull(row("KuotaMinimum")), 0, CInt(row("KuotaMinimum")))

        ' --- MULAI PENAMBAHAN HARGA BELI ---
        ' Mengambil nilai HargaBeli dari database, jika Null maka set ke 0
        Dim hargaBeli As Decimal = If(IsDBNull(row("HargaBeli")), 0D, CDec(row("HargaBeli")))
        ' --- AKHIR PENAMBAHAN HARGA BELI ---

        Dim kuotaSisa As Integer = kuotaTotal - kuotaTerpakai

        ' =============================
        ' ISI FORM
        ' =============================

        TextBoxIdCartridge.Text = row("CartridgeID").ToString()
        TextBoxKodeCartridge.Text = row("KodeCartridge").ToString()
        TextBoxNamaCartridge.Text = row("NamaCartridge").ToString()

        ' --- MEMASUKKAN NILAI HARGA BELI KE TEXTBOX ---
        ' Anda bisa menggunakan format tertentu, misal hargaBeli.ToString("N0") untuk pemisah ribuan
        TextBoxHargaBeli.Text = Math.Round(hargaBeli, 0).ToString()
        ' ----------------------------------------------

        NumericKuotaAwalCartridge.Text = kuotaAwal.ToString()
        NumericKuotaMinimumCartridge.Text = kuotaMinimum.ToString()
        TextBoxKuotaTotalCartridge.Text = kuotaTotal.ToString()
        TextBoxKuotaTerpakaiCartridge.Text = kuotaTerpakai.ToString()
        TextBoxKuotaSisaCartridge.Text = kuotaSisa.ToString()

        CheckBoxCartridge.Checked = CBool(row("IsAktif"))

        ' =============================
        ' ⭐ SET COMBO ALAT
        ' =============================

        If Not IsDBNull(row("AlatID")) Then
            ComboBoxNamaAlat.SelectedValue = CInt(row("AlatID"))
        Else
            ComboBoxNamaAlat.SelectedIndex = -1
        End If

        ' =============================
        ' LOAD GRID (OPTIONAL)
        ' =============================

        'LoadGridMasterCartridge(row("AlatID").ToString())
        LoadGridMasterCartridge("", row("CartridgeID").ToString())
    End Sub

    Private isLoadingCombo As Boolean = False

    Private Sub ComboBoxAlatTindakan_SelectedIndexChanged() _
        Handles ComboBoxAlatTindakan.SelectedIndexChanged

        If isLoadingCombo Then Exit Sub
        If ComboBoxAlatTindakan.SelectedIndex = -1 Then Exit Sub
        If ComboBoxAlatTindakan.SelectedValue Is Nothing Then Exit Sub
        If TypeOf ComboBoxAlatTindakan.SelectedValue Is DataRowView Then Exit Sub
        If Not IsNumeric(ComboBoxAlatTindakan.SelectedValue) Then Exit Sub

        Dim alatID As Integer = CInt(ComboBoxAlatTindakan.SelectedValue)

        LoadCartridgeByAlat(alatID)

    End Sub

    Private Sub LoadComboAlatTindakan()

        isLoadingCombo = True

        Dim sql As String = "SELECT AlatID, NamaAlat FROM MasterAlat WHERE IsAktif=1"

        Dim dt As DataTable = ReadSQL(sql)

        ComboBoxAlatTindakan.DataSource = dt
        ComboBoxAlatTindakan.DisplayMember = "NamaAlat"
        ComboBoxAlatTindakan.ValueMember = "AlatID"
        ComboBoxAlatTindakan.SelectedIndex = -1

        isLoadingCombo = False

    End Sub

    Private Sub LoadCartridgeByAlat(ByVal alatID As Integer)

        Dim sql As String = _
            "SELECT CartridgeID, NamaCartridge, ISNULL(HargaBeli, 0) AS HargaBeli " & _
            "FROM MasterCartridge " & _
            "WHERE AlatID = " & alatID & " " & _
            "AND IsAktif = 1 " & _
            "ORDER BY NamaCartridge"

        Dim dt As DataTable = ReadSQL(sql)

        ComboBoxCartridgeTindakan.DataSource = Nothing

        ComboBoxCartridgeTindakan.DisplayMember = "NamaCartridge"
        ComboBoxCartridgeTindakan.ValueMember = "CartridgeID"
        ComboBoxCartridgeTindakan.DataSource = dt

        ComboBoxCartridgeTindakan.SelectedIndex = -1

    End Sub

    Private Sub CheckBoxPakaiAlat_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxPakaiAlat.CheckedChanged
        If CheckBoxPakaiAlat.Checked Then
            NumericShootPerTindakan.ReadOnly = False
            ComboBoxAlatTindakan.Enabled = True
            ComboBoxCartridgeTindakan.Enabled = True

            LoadComboAlatTindakan()
        Else
            NumericShootPerTindakan.ReadOnly = True
            ComboBoxAlatTindakan.DataSource = Nothing
            ComboBoxCartridgeTindakan.DataSource = Nothing

            ComboBoxAlatTindakan.Enabled = False
            ComboBoxCartridgeTindakan.Enabled = False
        End If
    End Sub

    Public Sub LoadDetailAlatTindakan(ByRef AlatID As String)
        Dim sql As String = ""
        sql = "SELECT * FROM MasterAlat WHERE AlatID = '" & AlatID & "'"

        Dim dt As DataTable = ReadSQL(sql)

        If dt.Rows.Count = 0 Then
            MsgBox("Data alat tidak ditemukan.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim row As DataRow = dt.Rows(0)
    End Sub

    Private Sub ButtonCariAlat_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCariAlat.Click
        Find.Show()
        Find.Query("SELECT AlatID, KodeAlat, NamaAlat, IsAktif FROM MasterAlat ", "ORDER BY CreatedAt DESC", "FindMasterAlat", 2)
    End Sub

    Private Sub ButtonCariCartridge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCariCartridge.Click
        Find.Show()
        Find.Query("SELECT CartridgeID, KodeCartridge, NamaCartridge, KuotaAwal, KuotaTotal, KuotaTerpakai, KuotaMinimum IsAktif FROM MasterCartridge ", "ORDER BY CreatedAt DESC", "FindMasterCartridge", 2)
    End Sub

    Private Sub ButtonCetakRiwayat_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCetakRiwayat.Click
        If TextBoxAlatId.Text.Trim() = "" Then
            MsgBox("Pilih alat terlebih dahulu.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim alatID As String = TextBoxAlatId.Text.Trim()
        Dim tglAwal As Date = dtAwal.Value
        Dim tglAkhir As Date = dtAkhir.Value
        Dim namaAlat As String = TextBoxNamaAlat.Text
        Dim stringTglTransaksiAwal As String = tglAwal.Day & " " & BulanIndonesia(tglAwal.Month) & " " & tglAwal.Year
        Dim stringTglTransaksiAkhir As String = tglAkhir.Day & " " & BulanIndonesia(tglAkhir.Month) & " " & tglAkhir.Year

        Dim sql As String = ""
        sql = "DECLARE @AlatID INT = " & alatID & " " & _
              "DECLARE @TglAwal DATETIME = '" & tglAwal & "' " & _
              "DECLARE @TglAkhir DATETIME = '" & tglAkhir & "' " & _
              " ;WITH Ledger AS ( " & _
              " SELECT " & _
              " MC.CreatedAt AS Tanggal, " & _
              " 'SALDO AWAL' AS JenisTransaksi, " & _
              " MA.NamaAlat + ' - ' + MC.NamaCartridge AS Keterangan, " & _
              " MC.KuotaAwal AS Debet, " & _
              " 0 AS Kredit, " & _
              " MC.CartridgeID, " & _
              " ISNULL(MC.HargaBeli, 0) AS HargaBeli " & _
              " FROM MasterCartridge MC " & _
              " INNER JOIN MasterAlat MA ON MA.AlatID = MC.AlatID " & _
              " WHERE MA.AlatID = @AlatID " & _
              " UNION ALL " & _
              " SELECT " & _
              " LPA.TglPemakaian, " & _
              " CAST(TP.NoUrut AS VARCHAR(50)), " & _
              " TPD.NamaTindakan + ' - ' + MP.Nama + " & _
              " ' | ' + MC.NamaCartridge, " & _
              " 0, " & _
              " LPA.ShootDipakai, " & _
              " MC.CartridgeID, " & _
              " ISNULL(LPA.HargaBeli, 0) " & _
              " FROM LogPemakaianAlat LPA " & _
              " INNER JOIN MasterCartridge MC ON MC.CartridgeID = LPA.CartridgeID " & _
              " INNER JOIN MasterAlat MA ON MA.AlatID = MC.AlatID " & _
              " INNER JOIN TransaksiPoliDetail TPD ON TPD.TransPoliIDDetail = LPA.TransaksiDetailID " & _
              " INNER JOIN TransaksiPoli TP ON TP.TransPoliID = TPD.TransPoliID " & _
              " INNER JOIN MasterPasien MP ON MP.NoRekamMedis = TP.IDInternet " & _
              " WHERE MA.AlatID = @AlatID " & _
              " AND LPA.IsVoid = 0 " & _
              " AND LPA.Status = 1 " & _
              " AND LPA.TglPemakaian BETWEEN @TglAwal AND @TglAkhir " & _
              " UNION ALL " & _
              " SELECT " & _
              " LA.TglAdjustment, " & _
              " 'ADJUSTMENT', " & _
              " 'Penyesuaian - ' + ISNULL(LA.Keterangan, ''), " & _
              " CASE WHEN LA.QtyAdjustment > 0 THEN LA.QtyAdjustment ELSE 0 END, " & _
              " CASE WHEN LA.QtyAdjustment < 0 THEN ABS(LA.QtyAdjustment) ELSE 0 END, " & _
              " MC.CartridgeID, " & _
              " 0 " & _
              " FROM LogAdjustmentCartridge LA " & _
              " INNER JOIN MasterCartridge MC ON MC.CartridgeID = LA.CartridgeID " & _
              " WHERE MC.AlatID = @AlatID " & _
              " AND LA.TglAdjustment BETWEEN @TglAwal AND @TglAkhir " & _
              " ) " & _
              " SELECT " & _
              " L1.Tanggal, " & _
              " L1.JenisTransaksi, " & _
              " L1.Keterangan, " & _
              " L1.Debet, " & _
              " L1.Kredit, " & _
              " (SELECT SUM(L2.Debet - L2.Kredit) " & _
              "  FROM Ledger L2 " & _
              "  WHERE L2.CartridgeID = L1.CartridgeID " & _
              "  AND L2.Tanggal <= L1.Tanggal) AS Saldo, " & _
              " L1.HargaBeli " & _
              " FROM Ledger L1 " & _
              " ORDER BY L1.CartridgeID, L1.Tanggal"

        Dim dt As DataTable = ReadSQL(sql)

        If dt.Rows.Count = 0 Then
            MsgBox("Tidak ada data riwayat alat.", MsgBoxStyle.Information)
            Exit Sub
        End If

        ' ========================================================
        ' ⭐ MENGHITUNG TOTAL DARI DATATABLE
        ' ========================================================
        Dim totalDebet As Decimal = If(IsDBNull(dt.Compute("SUM(Debet)", "")), 0, CDec(dt.Compute("SUM(Debet)", "")))
        Dim totalKredit As Decimal = If(IsDBNull(dt.Compute("SUM(Kredit)", "")), 0, CDec(dt.Compute("SUM(Kredit)", "")))
        Dim totalHargaBeli As Decimal = If(IsDBNull(dt.Compute("SUM(HargaBeli)", "")), 0, CDec(dt.Compute("SUM(HargaBeli)", "")))
        Dim totalSaldo As Decimal = totalDebet - totalKredit
        ' ========================================================

        Dim sqlSummary As String = ""
        sqlSummary = _
            "DECLARE @AlatID INT = " & alatID & " " & _
            "SELECT " & _
            "(SELECT ISNULL(SUM(KuotaAwal),0) " & _
            " FROM MasterCartridge " & _
            " WHERE AlatID=@AlatID) AS KuotaAwal, " & _
            "(SELECT ISNULL(SUM(JumlahTopUp),0) " & _
            " FROM LogTopUpAlat " & _
            " WHERE AlatID=@AlatID) AS TotalTopUp, " & _
            "(SELECT ISNULL(SUM(LPA.ShootDipakai),0) " & _
            " FROM LogPemakaianAlat LPA " & _
            " INNER JOIN MasterCartridge MC " & _
            " ON MC.CartridgeID = LPA.CartridgeID " & _
            " WHERE MC.AlatID=@AlatID " & _
            " AND LPA.Status=1 " & _
            " AND LPA.IsVoid=0) AS TotalPemakaian, " & _
            "( " & _
            " (SELECT ISNULL(SUM(KuotaAwal),0) FROM MasterCartridge WHERE AlatID=@AlatID) " & _
            " + (SELECT ISNULL(SUM(JumlahTopUp),0) FROM LogTopUpAlat WHERE AlatID=@AlatID) " & _
            " - (SELECT ISNULL(SUM(LPA.ShootDipakai),0) " & _
            "    FROM LogPemakaianAlat LPA " & _
            "    INNER JOIN MasterCartridge MC ON MC.CartridgeID=LPA.CartridgeID " & _
            "    WHERE MC.AlatID=@AlatID " & _
            "    AND LPA.Status=1 " & _
            "    AND LPA.IsVoid=0) " & _
            " + (SELECT ISNULL(SUM(LA.QtyAdjustment),0) " & _
            "    FROM LogAdjustmentCartridge LA " & _
            "    INNER JOIN MasterCartridge MC ON MC.CartridgeID=LA.CartridgeID " & _
            "    WHERE MC.AlatID=@AlatID) " & _
            ") AS SaldoAkhir"

        Dim dtSummary As DataTable = ReadSQL(sqlSummary)

        Dim kuotaAwal As Integer = 0
        Dim totalTopUp As Integer = 0
        Dim totalPakai As Integer = 0
        Dim saldoAkhir As Integer = 0

        If dtSummary.Rows.Count > 0 Then
            kuotaAwal = CInt(dtSummary.Rows(0)("KuotaAwal"))
            totalTopUp = CInt(dtSummary.Rows(0)("TotalTopUp"))
            totalPakai = CInt(dtSummary.Rows(0)("TotalPemakaian"))
            saldoAkhir = CInt(dtSummary.Rows(0)("SaldoAkhir"))
        End If

        ' Load report dari file
        Dim report As New DevExpress.XtraReports.UI.XtraReport
        report = XtraReport.FromFile(Application.StartupPath + "\reports\RiwayatTransaksiAlat.repx", True)

        ' ========================================================
        ' 1. VARIABEL REPORT HEADER (Untuk Info Alat & Tanggal)
        ' ========================================================
        Dim bandHeader As ReportHeaderBand = CType(report.Bands(BandKind.ReportHeader), ReportHeaderBand)
        If bandHeader IsNot Nothing Then
            bandHeader.Controls("lblNamaAlat").Text = namaAlat
            bandHeader.Controls("lblKodeAlat").Text = TextBoxKodeAlat.Text
            bandHeader.Controls("lbltanggal").Text = stringTglTransaksiAwal & " - " & stringTglTransaksiAkhir
        End If

        ' ========================================================
        ' 2. VARIABEL GROUP HEADER (Untuk Summary Header)
        ' ========================================================
        Dim bandGroup As GroupHeaderBand = CType(report.Bands("GroupHeader3"), GroupHeaderBand)
        If bandGroup IsNot Nothing Then
            bandGroup.Controls("saldoAwal").Text = Format(kuotaAwal, "#,##0")
            bandGroup.Controls("totalTopUp").Text = Format(totalTopUp, "#,##0")
            bandGroup.Controls("totalPemakaian").Text = Format(totalPakai, "#,##0")
            bandGroup.Controls("saldoAkhir").Text = Format(saldoAkhir, "#,##0")
        End If

        ' ========================================================
        ' 3. VARIABEL PAGE FOOTER (Untuk Info Cetak)
        ' ========================================================
        Dim bandPageFooter As PageFooterBand = CType(report.Bands(BandKind.PageFooter), PageFooterBand)
        If bandPageFooter IsNot Nothing Then
            bandPageFooter.Controls("lblprinted").Text = Format(DateAndTime.Now, "dd-MMM-yyyy HH:mm:ss") & " By " & userInput
        End If

        ' ========================================================
        ' 4. VARIABEL REPORT FOOTER (Untuk Total Keseluruhan)
        ' ========================================================
        Dim bandReportFooter As ReportFooterBand = CType(report.Bands(BandKind.ReportFooter), ReportFooterBand)
        If bandReportFooter IsNot Nothing Then
            If bandReportFooter.Controls("lblTotalDebet") IsNot Nothing Then bandReportFooter.Controls("lblTotalDebet").Text = Format(totalDebet, "#,##0")
            If bandReportFooter.Controls("lblTotalKredit") IsNot Nothing Then bandReportFooter.Controls("lblTotalKredit").Text = Format(totalKredit, "#,##0")
            If bandReportFooter.Controls("lblTotalHargaBeli") IsNot Nothing Then bandReportFooter.Controls("lblTotalHargaBeli").Text = Format(totalHargaBeli, "#,##0")
            If bandReportFooter.Controls("lblTotalSaldo") IsNot Nothing Then bandReportFooter.Controls("lblTotalSaldo").Text = Format(totalSaldo, "#,##0")
        End If

        report.DataSource = dt

        'report.RunDesigner()
        report.ShowPreview()
    End Sub

    Private Sub ButtonPreOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonPreOrder.Click
        'FormPreOrderCartridge.ShowDialog()
        FormPreOrderAlat.ShowDialog()
    End Sub

    Private Sub ButtonGetAlatSarpras_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonGetAlatSarpras.Click
        FormPencarianAlatSarpras.ShowDialog()
    End Sub

    Private Sub ButtonGetAlatCartridge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonGetAlatCartridge.Click
        FormPencarianAlatSarpras.ShowDialog()
    End Sub

    Private Sub ComboBoxAlatTindakan_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxAlatTindakan.SelectedIndexChanged

    End Sub
End Class