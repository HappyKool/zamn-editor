Public Class ViewImage

    Public Overloads Function ShowDialog(ByVal img As Bitmap)
        PictureBox1.Image = img
        Me.ShowDialog()
    End Function
End Class