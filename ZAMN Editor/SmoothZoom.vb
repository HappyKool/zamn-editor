Public Class SmoothZoom

    Public zoomEnd As Single
    Public zoomDelta As Single
    Public HScrollEnd As Integer
    Public HScrollDelta As Integer
    Public HScrollValue As Single
    Public VScrollEnd As Integer
    Public VScrollDelta As Integer
    Public VScrollValue As Single

    Public curBounds As RectangleF
    Public newBounds As RectangleF

    Public Sub New(ByVal curZoom As Single, ByVal newZoom As Single, ByVal HScroll As Integer, ByVal VScroll As Integer, ByVal width As Integer, ByVal height As Integer, ByVal totalHeight As Integer, ByVal totalWidth As Integer)
        zoomEnd = newZoom
        curBounds = New RectangleF(HScroll / curZoom, VScroll / curZoom, width / curZoom, height / curZoom)
        Dim newWidth As Single = curBounds.Width * (curZoom / newZoom)
        Dim newHeight As Single = curBounds.Height * (curZoom / newZoom)
        Dim xDelta As Single = curBounds.Width * (curZoom / newZoom) - curBounds.Width
        Dim yDelta As Single = curBounds.Height * (curZoom / newZoom) - curBounds.Height
        newBounds = New RectangleF(curBounds.X - xDelta / 2, curBounds.Y - yDelta / 2, curBounds.Width + xDelta, curBounds.Height + yDelta)
        If newBounds.Right > totalWidth Then
            newBounds.X += totalWidth - newBounds.Right
        End If
        If newBounds.Bottom > totalHeight Then
            newBounds.Y += totalHeight - newBounds.Bottom
        End If
        If newBounds.X < 0 Then
            newBounds.X = 0
        End If
        If newBounds.Y < 0 Then
            newBounds.Y = 0
        End If
        'Transition the zoom over 10 frames
        zoomDelta = (newZoom - curZoom) / 10
        HScrollDelta = CInt((newBounds.X - curBounds.X) / 10)
        VScrollDelta = CInt((newBounds.Y - curBounds.Y) / 10)
        HScrollEnd = CInt(newBounds.X)
        VScrollEnd = CInt(newBounds.Y)
        HScrollValue = HScroll
        VScrollValue = VScroll
    End Sub

    Public Function IsDone(ByVal curZoom As Single) As Boolean
        Return (curZoom >= zoomEnd And zoomDelta > 0) Or (curZoom <= zoomEnd And zoomDelta < 0)
    End Function
End Class
