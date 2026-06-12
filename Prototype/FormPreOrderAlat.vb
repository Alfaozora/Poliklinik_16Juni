Imports Newtonsoft.Json
Imports Newtonsoft
Imports System.Security.Cryptography
Imports System.Collections.Generic
Imports Newtonsoft.Json.Linq
Imports System.Drawing.Imaging
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Drawing
Imports PoliKlinik_Net2.ShapeDrawingFunctions
Imports System.Text
Imports System.Globalization
Imports System.Net
Public Class FormPreOrderAlat

    Private Sub ButtonGetSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonGetSupplier.Click
        FormSupplier.ShowDialog()
    End Sub
End Class