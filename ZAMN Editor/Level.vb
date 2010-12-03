Public Class Level
    Public name As String
    Public num As Integer
    Public tileset As Tileset
    Public Tiles As Integer(,)
    Public Width As Integer
    Public Height As Integer
    Public items As New List(Of Item)
    Public victims As New List(Of Victim)
    Public NRMonsters As New List(Of NRMonster)
    Public Monsters As New List(Of Monster)
    Public p1Start As Point
    Public p2Start As Point
    Public music As Integer
    Public unknown As Integer
    Public unknown2 As Integer
    Public unknown3 As Integer
    Public page1 As TitlePage
    Public page2 As TitlePage
    Public bonuses As New List(Of Integer)
    Public spritePal As Integer
    Public bossMonsters As New List(Of BossMonster)
    Public GFX As LevelGFX

    Public Sub New(ByVal s As IO.Stream, ByVal name As String, ByVal num As Integer)
        Me.name = name
        Me.num = num
        Dim startAddr As Long = s.Position
        Debug.WriteLine(name + ": " + Hex(startAddr))
        tileset = New Tileset(s)
        s.Seek(&H22, IO.SeekOrigin.Current)
        Width = s.ReadByte() + s.ReadByte() * &H100
        Height = s.ReadByte() + s.ReadByte() * &H100
        ReDim Tiles(Width - 1, Height - 1)
        unknown = s.ReadByte + s.ReadByte * &H100
        unknown3 = s.ReadByte + s.ReadByte * &H100
        p1Start = New Point(s.ReadByte + s.ReadByte * &H100, s.ReadByte + s.ReadByte * &H100)
        p2Start = New Point(s.ReadByte + s.ReadByte * &H100, s.ReadByte + s.ReadByte * &H100)
        music = s.ReadByte + s.ReadByte * &H100
        unknown2 = s.ReadByte + s.ReadByte * &H100
        s.Seek(startAddr + &H14, IO.SeekOrigin.Begin)
        spritePal = Shrd.ReadFileAddr(s)
        s.Seek(startAddr + &H20, IO.SeekOrigin.Begin)
        Shrd.GoToRelativePointer(s, &H9F)
        If Not ErrorLog.HasError Then
            Do
                Dim v As Integer = s.ReadByte + s.ReadByte * &H100
                If v = 0 Then Exit Do
                items.Add(New Item(v - 8, s.ReadByte + s.ReadByte * &H100 - 8, s.ReadByte \ 2))
            Loop
        End If
        s.Seek(startAddr + &H1E, IO.SeekOrigin.Begin)
        Shrd.GoToRelativePointer(s, &H9F)
        If Not ErrorLog.HasError Then
            For n As Integer = 1 To 10
                Dim vic As New Victim(s.ReadByte + s.ReadByte * &H100, s.ReadByte + s.ReadByte * &H100, s.ReadByte + s.ReadByte * &H100, _
                                      s.ReadByte + s.ReadByte * &H100, Shrd.ReadFileAddr(s))
                vic.x -= LevelGFX.offsets(vic.index * 2)
                vic.y -= LevelGFX.offsets(vic.index * 2 + 1)
                victims.Add(vic)
            Next
            Do
                Dim x As Integer = s.ReadByte + s.ReadByte * &H100
                If x = 0 Then Exit Do
                Dim mon As New NRMonster(x, s.ReadByte + s.ReadByte * &H100, s.ReadByte + s.ReadByte * &H100, _
                                         s.ReadByte + s.ReadByte * &H100, Shrd.ReadFileAddr(s))
                mon.x -= LevelGFX.offsets(mon.index * 2)
                mon.y -= LevelGFX.offsets(mon.index * 2 + 1)
                NRMonsters.Add(mon)
            Loop
        End If
        s.Seek(startAddr + 28, IO.SeekOrigin.Begin)
        Shrd.GoToRelativePointer(s, &H9F)
        If Not ErrorLog.HasError Then
            Do
                Dim radius As Integer = s.ReadByte
                Dim x1 As Integer = s.ReadByte
                If x1 = 0 And radius = 0 Then Exit Do
                Dim mon As New Monster(radius, x1 + s.ReadByte * &H100, s.ReadByte + s.ReadByte * &H100, _
                                       s.ReadByte, Shrd.ReadFileAddr(s))
                mon.x -= LevelGFX.offsets(mon.index * 2)
                mon.y -= LevelGFX.offsets(mon.index * 2 + 1)
                Monsters.Add(mon)
            Loop
        End If
        s.Seek(startAddr + &H36, IO.SeekOrigin.Begin)
        Shrd.GoToRelativePointer(s, &H9F)
        ErrorLog.CheckError("Level is missing title page 1.")
        page1 = New TitlePage(s)
        s.Seek(startAddr + &H38, IO.SeekOrigin.Begin)
        Shrd.GoToRelativePointer(s, &H9F)
        ErrorLog.CheckError("Level is missing title page 2.")
        page2 = New TitlePage(s)
        s.Seek(startAddr + &H3A, IO.SeekOrigin.Begin)
        Shrd.GoToRelativePointer(s, &H9F)
        If Not ErrorLog.HasError Then
            Do
                Dim n As Integer = s.ReadByte + s.ReadByte * &H100
                If n = 0 Then Exit Do
                bonuses.Add(n)
            Loop
        End If
        s.Seek(startAddr + &H3C, IO.SeekOrigin.Begin)
        Do
            Dim ptr As Integer = Shrd.ReadFileAddr(s)
            If ptr = -1 Then Exit Do
            If ptr = &H12D95 Then 'Palette fade
                Dim curaddr As Integer = s.Position
                Shrd.GoToPointer(s)
                bossMonsters.Add(New BossMonster(ptr, Shrd.ReadFileAddr(s), Shrd.ReadFileAddr(s), False))
                s.Seek(curaddr + 4, IO.SeekOrigin.Begin)
            Else
                bossMonsters.Add(New BossMonster(ptr, s.ReadByte + s.ReadByte * &H100, s.ReadByte + s.ReadByte * &H100))
            End If
        Loop
        s.Seek(startAddr + 4, IO.SeekOrigin.Begin)
        Shrd.GoToPointer(s)
        ErrorLog.CheckError("Level has no background data.")
        For l As Integer = 0 To Width * Height - 1
            Tiles(l Mod Width, l \ Width) = (s.ReadByte() + s.ReadByte() * &H100) And &HFF
        Next
        GFX = New LevelGFX(s, spritePal)
        s.Close()
    End Sub

    Public Function WriteToFile() As LevelWriteData
        Dim file As New List(Of Byte)
        Dim addrOffsets(5) As Integer
        file.AddRange(Shrd.ConvertAddr(tileset.address))
        file.AddRange(New Byte() {0, 0, 0, 0})
        file.AddRange(Shrd.ConvertAddr(tileset.collisionAddr))
        file.AddRange(Shrd.ConvertAddr(tileset.gfxAddr))
        file.AddRange(Shrd.ConvertAddr(tileset.paletteAddr))
        file.AddRange(New Byte() {&H76, &H8F, &H9E, 0})
        file.AddRange(Shrd.ConvertAddr(tileset.pltAnimAddr))
        file.AddRange(New Byte() {0, 0, 0, 0, 0, 0, _
                                  Width Mod &H100, Width \ &H100, _
                                  Height Mod &H100, Height \ &H100, _
                                  unknown Mod &H100, unknown \ &H100, _
                                  unknown3 Mod &H100, unknown3 \ &H100, _
                                  p1Start.X Mod &H100, p1Start.X \ &H100, p1Start.Y Mod &H100, p1Start.Y \ &H100, _
                                  p2Start.X Mod &H100, p2Start.X \ &H100, p2Start.Y Mod &H100, p2Start.Y \ &H100, _
                                  music Mod &H100, music \ &H100, _
                                  unknown2 Mod &H100, unknown2 \ &H100, _
                                  0, 0, 0, 0, 0, 0})
        Dim fadeM As BossMonster = Nothing
        Dim bm As Integer = 0
        Do Until bm = bossMonsters.Count
            If bossMonsters(bm).ptr = &H12D95 Then
                fadeM = bossMonsters(bm)
                bossMonsters.RemoveAt(bm)
            Else
                bm += 1
            End If
        Loop
        If fadeM IsNot Nothing Then
            bossMonsters.Add(fadeM)
        End If
        For Each m As BossMonster In bossMonsters
            file.AddRange(Shrd.ConvertAddr(m.ptr))
            file.AddRange(New Byte() {m.x Mod &H100, m.x \ &H100, m.y Mod &H100, m.y \ &H100})
        Next
        file.AddRange(New Byte() {0, 0, 0, 0})
        If bossMonsters.Last.ptr = &H12D95 Then
            file.AddRange(Shrd.ConvertAddr(bossMonsters.Last.bgPlt))
            file.AddRange(Shrd.ConvertAddr(bossMonsters.Last.sPlt))
        End If
        Dim x As Integer, y As Integer
        'Monster data
        addrOffsets(0) = file.Count
        For Each m As Monster In Monsters
            x = m.x + LevelGFX.offsets(m.index * 2)
            y = m.y + LevelGFX.offsets(m.index * 2 + 1)
            file.AddRange(New Byte() {m.radius, x Mod &H100, x \ &H100, y Mod &H100, y \ &H100, m.delay})
            file.AddRange(Shrd.ConvertAddr(m.ptr))
        Next
        file.AddRange(New Byte() {0, 0})
        'Victim data
        addrOffsets(1) = file.Count
        For Each v As Victim In victims
            x = v.x + LevelGFX.offsets(v.index * 2)
            y = v.y + LevelGFX.offsets(v.index * 2 + 1)
            file.AddRange(New Byte() {x Mod &H100, x \ &H100, y Mod &H100, y \ &H100, v.unused Mod &H100, v.unused \ &H100, _
                                      IIf(v.num = 10, 16, v.num), 0})
            file.AddRange(Shrd.ConvertAddr(v.ptr))
        Next
        'NRMonster data
        For Each m As NRMonster In NRMonsters
            x = m.x + LevelGFX.offsets(m.index * 2)
            y = m.y + LevelGFX.offsets(m.index * 2 + 1)
            file.AddRange(New Byte() {x Mod &H100, x \ &H100, y Mod &H100, y \ &H100, m.unused1 Mod &H100, m.unused1 \ &H100, _
                                      m.unused2 Mod &H100, m.unused2 \ &H100})
            file.AddRange(Shrd.ConvertAddr(m.ptr))
        Next
        file.AddRange(New Byte() {0, 0})
        'Item data
        addrOffsets(2) = file.Count
        For Each i As Item In items
            x = i.x + 8
            y = i.y + 8
            file.AddRange(New Byte() {x Mod &H100, x \ &H100, y Mod &H100, y \ &H100, i.type * 2})
        Next
        file.AddRange(New Byte() {0, 0})
        'Level titles
        addrOffsets(3) = file.Count
        page1.Write(file, 1)
        addrOffsets(4) = file.Count
        page2.Write(file, 0)
        'Bonuses
        addrOffsets(5) = file.Count
        For Each b As Integer In bonuses
            file.AddRange(New Byte() {b Mod &H100, b \ &H100})
        Next
        file.AddRange(New Byte() {0, 0})
        Return New LevelWriteData(file, addrOffsets)
    End Function
End Class
