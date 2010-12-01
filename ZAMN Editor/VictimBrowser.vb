Public Class VictimBrowser

    Private bgBrush As Drawing2D.LinearGradientBrush
    Private borderPen As Pen
    Private selectedBrush As SolidBrush
    Public SelectedIndex As Integer = 0
    Public gfx As LevelGFX
    Public Event ValueChanged(ByVal sender As Object, ByVal e As EventArgs)

    Public Sub New(ByVal gfx As LevelGFX)
        InitializeComponent()
        Me.gfx = gfx
        bgBrush = New Drawing2D.LinearGradientBrush(New Rectangle(Point.Empty, New Size(Me.Width - 17, Me.Height)), Color.White, Color.FromArgb(228, 225, 208), Drawing2D.LinearGradientMode.Horizontal)
        borderPen = New Pen(Color.FromArgb(49, 106, 197))
        selectedBrush = New SolidBrush(Color.FromArgb(225, 230, 232))
        UpdateScrollBar()
    End Sub

    Private Sub UpdateScrollBar()
        VScrl.Maximum = 10
        VScrl.LargeChange = Math.Max(1, Me.Height \ 88)
        VScrl.Value = Math.Min(VScrl.Value, Math.Max(0, VScrl.Maximum - VScrl.LargeChange))
        VScrl.Enabled = (VScrl.Maximum > VScrl.LargeChange)
        Me.Invalidate()
    End Sub

    Private Sub VictimBrowser_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        e.Graphics.FillRectangle(bgBrush, Me.DisplayRectangle)
        Dim yPos As Integer = 0
        For l As Integer = VScrl.Value + 1 To Math.Min(10, VScrl.Value + VScrl.LargeChange) + 1
            e.Graphics.DrawLine(Pens.Black, 0, yPos + 88, Me.Width, yPos + 88)
            If l = SelectedIndex Then
                e.Graphics.FillRectangle(selectedBrush, 0, yPos + 1, Me.Width - 18, 88)
                e.Graphics.DrawRectangle(borderPen, 0, yPos, Me.Width - 18, 88)
            End If
            e.Graphics.DrawImage(gfx.VictimImages(l), 8, yPos + (88 - gfx.VictimImages(l).Height) \ 2)
            yPos += 88
        Next
    End Sub

    Private Sub VScrl_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles VScrl.ValueChanged
        Me.Invalidate()
    End Sub

    Private Sub VictimBrowser_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged, Me.DockChanged
        UpdateScrollBar()
    End Sub

    Private Sub VictimBrowser_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        Dim v As Integer = e.Y \ 88 + VScrl.Value
        If v <= 11 Then
            SelectedIndex = v + 1
            RaiseEvent ValueChanged(Me, EventArgs.Empty)
        End If
        Me.Invalidate()
    End Sub

    Public Sub ScollToSelected()
        VScrl.Value = Math.Max(0, Math.Min(VScrl.Maximum - VScrl.LargeChange + 1, SelectedIndex - VScrl.LargeChange \ 2))
        Me.Invalidate()
    End Sub
End Class
