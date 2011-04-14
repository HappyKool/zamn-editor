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
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnAdd1 = New System.Windows.Forms.Button()
        Me.nudFont = New System.Windows.Forms.NumericUpDown()
        Me.lblFont = New System.Windows.Forms.Label()
        Me.lblX = New System.Windows.Forms.Label()
        Me.lblY = New System.Windows.Forms.Label()
        Me.nudX = New System.Windows.Forms.NumericUpDown()
        Me.nudY = New System.Windows.Forms.NumericUpDown()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.txtWord = New System.Windows.Forms.TextBox()
        Me.btnAdd2 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TitlePageEdCtrl2 = New ZAMNEditor.TitlePageEdCtrl()
        Me.TitlePageEdCtrl1 = New ZAMNEditor.TitlePageEdCtrl()
        CType(Me.nudFont, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudY, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnDelete
        '
        Me.btnDelete.Image = Global.ZAMNEditor.My.Resources.Resources.Delete
        Me.btnDelete.Location = New System.Drawing.Point(0, 81)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(31, 23)
        Me.btnDelete.TabIndex = 20
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnAdd1
        '
        Me.btnAdd1.Image = Global.ZAMNEditor.My.Resources.Resources.Add
        Me.btnAdd1.Location = New System.Drawing.Point(144, 12)
        Me.btnAdd1.Name = "btnAdd1"
        Me.btnAdd1.Size = New System.Drawing.Size(31, 23)
        Me.btnAdd1.TabIndex = 19
        Me.btnAdd1.UseVisualStyleBackColor = True
        '
        'nudFont
        '
        Me.nudFont.Increment = New Decimal(New Integer() {2, 0, 0, 0})
        Me.nudFont.Location = New System.Drawing.Point(92, 55)
        Me.nudFont.Maximum = New Decimal(New Integer() {6, 0, 0, 0})
        Me.nudFont.Name = "nudFont"
        Me.nudFont.Size = New System.Drawing.Size(60, 20)
        Me.nudFont.TabIndex = 18
        '
        'lblFont
        '
        Me.lblFont.AutoSize = True
        Me.lblFont.Location = New System.Drawing.Point(92, 39)
        Me.lblFont.Name = "lblFont"
        Me.lblFont.Size = New System.Drawing.Size(31, 13)
        Me.lblFont.TabIndex = 17
        Me.lblFont.Text = "Font:"
        '
        'lblX
        '
        Me.lblX.AutoSize = True
        Me.lblX.Location = New System.Drawing.Point(3, 31)
        Me.lblX.Name = "lblX"
        Me.lblX.Size = New System.Drawing.Size(17, 13)
        Me.lblX.TabIndex = 16
        Me.lblX.Text = "X:"
        '
        'lblY
        '
        Me.lblY.AutoSize = True
        Me.lblY.Location = New System.Drawing.Point(3, 57)
        Me.lblY.Name = "lblY"
        Me.lblY.Size = New System.Drawing.Size(17, 13)
        Me.lblY.TabIndex = 15
        Me.lblY.Text = "Y:"
        '
        'nudX
        '
        Me.nudX.Location = New System.Drawing.Point(26, 29)
        Me.nudX.Maximum = New Decimal(New Integer() {31, 0, 0, 0})
        Me.nudX.Name = "nudX"
        Me.nudX.Size = New System.Drawing.Size(60, 20)
        Me.nudX.TabIndex = 14
        '
        'nudY
        '
        Me.nudY.Location = New System.Drawing.Point(26, 55)
        Me.nudY.Maximum = New Decimal(New Integer() {27, 0, 0, 0})
        Me.nudY.Name = "nudY"
        Me.nudY.Size = New System.Drawing.Size(60, 20)
        Me.nudY.TabIndex = 13
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(105, 81)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(47, 23)
        Me.btnSave.TabIndex = 12
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'txtWord
        '
        Me.txtWord.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtWord.Location = New System.Drawing.Point(26, 3)
        Me.txtWord.Name = "txtWord"
        Me.txtWord.Size = New System.Drawing.Size(126, 20)
        Me.txtWord.TabIndex = 11
        '
        'btnAdd2
        '
        Me.btnAdd2.Image = Global.ZAMNEditor.My.Resources.Resources.Add
        Me.btnAdd2.Location = New System.Drawing.Point(144, 222)
        Me.btnAdd2.Name = "btnAdd2"
        Me.btnAdd2.Size = New System.Drawing.Size(31, 23)
        Me.btnAdd2.TabIndex = 21
        Me.btnAdd2.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtWord)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.btnDelete)
        Me.Panel1.Controls.Add(Me.nudY)
        Me.Panel1.Controls.Add(Me.nudX)
        Me.Panel1.Controls.Add(Me.nudFont)
        Me.Panel1.Controls.Add(Me.lblY)
        Me.Panel1.Controls.Add(Me.lblFont)
        Me.Panel1.Controls.Add(Me.lblX)
        Me.Panel1.Enabled = False
        Me.Panel1.Location = New System.Drawing.Point(144, 75)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(155, 106)
        Me.Panel1.TabIndex = 22
        '
        'TitlePageEdCtrl2
        '
        Me.TitlePageEdCtrl2.Location = New System.Drawing.Point(12, 132)
        Me.TitlePageEdCtrl2.Name = "TitlePageEdCtrl2"
        Me.TitlePageEdCtrl2.Size = New System.Drawing.Size(126, 114)
        Me.TitlePageEdCtrl2.TabIndex = 1
        '
        'TitlePageEdCtrl1
        '
        Me.TitlePageEdCtrl1.Location = New System.Drawing.Point(12, 12)
        Me.TitlePageEdCtrl1.Name = "TitlePageEdCtrl1"
        Me.TitlePageEdCtrl1.Size = New System.Drawing.Size(126, 114)
        Me.TitlePageEdCtrl1.TabIndex = 0
        '
        'TitlePageEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(311, 257)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnAdd2)
        Me.Controls.Add(Me.btnAdd1)
        Me.Controls.Add(Me.TitlePageEdCtrl2)
        Me.Controls.Add(Me.TitlePageEdCtrl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TitlePageEditor"
        Me.Text = "Title"
        CType(Me.nudFont, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudY, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TitlePageEdCtrl1 As ZAMNEditor.TitlePageEdCtrl
    Friend WithEvents TitlePageEdCtrl2 As ZAMNEditor.TitlePageEdCtrl
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnAdd1 As System.Windows.Forms.Button
    Friend WithEvents nudFont As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblFont As System.Windows.Forms.Label
    Friend WithEvents lblX As System.Windows.Forms.Label
    Friend WithEvents lblY As System.Windows.Forms.Label
    Friend WithEvents nudX As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudY As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtWord As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd2 As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
