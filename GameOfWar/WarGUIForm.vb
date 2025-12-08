'====================================================================
' Christopher Z
' Fall 2025
' RCET 3375
' git placeholder
' The Game of War
'====================================================================
' WarGUIForm.vb
' Main game window that shows the current state of play
' Reference Version 1.0 – saved before switching to overlapping war display
'====================================================================

Imports System.ComponentModel

Public Class WarGUIForm

    ' Keeps track of whether a game is currently running
    Private isGameRunning As Boolean = False
    ' Reference to the object that runs the actual game rules
    Private gameEngine As WarGameLogic = Nothing

    ' Runs once when the form first appears
    Private Sub WarGUIForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ResetUIForNewGame()                     ' Make sure everything starts clean
    End Sub

    '====================================================================
    ' Button click handlers
    '====================================================================
    Private Sub btnStartGame_Click(sender As Object, e As EventArgs) Handles btnStartGame.Click
        ' Prevent starting a new game while one is already in progress
        If isGameRunning Then
            MessageBox.Show("A game is already in progress!", "War", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        StartNewGame()                          ' Begin a fresh game
    End Sub

    Private Sub btnEndGame_Click(sender As Object, e As EventArgs) Handles btnEndGame.Click
        ' Only allow ending a game that has actually started
        If Not isGameRunning Then Return

        Dim result = MessageBox.Show("Are you sure you want to end the current game?", "End Game",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            EndCurrentGame()                    ' Stop everything and return to start screen
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()                              ' Close the program
    End Sub

    Private Sub btnPlayAgain_Click(sender As Object, e As EventArgs) Handles btnPlayAgain.Click
        pnlGameOver.Visible = False             ' Hide the game-over panel
        StartNewGame()                          ' Start a completely new game
    End Sub

    '====================================================================
    ' Game start / stop logic
    '====================================================================
    Private Sub StartNewGame()
        ' Remove any old game engine that might still exist
        If gameEngine IsNot Nothing Then gameEngine = Nothing

        ' Update flags and button states
        isGameRunning = True
        btnStartGame.Enabled = False
        btnEndGame.Enabled = True
        Timer1.Stop()                           ' Stop any leftover timer

        ' Clear the screen so the new game starts fresh
        ResetUIForNewGame()

        ' Create a new game engine and tell it to begin playing
        gameEngine = New WarGameLogic(Me)
        gameEngine.StartGame()
    End Sub

    Private Sub EndCurrentGame()
        Timer1.Stop()                           ' Stop any ongoing timer
        isGameRunning = False
        btnStartGame.Enabled = True
        btnEndGame.Enabled = False
        ResetUIForNewGame()                     ' Return UI to the initial state
    End Sub

    Private Sub ResetUIForNewGame()
        ' Remove all cards from the playing area
        ClearPlayArea()
        ' Show card backs in the "next card" preview boxes
        picP1NextCard.Image = My.Resources.CardBack
        picP2NextCard.Image = My.Resources.CardBack
        ' Reset all text fields
        lblRoundWinner.Text = "Click 'Start New Game' to begin!"
        lblGameStatus.Text = ""
        pnlGameOver.Visible = False
    End Sub

    '====================================================================
    ' Methods called by WarGameLogic to update the screen
    '====================================================================
    Public Sub UpdateHandCounts(p1 As Integer, p2 As Integer)
        lblPlayer1Hand.Text = p1.ToString()     ' Show how many cards Player 1 has left
        lblPlayer2Hand.Text = p2.ToString()     ' Show how many cards Player 2 has left
    End Sub

    Public Sub UpdateCapturedCounts(p1 As Integer, p2 As Integer)
        lblPlayer1Captured.Text = $"Captured: {p1}"   ' Cards Player 1 has won
        lblPlayer2Captured.Text = $"Captured: {p2}"   ' Cards Player 2 has won
    End Sub

    Public Sub UpdateRoundAndWarCounts(rounds As Integer, wars As Integer)
        lblRoundCount.Text = rounds.ToString() ' Total rounds played
        lblWarCount.Text = wars.ToString()     ' Number of wars that have occurred
    End Sub

    Public Sub ShowNextCards(p1Img As Image, p2Img As Image)
        ' Display the card each player will play next (preview only)
        picP1NextCard.Image = p1Img
        picP2NextCard.Image = p2Img
    End Sub

    Public Sub ClearPlayArea()
        Dim back = My.Resources.CardBack
        ' Reset every picture box that can show a card
        picP1Card1.Image = back : picP1Card2.Image = back : picP1Card3.Image = back : picP1WarCard.Image = back
        picP2Card1.Image = back : picP2Card2.Image = back : picP2Card3.Image = back : picP2WarCard.Image = back
    End Sub

    Public Sub DisplayNormalRound(p1Card As Image, p2Card As Image, p1Wins As Boolean)
        ' Show a regular round (no war)
        ClearPlayArea()
        picP1Card1.Image = p1Card               ' Player 1's played card
        picP2Card1.Image = p2Card               ' Player 2's played card
        lblGameStatus.Text = ""
        lblRoundWinner.Text = If(p1Wins, "Player 1 wins the round!", "Player 2 wins the round!")
        lblRoundWinner.ForeColor = If(p1Wins, Color.Cyan, Color.Orange)
    End Sub

    Public Sub DisplayWar(faceDownCount As Integer, p1War As Image, p2War As Image, backToBack As Boolean)
        ' Show a war situation – up to 3 face-down cards plus one face-up per player
        ClearPlayArea()
        Dim back = My.Resources.CardBack

        If faceDownCount >= 1 Then picP1Card1.Image = back : picP2Card1.Image = back
        If faceDownCount >= 2 Then picP1Card2.Image = back : picP2Card2.Image = back
        If faceDownCount >= 3 Then picP1Card3.Image = back : picP2Card3.Image = back

        picP1WarCard.Image = p1War              ' Player 1's face-up war card
        picP2WarCard.Image = p2War              ' Player 2's face-up war card

        lblGameStatus.Text = If(backToBack, "DOUBLE WAR!!!", "WAR!")
        lblGameStatus.ForeColor = Color.Red
        lblRoundWinner.Text = "War in progress..."
        lblRoundWinner.ForeColor = Color.Yellow
    End Sub

    Public Sub ShowGameOver(winnerIsPlayer1 As Boolean, p1Score As Integer, p2Score As Integer)
        Timer1.Stop()                           ' Stop any animation timers

        ' Determine who won or if it was a tie
        lblFinalWinner.Text = If(winnerIsPlayer1,
                                "PLAYER 1 WINS THE GAME!",
                                If(p1Score = p2Score, "IT'S A TIE!", "PLAYER 2 WINS THE GAME!"))

        lblFinalWinner.ForeColor = If(p1Score = p2Score, Color.Yellow,
                                    If(winnerIsPlayer1, Color.Cyan, Color.Orange))

        lblP1FinalCount.Text = $"Player 1 Captured: {p1Score} cards"
        lblP2FinalCount.Text = $"Player 2 Captured: {p2Score} cards"

        pnlGameOver.Visible = True
        pnlGameOver.BringToFront()

        ' Reset state so a new game can be started
        isGameRunning = False
        btnStartGame.Enabled = True
        btnEndGame.Enabled = False

        ' Remove the old game engine – important for fresh restarts
        gameEngine = Nothing
    End Sub
End Class