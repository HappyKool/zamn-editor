Public Class Monster
    Inherits LevelObj

    Public radius As Byte
    Public delay As Byte
    Private _ptr As Integer
    Public Property ptr As Integer
        Get
            Return _ptr
        End Get
        Set(value As Integer)
            _ptr = value
            UpdateIdx()
        End Set
    End Property
    Private _index As Integer
    Public Property index As Integer
        Get
            Return _index
        End Get
        Set(value As Integer)
            _index = value
            UpdatePtr()
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub New(ByVal radius As Byte, ByVal x As Integer, ByVal y As Integer, ByVal delay As Byte, ByVal ptr As Integer)
        Me.radius = radius
        Me.x = x
        Me.y = y
        Me.delay = delay
        _ptr = ptr
        UpdateIdx()
    End Sub

    Public Sub New(ByVal m As Monster)
        Me.radius = m.radius
        Me.x = m.x
        Me.y = m.y
        Me.delay = m.delay
        _ptr = m.ptr
        UpdateIdx()
    End Sub

    Public Overrides Function Clone() As LevelObj
        Return New Monster(Me)
    End Function

    Public Overrides Function Width(ByVal gfx As LevelGFX) As Integer
        Return gfx.VictimImages(_index).Width
    End Function

    Public Overrides Function Height(ByVal gfx As LevelGFX) As Integer
        Return gfx.VictimImages(_index).Height
    End Function

    Private Sub UpdateIdx()
        _index = Array.IndexOf(ZAMNEditor.Ptr.SpritePtrs, _ptr)
        If _index = -1 Then _index = 0
    End Sub

    Private Sub UpdatePtr()
        If _index > 0 Then
            _ptr = ZAMNEditor.Ptr.SpritePtrs(_index)
        Else
            _ptr = 0
        End If
    End Sub
End Class
