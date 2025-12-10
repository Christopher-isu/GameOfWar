'====================================================================
' Christopher Z
' Fall 2025
' RCET 3375
' https://github.com/Christopher-isu/GameOfWar.git
' The Game of War
'====================================================================
' WarGUIForm.vb
' Main Windows Form for the War game. Responsibilities:
' - Present the GUI controls: Start/End, Play Card, Exit, About, etc.
' - Manage game state transitions (Idle → P1Turn → P2Turn → War → GameOver)
' - Interact with WarGameLogic to play cards and determine outcomes
' - Display cards currently in play, with overlapping war stacks for wars
' - Track and show hand counts, captured counts, round and war counts
'
' This form fulfills the UI requirements: a clickable Start, End, and Exit,
' human-controlled Player 1 via btnPlayCardP1, automatic computer turns,
' and full war visualizations in grpP1War / grpP2War.
'====================================================================
Option Strict On
Option Explicit On

Imports System.ComponentModel

Public Class WarGUIForm

    ' High-level state of the game, used to coordinate button logic and Timer1 behavior.
    Private Enum GameState
        Idle        ' No game running yet or after game over
        P1Turn      ' Human player's turn to click Play Card
        P2Turn      ' Computer's turn (Auto play via Timer1)
        Resolving   ' Resolving a normal battle after P2 plays
        WarAnimating ' Animating a war sequence step-by-step
        GameOver    ' Game has finished; replay or exit
    End Enum

    ' Current overall game state.
    Private currentState As GameState

    ' Tracks whether a game is currently active, to prevent starting a new one mid-game.
    Private isGameRunning As Boolean

    ' Reference to the game logic class that controls deck, players, and rules.
    Private gameEngine As WarGameLogic

    ' Cards currently in play for the ongoing battle (one from each player).
    Private pendingCard1 As Card
    Private pendingCard2 As Card

    ' Counters for statistics required by the project (rounds and wars).
    Private roundCount As Integer
    Private warCount As Integer

    ' Collections used during a war to animate the face-down and face-up cards.
    Private warCards1 As List(Of Card)
    Private warCards2 As List(Of Card)
    Private allWarCards As List(Of Card)

    ' Controls the step-by-step war animation sequence.
    Private warAnimationStep As Integer

    ' Ensures the splash screen only shows once when the form is first loaded.
    Private hasShownSplash As Boolean

    '====================================================================
    ' Form Load & Initialization
    '====================================================================
    Private Sub WarGUIForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeForm()
        ShowSplashScreenOnce()
    End Sub

    ' Sets up UI baseline state and initializes collections used during play.
    Private Sub InitializeForm()
        ResetUIForNewGame()

        ' Human cannot play until a game is started.
        btnPlayCardP1.Enabled = False
        btnPlayCardP2.Enabled = False
        btnPlayCardP2.Text = "Computer"

        ' Timer1 drives computer moves and war animation at fixed intervals.
        Timer1.Interval = 500
        Timer1.Stop()

        ' Initialize collections used during war animations.
        warCards1 = New List(Of Card)()
        warCards2 = New List(Of Card)()
        allWarCards = New List(Of Card)()

        hasShownSplash = False
    End Sub

    ' Shows the splash screen only once at application startup.
    ' This satisfies the requirement for an initial splash screen.
    Private Sub ShowSplashScreenOnce()
        If Not hasShownSplash Then
            hasShownSplash = True
            Using splash As New SplashScreenForm()
                splash.Show()
                splash.Refresh()
                System.Threading.Thread.Sleep(1000) ' Show for approx. 1 second
            End Using
        End If
    End Sub

    '====================================================================
    ' Button Handlers (Start/End/Exit/About/Play Card)
    '====================================================================

    ' Starts a new game if one is not already in progress.
    Private Sub btnStartGame_Click(sender As Object, e As EventArgs) Handles btnStartGame.Click
        If isGameRunning Then
            MessageBox.Show("A game is already in progress!", "War",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        StartNewGame()
    End Sub

    ' Ends the current game after confirmation and resets UI.
    Private Sub btnEndGame_Click(sender As Object, e As EventArgs) Handles btnEndGame.Click
        If Not isGameRunning Then Return

        If MessageBox.Show("End current game?", "Confirm",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            EndCurrentGame()
        End If
    End Sub

    ' Closes the application.
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    ' Starts a new game from the Game Over panel.
    Private Sub btnPlayAgain_Click(sender As Object, e As EventArgs) Handles btnPlayAgain.Click
        pnlGameOver.Visible = False
        StartNewGame()
    End Sub

    ' Shows the About dialog, providing project metadata.
    Private Sub btnAbout_Click(sender As Object, e As EventArgs) Handles btnAbout.Click
        Using about As New AboutForm()
            about.ShowDialog(Me)
        End Using
    End Sub

    ' Human player's "Play Card" button.
    ' This triggers Player 1's card play and hands control to the computer.
    Private Sub btnPlayCardP1_Click(sender As Object, e As EventArgs) Handles btnPlayCardP1.Click
        If currentState <> GameState.P1Turn OrElse gameEngine Is Nothing Then Return

        Try
            ' Ask the game logic for the next card from Player 1's hand.
            pendingCard1 = gameEngine.PlayPlayerCard(gameEngine.Player1)
            picP1NextCard.Image = pendingCard1.GetImage()

            SetStatus("Computer thinking...", Color.Orange)
            SetButtons(GameState.P2Turn)

            currentState = GameState.P2Turn
            Timer1.Start() ' Timer will drive the computer play next.
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '====================================================================
    ' Timer1 - Drives Computer Turns and War Animation
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

    ' Computer automatically plays its card when Timer1 fires in P2Turn state.
    Private Sub ComputerPlayTurn()
        Timer1.Stop()

        Try
            pendingCard2 = gameEngine.PlayPlayerCard(gameEngine.Player2)
            picP2NextCard.Image = pendingCard2.GetImage()

            SetStatus("Resolving round...", Color.Yellow)
            currentState = GameState.Resolving

            Timer1.Start() ' Next tick will resolve the round.
        Catch ex As Exception
            ' If a card cannot be played, the game should end due to no cards.
            EndGameDueToNoCards()
        End Try
    End Sub

    ' Resolves a normal round (non-war) after both players have played a card.
    Private Sub ResolveRound()
        Timer1.Stop()

        Dim result As RoundResult = gameEngine.CompareCards(pendingCard1, pendingCard2)

        Select Case result
            Case RoundResult.P1Wins
                ' Human wins the battle; both cards go to Player 1's captured pile.
                gameEngine.AwardRound(gameEngine.Player1,
                                      New List(Of Card) From {pendingCard1, pendingCard2})
                SetRoundWinner("You win the round!", Color.Cyan)
                roundCount += 1

            Case RoundResult.P2Wins
                ' Computer wins the battle.
                gameEngine.AwardRound(gameEngine.Player2,
                                      New List(Of Card) From {pendingCard1, pendingCard2})
                SetRoundWinner("Computer wins the round!", Color.Orange)
                roundCount += 1

            Case RoundResult.Tie
                ' Equal ranks trigger a war sequence.
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
    ' War Animation System
    ' Animates war in several steps using Timer1:
    ' 0: Show original tie
    ' 1–3: Show 1, 2, then 3 face-down cards
    ' 4: Show 3 face-down + face-up war card
    ' 5: Determine winner and award all war cards
    '====================================================================
    Private Sub StartWarAnimation()
        warCount += 1
        SetStatus("WAR! Laying down cards...", Color.Red)

        ' Collect the war cards to display and ultimately award.
        CollectWarCards()

        warAnimationStep = 0
        currentState = GameState.WarAnimating
        Timer1.Start()
    End Sub

    ' Collects all cards involved in the war from both players' hands.
    Private Sub CollectWarCards()
        ' Reset container lists for this war.
        warCards1.Clear()
        warCards2.Clear()
        allWarCards.Clear()

        ' First, include the original tied cards.
        warCards1.Add(pendingCard1)
        warCards2.Add(pendingCard2)

        ' Then, add three face-down cards and one face-up card from each player
        ' (as long as they have cards available). This models the rules:
        ' 3 face-down + 1 face-up during a war level.
        For i As Integer = 1 To 4
            If gameEngine.Player1.HasCards() Then
                warCards1.Add(gameEngine.Player1.PlayCard())
            End If
            If gameEngine.Player2.HasCards() Then
                warCards2.Add(gameEngine.Player2.PlayCard())
            End If
        Next

        ' Combine all war cards into a single list to award later to the winner.
        allWarCards.AddRange(warCards1)
        allWarCards.AddRange(warCards2)
    End Sub

    ' Performs one step of the war animation on each Timer tick.
    Private Sub AnimateWarStep()
        ClearPlayArea()

        Select Case warAnimationStep
            Case 0
                ' Step 0: show the original tie (both face-up in the preview boxes).
                picP1NextCard.Image = warCards1(0).GetImage()
                picP2NextCard.Image = warCards2(0).GetImage()
                lblRoundWinner.Text = "TIE! Preparing war..."

            Case 1, 2, 3
                ' Steps 1–3: progressively show 1, 2, then 3 face-down cards in each war group box.
                Dim faceDownCount As Integer = warAnimationStep - 1
                DisplayWarStack(warCards1, warCards2, faceDownCount, False, False)
                lblRoundWinner.Text = "WAR: " & faceDownCount.ToString() & "/3 face-down cards..."

            Case 4
                ' Step 4: show all 3 face-down cards plus each player's face-up war card.
                DisplayWarStack(warCards1, warCards2, 3, True, True)
                lblRoundWinner.Text = "Comparing war cards..."

            Case 5
                ' Step 5: finalize war, determine winner, and award all war cards.
                ResolveWar()
                Return
        End Select

        warAnimationStep += 1
        Timer1.Start()
    End Sub

    ' Determines the winner of the war based on the last face-up cards.
    Private Sub ResolveWar()
        Timer1.Stop()

        ' The last card in each warCards list is treated as the face-up war card.
        Dim p1WarCard As Card = warCards1(warCards1.Count - 1)
        Dim p2WarCard As Card = warCards2(warCards2.Count - 1)
        Dim warResult As RoundResult = gameEngine.CompareCards(p1WarCard, p2WarCard)

        ' Keep war stacks visible while announcing the winner.
        DisplayWarStack(warCards1, warCards2, 3, True, True)

        If warResult = RoundResult.P1Wins Then
            gameEngine.AwardRound(gameEngine.Player1, allWarCards)
            SetRoundWinner("YOU WIN THE WAR!", Color.Cyan)
        Else
            gameEngine.AwardRound(gameEngine.Player2, allWarCards)
            SetRoundWinner("COMPUTER WINS THE WAR!", Color.Orange)
        End If

        roundCount += 1
        UpdateCounts()
        CheckGameOver()

        If currentState <> GameState.GameOver Then
            PrepareNextTurn()
        End If
    End Sub

    '====================================================================
    ' UI Helper Methods (Status, Buttons, War Stacks)
    '====================================================================
    Private Sub SetStatus(text As String, statusColor As Color)
        lblGameStatus.Text = text
        lblGameStatus.ForeColor = statusColor
    End Sub

    Private Sub SetRoundWinner(text As String, winnerColor As Color)
        lblRoundWinner.Text = text
        lblRoundWinner.ForeColor = winnerColor
    End Sub

    ' Enables/disables buttons according to game state (only Play Card needs toggling).
    Private Sub SetButtons(state As GameState)
        btnPlayCardP1.Enabled = (state = GameState.P1Turn)
    End Sub

    ' Renders war stacks for both players in their respective group boxes
    ' with overlapping, offset cards to visualize face-down and face-up stacks.
    Private Sub DisplayWarStack(cards1 As List(Of Card), cards2 As List(Of Card),
                               faceDownCount As Integer,
                               showP1FaceUp As Boolean, showP2FaceUp As Boolean)

        ClearPlayArea()

        CreateWarStack(grpP1War, cards1, faceDownCount, showP1FaceUp)
        CreateWarStack(grpP2War, cards2, faceDownCount, showP2FaceUp)
    End Sub

    ' Creates a bottom-to-top stack of war cards in one group box.
    Private Sub CreateWarStack(groupBox As GroupBox, cards As List(Of Card),
                              faceDownCount As Integer, showFaceUp As Boolean)

        ' Draw face-down cards with incremental offsets to create the stacking effect.
        For i As Integer = 0 To faceDownCount - 1
            Dim pb As New PictureBox() With {
                .Image = My.Resources.CardBack,
                .Size = New Size(125, 175),
                .Location = New Point(30 + i * 22, 50 + i * 16),
                .SizeMode = PictureBoxSizeMode.StretchImage
            }
            groupBox.Controls.Add(pb)
            pb.SendToBack()
        Next

        ' Optionally draw a face-up card on top of the stack.
        If showFaceUp AndAlso cards.Count > faceDownCount Then
            Dim faceUpPb As New PictureBox() With {
                .Image = cards(faceDownCount).GetImage(),
                .Size = New Size(125, 175),
                .Location = New Point(30 + faceDownCount * 22, 50 + faceDownCount * 16),
                .SizeMode = PictureBoxSizeMode.StretchImage
            }
            groupBox.Controls.Add(faceUpPb)
            faceUpPb.BringToFront()
        End If
    End Sub

    '====================================================================
    ' Game Control Helpers (Start/End/Counts/Game Over)
    '====================================================================
    Private Sub StartNewGame()
        If gameEngine IsNot Nothing Then
            gameEngine = Nothing
        End If

        isGameRunning = True
        currentState = GameState.P1Turn
        roundCount = 0
        warCount = 0

        btnStartGame.Enabled = False
        btnEndGame.Enabled = True
        SetButtons(GameState.P1Turn)

        ResetUIForNewGame()

        gameEngine = New WarGameLogic(Me)
        gameEngine.StartGame()

        SetStatus("Your turn - click Play Card!", Color.Green)
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

    ' Resets labels and image placeholders when starting a new game or ending.
    Private Sub ResetUIForNewGame()
        ClearPlayArea()
        picP1NextCard.Image = My.Resources.CardBack
        picP2NextCard.Image = My.Resources.CardBack
        lblRoundWinner.Text = "Click 'Start New Game' to begin!"
        lblGameStatus.Text = ""
        pnlGameOver.Visible = False
    End Sub

    ' Prepares the next round: returns control to the human, clears war visuals.
    Private Sub PrepareNextTurn()
        currentState = GameState.P1Turn
        SetButtons(GameState.P1Turn)
        SetStatus("Your turn - click Play Card!", Color.Green)

        ClearPlayArea()
        picP1NextCard.Image = My.Resources.CardBack
        picP2NextCard.Image = My.Resources.CardBack

        warAnimationStep = 0
    End Sub

    ' Checks whether the game should end because a player has no cards left.
    Private Sub CheckGameOver()
        If gameEngine IsNot Nothing AndAlso
           (Not gameEngine.Player1.HasCards() OrElse Not gameEngine.Player2.HasCards()) Then

            gameEngine.EndGame()
            currentState = GameState.GameOver
        End If
    End Sub

    ' Ends game when a card play fails due to empty hand.
    Private Sub EndGameDueToNoCards()
        SetStatus("Game over - no cards left!", Color.Red)
        CheckGameOver()
    End Sub

    ' Refreshes the counts shown on the GUI (hands, captured, rounds, wars).
    Private Sub UpdateCounts()
        If gameEngine IsNot Nothing Then
            UpdateHandCounts(gameEngine.Player1.Hand.Count, gameEngine.Player2.Hand.Count)
            UpdateCapturedCounts(gameEngine.Player1.Captured.Count, gameEngine.Player2.Captured.Count)
            UpdateRoundAndWarCounts(roundCount, warCount)
        End If
    End Sub

    '====================================================================
    ' Public UI Update Methods (called by WarGameLogic)
    '====================================================================
    Public Sub UpdateHandCounts(p1Count As Integer, p2Count As Integer)
        lblPlayer1Hand.Text = p1Count.ToString()
        lblPlayer2Hand.Text = p2Count.ToString()
    End Sub

    Public Sub UpdateCapturedCounts(p1Count As Integer, p2Count As Integer)
        lblPlayer1Captured.Text = "You Captured: " & p1Count.ToString()
        lblPlayer2Captured.Text = "Computer Captured: " & p2Count.ToString()
    End Sub

    Public Sub UpdateRoundAndWarCounts(rounds As Integer, wars As Integer)
        lblRoundCount.Text = rounds.ToString()
        lblWarCount.Text = wars.ToString()
    End Sub

    ' Clears the war display group boxes; used before drawing any war stacks.
    Public Sub ClearPlayArea()
        grpP1War.Controls.Clear()
        grpP2War.Controls.Clear()
    End Sub

    ' Called by WarGameLogic at the end to display the final winner and counts.
    Public Sub ShowGameOver(winnerIsPlayer1 As Boolean, p1Score As Integer, p2Score As Integer)
        Timer1.Stop()

        lblFinalWinner.Text = If(winnerIsPlayer1,
                                 "YOU WIN THE GAME!",
                                 If(p1Score = p2Score, "IT'S A TIE!", "COMPUTER WINS!"))

        lblFinalWinner.ForeColor = If(p1Score = p2Score, Color.Yellow,
                                      If(winnerIsPlayer1, Color.Cyan, Color.Orange))

        lblP1FinalCount.Text = "You Captured: " & p1Score.ToString() & " cards"
        lblP2FinalCount.Text = "Computer Captured: " & p2Score.ToString() & " cards"

        pnlGameOver.Visible = True
        pnlGameOver.BringToFront()

        isGameRunning = False
        currentState = GameState.GameOver

        btnStartGame.Enabled = True
        btnEndGame.Enabled = False
        btnPlayCardP1.Enabled = False

        gameEngine = Nothing
    End Sub

End Class
