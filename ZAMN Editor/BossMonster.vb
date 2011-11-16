Public Class BossMonster
    Public x As UShort
    Public y As UShort
    Public ptr As Integer
    Public name As String

    Public exData As Byte()

    Public Shared dispfont As New Font("Microsoft Sans Serif", 8.25, FontStyle.Regular, GraphicsUnit.Point)

    Public Sub New()

    End Sub

    Public Sub New(ByVal ptr As Integer, ByVal x As UShort, ByVal y As UShort)
        Me.ptr = ptr
        Me.x = x
        Me.y = y
        UpdateName()
    End Sub

    Public Sub New(ByVal ptr As Integer, ByVal s As IO.Stream, ByVal dataLen As Integer)
        Me.ptr = ptr
        ReDim exData(dataLen - 1)
        For l As Integer = 0 To dataLen - 1
            exData(l) = s.ReadByte
        Next
    End Sub

    Public Sub New(ByVal ptr As Integer, ByVal exData As Byte())
        Me.ptr = ptr
        Me.exData = exData
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
        Dim indx As Integer = Array.IndexOf(ZAMNEditor.Ptr.BossMonsters, ptr)
        If indx > -1 Then
            name = ZAMNEditor.Ptr.BossMonsterNames(indx)
        Else
            name = "Unknown: 0x" & Hex(ptr)
        End If
    End Sub

    Public Function GetBGPalette() As Integer
        If exData IsNot Nothing AndAlso exData.Length >= 4 Then
            Return Shrd.GetFileAddr(exData, 0)
        Else
            Return -1
        End If
    End Function

    Public Function GetSpritePalette() As Integer
        If exData IsNot Nothing AndAlso exData.Length >= 8 Then
            Return Shrd.GetFileAddr(exData, 4)
        Else
            Return -1
        End If
    End Function
End Class
