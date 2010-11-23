<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LevelSettings
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
        Me.grpTileset = New System.Windows.Forms.GroupBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.radUnkAuto = New System.Windows.Forms.RadioButton()
        Me.radUnkMan = New System.Windows.Forms.RadioButton()
        Me.nudUnk = New System.Windows.Forms.NumericUpDown()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.radColAuto = New System.Windows.Forms.RadioButton()
        Me.radColMan = New System.Windows.Forms.RadioButton()
        Me.addrCol = New ZAMNEditor.AddressUpDown()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.radGFXAuto = New System.Windows.Forms.RadioButton()
        Me.radGFXMan = New System.Windows.Forms.RadioButton()
        Me.addrGFX = New ZAMNEditor.AddressUpDown()
        Me.pnlPal = New System.Windows.Forms.Panel()
        Me.radPalAuto = New System.Windows.Forms.RadioButton()
        Me.cboPal = New System.Windows.Forms.ComboBox()
        Me.radPalMan = New System.Windows.Forms.RadioButton()
        Me.addrPal = New ZAMNEditor.AddressUpDown()
        Me.pnlTiles = New System.Windows.Forms.Panel()
        Me.cboTiles = New System.Windows.Forms.ComboBox()
        Me.radTilesAuto = New System.Windows.Forms.RadioButton()
        Me.radTilesMan = New System.Windows.Forms.RadioButton()
        Me.addrTiles = New ZAMNEditor.AddressUpDown()
        Me.lblUnknown = New System.Windows.Forms.Label()
        Me.lblCollision = New System.Windows.Forms.Label()
        Me.lblGraphics = New System.Windows.Forms.Label()
        Me.lblPalette = New System.Windows.Forms.Label()
        Me.lblTiles = New System.Windows.Forms.Label()
        Me.grpOther = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.nudUnk3 = New System.Windows.Forms.NumericUpDown()
        Me.radUnk3Auto = New System.Windows.Forms.RadioButton()
        Me.radUnk3Man = New System.Windows.Forms.RadioButton()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.radBossAuto = New System.Windows.Forms.RadioButton()
        Me.cboBoss = New System.Windows.Forms.ComboBox()
        Me.radBossMan = New System.Windows.Forms.RadioButton()
        Me.addrBoss = New ZAMNEditor.AddressUpDown()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.cboMusic = New System.Windows.Forms.ComboBox()
        Me.radMusicMan = New System.Windows.Forms.RadioButton()
        Me.nudMusic = New System.Windows.Forms.NumericUpDown()
        Me.radMusicAuto = New System.Windows.Forms.RadioButton()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.cboPltAnim = New System.Windows.Forms.ComboBox()
        Me.radPAnimAuto = New System.Windows.Forms.RadioButton()
        Me.radPAnimMan = New System.Windows.Forms.RadioButton()
        Me.addrPAnim = New ZAMNEditor.AddressUpDown()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.radSPalAuto = New System.Windows.Forms.RadioButton()
        Me.radSPalMan = New System.Windows.Forms.RadioButton()
        Me.addrSPal = New ZAMNEditor.AddressUpDown()
        Me.lblBoss = New System.Windows.Forms.Label()
        Me.lblMusic = New System.Windows.Forms.Label()
        Me.lblUnknown3 = New System.Windows.Forms.Label()
        Me.lblPalAnim = New System.Windows.Forms.Label()
        Me.lblSpritePal = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.nudUnk2 = New System.Windows.Forms.NumericUpDown()
        Me.radUnk2Auto = New System.Windows.Forms.RadioButton()
        Me.radUnk2Man = New System.Windows.Forms.RadioButton()
        Me.lblUnknown2 = New System.Windows.Forms.Label()
        Me.grpTileset.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.nudUnk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlPal.SuspendLayout()
        Me.pnlTiles.SuspendLayout()
        Me.grpOther.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.nudUnk3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel8.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.nudMusic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel9.SuspendLayout()
        CType(Me.nudUnk2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpTileset
        '
        Me.grpTileset.Controls.Add(Me.Panel4)
        Me.grpTileset.Controls.Add(Me.Panel3)
        Me.grpTileset.Controls.Add(Me.Panel2)
        Me.grpTileset.Controls.Add(Me.pnlPal)
        Me.grpTileset.Controls.Add(Me.pnlTiles)
        Me.grpTileset.Controls.Add(Me.lblUnknown)
        Me.grpTileset.Controls.Add(Me.lblCollision)
        Me.grpTileset.Controls.Add(Me.lblGraphics)
        Me.grpTileset.Controls.Add(Me.lblPalette)
        Me.grpTileset.Controls.Add(Me.lblTiles)
        Me.grpTileset.Location = New System.Drawing.Point(12, 12)
        Me.grpTileset.Name = "grpTileset"
        Me.grpTileset.Size = New System.Drawing.Size(378, 165)
        Me.grpTileset.TabIndex = 0
        Me.grpTileset.TabStop = False
        Me.grpTileset.Text = "Tileset"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.radUnkAuto)
        Me.Panel4.Controls.Add(Me.radUnkMan)
        Me.Panel4.Controls.Add(Me.nudUnk)
        Me.Panel4.Location = New System.Drawing.Point(65, 134)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(307, 21)
        Me.Panel4.TabIndex = 5
        '
        'radUnkAuto
        '
        Me.radUnkAuto.AutoSize = True
        Me.radUnkAuto.Location = New System.Drawing.Point(3, 3)
        Me.radUnkAuto.Name = "radUnkAuto"
        Me.radUnkAuto.Size = New System.Drawing.Size(72, 17)
        Me.radUnkAuto.TabIndex = 19
        Me.radUnkAuto.TabStop = True
        Me.radUnkAuto.Text = "Automatic"
        Me.radUnkAuto.UseVisualStyleBackColor = True
        '
        'radUnkMan
        '
        Me.radUnkMan.AutoSize = True
        Me.radUnkMan.Location = New System.Drawing.Point(142, 5)
        Me.radUnkMan.Name = "radUnkMan"
        Me.radUnkMan.Size = New System.Drawing.Size(14, 13)
        Me.radUnkMan.TabIndex = 20
        Me.radUnkMan.TabStop = True
        Me.radUnkMan.UseVisualStyleBackColor = True
        '
        'nudUnk
        '
        Me.nudUnk.Hexadecimal = True
        Me.nudUnk.Location = New System.Drawing.Point(184, 0)
        Me.nudUnk.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nudUnk.Name = "nudUnk"
        Me.nudUnk.Size = New System.Drawing.Size(77, 20)
        Me.nudUnk.TabIndex = 21
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.radColAuto)
        Me.Panel3.Controls.Add(Me.radColMan)
        Me.Panel3.Controls.Add(Me.addrCol)
        Me.Panel3.Location = New System.Drawing.Point(65, 107)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(307, 21)
        Me.Panel3.TabIndex = 5
        '
        'radColAuto
        '
        Me.radColAuto.AutoSize = True
        Me.radColAuto.Location = New System.Drawing.Point(3, 3)
        Me.radColAuto.Name = "radColAuto"
        Me.radColAuto.Size = New System.Drawing.Size(72, 17)
        Me.radColAuto.TabIndex = 15
        Me.radColAuto.TabStop = True
        Me.radColAuto.Text = "Automatic"
        Me.radColAuto.UseVisualStyleBackColor = True
        '
        'radColMan
        '
        Me.radColMan.AutoSize = True
        Me.radColMan.Location = New System.Drawing.Point(142, 3)
        Me.radColMan.Name = "radColMan"
        Me.radColMan.Size = New System.Drawing.Size(14, 13)
        Me.radColMan.TabIndex = 16
        Me.radColMan.TabStop = True
        Me.radColMan.UseVisualStyleBackColor = True
        '
        'addrCol
        '
        Me.addrCol.Location = New System.Drawing.Point(162, 0)
        Me.addrCol.Name = "addrCol"
        Me.addrCol.Size = New System.Drawing.Size(147, 20)
        Me.addrCol.TabIndex = 17
        Me.addrCol.Value = 512
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.radGFXAuto)
        Me.Panel2.Controls.Add(Me.radGFXMan)
        Me.Panel2.Controls.Add(Me.addrGFX)
        Me.Panel2.Location = New System.Drawing.Point(65, 80)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(307, 21)
        Me.Panel2.TabIndex = 5
        '
        'radGFXAuto
        '
        Me.radGFXAuto.AutoSize = True
        Me.radGFXAuto.Location = New System.Drawing.Point(3, 3)
        Me.radGFXAuto.Name = "radGFXAuto"
        Me.radGFXAuto.Size = New System.Drawing.Size(72, 17)
        Me.radGFXAuto.TabIndex = 10
        Me.radGFXAuto.TabStop = True
        Me.radGFXAuto.Text = "Automatic"
        Me.radGFXAuto.UseVisualStyleBackColor = True
        '
        'radGFXMan
        '
        Me.radGFXMan.AutoSize = True
        Me.radGFXMan.Location = New System.Drawing.Point(142, 3)
        Me.radGFXMan.Name = "radGFXMan"
        Me.radGFXMan.Size = New System.Drawing.Size(14, 13)
        Me.radGFXMan.TabIndex = 12
        Me.radGFXMan.TabStop = True
        Me.radGFXMan.UseVisualStyleBackColor = True
        '
        'addrGFX
        '
        Me.addrGFX.Location = New System.Drawing.Point(162, 0)
        Me.addrGFX.Name = "addrGFX"
        Me.addrGFX.Size = New System.Drawing.Size(147, 20)
        Me.addrGFX.TabIndex = 13
        Me.addrGFX.Value = 512
        '
        'pnlPal
        '
        Me.pnlPal.Controls.Add(Me.radPalAuto)
        Me.pnlPal.Controls.Add(Me.cboPal)
        Me.pnlPal.Controls.Add(Me.radPalMan)
        Me.pnlPal.Controls.Add(Me.addrPal)
        Me.pnlPal.Location = New System.Drawing.Point(65, 53)
        Me.pnlPal.Name = "pnlPal"
        Me.pnlPal.Size = New System.Drawing.Size(307, 21)
        Me.pnlPal.TabIndex = 5
        '
        'radPalAuto
        '
        Me.radPalAuto.AutoSize = True
        Me.radPalAuto.Location = New System.Drawing.Point(3, 3)
        Me.radPalAuto.Name = "radPalAuto"
        Me.radPalAuto.Size = New System.Drawing.Size(14, 13)
        Me.radPalAuto.TabIndex = 5
        Me.radPalAuto.TabStop = True
        Me.radPalAuto.UseVisualStyleBackColor = True
        '
        'cboPal
        '
        Me.cboPal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPal.FormattingEnabled = True
        Me.cboPal.Location = New System.Drawing.Point(23, 0)
        Me.cboPal.Name = "cboPal"
        Me.cboPal.Size = New System.Drawing.Size(103, 21)
        Me.cboPal.TabIndex = 6
        '
        'radPalMan
        '
        Me.radPalMan.AutoSize = True
        Me.radPalMan.Location = New System.Drawing.Point(142, 3)
        Me.radPalMan.Name = "radPalMan"
        Me.radPalMan.Size = New System.Drawing.Size(14, 13)
        Me.radPalMan.TabIndex = 7
        Me.radPalMan.TabStop = True
        Me.radPalMan.UseVisualStyleBackColor = True
        '
        'addrPal
        '
        Me.addrPal.Location = New System.Drawing.Point(162, 0)
        Me.addrPal.Name = "addrPal"
        Me.addrPal.Size = New System.Drawing.Size(147, 20)
        Me.addrPal.TabIndex = 8
        Me.addrPal.Value = 512
        '
        'pnlTiles
        '
        Me.pnlTiles.Controls.Add(Me.cboTiles)
        Me.pnlTiles.Controls.Add(Me.radTilesAuto)
        Me.pnlTiles.Controls.Add(Me.radTilesMan)
        Me.pnlTiles.Controls.Add(Me.addrTiles)
        Me.pnlTiles.Location = New System.Drawing.Point(65, 26)
        Me.pnlTiles.Name = "pnlTiles"
        Me.pnlTiles.Size = New System.Drawing.Size(307, 21)
        Me.pnlTiles.TabIndex = 2
        '
        'cboTiles
        '
        Me.cboTiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTiles.FormattingEnabled = True
        Me.cboTiles.Items.AddRange(New Object() {"Grass", "Mall", "Castle", "Office", "Beach/Pyramid"})
        Me.cboTiles.Location = New System.Drawing.Point(23, 0)
        Me.cboTiles.Name = "cboTiles"
        Me.cboTiles.Size = New System.Drawing.Size(103, 21)
        Me.cboTiles.TabIndex = 2
        '
        'radTilesAuto
        '
        Me.radTilesAuto.AutoSize = True
        Me.radTilesAuto.Location = New System.Drawing.Point(3, 3)
        Me.radTilesAuto.Name = "radTilesAuto"
        Me.radTilesAuto.Size = New System.Drawing.Size(14, 13)
        Me.radTilesAuto.TabIndex = 1
        Me.radTilesAuto.TabStop = True
        Me.radTilesAuto.UseVisualStyleBackColor = True
        '
        'radTilesMan
        '
        Me.radTilesMan.AutoSize = True
        Me.radTilesMan.Location = New System.Drawing.Point(142, 3)
        Me.radTilesMan.Name = "radTilesMan"
        Me.radTilesMan.Size = New System.Drawing.Size(14, 13)
        Me.radTilesMan.TabIndex = 3
        Me.radTilesMan.TabStop = True
        Me.radTilesMan.UseVisualStyleBackColor = True
        '
        'addrTiles
        '
        Me.addrTiles.Location = New System.Drawing.Point(162, 0)
        Me.addrTiles.Name = "addrTiles"
        Me.addrTiles.Size = New System.Drawing.Size(147, 20)
        Me.addrTiles.TabIndex = 1
        Me.addrTiles.Value = 512
        '
        'lblUnknown
        '
        Me.lblUnknown.AutoSize = True
        Me.lblUnknown.Location = New System.Drawing.Point(6, 139)
        Me.lblUnknown.Name = "lblUnknown"
        Me.lblUnknown.Size = New System.Drawing.Size(56, 13)
        Me.lblUnknown.TabIndex = 18
        Me.lblUnknown.Text = "Unknown:"
        '
        'lblCollision
        '
        Me.lblCollision.AutoSize = True
        Me.lblCollision.Location = New System.Drawing.Point(6, 112)
        Me.lblCollision.Name = "lblCollision"
        Me.lblCollision.Size = New System.Drawing.Size(48, 13)
        Me.lblCollision.TabIndex = 14
        Me.lblCollision.Text = "Collision:"
        '
        'lblGraphics
        '
        Me.lblGraphics.AutoSize = True
        Me.lblGraphics.Location = New System.Drawing.Point(6, 85)
        Me.lblGraphics.Name = "lblGraphics"
        Me.lblGraphics.Size = New System.Drawing.Size(52, 13)
        Me.lblGraphics.TabIndex = 9
        Me.lblGraphics.Text = "Graphics:"
        '
        'lblPalette
        '
        Me.lblPalette.AutoSize = True
        Me.lblPalette.Location = New System.Drawing.Point(6, 56)
        Me.lblPalette.Name = "lblPalette"
        Me.lblPalette.Size = New System.Drawing.Size(43, 13)
        Me.lblPalette.TabIndex = 4
        Me.lblPalette.Text = "Palette:"
        '
        'lblTiles
        '
        Me.lblTiles.AutoSize = True
        Me.lblTiles.Location = New System.Drawing.Point(6, 29)
        Me.lblTiles.Name = "lblTiles"
        Me.lblTiles.Size = New System.Drawing.Size(32, 13)
        Me.lblTiles.TabIndex = 1
        Me.lblTiles.Text = "Tiles:"
        '
        'grpOther
        '
        Me.grpOther.Controls.Add(Me.Panel9)
        Me.grpOther.Controls.Add(Me.lblUnknown2)
        Me.grpOther.Controls.Add(Me.Panel1)
        Me.grpOther.Controls.Add(Me.Panel8)
        Me.grpOther.Controls.Add(Me.Panel7)
        Me.grpOther.Controls.Add(Me.Panel6)
        Me.grpOther.Controls.Add(Me.Panel5)
        Me.grpOther.Controls.Add(Me.lblBoss)
        Me.grpOther.Controls.Add(Me.lblMusic)
        Me.grpOther.Controls.Add(Me.lblUnknown3)
        Me.grpOther.Controls.Add(Me.lblPalAnim)
        Me.grpOther.Controls.Add(Me.lblSpritePal)
        Me.grpOther.Location = New System.Drawing.Point(12, 183)
        Me.grpOther.Name = "grpOther"
        Me.grpOther.Size = New System.Drawing.Size(378, 210)
        Me.grpOther.TabIndex = 1
        Me.grpOther.TabStop = False
        Me.grpOther.Text = "Other"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.nudUnk3)
        Me.Panel1.Controls.Add(Me.radUnk3Auto)
        Me.Panel1.Controls.Add(Me.radUnk3Man)
        Me.Panel1.Location = New System.Drawing.Point(65, 180)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(307, 21)
        Me.Panel1.TabIndex = 4
        '
        'nudUnk3
        '
        Me.nudUnk3.Hexadecimal = True
        Me.nudUnk3.Location = New System.Drawing.Point(184, 0)
        Me.nudUnk3.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nudUnk3.Name = "nudUnk3"
        Me.nudUnk3.Size = New System.Drawing.Size(77, 20)
        Me.nudUnk3.TabIndex = 36
        '
        'radUnk3Auto
        '
        Me.radUnk3Auto.AutoSize = True
        Me.radUnk3Auto.Location = New System.Drawing.Point(3, 3)
        Me.radUnk3Auto.Name = "radUnk3Auto"
        Me.radUnk3Auto.Size = New System.Drawing.Size(72, 17)
        Me.radUnk3Auto.TabIndex = 27
        Me.radUnk3Auto.TabStop = True
        Me.radUnk3Auto.Text = "Automatic"
        Me.radUnk3Auto.UseVisualStyleBackColor = True
        '
        'radUnk3Man
        '
        Me.radUnk3Man.AutoSize = True
        Me.radUnk3Man.Location = New System.Drawing.Point(142, 3)
        Me.radUnk3Man.Name = "radUnk3Man"
        Me.radUnk3Man.Size = New System.Drawing.Size(14, 13)
        Me.radUnk3Man.TabIndex = 28
        Me.radUnk3Man.TabStop = True
        Me.radUnk3Man.UseVisualStyleBackColor = True
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.radBossAuto)
        Me.Panel8.Controls.Add(Me.cboBoss)
        Me.Panel8.Controls.Add(Me.radBossMan)
        Me.Panel8.Controls.Add(Me.addrBoss)
        Me.Panel8.Location = New System.Drawing.Point(65, 126)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(307, 21)
        Me.Panel8.TabIndex = 5
        '
        'radBossAuto
        '
        Me.radBossAuto.AutoSize = True
        Me.radBossAuto.Location = New System.Drawing.Point(3, 3)
        Me.radBossAuto.Name = "radBossAuto"
        Me.radBossAuto.Size = New System.Drawing.Size(14, 13)
        Me.radBossAuto.TabIndex = 23
        Me.radBossAuto.TabStop = True
        Me.radBossAuto.UseVisualStyleBackColor = True
        '
        'cboBoss
        '
        Me.cboBoss.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBoss.FormattingEnabled = True
        Me.cboBoss.Items.AddRange(New Object() {"Nothing", "UFO", "Giant Baby", "Desert Snakeoid", "Grass Snakeoid", "Gets Dark", "Unknown", "Giant Spider"})
        Me.cboBoss.Location = New System.Drawing.Point(23, 0)
        Me.cboBoss.Name = "cboBoss"
        Me.cboBoss.Size = New System.Drawing.Size(103, 21)
        Me.cboBoss.TabIndex = 25
        '
        'radBossMan
        '
        Me.radBossMan.AutoSize = True
        Me.radBossMan.Location = New System.Drawing.Point(142, 3)
        Me.radBossMan.Name = "radBossMan"
        Me.radBossMan.Size = New System.Drawing.Size(14, 13)
        Me.radBossMan.TabIndex = 26
        Me.radBossMan.TabStop = True
        Me.radBossMan.UseVisualStyleBackColor = True
        '
        'addrBoss
        '
        Me.addrBoss.Location = New System.Drawing.Point(162, 0)
        Me.addrBoss.Name = "addrBoss"
        Me.addrBoss.Size = New System.Drawing.Size(147, 20)
        Me.addrBoss.TabIndex = 24
        Me.addrBoss.Value = 512
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.cboMusic)
        Me.Panel7.Controls.Add(Me.radMusicMan)
        Me.Panel7.Controls.Add(Me.nudMusic)
        Me.Panel7.Controls.Add(Me.radMusicAuto)
        Me.Panel7.Location = New System.Drawing.Point(65, 99)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(307, 21)
        Me.Panel7.TabIndex = 5
        '
        'cboMusic
        '
        Me.cboMusic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMusic.FormattingEnabled = True
        Me.cboMusic.Items.AddRange(New Object() {"Evening of the Undead", "Title Screen", "Pyramid", "Terror in Aisle 5", "Zombie Panic", "Chainsaw Mayhem", "Mars Needs Cheerleaders", "Dr. Tongue's Castle", "Unused Song", "Titanic Toddler"})
        Me.cboMusic.Location = New System.Drawing.Point(23, 0)
        Me.cboMusic.Name = "cboMusic"
        Me.cboMusic.Size = New System.Drawing.Size(103, 21)
        Me.cboMusic.TabIndex = 35
        '
        'radMusicMan
        '
        Me.radMusicMan.AutoSize = True
        Me.radMusicMan.Location = New System.Drawing.Point(142, 3)
        Me.radMusicMan.Name = "radMusicMan"
        Me.radMusicMan.Size = New System.Drawing.Size(14, 13)
        Me.radMusicMan.TabIndex = 32
        Me.radMusicMan.TabStop = True
        Me.radMusicMan.UseVisualStyleBackColor = True
        '
        'nudMusic
        '
        Me.nudMusic.Hexadecimal = True
        Me.nudMusic.Location = New System.Drawing.Point(184, 1)
        Me.nudMusic.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nudMusic.Name = "nudMusic"
        Me.nudMusic.Size = New System.Drawing.Size(77, 20)
        Me.nudMusic.TabIndex = 33
        '
        'radMusicAuto
        '
        Me.radMusicAuto.AutoSize = True
        Me.radMusicAuto.Location = New System.Drawing.Point(3, 3)
        Me.radMusicAuto.Name = "radMusicAuto"
        Me.radMusicAuto.Size = New System.Drawing.Size(14, 13)
        Me.radMusicAuto.TabIndex = 34
        Me.radMusicAuto.TabStop = True
        Me.radMusicAuto.UseVisualStyleBackColor = True
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.cboPltAnim)
        Me.Panel6.Controls.Add(Me.radPAnimAuto)
        Me.Panel6.Controls.Add(Me.radPAnimMan)
        Me.Panel6.Controls.Add(Me.addrPAnim)
        Me.Panel6.Location = New System.Drawing.Point(65, 63)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(307, 21)
        Me.Panel6.TabIndex = 5
        '
        'cboPltAnim
        '
        Me.cboPltAnim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPltAnim.FormattingEnabled = True
        Me.cboPltAnim.Items.AddRange(New Object() {"None", "Castle", "Grass", "Beach", "Pyramid"})
        Me.cboPltAnim.Location = New System.Drawing.Point(23, 0)
        Me.cboPltAnim.Name = "cboPltAnim"
        Me.cboPltAnim.Size = New System.Drawing.Size(103, 21)
        Me.cboPltAnim.TabIndex = 9
        '
        'radPAnimAuto
        '
        Me.radPAnimAuto.AutoSize = True
        Me.radPAnimAuto.Location = New System.Drawing.Point(3, 3)
        Me.radPAnimAuto.Name = "radPAnimAuto"
        Me.radPAnimAuto.Size = New System.Drawing.Size(14, 13)
        Me.radPAnimAuto.TabIndex = 23
        Me.radPAnimAuto.TabStop = True
        Me.radPAnimAuto.UseVisualStyleBackColor = True
        '
        'radPAnimMan
        '
        Me.radPAnimMan.AutoSize = True
        Me.radPAnimMan.Location = New System.Drawing.Point(142, 3)
        Me.radPAnimMan.Name = "radPAnimMan"
        Me.radPAnimMan.Size = New System.Drawing.Size(14, 13)
        Me.radPAnimMan.TabIndex = 24
        Me.radPAnimMan.TabStop = True
        Me.radPAnimMan.UseVisualStyleBackColor = True
        '
        'addrPAnim
        '
        Me.addrPAnim.Location = New System.Drawing.Point(162, 0)
        Me.addrPAnim.Name = "addrPAnim"
        Me.addrPAnim.Size = New System.Drawing.Size(147, 20)
        Me.addrPAnim.TabIndex = 25
        Me.addrPAnim.Value = 512
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.radSPalAuto)
        Me.Panel5.Controls.Add(Me.radSPalMan)
        Me.Panel5.Controls.Add(Me.addrSPal)
        Me.Panel5.Location = New System.Drawing.Point(65, 26)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(307, 21)
        Me.Panel5.TabIndex = 5
        '
        'radSPalAuto
        '
        Me.radSPalAuto.AutoSize = True
        Me.radSPalAuto.Location = New System.Drawing.Point(3, 3)
        Me.radSPalAuto.Name = "radSPalAuto"
        Me.radSPalAuto.Size = New System.Drawing.Size(72, 17)
        Me.radSPalAuto.TabIndex = 19
        Me.radSPalAuto.TabStop = True
        Me.radSPalAuto.Text = "Automatic"
        Me.radSPalAuto.UseVisualStyleBackColor = True
        '
        'radSPalMan
        '
        Me.radSPalMan.AutoSize = True
        Me.radSPalMan.Location = New System.Drawing.Point(142, 3)
        Me.radSPalMan.Name = "radSPalMan"
        Me.radSPalMan.Size = New System.Drawing.Size(14, 13)
        Me.radSPalMan.TabIndex = 20
        Me.radSPalMan.TabStop = True
        Me.radSPalMan.UseVisualStyleBackColor = True
        '
        'addrSPal
        '
        Me.addrSPal.Location = New System.Drawing.Point(162, 0)
        Me.addrSPal.Name = "addrSPal"
        Me.addrSPal.Size = New System.Drawing.Size(147, 20)
        Me.addrSPal.TabIndex = 21
        Me.addrSPal.Value = 512
        '
        'lblBoss
        '
        Me.lblBoss.AutoSize = True
        Me.lblBoss.Location = New System.Drawing.Point(6, 129)
        Me.lblBoss.Name = "lblBoss"
        Me.lblBoss.Size = New System.Drawing.Size(33, 13)
        Me.lblBoss.TabIndex = 22
        Me.lblBoss.Text = "Boss:"
        '
        'lblMusic
        '
        Me.lblMusic.AutoSize = True
        Me.lblMusic.Location = New System.Drawing.Point(6, 102)
        Me.lblMusic.Name = "lblMusic"
        Me.lblMusic.Size = New System.Drawing.Size(38, 13)
        Me.lblMusic.TabIndex = 30
        Me.lblMusic.Text = "Music:"
        '
        'lblUnknown3
        '
        Me.lblUnknown3.AutoSize = True
        Me.lblUnknown3.Location = New System.Drawing.Point(6, 185)
        Me.lblUnknown3.Name = "lblUnknown3"
        Me.lblUnknown3.Size = New System.Drawing.Size(56, 13)
        Me.lblUnknown3.TabIndex = 26
        Me.lblUnknown3.Text = "Unknown:"
        '
        'lblPalAnim
        '
        Me.lblPalAnim.AutoSize = True
        Me.lblPalAnim.Location = New System.Drawing.Point(6, 63)
        Me.lblPalAnim.Name = "lblPalAnim"
        Me.lblPalAnim.Size = New System.Drawing.Size(56, 26)
        Me.lblPalAnim.TabIndex = 22
        Me.lblPalAnim.Text = "Palette" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Animation:"
        '
        'lblSpritePal
        '
        Me.lblSpritePal.AutoSize = True
        Me.lblSpritePal.Location = New System.Drawing.Point(6, 26)
        Me.lblSpritePal.Name = "lblSpritePal"
        Me.lblSpritePal.Size = New System.Drawing.Size(43, 26)
        Me.lblSpritePal.TabIndex = 18
        Me.lblSpritePal.Text = "Sprite" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Palette:"
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.nudUnk2)
        Me.Panel9.Controls.Add(Me.radUnk2Auto)
        Me.Panel9.Controls.Add(Me.radUnk2Man)
        Me.Panel9.Location = New System.Drawing.Point(65, 153)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(307, 21)
        Me.Panel9.TabIndex = 37
        '
        'nudUnk2
        '
        Me.nudUnk2.Hexadecimal = True
        Me.nudUnk2.Location = New System.Drawing.Point(184, 0)
        Me.nudUnk2.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nudUnk2.Name = "nudUnk2"
        Me.nudUnk2.Size = New System.Drawing.Size(77, 20)
        Me.nudUnk2.TabIndex = 36
        '
        'radUnk2Auto
        '
        Me.radUnk2Auto.AutoSize = True
        Me.radUnk2Auto.Location = New System.Drawing.Point(3, 3)
        Me.radUnk2Auto.Name = "radUnk2Auto"
        Me.radUnk2Auto.Size = New System.Drawing.Size(72, 17)
        Me.radUnk2Auto.TabIndex = 27
        Me.radUnk2Auto.TabStop = True
        Me.radUnk2Auto.Text = "Automatic"
        Me.radUnk2Auto.UseVisualStyleBackColor = True
        '
        'radUnk2Man
        '
        Me.radUnk2Man.AutoSize = True
        Me.radUnk2Man.Location = New System.Drawing.Point(142, 3)
        Me.radUnk2Man.Name = "radUnk2Man"
        Me.radUnk2Man.Size = New System.Drawing.Size(14, 13)
        Me.radUnk2Man.TabIndex = 28
        Me.radUnk2Man.TabStop = True
        Me.radUnk2Man.UseVisualStyleBackColor = True
        '
        'lblUnknown2
        '
        Me.lblUnknown2.AutoSize = True
        Me.lblUnknown2.Location = New System.Drawing.Point(6, 158)
        Me.lblUnknown2.Name = "lblUnknown2"
        Me.lblUnknown2.Size = New System.Drawing.Size(56, 13)
        Me.lblUnknown2.TabIndex = 38
        Me.lblUnknown2.Text = "Unknown:"
        '
        'LevelSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(404, 405)
        Me.Controls.Add(Me.grpOther)
        Me.Controls.Add(Me.grpTileset)
        Me.Name = "LevelSettings"
        Me.Text = "Level Settings"
        Me.grpTileset.ResumeLayout(False)
        Me.grpTileset.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.nudUnk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlPal.ResumeLayout(False)
        Me.pnlPal.PerformLayout()
        Me.pnlTiles.ResumeLayout(False)
        Me.pnlTiles.PerformLayout()
        Me.grpOther.ResumeLayout(False)
        Me.grpOther.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.nudUnk3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.nudMusic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        CType(Me.nudUnk2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpTileset As System.Windows.Forms.GroupBox
    Friend WithEvents lblTiles As System.Windows.Forms.Label
    Friend WithEvents radTilesMan As System.Windows.Forms.RadioButton
    Friend WithEvents cboTiles As System.Windows.Forms.ComboBox
    Friend WithEvents radTilesAuto As System.Windows.Forms.RadioButton
    Friend WithEvents addrCol As ZAMNEditor.AddressUpDown
    Friend WithEvents radColMan As System.Windows.Forms.RadioButton
    Friend WithEvents radColAuto As System.Windows.Forms.RadioButton
    Friend WithEvents lblCollision As System.Windows.Forms.Label
    Friend WithEvents addrGFX As ZAMNEditor.AddressUpDown
    Friend WithEvents radGFXMan As System.Windows.Forms.RadioButton
    Friend WithEvents radGFXAuto As System.Windows.Forms.RadioButton
    Friend WithEvents lblGraphics As System.Windows.Forms.Label
    Friend WithEvents addrPal As ZAMNEditor.AddressUpDown
    Friend WithEvents radPalMan As System.Windows.Forms.RadioButton
    Friend WithEvents cboPal As System.Windows.Forms.ComboBox
    Friend WithEvents radPalAuto As System.Windows.Forms.RadioButton
    Friend WithEvents lblPalette As System.Windows.Forms.Label
    Friend WithEvents addrTiles As ZAMNEditor.AddressUpDown
    Friend WithEvents nudUnk As System.Windows.Forms.NumericUpDown
    Friend WithEvents radUnkMan As System.Windows.Forms.RadioButton
    Friend WithEvents radUnkAuto As System.Windows.Forms.RadioButton
    Friend WithEvents lblUnknown As System.Windows.Forms.Label
    Friend WithEvents grpOther As System.Windows.Forms.GroupBox
    Friend WithEvents cboMusic As System.Windows.Forms.ComboBox
    Friend WithEvents radMusicAuto As System.Windows.Forms.RadioButton
    Friend WithEvents nudMusic As System.Windows.Forms.NumericUpDown
    Friend WithEvents radMusicMan As System.Windows.Forms.RadioButton
    Friend WithEvents lblMusic As System.Windows.Forms.Label
    Friend WithEvents radUnk3Man As System.Windows.Forms.RadioButton
    Friend WithEvents radUnk3Auto As System.Windows.Forms.RadioButton
    Friend WithEvents lblUnknown3 As System.Windows.Forms.Label
    Friend WithEvents addrPAnim As ZAMNEditor.AddressUpDown
    Friend WithEvents radPAnimMan As System.Windows.Forms.RadioButton
    Friend WithEvents radPAnimAuto As System.Windows.Forms.RadioButton
    Friend WithEvents lblPalAnim As System.Windows.Forms.Label
    Friend WithEvents addrSPal As ZAMNEditor.AddressUpDown
    Friend WithEvents radSPalMan As System.Windows.Forms.RadioButton
    Friend WithEvents radSPalAuto As System.Windows.Forms.RadioButton
    Friend WithEvents lblSpritePal As System.Windows.Forms.Label
    Friend WithEvents addrBoss As ZAMNEditor.AddressUpDown
    Friend WithEvents radBossMan As System.Windows.Forms.RadioButton
    Friend WithEvents cboBoss As System.Windows.Forms.ComboBox
    Friend WithEvents radBossAuto As System.Windows.Forms.RadioButton
    Friend WithEvents lblBoss As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pnlPal As System.Windows.Forms.Panel
    Friend WithEvents pnlTiles As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents cboPltAnim As System.Windows.Forms.ComboBox
    Friend WithEvents nudUnk3 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents nudUnk2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents radUnk2Auto As System.Windows.Forms.RadioButton
    Friend WithEvents radUnk2Man As System.Windows.Forms.RadioButton
    Friend WithEvents lblUnknown2 As System.Windows.Forms.Label
End Class
