Public Class Victim
    Public x As Integer
    Public y As Integer
    Public num As Integer
    Public ptr As Integer
    Public index As Integer
    Public unused As Integer

    Public Sub New()

    End Sub

    Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal unused As Integer, ByVal num As Integer, ByVal ptr As Integer)
        Me.x = x
        Me.y = y
        Me.unused = unused
        Me.num = num
        If num = 16 Then
            Me.num = 10
        End If
        Me.ptr = ptr
        UpdateIdx()
    End Sub

    Public Sub New(ByVal v As Victim)
        Me.x = v.x
        Me.y = v.y
        Me.unused = v.unused
        Me.ptr = v.ptr
        Me.num = v.num
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
