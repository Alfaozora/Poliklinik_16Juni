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

Public Class FindDiagnosa
    Private formName As String = "PENCARIAN DIAGNOSA"
    Private TForm As String

    Private Class varJson
        Public metaData As MetaData
        Public response As v4_BPJS
    End Class

    Function CreateTableICD() As DataTable
        Dim Thasil As New DataTable

        insertRowToTbls(Thasil, "Typstring", "Kode Diagnosa")
        insertRowToTbls(Thasil, "Typstring", "Nama Diagnosa")
        insertRowToTbls(Thasil, "typchecked", "Non Spesialis")

        For Each Tcol As DataColumn In Thasil.Columns
            Tcol.ReadOnly = True
        Next

        Return Thasil
    End Function

    Private Sub PanelTop_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PanelTop.Paint
        Try
            e.Graphics.DrawString(formName, New Font("Arial", 12, FontStyle.Bold), Brushes.White, New Point(10, 10))
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub SetForm(ByRef Form As String)
        Me.BringToFront()
        TForm = Form
    End Sub

    Private Sub SetAwal()
        Me.CenterToScreen()
        TextBoxKriteria.Focus()
        TextBoxKriteria.ResetText()
    End Sub

    Private Sub FindQuery()
        Try
            If Checking() = True Then
                Dim kriteria As String = TextBoxKriteria.Text
                Dim Tdata As DataTable = New DataTable

                Tdata = CreateTableICD()
                Tdata.Rows.Clear()

                Dim getResponse As String = ""
                Dim urlPcare As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName='pcare_base_url'")
                Dim uri As String = urlPcare & "diagnosa/" & kriteria & "/1/100"

                getResponse = getDataFromPcare(uri)

                If getResponse <> "Nothing" Then
                    Dim varsespoli() = Newtonsoft.Json.JsonConvert.DeserializeObject(Of varJson())(getResponse)

                    For Each oneVar As varJson In varsespoli
                        ' Tambah eror pas isian kosong
                        If oneVar.metaData.message = "OK" Then
                            For index As Integer = 0 To oneVar.response.list.Length - 1
                                Dim Tlastnew As DataRow = Tdata.NewRow
                                Tlastnew("Kode Diagnosa") = oneVar.response.list(index).kdDiag
                                Tlastnew("Nama Diagnosa") = oneVar.response.list(index).nmDiag
                                Tlastnew("Non Spesialis") = oneVar.response.list(index).nonSpesialis
                                Tdata.Rows.Add(Tlastnew)
                            Next
                        Else
                            MsgBox("Data tidak ditemukan.", MsgBoxStyle.Critical, "Warning!")
                        End If
                    Next
                Else
                    MsgBox("Data tidak ditemukan.", MsgBoxStyle.Critical, "Warning!")
                End If

                GridControlPencarian.DataSource = Tdata
                GridViewPencarian.BestFitColumns()
                'GridViewPencarian.Columns("NonSpesialis ").Visible = False
            End If
        Catch ex As Exception
            MsgBox("Eror : " & ex.Message, MsgBoxStyle.Exclamation, "Error!")
        End Try
    End Sub

    Private Sub Find_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GridControlPencarian.DataSource = CreateTableICD()
        SetAwal()
    End Sub

    Private Sub LoadData(ByRef Baris As Integer)
        If GridViewPencarian.RowCount > 0 Then
            Dim Grdrow As DataRowView = GridViewPencarian.GetRow(Baris)

            Select Case TForm
                Case "RekamMedisDokterDiagnosa"
                    FormRekamMedisDokter.LoadDiagnosa(Grdrow, TForm)
                Case "DiagnosaComplicationTACC", "DiagnosaComorbidityTACC"
                    FindRujukanTACC.LoadDiagnosa(Grdrow, TForm)
            End Select

            Me.Close()
        End If
    End Sub

    Private Sub GridControl1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridControlPencarian.DoubleClick
        If GridViewPencarian.FocusedRowHandle >= 0 Then
            Dim Index() As Integer = GridViewPencarian.GetSelectedRows
            If Index.Length > 0 Then
                LoadData(Index(0))
            End If
        End If
    End Sub

    Private Sub txKriteria_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBoxKriteria.KeyDown
        If e.KeyCode = Keys.Enter Then FindQuery()
    End Sub

    Function Checking() As Boolean
        If TextBoxKriteria.Text = "" Then
            MsgBox("Kriteria pencarian tidak boleh kosong.", MsgBoxStyle.Critical, "Warning!")
            Return False
        End If

        Return True
    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub ButtonFindDiagnosa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFindDiagnosa.Click
        FindQuery()
    End Sub
End Class