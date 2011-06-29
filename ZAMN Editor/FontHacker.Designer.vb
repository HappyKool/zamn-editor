<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FontHacker
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.start = New System.Windows.Forms.NumericUpDown()
        Me.numOfTiles = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.imgWidth = New System.Windows.Forms.NumericUpDown()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.chkAltImg = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.start, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numOfTiles, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(256, 256)
        Me.Panel1.TabIndex = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.TabIndex = 8
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(274, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Start:"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(274, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Number of tiles:"
        '
        'start
        '
        Me.start.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.start.Hexadecimal = True
        Me.start.Location = New System.Drawing.Point(360, 10)
        Me.start.Maximum = New Decimal(New Integer() {1002000, 0, 0, 0})
        Me.start.Name = "start"
        Me.start.Size = New System.Drawing.Size(81, 20)
        Me.start.TabIndex = 3
        Me.start.Value = New Decimal(New Integer() {743489, 0, 0, 0})
        '
        'numOfTiles
        '
        Me.numOfTiles.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numOfTiles.Hexadecimal = True
        Me.numOfTiles.Location = New System.Drawing.Point(360, 36)
        Me.numOfTiles.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.numOfTiles.Name = "numOfTiles"
        Me.numOfTiles.Size = New System.Drawing.Size(81, 20)
        Me.numOfTiles.TabIndex = 4
        Me.numOfTiles.Value = New Decimal(New Integer() {2303, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(274, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Image Width:"
        '
        'imgWidth
        '
        Me.imgWidth.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.imgWidth.Hexadecimal = True
        Me.imgWidth.Location = New System.Drawing.Point(360, 62)
        Me.imgWidth.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.imgWidth.Name = "imgWidth"
        Me.imgWidth.Size = New System.Drawing.Size(81, 20)
        Me.imgWidth.TabIndex = 6
        Me.imgWidth.Value = New Decimal(New Integer() {128, 0, 0, 0})
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(366, 88)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "View"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'chkAltImg
        '
        Me.chkAltImg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkAltImg.AutoSize = True
        Me.chkAltImg.Location = New System.Drawing.Point(277, 124)
        Me.chkAltImg.Name = "chkAltImg"
        Me.chkAltImg.Size = New System.Drawing.Size(70, 17)
        Me.chkAltImg.TabIndex = 8
        Me.chkAltImg.Text = "Alt Image"
        Me.chkAltImg.UseVisualStyleBackColor = True
        '
        'FontHacker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(453, 284)
        Me.Controls.Add(Me.chkAltImg)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.imgWidth)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.numOfTiles)
        Me.Controls.Add(Me.start)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FontHacker"
        Me.Text = "FontHacker"
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.start, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numOfTiles, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgWidth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents start As System.Windows.Forms.NumericUpDown
    Friend WithEvents numOfTiles As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents imgWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents chkAltImg As System.Windows.Forms.CheckBox
End Class
