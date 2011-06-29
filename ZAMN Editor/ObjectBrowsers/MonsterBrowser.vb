Public Class MonsterBrowser
    Inherits ObjectBrowser

    Public Sub New(ByVal gfx As LevelGFX)
        MyBase.New(gfx)
    End Sub

    Public Overrides Sub Init()
        itemCt = 19
        startIdx = 20
    End Sub
End Class
