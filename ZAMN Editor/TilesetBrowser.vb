Public Class TilesetBrowser

    Private ts As Tileset
    Private bgBrush As Drawing2D.LinearGradientBrush
    Private borderPen As Pen
    Private selectedBrush As SolidBrush
    Private maxIndex As Integer = 255
    Public Const ItemHeight As Integer = 80
    Public SelectedIndex As Integer = -1
    Public SelectedTile As Integer = -1
    Public tiles As List(Of Byte)
    Public Event ValueChanged(ByVal sender As Object, ByVal e As EventArgs)

    Public Sub LoadTileset(ByVal ts As Tileset)
        Me.ts = ts
        bgBrush = New Drawing2D.LinearGradientBrush(New Rectangle(Point.Empty, New Size(Me.Width - 17, Me.Height)), Color.White, Color.FromArgb(228, 225, 208), Drawing2D.LinearGradientMode.Horizontal)
        borderPen = New Pen(Color.FromArgb(49, 106, 197))
        selectedBrush = New SolidBrush(Color.FromArgb(225, 230, 232))
        SetAll()
        UpdateScrollBar()
    End Sub

    Private Sub UpdateScrollBar()
        VScrl.Maximum = (maxIndex + 1) * ItemHeight
        VScrl.LargeChange = Math.Max(1, Me.Height)
        VScrl.SmallChange = ItemHeight
        VScrl.Value = Math.Min(VScrl.Value, Math.Max(0, VScrl.Maximum - VScrl.LargeChange))
        VScrl.Enabled = (VScrl.Maximum > VScrl.LargeChange)
        Me.Invalidate()
    End Sub

    Private Sub TilesetBrowser_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If ts Is Nothing Then Return
        e.Graphics.FillRectangle(bgBrush, Me.DisplayRectangle)
        Dim yPos As Integer = -(VScrl.Value Mod ItemHeight)
        For l As Integer = VScrl.Value \ ItemHeight To Math.Min(maxIndex, (VScrl.Value + VScrl.LargeChange) \ ItemHeight)
            e.Graphics.DrawLine(Pens.Black, 0, yPos + 80, Me.Width, yPos + 80)
            If l = SelectedIndex Then
                e.Graphics.FillRectangle(selectedBrush, 0, yPos + 1, Me.Width - 18, ItemHeight)
                e.Graphics.DrawRectangle(borderPen, 0, yPos, Me.Width - 18, ItemHeight)
            End If
            e.Graphics.DrawImage(ts.images(tiles(l)), 8, yPos + 8)
            e.Graphics.DrawString(tiles(l).ToString(), Me.Font, Brushes.Black, 80, yPos + 10)
            yPos += ItemHeight
        Next
    End Sub

    Private Sub VScrl_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles VScrl.ValueChanged
        Me.Invalidate()
    End Sub

    Private Sub TilesetBrowser_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged, Me.DockChanged
        UpdateScrollBar()
    End Sub

    Private Sub TilesetBrowser_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        Dim v As Integer = (e.Y + VScrl.Value) \ ItemHeight
        If v <= maxIndex Then
            SelectedIndex = v
            SelectedTile = tiles(v)
            RaiseEvent ValueChanged(Me, EventArgs.Empty)
        End If
        Me.Invalidate()
    End Sub

    Public Sub ScollToSelected()
        VScrl.Value = Math.Max(0, Math.Min(VScrl.Maximum - VScrl.LargeChange + 1, SelectedIndex * ItemHeight - (VScrl.LargeChange - ItemHeight) \ 2))
        Me.Invalidate()
    End Sub

    Public Sub SetTiles(ByVal tiles As List(Of Byte))
        tiles.Sort()
        Me.tiles = tiles
        maxIndex = tiles.Count - 1
        UpdateScrollBar()
    End Sub

    Public Sub SetAll()
        If tiles Is Nothing Then
            tiles = New List(Of Byte)
        End If
        tiles.Clear()
        tiles.Capacity = 256
        For l As Integer = 0 To 255
            tiles.Add(l)
        Next
        maxIndex = 255
        UpdateScrollBar()
    End Sub
End Class
