﻿Public Class BMonsterBrowser

    Private bgBrush As Drawing2D.LinearGradientBrush
    Private borderPen As Pen
    Private selectedBrush As SolidBrush
    Public objCount As Integer = 5
    Public Const itemHeight As Integer = 29
    Public SelectedIndex As Integer = -1
    Public Event ValueChanged(ByVal sender As Object, ByVal e As EventArgs)

    Public Sub New()
        InitializeComponent()
        bgBrush = New Drawing2D.LinearGradientBrush(New Rectangle(Point.Empty, New Size(Me.Width - 17, Me.Height)), Color.White, Color.FromArgb(228, 225, 208), Drawing2D.LinearGradientMode.Horizontal)
        borderPen = New Pen(Color.FromArgb(49, 106, 197))
        selectedBrush = New SolidBrush(Color.FromArgb(225, 230, 232))
        UpdateScrollBar()
    End Sub

    Private Sub UpdateScrollBar()
        VScrl.Maximum = (objCount + 1) * itemHeight
        VScrl.LargeChange = Math.Max(1, Me.Height)
        VScrl.Value = Math.Min(VScrl.Value, Math.Max(0, VScrl.Maximum - VScrl.LargeChange))
        VScrl.Enabled = (VScrl.Maximum > VScrl.LargeChange)
        Me.Invalidate()
    End Sub

    Private Sub NRMBrowser_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        e.Graphics.FillRectangle(bgBrush, Me.DisplayRectangle)
        Dim yPos As Integer = -(VScrl.Value Mod itemHeight)
        For l As Integer = VScrl.Value \ itemHeight To Math.Min(objCount, (VScrl.Value + VScrl.LargeChange) \ itemHeight)
            e.Graphics.DrawLine(Pens.Black, 0, yPos + itemHeight, Me.Width, yPos + itemHeight)
            If l = SelectedIndex Then
                e.Graphics.FillRectangle(selectedBrush, 0, yPos + 1, Me.Width - 18, itemHeight)
                e.Graphics.DrawRectangle(borderPen, 0, yPos, Me.Width - 18, itemHeight)
            End If
            e.Graphics.DrawString(BossMonster.names(l), BossMonster.dispfont, Brushes.Black, 8, yPos + 8)
            yPos += itemHeight
        Next
    End Sub

    Private Sub VScrl_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles VScrl.ValueChanged
        Me.Invalidate()
    End Sub

    Private Sub NRMBrowser_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged, Me.DockChanged
        UpdateScrollBar()
    End Sub

    Private Sub NRMBrowser_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        Dim v As Integer = (e.Y + VScrl.Value) \ itemHeight
        If v <= objCount Then
            SelectedIndex = v
            RaiseEvent ValueChanged(Me, EventArgs.Empty)
        End If
        Me.Invalidate()
    End Sub

    Public Sub ScollToSelected()
        VScrl.Value = Math.Max(0, Math.Min(VScrl.Maximum - VScrl.LargeChange + 1, (SelectedIndex * itemHeight) - (VScrl.LargeChange - itemHeight) \ 2))
        Me.Invalidate()
    End Sub
End Class