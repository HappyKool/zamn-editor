Public Class PaintTileAction
    Inherits Action

    Public points As New List(Of Point)
    Public pType As New List(Of Integer)
    Public tileType As Integer

    Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal tileType As Integer)
        Me.points.Add(New Point(x, y))
        Me.tileType = tileType
    End Sub

    Public Overrides Sub AfterSetEdControl()
        pType.Add(level.Tiles(points(0).X, points(0).Y))
    End Sub

    Public Overrides Sub Undo()
        For l As Integer = 0 To points.Count - 1
            level.Tiles(points(l).X, points(l).Y) = pType(l)
        Next
    End Sub

    Public Overrides Sub Redo()
        For l As Integer = 0 To points.Count - 1
            level.Tiles(points(l).X, points(l).Y) = tileType
        Next
    End Sub

    Public Overrides ReadOnly Property CanMerge As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Sub Merge(ByVal act As Action)
        Dim act2 As PaintTileAction = DirectCast(act, PaintTileAction)
        If Not (points.Contains(act2.points(0))) Then
            points.Add(act2.points(0))
            pType.Add(act2.pType(0))
        End If
    End Sub

    Public Overrides Function ToString() As String
        Return "Paint tiles"
    End Function
End Class

Public Class TileSuggestAction
    Inherits Action

    Public x As Integer
    Public y As Integer
    Public pType As Integer
    Public tileType As Integer

    Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal tileType As Integer)
        Me.x = x
        Me.y = y
        Me.tileType = tileType
    End Sub

    Public Overrides Sub AfterSetEdControl()
        pType = level.Tiles(x, y)
    End Sub

    Public Overrides Sub Undo()
        level.Tiles(x, y) = pType
    End Sub

    Public Overrides Sub Redo()
        level.Tiles(x, y) = tileType
    End Sub

    Public Overrides ReadOnly Property CanMerge As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Sub Merge(ByVal act As Action)
        Me.tileType = DirectCast(act, TileSuggestAction).tileType
    End Sub

    Public Overrides Function ToString() As String
        Return "Change tile"
    End Function
End Class