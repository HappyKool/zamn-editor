﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LvlEdCtrl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.VScrl = New System.Windows.Forms.VScrollBar
        Me.HScrl = New System.Windows.Forms.HScrollBar
        Me.canvas = New System.Windows.Forms.PictureBox
        Me.SideContent = New System.Windows.Forms.Panel
        Me.BorderTimer = New System.Windows.Forms.Timer(Me.components)
        Me.DragTimer = New System.Windows.Forms.Timer(Me.components)
        CType(Me.canvas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'VScrl
        '
        Me.VScrl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VScrl.LargeChange = 8
        Me.VScrl.Location = New System.Drawing.Point(533, 0)
        Me.VScrl.Maximum = 8
        Me.VScrl.Name = "VScrl"
        Me.VScrl.Size = New System.Drawing.Size(17, 383)
        Me.VScrl.SmallChange = 8
        Me.VScrl.TabIndex = 0
        '
        'HScrl
        '
        Me.HScrl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HScrl.LargeChange = 8
        Me.HScrl.Location = New System.Drawing.Point(133, 383)
        Me.HScrl.Maximum = 8
        Me.HScrl.Name = "HScrl"
        Me.HScrl.Size = New System.Drawing.Size(400, 17)
        Me.HScrl.SmallChange = 8
        Me.HScrl.TabIndex = 1
        '
        'canvas
        '
        Me.canvas.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.canvas.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.canvas.Location = New System.Drawing.Point(133, 0)
        Me.canvas.Name = "canvas"
        Me.canvas.Size = New System.Drawing.Size(400, 383)
        Me.canvas.TabIndex = 2
        Me.canvas.TabStop = False
        '
        'SideContent
        '
        Me.SideContent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SideContent.Location = New System.Drawing.Point(0, 0)
        Me.SideContent.Name = "SideContent"
        Me.SideContent.Size = New System.Drawing.Size(133, 400)
        Me.SideContent.TabIndex = 3
        '
        'BorderTimer
        '
        Me.BorderTimer.Enabled = True
        '
        'DragTimer
        '
        Me.DragTimer.Interval = 20
        '
        'LvlEdCtrl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SideContent)
        Me.Controls.Add(Me.canvas)
        Me.Controls.Add(Me.HScrl)
        Me.Controls.Add(Me.VScrl)
        Me.DoubleBuffered = True
        Me.Name = "LvlEdCtrl"
        Me.Size = New System.Drawing.Size(550, 400)
        CType(Me.canvas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents VScrl As System.Windows.Forms.VScrollBar
    Friend WithEvents HScrl As System.Windows.Forms.HScrollBar
    Friend WithEvents canvas As System.Windows.Forms.PictureBox
    Friend WithEvents SideContent As System.Windows.Forms.Panel
    Friend WithEvents BorderTimer As System.Windows.Forms.Timer
    Friend WithEvents DragTimer As System.Windows.Forms.Timer

End Class