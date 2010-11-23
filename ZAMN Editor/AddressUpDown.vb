Public Class AddressUpDown

    Public Property Value As Integer
        Get
            Return (Bank.Value - &H80) * &H8000 + Part2.Value - &H7E00
        End Get
        Set(ByVal value As Integer)
            If value = 0 Or value = -1 Then Return
            value -= &H200
            Dim bnk As Integer = value \ &H8000
            Bank.Value = bnk + &H80
            Part2.Value = value - bnk * &H8000 + &H8000
        End Set
    End Property
End Class
