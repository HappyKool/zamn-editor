Public Class TitlePageEditor

    Public ed As Editor
    Public curWord As Word
    Public curCtrl As TitlePageEdCtrl

    Public Overloads Function ShowDialog(ByVal ed As Editor) As DialogResult
        Me.ed = ed
        TitlePageEdCtrl1.SetPage(ed.EdControl.lvl.page1)
        TitlePageEdCtrl2.SetPage(ed.EdControl.lvl.page2)
        Return Me.ShowDialog
    End Function

    Private Sub TitlePageEdCtrl1_ItemSelected(ByVal sender As Object, ByVal w As Word) Handles TitlePageEdCtrl1.ItemSelected
        curWord = w
        curCtrl = TitlePageEdCtrl1
        TitlePageEdCtrl2.Deselect()
        UpdateWord()
    End Sub

    Private Sub TitlePageEdCtrl2_ItemSelected(ByVal sender As Object, ByVal w As Word) Handles TitlePageEdCtrl2.ItemSelected
        curWord = w
        curCtrl = TitlePageEdCtrl2
        TitlePageEdCtrl1.Deselect()
        UpdateWord()
    End Sub

    Private Sub UpdateWord()
        txtWord.Text = curWord.ToString
        nudX.Value = curWord.x
        nudY.Value = curWord.y
        nudFont.Value = curWord.font
        Panel1.Enabled = True
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        curCtrl.Delete()
        Panel1.Enabled = False
    End Sub

    Private Sub btnAdd1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd1.Click
        TitlePageEdCtrl1.Add()
    End Sub

    Private Sub btnAdd2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd2.Click
        TitlePageEdCtrl2.Add()
    End Sub

    Private validChars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890 !"
    Private pText As String = ""

    Private Sub txtWord_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim pos As Integer = txtWord.SelectionStart
        For l As Integer = 0 To txtWord.Text.Length - 1
            If Not validChars.Contains(txtWord.Text(l)) Then
                txtWord.Text = pText
                txtWord.SelectionStart = pos
                Return
            End If
        Next
        pText = txtWord.Text
    End Sub

    Private Sub nudFont_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles nudFont.ValueChanged
        nudFont.Value = (nudFont.Value \ 2) * 2
    End Sub

    Public TextTable As String() = {"NT", "TH", "TE", "STE", "IT", "ET", "STI", "STR", "RTI", "RTH", "TO", "NTS", "RTY", "T", "ITA", "NTA", "PTS", _
                                    "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "RO", "Q", _
                                    "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "O", _
                                    "P", "R", "S", "U", "EI", "LI", "X", "Y", "Z", "RE", "RS", "HTM", _
                                    "I", "!", "FE", "FO", "LE", "W", _
                                    "VE", "V", " ", "VEL", "RR", "VEI"}

    Public ByteTable As Integer() = {&H21, &H22, &H23, &H24, &H25, &H26, &H27, &H7127, &H28, &H7228, &H29, &H772A, &H2B6A, &H2D, &H2E, &H2E7B, &H772F, _
                                     &H30, &H31, &H32, &H33, &H34, &H35, &H36, &H37, &H38, &H39, &H3A6A, &H3B, _
                                     &H41, &H42, &H43, &H44, &H45, &H46, &H47, &H48, &H4A, &H4B, &H4C, &H4D, &H4E, &H4F, _
                                     &H50, &H51, &H53, &H55, &H56, &H57, &H58, &H59, &H5A, &H5B6A, &H5C6A, &H5D7473, _
                                     &H64, &H67, &H6968, &H7A68, &H6D6C, &H6F6E, _
                                     &H7675, &H75, &H7D, &H647675, &H5251, &H647675}

    Public TextTableR As String() = {"E", "I", "O", "R", "S"}

    Public ByteTableR As Integer() = {&H456263, &H6470, &H4F65, &H5152, &H5366}

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        curWord.x = nudX.Value
        curWord.y = nudY.Value
        curWord.font = nudFont.Value
        Dim chars As New List(Of Byte)
        Dim pos As Integer = 1
        Dim str As String = txtWord.Text
        Dim rand As New Random()
        Do Until pos > str.Length
            For l As Integer = Math.Min(3, str.Length - pos + 1) To 1 Step -1
                Dim str2 As String = Mid(str, pos, l)
                If TextTableR.Contains(str2) Then
                    Dim nums As Byte() = Shrd.GetBytes(ByteTableR(Array.IndexOf(TextTableR, str2)))
                    chars.Add(nums(rand.Next(0, nums.Length)))
                    pos += l
                    Exit For
                ElseIf TextTable.Contains(str2) Then
                    chars.AddRange(Shrd.GetBytes(ByteTable(Array.IndexOf(TextTable, str2))))
                    pos += l
                    Exit For
                End If
            Next
        Loop
        curWord.chars = chars
        curCtrl.LoadWords()
    End Sub
End Class