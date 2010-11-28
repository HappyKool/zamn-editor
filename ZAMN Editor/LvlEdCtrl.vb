Public Class LvlEdCtrl
    Public lvl As Level
    Public t As Tool
    Public TilePicker As New TilesetBrowser
    Public ItemPicker As New ItemBrowser
    Public VictimPicker As New VictimBrowser
    Public NRMPicker As New NRMBrowser
    Public MonsterPicker As New MonsterBrowser
    Public BMonsterPicker As New BMonsterBrowser

    Public Grid As Boolean
    Public priority As Boolean
    Public zoom As Single = 1
    Public selection As Selection

    Private fillBrush As SolidBrush
    Private eraserBrush As SolidBrush
    Private borderPen As Pen
    Private selectionGP As Drawing2D.GraphicsPath
    Private eraserRect As Rectangle
    Private forceMove As Boolean = False

    Public Sub New()
        InitializeComponent()
        fillBrush = New SolidBrush(Color.FromArgb(96, 0, 0, 0))
        eraserBrush = New SolidBrush(Color.FromArgb(128, 255, 255, 255))
        borderPen = New Pen(Color.Black)
        borderPen.DashOffset = 0
        borderPen.DashPattern = New Single() {4, 4}
    End Sub

    Public Sub LoadLevel(ByVal lvl As Level)
        Me.lvl = lvl
        selection = New Selection(lvl.Width, lvl.Height)
        TilePicker.LoadTileset(lvl.tileset)
        UpdateScrollBars()
    End Sub

    Public Sub SetSidePanel(ByVal ctrl As Control)
        SideContent.Controls.Clear()
        SideContent.Controls.Add(ctrl)
        ctrl.Dock = DockStyle.Fill
    End Sub

    Public Sub SetStatusText(ByVal txt As String)
        Status.Text = txt
    End Sub

    Public Sub Repaint()
        Me.Invalidate(True)
    End Sub

    Public Sub UpdateSelection()
        selectionGP = selection.ToGP
        eraserRect = selection.GetEraserRect
        Repaint()
    End Sub

    Public Sub SetCursor(ByVal c As Cursor)
        canvas.Cursor = c
    End Sub

    Public Sub FillSelection()
        For m As Integer = 0 To lvl.Height - 1
            For l As Integer = 0 To lvl.Width - 1
                If selection.selectPts(l, m) Then
                    lvl.Tiles(l, m) = TilePicker.SelectedTile
                End If
            Next
        Next
        Repaint()
    End Sub

    Public Sub UpdateScrollBars()
        If lvl Is Nothing Then Return
        HScrl.Maximum = lvl.Width * 64 * zoom
        VScrl.Maximum = lvl.Height * 64 * zoom
        HScrl.LargeChange = canvas.Width
        VScrl.LargeChange = canvas.Height
        HScrl.Value = Math.Min(HScrl.Value, Math.Max(0, HScrl.Maximum - HScrl.LargeChange))
        VScrl.Value = Math.Min(VScrl.Value, Math.Max(0, VScrl.Maximum - VScrl.LargeChange))
        HScrl.Enabled = (HScrl.Maximum > HScrl.LargeChange)
        VScrl.Enabled = (VScrl.Maximum > VScrl.LargeChange)
        canvas.Invalidate()
    End Sub

    Private Sub canvas_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles canvas.Paint
        If lvl Is Nothing Then Return
        e.Graphics.TranslateTransform(-HScrl.Value, -VScrl.Value)
        e.Graphics.ScaleTransform(zoom, zoom)
        For l As Integer = HScrl.Value \ (64 * zoom) To Math.Min(lvl.Width - 1, (HScrl.Value + canvas.Width) \ (64 * zoom) + 1)
            For m As Integer = VScrl.Value \ (64 * zoom) To Math.Min(lvl.Height - 1, (VScrl.Value + canvas.Height) \ (64 * zoom) + 1)
                e.Graphics.DrawImage(lvl.tileset.images(lvl.Tiles(l, m)), l * 64, m * 64)
            Next
        Next
        For Each v As Victim In lvl.victims
            Dim img As Bitmap = LevelGFX.VictimImages(v.index)
            e.Graphics.DrawImage(img, v.x, v.y)
            e.Graphics.DrawString(v.num.ToString, Me.Font, Brushes.Black, v.x + img.Width \ 2 - 3, v.y + img.Height + 5)
            e.Graphics.DrawString(v.num.ToString, Me.Font, Brushes.White, v.x + img.Width \ 2 - 4, v.y + img.Height + 4)
        Next
        For Each m As NRMonster In lvl.NRMonsters
            e.Graphics.DrawImage(LevelGFX.VictimImages(m.index), m.x, m.y)
            If m.index = 0 Then
                e.Graphics.DrawRectangle(Pens.Yellow, m.GetRect)
            End If
        Next
        For Each m As Monster In lvl.Monsters
            e.Graphics.DrawImage(LevelGFX.VictimImages(m.index), m.x, m.y)
            If m.index = 0 Then
                e.Graphics.DrawRectangle(Pens.Blue, m.GetRect)
            End If
        Next
        For Each m As BossMonster In lvl.bossMonsters
            Dim rect As Rectangle = m.GetRect
            e.Graphics.FillRectangle(Brushes.White, rect)
            e.Graphics.DrawRectangle(Pens.Black, rect)
            e.Graphics.DrawString(m.name, BossMonster.dispfont, Brushes.Black, rect.Location)
        Next
        For Each i As Item In lvl.items
            If i.type < LevelGFX.ItemImages.Count Then
                e.Graphics.DrawImage(LevelGFX.ItemImages(i.type), i.x, i.y)
            Else
                e.Graphics.DrawImage(My.Resources.UnknownItem, i.x, i.y)
            End If
        Next
        If priority Then
            For l As Integer = HScrl.Value \ (64 * zoom) To Math.Min(lvl.Width - 1, (HScrl.Value + canvas.Width) \ (64 * zoom) + 1)
                For m As Integer = VScrl.Value \ (64 * zoom) To Math.Min(lvl.Height - 1, (VScrl.Value + canvas.Height) \ (64 * zoom) + 1)
                    e.Graphics.DrawImage(lvl.tileset.priorityImages(lvl.Tiles(l, m)), l * 64, m * 64)
                Next
            Next
        End If
        If Grid Then
            For l As Integer = HScrl.Value \ (64 * zoom) To (HScrl.Value + HScrl.LargeChange) \ (64 * zoom)
                e.Graphics.DrawLine(Pens.White, l * 64, VScrl.Value, l * 64, (VScrl.Value + VScrl.LargeChange) / zoom)
            Next
            For l As Integer = VScrl.Value \ (64 * zoom) To (VScrl.Value + VScrl.LargeChange) \ (64 * zoom)
                e.Graphics.DrawLine(Pens.White, HScrl.Value, l * 64, (HScrl.Value + HScrl.LargeChange) / zoom, l * 64)
            Next
        End If
        If selectionGP IsNot Nothing And selection.exists Then
            e.Graphics.FillPath(fillBrush, selectionGP)
            e.Graphics.DrawPath(Pens.White, selectionGP)
            e.Graphics.DrawPath(borderPen, selectionGP)
        End If
        If eraserRect <> Rectangle.Empty Then
            e.Graphics.FillRectangle(eraserBrush, eraserRect)
            e.Graphics.DrawRectangle(Pens.White, eraserRect)
            e.Graphics.DrawRectangle(borderPen, eraserRect)
        End If
        If t IsNot Nothing Then
            t.Paint(e.Graphics)
        End If
    End Sub

    Private Sub LvlEdCtrl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        canvas.Focus()
    End Sub

    Private Sub LvlEdCtrl_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged, Me.DockChanged
        If lvl Is Nothing Then Return
        UpdateScrollBars()
    End Sub

    Private Sub Scrolled(ByVal sender As Object, ByVal e As System.EventArgs) Handles HScrl.ValueChanged, VScrl.ValueChanged
        canvas.Invalidate()
    End Sub

    Private Sub LvlEdCtrl_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        If VScrl.Enabled Then
            VScrl.Value = Math.Max(0, Math.Min(VScrl.Maximum - VScrl.LargeChange + 1, VScrl.Value - 64 * Math.Sign(e.Delta)))
        ElseIf HScrl.Enabled Then
            HScrl.Value = Math.Max(0, Math.Min(HScrl.Maximum - HScrl.LargeChange + 1, HScrl.Value - 64 * Math.Sign(e.Delta)))
        End If
        DoMouseMove()
    End Sub

    Private Sub canvas_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles canvas.MouseDown
        If t Is Nothing Then Return
        canvas.Focus()
        t.MouseDown(CreateMouseEventArgs(e))
        DragTimer.Start()
    End Sub

    Private Sub canvas_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles canvas.MouseUp
        If t Is Nothing Then Return
        t.MouseUp(CreateMouseEventArgs(e))
        DragTimer.Stop()
    End Sub

    Private Sub canvas_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles canvas.MouseMove
        If t Is Nothing Then Return
        t.MouseMove(CreateMouseEventArgs(e))
        If DragTimer.Enabled And Not forceMove Then
            DragTimer_Tick(Nothing, Nothing)
        End If
        forceMove = False
    End Sub

    Private Sub canvas_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles canvas.KeyDown
        If t Is Nothing Then Return
        t.KeyDown(e)
    End Sub

    Private Sub canvas_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles canvas.KeyUp
        If t Is Nothing Then Return
        t.KeyUp(e)
    End Sub

    Private Function CreateMouseEventArgs(ByVal e As MouseEventArgs)
        Return New MouseEventArgs(e.Button, e.Clicks, (e.X + HScrl.Value) / zoom, (e.Y + VScrl.Value) / zoom, e.Delta)
    End Function

    Public Sub DoMouseMove()
        Dim pt As Point = canvas.PointToClient(MousePosition)
        forceMove = True
        canvas_MouseMove(Me, New MouseEventArgs(Control.MouseButtons, 0, pt.X, pt.Y, 0))
    End Sub

    Private Sub borderTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BorderTimer.Tick
        If selection.exists Then
            borderPen.DashOffset += 1
            If borderPen.DashOffset = 8 Then
                borderPen.DashOffset = 0
            End If
            canvas.Invalidate()
        End If
    End Sub

    Private Sub DragTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DragTimer.Tick
        Dim pt As Point = canvas.PointToClient(MousePosition)
        If pt.X > canvas.Width Then
            HScrl.Value = Math.Min(HScrl.Maximum - HScrl.LargeChange + 1, HScrl.Value + (pt.X - canvas.Width) \ 2)
            DoMouseMove()
        End If
        If pt.Y > canvas.Height Then
            VScrl.Value = Math.Min(VScrl.Maximum - VScrl.LargeChange + 1, VScrl.Value + (pt.Y - canvas.Height) \ 2)
            DoMouseMove()
        End If
        If pt.X < 0 Then
            HScrl.Value = Math.Max(0, HScrl.Value + pt.X \ 2)
            DoMouseMove()
        End If
        If pt.Y < 0 Then
            VScrl.Value = Math.Max(0, VScrl.Value + pt.Y \ 2)
            DoMouseMove()
        End If
    End Sub
End Class
