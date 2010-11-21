Public Class MonsterTool
    Inherits Tool

    Public selectedMonsters As List(Of Monster)
    Public selectedMonster As Monster
    Public allSelectedMonsters As New Dictionary(Of LvlEdCtrl, List(Of Monster))
    Public XStart As Integer
    Public YStart As Integer
    Public width As Integer
    Public height As Integer

    Private curSelMonsters As New List(Of Monster)
    Private selecting As Boolean = False
    Private curX As Integer
    Private curY As Integer
    Private dragXOff As Integer
    Private dragYOff As Integer
    Private borderPen As New Pen(Color.Black)
    Private darkBrush As New SolidBrush(Color.FromArgb(92, 0, 0, 0))

    Private Const DefaultText As String = "Click to select or move monsters. Hold shift to add to selection. Hold ctrl to create a new monster."
    Private Const ShiftText As String = "Click to add to the selection."
    Private Const CtrlText As String = "Click to add a new monster or click an existing monster to clone it."
    Private Const MoveText As String = "Moved monsters {0}, {1}. Hold shift to snap to 8x8 grid."

    Public Sub New(ByVal ed As Editor)
        MyBase.New(ed)
        Me.SidePanel = SideContentType.Monsters
        Me.Status = DefaultText
        borderPen.DashPattern = New Single() {4, 4}
    End Sub

    Public Overrides Sub Refresh()
        If Not allSelectedMonsters.ContainsKey(ed.EdControl) Then
            allSelectedMonsters.Add(ed.EdControl, New List(Of Monster))
        End If
        selectedMonsters = allSelectedMonsters(ed.EdControl)
    End Sub

    Public Overrides Sub MouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim addmonster As Boolean = False
        For l As Integer = ed.EdControl.lvl.Monsters.Count - 1 To 0 Step -1 'Find monster under mouse
            Dim m As Monster = ed.EdControl.lvl.Monsters(l)
            If m.GetRect.Contains(e.Location) Then
                If Not selectedMonsters.Contains(m) Then
                    If Control.ModifierKeys <> Keys.Shift Then
                        selectedMonsters.Clear()
                    End If
                    selectedMonsters.Add(m)
                End If
                selectedMonster = m
                dragXOff = e.X - m.x
                dragYOff = e.Y - m.y
                addmonster = True
                Exit For
            End If
        Next
        curSelMonsters.Clear()
        If addmonster Then 'Move selected monster
            XStart = selectedMonster.x
            YStart = selectedMonster.y
        Else 'Create a selection rectangle
            If Control.ModifierKeys <> Keys.Shift Then
                selectedMonsters.Clear()
                ed.SetCopy(False)
            End If
            XStart = e.X
            YStart = e.Y
            curX = e.X
            curY = e.Y
            width = 0
            height = 0
        End If
        selecting = Not addmonster
        If Control.ModifierKeys = Keys.Control Then 'Add a new monster
            If selectedMonsters.Count = 0 Then
                If MonsterPicker.SelectedIndex > -1 Then
                    Dim m As New Monster(100, e.X - LevelGFX.VictimImages(MonsterPicker.SelectedIndex).Width / 2, _
                                         e.Y - LevelGFX.VictimImages(MonsterPicker.SelectedIndex).Height / 2, 100, LevelGFX.ptrs(MonsterPicker.SelectedIndex))
                    selectedMonsters.Clear()
                    selectedMonsters.Add(m)
                    selectedMonster = m
                    ed.EdControl.lvl.Monsters.Add(m)
                    dragXOff = 8
                    dragYOff = 8
                End If
            Else 'Clone the current monster
                Dim newMonsters As New List(Of Monster)
                For Each m As Monster In selectedMonsters
                    newMonsters.Add(New Monster(m))
                    ed.EdControl.lvl.Monsters.Add(newMonsters.Last)
                Next
                selectedMonster = newMonsters(selectedMonsters.IndexOf(selectedMonster))
                selectedMonsters = newMonsters
            End If
            selecting = False
        End If
        ed.SetCopy(selectedMonsters.Count > 0 Or curSelMonsters.Count > 0)
        Repaint()
    End Sub

    Public Overrides Sub MouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            If selecting Then
                width = Math.Abs(e.X - XStart)
                height = Math.Abs(e.Y - YStart)
                If e.X < XStart Then
                    curX = XStart - width
                Else
                    curX = XStart
                End If
                If e.Y < YStart Then
                    curY = YStart - height
                Else
                    curY = YStart
                End If
                Dim selRect As New Rectangle(curX, curY, width, height)
                curSelMonsters.Clear()
                For Each m As Monster In ed.EdControl.lvl.Monsters 'Find monsters in selection rectangle
                    If selRect.IntersectsWith(m.GetRect) Then
                        curSelMonsters.Add(m)
                    End If
                Next
                ed.SetCopy(selectedMonsters.Count > 0 Or curSelMonsters.Count > 0)
            ElseIf selectedMonster IsNot Nothing Then
                Dim minX As Integer = Integer.MaxValue
                Dim minY As Integer = Integer.MaxValue
                Dim stp As Integer = 1
                If Control.ModifierKeys = Keys.Shift Then
                    stp = 8
                End If
                For Each m As Monster In selectedMonsters
                    If m.x < minX Then minX = m.x
                    If m.y < minY Then minY = m.y
                Next
                Dim XDelta As Integer = Math.Max(-minX, e.X - (selectedMonster.x + dragXOff))
                Dim YDelta As Integer = Math.Max(-minY, e.Y - (selectedMonster.y + dragYOff))
                For Each m As Monster In selectedMonsters
                    m.x = ((m.x + XDelta) \ stp) * stp
                    m.y = ((m.y + YDelta) \ stp) * stp
                Next
                Me.Status = String.Format(MoveText, selectedMonster.x - XStart, selectedMonster.y - YStart)
                UpdateStatus()
            End If
            Repaint()
        End If
    End Sub

    Public Overrides Sub MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        For Each m As Monster In curSelMonsters
            If Not selectedMonsters.Contains(m) Then
                selectedMonsters.Add(m)
            End If
        Next
        curSelMonsters.Clear()
        selecting = False
        ResetStatus()
        Repaint()
    End Sub

    Public Overrides Sub KeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Delete Then
            For Each m As Monster In selectedMonsters
                ed.EdControl.lvl.Monsters.Remove(m)
            Next
            selectedMonsters.Clear()
            Repaint()
        End If
        ResetStatus()
    End Sub

    Public Overrides Sub KeyUp(ByVal e As System.Windows.Forms.KeyEventArgs)
        ResetStatus()
    End Sub

    Private Sub ResetStatus()
        If Control.MouseButtons = MouseButtons.Left Then Return
        If Control.ModifierKeys = Keys.Shift Then
            Me.Status = ShiftText
        ElseIf Control.ModifierKeys = Keys.Control Then
            Me.Status = CtrlText
        Else
            Me.Status = DefaultText
        End If
        UpdateStatus()
    End Sub

    Public Overrides Sub MonsterChanged()
        For Each m As Monster In selectedMonsters
            m.index = MonsterPicker.SelectedIndex + 1
            m.UpdatePtr()
            Repaint()
        Next
    End Sub

    Public Overrides Sub Paint(ByVal g As System.Drawing.Graphics)
        If selecting Then
            g.DrawRectangle(Pens.White, curX, curY, width, height)
            g.DrawRectangle(borderPen, curX, curY, width, height)
        End If
        For Each m As Monster In selectedMonsters
            g.FillRectangle(darkBrush, m.GetRect)
            g.DrawRectangle(Pens.White, m.GetRect)
        Next
        For Each m As Monster In curSelMonsters
            g.FillRectangle(darkBrush, m.GetRect)
            g.DrawRectangle(Pens.White, m.GetRect)
        Next
    End Sub

    Public Overrides Sub RemoveEdCtrl(ByVal e As LvlEdCtrl)
        allSelectedMonsters.Remove(e)
    End Sub

    Public Overrides Function SelectAll(ByVal selected As Boolean) As Boolean
        curSelMonsters.Clear()
        selecting = False
        selectedMonsters.Clear()
        selectedMonster = Nothing
        If selected Then
            For Each m As Monster In ed.EdControl.lvl.Monsters
                selectedMonsters.Add(m)
            Next
        End If
        Return False
    End Function

    Public Overrides Function Copy() As Boolean
        Clipboard.SetText(ToText(selectedMonsters))
        Return False
    End Function

    Public Overrides Function Cut() As Boolean
        Copy()
        For Each m As Monster In selectedMonsters
            ed.EdControl.lvl.Monsters.Remove(m)
        Next
        Return False
    End Function

    Public Overrides Function Paste() As Boolean
        selectedMonsters = FromText(Clipboard.GetText)
        Dim MinX As Integer = Integer.MaxValue, MinY As Integer = Integer.MaxValue
        For Each m As Monster In selectedMonsters
            ed.EdControl.lvl.Monsters.Add(m)
            If m.x < MinX Then MinX = m.x
            If m.y < MinY Then MinY = m.y
        Next
        Dim dx As Integer = ed.EdControl.HScrl.Value * ed.zoomLevel - MinX
        Dim dy As Integer = ed.EdControl.VScrl.Value * ed.zoomLevel - MinY
        For Each m As Monster In selectedMonsters
            m.x += dx
            m.y += dy
        Next
        Return False
    End Function

    Private Function ToText(ByVal Monsters As List(Of Monster)) As String
        Dim str As String = ""
        For Each m As Monster In Monsters
            str &= Shrd.HexL(m.radius, 2) & Shrd.HexL(m.x, 4) & Shrd.HexL(m.y, 4) & _
                   Shrd.HexL(m.delay, 2) & Shrd.HexL(m.ptr, 8)
        Next
        Return str
    End Function

    Private Function FromText(ByVal txt As String) As List(Of Monster)
        Dim Monsters As New List(Of Monster)
        Try
            Dim indx As Integer = 1
            Do Until indx > txt.Length
                Monsters.Add(New Monster(CInt("&H" & Mid(txt, indx, 2)), CInt("&H" & Mid(txt, indx + 2, 4)), _
                                         CInt("&H" & Mid(txt, indx + 6, 4)), CInt("&H" & Mid(txt, indx + 10, 2)), _
                                         CInt("&H" & Mid(txt, indx + 12, 8))))
                indx += 20
            Loop
        Catch
        End Try
        Return Monsters
    End Function
End Class
