Imports Newtonsoft.Json

Public Module ModelPosWeb

    ' ==========================================
    ' 1. API RESPONSE
    ' ==========================================
    Public Class ApiResponse
        Private _code As Integer
        Private _status As String
        Private _message As String
        Private _data As List(Of Transaksi)

        Public Property code() As Integer
            Get
                Return _code
            End Get
            Set(ByVal value As Integer)
                _code = value
            End Set
        End Property

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

        Public Property data() As List(Of Transaksi)
            Get
                Return _data
            End Get
            Set(ByVal value As List(Of Transaksi))
                _data = value
            End Set
        End Property
    End Class

    ' ==========================================
    ' 2. TRANSAKSI (HEADER)
    ' ==========================================
    Public Class Transaksi
        Private _noRekamMedis As String
        Private _namaLengkap As String
        Private _noTelepon As String
        Private _tanggalTransaksi As String
        Private _jenisPembayaran As String
        Private _noKartu As String
        Private _tagihanPerusahaan As String
        Private _mesinEDC As String
        Private _penerbitKartu As String
        Private _totalPembayaranAwal As Decimal
        Private _totalNettPembayaran As Decimal
        Private _totalPembayaranAkhir As Decimal
        Private _totalKembalian As Decimal
        Private _admin As String
        Private _tanggalLahir As String
        Private _jenisKelamin As String
        Private _alamat As String
        Private _paketTindakan As List(Of Paket)

        Public Property TanggalLahir() As String
            Get
                Return _tanggalLahir
            End Get
            Set(ByVal value As String)
                _tanggalLahir = value
            End Set
        End Property

        Public Property JenisKelamin() As String
            Get
                Return _jenisKelamin
            End Get
            Set(ByVal value As String)
                _jenisKelamin = value
            End Set
        End Property

        Public Property Alamat() As String
            Get
                Return _alamat
            End Get
            Set(ByVal value As String)
                _alamat = value
            End Set
        End Property

        Public Property NoRekamMedis() As String
            Get
                Return _noRekamMedis
            End Get
            Set(ByVal value As String)
                _noRekamMedis = value
            End Set
        End Property

        Public Property NamaLengkap() As String
            Get
                Return _namaLengkap
            End Get
            Set(ByVal value As String)
                _namaLengkap = value
            End Set
        End Property

        Public Property NoTelepon() As String
            Get
                Return _noTelepon
            End Get
            Set(ByVal value As String)
                _noTelepon = value
            End Set
        End Property

        Public Property TanggalTransaksi() As String
            Get
                Return _tanggalTransaksi
            End Get
            Set(ByVal value As String)
                _tanggalTransaksi = value
            End Set
        End Property

        Public Property JenisPembayaran() As String
            Get
                Return _jenisPembayaran
            End Get
            Set(ByVal value As String)
                _jenisPembayaran = value
            End Set
        End Property

        Public Property NoKartu() As String
            Get
                Return _noKartu
            End Get
            Set(ByVal value As String)
                _noKartu = value
            End Set
        End Property

        Public Property TagihanPerusahaan() As String
            Get
                Return _tagihanPerusahaan
            End Get
            Set(ByVal value As String)
                _tagihanPerusahaan = value
            End Set
        End Property

        Public Property MesinEDC() As String
            Get
                Return _mesinEDC
            End Get
            Set(ByVal value As String)
                _mesinEDC = value
            End Set
        End Property

        Public Property PenerbitKartu() As String
            Get
                Return _penerbitKartu
            End Get
            Set(ByVal value As String)
                _penerbitKartu = value
            End Set
        End Property

        Public Property TotalPembayaranAwal() As Decimal
            Get
                Return _totalPembayaranAwal
            End Get
            Set(ByVal value As Decimal)
                _totalPembayaranAwal = value
            End Set
        End Property

        Public Property TotalNettPembayaran() As Decimal
            Get
                Return _totalNettPembayaran
            End Get
            Set(ByVal value As Decimal)
                _totalNettPembayaran = value
            End Set
        End Property

        Public Property TotalPembayaranAkhir() As Decimal
            Get
                Return _totalPembayaranAkhir
            End Get
            Set(ByVal value As Decimal)
                _totalPembayaranAkhir = value
            End Set
        End Property

        Public Property TotalKembalian() As Decimal
            Get
                Return _totalKembalian
            End Get
            Set(ByVal value As Decimal)
                _totalKembalian = value
            End Set
        End Property

        Public Property Admin() As String
            Get
                Return _admin
            End Get
            Set(ByVal value As String)
                _admin = value
            End Set
        End Property

        Public Property PaketTindakan() As List(Of Paket)
            Get
                Return _paketTindakan
            End Get
            Set(ByVal value As List(Of Paket))
                _paketTindakan = value
            End Set
        End Property
    End Class

    ' ==========================================
    ' 3. PAKET
    ' ==========================================
    Public Class Paket
        Private _namaPaket As String
        Private _jenisPoli As String
        Private _jenisPaket As String
        Private _hargaAwal As Decimal
        Private _diskon As Decimal
        Private _hargaAkhir As Decimal
        Private _kuota As Integer
        Private _kodeVoucher As String
        Private _tindakan As List(Of DetailTindakan)
        Private _tanggalMulai As String
        Private _tanggalAkhir As String

        Public Property TanggalMulai() As String
            Get
                Return _tanggalMulai
            End Get
            Set(ByVal value As String)
                _tanggalMulai = value
            End Set
        End Property

        Public Property TanggalAkhir() As String
            Get
                Return _tanggalAkhir
            End Get
            Set(ByVal value As String)
                _tanggalAkhir = value
            End Set
        End Property

        Public Property JenisPoli() As String
            Get
                Return _jenisPoli
            End Get
            Set(ByVal value As String)
                _jenisPoli = value
            End Set
        End Property

        Public Property NamaPaket() As String
            Get
                Return _namaPaket
            End Get
            Set(ByVal value As String)
                _namaPaket = value
            End Set
        End Property

        Public Property JenisPaket() As String
            Get
                Return _jenisPaket
            End Get
            Set(ByVal value As String)
                _jenisPaket = value
            End Set
        End Property

        Public Property HargaAwal() As Decimal
            Get
                Return _hargaAwal
            End Get
            Set(ByVal value As Decimal)
                _hargaAwal = value
            End Set
        End Property

        Public Property Diskon() As Decimal
            Get
                Return _diskon
            End Get
            Set(ByVal value As Decimal)
                _diskon = value
            End Set
        End Property

        Public Property HargaAkhir() As Decimal
            Get
                Return _hargaAkhir
            End Get
            Set(ByVal value As Decimal)
                _hargaAkhir = value
            End Set
        End Property

        Public Property Kuota() As Integer
            Get
                Return _kuota
            End Get
            Set(ByVal value As Integer)
                _kuota = value
            End Set
        End Property

        Public Property KodeVoucher() As String
            Get
                Return _kodeVoucher
            End Get
            Set(ByVal value As String)
                _kodeVoucher = value
            End Set
        End Property

        Public Property Tindakan() As List(Of DetailTindakan)
            Get
                Return _tindakan
            End Get
            Set(ByVal value As List(Of DetailTindakan))
                _tindakan = value
            End Set
        End Property
    End Class

    ' ==========================================
    ' 4. DETAIL TINDAKAN
    ' ==========================================
    Public Class DetailTindakan
        Private _namaTindakan As String
        Private _jenisTindakan As String
        Private _kuotaTindakan As Integer

        Public Property NamaTindakan() As String
            Get
                Return _namaTindakan
            End Get
            Set(ByVal value As String)
                _namaTindakan = value
            End Set
        End Property

        Public Property JenisTindakan() As String
            Get
                Return _jenisTindakan
            End Get
            Set(ByVal value As String)
                _jenisTindakan = value
            End Set
        End Property

        Public Property KuotaTindakan() As Integer
            Get
                Return _kuotaTindakan
            End Get
            Set(ByVal value As Integer)
                _kuotaTindakan = value
            End Set
        End Property
    End Class

    Public Class DetailTindakanItem
        ' Backing Fields
        Private _tindakanID As Integer
        Private _namaTindakan As String
        Private _jenisTindakan As String
        Private _biayaTindakan As Decimal
        Private _diskonTindakan As Decimal
        Private _nettBayar As Decimal
        Private _kuotaTindakan As Decimal
        Private _grandTotal As Decimal

        ' Properties
        Public Property TindakanID() As Integer
            Get
                Return _tindakanID
            End Get
            Set(ByVal value As Integer)
                _tindakanID = value
            End Set
        End Property

        Public Property NamaTindakan() As String
            Get
                Return _namaTindakan
            End Get
            Set(ByVal value As String)
                _namaTindakan = value
            End Set
        End Property

        Public Property JenisTindakan() As String
            Get
                Return _jenisTindakan
            End Get
            Set(ByVal value As String)
                _jenisTindakan = value
            End Set
        End Property

        Public Property BiayaTindakan() As Decimal
            Get
                Return _biayaTindakan
            End Get
            Set(ByVal value As Decimal)
                _biayaTindakan = value
            End Set
        End Property

        Public Property DiskonTindakan() As Decimal
            Get
                Return _diskonTindakan
            End Get
            Set(ByVal value As Decimal)
                _diskonTindakan = value
            End Set
        End Property

        Public Property NettBayar() As Decimal
            Get
                Return _nettBayar
            End Get
            Set(ByVal value As Decimal)
                _nettBayar = value
            End Set
        End Property

        Public Property KuotaTindakan() As Decimal
            Get
                Return _kuotaTindakan
            End Get
            Set(ByVal value As Decimal)
                _kuotaTindakan = value
            End Set
        End Property

        Public Property GrandTotal() As Decimal
            Get
                Return _grandTotal
            End Get
            Set(ByVal value As Decimal)
                _grandTotal = value
            End Set
        End Property
    End Class
End Module