Public Class VictimTool
    Inherits ObjectTool(Of Victim)

    Public Sub New(ByVal ed As Editor)
        MyBase.New(ed)
        removable = False
        SidePanel = SideContentType.Victims
        typeName = "victim"
        UpdateCaptions()
    End Sub

    Public Overrides Function CloneT(ByVal o As Victim) As Victim

    End Function

    Public Overrides Function FromText(ByVal txt As String) As System.Collections.Generic.List(Of Victim)

    End Function

    Public Overrides Function GetChangeAction(ByVal objs As System.Collections.Generic.List(Of Victim), ByVal newType As Integer) As Action
        Return New ChangeVictimTypeAction(objs, newType)
    End Function

    Public Overrides Function GetMoveAction(ByVal objs As System.Collections.Generic.List(Of Victim), ByVal dx As Integer, ByVal dy As Integer, ByVal stp As Integer) As Action
        Return New MoveVictimAction(objs, dx, dy, stp)
    End Function

    Public Overrides Function NewOfT(ByVal x As Integer, ByVal y As Integer) As Victim

    End Function

    Public Overrides Function RectOfT(ByVal obj As Victim) As System.Drawing.Rectangle
        Return obj.GetRect(ed.EdControl.lvl.GFX)
    End Function

    Public Overrides Sub RefreshList()
        levelList = ed.EdControl.lvl.victims
    End Sub

    Public Overrides Function ToText(ByVal Objs As System.Collections.Generic.List(Of Victim)) As String

    End Function

    Public Overrides Property X(ByVal obj As Victim) As Integer
        Get
            Return obj.x
        End Get
        Set(ByVal value As Integer)
            obj.x = value
        End Set
    End Property

    Public Overrides Property Y(ByVal obj As Victim) As Integer
        Get
            Return obj.y
        End Get
        Set(ByVal value As Integer)
            obj.y = value
        End Set
    End Property
End Class
