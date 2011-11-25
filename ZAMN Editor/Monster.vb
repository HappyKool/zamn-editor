Public Class Monster
    Public x As Integer
    Public y As Integer
    Public radius As Byte
    Public delay As Byte
    Public ptr As Integer
    Public index As Integer

    Public Sub New()

    End Sub

    Public Sub New(ByVal radius As Byte, ByVal x As Integer, ByVal y As Integer, ByVal delay As Byte, ByVal ptr As Integer)
        Me.radius = radius
        Me.x = x
        Me.y = y
        Me.delay = delay
        Me.ptr = ptr
        UpdateIdx()
    End Sub

    Public Sub New(ByVal m As Monster)
        Me.radius = m.radius
        Me.x = m.x
        Me.y = m.y
        Me.delay = m.delay
        Me.ptr = m.ptr
        UpdateIdx()
    End Sub

    Public Function GetRect(ByVal gfx As LevelGFX) As Rectangle
        Return Shrd.RealSize(New Rectangle(New Point(Me.x, Me.y), gfx.VictimImages(index).Size))
    End Function

    Public Sub UpdateIdx()
        Me.index = Array.IndexOf(ZAMNEditor.Ptr.SpritePtrs, ptr)
        If Me.index = -1 Then Me.index = 0
    End Sub

    Public Sub UpdatePtr()
        If index > 0 Then
            ptr = ZAMNEditor.Ptr.SpritePtrs(index)
        End If
    End Sub
End Class
