Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Drawing
Imports Newtonsoft.Json
Imports Newtonsoft
Imports System.Security.Cryptography
Imports System.Collections.Generic
Imports Newtonsoft.Json.Linq
Imports System.Web.UI.WebControls
Imports DevExpress.XtraGrid.Columns
Imports System.Text

Public Class DialogBoxPaketTindakan
    Private formName As String = "PENCARIAN PAKET"
    Private formInput As String = ""
    Private userInput As String = PoliKlinik.Lbluser.Text

    Private Sub PanelTop_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PanelTop.Paint
        Try
            e.Graphics.DrawString(formName, New Font("Arial", 12, FontStyle.Bold), Brushes.White, New Point(10, 10))
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Function CreateTablePaket() As DataTable
        Dim Thasil As New DataTable

        insertRowToTbls(Thasil, "Typstring", "PaketID")
        insertRowToTbls(Thasil, "Typstring", "TindakanID")
        insertRowToTbls(Thasil, "Typstring", "KodeVoucher")
        insertRowToTbls(Thasil, "Typstring", "Nama Paket")
        insertRowToTbls(Thasil, "Typstring", "Keterangan Paket")
        insertRowToTbls(Thasil, "Typstring", "Keterangan Tindakan")
        insertRowToTbls(Thasil, "Typstring", "Tanggal Mulai")
        insertRowToTbls(Thasil, "Typstring", "Tanggal Berakhir")
        insertRowToTbls(Thasil, "Typstring", "Kuota")
        insertRowToTbls(Thasil, "Typstring", "Penjamin")
        insertRowToTbls(Thasil, "Typstring", "Harga Awal")
        insertRowToTbls(Thasil, "Typstring", "Diskon")
        insertRowToTbls(Thasil, "Typstring", "Harga Nett")
        insertRowToTbls(Thasil, "Typstring", "Jenis Paket")
        insertRowToTbls(Thasil, "typchecked", "Pilihan")

        For Each Tcol As DataColumn In Thasil.Columns
            Tcol.ReadOnly = True
        Next

        Return Thasil
    End Function

    Private Sub ButtonFindPaket_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFindPaket.Click
        PencarianPaket()
    End Sub

    Private Sub PencarianPaket()
        Try
            If ValidasiPencarian() = True Then

                Dim kodeVoucher As String = ""

                If TypeOf ComboBoxPaketTindakan.SelectedValue Is DataRowView Then
                    kodeVoucher = CType(ComboBoxPaketTindakan.SelectedValue, DataRowView)("KodeVoucher").ToString()
                Else
                    kodeVoucher = ComboBoxPaketTindakan.SelectedValue.ToString()
                End If

                Dim queryBuilder As New StringBuilder()

                ' Query Paket Reguler
                queryBuilder.AppendLine("SELECT tblHeader.NoRekamMedis, tblHeader.NoTransaksi, tblHeader.PaketID, '0' AS TindakanID,")
                queryBuilder.AppendLine("       tblHeader.NamaPaket, tblHeader.KeteranganPaket,")
                queryBuilder.AppendLine("       tblHeader.JenisPaket, tblHeader.TanggalMulaiPaket, tblHeader.TanggalAkhirPaket,")
                queryBuilder.AppendLine("       tblHeader.HargaAwal, tblHeader.DiskonNominal,")
                queryBuilder.AppendLine("       tblHeader.HargaNett, tblHeader.Kuota AS Kuota,") ' Kuota dari tblHeader
                queryBuilder.AppendLine("       tblHeader.Penjamin, tblHeader.KodeVoucher")
                queryBuilder.AppendLine("FROM TransaksiPaketTindakan tblHeader")
                queryBuilder.AppendLine("WHERE tblHeader.KodeVoucher = '" & kodeVoucher & "'")
                queryBuilder.AppendLine("      AND (GETDATE() BETWEEN ISNULL(tblHeader.TanggalMulaiPaket, GETDATE())")
                queryBuilder.AppendLine("      AND ISNULL(tblHeader.TanggalAkhirPaket, GETDATE()))")
                queryBuilder.AppendLine("      AND tblHeader.DeletedDate IS NULL")
                queryBuilder.AppendLine("      AND tblHeader.Kuota > 0") ' Kuota dari Header
                queryBuilder.AppendLine("      AND tblHeader.JenisPaket = 'Paket Reguler'")

                queryBuilder.AppendLine("UNION ALL")

                ' Query Paket Per-Tindakan
                queryBuilder.AppendLine("SELECT tblHeader.NoRekamMedis, tblHeader.NoTransaksi, tblHeader.PaketID, tblDetail.TindakanID,")
                queryBuilder.AppendLine("       tblDetail.NamaTindakan AS NamaPaket, tblDetail.JenisTindakan AS KeteranganPaket,")
                queryBuilder.AppendLine("       tblHeader.JenisPaket, tblHeader.TanggalMulaiPaket, tblHeader.TanggalAkhirPaket,")
                queryBuilder.AppendLine("       tblDetail.BiayaTindakan AS HargaAwal, tblDetail.DiskonTindakan AS DiskonNominal,")
                queryBuilder.AppendLine("       tblDetail.NettBayar AS HargaNett, tblDetail.KuotaTindakan AS Kuota,") ' Kuota dari tblDetail
                queryBuilder.AppendLine("       tblHeader.Penjamin, tblHeader.KodeVoucher")
                queryBuilder.AppendLine("FROM TransaksiPaketTindakanDetail AS tblDetail")
                queryBuilder.AppendLine("     INNER JOIN TransaksiPaketTindakan AS tblHeader")
                queryBuilder.AppendLine("     ON tblDetail.PaketID = tblHeader.PaketID AND tblDetail.KodeVoucher = tblHeader.KodeVoucher")
                queryBuilder.AppendLine("WHERE tblHeader.KodeVoucher = '" & kodeVoucher & "'")
                queryBuilder.AppendLine("      AND (GETDATE() BETWEEN ISNULL(tblHeader.TanggalMulaiPaket, GETDATE())")
                queryBuilder.AppendLine("      AND ISNULL(tblHeader.TanggalAkhirPaket, GETDATE()))")
                queryBuilder.AppendLine("      AND tblHeader.DeletedDate IS NULL")
                queryBuilder.AppendLine("      AND tblDetail.KuotaTindakan > 0") ' Kuota dari Detail
                queryBuilder.AppendLine("      AND tblHeader.JenisPaket = 'Paket Per-Tindakan'")

                Dim tblPaket As DataTable = ReadSQL(queryBuilder.ToString())

                If tblPaket.Rows.Count > 0 Then
                    Dim gridPaket As DataTable = GridControlPaket.DataSource
                    gridPaket.Clear()

                    For Each rowPaket As DataRow In tblPaket.Rows
                        Dim newRow As DataRow = gridPaket.NewRow
                        newRow("PaketID") = rowPaket("PaketID").ToString()
                        newRow("TindakanID") = rowPaket("TindakanID").ToString()
                        newRow("KodeVoucher") = rowPaket("KodeVoucher").ToString()
                        newRow("Nama Paket") = rowPaket("NamaPaket").ToString()
                        newRow("Keterangan Paket") = rowPaket("KeteranganPaket").ToString()
                        newRow("Keterangan Tindakan") = rowPaket("KeteranganPaket").ToString()
                        newRow("Tanggal Mulai") = Format(rowPaket("TanggalMulaiPaket"), "yyyy-MM-dd")
                        newRow("Tanggal Berakhir") = Format(rowPaket("TanggalAkhirPaket"), "yyyy-MM-dd")
                        newRow("Kuota") = rowPaket("Kuota").ToString()
                        newRow("Penjamin") = rowPaket("Penjamin").ToString()
                        newRow("Harga Awal") = Splitmoney(rowPaket("HargaAwal"))
                        newRow("Diskon") = Splitmoney(rowPaket("DiskonNominal"))
                        newRow("Harga Nett") = Splitmoney(rowPaket("HargaNett"))
                        newRow("Jenis Paket") = Splitmoney(rowPaket("JenisPaket"))
                        gridPaket.Rows.Add(newRow)
                    Next

                    GridViewPaket.ExpandAllGroups()
                Else
                    MsgBox("Data tidak ditemukan.", MsgBoxStyle.Critical, "Warning!")
                End If
            End If
        Catch ex As Exception
            MsgBox("Eror : " & ex.Message, MsgBoxStyle.Exclamation, "Error!")
        End Try
    End Sub

    Function ValidasiPencarian() As Boolean
        If ComboBoxPaketTindakan.SelectedValue Is Nothing OrElse ComboBoxPaketTindakan.SelectedIndex = -1 Then
            MsgBox("Silakan pilih paket terlebih dahulu.", MsgBoxStyle.Critical, "Warning!")
            ComboBoxPaketTindakan.Focus()
            Return False
        End If

        Return True
    End Function

    Public NoRM As String

    Private dtPasienCache As DataTable

    Private Sub LoadCachePasien()

        Dim query As New StringBuilder()

        query.AppendLine("SELECT NoRekamMedis, Nama ")
        query.AppendLine("FROM MasterPasien ")
        query.AppendLine("WHERE DeletedBy IS NULL ")

        dtPasienCache = ReadSQL(query.ToString())

    End Sub

    Private Sub SetupAutoCompletePasien()

        Dim autoSource As New AutoCompleteStringCollection()

        For Each row As DataRow In dtPasienCache.Rows
            ' Format bebas (biar enak dicari)
            autoSource.Add(row("Nama").ToString() & " - " & row("NoRekamMedis").ToString())
        Next

        With TextBoxRMPasien
            .AutoCompleteMode = AutoCompleteMode.SuggestAppend
            .AutoCompleteSource = AutoCompleteSource.CustomSource
            .AutoCompleteCustomSource = autoSource
        End With

    End Sub

    Private Function LoadComboPaket(ByVal NoRM As String) As DataTable

        Dim tblResult As New DataTable
        Dim query As New StringBuilder()

        query.AppendLine("SELECT ")
        query.AppendLine("    TPT.PaketID, ")
        query.AppendLine("    TPT.NamaPaket + ' (' + TPT.KodeVoucher + ')' AS NamaPaket, ")
        query.AppendLine("    TPT.KodeVoucher ")
        query.AppendLine("FROM TransaksiPaketTindakan TPT ")
        query.AppendLine("WHERE TPT.DeletedBy IS NULL ")

        query.AppendLine("AND ( ")
        query.AppendLine("    GETDATE() >= ISNULL(TPT.TanggalMulaiPaket, GETDATE()) ")
        query.AppendLine("    AND GETDATE() <= ISNULL(TPT.TanggalAkhirPaket, GETDATE()) ")
        query.AppendLine(") ")

        If NoRM <> "" Then
            query.AppendLine("AND TPT.NoRekamMedis = '" & NoRM & "' ")
        End If

        query.AppendLine("AND ( ")
        query.AppendLine("    (TPT.JenisPaket = 'Paket Reguler' AND ISNULL(TPT.Kuota, 0) > 0) ")
        query.AppendLine("    OR ")
        query.AppendLine("    ( ")
        query.AppendLine("        TPT.JenisPaket = 'Paket Per-Tindakan' ")
        query.AppendLine("        AND EXISTS ( ")
        query.AppendLine("            SELECT 1 ")
        query.AppendLine("            FROM TransaksiPaketTindakanDetail TPTD ")
        query.AppendLine("            WHERE TPTD.KodeVoucher = TPT.KodeVoucher ")
        query.AppendLine("            AND ISNULL(TPTD.KuotaTindakan, 0) > 0 ")
        query.AppendLine("        ) ")
        query.AppendLine("    ) ")
        query.AppendLine(") ")

        query.AppendLine("ORDER BY TPT.TanggalMulaiPaket DESC, TPT.KodeVoucher DESC ")

        tblResult = ReadSQL(query.ToString())
        Return tblResult

    End Function

    Private Sub CheckBoxPaketLain_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxPaketLain.CheckedChanged

        If CheckBoxPaketLain.Checked Then
            TextBoxRMPasien.Enabled = True
            TextBoxRMPasien.Clear()

        Else
            TextBoxRMPasien.Enabled = False
            TextBoxRMPasien.Text = NoRM

            ' reload paket pasien sendiri
            Dim dt As DataTable = LoadComboPaket(NoRM)

            With ComboBoxPaketTindakan
                .DataSource = dt
                .DisplayMember = "NamaPaket"
                .ValueMember = "KodeVoucher"
                .SelectedIndex = -1
            End With
        End If
    End Sub

    Private Function GetNoRMFromTextbox() As String

        If TextBoxRMPasien.Text.Contains(" - ") Then
            Dim parts = TextBoxRMPasien.Text.Split(New String() {" - "}, StringSplitOptions.None)
            Return parts(1).Trim()
        End If

        Return TextBoxRMPasien.Text.Trim()

    End Function

    Private Sub TextBoxRMPasien_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles TextBoxRMPasien.KeyDown

        If CheckBoxPaketLain.Checked = False Then Exit Sub

        If e.KeyCode = Keys.Enter Then

            Dim noRM As String = GetNoRMFromTextbox()

            If noRM <> "" Then

                Dim dt As DataTable = LoadComboPaket(noRM)

                If dt.Rows.Count = 0 Then
                    MsgBox("Pasien tidak memiliki paket aktif")
                    ComboBoxPaketTindakan.DataSource = Nothing
                    Exit Sub
                End If

                With ComboBoxPaketTindakan
                    .DataSource = dt
                    .DisplayMember = "NamaPaket"
                    .ValueMember = "KodeVoucher"
                    .SelectedIndex = -1
                End With

            Else
                MsgBox("No RM tidak valid")
            End If

        End If

    End Sub

    Private Sub DialogBoxPaketTindakan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBoxKodeVoucher.Focus()
        TextBoxKodeVoucher.ResetText()

        LoadCachePasien()
        SetupAutoCompletePasien()

        TextBoxRMPasien.Enabled = False
        CheckBoxPaketLain.Checked = False

        If String.IsNullOrEmpty(NoRM) Then
            MsgBox("No RM tidak ditemukan!")
            Me.Close()
            Exit Sub
        End If

        TextBoxRMPasien.Text = NoRM

        Dim dt As DataTable = LoadComboPaket(NoRM)

        With ComboBoxPaketTindakan
            .DataSource = dt
            .DisplayMember = "NamaPaket"
            .ValueMember = "KodeVoucher"
            .SelectedIndex = -1
        End With

        SetDataGrid()
    End Sub

    Private Sub SetDataGrid()
        Dim tblPaket As DataTable = CreateTablePaket()

        tblPaket.Columns("Pilihan").ReadOnly = False
        GridControlPaket.DataSource = tblPaket
        GridViewPaket.BestFitColumns()
        GridViewPaket.Columns("PaketID").Visible = False
        GridViewPaket.Columns("TindakanID").Visible = False
        GridViewPaket.Columns("KodeVoucher").Visible = False
        GridViewPaket.Columns("Tanggal Mulai").Visible = False
        GridViewPaket.Columns("Penjamin").Visible = False
        GridViewPaket.Columns("Harga Awal").Visible = False
        GridViewPaket.Columns("Diskon").Visible = False
        GridViewPaket.Columns("Keterangan Tindakan").Visible = False

        GridViewPaket.SortInfo.ClearAndAddRange(New GridColumnSortInfo() _
                                                {New GridColumnSortInfo(GridViewPaket.Columns("Jenis Paket"), DevExpress.Data.ColumnSortOrder.Ascending) _
                                                 }, 1)
    End Sub

    Private Sub TextBoxKodeVoucher_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBoxKodeVoucher.KeyDown
        If e.KeyCode = Keys.Enter Then PencarianPaket()
    End Sub

    Private Sub ButtonBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBatal.Click
        Me.Close()
    End Sub

    Private Sub ButtonSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSimpan.Click
        Dim tblPaket As DataTable = GridControlPaket.DataSource
        Dim rowPaket() As DataRow = tblPaket.Select("[Pilihan]=1")
        Dim kodeVoucher As String = TextBoxKodeVoucher.Text

        If rowPaket.Count > 0 Then
            Me.Close()
            Select Case formInput
                Case "Pendaftaran Awal"
                    FormPendaftaranAwal.LoadTindakanPaketQR(rowPaket)
                Case "Perencanaan Tindakan"
                    FormRekamMedisDokter.SubmitPotongPaket(rowPaket, kodeVoucher)
                Case Else
                    MsgBox("Error...")
            End Select
        Else
            MsgBox("Tidak ada data yang dipilih.", MsgBoxStyle.Critical, "Warning!")
        End If
    End Sub

    Public Sub SetForm(ByVal NamaForm As String)
        formInput = NamaForm
    End Sub

    'Private Sub TextBoxRMPasien_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxRMPasien.TextChanged
    '    If CheckBoxPaketLain.Checked = False Then Exit Sub

    '    ' 🔥 tahan kalau format belum lengkap
    '    If TextBoxRMPasien.Text.Split("-").Length < 2 Then Exit Sub

    '    Dim noRM As String = GetNoRMFromTextbox()

    '    If noRM <> "" Then

    '        Dim dt As DataTable = LoadComboPaket(noRM)

    '        With ComboBoxPaketTindakan
    '            .DataSource = dt
    '            .DisplayMember = "NamaPaket"
    '            .ValueMember = "KodeVoucher"
    '            .SelectedIndex = -1
    '        End With

    '    End If

    'End Sub
End Class