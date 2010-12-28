Public Class MonsterAction
    Inherits Action

    Public Monsters As New List(Of Monster)

    Public Sub New(ByVal monsters As List(Of Monster))
        For Each m As Monster In monsters
            Me.Monsters.Add(m)
        Next
    End Sub
End Class

Public Class AddMonsterAction
    Inherits MonsterAction

    Public Sub New(ByVal monsters As List(Of Monster))
        MyBase.New(monsters)
    End Sub

    Public Overrides Sub Undo()
        For Each m As Monster In Monsters
            level.Monsters.Remove(m)
        Next
    End Sub

    Public Overrides Sub Redo()
        For Each m As Monster In Monsters
            level.Monsters.Add(m)
        Next
    End Sub

    Public Overrides Function ToString() As String
        If Monsters.Count = 1 Then
            Return "Add monster"
        Else
            Return "Add " & Monsters.Count.ToString & " monsters"
        End If
    End Function
End Class

Public Class RemoveMonsterAction
    Inherits MonsterAction

    Public Sub New(ByVal monsters As List(Of Monster))
        MyBase.New(monsters)
    End Sub

    Public Overrides Sub Undo()
        For Each m As Monster In Monsters
            level.Monsters.Add(m)
        Next
    End Sub

    Public Overrides Sub Redo()
        For Each m As Monster In Monsters
            level.Monsters.Remove(m)
        Next
    End Sub

    Public Overrides Function ToString() As String
        If Monsters.Count = 1 Then
            Return "Remove monster"
        Else
            Return "Remove " & Monsters.Count.ToString & " monsters"
        End If
    End Function
End Class

Public Class MoveMonsterAction
    Inherits MonsterAction

    Public px As New List(Of Integer)
    Public py As New List(Of Integer)
    Public nx As New List(Of Integer)
    Public ny As New List(Of Integer)

    Public Sub New(ByVal monsters As List(Of Monster), ByVal dx As Integer, ByVal dy As Integer, ByVal stp As Integer)
        MyBase.New(monsters)
        For Each m As Monster In monsters
            px.Add(m.x)
            py.Add(m.y)
            nx.Add(((m.x + dx) \ stp) * stp)
            ny.Add(((m.y + dy) \ stp) * stp)
        Next
    End Sub

    Public Overrides Sub Undo()
        For l As Integer = 0 To Monsters.Count - 1
            Monsters(l).x = px(l)
            Monsters(l).y = py(l)
        Next
    End Sub

    Public Overrides Sub Redo()
        For l As Integer = 0 To Monsters.Count - 1
            Monsters(l).x = nx(l)
            Monsters(l).y = ny(l)
        Next
    End Sub

    Public Overrides ReadOnly Property CanMerge As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Sub Merge(ByVal act As Action)
        Dim act2 As MoveMonsterAction = act
        For l As Integer = 0 To Monsters.Count - 1
            Me.nx(l) = act2.nx(l)
            Me.ny(l) = act2.ny(l)
        Next
    End Sub

    Public Overrides Function ToString() As String
        If Monsters.Count = 1 Then
            Return "Move monster"
        Else
            Return "Move " & Monsters.Count.ToString & " monsters"
        End If
    End Function
End Class

Public Class ChangeMonsterTypeAction
    Inherits MonsterAction

    Public newptr As Integer
    Public prevptr As New List(Of Integer)

    Public Sub New(ByVal monsters As List(Of Monster), ByVal ptr As Integer)
        MyBase.New(monsters)
        For Each m As Monster In monsters
            prevptr.Add(m.ptr)
        Next
        newptr = ptr
    End Sub

    Public Overrides Sub Undo()
        For l As Integer = 0 To Monsters.Count - 1
            Monsters(l).ptr = prevptr(l)
            Monsters(l).UpdateIdx()
        Next
    End Sub

    Public Overrides Sub Redo()
        For Each m As Monster In Monsters
            m.ptr = newptr
            m.UpdateIdx()
        Next
    End Sub

    Public Overrides ReadOnly Property CanMerge As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Sub Merge(ByVal act As Action)
        Me.newptr = CType(act, ChangeMonsterTypeAction).newptr
    End Sub

    Public Overrides Function ToString() As String
        If Monsters.Count = 1 Then
            Return "Change monster type"
        Else
            Return "Change " & Monsters.Count.ToString & " monsters types"
        End If
    End Function
End Class