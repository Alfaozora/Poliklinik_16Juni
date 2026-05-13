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

    'Public Function ValidasiInformedConsentPaket(ByVal noRm As String, ByVal idTindakan As String, ByVal kodeVoucher As String, ByVal userInput As String) As Boolean

    '    MsgBox("START ValidasiInformedConsentPaket")

    '    Dim statusInformedConsent As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'SettingInformedConsent'")
    '    MsgBox("Setting IC: " & statusInformedConsent)

    '    If statusInformedConsent = "No" Then
    '        MsgBox("IC dimatikan → lanjut")
    '        Return True
    '    End If

    '    ' ===================================
    '    ' CEK APAKAH TINDAKAN BUTUH IC
    '    ' ===================================
    '    Dim wajibIC As String = ReadSQLNIK("SELECT ISNULL(StatusInformedConsent,0) FROM MasterPaketTindakan WHERE PaketID = '" & idTindakan & "'")
    '    MsgBox("Wajib IC Paket: " & wajibIC)

    '    If Not CBool(wajibIC) Then
    '        MsgBox("Tidak wajib IC → lanjut")
    '        Return True
    '    End If

    '    ' ===================================
    '    ' CEK JENIS PAKET
    '    ' ===================================
    '    Dim jenisPaket As String = ReadSQLNIK("SELECT ISNULL(JenisPaket,'') FROM TransaksiPaketTindakan WHERE PaketID = '" & idTindakan & "'")
    '    MsgBox("Jenis Paket: " & jenisPaket)

    '    Dim baseURL As String = ReadSQLNIK("SELECT StrValue FROM MsConfig WHERE ConfigName = 'ws_RM'")
    '    MsgBox("BaseURL: " & baseURL)

    '    If String.IsNullOrEmpty(baseURL) Then
    '        MsgBox("BaseURL kosong!", MsgBoxStyle.Critical)
    '        Return False
    '    End If

    '    Try
    '        ' ===================================
    '        ' PAKET PER-TINDAKAN
    '        ' ===================================
    '        If jenisPaket = "Paket Per-Tindakan" Then

    '            MsgBox("Masuk Paket Per-Tindakan")

    '            Dim jsonDetail As String = ReadSQLNIK("SELECT DetailTindakan FROM TransaksiPaketTindakan WHERE PaketID = '" & idTindakan & "'")
    '            MsgBox("JSON Detail: " & jsonDetail)

    '            If String.IsNullOrEmpty(jsonDetail) Then
    '                MsgBox("Detail tindakan kosong!", MsgBoxStyle.Exclamation)
    '                Return False
    '            End If

    '            Dim arr As JArray = JArray.Parse(jsonDetail)
    '            MsgBox("Jumlah item: " & arr.Count)

    '            For Each item As JObject In arr

    '                MsgBox("Loop item mulai")

    '                Dim tindakanIDDetail As String = ""
    '                If item("TindakanID") IsNot Nothing Then
    '                    tindakanIDDetail = item("TindakanID").ToString()
    '                End If

    '                MsgBox("TindakanID Detail: " & tindakanIDDetail)

    '                Dim wajibICDetail As String = ReadSQLNIK("SELECT ISNULL(StatusInformedConsent,0) FROM MasterTindakan WHERE TindakanID = '" & tindakanIDDetail & "'")
    '                MsgBox("Wajib IC Detail: " & wajibICDetail)

    '                If CBool(wajibICDetail) Then

    '                    Dim urlWebService As String = baseURL & "api/lumiera-luzelle/ic/paket/" & kodeVoucher & "/" & tindakanIDDetail
    '                    MsgBox("URL: " & urlWebService)

    '                    Dim response As String = GetDataAPIRM(urlWebService)
    '                    MsgBox("Response: " & response)

    '                    If String.IsNullOrEmpty(response) Then
    '                        MsgBox("Response kosong!", MsgBoxStyle.Critical)
    '                        Return False
    '                    End If

    '                    Dim json As JObject = JObject.Parse(response)
    '                    MsgBox("Status API: " & If(json("status") IsNot Nothing, json("status").ToString(), "NULL"))

    '                    If json("status") Is Nothing OrElse json("status").ToString() <> "success" Then
    '                        MsgBox(If(json("message") IsNot Nothing, json("message").ToString(), "Gagal tanpa message"), MsgBoxStyle.Exclamation)
    '                        Return False
    '                    End If

    '                End If

    '            Next

    '            MsgBox("Selesai Paket Per-Tindakan")
    '            Return True

    '        End If

    '        ' ===================================
    '        ' PAKET REGULER
    '        ' ===================================
    '        MsgBox("Masuk Paket Reguler")

    '        Dim jsonDetailReguler As String = ReadSQLNIK("SELECT DetailTindakan FROM TransaksiPaketTindakan WHERE PaketID = '" & idTindakan & "'")
    '        MsgBox("JSON Detail Reguler: " & jsonDetailReguler)

    '        If String.IsNullOrEmpty(jsonDetailReguler) Then
    '            MsgBox("Detail tindakan kosong!", MsgBoxStyle.Exclamation)
    '            Return False
    '        End If

    '        Dim arrReg As JArray = JArray.Parse(jsonDetailReguler)
    '        MsgBox("Jumlah item: " & arrReg.Count)

    '        For Each item As JObject In arrReg

    '            MsgBox("Loop item reguler")

    '            Dim tindakanIDDetail As String = ""
    '            If item("TindakanID") IsNot Nothing Then
    '                tindakanIDDetail = item("TindakanID").ToString()
    '            End If

    '            MsgBox("TindakanID Detail: " & tindakanIDDetail)

    '            Dim wajibICDetail As String = ReadSQLNIK("SELECT ISNULL(StatusInformedConsent,0) FROM MasterTindakan WHERE TindakanID = '" & tindakanIDDetail & "'")
    '            MsgBox("Wajib IC Detail: " & wajibICDetail)

    '            If CBool(wajibICDetail) Then

    '                Dim urlWebService As String = baseURL & "api/lumiera-luzelle/ic/paket/" & kodeVoucher & "/" & tindakanIDDetail
    '                MsgBox("URL: " & urlWebService)

    '                Dim response As String = GetDataAPIRM(urlWebService)
    '                MsgBox("Response: " & response)

    '                If String.IsNullOrEmpty(response) Then
    '                    MsgBox("Response kosong!", MsgBoxStyle.Critical)
    '                    Return False
    '                End If

    '                Dim json As JObject = JObject.Parse(response)
    '                MsgBox("Status API: " & If(json("status") IsNot Nothing, json("status").ToString(), "NULL"))

    '                If json("status") Is Nothing OrElse json("status").ToString() <> "success" Then
    '                    MsgBox(If(json("message") IsNot Nothing, json("message").ToString(), "Gagal tanpa message"), MsgBoxStyle.Exclamation)
    '                    Return False
    '                End If

    '            End If

    '        Next

    '        MsgBox("Selesai Paket Reguler")
    '        Return True

    '    Catch ex As Exception
    '        MsgBox("ERROR DETAIL:" & vbCrLf & ex.ToString(), MsgBoxStyle.Critical, "ERROR")
    '        Return False
    '    End Try

    'End Function

    Public Function ValidasiInformedConsentPaket(ByVal noRm As String, ByVal idTindakan As String, ByVal kodeVoucherPaket As String, ByVal userInput As String) As Boolean

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
                        Dim urlWebService As String = baseURL & "api/lumiera-luzelle/ic/paket/" & kodeVoucherPaket & "/" & tindakanIDDetail

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
            'MsgBox("Mulai validasi paket..." & vbCrLf & _
            '       "PaketID : " & idTindakan & vbCrLf & _
            '       "KodeVoucher : " & kodeVoucherPaket)

            Dim jsonDetailReguler As String = ReadSQLNIK("SELECT DetailTindakan FROM TransaksiPaketTindakan WHERE PaketID = '" & idTindakan & "'")

            'MsgBox("JSON Detail Tindakan :" & vbCrLf & jsonDetailReguler)

            If String.IsNullOrEmpty(jsonDetailReguler) Then
                MsgBox("Detail tindakan paket tidak ditemukan.", MsgBoxStyle.Exclamation, "Warning")
                Return False
            End If

            Dim arrReg As JArray = JArray.Parse(jsonDetailReguler)

            'MsgBox("Jumlah detail tindakan : " & arrReg.Count)

            For Each item As JObject In arrReg
                'MsgBox("Item JSON :" & vbCrLf & item.ToString())

                Dim tindakanIDDetail As String = item("TindakanID").ToString()

                'MsgBox("TindakanID Detail : " & tindakanIDDetail)

                ' Cek apakah tindakan ini wajib IC
                Dim wajibICDetail As String = ReadSQLNIK("SELECT ISNULL(StatusInformedConsent,0) FROM MasterTindakan WHERE TindakanID = '" & tindakanIDDetail & "'")

                'MsgBox("Status Informed Consent : " & wajibICDetail)

                If CBool(wajibICDetail) Then
                    'MsgBox("Tindakan WAJIB informed consent")
                    ' 🔥 Kirim TindakanIDDetail (bukan idPaket)
                    Dim urlWebService As String = baseURL & "api/lumiera-luzelle/ic/paket/" & kodeVoucherPaket & "/" & tindakanIDDetail
                    'MsgBox("URL Web Service :" & vbCrLf & urlWebService)
                    Dim response As String = GetDataAPIRM(urlWebService)
                    'MsgBox("Response API :" & vbCrLf & response)
                    Dim json As JObject = JObject.Parse(response)
                    'MsgBox("Status API : " & json("status").ToString())
                    If json("status").ToString() <> "success" Then
                        'MsgBox("Message API :" & vbCrLf & json("message").ToString())
                        MsgBox(json("message").ToString(), MsgBoxStyle.Exclamation, "Warning")
                        Return False
                    Else
                        MsgBox("Validasi informed consent SUCCESS")
                    End If
                Else

                    'MsgBox("Tindakan TIDAK wajib informed consent")
                End If

            Next
            'MsgBox("Semua validasi selesai SUCCESS")
            Return True
        Catch ex As Exception
            MsgBox("Gagal validasi informed consent:" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "ERROR")
            Return False
        End Try

    End Function
End Module
