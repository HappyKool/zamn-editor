<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TitlePageEditor
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.lblEnterText = New System.Windows.Forms.Label()
        Me.txtWord = New System.Windows.Forms.TextBox()
        Me.chkRand = New System.Windows.Forms.CheckBox()
        Me.btnAlignMiddle = New System.Windows.Forms.Button()
        Me.btnAlignCenter = New System.Windows.Forms.Button()
        Me.btnAdd2 = New System.Windows.Forms.Button()
        Me.btnAdd1 = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.nudPlt = New System.Windows.Forms.NumericUpDown()
        Me.lblPalette = New System.Windows.Forms.Label()
        Me.chkPltAll = New System.Windows.Forms.CheckBox()
        Me.TitlePageEdCtrl2 = New ZAMNEditor.TitlePageEdCtrl()
        Me.TitlePageEdCtrl1 = New ZAMNEditor.TitlePageEdCtrl()
        CType(Me.nudPlt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblEnterText
        '
        Me.lblEnterText.AutoSize = True
        Me.lblEnterText.Location = New System.Drawing.Point(76, 277)
        Me.lblEnterText.Name = "lblEnterText"
        Me.lblEnterText.Size = New System.Drawing.Size(59, 13)
        Me.lblEnterText.TabIndex = 2
        Me.lblEnterText.Text = "Enter Text:"
        '
        'txtWord
        '
        Me.txtWord.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtWord.Enabled = False
        Me.txtWord.Location = New System.Drawing.Point(79, 293)
        Me.txtWord.Name = "txtWord"
        Me.txtWord.Size = New System.Drawing.Size(133, 20)
        Me.txtWord.TabIndex = 3
        '
        'chkRand
        '
        Me.chkRand.AutoSize = True
        Me.chkRand.Checked = True
        Me.chkRand.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRand.Location = New System.Drawing.Point(79, 319)
        Me.chkRand.Name = "chkRand"
        Me.chkRand.Size = New System.Drawing.Size(132, 17)
        Me.chkRand.TabIndex = 4
        Me.chkRand.Text = "Randomize characters"
        Me.chkRand.UseVisualStyleBackColor = True
        '
        'btnAlignMiddle
        '
        Me.btnAlignMiddle.Enabled = False
        Me.btnAlignMiddle.Image = Global.ZAMNEditor.My.Resources.Resources.Align_middle
        Me.btnAlignMiddle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAlignMiddle.Location = New System.Drawing.Point(247, 311)
        Me.btnAlignMiddle.Name = "btnAlignMiddle"
        Me.btnAlignMiddle.Size = New System.Drawing.Size(118, 24)
        Me.btnAlignMiddle.TabIndex = 10
        Me.btnAlignMiddle.Text = "Center Vertically  "
        Me.btnAlignMiddle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAlignMiddle.UseVisualStyleBackColor = True
        '
        'btnAlignCenter
        '
        Me.btnAlignCenter.Enabled = False
        Me.btnAlignCenter.Image = Global.ZAMNEditor.My.Resources.Resources.Align_center
        Me.btnAlignCenter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAlignCenter.Location = New System.Drawing.Point(247, 281)
        Me.btnAlignCenter.Name = "btnAlignCenter"
        Me.btnAlignCenter.Size = New System.Drawing.Size(118, 24)
        Me.btnAlignCenter.TabIndex = 9
        Me.btnAlignCenter.Text = "Center Horizontally"
        Me.btnAlignCenter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAlignCenter.UseVisualStyleBackColor = True
        '
        'btnAdd2
        '
        Me.btnAdd2.Image = Global.ZAMNEditor.My.Resources.Resources.Add
        Me.btnAdd2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAdd2.Location = New System.Drawing.Point(450, 242)
        Me.btnAdd2.Name = "btnAdd2"
        Me.btnAdd2.Size = New System.Drawing.Size(80, 23)
        Me.btnAdd2.TabIndex = 8
        Me.btnAdd2.Text = "Add Word"
        Me.btnAdd2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAdd2.UseVisualStyleBackColor = True
        '
        'btnAdd1
        '
        Me.btnAdd1.Image = Global.ZAMNEditor.My.Resources.Resources.Add
        Me.btnAdd1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAdd1.Location = New System.Drawing.Point(12, 242)
        Me.btnAdd1.Name = "btnAdd1"
        Me.btnAdd1.Size = New System.Drawing.Size(80, 23)
        Me.btnAdd1.TabIndex = 7
        Me.btnAdd1.Text = "Add Word"
        Me.btnAdd1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAdd1.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Enabled = False
        Me.btnDelete.Image = Global.ZAMNEditor.My.Resources.Resources.Delete
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(225, 242)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(93, 23)
        Me.btnDelete.TabIndex = 6
        Me.btnDelete.Text = "Delete Word"
        Me.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Enabled = False
        Me.btnRefresh.Image = Global.ZAMNEditor.My.Resources.Resources.Refresh
        Me.btnRefresh.Location = New System.Drawing.Point(217, 315)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(24, 24)
        Me.btnRefresh.TabIndex = 5
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'nudPlt
        '
        Me.nudPlt.Location = New System.Drawing.Point(429, 285)
        Me.nudPlt.Maximum = New Decimal(New Integer() {7, 0, 0, 0})
        Me.nudPlt.Name = "nudPlt"
        Me.nudPlt.Size = New System.Drawing.Size(38, 20)
        Me.nudPlt.TabIndex = 11
        '
        'lblPalette
        '
        Me.lblPalette.AutoSize = True
        Me.lblPalette.Location = New System.Drawing.Point(380, 287)
        Me.lblPalette.Name = "lblPalette"
        Me.lblPalette.Size = New System.Drawing.Size(43, 13)
        Me.lblPalette.TabIndex = 12
        Me.lblPalette.Text = "Palette:"
        '
        'chkPltAll
        '
        Me.chkPltAll.AutoSize = True
        Me.chkPltAll.Checked = True
        Me.chkPltAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPltAll.Location = New System.Drawing.Point(383, 313)
        Me.chkPltAll.Name = "chkPltAll"
        Me.chkPltAll.Size = New System.Drawing.Size(77, 17)
        Me.chkPltAll.TabIndex = 13
        Me.chkPltAll.Text = "Apply to all"
        Me.chkPltAll.UseVisualStyleBackColor = True
        '
        'TitlePageEdCtrl2
        '
        Me.TitlePageEdCtrl2.Location = New System.Drawing.Point(274, 12)
        Me.TitlePageEdCtrl2.Name = "TitlePageEdCtrl2"
        Me.TitlePageEdCtrl2.Size = New System.Drawing.Size(256, 224)
        Me.TitlePageEdCtrl2.TabIndex = 1
        '
        'TitlePageEdCtrl1
        '
        Me.TitlePageEdCtrl1.Location = New System.Drawing.Point(12, 12)
        Me.TitlePageEdCtrl1.Name = "TitlePageEdCtrl1"
        Me.TitlePageEdCtrl1.Size = New System.Drawing.Size(256, 224)
        Me.TitlePageEdCtrl1.TabIndex = 0
        '
        'TitlePageEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(542, 348)
        Me.Controls.Add(Me.chkPltAll)
        Me.Controls.Add(Me.lblPalette)
        Me.Controls.Add(Me.nudPlt)
        Me.Controls.Add(Me.btnAlignMiddle)
        Me.Controls.Add(Me.btnAlignCenter)
        Me.Controls.Add(Me.btnAdd2)
        Me.Controls.Add(Me.btnAdd1)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.chkRand)
        Me.Controls.Add(Me.txtWord)
        Me.Controls.Add(Me.lblEnterText)
        Me.Controls.Add(Me.TitlePageEdCtrl2)
        Me.Controls.Add(Me.TitlePageEdCtrl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TitlePageEditor"
        Me.Text = "Level Title"
        CType(Me.nudPlt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TitlePageEdCtrl1 As ZAMNEditor.TitlePageEdCtrl
    Friend WithEvents TitlePageEdCtrl2 As ZAMNEditor.TitlePageEdCtrl
    Friend WithEvents lblEnterText As System.Windows.Forms.Label
    Friend WithEvents txtWord As System.Windows.Forms.TextBox
    Friend WithEvents chkRand As System.Windows.Forms.CheckBox
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnAdd1 As System.Windows.Forms.Button
    Friend WithEvents btnAdd2 As System.Windows.Forms.Button
    Friend WithEvents btnAlignCenter As System.Windows.Forms.Button
    Friend WithEvents btnAlignMiddle As System.Windows.Forms.Button
    Friend WithEvents nudPlt As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblPalette As System.Windows.Forms.Label
    Friend WithEvents chkPltAll As System.Windows.Forms.CheckBox
End Class
