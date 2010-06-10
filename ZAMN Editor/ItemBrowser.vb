﻿Public Class ItemBrowser

    Private bgBrush As Drawing2D.LinearGradientBrush
    Private borderPen As Pen
    Private selectedBrush As SolidBrush
    Public SelectedIndex As Integer = -1
    Public itemCt As Integer
    Public Event ValueChanged(ByVal sender As Object, ByVal e As EventArgs)

    Public Sub New()
        InitializeComponent()
        bgBrush = New Drawing2D.LinearGradientBrush(New Rectangle(Point.Empty, New Size(Me.Width - 17, Me.Height)), Color.White, Color.FromArgb(228, 225, 208), Drawing2D.LinearGradientMode.Horizontal)
        borderPen = New Pen(Color.FromArgb(49, 106, 197))
        selectedBrush = New SolidBrush(Color.FromArgb(225, 230, 232))
        itemCt = LevelGFX.ItemImages.Count - 1
        UpdateScrollBar()
    End Sub

    Private Sub UpdateScrollBar()
        VScrl.Maximum = itemCt
        VScrl.LargeChange = Math.Max(1, Me.Height \ 32)
        VScrl.Value = Math.Min(VScrl.Value, Math.Max(0, VScrl.Maximum - VScrl.LargeChange))
        VScrl.Enabled = (VScrl.Maximum > VScrl.LargeChange)
        Me.Invalidate()
    End Sub

    Private Sub ItemBrowser_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        e.Graphics.FillRectangle(bgBrush, Me.DisplayRectangle)
        Dim yPos As Integer = 0
        For l As Integer = VScrl.Value To Math.Min(itemCt, VScrl.Value + VScrl.LargeChange)
            e.Graphics.DrawLine(Pens.Black, 0, yPos + 32, Me.Width, yPos + 32)
            If l = SelectedIndex Then
                e.Graphics.FillRectangle(selectedBrush, 0, yPos + 1, Me.Width - 18, 32)
                e.Graphics.DrawRectangle(borderPen, 0, yPos, Me.Width - 18, 32)
            End If
            e.Graphics.DrawImage(LevelGFX.ItemImages(l), 8, yPos + 8)
            e.Graphics.DrawString(l.ToString(), Me.Font, Brushes.Black, 32, yPos + 10)
            yPos += 32
        Next
    End Sub

    Private Sub VScrl_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles VScrl.ValueChanged
        Me.Invalidate()
    End Sub

    Private Sub TilesetBrowser_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged, Me.DockChanged
        UpdateScrollBar()
    End Sub

    Private Sub ItemBrowser_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        Dim v As Integer = e.Y \ 32 + VScrl.Value
        If v <= itemCt Then
            SelectedIndex = v
            RaiseEvent ValueChanged(Me, EventArgs.Empty)
        End If
        Me.Invalidate()
    End Sub

    Public Sub ScollToSelected()
        VScrl.Value = Math.Max(0, Math.Min(VScrl.Maximum - VScrl.LargeChange + 1, SelectedIndex - VScrl.LargeChange \ 2))
        Me.Invalidate()
    End Sub
End Class
