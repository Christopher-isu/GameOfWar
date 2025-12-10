<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class WarGUIForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.flpTopButtons = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnStartGame = New System.Windows.Forms.Button()
        Me.btnEndGame = New System.Windows.Forms.Button()
        Me.btnAbout = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.tlpStatus = New System.Windows.Forms.TableLayoutPanel()
        Me.lblWarCount = New System.Windows.Forms.Label()
        Me.lblRoundCount = New System.Windows.Forms.Label()
        Me.lblWarsFought = New System.Windows.Forms.Label()
        Me.lblRoundsPlayed = New System.Windows.Forms.Label()
        Me.lblPlayer2Captured = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblPlayer1Captured = New System.Windows.Forms.Label()
        Me.lblPlayer1Capt = New System.Windows.Forms.Label()
        Me.lblPlayer2Hand = New System.Windows.Forms.Label()
        Me.lblPlayer2Cards = New System.Windows.Forms.Label()
        Me.lblPlayer1Hand = New System.Windows.Forms.Label()
        Me.lblPlayer1Cards = New System.Windows.Forms.Label()
        Me.tlpPlayArea = New System.Windows.Forms.TableLayoutPanel()
        Me.lblGameStatus = New System.Windows.Forms.Label()
        Me.lblRoundWinner = New System.Windows.Forms.Label()
        Me.tlpNextCards = New System.Windows.Forms.TableLayoutPanel()
        Me.lblVs = New System.Windows.Forms.Label()
        Me.lblPlayer2 = New System.Windows.Forms.Label()
        Me.picP1NextCard = New System.Windows.Forms.PictureBox()
        Me.picP2NextCard = New System.Windows.Forms.PictureBox()
        Me.lblPlayer1 = New System.Windows.Forms.Label()
        Me.pnlGameOver = New System.Windows.Forms.Panel()
        Me.btnPlayAgain = New System.Windows.Forms.Button()
        Me.lblP2FinalCount = New System.Windows.Forms.Label()
        Me.lblP1FinalCount = New System.Windows.Forms.Label()
        Me.lblFinalWinner = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.grpP1War = New System.Windows.Forms.GroupBox()
        Me.grpP2War = New System.Windows.Forms.GroupBox()
        Me.btnPlayCardP1 = New System.Windows.Forms.Button()
        Me.btnPlayCardP2 = New System.Windows.Forms.Button()
        Me.flpTopButtons.SuspendLayout()
        Me.tlpStatus.SuspendLayout()
        Me.tlpPlayArea.SuspendLayout()
        Me.tlpNextCards.SuspendLayout()
        CType(Me.picP1NextCard, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picP2NextCard, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlGameOver.SuspendLayout()
        Me.SuspendLayout()
        '
        'flpTopButtons
        '
        Me.flpTopButtons.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.flpTopButtons.Controls.Add(Me.btnStartGame)
        Me.flpTopButtons.Controls.Add(Me.btnEndGame)
        Me.flpTopButtons.Controls.Add(Me.btnAbout)
        Me.flpTopButtons.Controls.Add(Me.btnExit)
        Me.flpTopButtons.Dock = System.Windows.Forms.DockStyle.Top
        Me.flpTopButtons.Location = New System.Drawing.Point(0, 0)
        Me.flpTopButtons.Name = "flpTopButtons"
        Me.flpTopButtons.Padding = New System.Windows.Forms.Padding(10)
        Me.flpTopButtons.Size = New System.Drawing.Size(1330, 60)
        Me.flpTopButtons.TabIndex = 1
        '
        'btnStartGame
        '
        Me.btnStartGame.BackColor = System.Drawing.Color.ForestGreen
        Me.btnStartGame.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStartGame.ForeColor = System.Drawing.Color.White
        Me.btnStartGame.Location = New System.Drawing.Point(13, 13)
        Me.btnStartGame.Name = "btnStartGame"
        Me.btnStartGame.Size = New System.Drawing.Size(160, 40)
        Me.btnStartGame.TabIndex = 0
        Me.btnStartGame.Text = "Start New Game"
        Me.btnStartGame.UseVisualStyleBackColor = False
        '
        'btnEndGame
        '
        Me.btnEndGame.BackColor = System.Drawing.Color.ForestGreen
        Me.btnEndGame.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEndGame.ForeColor = System.Drawing.Color.White
        Me.btnEndGame.Location = New System.Drawing.Point(179, 13)
        Me.btnEndGame.Name = "btnEndGame"
        Me.btnEndGame.Size = New System.Drawing.Size(160, 40)
        Me.btnEndGame.TabIndex = 0
        Me.btnEndGame.Text = "End Game"
        Me.btnEndGame.UseVisualStyleBackColor = False
        '
        'btnAbout
        '
        Me.btnAbout.BackColor = System.Drawing.Color.ForestGreen
        Me.btnAbout.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAbout.ForeColor = System.Drawing.Color.White
        Me.btnAbout.Location = New System.Drawing.Point(345, 13)
        Me.btnAbout.Name = "btnAbout"
        Me.btnAbout.Size = New System.Drawing.Size(160, 40)
        Me.btnAbout.TabIndex = 1
        Me.btnAbout.Text = "About"
        Me.btnAbout.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.ForestGreen
        Me.btnExit.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(511, 13)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(160, 40)
        Me.btnExit.TabIndex = 0
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'tlpStatus
        '
        Me.tlpStatus.ColumnCount = 6
        Me.tlpStatus.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.24409!))
        Me.tlpStatus.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.75591!))
        Me.tlpStatus.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 239.0!))
        Me.tlpStatus.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230.0!))
        Me.tlpStatus.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 219.0!))
        Me.tlpStatus.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230.0!))
        Me.tlpStatus.Controls.Add(Me.lblWarCount, 5, 1)
        Me.tlpStatus.Controls.Add(Me.lblRoundCount, 4, 1)
        Me.tlpStatus.Controls.Add(Me.lblWarsFought, 5, 0)
        Me.tlpStatus.Controls.Add(Me.lblRoundsPlayed, 4, 0)
        Me.tlpStatus.Controls.Add(Me.lblPlayer2Captured, 3, 1)
        Me.tlpStatus.Controls.Add(Me.Label2, 3, 0)
        Me.tlpStatus.Controls.Add(Me.lblPlayer1Captured, 1, 1)
        Me.tlpStatus.Controls.Add(Me.lblPlayer1Capt, 1, 0)
        Me.tlpStatus.Controls.Add(Me.lblPlayer2Hand, 2, 1)
        Me.tlpStatus.Controls.Add(Me.lblPlayer2Cards, 2, 0)
        Me.tlpStatus.Controls.Add(Me.lblPlayer1Hand, 0, 1)
        Me.tlpStatus.Controls.Add(Me.lblPlayer1Cards, 0, 0)
        Me.tlpStatus.Location = New System.Drawing.Point(8, 66)
        Me.tlpStatus.Name = "tlpStatus"
        Me.tlpStatus.RowCount = 2
        Me.tlpStatus.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.13445!))
        Me.tlpStatus.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.86555!))
        Me.tlpStatus.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.tlpStatus.Size = New System.Drawing.Size(1310, 93)
        Me.tlpStatus.TabIndex = 2
        '
        'lblWarCount
        '
        Me.lblWarCount.AutoSize = True
        Me.lblWarCount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblWarCount.Font = New System.Drawing.Font("Consolas", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWarCount.ForeColor = System.Drawing.Color.Tomato
        Me.lblWarCount.Location = New System.Drawing.Point(1082, 43)
        Me.lblWarCount.Margin = New System.Windows.Forms.Padding(3, 10, 3, 3)
        Me.lblWarCount.Name = "lblWarCount"
        Me.lblWarCount.Size = New System.Drawing.Size(225, 47)
        Me.lblWarCount.TabIndex = 18
        Me.lblWarCount.Text = "0"
        Me.lblWarCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRoundCount
        '
        Me.lblRoundCount.AutoSize = True
        Me.lblRoundCount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblRoundCount.Font = New System.Drawing.Font("Consolas", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoundCount.ForeColor = System.Drawing.Color.Tomato
        Me.lblRoundCount.Location = New System.Drawing.Point(863, 43)
        Me.lblRoundCount.Margin = New System.Windows.Forms.Padding(3, 10, 3, 3)
        Me.lblRoundCount.Name = "lblRoundCount"
        Me.lblRoundCount.Size = New System.Drawing.Size(213, 47)
        Me.lblRoundCount.TabIndex = 17
        Me.lblRoundCount.Text = "0"
        Me.lblRoundCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblWarsFought
        '
        Me.lblWarsFought.AutoSize = True
        Me.lblWarsFought.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblWarsFought.ForeColor = System.Drawing.Color.Tomato
        Me.lblWarsFought.Location = New System.Drawing.Point(1082, 10)
        Me.lblWarsFought.Name = "lblWarsFought"
        Me.lblWarsFought.Size = New System.Drawing.Size(225, 23)
        Me.lblWarsFought.TabIndex = 16
        Me.lblWarsFought.Text = "Wars Fought:"
        Me.lblWarsFought.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRoundsPlayed
        '
        Me.lblRoundsPlayed.AutoSize = True
        Me.lblRoundsPlayed.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblRoundsPlayed.ForeColor = System.Drawing.Color.Tomato
        Me.lblRoundsPlayed.Location = New System.Drawing.Point(863, 10)
        Me.lblRoundsPlayed.Name = "lblRoundsPlayed"
        Me.lblRoundsPlayed.Size = New System.Drawing.Size(213, 23)
        Me.lblRoundsPlayed.TabIndex = 15
        Me.lblRoundsPlayed.Text = "Rounds Played:"
        Me.lblRoundsPlayed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPlayer2Captured
        '
        Me.lblPlayer2Captured.AutoSize = True
        Me.lblPlayer2Captured.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPlayer2Captured.Font = New System.Drawing.Font("Consolas", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlayer2Captured.ForeColor = System.Drawing.Color.Orange
        Me.lblPlayer2Captured.Location = New System.Drawing.Point(633, 43)
        Me.lblPlayer2Captured.Margin = New System.Windows.Forms.Padding(3, 10, 3, 3)
        Me.lblPlayer2Captured.Name = "lblPlayer2Captured"
        Me.lblPlayer2Captured.Size = New System.Drawing.Size(224, 47)
        Me.lblPlayer2Captured.TabIndex = 14
        Me.lblPlayer2Captured.Text = "0"
        Me.lblPlayer2Captured.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.ForeColor = System.Drawing.Color.Orange
        Me.Label2.Location = New System.Drawing.Point(633, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(224, 23)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Player 2 Captured:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPlayer1Captured
        '
        Me.lblPlayer1Captured.AutoSize = True
        Me.lblPlayer1Captured.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPlayer1Captured.Font = New System.Drawing.Font("Consolas", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlayer1Captured.ForeColor = System.Drawing.Color.Cyan
        Me.lblPlayer1Captured.Location = New System.Drawing.Point(188, 43)
        Me.lblPlayer1Captured.Margin = New System.Windows.Forms.Padding(3, 10, 3, 3)
        Me.lblPlayer1Captured.Name = "lblPlayer1Captured"
        Me.lblPlayer1Captured.Size = New System.Drawing.Size(200, 47)
        Me.lblPlayer1Captured.TabIndex = 10
        Me.lblPlayer1Captured.Text = "0"
        Me.lblPlayer1Captured.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPlayer1Capt
        '
        Me.lblPlayer1Capt.AutoSize = True
        Me.lblPlayer1Capt.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblPlayer1Capt.ForeColor = System.Drawing.Color.Cyan
        Me.lblPlayer1Capt.Location = New System.Drawing.Point(188, 10)
        Me.lblPlayer1Capt.Name = "lblPlayer1Capt"
        Me.lblPlayer1Capt.Size = New System.Drawing.Size(200, 23)
        Me.lblPlayer1Capt.TabIndex = 9
        Me.lblPlayer1Capt.Text = "Player 1 Captured:"
        Me.lblPlayer1Capt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPlayer2Hand
        '
        Me.lblPlayer2Hand.AutoSize = True
        Me.lblPlayer2Hand.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPlayer2Hand.Font = New System.Drawing.Font("Consolas", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlayer2Hand.ForeColor = System.Drawing.Color.Orange
        Me.lblPlayer2Hand.Location = New System.Drawing.Point(394, 43)
        Me.lblPlayer2Hand.Margin = New System.Windows.Forms.Padding(3, 10, 3, 3)
        Me.lblPlayer2Hand.Name = "lblPlayer2Hand"
        Me.lblPlayer2Hand.Size = New System.Drawing.Size(233, 47)
        Me.lblPlayer2Hand.TabIndex = 8
        Me.lblPlayer2Hand.Text = "26"
        Me.lblPlayer2Hand.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPlayer2Cards
        '
        Me.lblPlayer2Cards.AutoSize = True
        Me.lblPlayer2Cards.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblPlayer2Cards.ForeColor = System.Drawing.Color.Orange
        Me.lblPlayer2Cards.Location = New System.Drawing.Point(394, 10)
        Me.lblPlayer2Cards.Name = "lblPlayer2Cards"
        Me.lblPlayer2Cards.Size = New System.Drawing.Size(233, 23)
        Me.lblPlayer2Cards.TabIndex = 7
        Me.lblPlayer2Cards.Text = "Player 2 Cards:"
        Me.lblPlayer2Cards.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPlayer1Hand
        '
        Me.lblPlayer1Hand.AutoSize = True
        Me.lblPlayer1Hand.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPlayer1Hand.Font = New System.Drawing.Font("Consolas", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlayer1Hand.ForeColor = System.Drawing.Color.Cyan
        Me.lblPlayer1Hand.Location = New System.Drawing.Point(3, 43)
        Me.lblPlayer1Hand.Margin = New System.Windows.Forms.Padding(3, 10, 3, 3)
        Me.lblPlayer1Hand.Name = "lblPlayer1Hand"
        Me.lblPlayer1Hand.Size = New System.Drawing.Size(179, 47)
        Me.lblPlayer1Hand.TabIndex = 4
        Me.lblPlayer1Hand.Text = "26"
        Me.lblPlayer1Hand.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPlayer1Cards
        '
        Me.lblPlayer1Cards.AutoSize = True
        Me.lblPlayer1Cards.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblPlayer1Cards.ForeColor = System.Drawing.Color.Cyan
        Me.lblPlayer1Cards.Location = New System.Drawing.Point(3, 10)
        Me.lblPlayer1Cards.Name = "lblPlayer1Cards"
        Me.lblPlayer1Cards.Size = New System.Drawing.Size(179, 23)
        Me.lblPlayer1Cards.TabIndex = 0
        Me.lblPlayer1Cards.Text = "Player 1 Cards:"
        Me.lblPlayer1Cards.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tlpPlayArea
        '
        Me.tlpPlayArea.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.tlpPlayArea.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpPlayArea.ColumnCount = 9
        Me.tlpPlayArea.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.0!))
        Me.tlpPlayArea.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.0!))
        Me.tlpPlayArea.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.0!))
        Me.tlpPlayArea.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.0!))
        Me.tlpPlayArea.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.0!))
        Me.tlpPlayArea.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.0!))
        Me.tlpPlayArea.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.0!))
        Me.tlpPlayArea.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.0!))
        Me.tlpPlayArea.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.0!))
        Me.tlpPlayArea.Controls.Add(Me.lblGameStatus, 0, 1)
        Me.tlpPlayArea.Controls.Add(Me.lblRoundWinner, 0, 0)
        Me.tlpPlayArea.Location = New System.Drawing.Point(5, 144)
        Me.tlpPlayArea.Name = "tlpPlayArea"
        Me.tlpPlayArea.RowCount = 2
        Me.tlpPlayArea.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.19149!))
        Me.tlpPlayArea.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.80851!))
        Me.tlpPlayArea.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64.0!))
        Me.tlpPlayArea.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpPlayArea.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpPlayArea.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpPlayArea.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpPlayArea.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpPlayArea.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpPlayArea.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpPlayArea.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpPlayArea.Size = New System.Drawing.Size(1310, 198)
        Me.tlpPlayArea.TabIndex = 3
        '
        'lblGameStatus
        '
        Me.lblGameStatus.AutoSize = True
        Me.tlpPlayArea.SetColumnSpan(Me.lblGameStatus, 9)
        Me.lblGameStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblGameStatus.Font = New System.Drawing.Font("Segoe UI", 28.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGameStatus.ForeColor = System.Drawing.Color.Red
        Me.lblGameStatus.Location = New System.Drawing.Point(3, 107)
        Me.lblGameStatus.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblGameStatus.Name = "lblGameStatus"
        Me.lblGameStatus.Size = New System.Drawing.Size(1304, 89)
        Me.lblGameStatus.TabIndex = 11
        Me.lblGameStatus.Text = "status"
        Me.lblGameStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRoundWinner
        '
        Me.lblRoundWinner.AutoSize = True
        Me.tlpPlayArea.SetColumnSpan(Me.lblRoundWinner, 9)
        Me.lblRoundWinner.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblRoundWinner.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoundWinner.ForeColor = System.Drawing.Color.Yellow
        Me.lblRoundWinner.Location = New System.Drawing.Point(3, 2)
        Me.lblRoundWinner.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblRoundWinner.Name = "lblRoundWinner"
        Me.lblRoundWinner.Size = New System.Drawing.Size(1304, 101)
        Me.lblRoundWinner.TabIndex = 10
        Me.lblRoundWinner.Text = "Click 'Start New Game' to begin!"
        Me.lblRoundWinner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tlpNextCards
        '
        Me.tlpNextCards.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.tlpNextCards.ColumnCount = 3
        Me.tlpNextCards.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.tlpNextCards.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.0!))
        Me.tlpNextCards.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.tlpNextCards.Controls.Add(Me.lblVs, 1, 0)
        Me.tlpNextCards.Controls.Add(Me.lblPlayer2, 2, 1)
        Me.tlpNextCards.Controls.Add(Me.picP1NextCard, 0, 0)
        Me.tlpNextCards.Controls.Add(Me.picP2NextCard, 2, 0)
        Me.tlpNextCards.Controls.Add(Me.lblPlayer1, 0, 1)
        Me.tlpNextCards.Location = New System.Drawing.Point(437, 411)
        Me.tlpNextCards.Name = "tlpNextCards"
        Me.tlpNextCards.RowCount = 2
        Me.tlpNextCards.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpNextCards.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59.0!))
        Me.tlpNextCards.Size = New System.Drawing.Size(449, 314)
        Me.tlpNextCards.TabIndex = 4
        '
        'lblVs
        '
        Me.lblVs.AutoSize = True
        Me.lblVs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblVs.Font = New System.Drawing.Font("Segoe UI", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVs.ForeColor = System.Drawing.Color.Gold
        Me.lblVs.Location = New System.Drawing.Point(151, 5)
        Me.lblVs.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.lblVs.Name = "lblVs"
        Me.lblVs.Size = New System.Drawing.Size(146, 245)
        Me.lblVs.TabIndex = 9
        Me.lblVs.Text = "VS"
        Me.lblVs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPlayer2
        '
        Me.lblPlayer2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblPlayer2.AutoEllipsis = True
        Me.lblPlayer2.AutoSize = True
        Me.lblPlayer2.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlayer2.ForeColor = System.Drawing.Color.Orange
        Me.lblPlayer2.Location = New System.Drawing.Point(348, 264)
        Me.lblPlayer2.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.lblPlayer2.Name = "lblPlayer2"
        Me.lblPlayer2.Size = New System.Drawing.Size(53, 41)
        Me.lblPlayer2.TabIndex = 5
        Me.lblPlayer2.Text = "P2"
        Me.lblPlayer2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'picP1NextCard
        '
        Me.picP1NextCard.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picP1NextCard.Location = New System.Drawing.Point(3, 3)
        Me.picP1NextCard.Name = "picP1NextCard"
        Me.picP1NextCard.Size = New System.Drawing.Size(142, 249)
        Me.picP1NextCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picP1NextCard.TabIndex = 0
        Me.picP1NextCard.TabStop = False
        '
        'picP2NextCard
        '
        Me.picP2NextCard.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picP2NextCard.Location = New System.Drawing.Point(303, 3)
        Me.picP2NextCard.Name = "picP2NextCard"
        Me.picP2NextCard.Size = New System.Drawing.Size(143, 249)
        Me.picP2NextCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picP2NextCard.TabIndex = 1
        Me.picP2NextCard.TabStop = False
        '
        'lblPlayer1
        '
        Me.lblPlayer1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblPlayer1.AutoSize = True
        Me.lblPlayer1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlayer1.ForeColor = System.Drawing.Color.Cyan
        Me.lblPlayer1.Location = New System.Drawing.Point(47, 264)
        Me.lblPlayer1.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.lblPlayer1.Name = "lblPlayer1"
        Me.lblPlayer1.Size = New System.Drawing.Size(53, 41)
        Me.lblPlayer1.TabIndex = 2
        Me.lblPlayer1.Text = "P1"
        Me.lblPlayer1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlGameOver
        '
        Me.pnlGameOver.BackColor = System.Drawing.Color.Black
        Me.pnlGameOver.Controls.Add(Me.btnPlayAgain)
        Me.pnlGameOver.Controls.Add(Me.lblP2FinalCount)
        Me.pnlGameOver.Controls.Add(Me.lblP1FinalCount)
        Me.pnlGameOver.Controls.Add(Me.lblFinalWinner)
        Me.pnlGameOver.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlGameOver.Location = New System.Drawing.Point(0, 60)
        Me.pnlGameOver.Name = "pnlGameOver"
        Me.pnlGameOver.Size = New System.Drawing.Size(1330, 793)
        Me.pnlGameOver.TabIndex = 5
        Me.pnlGameOver.Visible = False
        '
        'btnPlayAgain
        '
        Me.btnPlayAgain.BackColor = System.Drawing.Color.DimGray
        Me.btnPlayAgain.ForeColor = System.Drawing.Color.White
        Me.btnPlayAgain.Location = New System.Drawing.Point(656, 477)
        Me.btnPlayAgain.Name = "btnPlayAgain"
        Me.btnPlayAgain.Size = New System.Drawing.Size(149, 65)
        Me.btnPlayAgain.TabIndex = 3
        Me.btnPlayAgain.Text = "Play Again"
        Me.btnPlayAgain.UseVisualStyleBackColor = False
        '
        'lblP2FinalCount
        '
        Me.lblP2FinalCount.AutoSize = True
        Me.lblP2FinalCount.Font = New System.Drawing.Font("Segoe UI", 19.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblP2FinalCount.ForeColor = System.Drawing.Color.Orange
        Me.lblP2FinalCount.Location = New System.Drawing.Point(656, 388)
        Me.lblP2FinalCount.Name = "lblP2FinalCount"
        Me.lblP2FinalCount.Size = New System.Drawing.Size(145, 45)
        Me.lblP2FinalCount.TabIndex = 2
        Me.lblP2FinalCount.Text = "Countp2"
        '
        'lblP1FinalCount
        '
        Me.lblP1FinalCount.AutoSize = True
        Me.lblP1FinalCount.Font = New System.Drawing.Font("Segoe UI", 19.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblP1FinalCount.ForeColor = System.Drawing.Color.Cyan
        Me.lblP1FinalCount.Location = New System.Drawing.Point(656, 322)
        Me.lblP1FinalCount.Name = "lblP1FinalCount"
        Me.lblP1FinalCount.Size = New System.Drawing.Size(145, 45)
        Me.lblP1FinalCount.TabIndex = 2
        Me.lblP1FinalCount.Text = "Countp1"
        '
        'lblFinalWinner
        '
        Me.lblFinalWinner.AutoSize = True
        Me.lblFinalWinner.Font = New System.Drawing.Font("Segoe UI", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFinalWinner.ForeColor = System.Drawing.Color.Gold
        Me.lblFinalWinner.Location = New System.Drawing.Point(600, 211)
        Me.lblFinalWinner.Name = "lblFinalWinner"
        Me.lblFinalWinner.Size = New System.Drawing.Size(240, 81)
        Me.lblFinalWinner.TabIndex = 1
        Me.lblFinalWinner.Text = "Winner"
        '
        'Timer1
        '
        Me.Timer1.Interval = 500
        '
        'grpP1War
        '
        Me.grpP1War.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpP1War.ForeColor = System.Drawing.Color.White
        Me.grpP1War.Location = New System.Drawing.Point(75, 348)
        Me.grpP1War.Name = "grpP1War"
        Me.grpP1War.Size = New System.Drawing.Size(264, 377)
        Me.grpP1War.TabIndex = 6
        Me.grpP1War.TabStop = False
        Me.grpP1War.Text = "War - P1"
        '
        'grpP2War
        '
        Me.grpP2War.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpP2War.ForeColor = System.Drawing.Color.White
        Me.grpP2War.Location = New System.Drawing.Point(914, 348)
        Me.grpP2War.Name = "grpP2War"
        Me.grpP2War.Size = New System.Drawing.Size(264, 377)
        Me.grpP2War.TabIndex = 7
        Me.grpP2War.TabStop = False
        Me.grpP2War.Text = "War - P2"
        '
        'btnPlayCardP1
        '
        Me.btnPlayCardP1.BackColor = System.Drawing.Color.ForestGreen
        Me.btnPlayCardP1.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPlayCardP1.Location = New System.Drawing.Point(437, 358)
        Me.btnPlayCardP1.Name = "btnPlayCardP1"
        Me.btnPlayCardP1.Size = New System.Drawing.Size(145, 38)
        Me.btnPlayCardP1.TabIndex = 8
        Me.btnPlayCardP1.Text = "Play Card"
        Me.btnPlayCardP1.UseVisualStyleBackColor = False
        '
        'btnPlayCardP2
        '
        Me.btnPlayCardP2.BackColor = System.Drawing.Color.ForestGreen
        Me.btnPlayCardP2.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPlayCardP2.Location = New System.Drawing.Point(738, 358)
        Me.btnPlayCardP2.Name = "btnPlayCardP2"
        Me.btnPlayCardP2.Size = New System.Drawing.Size(145, 38)
        Me.btnPlayCardP2.TabIndex = 9
        Me.btnPlayCardP2.Text = "Play Card"
        Me.btnPlayCardP2.UseVisualStyleBackColor = False
        '
        'WarGUIForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1330, 853)
        Me.Controls.Add(Me.pnlGameOver)
        Me.Controls.Add(Me.btnPlayCardP2)
        Me.Controls.Add(Me.btnPlayCardP1)
        Me.Controls.Add(Me.grpP2War)
        Me.Controls.Add(Me.grpP1War)
        Me.Controls.Add(Me.tlpNextCards)
        Me.Controls.Add(Me.tlpPlayArea)
        Me.Controls.Add(Me.tlpStatus)
        Me.Controls.Add(Me.flpTopButtons)
        Me.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MinimizeBox = False
        Me.Name = "WarGUIForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Game of War"
        Me.flpTopButtons.ResumeLayout(False)
        Me.tlpStatus.ResumeLayout(False)
        Me.tlpStatus.PerformLayout()
        Me.tlpPlayArea.ResumeLayout(False)
        Me.tlpPlayArea.PerformLayout()
        Me.tlpNextCards.ResumeLayout(False)
        Me.tlpNextCards.PerformLayout()
        CType(Me.picP1NextCard, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picP2NextCard, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlGameOver.ResumeLayout(False)
        Me.pnlGameOver.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents flpTopButtons As FlowLayoutPanel
    Friend WithEvents btnStartGame As Button
    Friend WithEvents btnEndGame As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents tlpStatus As TableLayoutPanel
    Friend WithEvents lblPlayer1Cards As Label
    Friend WithEvents lblPlayer1Hand As Label
    Friend WithEvents lblPlayer1Captured As Label
    Friend WithEvents lblPlayer1Capt As Label
    Friend WithEvents lblPlayer2Hand As Label
    Friend WithEvents lblPlayer2Cards As Label
    Friend WithEvents lblWarsFought As Label
    Friend WithEvents lblRoundsPlayed As Label
    Friend WithEvents lblPlayer2Captured As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lblWarCount As Label
    Friend WithEvents lblRoundCount As Label
    Friend WithEvents tlpPlayArea As TableLayoutPanel
    Friend WithEvents tlpNextCards As TableLayoutPanel
    Friend WithEvents picP1NextCard As PictureBox
    Friend WithEvents picP2NextCard As PictureBox
    Friend WithEvents lblPlayer1 As Label
    Friend WithEvents lblPlayer2 As Label
    Friend WithEvents lblRoundWinner As Label
    Friend WithEvents pnlGameOver As Panel
    Friend WithEvents lblP2FinalCount As Label
    Friend WithEvents lblP1FinalCount As Label
    Friend WithEvents lblFinalWinner As Label
    Friend WithEvents btnPlayAgain As Button
    Friend WithEvents Timer1 As Timer
    Friend WithEvents grpP1War As GroupBox
    Friend WithEvents grpP2War As GroupBox
    Friend WithEvents lblVs As Label
    Friend WithEvents lblGameStatus As Label
    Friend WithEvents btnPlayCardP1 As Button
    Friend WithEvents btnPlayCardP2 As Button
    Friend WithEvents btnAbout As Button
End Class
