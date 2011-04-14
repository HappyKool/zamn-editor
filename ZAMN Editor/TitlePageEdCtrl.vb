Public Class TitlePageEdCtrl

    Public tp As TitlePage
    Public curWord As Word

    Public Event ItemSelected(ByVal sender As Object, ByVal w As Word)

    Public Sub SetPage(ByVal tp As TitlePage)
        Me.tp = tp
        LoadWords()
    End Sub

    Public Sub LoadWords()
        lstWords.Items.Clear()
        For Each w As Word In tp.words
            lstWords.Items.Add(w.ToString)
        Next
    End Sub

    Private Sub lstWords_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstWords.SelectedIndexChanged
        If lstWords.SelectedIndex > -1 Then
            curWord = tp.words(lstWords.SelectedIndex)
            RaiseEvent ItemSelected(Me, curWord)
        End If
    End Sub

    Public Sub Deselect()
        lstWords.SelectedIndex = -1
    End Sub

    Public Sub Delete()
        tp.words.Remove(curWord)
        lstWords.Items.RemoveAt(lstWords.SelectedIndex)
    End Sub

    Public Sub Add()
        curWord = New Word()
        tp.words.Add(curWord)
        curWord.chars.AddRange({&H4E, &H45, &H6E, &H6F})
        lstWords.Items.Add(curWord.ToString)
        lstWords.SelectedIndex = lstWords.Items.Count - 1
    End Sub
End Class
