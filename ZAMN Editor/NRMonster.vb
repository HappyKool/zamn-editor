Public Class NRMonster
    Public x As Integer
    Public y As Integer
    Public unused1 As UShort
    Public unused2 As UShort
    Public ptr As Integer
    Public index As Integer

    Public Sub New()

    End Sub

    Public Sub New(ByVal x As UShort, ByVal y As UShort, ByVal unused1 As UShort, ByVal unused2 As UShort, ByVal ptr As Integer)
        Me.x = x
        Me.y = y
        Me.unused1 = unused1
        Me.unused2 = unused2
        Me.ptr = ptr
        UpdateIdx()
    End Sub

    Public Sub New(ByVal m As NRMonster)
        Me.x = m.x
        Me.y = m.y
        Me.unused1 = m.unused1
        Me.unused2 = m.unused2
        Me.ptr = m.ptr
        UpdateIdx()
    End Sub

    Public Function GetRect(ByVal gfx As LevelGFX) As Rectangle
        Return Shrd.RealSize(New Rectangle(New Point(Me.x, Me.y), gfx.VictimImages(index).Size))
    End Function

    Public Sub UpdatePtr()
        If index > 0 Then
            ptr = ZAMNEditor.Ptr.SpritePtrs(index)
        End If
    End Sub

    Public Sub UpdateIdx()
        Me.index = Array.IndexOf(ZAMNEditor.Ptr.SpritePtrs, ptr)
        If Me.index = -1 Then Me.index = 0
    End Sub
End Class
