Public Class OpenLevel

    Public levelNums As Integer()
    Public levelNames As String()
    Private r As ROM

    Public Sub LoadROM(ByVal r As ROM)
        Me.r = r
        levels.Items.Clear()
        'For l As Integer = 0 To r.regLvlCount - 2
        '    levels.Items.Add("Level " & l.ToString())
        'Next
        'levels.Items.Add("Credit Level")
        'For l As Integer = 0 To r.bonusLvls.Count - 1
        '    levels.Items.Add("Bonus Level " & l.ToString())
        'Next
        levels.Items.AddRange(r.names.Values.ToArray)
    End Sub

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        If levels.SelectedIndices.Count = 0 Then
            MsgBox("Select a level", MsgBoxStyle.Information, "Select a level")
            Return
        End If
        ReDim levelNums(levels.SelectedIndices.Count - 1)
        ReDim levelNames(levels.SelectedIndices.Count - 1)
        For l As Integer = 0 To levelNames.Length - 1
            levelNums(l) = r.names.Keys(levels.SelectedIndices(l))
            levelNames(l) = levels.SelectedItems(l).ToString
        Next
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Public Sub SetName(ByVal index As Integer, ByVal name As String)
        levels.Items(index) = name
    End Sub
End Class