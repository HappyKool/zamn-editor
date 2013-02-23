Imports System.Drawing.Imaging

Public Class Tileset

    Public images(&HFF) As Bitmap
    Public priorityImages(&HFF) As Bitmap
    Public address As Integer
    Public collisionAddr As Integer
    Public paletteAddr As Integer
    Public gfxAddr As Integer
    Public pltAnimAddr As Integer

    'Testing
    Public TileTiles(&HFF)(,) As Short
    Public collision As Byte()

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

    Public Sub New(ByVal s As IO.Stream, ByVal tilesAddr As Integer, ByVal colAddr As Integer, ByVal gfxAddr As Integer, ByVal palAddr As Integer, ByVal sprPal As Integer, ByVal pltAnimAddr As Integer)
        Me.address = tilesAddr
        Me.collisionAddr = colAddr
        Me.gfxAddr = gfxAddr
        Me.paletteAddr = palAddr
        Me.pltAnimAddr = pltAnimAddr
        Reload(s)
    End Sub

    Public Shared Function DecompressMap16(ByVal s As IO.Stream) As Byte()
        Dim result(&H10000) As Byte
        Dim dict(&HFFF) As Byte
        Dim bytesLeft As Integer = s.ReadByte + s.ReadByte * &H100
        Dim writeDictPos As Integer = &HFEE
        Dim readDictPos As Integer
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
        'ReDim Preserve result(writeIndex)
        Return result
    End Function

    Private Shared Function ReadNext(ByVal s As IO.Stream, ByRef result As Integer, ByRef bytesLeft As Integer) As Boolean
        If bytesLeft = 0 Then Return True
        bytesLeft -= 1
        result = s.ReadByte()
        Return False
    End Function

    Private Shared Sub WriteNext(ByRef result As Byte(), ByRef writeindex As Integer, ByVal value As Byte)
        result(writeindex) = value
        writeindex += 1
    End Sub

    Public Shared Function Compress(ByVal data As Byte()) As Byte()
        Dim result As New List(Of Byte)(data.Length)
        Dim dict(&HFFF) As Byte
        Dim dictIndex As Integer = &HFEE
        Dim dataIndex As Integer = 0
        Dim formatByte As Byte
        Dim formatBitCt As Integer = 0
        Dim formatByteIndex As Integer = 2

        Dim findDictPos As Integer, findDictLen As Integer
        Dim findRepSize As Integer, findRepLen As Integer

        result.Add(0) 'The final size will be put here
        result.Add(0)
        result.Add(0) 'And one more to hold the first format byte
        Do Until dataIndex = data.Length
            formatByte >>= 1
            formatBitCt += 1
            FindInDict(dict, data, dataIndex, findDictPos, findDictLen)
            FindRepeat(data, dataIndex, findRepSize, findRepLen)
            If findRepLen - findRepSize > findDictLen And findRepLen >= 3 Then 'Most efficient to insert a data repeat
                For i As Integer = 0 To findRepSize - 1
                    result.Add(data(dataIndex + i))
                    formatByte = formatByte Or &H80
                    If formatBitCt = 8 Then 'This has to be put here too sadly
                        result(formatByteIndex) = formatByte
                        formatByte = 0
                        formatBitCt = 0
                        formatByteIndex = result.Count
                        result.Add(0)
                    End If
                    formatByte >>= 1
                    formatBitCt += 1
                Next
                result.Add(dictIndex And &HFF)
                result.Add(((dictIndex And &HF00) >> 4) + (findRepLen - 3))
                For i As Integer = 0 To findRepLen + findRepSize - 1
                    dict((dictIndex + i) And &HFFF) = data(dataIndex + (i Mod findRepSize))
                Next
                dictIndex = (dictIndex + findRepLen + findRepSize) And &HFFF
                dataIndex += findRepLen + findRepSize
            ElseIf findDictPos > -1 Then 'Insert a load from the dictionary
                For i As Integer = 0 To findDictLen - 1
                    dict(dictIndex) = dict((findDictPos + i) And &HFFF)
                    dictIndex = (dictIndex + 1) And &HFFF
                Next
                dataIndex += findDictLen
                result.Add(findDictPos And &HFF)
                result.Add(((findDictPos And &HF00) >> 4) + (findDictLen - 3))
            Else 'Just insert the byte by itself
                result.Add(data(dataIndex))
                dict(dictIndex) = data(dataIndex)
                dictIndex = (dictIndex + 1) And &HFFF
                dataIndex += 1
                formatByte = formatByte Or &H80
            End If
            If formatBitCt = 8 Or dataIndex = data.Length Then
                formatByte >>= (8 - formatBitCt) 'If the file has ended, but there is still part of a format byte left
                result(formatByteIndex) = formatByte
                formatByte = 0
                formatBitCt = 0
                formatByteIndex = result.Count
                result.Add(0)
            End If
        Loop

        Dim len As Integer = result.Count - 2
        result(0) = len And &HFF
        result(1) = (len And &HFF00) >> 8
        Return result.ToArray()
    End Function

    Private Shared Sub FindInDict(ByVal dict As Byte(), ByVal data As Byte(), ByVal index As Integer, ByRef outPos As Integer, ByRef outLen As Integer)
        Dim maxMatchCt As Integer = 0
        Dim maxMatchPos As Integer = -1
        For idict As Integer = 0 To dict.Length - 1
            If dict(idict) = data(index) Then
                Dim i2 As Integer = 0
                For i2 = 0 To 17
                    If index + i2 >= data.Length Then Exit For
                    If dict((idict + i2) And &HFFF) <> data(index + i2) Then Exit For
                Next
                If i2 > maxMatchCt And i2 > 2 Then
                    maxMatchCt = i2
                    maxMatchPos = idict
                End If
            End If
        Next
        outPos = maxMatchPos
        outLen = maxMatchCt
    End Sub

    Private Shared Sub FindRepeat(ByVal data As Byte(), ByVal index As Integer, ByRef dataSize As Integer, ByRef totalSize As Integer)
        Dim maxSize As Integer = 1
        Dim maxDataSize As Integer = 0
        For dsize As Integer = 1 To 8
            Dim tsize As Integer
            For tsize = dsize To dsize + 17
                If index + tsize >= data.Length Then Exit For
                If data(index + tsize) <> data(index + (tsize Mod dsize)) Then Exit For
            Next
            If tsize - dsize > maxDataSize Then
                maxDataSize = tsize - dsize
                maxSize = dsize
            End If
        Next
        dataSize = maxSize
        totalSize = maxDataSize
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

        Me.collision = collision

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
            'Testing
            TileTiles(i) = New Short(7, 7) {}

            Dim CurBmp As New Bitmap(64, 64, PixelFormat.Format8bppIndexed), CurPrBmp As New Bitmap(64, 64, PixelFormat.Format8bppIndexed)
            Shrd.FillPalette(CurBmp, plt)
            Shrd.FillPalette(CurPrBmp, plt)
            'Shrd.MakePltTransparent(CurPrBmp)
            Dim data As BitmapData = CurBmp.LockBits(New Rectangle(0, 0, 64, 64), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed)
            Dim dataPr As BitmapData = CurPrBmp.LockBits(New Rectangle(0, 0, 64, 64), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed)
            Dim x As Integer = 0, y As Integer = 0
            For m As Integer = i * &H80 To i * &H80 + &H7F Step 2
                Dim g As Integer = map16(m)
                If (map16(m + 1) And 1) = 1 Then
                    g += &H100
                End If
                Shrd.DrawTile(data, x, y, map16(m + 1), map16(m), LinGFX)
                If (collision(g * 2) = 0 And collision(g * 2 + 1) = 0) Then
                    Shrd.DrawTile(dataPr, x, y, map16(m + 1), map16(m), LinGFX)
                End If
                'Testing
                TileTiles(i)(x \ 8, y \ 8) = (map16(m + 1) And 1) * &H100 + map16(m)

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
