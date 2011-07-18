Imports System.Drawing.Imaging

Public Class TitlePageEdCtrl

    Public tp As TitlePage
    Public wordRects As List(Of Rectangle)
    Public gfx As TitleGFX
    Public selectedIndex As Integer = -1
    Public selectedWord As Word
    Public dragging As Boolean = False
    Public dragXOff As Integer
    Public dragYOff As Integer

    Public Event WordSelected(ByVal sender As Object, ByVal w As Word)

    Public Sub LoadTP(ByVal tp As TitlePage, ByVal gfx As TitleGFX)
        Me.tp = tp
        Me.gfx = gfx
        LoadAllWordRects()
    End Sub

    Public Sub SelectNone()
        selectedIndex = -1
        selectedWord = Nothing
        canvas.Invalidate()
    End Sub

    Public Sub Delete()
        tp.words.Remove(selectedWord)
        wordRects.RemoveAt(selectedIndex)
        SelectNone()
    End Sub

    Public Sub Add(ByVal plt As Integer)
        Dim w As New Word
        w.x = 1
        w.y = 1
        w.font = plt
        w.chars = TitlePageEditor.StrToTitle("NEW", False)
        tp.words.Add(w)
        wordRects.Add(GetWordRect(w))
        selectedWord = w
        selectedIndex = wordRects.Count - 1
        canvas.Invalidate()
        RaiseEvent WordSelected(Me, w)
    End Sub

    Public Sub AlignCenter()
        selectedWord.x = Math.Max(0, (256 - wordRects(selectedIndex).Width) \ 16)
        wordRects(selectedIndex) = GetWordRect(selectedWord)
        canvas.Invalidate()
    End Sub

    Public Sub AlignMiddle()
        selectedWord.y = 11
        wordRects(selectedIndex) = GetWordRect(selectedWord)
        canvas.Invalidate()
    End Sub

    Public Sub UpdateSelection()
        If selectedWord IsNot Nothing Then
            wordRects(selectedIndex) = GetWordRect(selectedWord)
            canvas.Invalidate()
        End If
    End Sub

    Public Sub LoadAllWordRects()
        wordRects = New List(Of Rectangle)
        For l As Integer = 0 To tp.words.Count - 1
            wordRects.Add(GetWordRect(tp.words(l)))
        Next
    End Sub

    Public Function GetWordRect(ByVal w As Word) As Rectangle
        If gfx Is Nothing Then Return Rectangle.Empty
        Dim width As Integer = 0
        For l As Integer = 0 To w.chars.Count - 1
            If w.chars(l) >= &H20 And w.chars(l) <= &H7F Then
                width += gfx.LetterImgs(w.chars(l) - &H20).Width
            End If
        Next
        Return New Rectangle(w.x * 8, w.y * 8, width, 48)
    End Function

    Public Sub DrawWord(ByVal g As Graphics, ByVal w As Word)
        Dim curX As Integer = w.x * 8
        For l As Integer = 0 To w.chars.Count - 1
            Dim idx As Integer = w.chars(l) - &H20
            If idx >= 0 And idx <= &H5F Then
                Shrd.DrawWithPlt(g, curX, w.y * 8, gfx.LetterImgs(idx), gfx.plt, w.font)
                curX += gfx.LetterImgs(idx).Width
            End If
        Next
    End Sub

    Private Sub canvas_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles canvas.Paint
        If tp IsNot Nothing And gfx IsNot Nothing Then
            For l As Integer = 0 To tp.words.Count - 1
                DrawWord(e.Graphics, tp.words(l))
                e.Graphics.DrawRectangle(Pens.Gray, wordRects(l))
            Next
            If selectedIndex > -1 Then
                Dim rect As Rectangle = wordRects(selectedIndex)
                e.Graphics.DrawRectangle(Pens.White, rect.X - 1, rect.Y - 1, rect.Width + 2, rect.Height + 2)
            End If
        End If
    End Sub

    Private Sub canvas_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles canvas.MouseDown
        For l As Integer = wordRects.Count - 1 To 0 Step -1
            If wordRects(l).Contains(e.Location) Then
                selectedIndex = l
                selectedWord = tp.words(selectedIndex)
                dragXOff = e.X - wordRects(l).X
                dragYOff = e.Y - wordRects(l).Y
                dragging = True
                Exit For
            End If
        Next
        If Not dragging Then
            selectedIndex = -1
            selectedWord = Nothing
        End If
        canvas.Invalidate()
        RaiseEvent WordSelected(Me, selectedWord)
    End Sub

    Private Sub canvas_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles canvas.MouseMove
        If dragging Then
            Dim x As Integer = Math.Max(0, Math.Min(256 - wordRects(selectedIndex).Width, e.X - dragXOff)) \ 8
            Dim y As Integer = Math.Max(0, Math.Min(176, e.Y - dragYOff)) \ 8
            If e.X <> selectedWord.x Or e.Y <> selectedWord.y Then
                selectedWord.x = x
                selectedWord.y = y
                wordRects(selectedIndex) = GetWordRect(selectedWord)
                canvas.Invalidate()
            End If
        End If
    End Sub

    Private Sub canvas_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles canvas.MouseUp
        dragging = False
    End Sub
End Class
