Public Class PasteTilesTool
    Inherits Tool

    Public NewTiles As Integer(,)
    Public width As Integer
    Public height As Integer
    Public xPos As Integer
    Public yPos As Integer
    Public xPosT As Integer
    Public yPosT As Integer
    Public pasting As Boolean = False
    Public moving As Boolean = False
    Public DragXOff As Integer
    Public DragYOff As Integer
    Public selection As Selection
    Public gp As Drawing2D.GraphicsPath

    Public Sub New(ByVal ed As Editor)
        MyBase.New(ed)
    End Sub

    Public Event DonePasting(ByVal sender As Object, ByVal e As EventArgs)

    Public Overrides Function Paste() As Boolean
        Dim txt As String = Clipboard.GetText
        If txt.StartsWith("A") Then
            ed.SelectAll(False)
            width = CInt("&H" & Mid(txt, 2, 2))
            height = CInt("&H" & Mid(txt, 4, 2))
            ReDim NewTiles(width - 1, height - 1)
            selection = New Selection(width, height)
            For l As Integer = 0 To txt.Length - 6 Step 2
                Dim l2 As Integer = l \ 2
                If txt(l + 6) = "-" Then
                    NewTiles(l2 Mod width, l2 \ width) = -1
                Else
                    NewTiles(l2 Mod width, l2 \ width) = CInt("&H" & Mid(txt, l + 6, 2))
                    selection.selectPts(l2 Mod width, l2 \ width) = True
                End If
            Next
            selection.Refresh()
            gp = selection.ToGP
            pasting = True
            xPosT = Math.Ceiling(ed.EdControl.HScrl.Value / 64)
            yPosT = Math.Ceiling(ed.EdControl.VScrl.Value / 64)
            xPos = xPosT * 64
            yPos = yPosT * 64
            MoveGP(gp, xPos, yPos)
            Repaint()
        Else
            RaiseEvent DonePasting(Me, EventArgs.Empty)
        End If
    End Function

    Public Sub MoveGP(ByVal gp As Drawing2D.GraphicsPath, ByVal dX As Integer, ByVal dY As Integer)
        If dX = 0 And dY = 0 Then Return
        Dim matrix As New Drawing2D.Matrix
        matrix.Translate(dX, dY)
        gp.Transform(matrix)
        matrix.Dispose()
    End Sub

    Public Overrides Sub Paint(ByVal g As System.Drawing.Graphics)
        For y As Integer = 0 To height - 1
            For x As Integer = 0 To width - 1
                If NewTiles(x, y) > -1 Then
                    g.DrawImage(ed.EdControl.lvl.tileset.images(NewTiles(x, y)), xPos + x * 64, yPos + y * 64)
                End If
            Next
        Next
        g.FillPath(ed.EdControl.fillBrush, gp)
        g.DrawPath(Pens.White, gp)
        g.DrawPath(ed.EdControl.borderPen, gp)
    End Sub

    Public Overrides Sub MouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        If moving Then
            Dim px As Integer = xPos, py As Integer = yPos
            xPos = Math.Max(0, e.X - DragXOff)
            yPos = Math.Max(0, e.Y - DragYOff)
            MoveGP(gp, xPos - px, yPos - py)
            Repaint()
        Else
            Dim x As Integer = (e.X - xPos) \ 64
            Dim y As Integer = (e.Y - yPos) \ 64
            If pasting And x > -1 And x < width And y > -1 And y < height Then
                If NewTiles(x, y) > -1 Then
                    SetCursor(Cursors.SizeAll)
                Else
                    SetCursor(Cursors.Arrow)
                End If
            Else
                SetCursor(Cursors.Arrow)
            End If
        End If
    End Sub

    Public Overrides Sub MouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim x As Integer = (e.X - xPos) \ 64
        Dim y As Integer = (e.Y - yPos) \ 64
        If pasting And x > -1 And x < width And y > -1 And y < height Then
            If NewTiles(x, y) > -1 Then
                moving = True
                DragXOff = e.X - xPos
                DragYOff = e.Y - yPos
            Else
                RaiseEvent DonePasting(Me, EventArgs.Empty)
            End If
        Else
            RaiseEvent DonePasting(Me, EventArgs.Empty)
        End If
    End Sub

    Public Overrides Sub MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        moving = False
        Dim px As Integer = xPos, py As Integer = yPos
        If xPos Mod 64 < 32 Then
            xPosT = Math.Floor(xPos / 64)
        Else
            xPosT = Math.Ceiling(xPos / 64)
        End If
        xPos = xPosT * 64
        If yPos Mod 64 < 32 Then
            yPosT = Math.Floor(yPos / 64)
        Else
            yPosT = Math.Ceiling(yPos / 64)
        End If
        yPos = yPosT * 64
        MoveGP(gp, xPos - px, yPos - py)
        Repaint()
    End Sub

    Private Sub PasteTilesTool_DonePasting(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DonePasting
        pasting = False
        For y As Integer = 0 To height - 1
            For x As Integer = 0 To width - 1
                If NewTiles(x, y) > -1 Then
                    ed.EdControl.lvl.Tiles(x + xPosT, y + yPosT) = NewTiles(x, y)
                End If
            Next
        Next
    End Sub
End Class
