Imports System.Runtime.InteropServices
Imports System.Drawing.Imaging

Public Class Shrd

    Private Shared nameLCase As String() = {"The", "A", "An", "And", "But", "As", "At", "By", "For", "From", "In", "Into", "Of", "Off", "On", "Onto", "Over", "Past", "To", "Upon", "With", "Vs"}

    Public Shared Function ReadFileAddr(ByVal s As IO.Stream) As Integer
        Dim part2 As Integer = s.ReadByte() + s.ReadByte() * &H100
        Dim Banknum As Integer = s.ReadByte()
        s.ReadByte()
        If Banknum < &H80 Or part2 < &H8000 Then
            Return -1
        End If
        Return (Banknum - &H80) * &H8000 + part2 - &H7E00
    End Function

    Public Shared Sub GoToPointer(ByVal s As IO.Stream)
        Dim addr As Integer = ReadFileAddr(s)
        If addr = -1 Then
            ErrorLog.Report()
        Else
            s.Seek(addr, IO.SeekOrigin.Begin)
        End If
    End Sub

    Public Shared Function ReadRelativeFileAddr(ByVal s As IO.Stream, ByVal bank As Byte) As Integer
        Dim part2 As Integer = s.ReadByte + s.ReadByte * &H100
        If bank < &H80 Or part2 < &H8000 Then
            Return -1
        End If
        Return (bank - &H80) * &H8000 + part2 - &H7E00
    End Function

    Public Shared Sub GoToRelativePointer(ByVal s As IO.Stream, ByVal bank As Byte)
        Dim addr As Integer = ReadRelativeFileAddr(s, bank)
        If addr = -1 Then
            ErrorLog.Report()
        Else
            s.Seek(addr, IO.SeekOrigin.Begin)
        End If
    End Sub

    Public Shared Function ConvertAddr(ByVal address As Integer) As Byte()
        If address = 0 Or address = -1 Then
            Return New Byte() {0, 0, 0, 0}
        End If
        address -= &H200 'don't include header
        Dim bank As Integer = address \ &H8000
        Dim part2 As Integer = address - bank * &H8000 + &H8000
        Return New Byte() {part2 Mod &H100, part2 \ &H100, bank + &H80, 0}
    End Function

    Public Shared Function RGBtoSNESLo(ByVal RGB As Color) As Byte
        Return (RGB.B \ 8 * &H400 + RGB.G \ 8 * &H20 + RGB.R \ 8) Mod &H100
    End Function

    Public Shared Function RGBtoSNESHi(ByVal RGB As Color) As Byte
        Return (RGB.B \ 8 * &H400 + RGB.G \ 8 * 32 + RGB.R \ 8) \ &H100
    End Function

    Public Shared Function SNEStoRGB(ByVal LoByte As Byte, ByVal HiByte As Byte) As Color
        Dim v As Integer = LoByte + &H100 * HiByte
        Return Color.FromArgb((v Mod &H20) * 8, ((v \ &H20) Mod &H20) * 8, ((v \ &H400) Mod &H20) * 8)
    End Function

    Public Shared Function ReadPalette(ByVal s As IO.Stream, ByVal colorCount As Integer, ByVal transparant As Boolean) As Color()
        Dim plt(colorCount - 1) As Color
        For l As Integer = 0 To colorCount - 1
            plt(l) = SNEStoRGB(s.ReadByte, s.ReadByte)
            If transparant AndAlso l Mod 16 = 0 Then
                plt(l) = Color.FromArgb(0, plt(l))
            End If
        Next
        Return plt
    End Function

    Public Shared Function PlanarToLinear(ByVal bytes As Byte(), ByVal index As Integer) As Byte(,)
        Dim result(7, 7) As Byte
        Dim line As Integer = 0
        Dim bit As Integer = 0
        For l As Integer = index To index + &H1F Step 2
            For m As Integer = 0 To 7
                If (bytes(l) And (1 << m)) <> 0 Then
                    result(line, 7 - m) = result(line, 7 - m) Or (1 << bit)
                End If
                If (bytes(l + 1) And (1 << m)) <> 0 Then
                    result(line, 7 - m) = result(line, 7 - m) Or (1 << bit + 1)
                End If
            Next
            line += 1
            If line = 8 Then
                line = 0
                bit += 2
            End If
        Next
        Return result
    End Function

    Public Shared Sub DrawTile(ByVal bmp As Bitmap, ByVal x As Integer, ByVal y As Integer, ByVal gfx As Byte(), ByVal gfxindex As Integer, ByVal palette As Color(), ByVal palIndex As Integer, ByVal xFlip As Boolean, ByVal yFlip As Boolean)
        Dim tile As Byte(,) = PlanarToLinear(gfx, gfxindex)
        Dim xStep As Integer = 1, yStep As Integer = 1
        If xFlip Then
            x += 7
            xStep = -1
        End If
        Dim xOrig As Integer = x
        If yFlip Then
            y += 7
            yStep = -1
        End If
        For l As Integer = 0 To 7
            For m As Integer = 0 To 7
                If palette(palIndex + tile(l, m)).A > 0 Then
                    bmp.SetPixel(x, y, palette(palIndex + tile(l, m)))
                End If
                x += xStep
            Next
            y += yStep
            x = xOrig
        Next
    End Sub

    Public Shared Sub DrawTile(ByVal bmp As Bitmap, ByVal x As Integer, ByVal y As Integer, ByVal s As IO.Stream, ByVal palette As Color(), ByVal palIndex As Integer, ByVal xFlip As Boolean, ByVal yFlip As Boolean)
        Dim gfx(31) As Byte
        s.Read(gfx, 0, 32)
        DrawTile(bmp, x, y, gfx, 0, palette, palIndex, xFlip, yFlip)
    End Sub

    Public Shared Sub DrawTile(ByVal bmp As BitmapData, ByVal x As Integer, ByVal y As Integer, ByVal tile As Byte(,), ByVal palIndex As Byte, ByVal xFlip As Boolean, ByVal yFlip As Boolean)
        Dim xStep As Integer = 1, yStep As Integer = 1
        If xFlip Then
            x += 7
            xStep = -1
        End If
        Dim xOrig As Integer = x
        If yFlip Then
            y += 7
            yStep = -1
        End If
        For l As Integer = 0 To 7
            For m As Integer = 0 To 7
                Marshal.WriteByte(bmp.Scan0, y * bmp.Stride + x, palIndex + tile(l, m))
                x += xStep
            Next
            y += yStep
            x = xOrig
        Next
    End Sub

    Public Shared Sub FillPalette(ByVal bmp As Bitmap, ByVal colors As Color())
        Dim pal As ColorPalette = bmp.Palette
        For l As Integer = 0 To colors.Length - 1
            pal.Entries(l) = colors(l)
        Next
        bmp.Palette = pal
    End Sub

    Public Shared Sub MakePltTransparent(ByVal bmp As Bitmap)
        Dim pal As ColorPalette = bmp.Palette
        For l As Integer = 0 To pal.Entries.Length - 1 Step 16
            pal.Entries(l) = Color.Transparent
        Next
        bmp.Palette = pal
    End Sub

    Public Shared Function RealSize(ByVal rect As Rectangle) As Rectangle
        Return New Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 1)
    End Function

    Public Shared Function HexL(ByVal n As Integer, ByVal length As Integer) As String
        Dim str As String = Hex(n)
        If str.Length < length Then
            Return StrDup(length - str.Length, "0") & str
        Else
            Return str
        End If
    End Function

    Public Shared Sub InsertBytes(ByVal s As IO.Stream, ByVal byteCount As Integer)
        If byteCount < 0 Then
            s.Seek(-byteCount, IO.SeekOrigin.Current)
        End If
        Dim rest(s.Length - s.Position - 1) As Byte
        Dim start As Long = s.Position
        s.Read(rest, 0, rest.Length)
        s.Seek(start + byteCount, IO.SeekOrigin.Begin)
        s.Write(rest, 0, rest.Length)
    End Sub

    Public Shared Function ToText(ByVal data As Byte()) As String
        Dim str As String = ""
        For l As Integer = 0 To data.Length - 1
            str &= HexL(data(l), 2)
        Next
        Return str
    End Function

    Public Shared Function FromText(ByVal str As String) As Byte()
        Dim data(str.Length \ 2 - 1) As Byte
        For l As Integer = 0 To data.Length - 1
            data(l) = CByte("&H" & Mid(str, l * 2 + 1, 2))
        Next
        Return data
    End Function

    Public Shared Function PropperCase(ByVal str As String) As String
        Dim pos As Integer = 1
        Dim len As Integer
        Dim rstr As String = UCase(str(0)) & LCase(Mid(str, 2))
        Do While InStr(pos, rstr, " ") > 0
            pos = InStr(pos, rstr, " ")
            rstr = Mid(rstr, 1, pos) & UCase(rstr(pos)) & Mid(rstr, pos + 2)
            pos += 1
        Loop
        For l As Integer = 0 To nameLCase.Length - 1
            len = nameLCase(l).Length
            pos = 1
            Do While InStr(pos, rstr, nameLCase(l))
                pos = InStr(pos, rstr, nameLCase(l))
                If (pos >= 3 AndAlso rstr(pos - 3) <> ":") And (pos = rstr.Length - len + 1 OrElse rstr(pos + len - 1) = " ") Then
                    rstr = Mid(rstr, 1, pos - 1) & LCase(nameLCase(l)) & Mid(rstr, pos + len)
                End If
                pos += 1
            Loop
        Next
        Return rstr
    End Function

    Public Shared Function ReplaceFirst(ByVal str As String, ByVal findStr As String, ByVal replaceStr As String) As String
        Dim pos As Integer = InStr(str, findStr)
        If pos > 0 Then
            Return Mid(str, 1, pos - 1) & replaceStr & Mid(str, pos + findStr.Length)
        End If
        Return str
    End Function

    Public Shared Function InStrN(ByVal str As String, ByVal find As String, ByVal n As Integer) As Integer
        Dim p As Integer = 1
        Do While InStr(p, str, find) > 0 And n > 0
            p = InStr(p, str, find) + 1
            n -= 1
        Loop
        Return p - 1
    End Function
End Class
