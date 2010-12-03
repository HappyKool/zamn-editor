Public Class LevelSettings

    Public lvl As Level
    Public ed As Editor
    Public reloadTileset As Boolean

    Public tilesets As Integer() = {&HD8200, &HE38EF, &HD4200, &HE0200, &HDBEB5}
    Public palettes As Integer() = {&HF1076, &HF1276, &HF1376, &HF1476, 0, _
                                    &HF1E76, &HF1F76, 0, 0, 0, _
                                    &HF1A76, &HF1B76, &HF1C76, &HF1D76, 0, _
                                    &HF2076, &HF2176, &HF2276, &HF2376, &HF2476, _
                                    &HF1676, &HF1776, &HF1876, &HF1976, 0}
    Public palNames As String() = {"Standard", "Fall", "Winter", "Night", "", _
                                   "Standard", "Alternate", "", "", "", _
                                   "Standard", "Night", "Bright", "Dark", "", _
                                   "Office", "Dark Cave", "Light Office", "Dark Office", "Cave", _
                                   "Pyramid", "Beach", "Dark Beach", "Cave", ""}
    Public graphics As Integer() = {&HC0200, &HCC200, &HC8200, &HD0200, &HC4200}
    Public collision As Integer() = {&HDF6D1, &HE70AB, &HE6CAB, &HE74AB, &HDFAD1}
    Public unknown As Integer() = {&H70, &H69, &H70, &H57, &H59}
    Public pltAnim As Integer() = {-1, &H22AD, &H22EF, &H2337, &H2422, &H2464}
    Public boss As Integer() = {-1, &H1092C, &H11769, &H12ABB, &H12AC3, &H12D95, &H159CF, &H1AF33}

    Public Overloads Function ShowDialog(ByVal ed As Editor) As DialogResult
        Me.lvl = ed.EdControl.lvl
        Me.ed = ed
        reloadTileset = False
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
        If Array.IndexOf(unknown, lvl.unknown) = cboTiles.SelectedIndex Or Array.LastIndexOf(unknown, lvl.unknown) = cboTiles.SelectedIndex Then
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
        'Bonuses
        lvl.bonuses.Sort()
        lstCustomBonuses.Items.Clear()
        For l As Integer = 0 To lstBonuses.Items.Count - 1
            lstBonuses.SetItemChecked(l, False)
        Next
        For Each b As Integer In lvl.bonuses
            If b >= 4 And b <= 32 And b Mod 2 = 0 Then
                lstBonuses.SetItemChecked(b \ 2 - 2, True)
            Else
                lstCustomBonuses.Items.Add(Hex(b))
            End If
        Next
        'Palette fade
        chkPltFade.Checked = False
        For Each m As BossMonster In lvl.bossMonsters
            If m.ptr = &H12D95 Then
                chkPltFade.Checked = True
                addrPalF.Value = m.bgPlt
                palIdx = Array.IndexOf(palettes, m.bgPlt)
                If palIdx >= cboTiles.SelectedIndex * 5 And palIdx <= cboTiles.SelectedIndex * 5 + 4 Then
                    cboPalF.SelectedIndex = palIdx Mod 5
                End If
                If cboPalF.Items.Count > 0 And cboPalF.SelectedIndex > -1 Then
                    radPalAutoF.Checked = True
                Else
                    radPalManF.Checked = True
                End If
                addrSPalF.Value = m.sPlt
                If m.sPlt = &HF1176 Then
                    radSPalAutoF.Checked = True
                Else
                    radSPalManF.Checked = True
                End If
                Exit For
            End If
        Next
        Return Me.ShowDialog()
    End Function

    Private Sub UpdatePals()
        cboPal.Items.Clear()
        cboPalF.Items.Clear()
        If cboTiles.SelectedIndex = -1 Then Return
        Dim startIdx As Integer = cboTiles.SelectedIndex * 5
        For l As Integer = startIdx To startIdx + 4
            If palettes(l) <> 0 Then
                cboPal.Items.Add(palNames(l))
                cboPalF.Items.Add(palNames(l))
            End If
        Next
    End Sub

    Private Sub radAuto_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radColAuto.CheckedChanged, radPalAutoF.CheckedChanged, _
        radGFXAuto.CheckedChanged, radMusicAuto.CheckedChanged, radPalAuto.CheckedChanged, radSPalAuto.CheckedChanged, radPAnimAuto.CheckedChanged, _
        radTilesAuto.CheckedChanged, radUnk2Auto.CheckedChanged, radUnk3Auto.CheckedChanged, radUnkAuto.CheckedChanged, radSPalAutoF.CheckedChanged
        Dim rad As Control = sender
        For Each ctrl As Control In rad.Parent.Controls
            If ctrl.GetType.Equals(GetType(AddressUpDown)) Or ctrl.GetType.Equals(GetType(NumericUpDown)) Then
                ctrl.Enabled = False
            End If
            If ctrl.GetType.Equals(GetType(ComboBox)) Then
                ctrl.Enabled = True
            End If
        Next
        'Update values
        If cboTiles.SelectedIndex > -1 Then
            If radTilesAuto.Checked Then addrTiles.Value = tilesets(cboTiles.SelectedIndex)
            If cboPal.SelectedIndex > -1 And radPalAuto.Checked Then addrPal.Value = palettes(cboTiles.SelectedIndex * 5 + cboPal.SelectedIndex)
            If radGFXAuto.Checked Then addrGFX.Value = graphics(cboTiles.SelectedIndex)
            If radColAuto.Checked Then addrCol.Value = collision(cboTiles.SelectedIndex)
            If radUnkAuto.Checked Then nudUnk.Value = unknown(cboTiles.SelectedIndex)
            If cboPalF.SelectedIndex > -1 And radPalAutoF.Checked Then addrPalF.Value = palettes(cboTiles.SelectedIndex * 5 + cboPalF.SelectedIndex)
        End If
        If radSPalAuto.Checked Then addrSPal.Value = &HF1176
        If cboPltAnim.SelectedIndex > -1 And radPAnimAuto.Checked Then addrPAnim.Value = pltAnim(cboPltAnim.SelectedIndex)
        If cboMusic.SelectedIndex > -1 And radMusicAuto.Checked Then nudMusic.Value = cboMusic.SelectedIndex + 2
        If radUnk3Auto.Checked Then nudUnk3.Value = 511
        If radSPalAutoF.Checked Then addrSPalF.Value = &HF1176
    End Sub

    Private Sub radMan_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radColMan.CheckedChanged, radPalManF.CheckedChanged, _
        radGFXMan.CheckedChanged, radMusicMan.CheckedChanged, radPalMan.CheckedChanged, radPAnimMan.CheckedChanged, radSPalMan.CheckedChanged, _
        radTilesMan.CheckedChanged, radUnk2Man.CheckedChanged, radUnk3Man.CheckedChanged, radUnkMan.CheckedChanged, radSPalManF.CheckedChanged
        Dim rad As Control = sender
        For Each ctrl As Control In rad.Parent.Controls
            If ctrl.GetType.Equals(GetType(ComboBox)) Then
                ctrl.Enabled = False
            End If
            If ctrl.GetType.Equals(GetType(AddressUpDown)) Or ctrl.GetType.Equals(GetType(NumericUpDown)) Then
                ctrl.Enabled = True
            End If
        Next
    End Sub

    Private Sub cboMusic_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMusic.SelectedIndexChanged
        If cboMusic.SelectedIndex = -1 Then Return
        nudMusic.Value = cboMusic.SelectedIndex + 2
    End Sub

    Private Sub cboPal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPal.SelectedIndexChanged
        If cboPal.SelectedIndex = -1 Then Return
        addrPal.Value = palettes(cboTiles.SelectedIndex * 5 + cboPal.SelectedIndex)
    End Sub

    Private Sub cboPalF_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPalF.SelectedIndexChanged
        If cboPalF.SelectedIndex = -1 Then Return
        addrPalF.Value = palettes(cboTiles.SelectedIndex * 5 + cboPalF.SelectedIndex)
    End Sub

    Private Sub cboPltAnim_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPltAnim.SelectedIndexChanged
        If cboPltAnim.SelectedIndex = -1 Then Return
        addrPAnim.Value = pltAnim(cboPltAnim.SelectedIndex)
    End Sub

    Private Sub cboTiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTiles.SelectedIndexChanged
        If cboTiles.SelectedIndex = -1 Then Return
        If radGFXAuto.Checked Then addrGFX.Value = graphics(cboTiles.SelectedIndex)
        If radColAuto.Checked Then addrCol.Value = collision(cboTiles.SelectedIndex)
        If radUnkAuto.Checked Then nudUnk.Value = unknown(cboTiles.SelectedIndex)
        UpdatePals()
        If radPalMan.Checked = True Then Return
        cboPal.SelectedIndex = 0
        addrTiles.Value = tilesets(cboTiles.SelectedIndex)
    End Sub

    Private Sub chkPltFade_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPltFade.CheckedChanged
        grpPltFade.Enabled = chkPltFade.Checked
    End Sub

    Private Sub tileset_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles addrCol.ValueChanged, addrGFX.ValueChanged, addrPal.ValueChanged, addrTiles.ValueChanged
        reloadTileset = True
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        lvl.tileset.address = addrTiles.Value
        lvl.tileset.paletteAddr = addrPal.Value
        lvl.tileset.gfxAddr = addrGFX.Value
        lvl.tileset.collisionAddr = addrCol.Value
        lvl.unknown = nudUnk.Value
        lvl.spritePal = addrSPal.Value
        lvl.tileset.pltAnimAddr = addrPAnim.Value
        If cboPltAnim.SelectedIndex = 0 And radPAnimAuto.Checked Then lvl.tileset.pltAnimAddr = -1
        lvl.music = nudMusic.Value
        lvl.unknown2 = nudUnk2.Value
        lvl.unknown3 = nudUnk3.Value
        lvl.bonuses.Clear()
        For l As Integer = 0 To lstBonuses.Items.Count - 1
            If lstBonuses.GetItemChecked(l) Then
                lvl.bonuses.Add(l * 2 + 4)
            End If
        Next
        For Each i As String In lstCustomBonuses.Items
            lvl.bonuses.Add(CInt("&H" & i))
        Next
        Dim m As Integer = 0
        Do Until m = lvl.bossMonsters.Count
            If lvl.bossMonsters(m).ptr = &H12D95 Then
                lvl.bossMonsters.RemoveAt(m)
            Else
                m += 1
            End If
        Loop
        If chkPltFade.Checked Then
            lvl.bossMonsters.Add(New BossMonster(&H12D95, addrPalF.Value, addrSPalF.Value, False))
        End If
        If reloadTileset Then
            Dim s As New IO.FileStream(ed.r.path, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
            ed.EdControl.lvl.tileset.Reload(s)
            s.Close()
            ed.EdControl.Invalidate(True)
        End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        btnApply_Click(sender, e)
        Me.Close()
    End Sub

    Private Sub btnAddBonus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddBonus.Click
        If Not lstCustomBonuses.Items.Contains(Hex(nudCustBonus.Value)) And Not (nudCustBonus.Value >= 4 And nudCustBonus.Value <= 32 And nudCustBonus.Value Mod 2 = 0) Then
            lstCustomBonuses.Items.Add(Hex(nudCustBonus.Value))
        End If
    End Sub

    Private Sub btnDeleteBonus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteBonus.Click
        Dim items As New List(Of String)
        For Each i As String In lstCustomBonuses.SelectedItems
            items.Add(i)
        Next
        For Each i As String In items
            lstCustomBonuses.Items.Remove(i)
        Next
    End Sub
End Class
