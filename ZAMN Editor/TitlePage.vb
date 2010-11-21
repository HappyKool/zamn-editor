Public Class TitlePage

    Public words As New List(Of Word)

    Public Sub New(ByVal s As IO.Stream)
        Do
            words.Add(New Word(s))
            If words.Last.last Then Return
        Loop
    End Sub

    Public Sub Write(ByVal lst As List(Of Byte), ByVal pageNum As Byte)
        For Each w As Word In words
            lst.AddRange(New Byte() {w.x, w.y, w.font, pageNum})
            For Each c As Integer In w.chars
                lst.Add(c)
            Next
            lst.Add(&HFF)
        Next
        lst(lst.Count - 1) = 0
    End Sub
End Class

Public Class Word
    Public x As Integer
    Public y As Integer
    Public font As Integer
    Public chars As New List(Of Integer)
    Public last As Boolean

    Public Sub New(ByVal s As IO.Stream)
        Me.x = s.ReadByte
        Me.y = s.ReadByte
        Me.font = s.ReadByte
        s.ReadByte() 'skip page number
        Do
            Dim num As Integer = s.ReadByte
            If num = &HFF Or num = 0 Then
                If num = 0 Then last = True
                Return
            End If
            chars.Add(num)
        Loop
    End Sub
End Class