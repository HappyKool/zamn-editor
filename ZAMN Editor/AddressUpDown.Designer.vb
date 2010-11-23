<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddressUpDown
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
        Me.lblDollars = New System.Windows.Forms.Label()
        Me.lblColon = New System.Windows.Forms.Label()
        Me.Bank = New System.Windows.Forms.NumericUpDown()
        Me.Part2 = New System.Windows.Forms.NumericUpDown()
        CType(Me.Bank, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Part2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblDollars
        '
        Me.lblDollars.AutoSize = True
        Me.lblDollars.Location = New System.Drawing.Point(3, 3)
        Me.lblDollars.Name = "lblDollars"
        Me.lblDollars.Size = New System.Drawing.Size(13, 13)
        Me.lblDollars.TabIndex = 1
        Me.lblDollars.Text = "$"
        '
        'lblColon
        '
        Me.lblColon.AutoSize = True
        Me.lblColon.Location = New System.Drawing.Point(70, 3)
        Me.lblColon.Name = "lblColon"
        Me.lblColon.Size = New System.Drawing.Size(10, 13)
        Me.lblColon.TabIndex = 2
        Me.lblColon.Text = ":"
        '
        'Bank
        '
        Me.Bank.Hexadecimal = True
        Me.Bank.Location = New System.Drawing.Point(22, 0)
        Me.Bank.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.Bank.Minimum = New Decimal(New Integer() {128, 0, 0, 0})
        Me.Bank.Name = "Bank"
        Me.Bank.Size = New System.Drawing.Size(42, 20)
        Me.Bank.TabIndex = 3
        Me.Bank.Value = New Decimal(New Integer() {128, 0, 0, 0})
        '
        'Part2
        '
        Me.Part2.Hexadecimal = True
        Me.Part2.Location = New System.Drawing.Point(86, 0)
        Me.Part2.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.Part2.Minimum = New Decimal(New Integer() {32768, 0, 0, 0})
        Me.Part2.Name = "Part2"
        Me.Part2.Size = New System.Drawing.Size(57, 20)
        Me.Part2.TabIndex = 4
        Me.Part2.Value = New Decimal(New Integer() {32768, 0, 0, 0})
        '
        'AddressUpDown
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Part2)
        Me.Controls.Add(Me.Bank)
        Me.Controls.Add(Me.lblColon)
        Me.Controls.Add(Me.lblDollars)
        Me.Name = "AddressUpDown"
        Me.Size = New System.Drawing.Size(147, 20)
        CType(Me.Bank, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Part2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblDollars As System.Windows.Forms.Label
    Friend WithEvents lblColon As System.Windows.Forms.Label
    Friend WithEvents Bank As System.Windows.Forms.NumericUpDown
    Friend WithEvents Part2 As System.Windows.Forms.NumericUpDown

End Class
