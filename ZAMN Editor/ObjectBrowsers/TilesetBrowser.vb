Public Class TilesetBrowser
    Inherits ObjectBrowser

    Public tiles As List(Of Byte)
    Public selectedTile As Integer

    Public Sub New(ByVal ts As Tileset)
        MyBase.New(ts)
    End Sub

    Public Overrides Sub Init()
        SetAll()
        selectedTile = -1
        SelectedIndex = -1
    End Sub

    Public Overrides Function ItemWidth(ByVal index As Integer) As Integer
        Return 64
    End Function

    Public Overrides Function ItemHeight(ByVal index As Integer) As Integer
        Return 64
    End Function

    Public Overrides ReadOnly Property hasProperties As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides Sub PaintObject(ByVal g As System.Drawing.Graphics, ByVal x As Integer, ByVal y As Integer, ByVal num As Integer)
        g.DrawImage(ts.images(tiles(num)), x, y)
        'g.DrawString(tiles(num).ToString(), Me.Font, Brushes.Black, x + 64, y + 8)
    End Sub

    Public Sub LoadTileset(ByVal ts As Tileset)
        Me.ts = ts
        SetAll()
        UpdateScrollBar()
    End Sub

    Private Sub TilesetBrowser_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ValueChanged
        If SelectedIndex = -1 Then
            selectedTile = -1
            Return
        End If
        selectedTile = tiles(SelectedIndex)
    End Sub

    Public Sub SetTiles(ByVal tiles As List(Of Byte))
        tiles.Sort()
        Me.tiles = tiles
        itemCt = tiles.Count - 1
        UpdateScrollBar()
        ResetScrollBar()
    End Sub

    Public Sub SetAll()
        If itemCt = 255 Then Return
        SelectedIndex = selectedTile
        If tiles Is Nothing Then
            tiles = New List(Of Byte)
        End If
        tiles.Clear()
        tiles.Capacity = 256
        For l As Integer = 0 To 255
            tiles.Add(l)
        Next
        itemCt = 255
        UpdateScrollBar()
        ScollToSelected()
    End Sub
End Class
