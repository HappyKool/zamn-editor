Public Class LevelSettings

    Public tilesets As Integer() = {&HD8200, &HE38EF, &HD4200, &HE0200, &HDBEB5}
    Public palettes As Integer() = {&HF1076, &HF1276, &HF1376, &HF1476, 0, _
                                    &HF1E76, &HF1F76, 0, 0, 0, _
                                    &HF1A76, &HF1B76, &HF1C76, &HF1D76, 0, _
                                    &HF2076, &HF2176, &HF2276, &HF2376, &HF2476, _
                                    &HF1676, &HF1776, &HF1876, &HF1976, 0}
    Public palNames As String() = {"Summer", "Fall", "Winter", "Night", "", _
                                   "Standard", "Alternate", "", "", "", _
                                   "Standard", "Night", "Bright", "Dark", "", _
                                   "Office", "Dark Cave", "Light Office", "Dark Office", "Cave", _
                                   "Pyramid", "Beach", "Dark Beach", "Cave", ""}
    Public graphics As Integer() = {&HC0200, &HCC200, &HC8200, &HD0200, &HC4200}
    Public collision As Integer() = {&HDF6D1, &HE70AB, &HE6CAB, &HE74AB, &HDFAD1}
    Public unknown As Integer() = {&H70, &H69, &H70, &H57, &H59}
    Public pltAnim As Integer() = {-1, &H22AD, &H22EF, &H2337, &H2422}
    Public boss As Integer() = {-1, &H1092C, &H11769, &H12ABB, &H12AC3, &H12D95, &H159CF, &H1AF33}

    Public Overloads Sub ShowDialog(ByVal lvl As Level)
        'Tiles
        addrTiles.Value = lvl.tileset.address
        cboTiles.SelectedIndex = Array.IndexOf(tilesets, lvl.tileset.address)
        If cboTiles.SelectedIndex <> -1 Then
            radTilesAuto.Checked = True
        Else
            radTilesMan.Checked = True
        End If
        'Palette
        addrPal.Value = lvl.tileset.paletteAddr
        UpdatePals()
        Dim palIdx As Integer = Array.IndexOf(palettes, lvl.tileset.paletteAddr)
        If palIdx >= cboTiles.SelectedIndex * 5 And palIdx <= cboTiles.SelectedIndex * 5 + 4 Then
            cboPal.SelectedIndex = palIdx Mod 5
        End If
        If cboPal.Items.Count > 0 And cboPal.SelectedIndex > -1 Then
            radPalAuto.Checked = True
        Else
            radPalMan.Checked = True
        End If
        'Graphics
        addrGFX.Value = lvl.tileset.gfxAddr
        If Array.IndexOf(graphics, lvl.tileset.gfxAddr) = cboTiles.SelectedIndex Then
            radGFXAuto.Checked = True
        Else
            radGFXMan.Checked = True
        End If
        'Collision
        addrCol.Value = lvl.tileset.collisionAddr
        If Array.IndexOf(collision, lvl.tileset.collisionAddr) = cboTiles.SelectedIndex Then
            radColAuto.Checked = True
        Else
            radColMan.Checked = True
        End If
        'Unknown
        nudUnk.Value = lvl.unknown
        If Array.IndexOf(unknown, lvl.unknown) = cboTiles.SelectedIndex Then
            radUnkAuto.Checked = True
        Else
            radUnkMan.Checked = True
        End If
        'Sprite palette
        addrSPal.Value = lvl.spritePal
        If lvl.spritePal = &HF1176 Then
            radSPalAuto.Checked = True
        Else
            radSPalMan.Checked = True
        End If
        'Palette Animation
        addrPAnim.Value = lvl.tileset.pltAnimAddr
        cboPltAnim.SelectedIndex = Array.IndexOf(pltAnim, lvl.tileset.pltAnimAddr)
        If cboPltAnim.SelectedIndex <> -1 Then
            radPAnimAuto.Checked = True
        Else
            radPAnimMan.Checked = True
        End If
        'Music
        nudMusic.Value = lvl.music
        If lvl.music >= 2 And lvl.music <= 11 Then
            cboMusic.SelectedIndex = lvl.music - 2
        End If
        If cboMusic.SelectedIndex <> -1 Then
            radMusicAuto.Checked = True
        Else
            radMusicMan.Checked = True
        End If
        'Boss
        addrBoss.Value = lvl.boss
        cboBoss.SelectedIndex = Array.IndexOf(boss, lvl.boss)
        If cboBoss.SelectedIndex <> -1 Then
            radBossAuto.Checked = True
        Else
            radBossMan.Checked = True
        End If
        'Unknown2
        nudUnk2.Value = lvl.unknown2
        radUnk2Man.Checked = True
        'Unknown3
        nudUnk3.Value = lvl.unknown3
        If lvl.unknown3 = &H1FF Then
            radUnk3Auto.Checked = True
        Else
            radUnk3Man.Checked = True
        End If
        Me.ShowDialog()
    End Sub

    Private Sub UpdatePals()
        cboPal.Items.Clear()
        If cboTiles.SelectedIndex = -1 Then Return
        Dim startIdx As Integer = cboTiles.SelectedIndex * 5
        For l As Integer = startIdx To startIdx + 4
            If palettes(l) <> 0 Then
                cboPal.Items.Add(palNames(l))
            End If
        Next
    End Sub
End Class