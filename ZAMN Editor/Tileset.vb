Imports System.Drawing.Imaging

Public Class Tileset

    Public images(&HFF) As Bitmap
    Public priorityImages(&HFF) As Bitmap
    Public address As Integer
    Public collisionAddr As Integer
    Public paletteAddr As Integer
    Public gfxAddr As Integer
    Public pltAnimAddr As Integer

    Public Sub New(ByVal s As IO.Stream)
        Dim levelStartPos As Long = s.Position
        address = Shrd.ReadFileAddr(s)
        If address = -1 Then Throw New Exception("Invalid tiles pointer.")
        s.Seek(4, IO.SeekOrigin.Current)
        collisionAddr = Shrd.ReadFileAddr(s)
        If collisionAddr = -1 Then Throw New Exception("Invalid collision pointer.")
        gfxAddr = Shrd.ReadFileAddr(s)
        If gfxAddr = -1 Then Throw New Exception("Invalid graphics pointer.")
        paletteAddr = Shrd.ReadFileAddr(s)
        If paletteAddr = -1 Then Throw New Exception("Invalid palette pointer.")
        s.Seek(4, IO.SeekOrigin.Current)
        pltAnimAddr = Shrd.ReadFileAddr(s)
        Reload(s)
        s.Seek(levelStartPos, IO.SeekOrigin.Begin)
    End Sub

    Public Shared Function DecompressMap16(ByVal s As IO.Stream) As Byte()
        Dim result(&H7FFF) As Byte
        Dim dict(&HFFF) As Byte
        Dim writeDictPos As Integer = &HFEE
        Dim readDictPos As Integer
        Dim bytesLeft As Integer = s.ReadByte + s.ReadByte * &H100
        Dim bitsLeft As Integer = 0
        Dim writeByte As Byte
        Dim curByte As Byte
        Dim writeIndex As Integer
        Do
            If bitsLeft = 0 Then
                bitsLeft = 8
                If ReadNext(s, writeByte, bytesLeft) Then Exit Do
            End If
            bitsLeft -= 1
            If (writeByte And 1) = 1 Then
                If ReadNext(s, curByte, bytesLeft) Then Exit Do
                WriteNext(result, writeIndex, curByte)
                dict(writeDictPos) = curByte
                writeDictPos = (writeDictPos + 1) And &HFFF
            Else
                If ReadNext(s, readDictPos, bytesLeft) OrElse ReadNext(s, curByte, bytesLeft) Then Exit Do
                readDictPos = ((curByte And &HF0) * 16) Or readDictPos
                For l As Integer = 0 To (curByte And &HF) + 2
                    curByte = dict(readDictPos)
                    dict(writeDictPos) = curByte
                    WriteNext(result, writeIndex, curByte)
                    writeDictPos = (writeDictPos + 1) And &HFFF
                    readDictPos = (readDictPos + 1) And &HFFF
                Next
            End If
            writeByte = writeByte \ 2
        Loop
        Return result
    End Function

    Private Shared Function ReadNext(ByVal s As IO.Stream, ByRef result As Integer, ByRef bytesLeft As Integer) As Boolean
        If bytesLeft = 0 Then Return True
        bytesLeft -= 1
        result = s.ReadByte()
        Return False
    End Function

    Private Shared Sub WriteNext(ByRef result As Byte(), ByRef writeindex As Integer, ByVal value As Integer)
        result(writeindex) = CByte(value)
        writeindex += 1
    End Sub

    Public Sub Reload(ByVal s As IO.Stream)
        'Load palette
        s.Seek(paletteAddr, IO.SeekOrigin.Begin)
        Dim plt As Color() = Shrd.ReadPalette(s, &H80, False)
        'Load graphics data
        s.Seek(gfxAddr, IO.SeekOrigin.Begin)
        Dim gfx(&H3FFF) As Byte
        s.Read(gfx, 0, &H4000)
        'Load map16 data
        s.Seek(address, IO.SeekOrigin.Begin)
        Dim map16 As Byte() = DecompressMap16(s)
        'Load collision data
        s.Seek(collisionAddr, IO.SeekOrigin.Begin)
        Dim collision(&H3FF) As Byte
        s.Read(collision, 0, &H400)
        'Palette animation (currently not used)
        If pltAnimAddr > 0 Then
            'Shrd.GoToPointer(s)
        End If
        'Convert GFX to linear format
        Dim LinGFX(511)(,) As Byte
        For l As Integer = 0 To &H1FF
            LinGFX(l) = Shrd.PlanarToLinear(gfx, l * &H20)
        Next
        'Copy to bitmaps
        For i As Integer = 0 To &HFF
            Dim CurBmp As New Bitmap(64, 64, PixelFormat.Format8bppIndexed), CurPrBmp As New Bitmap(64, 64, PixelFormat.Format8bppIndexed)
            Shrd.FillPalette(CurBmp, plt)
            Shrd.FillPalette(CurPrBmp, plt)
            'Shrd.MakePltTransparent(CurPrBmp)
            Dim data As BitmapData = CurBmp.LockBits(New Rectangle(0, 0, 64, 64), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed)
            Dim dataPr As BitmapData = CurPrBmp.LockBits(New Rectangle(0, 0, 64, 64), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed)
            Dim x As Integer = 0, y As Integer = 0
            For m = i * &H80 To i * &H80 + &H7F Step 2
                Dim g As Integer = map16(m)
                If (map16(m + 1) And 1) = 1 Then
                    g += &H100
                End If
                Shrd.DrawTile(data, x, y, LinGFX(g), &H10 * ((map16(m + 1) \ 4) And 7), (map16(m + 1) And &H40) > 1, (map16(m + 1) And &H80) > 1)
                If (collision(g * 2) And 2) > 0 Then
                    Shrd.DrawTile(dataPr, x, y, LinGFX(g), &H10 * ((map16(m + 1) \ 4) And 7), (map16(m + 1) And &H40) > 1, (map16(m + 1) And &H80) > 1)
                End If
                x += 8
                If x = 64 Then
                    x = 0
                    y += 8
                End If
            Next
            CurBmp.UnlockBits(data)
            CurPrBmp.UnlockBits(dataPr)
            images(i) = CurBmp
            priorityImages(i) = CurPrBmp
        Next
    End Sub
End Class
