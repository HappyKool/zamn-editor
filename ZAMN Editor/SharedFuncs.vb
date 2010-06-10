﻿Public Class SharedFuncs

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
        s.Seek(ReadFileAddr(s), IO.SeekOrigin.Begin)
    End Sub

    Public Shared Function ReadRelativeFileAddr(ByVal s As IO.Stream, ByVal bank As Byte) As Integer
        Dim part2 As Integer = s.ReadByte + s.ReadByte * &H100
        If bank < &H80 Or part2 < &H8000 Then
            Return -1
        End If
        Return (bank - &H80) * &H8000 + part2 - &H7E00
    End Function

    Public Shared Sub GoToRelativePointer(ByVal s As IO.Stream, ByVal bank As Byte)
        s.Seek(ReadRelativeFileAddr(s, bank), IO.SeekOrigin.Begin)
    End Sub

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
        Dim result(8, 8) As Byte
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

    Public Shared Sub DrawTile(ByVal bmp As Bitmap, ByVal x As Integer, ByVal y As Integer, ByVal gfx As Byte(), ByVal gfxindex As Integer, ByVal pallette As Color(), ByVal palIndex As Integer, ByVal xFlip As Boolean, ByVal yFlip As Boolean)
        Dim tile As Byte(,) = PlanarToLinear(gfx, gfxindex)
        Dim xStep As Integer = 1, yStep As Integer = 1
        If xFlip Then
            x += 7
            xStep = -1
        End If
        If yFlip Then
            y += 7
            yStep = -1
        End If
        For l As Integer = 0 To 7
            For m As Integer = 0 To 7
                bmp.SetPixel(x, y, pallette(palIndex + tile(l, m)))
                x += xStep
            Next
            y += yStep
            x -= 8 * xStep
        Next
    End Sub
End Class