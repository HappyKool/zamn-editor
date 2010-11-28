Public Class AddressUpDown

    Public Property Value As Integer
        Get
            Return (Bank.Value - &H80) * &H8000 + Part2.Value - &H7E00
        End Get
        Set(ByVal value As Integer)
            If value = 0 Or value = -1 Then
                Me.Value = 512
                Return
            End If
            value -= &H200
            Dim bnk As Integer = value \ &H8000
            Bank.Value = bnk + &H80
            Part2.Value = value - bnk * &H8000 + &H8000
        End Set
    End Property

    Public Event ValueChanged(ByVal sender As Object, ByVal e As EventArgs)

    Private Sub nud_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Bank.ValueChanged, Part2.ValueChanged
        RaiseEvent ValueChanged(Me, EventArgs.Empty)
    End Sub
End Class
