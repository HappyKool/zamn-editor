Imports System.IO

Public Class ROM

    Public path As String
    Public regLvlCount As Integer
    Public maxLvlNum As Integer
    Public bonusLvls As New List(Of Integer)
    Public failed As Boolean = False

    Private Shared offsetPos As Integer() = {&H1C, &H1E, &H20, &H36, &H38, &H3A}
    Public Const lvlPtrs As Integer = &HF8200
    Public Const bonusLvlNums As Integer = &H1537E

    Public Sub New(ByVal path As String)
        Try
            Me.path = path
            Dim s As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)
            s.Seek(lvlPtrs, SeekOrigin.Begin)
            regLvlCount = s.ReadByte() + s.ReadByte() * &H100
            maxLvlNum = regLvlCount
            s.Seek(bonusLvlNums, SeekOrigin.Begin)
            Dim num As Integer
            Dim curLvl As Integer = 0
            For l As Integer = 0 To maxLvlNum
                num = s.ReadByte() + s.ReadByte() * &H100
                If num <> 0 Then
                    bonusLvls.Add(num)
                    maxLvlNum = Math.Max(maxLvlNum, num)
                End If
                curLvl += 1
            Next
            s.Close()
        Catch ex As Exception
            failed = True
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Public Function GetLvlPtr(ByVal num As Integer, ByVal s As Stream) As Integer
        s.Seek(&HF8202 + num * 2, SeekOrigin.Begin)
        Return s.ReadByte() + s.ReadByte * &H100 + &HF0200
    End Function

    Public Function GotoLvlPtr(ByVal num As Integer, ByVal s As Stream) As Integer
        s.Seek(GetLvlPtr(num, s), SeekOrigin.Begin)
    End Function

    Public Function GetLevel(ByVal num As Integer, ByVal name As String) As Level
        Dim s As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)
        GotoLvlPtr(num, s)
        Return New Level(s, name, num)
    End Function

    Public Function GetAllLvlPtrs(ByVal s As Stream) As DList(Of Integer, Integer)
        Dim ptrs As New DList(Of Integer, Integer)
        For l As Integer = 0 To maxLvlNum
            ptrs.Add(l, GetLvlPtr(l, s))
        Next
        For Each num As Integer In bonusLvls
            ptrs.Add(num, GetLvlPtr(num, s))
        Next
        Return ptrs
    End Function

    Public Sub SaveLevel(ByVal lvl As Level)
        Dim fs As New FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.Read)
        Dim data As LevelWriteData = lvl.WriteToFile()
        Dim fs2 As New FileStream(Application.StartupPath + "\lvl.bin", FileMode.Create)
        fs2.Write(data.data, 0, data.data.Length)
        fs2.Close()
        Dim ptrs As DList(Of Integer, Integer) = GetAllLvlPtrs(fs)
        ptrs.SortBySecond()
        Dim lvlPtr = GetLvlPtr(lvl.num, fs)
        fs.Seek(lvlPtr + 4, SeekOrigin.Begin)
        Shrd.GoToPointer(fs)
        Dim BGAddr As Byte() = Shrd.ConvertAddr(fs.Position)
        data.data(4) = BGAddr(0)
        data.data(5) = BGAddr(1)
        data.data(6) = BGAddr(2)
        For y As Integer = 0 To lvl.Height - 1
            For x As Integer = 0 To lvl.Width - 1
                fs.WriteByte(lvl.Tiles(x, y))
                fs.WriteByte(0)
            Next
        Next
        fs.Seek(lvlPtr, SeekOrigin.Begin)
        Dim SNESAddr As Byte()
        For l As Integer = 0 To 5
            SNESAddr = Shrd.ConvertAddr(data.addrOffsets(l) + lvlPtr)
            data.data(offsetPos(l)) = SNESAddr(0)
            data.data(offsetPos(l) + 1) = SNESAddr(1)
        Next
        Dim lenDiff As Integer = 0
        If ptrs.L2.IndexOf(lvlPtr) < ptrs.L2.Count - 1 Then
            lenDiff = data.data.Length - (ptrs.L2(ptrs.L2.IndexOf(lvlPtr) + 1) - lvlPtr)
            Shrd.InsertBytes(fs, lenDiff)
        End If
        fs.Seek(lvlPtr, SeekOrigin.Begin)
        fs.Write(data.data, 0, data.data.Length)
        fs.Seek(lvlPtr + &H3C, SeekOrigin.Begin)
        If Shrd.ReadFileAddr(fs) = &H12D95 Then
            fs.Write(Shrd.ConvertAddr(fs.Position + 8), 0, 4)
        End If
        For l As Integer = ptrs.L2.IndexOf(lvlPtr) + 1 To ptrs.L2.Count - 1 'update level pointers
            fs.Seek(lvlPtrs + 2 + ptrs.L1(l) * 2, SeekOrigin.Begin)
            Dim NewPtr As Integer = fs.ReadByte + fs.ReadByte * &H100 + lenDiff
            fs.Seek(-2, SeekOrigin.Current)
            fs.WriteByte(NewPtr Mod &H100)
            fs.WriteByte(NewPtr \ &H100)
            NewPtr += &HF0200
            fs.Seek(NewPtr, SeekOrigin.Begin)
            Dim NewPtr2 As Integer
            For m As Integer = 0 To 5 'Update pointers within level files
                fs.Seek(NewPtr + offsetPos(m), SeekOrigin.Begin)
                NewPtr2 = fs.ReadByte + fs.ReadByte * &H100 + lenDiff
                fs.Seek(-2, SeekOrigin.Current)
                fs.WriteByte(NewPtr2 Mod &H100)
                fs.WriteByte(NewPtr2 \ &H100)
            Next
            fs.Seek(NewPtr + &H3C, SeekOrigin.Begin)
            Do 'Update palette fade boss monsters
                NewPtr2 = Shrd.ReadFileAddr(fs)
                If NewPtr2 = &H12D95 Then
                    NewPtr2 = Shrd.ReadFileAddr(fs) + lenDiff
                    fs.Seek(-4, SeekOrigin.Current)
                    fs.Write(Shrd.ConvertAddr(NewPtr2), 0, 4)
                ElseIf NewPtr2 = -1 Then
                    Exit Do
                End If
            Loop
        Next
        fs.SetLength(&H100200)
        fs.Close()
    End Sub
End Class
