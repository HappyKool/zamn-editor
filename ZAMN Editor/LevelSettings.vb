Public Class LevelSettings

    Public lvl As Level
    Public ed As Editor

    Public Overloads Function ShowDialog(ByVal ed As Editor) As DialogResult
        Me.lvl = ed.EdControl.lvl
        Me.ed = ed
        'Tiles
        addrTiles.Value = lvl.tileset.address
        cboTiles.SelectedIndex = Array.IndexOf(Ptr.Tilesets, lvl.tileset.address)
        If cboTiles.SelectedIndex <> -1 Then
            radTilesAuto.Checked = True
        Else
            radTilesMan.Checked = True
        End If
        'Palette
        addrPal.Value = lvl.tileset.paletteAddr
        UpdatePals()
        Dim palIdx As Integer = Array.IndexOf(Ptr.Palettes, lvl.tileset.paletteAddr)
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
        If Array.IndexOf(Ptr.Graphics, lvl.tileset.gfxAddr) = cboTiles.SelectedIndex Then
            radGFXAuto.Checked = True
        Else
            radGFXMan.Checked = True
        End If
        'Collision
        addrCol.Value = lvl.tileset.collisionAddr
        If Array.IndexOf(Ptr.Collision, lvl.tileset.collisionAddr) = cboTiles.SelectedIndex Then
            radColAuto.Checked = True
        Else
            radColMan.Checked = True
        End If
        'Unknown
        nudUnk.Value = lvl.unknown
        If Array.IndexOf(Ptr.Unknown, lvl.unknown) = cboTiles.SelectedIndex Or Array.LastIndexOf(Ptr.Unknown, lvl.unknown) = cboTiles.SelectedIndex Then
            radUnkAuto.Checked = True
        Else
            radUnkMan.Checked = True
        End If
        'Sprite palette
        addrSPal.Value = lvl.spritePal
        If lvl.spritePal = Ptr.SpritePlt Then
            radSPalAuto.Checked = True
        Else
            radSPalMan.Checked = True
        End If
        'Palette Animation
        addrPAnim.Value = lvl.tileset.pltAnimAddr
        cboPltAnim.SelectedIndex = Array.IndexOf(Ptr.PltAnim, lvl.tileset.pltAnimAddr)
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
        For Each m As BossMonster In lvl.objects.BossMonsters
            If m.ptr = Ptr.SpBossMonsters(0) Then
                chkPltFade.Checked = True
                addrPalF.Value = m.GetBGPalette
                palIdx = Array.IndexOf(Ptr.Palettes, addrPalF.Value)
                If palIdx >= cboTiles.SelectedIndex * 5 And palIdx <= cboTiles.SelectedIndex * 5 + 4 Then
                    cboPalF.SelectedIndex = If((palIdx Mod 5) >= cboPalF.Items.Count, -1, palIdx Mod 5)
                End If
                If cboPalF.Items.Count > 0 And cboPalF.SelectedIndex > -1 Then
                    radPalAutoF.Checked = True
                Else
                    radPalManF.Checked = True
                End If
                addrSPalF.Value = m.GetSpritePalette
                If addrSPalF.Value = Ptr.SpritePlt Then
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
            If Ptr.Palettes(l) <> 0 Then
                cboPal.Items.Add(Ptr.PalNames(l))
                cboPalF.Items.Add(Ptr.PalNames(l))
            End If
        Next
    End Sub

    Private Sub radAuto_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radColAuto.CheckedChanged, radPalAutoF.CheckedChanged, _
        radGFXAuto.CheckedChanged, radMusicAuto.CheckedChanged, radPalAuto.CheckedChanged, radSPalAuto.CheckedChanged, radPAnimAuto.CheckedChanged, _
        radTilesAuto.CheckedChanged, radUnk2Auto.CheckedChanged, radUnk3Auto.CheckedChanged, radUnkAuto.CheckedChanged, radSPalAutoF.CheckedChanged
        Dim rad As Control = sender
        For Each ctrl As Control In rad.Parent.Controls
            If TypeOf ctrl Is AddressUpDown Or TypeOf ctrl Is NumericUpDown Then
                ctrl.Enabled = False
            End If
            If TypeOf ctrl Is ComboBox Then
                ctrl.Enabled = True
            End If
        Next
        'Update values
        If cboTiles.SelectedIndex > -1 Then
            If radTilesAuto.Checked Then addrTiles.Value = Ptr.Tilesets(cboTiles.SelectedIndex)
            If cboPal.SelectedIndex > -1 And radPalAuto.Checked Then addrPal.Value = Ptr.Palettes(cboTiles.SelectedIndex * 5 + cboPal.SelectedIndex)
            If radGFXAuto.Checked Then addrGFX.Value = Ptr.Graphics(cboTiles.SelectedIndex)
            If radColAuto.Checked Then addrCol.Value = Ptr.Collision(cboTiles.SelectedIndex)
            If radUnkAuto.Checked Then nudUnk.Value = Ptr.Unknown(cboTiles.SelectedIndex)
            If cboPalF.SelectedIndex > -1 And radPalAutoF.Checked Then addrPalF.Value = Ptr.Palettes(cboTiles.SelectedIndex * 5 + cboPalF.SelectedIndex)
        End If
        If radSPalAuto.Checked Then addrSPal.Value = Ptr.SpritePlt
        If cboPltAnim.SelectedIndex > -1 And radPAnimAuto.Checked Then addrPAnim.Value = Ptr.PltAnim(cboPltAnim.SelectedIndex)
        If cboMusic.SelectedIndex > -1 And radMusicAuto.Checked Then nudMusic.Value = cboMusic.SelectedIndex + 2
        If radUnk3Auto.Checked Then nudUnk3.Value = 511
        If radSPalAutoF.Checked Then addrSPalF.Value = Ptr.SpritePlt
    End Sub

    Private Sub radMan_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radColMan.CheckedChanged, radPalManF.CheckedChanged, _
        radGFXMan.CheckedChanged, radMusicMan.CheckedChanged, radPalMan.CheckedChanged, radPAnimMan.CheckedChanged, radSPalMan.CheckedChanged, _
        radTilesMan.CheckedChanged, radUnk2Man.CheckedChanged, radUnk3Man.CheckedChanged, radUnkMan.CheckedChanged, radSPalManF.CheckedChanged
        Dim rad As Control = sender
        For Each ctrl As Control In rad.Parent.Controls
            If TypeOf ctrl Is ComboBox Then
                ctrl.Enabled = False
            End If
            If TypeOf ctrl Is AddressUpDown Or TypeOf ctrl Is NumericUpDown Then
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
        addrPal.Value = Ptr.Palettes(cboTiles.SelectedIndex * 5 + cboPal.SelectedIndex)
    End Sub

    Private Sub cboPalF_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPalF.SelectedIndexChanged
        If cboPalF.SelectedIndex = -1 Then Return
        addrPalF.Value = Ptr.Palettes(cboTiles.SelectedIndex * 5 + cboPalF.SelectedIndex)
    End Sub

    Private Sub cboPltAnim_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPltAnim.SelectedIndexChanged
        If cboPltAnim.SelectedIndex = -1 Then Return
        addrPAnim.Value = Ptr.PltAnim(cboPltAnim.SelectedIndex)
    End Sub

    Private Sub cboTiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTiles.SelectedIndexChanged
        If cboTiles.SelectedIndex = -1 Then Return
        If radGFXAuto.Checked Then addrGFX.Value = Ptr.Graphics(cboTiles.SelectedIndex)
        If radColAuto.Checked Then addrCol.Value = Ptr.Collision(cboTiles.SelectedIndex)
        If radUnkAuto.Checked Then nudUnk.Value = Ptr.Unknown(cboTiles.SelectedIndex)
        UpdatePals()
        If radPalMan.Checked = True Then Return
        cboPal.SelectedIndex = 0
        addrTiles.Value = Ptr.Tilesets(cboTiles.SelectedIndex)
    End Sub

    Private Sub chkPltFade_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPltFade.CheckedChanged
        grpPltFade.Enabled = chkPltFade.Checked
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim reloadTileset As Boolean = False
        Dim reloadSprites As Boolean = False
        If lvl.tileset.address <> addrTiles.Value Or lvl.tileset.paletteAddr <> addrPal.Value Or lvl.tileset.gfxAddr <> addrGFX.Value Or lvl.tileset.collisionAddr <> addrCol.Value Then
            reloadTileset = True
        End If
        If lvl.spritePal <> addrSPal.Value Then
            reloadSprites = True
        End If
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
        Do Until m = lvl.objects.BossMonsters.Count
            If lvl.objects.BossMonsters(m).ptr = Ptr.SpBossMonsters(0) Then
                lvl.objects.BossMonsters.RemoveAt(m)
            Else
                m += 1
            End If
        Loop
        If chkPltFade.Checked Then
            Dim exData(7) As Byte
            Array.Copy(Shrd.ConvertAddr(addrPalF.Value), 0, exData, 0, 4)
            Array.Copy(Shrd.ConvertAddr(addrSPalF.Value), 0, exData, 4, 4)
            lvl.objects.BossMonsters.Add(New BossMonster(Ptr.SpBossMonsters(0), exData))
        End If
        If reloadTileset Then
            Dim s As New IO.FileStream(ed.r.path, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
            lvl.tileset.Reload(s)
            ed.EdControl.TilePicker.LoadTileset(ed.EdControl.lvl.tileset)
            s.Close()
            ed.EdControl.Invalidate(True)
        End If
        If reloadSprites Then
            Dim s As New IO.FileStream(ed.r.path, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
            lvl.GFX.Reload(s, lvl.spritePal)
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
