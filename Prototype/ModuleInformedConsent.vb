Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Module ModuleInformedConsent
    Private Function JVal(ByVal obj As JObject, ByVal key As String) As Object
        If obj(key) Is Nothing OrElse obj(key).Type = JTokenType.Null Then
            Return DBNull.Value
        End If
        Return obj(key).ToString()
    End Function

    Private Function JInt(ByVal obj As JObject, ByVal key As String) As Object
        If obj(key) Is Nothing OrElse obj(key).Type = JTokenType.Null Then
            Return DBNull.Value
        End If
        Return Convert.ToInt32(obj(key))
    End Function

    Private Function JDec(ByVal obj As JObject, ByVal key As String) As Object
        If obj(key) Is Nothing OrElse obj(key).Type = JTokenType.Null Then
            Return DBNull.Value
        End If
        Return Convert.ToDecimal(obj(key))
    End Function

    Private Function JBool(ByVal obj As JObject, ByVal key As String) As Object
        If obj(key) Is Nothing OrElse obj(key).Type = JTokenType.Null Then
            Return False
        End If
        Return Convert.ToBoolean(obj(key))
    End Function

    Public Function ValidasiInformedConsentPaket(ByVal noRm As String, ByVal idTindakan As String, ByVal userInput As String) As Boolean

        Dim statusInformedConsent As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'SettingInformedConsent'")

        ' ===================================
        ' JIKA FITUR IC MATI → BOLEH LANJUT
        ' ===================================
        If statusInformedConsent = "No" Then Return True

        ' ===================================
        ' CEK APAKAH TINDAKAN BUTUH IC
        ' ===================================
        Dim wajibIC As String = ReadSQLNIK("SELECT ISNULL(StatusInformedConsent,0) FROM MasterPaketTindakan WHERE PaketID = '" & idTindakan & "'")

        If wajibIC = "0" Then Return True

        Try
            Dim baseURL As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'ws_RM'")

            Dim urlWebService As String = baseURL & "api/lumiera-luzelle/ic/paket/" & noRm & "/" & idTindakan

            Dim response As String = GetDataAPIRM(urlWebService)

            Dim json As JObject = JObject.Parse(response)

            ' ===============================
            ' VALIDASI RESPONSE
            ' ===============================
            If json("status").ToString() <> "success" Then
                MsgBox(json("message").ToString(), MsgBoxStyle.Exclamation, "Warning")
                Return False
            End If

            ' ===============================
            ' INSERT KE DATABASE LOKAL
            ' ===============================
            Dim data As JObject = json("data")

            Dim query As String = "" & _
            "INSERT INTO InformedConsent " & _
            "( " & _
                "NoRekamMedis, " & _
                "NoTransaksi, " & _
                "TglTransaksi, " & _
                "NoNIK, " & _
                "NamaPasien, " & _
                "UmurPasien, " & _
                "JenisKelaminPasien, " & _
                "AlamatPasien, " & _
                "NamaDokter, " & _
                "NamaPerawat, " & _
                "KeteranganDiagnosis, " & _
                "DasarDiagnosis, " & _
                "TindakanID, " & _
                "KeteranganTindakan, " & _
                "IndikasiTindakan, " & _
                "TataCara, " & _
                "TujuanTindakan, " & _
                "Risiko, " & _
                "Komplikasi, " & _
                "Prognosis, " & _
                "AlternatifTindakan, " & _
                "BiayaTindakan, " & _
                "NamaPenanggungJawab, " & _
                "UmurPenanggungJawab, " & _
                "JenisKelaminPenanggungJawab, " & _
                "HubunganPenanggungJawab, " & _
                "AlamatPenanggungJawab, " & _
                "StatusPersetujuan, " & _
                "StatusTransaksi, " & _
                "SignaturePasien1, " & _
                "SignatureDokter1, " & _
                "SignaturePerawat, " & _
                "CreatedBy, " & _
                "CreatedDate " & _
                ") " & _
            "VALUES " & _
            "( " & _
                "@NoRM, " & _
                "@NoTransaksi, " & _
                "@Tgl, " & _
                "@NoNik, " & _
                "@Nama, " & _
                "@Umur, " & _
                "@Sex, " & _
                "@Alamat, " & _
                "@Dokter, " & _
                "@Perawat, " & _
                "@Diagnosis, " & _
                "@Dasar, " & _
                "@TindakanID, " & _
                "@KetTindakan, " & _
                "@Indikasi, " & _
                "@TataCara, " & _
                "@Tujuan, " & _
                "@Risiko, " & _
                "@Komplikasi, " & _
                "@Prognosis, " & _
                "@Alternatif, " & _
                "@Biaya, " & _
                "@PJ, " & _
                "@UmurPJ, " & _
                "@SexPJ, " & _
                "@HubunganPJ, " & _
                "@AlamatPJ, " & _
                "@StatusPersetujuan, " & _
                "@StatusTransaksi, " & _
                "@TTDPasien, " & _
                "@TTDDokter, " & _
                "@TTDPerawat, " & _
                "@User, " & _
            "GETDATE() " & _
            ")"


            WriteParameterizedLokalSQL(query, 4, _
                New SqlParameter("@NoRM", JVal(data, "no_rm")), _
                New SqlParameter("@NoTransaksi", JVal(data, "no_transaksi")), _
                New SqlParameter("@Tgl", JVal(data, "tgl")), _
                New SqlParameter("@NoNik", JVal(data, "no_nik")), _
                New SqlParameter("@Nama", JVal(data, "nama")), _
                New SqlParameter("@Umur", JInt(data, "umur")), _
                New SqlParameter("@Sex", JVal(data, "sex")), _
                New SqlParameter("@Alamat", JVal(data, "alamat")), _
                New SqlParameter("@Dokter", JVal(data, "dokter")), _
                New SqlParameter("@Perawat", JVal(data, "perawat")), _
                New SqlParameter("@Diagnosis", JVal(data, "ket_diagnosis")), _
                New SqlParameter("@Dasar", JVal(data, "dasar_diagnosis")), _
                New SqlParameter("@TindakanID", JVal(data, "id_tindakan")), _
                New SqlParameter("@KetTindakan", JVal(data, "ket_tindakan")), _
                New SqlParameter("@Indikasi", JVal(data, "indikasi_tindakan")), _
                New SqlParameter("@TataCara", JVal(data, "tata_cara")), _
                New SqlParameter("@Tujuan", JVal(data, "tujuan_tindakan")), _
                New SqlParameter("@Risiko", JVal(data, "risiko")), _
                New SqlParameter("@Komplikasi", JVal(data, "komplikasi")), _
                New SqlParameter("@Prognosis", JVal(data, "prognosis")), _
                New SqlParameter("@Alternatif", JVal(data, "alternatif_tindakan")), _
                New SqlParameter("@Biaya", JDec(data, "biaya_tindakan")), _
                New SqlParameter("@PJ", JVal(data, "nama_penanggung_jawab")), _
                New SqlParameter("@UmurPJ", JInt(data, "umur_penanggung_jawab")), _
                New SqlParameter("@SexPJ", JVal(data, "sex_penanggung_jawab")), _
                New SqlParameter("@HubunganPJ", JVal(data, "hubungan_penanggung_jawab")), _
                New SqlParameter("@AlamatPJ", JVal(data, "alamat_penanggung_jawab")), _
                New SqlParameter("@StatusPersetujuan", JBool(data, "status_persetujuan")), _
                New SqlParameter("@StatusTransaksi", JVal(data, "status_transaksi")), _
                New SqlParameter("@TTDPasien", JVal(data, "ttd_pasien")), _
                New SqlParameter("@TTDDokter", JVal(data, "ttd_dokter")), _
                New SqlParameter("@TTDPerawat", JVal(data, "ttd_perawat")), _
                New SqlParameter("@User", userInput))

            Return True

        Catch ex As Exception
            MsgBox("Gagal validasi informed consent:" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "ERROR")
            Return False
        End Try

    End Function
End Module
