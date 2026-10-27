Public Class FormPreOrderCartridge
    Private userInput As String = PoliKlinik.Lbluser.Text

    Private Sub FormPreOrderCartridge_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim dt As DataTable

        dt = ReadSQL("SELECT " & _
                     "A.KodeAlat, C.NamaCartridge, A.NamaAlat, " & _
                     "(C.KuotaTotal - C.KuotaTerpakai) AS SisaKuota, " & _
                     "C.KuotaMinimum " & _
                     "FROM MasterCartridge C " & _
                     "INNER JOIN MasterAlat A ON C.AlatID = A.AlatID " & _
                     "WHERE C.IsAktif = 1")

        DataGridView1.DataSource = dt

        'tambah kolom checkbox
        Dim colCheck As New DataGridViewCheckBoxColumn
        colCheck.HeaderText = "Pilih"
        colCheck.Name = "chkOrder"
        colCheck.Width = 60

        DataGridView1.Columns.Add(colCheck)

        'pindahkan checkbox ke kolom pertama
        DataGridView1.Columns("chkOrder").DisplayIndex = 0

        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.AllowUserToAddRows = False

        'buat semua kolom readonly kecuali checkbox
        For Each col As DataGridViewColumn In DataGridView1.Columns
            If col.Name <> "chkOrder" Then
                col.ReadOnly = True
            End If
        Next

        'highlight cartridge hampir habis
        For Each row As DataGridViewRow In DataGridView1.Rows

            If Not row.IsNewRow Then

                Dim sisa As Integer = CInt(row.Cells("SisaKuota").Value)
                Dim minimum As Integer = CInt(row.Cells("KuotaMinimum").Value)

                If sisa <= minimum Then
                    row.DefaultCellStyle.BackColor = Color.LightCoral
                    row.Cells("chkOrder").Value = True
                End If

            End If

        Next

    End Sub

    Private Sub ButtonGeneratePreOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonGeneratePreOrder.Click
        Dim dt As New DataTable

        dt.Columns.Add("KodeAlat")
        dt.Columns.Add("NamaCartridge")
        dt.Columns.Add("NamaAlat")
        dt.Columns.Add("SisaKuota")

        For Each row As DataGridViewRow In DataGridView1.Rows

            If Not row.IsNewRow Then

                If row.Cells("chkOrder").Value = True Then

                    Dim dr As DataRow = dt.NewRow

                    dr("KodeAlat") = row.Cells("KodeAlat").Value
                    dr("NamaCartridge") = row.Cells("NamaCartridge").Value
                    dr("NamaAlat") = row.Cells("NamaAlat").Value
                    dr("SisaKuota") = row.Cells("SisaKuota").Value

                    dt.Rows.Add(dr)

                End If

            End If

        Next

        If dt.Rows.Count = 0 Then
            MsgBox("Tidak ada cartridge yang dipilih.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If


        'Load report
        Dim report As New DevExpress.XtraReports.UI.XtraReport
        report = XtraReport.FromFile(Application.StartupPath + "\reports\SuratPreOrder.repx", True)

        Dim ctrl As XRControl = report.FindControl("lblTanggal", True)

        If ctrl IsNot Nothing Then
            ctrl.Text = Format(DateTime.Now, "dd MMMM yyyy")
        End If

        Dim ctrlUser As XRControl = report.FindControl("lblUser", True)

        If ctrlUser IsNot Nothing Then
            ctrlUser.Text = userInput
        End If

        'report.Bands.Item(BandKind.PageFooter).Controls("lblprinted").Text = Format(DateTime.Now, "dd-MMM-yyyy HH:mm:ss") & " By " & userInput

        report.DataSource = dt

        'report.RunDesigner()
        report.ShowPreview()
    End Sub
End Class