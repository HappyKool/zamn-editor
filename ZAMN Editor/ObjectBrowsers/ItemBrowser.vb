Public Class ItemBrowser
    Inherits ObjectBrowser

    Public Sub New(ByVal gfx As LevelGFX)
        MyBase.New(gfx)
    End Sub

    Public Overrides Function ItemWidth(ByVal index As Integer) As Integer
        Return 16
    End Function

    Public Overrides Function ItemHeight(ByVal index As Integer) As Integer
        Return 16
    End Function

    Public Overrides Sub Init()
        itemCt = gfx.ItemImages.Count - 1
        Dim ip As New ItemProp
        ip.Dock = DockStyle.Fill
        propCtrl = ip
    End Sub

    Public Overrides Sub PaintObject(ByVal g As System.Drawing.Graphics, ByVal x As Integer, ByVal y As Integer, ByVal num As Integer)
        g.DrawImage(gfx.ItemImages(num), x, y)
    End Sub
End Class