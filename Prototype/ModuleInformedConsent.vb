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

    Public Function ValidasiInformedConsentPaket(ByVal noRm As String, ByVal idTindakan As String, ByVal kodeVoucher As String, ByVal userInput As String) As Boolean

        Dim statusInformedConsent As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'SettingInformedConsent'")

        ' ===================================
        ' JIKA FITUR IC MATI → BOLEH LANJUT
        ' ===================================
        If statusInformedConsent = "No" Then Return True

        ' ===================================
        ' CEK APAKAH TINDAKAN BUTUH IC
        ' ===================================
        Dim wajibIC As String = ReadSQLNIK("SELECT ISNULL(StatusInformedConsent,0) FROM MasterPaketTindakan WHERE PaketID = '" & idTindakan & "'")

        If Not CBool(wajibIC) Then Return True

        ' ===================================
        ' CEK JENIS PAKET
        ' ===================================
        Dim jenisPaket As String = ReadSQLNIK("SELECT ISNULL(JenisPaket,'') FROM TransaksiPaketTindakan WHERE PaketID = '" & idTindakan & "'")

        Dim baseURL As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'ws_RM'")

        Try
            ' ===================================
            ' PAKET PER-TINDAKAN
            ' ===================================
            If jenisPaket = "Paket Per-Tindakan" Then

                Dim jsonDetail As String = ReadSQLNIK("SELECT DetailTindakan FROM TransaksiPaketTindakan WHERE PaketID = '" & idTindakan & "'")

                If String.IsNullOrEmpty(jsonDetail) Then
                    MsgBox("Detail tindakan paket tidak ditemukan.", MsgBoxStyle.Exclamation, "Warning")
                    Return False
                End If

                Dim arr As JArray = JArray.Parse(jsonDetail)

                For Each item As JObject In arr

                    Dim tindakanIDDetail As String = item("TindakanID").ToString()

                    ' Cek apakah tindakan ini wajib IC
                    Dim wajibICDetail As String = ReadSQLNIK("SELECT ISNULL(StatusInformedConsent,0) FROM MasterTindakan WHERE TindakanID = '" & tindakanIDDetail & "'")

                    If CBool(wajibICDetail) Then

                        ' 🔥 Kirim TindakanIDDetail (bukan idPaket)
                        Dim urlWebService As String = baseURL & "api/lumiera-luzelle/ic/paket/" & kodeVoucher & "/" & tindakanIDDetail

                        Dim response As String = GetDataAPIRM(urlWebService)
                        Dim json As JObject = JObject.Parse(response)

                        If json("status").ToString() <> "success" Then
                            MsgBox(json("message").ToString(), MsgBoxStyle.Exclamation, "Warning")
                            Return False
                        End If

                    End If

                Next

                Return True

            End If


            ' ===================================
            ' PAKET REGULER
            ' ===================================
            Dim jsonDetailReguler As String = ReadSQLNIK("SELECT DetailTindakan FROM TransaksiPaketTindakan WHERE PaketID = '" & idTindakan & "'")

            If String.IsNullOrEmpty(jsonDetailReguler) Then
                MsgBox("Detail tindakan paket tidak ditemukan.", MsgBoxStyle.Exclamation, "Warning")
                Return False
            End If

            Dim arrReg As JArray = JArray.Parse(jsonDetailReguler)

            For Each item As JObject In arrReg

                Dim tindakanIDDetail As String = item("TindakanID").ToString()

                ' Cek apakah tindakan ini wajib IC
                Dim wajibICDetail As String = ReadSQLNIK("SELECT ISNULL(StatusInformedConsent,0) FROM MasterTindakan WHERE TindakanID = '" & tindakanIDDetail & "'")

                If CBool(wajibICDetail) Then

                    ' 🔥 Kirim TindakanIDDetail (bukan idPaket)
                    Dim urlWebService As String = baseURL & "api/lumiera-luzelle/ic/paket/" & kodeVoucher & "/" & tindakanIDDetail
                    'MsgBox(urlWebService)
                    Dim response As String = GetDataAPIRM(urlWebService)
                    Dim json As JObject = JObject.Parse(response)

                    If json("status").ToString() <> "success" Then
                        MsgBox(json("message").ToString(), MsgBoxStyle.Exclamation, "Warning")
                        Return False
                    End If

                End If

            Next

            Return True
        Catch ex As Exception
            MsgBox("Gagal validasi informed consent:" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "ERROR")
            Return False
        End Try

    End Function
End Module
