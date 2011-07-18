Public Class FontHacker

    Public r As ROM
    Public properties As Byte()
    Public img As Bitmap
    Public img2 As Bitmap
    Public values As Byte() = {&H0, &H1, &H4, &H5, &H8, &H40, &H41, &H45}
    Public brshs As Brush() = {Brushes.Transparent, Brushes.Pink, Brushes.LimeGreen, Brushes.SkyBlue, Brushes.Yellow, Brushes.LightGray, Brushes.Orange, Brushes.Violet}

    Public Overloads Function ShowDialog(ByVal r As ROM) As DialogResult
        Me.r = r
        start.Maximum = Integer.MaxValue
        numOfTiles.Maximum = Integer.MaxValue
        imgWidth.Maximum = Integer.MaxValue
        Return Me.ShowDialog
    End Function

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        img = New Bitmap(CInt(imgWidth.Value * 8), CInt((numOfTiles.Value \ imgWidth.Value + 1) * 8), Imaging.PixelFormat.Format8bppIndexed)
        img2 = New Bitmap(img.Width, img.Height)
        Dim g As Graphics = Graphics.FromImage(img2)
        Dim fs As New IO.FileStream(r.path, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
        fs.Seek(&H1F28C, IO.SeekOrigin.Begin)
        Shrd.FillPalette(img, Shrd.ReadPalette(fs, &H80, False))
        fs.Seek(&H95180, IO.SeekOrigin.Begin)
        Dim GFX As Byte() = Tileset.DecompressMap16(fs)
        Dim LinGFX(511)(,) As Byte
        For l As Integer = 0 To 511
            LinGFX(l) = Shrd.PlanarToLinear(GFX, l * &H20)
        Next
        Dim data As Imaging.BitmapData = img.LockBits(New Rectangle(0, 0, img.Width, img.Height), Imaging.ImageLockMode.ReadOnly, Imaging.PixelFormat.Format8bppIndexed)
        ReDim properties(numOfTiles.Value - 1)
        Dim allProps As New List(Of Byte)
        Dim x As Integer = 0, y As Integer = 0
        fs.Seek(start.Value, IO.SeekOrigin.Begin)
        For l As Integer = 0 To numOfTiles.Value - 1
            Dim t As Byte = fs.ReadByte
            Dim p As Byte = fs.ReadByte
            properties(l) = p
            If Not allProps.Contains(p) Then
                allProps.Add(p)
            End If
            Shrd.DrawTile(data, x, y, p, t, LinGFX)
            'g.FillRectangle(brshs(Array.IndexOf(values, p)), x, y, 8, 8)
            x += 8
            If x = imgWidth.Value * 8 Then
                x = 0
                y += 8
            End If
        Next
        img.UnlockBits(data)
        PictureBox1.Width = img.Width
        PictureBox1.Height = img.Height
        PictureBox1.Image = img
        g.Dispose()
    End Sub

    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        Try
            Dim i As Integer = (e.X \ 8) + (e.Y \ 8) * imgWidth.Value
            MsgBox(Convert.ToString(properties(i), 2) & Environment.NewLine & "X:" & (e.X \ 8) & Environment.NewLine & "Y:" & (e.Y \ 8))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkAltImg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAltImg.CheckedChanged
        If chkAltImg.Checked Then
            'PictureBox1.Image = img2
        Else
            'PictureBox1.Image = img
        End If
        PictureBox1.Invalidate()
    End Sub

    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint
        If chkAltImg.Checked Then
            e.Graphics.DrawImage(img2, 0, 0)
        End If
    End Sub
End Class