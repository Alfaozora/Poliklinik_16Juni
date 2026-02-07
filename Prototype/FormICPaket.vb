Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Linq
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
Imports System.Windows.Forms
Imports System.Web.Script.Serialization


Public Class FormICPaket
    Public NamaForm As String
    Public NoRM As String
    Public PaketID As String
    Public StatusPersetujuan As Boolean
    Public poli As String
    Public SelectedPaketID As String
    Public namaDokter As String
    Public namaPerawat As String
    Public KodeDokter As String = ""

    Public Sub SetFormPaket(ByVal _namaForm As String, ByVal _noRM As String, ByVal _paketID As String, ByVal _status As Boolean, ByVal _poli As String, ByVal _namaDokter As String, ByVal _namaPerawat As String)

        NamaForm = _namaForm
        NoRM = _noRM
        StatusPersetujuan = _status
        poli = _poli
        SelectedPaketID = _paketID
        namaPerawat = _namaPerawat

        Select Case _poli.ToUpper()

            Case "POLI KECANTIKAN"
                namaDokter = "dr. Laurensia Scorvita"
                KodeDokter = "23021103"
            Case "POLI HBOT"
                namaDokter = "DR. ADRIANUS MICHAEL FOEH"
                KodeDokter = "18160012"
            Case "POLI FISIOTERAPI"
                namaDokter = "RICHARDO ROSARIO ADITYA PRAMANDA PUTRA"
                KodeDokter = "18170007"
            Case Else
                namaDokter = _namaDokter
                KodeDokter = ""

        End Select

    End Sub

    Private Sub FormICPaket_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadInformedConsentPaket()
    End Sub

    'Private Sub ButtonSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSubmit.Click
    '    Try
    '        Dim json As New JObject
    '        Dim baseURL As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'ws_RM'")
    '        Dim kodeDokter As String = ReadSQLNIK("SELECT ID FROM MasterDokter WHERE Nama = '" & NamaDokter & "'")
    '        Dim sql As String = "SELECT * FROM MasterPasien WHERE NoRekamMedis = '" & NoRM & "'"
    '        Dim dt As DataTable = ReadSQL(sql)

    '        If dt.Rows.Count = 0 Then
    '            MsgBox("Data pasien tidak ditemukan", MsgBoxStyle.Exclamation)
    '            Exit Sub
    '        End If
    '        Dim row As DataRow = dt.Rows(0)

    '        Dim tglLahir As Date = CDate(row("TglLahir"))

    '        ' =====================
    '        ' DATA PASIEN
    '        ' =====================
    '        json("nama") = row("Nama").ToString()
    '        json("no_rm") = NoRM
    '        json("no_hp") = row("NoHandphone").ToString()
    '        json("no_nik") = row("NoNIK").ToString()
    '        json("umur") = HitungUsia(tglLahir).ToString()
    '        json("sex") = row("JenisKelamin").ToString()
    '        json("alamat") = row("DomisiliAlamat").ToString()

    '        ' =====================
    '        ' TRANSAKSI
    '        ' =====================
    '        json("no_transaksi") = ""
    '        json("tgl") = Date.Now.ToString("yyyy-MM-dd")
    '        json("perawatan") = TextBoxNamaTindakan.Text
    '        json("status_transaksi") = "pending"

    '        ' =====================
    '        ' PETUGAS
    '        ' =====================
    '        json("dokter") = NamaDokter
    '        json("kode_dokter") = kodeDokter
    '        json("perawat") = NamaPerawat

    '        ' =====================
    '        ' MEDIS
    '        ' =====================
    '        json("ket_diagnosis") = TextBoxDiagnosa.Text
    '        json("dasar_diagnosis") = TextBoxDasarDiagnosa.Text
    '        json("id_tindakan") = SelectedPaketID
    '        json("ket_tindakan") = TextBoxNamaTindakan.Text
    '        json("indikasi_tindakan") = ComboBoxIndikasiTindakan.Text
    '        json("tata_cara") = ComboBoxTataCara.Text
    '        json("tujuan_tindakan") = ComboBoxTujuanTindakan.Text
    '        json("risiko") = ComboBoxResiko.Text
    '        json("komplikasi") = ComboBoxKomplikasi.Text
    '        json("prognosis") = ComboBoxPrognosa.Text
    '        json("alternatif_tindakan") = ComboBoxAlternatif.Text
    '        json("biaya_tindakan") = TextBoxBiayaTindakan.Text.Replace(",", "")

    '        ' =====================
    '        ' PENANGGUNG JAWAB
    '        ' =====================
    '        json("nama_penanggung_jawab") = TextBoxPenanggungJawabNama.Text
    '        json("umur_penanggung_jawab") = TextBoxPenanggungJawabUsia.Text
    '        json("sex_penanggung_jawab") = ComboBoxPenanggungJawabJenisKelamin.Text
    '        json("hubungan_penanggung_jawab") = TextBoxPenanggungJawabHubunganPasien.Text
    '        json("alamat_penanggung_jawab") = TextBoxPenanggungJawabAlamat.Text

    '        ' =====================
    '        ' STATUS
    '        ' =====================
    '        json("status_persetujuan") = StatusPersetujuan

    '        ' =====================
    '        ' KIRIM KE API
    '        ' =====================
    '        Dim urlWebService As String = baseURL & "api/lumiera-luzelle/ic"
    '        Dim response As String = InsertDataAPIRM(json.ToString(), urlWebService)

    '        ' =====================
    '        ' PARSE RESPONSE
    '        ' =====================
    '        Dim result As JObject = JObject.Parse(response)

    '        If result("metaData")("code").ToString() = "200" Then
    '            MsgBox("Informed consent berhasil dikirim", MsgBoxStyle.Information)
    '            Me.Close()
    '        Else
    '            MsgBox(result.ToString(), MsgBoxStyle.Critical)
    '        End If

    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical)
    '    End Try
    'End Sub

    Private Function SafeText(ByVal ctrl As Control) As String
        If ctrl Is Nothing Then Return ""
        If ctrl.Text Is Nothing Then Return ""
        Return ctrl.Text.Trim()
    End Function

    Private Function SafeValue(ByVal v As Object) As String
        If v Is Nothing Then Return ""
        Return v.ToString()
    End Function

    Private Function JsonSafe(ByVal value As String) As String
        If value Is Nothing Then Return ""

        value = value.Replace("\", "\\")
        value = value.Replace("""", "\""")
        value = value.Replace(vbCrLf, "\n")
        value = value.Replace(vbCr, "\n")
        value = value.Replace(vbLf, "\n")

        Return value.Trim()
    End Function

    Private Function JsonBool(ByVal val As Boolean) As String
        If val Then
            Return "true"
        Else
            Return "false"
        End If
    End Function

    Private Sub ButtonSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSubmit.Click
        Try
            Dim urlWebService As String = "https://mmc-online.mitramedicare.com/api/lumiera-luzelle/ic"

            Dim sql As String = "SELECT * FROM MasterPasien WHERE NoRekamMedis = '" & NoRM & "'"

            Dim dt As DataTable = ReadSQL(sql)

            If dt.Rows.Count = 0 Then
                MsgBox("STOP - Pasien tidak ditemukan")
                Exit Sub
            End If

            Dim row As DataRow = dt.Rows(0)

            Dim tglLahir As Date = CDate(row("TglLahir"))

            Dim biaya As String = "0"
            If TextBoxBiayaTindakan IsNot Nothing Then
                If Not String.IsNullOrEmpty(TextBoxBiayaTindakan.Text) Then
                    biaya = TextBoxBiayaTindakan.Text.Replace(",", "")
                End If
            End If

            Dim parameter As String = "{" & _
                """nama"":""" & JsonSafe(row("Nama").ToString()) & """," & _
                """no_rm"":""" & NoRM & """," & _
                """no_hp"":""" & JsonSafe(row("NoHandphone").ToString()) & """," & _
                """no_nik"":""" & JsonSafe(row("NoNIK").ToString()) & """," & _
                """umur"":""" & HitungUsia(tglLahir) & """," & _
                """sex"":""" & JsonSafe(row("JenisKelamin").ToString()) & """," & _
                """alamat"":""" & JsonSafe(row("DomisiliAlamat").ToString()) & """," & _
                """no_transaksi"":""-""," & _
                """tgl"":""" & Date.Now.ToString("yyyy-MM-dd") & """," & _
                """perawatan"":""" & JsonSafe(TextBoxNamaTindakan.Text) & """," & _
                """dokter"":""" & JsonSafe(SafeValue(namaDokter)) & """," & _
                """kode_dokter"":""" & JsonSafe(SafeValue(KodeDokter)) & """," & _
                """perawat"":""" & JsonSafe(SafeValue(namaPerawat)) & """," & _
                """ket_diagnosis"":""" & JsonSafe(SafeText(TextBoxDiagnosa)) & """," & _
                """dasar_diagnosis"":""" & JsonSafe(SafeText(TextBoxDasarDiagnosa)) & """," & _
                """id_tindakan"":""" & JsonSafe(SafeValue(SelectedPaketID)) & """," & _
                """ket_tindakan"":""" & JsonSafe(SafeText(TextBoxNamaTindakan)) & """," & _
                """indikasi_tindakan"":""" & JsonSafe(ComboBoxIndikasiTindakan.Text) & """," & _
                """tata_cara"":""" & JsonSafe(ComboBoxTataCara.Text) & """," & _
                """tujuan_tindakan"":""" & JsonSafe(ComboBoxTujuanTindakan.Text) & """," & _
                """risiko"":""" & JsonSafe(ComboBoxResiko.Text) & """," & _
                """komplikasi"":""" & JsonSafe(ComboBoxKomplikasi.Text) & """," & _
                """prognosis"":""" & JsonSafe(ComboBoxPrognosa.Text) & """," & _
                """alternatif_tindakan"":""" & JsonSafe(ComboBoxAlternatif.Text) & """," & _
                """biaya_tindakan"":" & biaya & "," & _
                """nama_penanggung_jawab"":""" & JsonSafe(TextBoxPenanggungJawabNama.Text) & """," & _
                """umur_penanggung_jawab"":""" & JsonSafe(TextBoxPenanggungJawabUsia.Text) & """," & _
                """sex_penanggung_jawab"":""" & JsonSafe(row("JenisKelamin").ToString()) & """," & _
                """hubungan_penanggung_jawab"":""" & JsonSafe(TextBoxPenanggungJawabHubunganPasien.Text) & """," & _
                """alamat_penanggung_jawab"":""" & JsonSafe(TextBoxPenanggungJawabAlamat.Text) & """," & _
                """status_persetujuan"":" & JsonBool(StatusPersetujuan) & "," & _
                """status_transaksi"":""pending""" & _
            "}"

            Dim response As String = InsertDataAPIRM(parameter, urlWebService)

            Dim jsonObj As JObject = JObject.Parse(response)

            If jsonObj("metaData") Is Nothing Then
                MsgBox("Response API tidak valid:" & vbCrLf & response, MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim code As String = jsonObj("metaData")("code").ToString()

            If code = "200" Then

                MsgBox("Berhasil disimpan." & vbCrLf & vbCrLf & "Silahkan menuju web Inform Consent untuk tanda tangan pasien.", MsgBoxStyle.Information, "Informed Consent")

                Me.Close()

            Else
                MsgBox("Gagal menyimpan data." & vbCrLf & vbCrLf & response, MsgBoxStyle.Critical, "API Error")
            End If

        Catch ex As Exception
            MsgBox("ERROR DI STEP:" & vbCrLf & ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub CheckBoxPenanggungJawab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxPenanggungJawab.CheckedChanged
        If CheckBoxPenanggungJawab.Checked = True Then
            LoadPenanggungJawabPasien()
        Else
            KosongkanPenanggungJawab()
        End If
    End Sub

    Private Sub LoadPenanggungJawabPasien()
        Dim sql As String = "SELECT * FROM MasterPasien WHERE NoRekamMedis = '" & NoRM & "'"

        Dim dt As DataTable = ReadSQL(sql)

        If dt Is Nothing Then
            MsgBox("Query pasien gagal (dt = Nothing)")
            Exit Sub
        End If

        If dt.Rows.Count = 0 Then
            MsgBox("Data pasien tidak ditemukan")
            Exit Sub
        End If

        Dim row As DataRow = dt.Rows(0)

        ' =====================
        ' ISI FIELD
        ' =====================
        TextBoxPenanggungJawabNama.Text = row("Nama").ToString()
        TextBoxPenanggungJawabHubunganPasien.Text = "Pasien Sendiri"
        TextBoxPenanggungJawabNama.ReadOnly = True
        TextBoxPenanggungJawabHubunganPasien.ReadOnly = True

        ' =====================
        ' HITUNG USIA
        ' =====================
        Dim tglLahir As Date = CDate(row("TglLahir"))
        TextBoxPenanggungJawabUsia.Text = HitungUsia(tglLahir).ToString()
        TextBoxPenanggungJawabUsia.ReadOnly = True

        ' =====================
        ' JENIS KELAMIN
        ' =====================
        ComboBoxPenanggungJawabJenisKelamin.Text = row("JenisKelamin").ToString()

        ' =====================
        ' ALAMAT
        ' =====================
        TextBoxPenanggungJawabAlamat.Text = row("DomisiliAlamat").ToString()
    End Sub

    Private Function HitungUsia(ByVal tglLahir As Date) As Integer
        Dim usia As Integer = Date.Now.Year - tglLahir.Year

        If Date.Now < tglLahir.AddYears(usia) Then
            usia -= 1
        End If

        Return usia
    End Function

    Private Sub KosongkanPenanggungJawab()
        TextBoxPenanggungJawabNama.Clear()
        TextBoxPenanggungJawabHubunganPasien.Clear()
        TextBoxPenanggungJawabUsia.Clear()
        TextBoxPenanggungJawabAlamat.Clear()

        TextBoxPenanggungJawabHubunganPasien.ReadOnly = False
        TextBoxPenanggungJawabHubunganPasien.ReadOnly = False
        TextBoxPenanggungJawabUsia.ReadOnly = False

        ComboBoxPenanggungJawabJenisKelamin.SelectedIndex = -1
    End Sub

    Private Sub SetComboBoxSelectedIndex(ByVal cmb As ComboBox)

        If cmb.Items.Count > 0 Then
            cmb.SelectedItem = cmb.Items(0)
            cmb.Refresh()
        End If

    End Sub

    Private Sub SetDefaultCombo()
        SetComboBoxSelectedIndex(ComboBoxIndikasiTindakan)
        SetComboBoxSelectedIndex(ComboBoxTataCara)
        SetComboBoxSelectedIndex(ComboBoxTujuanTindakan)
        SetComboBoxSelectedIndex(ComboBoxResiko)
        SetComboBoxSelectedIndex(ComboBoxKomplikasi)
        SetComboBoxSelectedIndex(ComboBoxPrognosa)
        SetComboBoxSelectedIndex(ComboBoxAlternatif)
    End Sub

    Public Class DetailTindakanItem
        Public TindakanID As Integer
        Public NamaTindakan As String
        Public JenisTindakan As String
        Public BiayaTindakan As Decimal
        Public DiskonTindakan As Decimal
        Public NettBayar As Decimal
        Public KuotaTindakan As Decimal
        Public GrandTotal As Decimal
    End Class


    Private Sub LoadInformedConsentPaket()

        ' ===================================
        ' AMBIL NAMA PAKET
        ' ===================================
        Dim dtPaket As DataTable = ReadSQL("Select NamaPaket, HargaNett, DetailTindakan FROM MasterPaketTindakan WHERE PaketID = '" & SelectedPaketID & "'")

        If dtPaket.Rows.Count > 0 Then

            Dim row As DataRow = dtPaket.Rows(0)
            Dim jsonDetail As String = ""

            If Not IsDBNull(row("DetailTindakan")) Then
                jsonDetail = row("DetailTindakan").ToString()
            End If

            If jsonDetail <> "" Then
                Try
                    Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim listTindakan As List(Of DetailTindakanItem) = serializer.Deserialize(Of List(Of DetailTindakanItem))(jsonDetail)

                    If listTindakan IsNot Nothing AndAlso listTindakan.Count > 0 Then
                        TextBoxNamaTindakan.Text = listTindakan(0).NamaTindakan
                    Else
                        TextBoxNamaTindakan.Text = row("NamaPaket").ToString()
                    End If
                Catch ex As Exception
                    ' fallback jika JSON error
                    TextBoxNamaTindakan.Text = row("NamaPaket").ToString()
                End Try
            Else
                TextBoxNamaTindakan.Text = row("NamaPaket").ToString()
            End If

            TextBoxBiayaTindakan.Text = Format(CDec(row("HargaNett")), "#,##0")
            TextBoxDiagnosa.Text = "General examination and investigation of persons without complaint and reported diagnosis (Z00)"

        Else
            TextBoxNamaTindakan.Text = ""
            TextBoxBiayaTindakan.Text = ""
            TextBoxDiagnosa.Text = ""
        End If


        ' ===================================
        ' INFORMED CONSENT PAKET
        ' ===================================
        Dim sql As String = "SELECT * " & _
            "FROM Klinik_MasterTindakanInformedConsent " & _
            "WHERE NamaTindakan IN ( " & _
                "SELECT MT.TindakanInformedConsent " & _
                "FROM MasterPaketTindakanDetail MP " & _
                "INNER JOIN MasterTindakan MT " & _
                    "ON MP.TindakanID = MT.TindakanID " & _
                "WHERE MP.PaketID = '" & SelectedPaketID & "')"

        Dim dt As DataTable = ReadSQL(sql)

        ' ========= CLEAR =========
        ComboBoxIndikasiTindakan.Items.Clear()
        ComboBoxTataCara.Items.Clear()
        ComboBoxTujuanTindakan.Items.Clear()
        ComboBoxResiko.Items.Clear()
        ComboBoxKomplikasi.Items.Clear()
        ComboBoxPrognosa.Items.Clear()
        ComboBoxAlternatif.Items.Clear()

        For Each row As DataRow In dt.Rows

            Select Case row("NamaInformasi").ToString()

                Case "Indikasi Tindakan"
                    ComboBoxIndikasiTindakan.Items.Add(row("KeteranganInformasi"))

                Case "Tata Cara"
                    ComboBoxTataCara.Items.Add(row("KeteranganInformasi"))

                Case "Tujuan Tindakan"
                    ComboBoxTujuanTindakan.Items.Add(row("KeteranganInformasi"))

                Case "Risiko"
                    ComboBoxResiko.Items.Add(row("KeteranganInformasi"))

                Case "Komplikasi"
                    ComboBoxKomplikasi.Items.Add(row("KeteranganInformasi"))

                Case "Prognosis"
                    ComboBoxPrognosa.Items.Add(row("KeteranganInformasi"))

                Case "Alternatif tindakan dan risiko"
                    ComboBoxAlternatif.Items.Add(row("KeteranganInformasi"))

                Case "Dasar Diagnosis"
                    TextBoxDasarDiagnosa.Text = row("KeteranganInformasi")

            End Select
        Next

        Me.BeginInvoke(New MethodInvoker(AddressOf SetDefaultCombo))

    End Sub

End Class