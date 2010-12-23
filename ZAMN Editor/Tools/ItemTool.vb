Public Class ItemTool
    Inherits Tool

    Public selectedItems As List(Of Item)
    Public selectedItem As Item
    Public allSelectedItems As New Dictionary(Of LvlEdCtrl, List(Of Item))
    Public XStart As Integer
    Public YStart As Integer
    Public width As Integer
    Public height As Integer
    Public removing As Boolean
    Public created As Boolean

    Private curSelItems As New List(Of Item)
    Private selecting As Boolean = False
    Private curX As Integer
    Private curY As Integer
    Private dragXOff As Integer
    Private dragYOff As Integer
    Private borderPen As New Pen(Color.Black)
    Private darkBrush As New SolidBrush(Color.FromArgb(92, 0, 0, 0))

    Private Const DefaultText As String = "Click to select or move items. Hold shift to add to selection or alt to remove. Hold ctrl to create a new item."
    Private Const ShiftText As String = "Click to add to the selection."
    Private Const CtrlText As String = "Click to add a new item or click an existing item to clone it."
    Private Const AltText As String = "Click to remove from the selection."
    Private Const MoveText As String = "Moved items {0}, {1}. Hold shift to snap to 8x8 grid."

    Public Sub New(ByVal ed As Editor)
        MyBase.New(ed)
        Me.SidePanel = SideContentType.Items
        Me.Status = DefaultText
        borderPen.DashPattern = New Single() {4, 4}
    End Sub

    Public Overrides Sub Refresh()
        If Not allSelectedItems.ContainsKey(ed.EdControl) Then
            allSelectedItems.Add(ed.EdControl, New List(Of Item))
        End If
        selectedItems = allSelectedItems(ed.EdControl)
    End Sub

    Public Overrides Sub MouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim additem As Boolean = False
        For l As Integer = ed.EdControl.lvl.items.Count - 1 To 0 Step -1 'Find item under mouse
            Dim i As Item = ed.EdControl.lvl.items(l)
            If i.GetRect.Contains(e.Location) Then
                If Control.ModifierKeys = Keys.Alt Then
                    selectedItems.Remove(i)
                    selecting = False
                    selectedItem = Nothing
                    Repaint()
                    Return
                End If
                If Not selectedItems.Contains(i) Then
                    If Control.ModifierKeys <> Keys.Shift Then
                        selectedItems.Clear()
                    End If
                    selectedItems.Add(i)
                End If
                selectedItem = i
                dragXOff = e.X - i.x
                dragYOff = e.Y - i.y
                additem = True
                Exit For
            End If
        Next
        curSelItems.Clear()
        If additem Then 'Move selected items
            XStart = selectedItem.x
            YStart = selectedItem.y
        Else 'Create a selection rectangle
            If Control.ModifierKeys <> Keys.Shift And Control.ModifierKeys <> Keys.Alt Then
                selectedItems.Clear()
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
        selecting = Not additem
        If Control.ModifierKeys = Keys.Control Then 'Add a new item
            If selectedItems.Count = 0 Then
                If ItemPicker.SelectedIndex > -1 Then
                    Dim i As New Item(Math.Max(0, e.X - 8), Math.Max(0, e.Y - 8), ItemPicker.SelectedIndex)
                    selectedItems.Clear()
                    selectedItems.Add(i)
                    selectedItem = i
                    dragXOff = 8
                    dragYOff = 8
                    created = True
                End If
            Else 'Clone the current item
                Dim newItems As New List(Of Item)
                For Each i As Item In selectedItems
                    newItems.Add(New Item(i))
                Next
                selectedItem = newItems(selectedItems.IndexOf(selectedItem))
                selectedItems = newItems
                created = True
            End If
            ed.EdControl.lvl.items.AddRange(selectedItems)
            selecting = False
        End If
        ed.SetCopy(selectedItems.Count > 0 Or curSelItems.Count > 0)
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
                curSelItems.Clear()
                For Each i As Item In ed.EdControl.lvl.items 'Find items in selection rectangle
                    If selRect.IntersectsWith(i.GetRect) Then
                        curSelItems.Add(i)
                    End If
                Next
                ed.SetCopy(selectedItems.Count > 0 Or curSelItems.Count > 0)
            ElseIf selectedItem IsNot Nothing Then
                Dim minX As Integer = Integer.MaxValue
                Dim minY As Integer = Integer.MaxValue
                Dim stp As Integer = 1
                If Control.ModifierKeys = Keys.Shift Then
                    stp = 8
                End If
                For Each i As Item In selectedItems
                    If i.x < minX Then minX = i.x
                    If i.y < minY Then minY = i.y
                Next
                Dim XDelta As Integer = Math.Max(-minX, e.X - (selectedItem.x + dragXOff))
                Dim YDelta As Integer = Math.Max(-minY, e.Y - (selectedItem.y + dragYOff))
                If created Then
                    ed.EdControl.UndoMgr.Perform(New MoveItemAction(selectedItems, XDelta, YDelta, stp))
                Else
                    ed.EdControl.UndoMgr.Do(New MoveItemAction(selectedItems, XDelta, YDelta, stp))
                End If
                Me.Status = String.Format(MoveText, selectedItem.x - XStart, selectedItem.y - YStart)
                UpdateStatus()
            End If
            Repaint()
        End If
    End Sub

    Public Overrides Sub MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        For Each i As Item In curSelItems
            If removing Then
                If selectedItems.Contains(i) Then
                    selectedItems.Remove(i)
                End If
            Else
                If Not selectedItems.Contains(i) Then
                    selectedItems.Add(i)
                End If
            End If
        Next
        If created Then
            For Each i As Item In selectedItems
                ed.EdControl.lvl.items.Remove(i)
            Next
            ed.EdControl.UndoMgr.Do(New AddItemAction(selectedItems))
            created = False
        End If
        curSelItems.Clear()
        selecting = False
        ResetStatus()
        Repaint()
        ed.EdControl.UndoMgr.merge = False
    End Sub

    Public Overrides Sub KeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Delete Then
            ed.EdControl.UndoMgr.Do(New RemoveItemAction(selectedItems))
            selectedItems.Clear()
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
        ElseIf Control.ModifierKeys = Keys.Alt Then
            Me.Status = AltText
        Else
            Me.Status = DefaultText
        End If
        UpdateStatus()
    End Sub

    Public Overrides Sub ItemChanged()
        If selectedItems.Count > 0 Then
            ed.EdControl.UndoMgr.Do(New ChangeItemTypeAction(selectedItems, ItemPicker.SelectedIndex))
            Repaint()
        End If
    End Sub

    Public Overrides Sub Paint(ByVal g As System.Drawing.Graphics)
        If selecting Then
            g.DrawRectangle(Pens.White, curX, curY, width, height)
            g.DrawRectangle(borderPen, curX, curY, width, height)
        End If
        For Each i As Item In selectedItems
            If Not curSelItems.Contains(i) Then
                g.FillRectangle(darkBrush, i.GetRect)
                g.DrawRectangle(Pens.White, i.GetRect)
            End If
        Next
        If Not removing Then
            For Each i As Item In curSelItems
                g.FillRectangle(darkBrush, i.GetRect)
                g.DrawRectangle(Pens.White, i.GetRect)
            Next
        End If
    End Sub

    Public Overrides Sub RemoveEdCtrl(ByVal e As LvlEdCtrl)
        allSelectedItems.Remove(e)
    End Sub

    Public Overrides Function SelectAll(ByVal selected As Boolean) As Boolean
        curSelItems.Clear()
        selecting = False
        selectedItems.Clear()
        selectedItem = Nothing
        If selected Then
            For Each i As Item In ed.EdControl.lvl.items
                selectedItems.Add(i)
            Next
        End If
        Return False
    End Function

    Public Overrides Function Copy() As Boolean
        Clipboard.SetText(ToText(selectedItems))
        Return False
    End Function

    Public Overrides Function Cut() As Boolean
        Copy()
        ed.EdControl.UndoMgr.Do(New RemoveItemAction(selectedItems))
        selectedItems.Clear()
        Return False
    End Function

    Public Overrides Function Paste() As Boolean
        If Not Clipboard.GetText.StartsWith("0") Then Return False
        selectedItems = FromText(Clipboard.GetText)
        Dim MinX As Integer = Integer.MaxValue, MinY As Integer = Integer.MaxValue
        For Each i As Item In selectedItems
            If i.x < MinX Then MinX = i.x
            If i.y < MinY Then MinY = i.y
        Next
        Dim dx As Integer = ed.EdControl.HScrl.Value * ed.zoomLevel - MinX
        Dim dy As Integer = ed.EdControl.VScrl.Value * ed.zoomLevel - MinY
        For Each i As Item In selectedItems
            i.x += dx
            i.y += dy
        Next
        ed.EdControl.UndoMgr.Do(New AddItemAction(selectedItems))
        Return False
    End Function

    Private Function ToText(ByVal items As List(Of Item)) As String
        Dim str As String = "0"
        For Each i As Item In items
            str &= Shrd.HexL(i.x, 4) & Shrd.HexL(i.y, 4) & Shrd.HexL(i.type, 2)
        Next
        Return str
    End Function

    Private Function FromText(ByVal txt As String) As List(Of Item)
        Dim items As New List(Of Item)
        Try
            Dim indx As Integer = 2
            Do Until indx > txt.Length
                items.Add(New Item(CInt("&H" & Mid(txt, indx, 4)), CInt("&H" & Mid(txt, indx + 4, 4)), _
                                   CInt("&H" & Mid(txt, indx + 8, 2))))
                indx += 10
            Loop
        Catch
        End Try
        Return items
    End Function
End Class
