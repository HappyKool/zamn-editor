Public Class BossMonster
    Public x As Integer
    Public y As Integer
    Public ptr As Integer
    Public name As String

    Public Shared names As String() = {"UFO", "Giant Baby", "Desert Snakeoid", "Grass Snakeoid", "Gets Dark", "Unknown", "Giant Spider"}
    Public Shared ptrs As Integer() = {&H1093C, &H11769, &H12ABB, &H12AC3, &H12D95, &H159CF, &H1AF33}
    Public Shared dispfont As New Font("Microsoft Sans Serif", 8.25, FontStyle.Regular, GraphicsUnit.Point)

    Public Sub New()

    End Sub

    Public Sub New(ByVal ptr As Integer, ByVal x As Integer, ByVal y As Integer)
        Me.ptr = ptr
        Me.x = x
        Me.y = y
        UpdateName()
    End Sub

    Public Sub New(ByVal m As BossMonster)
        Me.ptr = m.ptr
        Me.x = m.x
        Me.y = m.y
        Me.name = m.name
    End Sub

    Public Function GetRect() As Rectangle
        Return New Rectangle(New Point(x, y), TextRenderer.MeasureText(name, dispfont))
    End Function

    Public Sub UpdateName()
        Dim indx As Integer = Array.IndexOf(ptrs, ptr)
        If indx > -1 Then
            name = names(indx)
        Else
            name = "Unknown: 0x" & Hex(ptr)
        End If
    End Sub
End Class
