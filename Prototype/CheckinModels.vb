Module CheckinModels
    Public Class CheckinApiResponse
        Private _status As String
        Private _message As String
        Private _total As Integer
        Private _date As String
        Private _data As List(Of PasienCheckinItem)

        Public Property status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property

        Public Property message() As String
            Get
                Return _message
            End Get
            Set(ByVal value As String)
                _message = value
            End Set
        End Property

        Public Property total() As Integer
            Get
                Return _total
            End Get
            Set(ByVal value As Integer) ' <-- Menggunakan Integer
                _total = value
            End Set
        End Property

        Public Property [date]() As String
            Get
                Return _date
            End Get
            Set(ByVal value As String)
                _date = value
            End Set
        End Property

        Public Property data() As List(Of PasienCheckinItem)
            Get
                Return _data
            End Get
            Set(ByVal value As List(Of PasienCheckinItem))
                _data = value
            End Set
        End Property
    End Class

    Public Class PasienCheckinItem
        Private _booking_id As Integer
        Private _patient_name As String
        Private _patient_phone As String
        Private _clinic_name As String
        Private _room_name As String
        Private _doctor_name As String
        Private _nurse_name As String
        Private _nurse_code As String
        Private _treatment_name As String
        Private _start_time As String
        Private _checked_in_at As String

        Public Property booking_id() As Integer
            Get
                Return _booking_id
            End Get
            Set(ByVal value As Integer)
                _booking_id = value
            End Set
        End Property

        Public Property patient_name() As String
            Get
                Return _patient_name
            End Get
            Set(ByVal value As String)
                _patient_name = value
            End Set
        End Property

        Public Property patient_phone() As String
            Get
                Return _patient_phone
            End Get
            Set(ByVal value As String)
                _patient_phone = value
            End Set
        End Property

        Public Property clinic_name() As String
            Get
                Return _clinic_name
            End Get
            Set(ByVal value As String)
                _clinic_name = value
            End Set
        End Property

        Public Property room_name() As String
            Get
                Return _room_name
            End Get
            Set(ByVal value As String)
                _room_name = value
            End Set
        End Property

        Public Property doctor_name() As String
            Get
                Return _doctor_name
            End Get
            Set(ByVal value As String)
                _doctor_name = value
            End Set
        End Property

        Public Property nurse_name() As String
            Get
                Return _nurse_name
            End Get
            Set(ByVal value As String)
                _nurse_name = value
            End Set
        End Property

        Public Property nurse_code() As String
            Get
                Return _nurse_code
            End Get
            Set(ByVal value As String)
                _nurse_code = value
            End Set
        End Property

        Public Property treatment_name() As String
            Get
                Return _treatment_name
            End Get
            Set(ByVal value As String)
                _treatment_name = value
            End Set
        End Property

        Public Property start_time() As String
            Get
                Return _start_time
            End Get
            Set(ByVal value As String)
                _start_time = value
            End Set
        End Property

        Public Property checked_in_at() As String
            Get
                Return _checked_in_at
            End Get
            Set(ByVal value As String)
                _checked_in_at = value
            End Set
        End Property
    End Class
End Module
