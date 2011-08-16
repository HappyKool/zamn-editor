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

    Public Sub New(ByVal s As IO.Stream, ByVal name As String, ByVal num As Integer, Optional ByVal import As Boolean = False, Optional ByVal romStream As IO.Stream = Nothing)
        Me.name = name
        Me.num = num
        Dim startAddr As Long = s.Position
        Debug.WriteLine(name + ": 0x" + Hex(startAddr))
        If import Then
            Dim tiles As Integer = Shrd.ReadFileAddr(s)
            s.Seek(4, IO.SeekOrigin.Current)
            tileset = New Tileset(romStream, tiles, Shrd.ReadFileAddr(s), Shrd.ReadFileAddr(s), Shrd.ReadFileAddr(s), Shrd.ReadFileAddr(s), Shrd.ReadFileAddr(s))
        Else
            tileset = New Tileset(s)
        End If
        s.Seek(startAddr + &H22, IO.SeekOrigin.Begin)
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
        If import Then
            s.Seek(4, IO.SeekOrigin.Begin)
            s.Seek(s.ReadByte + s.ReadByte * &H100 + 14, IO.SeekOrigin.Begin)
        Else
            s.Seek(startAddr + &H20, IO.SeekOrigin.Begin)
            Shrd.GoToRelativePointer(s, &H9F)
        End If
        If Not ErrorLog.HasError Then
            Do
                Dim v As Integer = s.ReadByte + s.ReadByte * &H100
                If v = 0 Then Exit Do
                items.Add(New Item(v - 8, s.ReadByte + s.ReadByte * &H100 - 8, s.ReadByte \ 2))
            Loop
        End If
        If import Then
            s.Seek(2, IO.SeekOrigin.Begin)
            s.Seek(s.ReadByte + s.ReadByte * &H100 + 14, IO.SeekOrigin.Begin)
        Else
            s.Seek(startAddr + &H1E, IO.SeekOrigin.Begin)
            Shrd.GoToRelativePointer(s, &H9F)
        End If
        If Not ErrorLog.HasError Then
            For n As Integer = 1 To 10
                Dim vic As New Victim(s.ReadByte + s.ReadByte * &H100, s.ReadByte + s.ReadByte * &H100, s.ReadByte + s.ReadByte * &H100, _
                                      s.ReadByte + s.ReadByte * &H100, Shrd.ReadFileAddr(s))
                vic.x -= Ptr.SpriteOffsets(vic.index * 2)
                vic.y -= Ptr.SpriteOffsets(vic.index * 2 + 1)
                victims.Add(vic)
            Next
            victims.Add(New Victim(p1Start.X - 8, p1Start.Y - 39, 0, 0, 1))
            victims.Add(New Victim(p2Start.X - 16, p2Start.Y - 42, 0, 0, 2))
            Do
                Dim x As Integer = s.ReadByte + s.ReadByte * &H100
                If x = 0 Then Exit Do
                Dim mon As New NRMonster(x, s.ReadByte + s.ReadByte * &H100, s.ReadByte + s.ReadByte * &H100, _
                                         s.ReadByte + s.ReadByte * &H100, Shrd.ReadFileAddr(s))
                mon.x -= Ptr.SpriteOffsets(mon.index * 2)
                mon.y -= Ptr.SpriteOffsets(mon.index * 2 + 1)
                NRMonsters.Add(mon)
            Loop
        End If
        If import Then
            s.Seek(0, IO.SeekOrigin.Begin)
            s.Seek(s.ReadByte + s.ReadByte * &H100 + 14, IO.SeekOrigin.Begin)
        Else
            s.Seek(startAddr + 28, IO.SeekOrigin.Begin)
            Shrd.GoToRelativePointer(s, &H9F)
        End If
        If Not ErrorLog.HasError Then
            Do
                Dim radius As Integer = s.ReadByte
                Dim x1 As Integer = s.ReadByte
                If x1 = 0 And radius = 0 Then Exit Do
                Dim mon As New Monster(radius, x1 + s.ReadByte * &H100, s.ReadByte + s.ReadByte * &H100, _
                                       s.ReadByte, Shrd.ReadFileAddr(s))
                mon.x -= Ptr.SpriteOffsets(mon.index * 2)
                mon.y -= Ptr.SpriteOffsets(mon.index * 2 + 1)
                Monsters.Add(mon)
            Loop
        End If
        If import Then
            s.Seek(6, IO.SeekOrigin.Begin)
            s.Seek(s.ReadByte + s.ReadByte * &H100 + 14, IO.SeekOrigin.Begin)
            page1 = New TitlePage(s)
            s.Seek(8, IO.SeekOrigin.Begin)
            s.Seek(s.ReadByte + s.ReadByte * &H100 + 14, IO.SeekOrigin.Begin)
            page2 = New TitlePage(s)
        Else
            s.Seek(startAddr + &H36, IO.SeekOrigin.Begin)
            Shrd.GoToRelativePointer(s, &H9F)
            ErrorLog.CheckError("Level is missing title page 1.")
            page1 = New TitlePage(s)
            s.Seek(startAddr + &H38, IO.SeekOrigin.Begin)
            Shrd.GoToRelativePointer(s, &H9F)
            ErrorLog.CheckError("Level is missing title page 2.")
            page2 = New TitlePage(s)
        End If
        If import Then
            s.Seek(10, IO.SeekOrigin.Begin)
            s.Seek(s.ReadByte + s.ReadByte * &H100 + 14, IO.SeekOrigin.Begin)
        Else
            s.Seek(startAddr + &H3A, IO.SeekOrigin.Begin)
            Shrd.GoToRelativePointer(s, &H9F)
        End If
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
            If ZAMNEditor.Ptr.SpBossMonsters.Contains(ptr) Then
                Dim curaddr As Integer = s.Position
                Shrd.GoToPointer(s)
                Select Case ptr
                    Case ZAMNEditor.Ptr.SpBossMonsters(0)
                        bossMonsters.Add(New BossMonster(ptr, s, 8))
                    Case ZAMNEditor.Ptr.SpBossMonsters(1)
                        Dim value As Integer, count As Integer, passed As Boolean = False
                        Dim newData As New List(Of Byte)
                        Do
                            value = s.ReadByte + s.ReadByte * &H100
                            If value = 0 Then passed = True
                            If passed And (value = &HFFFF Or value = &HFFFE) Then count -= 1
                            If Not passed Then count += 1
                            newData.Add(value Mod &H100)
                            newData.Add(value \ &H100)
                            If passed And count = 0 Then Exit Do
                        Loop
                        bossMonsters.Add(New BossMonster(ptr, newData.ToArray()))
                End Select
                s.Seek(curaddr + 4, IO.SeekOrigin.Begin)
            Else
                bossMonsters.Add(New BossMonster(ptr, s.ReadByte + s.ReadByte * &H100, s.ReadByte + s.ReadByte * &H100))
            End If
        Loop
        If import Then
            s.Seek(12, IO.SeekOrigin.Begin)
            s.Seek(s.ReadByte + s.ReadByte * &H100, IO.SeekOrigin.Begin)
        Else
            s.Seek(startAddr + 4, IO.SeekOrigin.Begin)
            Shrd.GoToPointer(s)
            ErrorLog.CheckError("Level has no background data.")
        End If
        For l As Integer = 0 To Width * Height - 1
            Tiles(l Mod Width, l \ Width) = (s.ReadByte() + s.ReadByte() * &H100) And &HFF
        Next
        If import Then
            GFX = New LevelGFX(romStream, spritePal)
        Else
            GFX = New LevelGFX(s, spritePal)
        End If
        s.Close()
    End Sub

    Public Function GetWriteData() As LevelWriteData
        p1Start = New Point(victims(10).x + 8, victims(10).y + 39)
        p2Start = New Point(victims(11).x + 16, victims(11).y + 42)
        Dim file As New List(Of Byte)
        Dim addrOffsets(5) As Integer
        Dim bossPtrs As New List(Of Integer)
        file.AddRange(Shrd.ConvertAddr(tileset.address))
        file.AddRange(New Byte() {0, 0, 0, 0})
        file.AddRange(Shrd.ConvertAddr(tileset.collisionAddr))
        file.AddRange(Shrd.ConvertAddr(tileset.gfxAddr))
        file.AddRange(Shrd.ConvertAddr(tileset.paletteAddr))
        file.AddRange(Shrd.ConvertAddr(spritePal))
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
        For Each m As BossMonster In bossMonsters
            file.AddRange(Shrd.ConvertAddr(m.ptr))
            file.AddRange(New Byte() {m.x Mod &H100, m.x \ &H100, m.y Mod &H100, m.y \ &H100})
        Next
        file.AddRange(New Byte() {0, 0, 0, 0})
        For Each m As BossMonster In bossMonsters
            If Ptr.SpBossMonsters.Contains(m.ptr) Then
                bossPtrs.Add(file.Count)
                file.AddRange(m.exData)
            End If
        Next
        Dim x As Integer, y As Integer
        'Monster data
        addrOffsets(0) = file.Count
        For Each m As Monster In Monsters
            x = m.x + Ptr.SpriteOffsets(m.index * 2)
            y = m.y + Ptr.SpriteOffsets(m.index * 2 + 1)
            file.AddRange(New Byte() {m.radius, x Mod &H100, x \ &H100, y Mod &H100, y \ &H100, m.delay})
            file.AddRange(Shrd.ConvertAddr(m.ptr))
        Next
        file.AddRange(New Byte() {0, 0})
        'Victim data
        addrOffsets(1) = file.Count
        For Each v As Victim In victims
            If v.ptr > 2 Then
                x = v.x + Ptr.SpriteOffsets(v.index * 2)
                y = v.y + Ptr.SpriteOffsets(v.index * 2 + 1)
                file.AddRange(New Byte() {x Mod &H100, x \ &H100, y Mod &H100, y \ &H100, v.unused Mod &H100, v.unused \ &H100, _
                                          IIf(v.num = 10, 16, v.num), 0})
                file.AddRange(Shrd.ConvertAddr(v.ptr))
            End If
        Next
        'NRMonster data
        For Each m As NRMonster In NRMonsters
            x = m.x + Ptr.SpriteOffsets(m.index * 2)
            y = m.y + Ptr.SpriteOffsets(m.index * 2 + 1)
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
        Return New LevelWriteData(file, addrOffsets, bossPtrs)
    End Function

    Public Function ToFile() As Byte()
        Dim data As LevelWriteData = GetWriteData()
        Dim file(0) As Byte
        Dim fs As New ByteArrayStream(file)
        For l As Integer = 0 To 5
            fs.WriteByte(data.addrOffsets(l) Mod &H100)
            fs.WriteByte(data.addrOffsets(l) \ &H100)
        Next
        fs.WriteByte(0)
        fs.WriteByte(0)
        fs.Write(data.data, 0, data.data.Length)
        fs.Seek(12, IO.SeekOrigin.Begin)
        fs.WriteByte(fs.Length Mod &H100)
        fs.WriteByte(fs.Length \ &H100)
        fs.Seek(0, IO.SeekOrigin.End)
        For y As Integer = 0 To Height - 1
            For x As Integer = 0 To Width - 1
                fs.WriteByte(Tiles(x, y))
                fs.WriteByte(0)
            Next
        Next
        Return fs.array
    End Function
End Class
