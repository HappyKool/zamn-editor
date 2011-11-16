Public Class Item
    Public x As UShort
    Public y As UShort
    Public type As Byte

    Public Sub New()

    End Sub

    Public Sub New(ByVal x As UShort, ByVal y As UShort, ByVal type As Byte)
        Me.x = x
        Me.y = y
        Me.type = type
    End Sub

    Public Sub New(ByVal i As Item)
        Me.x = i.x
        Me.y = i.y
        Me.type = i.type
    End Sub

    Public Function GetRect() As Rectangle
        Return New Rectangle(x, y, 15, 15)
    End Function
End Class
