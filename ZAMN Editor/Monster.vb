Public Class Monster
    Public x As Integer
    Public y As Integer
    Public radius As Integer
    Public delay As Integer
    Public ptr As Integer
    Public index As Integer

    Public Sub New()

    End Sub

    Public Sub New(ByVal radius As Integer, ByVal x As Integer, ByVal y As Integer, ByVal delay As Integer, ByVal ptr As Integer)
        Me.radius = radius
        Me.x = x
        Me.y = y
        Me.delay = delay
        Me.ptr = ptr
        Me.index = 1 + Array.IndexOf(LevelGFX.ptrs, ptr)
    End Sub

    Public Sub New(ByVal m As Monster)
        Me.radius = m.radius
        Me.x = m.x
        Me.y = m.y
        Me.delay = m.delay
        Me.ptr = m.ptr
        Me.index = m.index
    End Sub

    Public Function GetRect(ByVal gfx As LevelGFX) As Rectangle
        Return Shrd.RealSize(New Rectangle(New Point(Me.x, Me.y), gfx.VictimImages(index).Size))
    End Function

    Public Sub UpdatePtr()
        If index > 0 Then
            ptr = LevelGFX.ptrs(index - 1)
        End If
    End Sub
End Class
