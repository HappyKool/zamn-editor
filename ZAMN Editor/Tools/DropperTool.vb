Public Class DropperTool
    Inherits Tool

    Private pX As Integer = -1
    Private pY As Integer = -1

    Public t As New ToolTip

    Public Sub New(ByVal ed As Editor)
        MyBase.New(ed)
        Me.SidePanel = SideContentType.Tiles
        Me.Status = "Click to select a tile type"
    End Sub

    Public Overrides Sub MouseDown(ByVal e As MouseEventArgs)
        Me.MouseMove(e)

        Dim nx As Integer = e.X \ 64
        Dim ny As Integer = e.Y \ 64

        'With ed.EdControl.lvl
        '    MsgBox(Hex(.tileset.TileTiles(.Tiles(nx, ny))(nx \ 8, nx \ 9)))
        'End With
    End Sub

    Public Overrides Sub MouseMove(ByVal e As MouseEventArgs)
        Dim nx As Integer = e.X \ 64
        Dim ny As Integer = e.Y \ 64
        If (nx <> pX Or ny <> pY) And e.Button = MouseButtons.Left And _
        nx < ed.EdControl.lvl.Width And ny < ed.EdControl.lvl.Height And nx >= 0 And ny >= 0 Then
            TilePicker.SelectedIndex = ed.EdControl.lvl.Tiles(nx, ny)
            TilePicker.SelectedTile = TilePicker.SelectedIndex
            TilePicker.ScollToSelected()
            pX = nx
            pY = ny
        End If
    End Sub

    Public Overrides Sub MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        pX = -1
        pY = -1
    End Sub
End Class
