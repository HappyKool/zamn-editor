﻿Public Class SaveStateEditor

    Private Shared ItemGFX As Integer() = {0, 1, 2, 3, 18, 19, 11, 12, 13, 16, 17, 20, 21, 4, 5, 6, 7, 8, 15, 22, 23, 24}
    Private Shared ItemRAM As Integer() = {0, 2, 4, 6, 8, 10, 12, 14, 16, 20, 22, 24, 26, 64, 66, 68, 70, 72, 78, 80, 82, 84}

    Public values As New List(Of NumericUpDown)
    Public lvl As Level
    Public baseState As Byte()
    Public RAMIndex As Integer
    Public fileName As String

    Public Overloads Function ShowDialog(ByVal lvl As Level, ByVal baseState As Byte(), ByVal RAMindex As Integer, ByVal fileName As String) As DialogResult
        Me.lvl = lvl
        Me.baseState = baseState
        Me.RAMIndex = RAMindex
        Me.fileName = fileName
        Dim defaults(ItemGFX.Length - 1) As Integer
        If My.Settings.StateSettings <> "" Then
            Dim stateArr As String() = My.Settings.StateSettings.Split("|")
            For l As Integer = 0 To defaults.Length - 1
                defaults(l) = CInt(stateArr(l))
            Next
        End If
        Dim x As Integer = 12, y As Integer = 12
        values.Clear()
        Me.Controls.Clear()
        For l As Integer = 0 To ItemGFX.Length - 1
            Dim pic As New PictureBox
            pic.Location = New Point(x, y)
            pic.Size = New Size(16, 16)
            pic.Image = lvl.GFX.ItemImages(ItemGFX(l))
            Me.Controls.Add(pic)
            Dim nud As New NumericUpDown
            nud.Location = New Point(x + 30, y - 2)
            nud.Width = 64
            nud.Maximum = If(x = 120, 99, 999)
            nud.Value = defaults(l)
            Me.Controls.Add(nud)
            values.Add(nud)
            y += 26
            If l = 12 Then
                x = 120
                y = 12
            End If
        Next
        Me.Controls.Add(btnOK)
        Me.Controls.Add(btnCancel)
        Me.Controls.Add(btnSave)
        Me.Controls.Add(lblNote)
        Return Me.ShowDialog()
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim fs As New IO.FileStream(fileName, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read)
        fs.Write(baseState, 0, baseState.Length)
        fs.Seek(RAMIndex + Ptr.RAMLevelNum, IO.SeekOrigin.Begin)
        fs.WriteByte(lvl.num)
        For l As Integer = 0 To ItemRAM.Length - 1
            fs.Seek(RAMIndex + Ptr.RAMWeaponQty + ItemRAM(l), IO.SeekOrigin.Begin)
            Dim value As Integer = CInt("&H" & values(l).Value.ToString)
            fs.WriteByte(value Mod &H100)
            fs.WriteByte(value \ &H100)
        Next
        fs.Close()
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim str As String = ""
        For l As Integer = 0 To values.Count - 1
            str &= values(l).Value.ToString() & "|"
        Next
        My.Settings.StateSettings = Mid(str, 1, str.Length - 1)
    End Sub
End Class