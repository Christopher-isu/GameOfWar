'====================================================================
' WarGUIForm.vb – HUMAN vs COMPUTER with FULL WAR ANIMATION
' War cards animate automatically at 500ms intervals with stacking visualization
'====================================================================
Imports System.ComponentModel

Public Class WarGUIForm
    Private Enum GameState
        Idle
        P1Turn
        P2Turn
        Resolving
        WarAnimating
        GameOver
    End Enum

    Private currentState As GameState = GameState.Idle
    Private isGameRunning As Boolean = False
    Private gameEngine As WarGameLogic = Nothing
    Private pendingCard1 As Card = Nothing
    Private pendingCard2 As Card = Nothing
    Private roundCount As Integer = 0
    Private warCount As Integer = 0
    Private warCards1 As New List(Of Card)
    Private warCards2 As New List(Of Card)
    Private warAnimationStep As Integer = 0
    Private allWarCards As New List(Of Card)

    Private hasShownSplash As Boolean = False

    Private Sub WarGUIForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not hasShownSplash Then
            hasShownSplash = True
            Using splash As New SplashScreenForm()
                splash.Show()
                splash.Refresh()
                Threading.Thread.Sleep(2000)
            End Using
        End If

        ResetUIForNewGame()
        btnPlayCardP1.Enabled = False
        btnPlayCardP2.Enabled = False
        btnPlayCardP2.Text = "Computer"
        Timer1.Interval = 500
        Timer1.Stop()
    End Sub

    '====================================================================
    ' Button handlers
    '====================================================================
    Private Sub btnStartGame_Click(sender As Object, e As EventArgs) Handles btnStartGame.Click
        If isGameRunning Then
            MessageBox.Show("A game is already in progress!", "War", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        StartNewGame()
    End Sub

    Private Sub btnEndGame_Click(sender As Object, e As EventArgs) Handles btnEndGame.Click
        If Not isGameRunning Then Return
        If MessageBox.Show("End current game?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            EndCurrentGame()
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnPlayAgain_Click(sender As Object, e As EventArgs) Handles btnPlayAgain.Click
        pnlGameOver.Visible = False
        StartNewGame()
    End Sub

    Private Sub btnPlayCardP1_Click(sender As Object, e As EventArgs) Handles btnPlayCardP1.Click
        If currentState <> GameState.P1Turn OrElse gameEngine Is Nothing Then Return

        Try
            pendingCard1 = gameEngine.PlayPlayerCard(gameEngine.Player1)
            picP1NextCard.Image = pendingCard1.GetImage()
            btnPlayCardP1.Enabled = False
            lblGameStatus.Text = "Computer thinking..."
            lblGameStatus.ForeColor = Color.Orange
            currentState = GameState.P2Turn
            Timer1.Start()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '====================================================================
    ' Timer1 - Master Game Director (Normal + War Animation)
    '====================================================================
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Select Case currentState
            Case GameState.P2Turn
                ComputerPlayTurn()
            Case GameState.Resolving
                ResolveRound()
            Case GameState.WarAnimating
                AnimateWarStep()
        End Select
    End Sub

    Private Sub ComputerPlayTurn()
        Timer1.Stop()
        Try
            pendingCard2 = gameEngine.PlayPlayerCard(gameEngine.Player2)
            picP2NextCard.Image = pendingCard2.GetImage()
            lblGameStatus.Text = "Resolving round..."
            lblGameStatus.ForeColor = Color.Yellow
            currentState = GameState.Resolving
            Timer1.Start()
        Catch ex As Exception
            EndGameDueToNoCards()
        End Try
    End Sub

    Private Sub ResolveRound()
        Timer1.Stop()
        Dim result = gameEngine.CompareCards(pendingCard1, pendingCard2)

        Select Case result
            Case RoundResult.P1Wins
                gameEngine.AwardRound(gameEngine.Player1, New List(Of Card) From {pendingCard1, pendingCard2})
                lblRoundWinner.Text = "You win the round!"
                lblRoundWinner.ForeColor = Color.Cyan
                roundCount += 1
            Case RoundResult.P2Wins
                gameEngine.AwardRound(gameEngine.Player2, New List(Of Card) From {pendingCard1, pendingCard2})
                lblRoundWinner.Text = "Computer wins the round!"
                lblRoundWinner.ForeColor = Color.Orange
                roundCount += 1
            Case RoundResult.Tie
                StartWarAnimation()
                Return
        End Select

        UpdateCounts()
        CheckGameOver()
        If currentState <> GameState.GameOver Then
            PrepareNextTurn()
        End If
    End Sub

    '====================================================================
    ' FULL WAR ANIMATION SYSTEM (500ms steps)
    '====================================================================
    Private Sub StartWarAnimation()
        warCount += 1
        lblGameStatus.Text = "WAR! Laying down cards..."
        lblGameStatus.ForeColor = Color.Red

        ' Collect initial tie cards + 3 face-down + 1 face-up for each player
        Try
            warCards1.Clear()
            warCards2.Clear()
            warCards1.Add(pendingCard1)
            warCards2.Add(pendingCard2)

            ' Add 3 face-down cards each
            For i = 1 To 3
                If gameEngine.Player1.HasCards() Then warCards1.Add(gameEngine.Player1.PlayCard())
                If gameEngine.Player2.HasCards() Then warCards2.Add(gameEngine.Player2.PlayCard())
            Next

            ' Add face-up war cards
            If gameEngine.Player1.HasCards() Then warCards1.Add(gameEngine.Player1.PlayCard())
            If gameEngine.Player2.HasCards() Then warCards2.Add(gameEngine.Player2.PlayCard())

            allWarCards.Clear()
            allWarCards.AddRange(warCards1)
            allWarCards.AddRange(warCards2)

            warAnimationStep = 0
            currentState = GameState.WarAnimating
            Timer1.Start()
        Catch ex As Exception
            CheckGameOver()
        End Try
    End Sub

    Private Sub AnimateWarStep()
        ClearPlayArea()

        Select Case warAnimationStep
            Case 0
                ' Step 1: Show original tie (face-up)
                picP1NextCard.Image = warCards1(0).GetImage()
                picP2NextCard.Image = warCards2(0).GetImage()
                lblRoundWinner.Text = "TIE! Preparing war..."

            Case 1, 2, 3
                ' Steps 2-4: Animate 3 face-down cards each (one per step)
                Dim faceDownCount = warAnimationStep - 1
                DisplayWarCards(warCards1, warCards2, faceDownCount, False, False)
                lblRoundWinner.Text = $"WAR: {faceDownCount}/3 face-down cards..."

            Case 4
                ' Step 5: Show ALL cards - 3 face-down + 1 face-up
                DisplayWarCards(warCards1, warCards2, 3, True, True)
                lblRoundWinner.Text = "Comparing war cards..."

            Case 5
                ' Step 6: Determine winner
                Timer1.Stop()
                Dim p1WarCard = warCards1.Last()
                Dim p2WarCard = warCards2.Last()
                Dim warResult = gameEngine.CompareCards(p1WarCard, p2WarCard)

                DisplayWarCards(warCards1, warCards2, 3, True, True)

                If warResult = RoundResult.P1Wins Then
                    gameEngine.AwardRound(gameEngine.Player1, allWarCards)
                    lblRoundWinner.Text = "YOU WIN THE WAR!"
                    lblRoundWinner.ForeColor = Color.Cyan
                Else
                    gameEngine.AwardRound(gameEngine.Player2, allWarCards)
                    lblRoundWinner.Text = "COMPUTER WINS THE WAR!"
                    lblRoundWinner.ForeColor = Color.Orange
                End If

                roundCount += 1
                UpdateCounts()
                CheckGameOver()
                If currentState <> GameState.GameOver Then
                    PrepareNextTurn()
                End If
                Return
        End Select

        warAnimationStep += 1
        Timer1.Start()
    End Sub

    Private Sub DisplayWarCards(cards1 As List(Of Card), cards2 As List(Of Card),
                               faceDownCount As Integer, showP1FaceUp As Boolean, showP2FaceUp As Boolean)
        ClearPlayArea()

        ' Player 1 war stack
        For i = 0 To faceDownCount - 1
            Dim pb As New PictureBox With {
                .Image = My.Resources.CardBack,
                .Size = New Size(125, 175),
                .Location = New Point(30 + i * 22, 50 + i * 16),
                .SizeMode = PictureBoxSizeMode.StretchImage,
                .Tag = "P1War"
            }
            grpP1War.Controls.Add(pb)
            pb.SendToBack()
        Next

        If showP1FaceUp AndAlso cards1.Count > faceDownCount Then
            Dim p1Top As New PictureBox With {
                .Image = cards1(faceDownCount).GetImage(),
                .Size = New Size(125, 175),
                .Location = New Point(30 + faceDownCount * 22, 50 + faceDownCount * 16),
                .SizeMode = PictureBoxSizeMode.StretchImage,
                .Tag = "P1WarFaceUp"
            }
            grpP1War.Controls.Add(p1Top)
            p1Top.BringToFront()
        End If

        ' Player 2 war stack (mirror)
        For i = 0 To faceDownCount - 1
            Dim pb As New PictureBox With {
                .Image = My.Resources.CardBack,
                .Size = New Size(125, 175),
                .Location = New Point(30 + i * 22, 50 + i * 16),
                .SizeMode = PictureBoxSizeMode.StretchImage,
                .Tag = "P2War"
            }
            grpP2War.Controls.Add(pb)
            pb.SendToBack()
        Next

        If showP2FaceUp AndAlso cards2.Count > faceDownCount Then
            Dim p2Top As New PictureBox With {
                .Image = cards2(faceDownCount).GetImage(),
                .Size = New Size(125, 175),
                .Location = New Point(30 + faceDownCount * 22, 50 + faceDownCount * 16),
                .SizeMode = PictureBoxSizeMode.StretchImage,
                .Tag = "P2WarFaceUp"
            }
            grpP2War.Controls.Add(p2Top)
            p2Top.BringToFront()
        End If
    End Sub

    '====================================================================
    ' Game control methods (unchanged structure)
    '====================================================================
    Private Sub StartNewGame()
        If gameEngine IsNot Nothing Then gameEngine = Nothing
        isGameRunning = True
        currentState = GameState.P1Turn
        roundCount = 0
        warCount = 0

        btnStartGame.Enabled = False
        btnEndGame.Enabled = True
        btnPlayCardP1.Enabled = True
        btnPlayCardP2.Enabled = False

        ResetUIForNewGame()
        gameEngine = New WarGameLogic(Me)
        gameEngine.StartGame()

        lblGameStatus.Text = "Your turn - click Play Card!"
        lblGameStatus.ForeColor = Color.Green
        UpdateCounts()
    End Sub

    Private Sub EndCurrentGame()
        Timer1.Stop()
        isGameRunning = False
        currentState = GameState.Idle
        btnStartGame.Enabled = True
        btnEndGame.Enabled = False
        btnPlayCardP1.Enabled = False
        ResetUIForNewGame()
        gameEngine = Nothing
    End Sub

    Private Sub ResetUIForNewGame()
        ClearPlayArea()
        picP1NextCard.Image = My.Resources.CardBack
        picP2NextCard.Image = My.Resources.CardBack
        lblRoundWinner.Text = "Click 'Start New Game' to begin!"
        lblGameStatus.Text = ""
        pnlGameOver.Visible = False
    End Sub

    Private Sub PrepareNextTurn()
        currentState = GameState.P1Turn
        btnPlayCardP1.Enabled = True
        lblGameStatus.Text = "Your turn - click Play Card!"
        lblGameStatus.ForeColor = Color.Green
        ClearPlayArea()
        picP1NextCard.Image = My.Resources.CardBack
        picP2NextCard.Image = My.Resources.CardBack
        warAnimationStep = 0
    End Sub

    Private Sub CheckGameOver()
        If gameEngine IsNot Nothing AndAlso
           (Not gameEngine.Player1.HasCards() OrElse Not gameEngine.Player2.HasCards()) Then
            gameEngine.EndGame()
            currentState = GameState.GameOver
        End If
    End Sub

    Private Sub EndGameDueToNoCards()
        lblGameStatus.Text = "Game over - no cards left!"
        CheckGameOver()
    End Sub

    Private Sub UpdateCounts()
        If gameEngine IsNot Nothing Then
            UpdateHandCounts(gameEngine.Player1.Hand.Count, gameEngine.Player2.Hand.Count)
            UpdateCapturedCounts(gameEngine.Player1.Captured.Count, gameEngine.Player2.Captured.Count)
            UpdateRoundAndWarCounts(roundCount, warCount)
        End If
    End Sub

    '====================================================================
    ' PUBLIC UI UPDATE METHODS
    '====================================================================
    Public Sub UpdateHandCounts(p1 As Integer, p2 As Integer)
        lblPlayer1Hand.Text = p1.ToString()
        lblPlayer2Hand.Text = p2.ToString()
    End Sub

    Public Sub UpdateCapturedCounts(p1 As Integer, p2 As Integer)
        lblPlayer1Captured.Text = $"You Captured: {p1}"
        lblPlayer2Captured.Text = $"Computer Captured: {p2}"
    End Sub

    Public Sub UpdateRoundAndWarCounts(rounds As Integer, wars As Integer)
        lblRoundCount.Text = rounds.ToString()
        lblWarCount.Text = wars.ToString()
    End Sub

    Public Sub ClearPlayArea()
        grpP1War.Controls.Clear()
        grpP2War.Controls.Clear()
    End Sub

    Public Sub ShowGameOver(winnerIsPlayer1 As Boolean, p1Score As Integer, p2Score As Integer)
        Timer1.Stop()
        lblFinalWinner.Text = If(winnerIsPlayer1,
                                "YOU WIN THE GAME!",
                                If(p1Score = p2Score, "IT'S A TIE!", "COMPUTER WINS!"))
        lblFinalWinner.ForeColor = If(p1Score = p2Score, Color.Yellow,
                                    If(winnerIsPlayer1, Color.Cyan, Color.Orange))
        lblP1FinalCount.Text = $"You Captured: {p1Score} cards"
        lblP2FinalCount.Text = $"Computer Captured: {p2Score} cards"
        pnlGameOver.Visible = True
        pnlGameOver.BringToFront()
        isGameRunning = False
        currentState = GameState.GameOver
        btnStartGame.Enabled = True
        btnEndGame.Enabled = False
        btnPlayCardP1.Enabled = False
        gameEngine = Nothing
    End Sub

    Private Sub btnAbout_Click(sender As Object, e As EventArgs) Handles btnAbout.Click
        Using about As New AboutForm()
            about.ShowDialog(Me)
        End Using
    End Sub

End Class
