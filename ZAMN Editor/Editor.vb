﻿Public Class Editor

    Public r As ROM
    Public EdControl As LvlEdCtrl
    Public CurTool As Tool
    Public zoomLevel As Single = 1
    Private updateTab As Boolean = True
    Private EditingTools As Tool()
    Private LevelItems As ToolStripItem()

    Private Sub Editor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If My.Settings.Initialized Then
            Me.Location = My.Settings.Location
            Me.Size = My.Settings.Size
            If My.Settings.Maximized Then
                Me.WindowState = FormWindowState.Maximized
            End If
        End If
        EditingTools = New Tool() {New PaintbrushTool(Me), New DropperTool(Me), New TileSuggestTool(Me), New RectangleSelectTool(Me), New PencilSelectTool(Me), _
                                   New TileSelectTool(Me), New ItemTool(Me), New VictimTool(Me), New NRMonsterTool(Me), New MonsterTool(Me), New BossMonsterTool(Me)}
        LevelItems = New ToolStripItem() {FileSave, SaveTool, EditPaste, PasteTool, EditSelectAll, EditSelectNone, ViewGrid, ViewPriority, _
                                          LevelExport, LevelImport, LevelCopy, LevelPaste, LevelSettingsM}
        TileSuggestList.LoadAll()
        If My.Settings.RecentROMs <> "" Then
            RecentROMs.Items = StringToList(My.Settings.RecentROMs)
        End If
    End Sub

    Private Sub Editor_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        My.Settings.Initialized = True
        My.Settings.Maximized = Me.WindowState = FormWindowState.Maximized
        Me.WindowState = FormWindowState.Normal
        My.Settings.Location = Me.Location
        My.Settings.Size = Me.Size
        My.Settings.RecentROMs = ListToString(RecentROMs.Items)
    End Sub

    Private Sub FileOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileOpen.Click, OpenTool.Click
        If OpenROM.ShowDialog = DialogResult.OK Then
            RecentROMs.Add(OpenROM.FileName)
            LoadROM(OpenROM.FileName)
        End If
    End Sub

    Private Sub RecentROMs_ItemClicked(ByVal sender As Object, ByVal e As ItemClickedEventArgs) Handles RecentROMs.ItemClicked
        LoadROM(e.Text)
    End Sub

    Public Sub LoadROM(ByVal path As String)
        r = New ROM(path)
        If r.failed Then Return
        FileOpenLevel.Enabled = True
        OpenLevelTool.Enabled = True
        EditPasswords.Enabled = True
        OpenLevel.LoadROM(r)
    End Sub

    Private Sub FileOpenLevel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileOpenLevel.Click, OpenLevelTool.Click
        If OpenLevel.ShowDialog = DialogResult.OK Then
            EdControl = Nothing
            LoadingLevel.Start(r, OpenLevel.levelNums, OpenLevel.LevelNames)
            For Each l As Level In LoadingLevel.lvls
                EdControl = New LvlEdCtrl
                updateTab = False
                Dim tp As TabPage = Tabs.AddXPage(l.name)
                tp.Controls.Add(EdControl)
                EdControl.Dock = DockStyle.Fill
                EdControl.LoadLevel(l)
            Next
            If EdControl IsNot Nothing Then
                SetTool(CurTool)
                UpdateEdControl()
                EdControl.UndoMgr = New UndoManager(UndoTool, RedoTool, EdControl)
                updateTab = True
                TSContainer.ContentPanel.BackColor = SystemColors.Control
                For Each item As ToolStripItem In LevelItems
                    item.Enabled = True
                Next
                Tabs.Visible = True
                EdControl.Focus()
            End If
        End If
    End Sub

    Private Sub FileSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FileSave.Click, SaveTool.Click
        r.SaveLevel(EdControl.lvl)
    End Sub

    Private Sub FileExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FileExit.Click
        Me.Close()
    End Sub

    Private Sub EditUndo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditUndo.Click
        UndoTool.PerformButtonClick()
    End Sub

    Private Sub EditRedo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditRedo.Click
        RedoTool.PerformButtonClick()
    End Sub

    Private Sub UndoTool_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles UndoTool.EnabledChanged
        EditUndo.Enabled = UndoTool.Enabled
    End Sub

    Private Sub RedoTool_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RedoTool.EnabledChanged
        EditRedo.Enabled = RedoTool.Enabled
    End Sub

    Private Sub EditCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditCopy.Click, CopyTool.Click
        If CurTool.Copy() Then
            'Copy selected tiles
        End If
    End Sub

    Private Sub EditCut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditCut.Click, CutTool.Click
        If CurTool.Cut() Then
            'Cut selected tiles
        End If
        EdControl.Repaint()
    End Sub

    Private Sub EditPaste_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditPaste.Click, PasteTool.Click
        If CurTool.Paste() Then
            'Paste tiles
        End If
        EdControl.Repaint()
    End Sub

    Private Sub EditSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditSelectAll.Click
        If CurTool Is Nothing Then Return
        If CurTool.SelectAll(True) Then
            SelectAll(True)
        End If
        SetCopy(True)
        EdControl.Repaint()
    End Sub

    Private Sub EditSelectNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditSelectNone.Click
        If CurTool Is Nothing Then Return
        If CurTool.SelectAll(False) Then
            SelectAll(False)
        End If
        SetCopy(False)
        EdControl.Repaint()
    End Sub

    Private Sub EditPasswords_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditPasswords.Click
        PasswordEditor.ShowDialog(Me)
    End Sub

    Private Sub SelectAll(ByVal selected As Boolean)
        For m As Integer = 0 To EdControl.lvl.Height - 1
            For l As Integer = 0 To EdControl.lvl.Width - 1
                EdControl.selection.selectPts(l, m) = selected
            Next
        Next
        EdControl.selection.exists = selected
        EdControl.selection.Refresh()
        EdControl.UpdateSelection()
        SetCopy(selected)
    End Sub

    Private Sub ViewGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewGrid.Click
        UpdateEdControl()
    End Sub

    Private Sub ViewPriority_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewPriority.Click
        UpdateEdControl()
    End Sub

    Private Sub LevelImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LevelImport.Click
        If ImportLevel.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim fs As New IO.FileStream(ImportLevel.FileName, IO.FileMode.Open, IO.FileAccess.Read)
            fs.Seek(14, IO.SeekOrigin.Begin)
            EdControl.lvl = New Level(fs, EdControl.lvl.name, EdControl.lvl.num, True, New IO.FileStream(r.path, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read))
            UpdateEdControl()
        End If
    End Sub

    Private Sub LevelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LevelExport.Click
        If ExportLevel.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim data As Byte() = EdControl.lvl.ToFile()
            Dim fs As New IO.FileStream(ExportLevel.FileName, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read)
            fs.Write(data, 0, data.Length)
        End If
    End Sub

    Private Sub LevelCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LevelCopy.Click
        Clipboard.SetText(Shrd.ToText(EdControl.lvl.ToFile()))
    End Sub

    Private Sub LevelPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LevelPaste.Click
        Dim data As Byte() = Shrd.FromText(Clipboard.GetText)
        Dim fs As New ByteArrayStream(data)
        fs.Seek(14, IO.SeekOrigin.Begin)
        EdControl.lvl = New Level(fs, EdControl.lvl.name, EdControl.lvl.num, True, New IO.FileStream(r.path, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read))
        UpdateEdControl()
    End Sub

    Private Sub LevelSettingsM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LevelSettingsM.Click
        LevelSettings.ShowDialog(Me)
    End Sub

    Private Sub Tools_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles Tools.ItemClicked
        Dim indx As Integer = Tools.Items.IndexOf(e.ClickedItem)
        Dim indx2 As Integer = Tools.Items.IndexOf(BrushTool)
        If indx >= indx2 Then
            indx2 = indx - indx2
            SwitchToTool(ToolsMenu.DropDownItems(indx2), e.ClickedItem, EditingTools(indx2))
        End If
    End Sub

    Private Sub ToolsMenu_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolsMenu.DropDownItemClicked
        Dim indx As Integer = ToolsMenu.DropDownItems.IndexOf(e.ClickedItem)
        Dim indx2 As Integer = indx + Tools.Items.IndexOf(BrushTool)
        SwitchToTool(e.ClickedItem, Tools.Items(indx2), EditingTools(indx))
    End Sub

    Private Sub UncheckTools()
        For Each Item As ToolStripMenuItem In ToolsMenu.DropDownItems
            Item.Checked = False
        Next
        For l As Integer = Tools.Items.IndexOf(BrushTool) To Tools.Items.Count - 1
            CType(Tools.Items(l), ToolStripButton).Checked = False
        Next
    End Sub

    Private Sub SwitchToTool(ByVal item1 As ToolStripMenuItem, ByVal item2 As ToolStripButton, ByVal t As Tool)
        If Not item1.Checked Then
            UncheckTools()
            item1.Checked = True
            item2.Checked = True
            SetTool(t)
        End If
        If EdControl IsNot Nothing Then
            EdControl.Focus()
        End If
    End Sub

    Public Sub SetTool(ByVal t As Tool)
        If EdControl Is Nothing Or t Is Nothing Then Return
        Select Case t.SidePanel
            Case SideContentType.Tiles
                t.TilePicker = EdControl.TilePicker
                EdControl.SetSidePanel(EdControl.TilePicker)
                EdControl.TilePicker.SetAll()
            Case SideContentType.Items
                t.ItemPicker = EdControl.ItemPicker
                EdControl.SetSidePanel(EdControl.ItemPicker)
            Case SideContentType.Victims
                t.VictimPicker = EdControl.VictimPicker
                EdControl.SetSidePanel(EdControl.VictimPicker)
            Case SideContentType.NRMonsters
                t.NRMPicker = EdControl.NRMPicker
                EdControl.SetSidePanel(EdControl.NRMPicker)
            Case SideContentType.Monsters
                t.MonsterPicker = EdControl.MonsterPicker
                EdControl.SetSidePanel(EdControl.MonsterPicker)
            Case SideContentType.BossMonsters
                t.BMonsterPicker = EdControl.BMonsterPicker
                EdControl.SetSidePanel(EdControl.BMonsterPicker)
        End Select
        CurTool = t
        EdControl.t = t
        EdControl.SetStatusText(t.Status)
        t.Refresh()
        EdControl.Repaint()
    End Sub

    Public Sub UpdateEdControl()
        If EdControl Is Nothing Then Return
        EdControl.Grid = ViewGrid.Checked
        EdControl.priority = ViewPriority.Checked
        EdControl.zoom = zoomLevel
        EdControl.UpdateScrollBars()
        EdControl.Focus()
        EdControl.Repaint()
    End Sub

    Public Sub SetCopy(ByVal enabled As Boolean)
        EditCopy.Enabled = enabled
        EditCut.Enabled = enabled
        CopyTool.Enabled = enabled
        CutTool.Enabled = enabled
    End Sub

    Public Sub CheckCopy()
        SetCopy(EdControl.selection.FindVisible())
    End Sub

    Private Sub Tabs_TabSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tabs.TabSelected
        If updateTab Then
            EdControl = Tabs.SelectedTab.Controls(0)
            SetTool(CurTool)
            UpdateEdControl()
        End If
    End Sub

    Private Sub Tabs_TabClosed(ByVal sender As Object, ByVal e As TabEventArgs) Handles Tabs.TabClosed
        For Each t As Tool In EditingTools
            t.RemoveEdCtrl(e.Tab.Controls(0))
        Next
    End Sub

    Private Sub Tabs_TabsClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tabs.TabsClosed
        For Each i As ToolStripItem In LevelItems
            i.Enabled = False
        Next
        Tabs.Visible = False
        TSContainer.ContentPanel.BackColor = SystemColors.AppWorkspace
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim b As New Bitmap(64 * 16, 64 * 16)
        Using g As Graphics = Graphics.FromImage(b)
            For l As Integer = 0 To 255
                g.DrawImage(EdControl.lvl.tileset.images(l), (l Mod 16) * 64, (l \ 16) * 64)
            Next
        End Using
        Clipboard.SetImage(b)
    End Sub

    Private ZoomLevels As Single() = {1.0F, 0.75F, 0.5F}

    Private Sub ViewMenu_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ViewMenu.DropDownItemClicked, Zoom.DropDownItemClicked
        Dim indx As Integer = e.ClickedItem.Owner.Items.IndexOf(e.ClickedItem)
        Dim indx2 As Integer = 0
        Dim viewMenuStart As Integer = ViewMenu.DropDownItems.IndexOf(View100P)
        If e.ClickedItem.Owner.Items Is ViewMenu.DropDownItems Then
            indx2 = viewMenuStart
        End If
        If indx >= indx2 Then
            indx -= indx2
            zoomLevel = ZoomLevels(indx)
            UpdateEdControl()
        End If
        For l As Integer = viewMenuStart To ViewMenu.DropDownItems.Count - 1
            CType(ViewMenu.DropDownItems(l), ToolStripMenuItem).Checked = False
        Next
        For Each i As ToolStripMenuItem In Zoom.DropDownItems
            i.Checked = False
        Next
        CType(ViewMenu.DropDownItems(indx + viewMenuStart), ToolStripMenuItem).Checked = True
        CType(Zoom.DropDownItems(indx), ToolStripMenuItem).Checked = True
    End Sub

    Private Function ListToString(ByVal items As List(Of String)) As String
        Dim str As String = ""
        For Each i As String In items
            str &= i & "|"
        Next
        Return str
    End Function

    Private Function StringToList(ByVal str As String) As List(Of String)
        Dim items As New List(Of String)
        Dim str2 As String = str
        Dim indx As Integer
        Do
            indx = InStr(str2, "|")
            If indx = 0 Then Exit Do
            items.Add(Mid(str2, 1, indx - 1))
            str2 = Mid(str2, indx + 1)
        Loop
        Return items
    End Function
End Class