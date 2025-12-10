'====================================================================
' WarGUIForm.vb – Final Adjusted Version (Reference 2.1)
' Normal round: uses picP1NextCard / picP2NextCard
' War: larger cards, bottom-to-top stacking in grpP1War / grpP2War
'====================================================================
Imports System.ComponentModel

Public Class WarGUIForm

    Private isGameRunning As Boolean = False
    Private gameEngine As WarGameLogic = Nothing

    Private Sub WarGUIForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ResetUIForNewGame()
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

    '====================================================================
    ' Game control
    '====================================================================
    Private Sub StartNewGame()
        If gameEngine IsNot Nothing Then gameEngine = Nothing
        isGameRunning = True
        btnStartGame.Enabled = False
        btnEndGame.Enabled = True
        Timer1.Stop()
        ResetUIForNewGame()
        gameEngine = New WarGameLogic(Me)
        gameEngine.StartGame()
    End Sub

    Private Sub EndCurrentGame()
        Timer1.Stop()
        isGameRunning = False
        btnStartGame.Enabled = True
        btnEndGame.Enabled = False
        ResetUIForNewGame()
    End Sub

    Private Sub ResetUIForNewGame()
        ClearPlayArea()
        picP1NextCard.Image = My.Resources.CardBack
        picP2NextCard.Image = My.Resources.CardBack
        lblRoundWinner.Text = "Click 'Start New Game' to begin!"
        lblGameStatus.Text = ""
        pnlGameOver.Visible = False
    End Sub

    '====================================================================
    ' PUBLIC UI UPDATE METHODS
    '====================================================================
    Public Sub UpdateHandCounts(p1 As Integer, p2 As Integer)
        lblPlayer1Hand.Text = p1.ToString()
        lblPlayer2Hand.Text = p2.ToString()
    End Sub

    Public Sub UpdateCapturedCounts(p1 As Integer, p2 As Integer)
        lblPlayer1Captured.Text = $"Captured: {p1}"
        lblPlayer2Captured.Text = $"Captured: {p2}"
    End Sub

    Public Sub UpdateRoundAndWarCounts(rounds As Integer, wars As Integer)
        lblRoundCount.Text = rounds.ToString()
        lblWarCount.Text = wars.ToString()
    End Sub

    Public Sub ShowNextCards(p1Img As Image, p2Img As Image)
        picP1NextCard.Image = p1Img
        picP2NextCard.Image = p2Img
    End Sub

    Public Sub ClearPlayArea()
        ' Clear war group boxes
        grpP1War.Controls.Clear()
        grpP2War.Controls.Clear()

        ' Reset preview cards (they show current played card during normal round)
        picP1NextCard.Image = My.Resources.CardBack
        picP2NextCard.Image = My.Resources.CardBack
    End Sub

    ' Normal round – use the existing preview boxes
    Public Sub DisplayNormalRound(p1Card As Image, p2Card As Image, p1Wins As Boolean)
        ClearPlayArea()
        picP1NextCard.Image = p1Card
        picP2NextCard.Image = p2Card

        lblGameStatus.Text = ""
        lblRoundWinner.Text = If(p1Wins, "Player 1 wins the round!", "Player 2 wins the round!")
        lblRoundWinner.ForeColor = If(p1Wins, Color.Cyan, Color.Orange)
    End Sub

    ' War display – larger cards, bottom-to-top stacking
    Public Sub DisplayWarOverlap(faceDownCount As Integer, p1War As Image, p2War As Image, backToBack As Boolean)
        ClearPlayArea()

        Dim back = My.Resources.CardBack
        Dim cardWidth As Integer = 125   ' 25% larger than 100
        Dim cardHeight As Integer = 175  ' 25% larger than 140
        Dim offsetX As Integer = 22
        Dim offsetY As Integer = 16

        ' === Player 1 War Stack (bottom-to-top) ===
        ' First: face-down cards from bottom
        For i = 0 To faceDownCount - 1
            Dim pb As New PictureBox With {
                .Image = back,
                .Size = New Size(cardWidth, cardHeight),
                .Location = New Point(30 + i * offsetX, 50 + i * offsetY),
                .SizeMode = PictureBoxSizeMode.StretchImage
            }
            grpP1War.Controls.Add(pb)
            pb.SendToBack()  ' Ensure face-up card appears on top
        Next

        ' Then: face-up war card on top
        Dim p1Top As New PictureBox With {
            .Image = p1War,
            .Size = New Size(cardWidth, cardHeight),
            .Location = New Point(30 + faceDownCount * offsetX, 50 + faceDownCount * offsetY),
            .SizeMode = PictureBoxSizeMode.StretchImage
        }
        grpP1War.Controls.Add(p1Top)
        p1Top.BringToFront()

        ' === Player 2 War Stack (bottom-to-top) ===
        For i = 0 To faceDownCount - 1
            Dim pb As New PictureBox With {
                .Image = back,
                .Size = New Size(cardWidth, cardHeight),
                .Location = New Point(30 + i * offsetX, 50 + i * offsetY),
                .SizeMode = PictureBoxSizeMode.StretchImage
            }
            grpP2War.Controls.Add(pb)
            pb.SendToBack()
        Next

        Dim p2Top As New PictureBox With {
            .Image = p2War,
            .Size = New Size(cardWidth, cardHeight),
            .Location = New Point(30 + faceDownCount * offsetX, 50 + faceDownCount * offsetY),
            .SizeMode = PictureBoxSizeMode.StretchImage
        }
        grpP2War.Controls.Add(p2Top)
        p2Top.BringToFront()

        lblGameStatus.Text = If(backToBack, "DOUBLE WAR!!!", "WAR!")
        lblGameStatus.ForeColor = Color.Red
        lblRoundWinner.Text = "War in progress..."
        lblRoundWinner.ForeColor = Color.Yellow
    End Sub

    Public Sub ShowGameOver(winnerIsPlayer1 As Boolean, p1Score As Integer, p2Score As Integer)
        Timer1.Stop()

        lblFinalWinner.Text = If(winnerIsPlayer1,
                                "PLAYER 1 WINS THE GAME!",
                                If(p1Score = p2Score, "IT'S A TIE!", "PLAYER 2 WINS THE GAME!"))

        lblFinalWinner.ForeColor = If(p1Score = p2Score, Color.Yellow,
                                    If(winnerIsPlayer1, Color.Cyan, Color.Orange))

        lblP1FinalCount.Text = $"Player 1 Captured: {p1Score} cards"
        lblP2FinalCount.Text = $"Player 2 Captured: {p2Score} cards"

        pnlGameOver.Visible = True
        pnlGameOver.BringToFront()

        isGameRunning = False
        btnStartGame.Enabled = True
        btnEndGame.Enabled = False
        gameEngine = Nothing
    End Sub
End Class