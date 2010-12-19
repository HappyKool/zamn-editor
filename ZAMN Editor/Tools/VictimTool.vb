Public Class VictimTool
    Inherits Tool

    Public selectedVictims As List(Of Victim)
    Public selectedVictim As Victim
    Public allSelectedVictims As New Dictionary(Of LvlEdCtrl, List(Of Victim))
    Public XStart As Integer
    Public YStart As Integer
    Public width As Integer
    Public height As Integer
    Public removing As Boolean

    Private curSelVictims As New List(Of Victim)
    Private selecting As Boolean = False
    Private curX As Integer
    Private curY As Integer
    Private dragXOff As Integer
    Private dragYOff As Integer
    Private borderPen As New Pen(Color.Black)
    Private darkBrush As New SolidBrush(Color.FromArgb(92, 0, 0, 0))

    Private Const DefaultText As String = "Click to select or move victims. Hold shift to add to selection or alt to remove."
    Private Const ShiftText As String = "Click to add to the selection."
    Private Const AltText As String = "Click to remove from the selection."
    Private Const MoveText As String = "Moved victims {0}, {1}. Hold shift to snap to 8x8 grid."

    Public Sub New(ByVal ed As Editor)
        MyBase.New(ed)
        Me.SidePanel = SideContentType.Victims
        Me.Status = DefaultText
        borderPen.DashPattern = New Single() {4, 4}
    End Sub

    Public Overrides Sub Refresh()
        If Not allSelectedVictims.ContainsKey(ed.EdControl) Then
            allSelectedVictims.Add(ed.EdControl, New List(Of Victim))
        End If
        selectedVictims = allSelectedVictims(ed.EdControl)
    End Sub

    Public Overrides Sub MouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim addvictim As Boolean = False
        For l As Integer = ed.EdControl.lvl.victims.Count - 1 To 0 Step -1 'Find victim under mouse
            Dim v As Victim = ed.EdControl.lvl.victims(l)
            If v.GetRect(ed.EdControl.lvl.GFX).Contains(e.Location) Then
                If Control.ModifierKeys = Keys.Alt Then
                    selectedVictims.Remove(v)
                    selecting = False
                    selectedVictim = Nothing
                    Repaint()
                    Return
                End If
                If Not selectedVictims.Contains(v) Then
                    If Control.ModifierKeys <> Keys.Shift Then
                        selectedVictims.Clear()
                    End If
                    selectedVictims.Add(v)
                End If
                selectedVictim = v
                dragXOff = e.X - v.x
                dragYOff = e.Y - v.y
                addvictim = True
                Exit For
            End If
        Next
        curSelVictims.Clear()
        If addvictim Then 'Move selected victims
            XStart = selectedVictim.x
            YStart = selectedVictim.y
        Else 'Create a selection rectangle
            If Control.ModifierKeys <> Keys.Shift And Control.ModifierKeys <> Keys.Alt Then
                selectedVictims.Clear()
                ed.SetCopy(False)
            End If
            XStart = e.X
            YStart = e.Y
            curX = e.X
            curY = e.Y
            width = 0
            height = 0
            removing = (Control.ModifierKeys = Keys.Alt)
        End If
        selecting = Not addvictim
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
                curSelVictims.Clear()
                For Each v As Victim In ed.EdControl.lvl.victims 'Find victims in selection rectangle
                    If selRect.IntersectsWith(v.GetRect(ed.EdControl.lvl.GFX)) Then
                        curSelVictims.Add(v)
                    End If
                Next
            ElseIf selectedVictim IsNot Nothing Then
                Dim minX As Integer = Integer.MaxValue
                Dim minY As Integer = Integer.MaxValue
                Dim stp As Integer = 1
                If Control.ModifierKeys = Keys.Shift Then
                    stp = 8
                End If
                For Each v As Victim In selectedVictims
                    If v.x < minX Then minX = v.x
                    If v.y < minY Then minY = v.y
                Next
                Dim XDelta As Integer = Math.Max(-minX, e.X - (selectedVictim.x + dragXOff))
                Dim YDelta As Integer = Math.Max(-minY, e.Y - (selectedVictim.y + dragYOff))
                For Each v As Victim In selectedVictims
                    v.x = ((v.x + XDelta) \ stp) * stp
                    v.y = ((v.y + YDelta) \ stp) * stp
                Next
                Me.Status = String.Format(MoveText, selectedVictim.x - XStart, selectedVictim.y - YStart)
                UpdateStatus()
            End If
            Repaint()
        End If
    End Sub

    Public Overrides Sub MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        For Each v As Victim In curSelVictims
            If removing Then
                If selectedVictims.Contains(v) Then
                    selectedVictims.Remove(v)
                End If
            Else
                If Not selectedVictims.Contains(v) Then
                    selectedVictims.Add(v)
                End If
            End If
        Next
        curSelVictims.Clear()
        selecting = False
        ResetStatus()
        Repaint()
    End Sub

    Public Overrides Sub KeyUp(ByVal e As System.Windows.Forms.KeyEventArgs)
        ResetStatus()
    End Sub

    Private Sub ResetStatus()
        If Control.MouseButtons = MouseButtons.Left Then Return
        If Control.ModifierKeys = Keys.Shift Then
            Me.Status = ShiftText
        ElseIf Control.ModifierKeys = Keys.Alt Then
            Me.Status = AltText
        Else
            Me.Status = DefaultText
        End If
        UpdateStatus()
    End Sub

    Public Overrides Sub VictimChanged()
        For Each v As Victim In selectedVictims
            If v.ptr > 2 Then
                v.index = VictimPicker.SelectedIndex
                v.UpdatePtr()
            End If
        Next
        Repaint()
    End Sub

    Public Overrides Sub Paint(ByVal g As System.Drawing.Graphics)
        If selecting Then
            g.DrawRectangle(Pens.White, curX, curY, width, height)
            g.DrawRectangle(borderPen, curX, curY, width, height)
        End If
        For Each v As Victim In selectedVictims
            If Not curSelVictims.Contains(v) Then
                g.FillRectangle(darkBrush, v.GetRect(ed.EdControl.lvl.GFX))
                g.DrawRectangle(Pens.White, v.GetRect(ed.EdControl.lvl.GFX))
            End If
        Next
        If Not removing Then
            For Each v As Victim In curSelVictims
                g.FillRectangle(darkBrush, v.GetRect(ed.EdControl.lvl.GFX))
                g.DrawRectangle(Pens.White, v.GetRect(ed.EdControl.lvl.GFX))
            Next
        End If
    End Sub

    Public Overrides Sub RemoveEdCtrl(ByVal e As LvlEdCtrl)
        allSelectedVictims.Remove(e)
    End Sub

    Public Overrides Function SelectAll(ByVal selected As Boolean) As Boolean
        curSelVictims.Clear()
        selecting = False
        selectedVictims.Clear()
        selectedVictim = Nothing
        If selected Then
            For Each v As Victim In ed.EdControl.lvl.victims
                selectedVictims.Add(v)
            Next
        End If
        Return False
    End Function
End Class
