'====================================================================
' Christopher Z
' Fall 2025
' RCET 3375
' git placeholder
' The Game of War
'====================================================================
' WarGameLogic.vb – FINAL PRODUCTION VERSION (All Errors Fixed)
' Fixes: War display, insufficient cards, EndGame param, Timer ambiguity
'====================================================================
Imports System.Collections.Generic
Imports System.Windows.Forms

Public Class WarGameLogic
    Private ReadOnly player1 As WarPlayer
    Private ReadOnly player2 As WarPlayer
    Private ReadOnly ui As WarGUIForm
    Private ReadOnly deck As New List(Of Card)
    Private roundCount As Integer = 0
    Private warCount As Integer = 0

    ' Constructor – receives the form so we can update the screen
    Public Sub New(form As WarGUIForm)
        ui = form
        player1 = New WarPlayer("Player 1")
        player2 = New WarPlayer("Player 2")
    End Sub

    ' Called when user clicks Start New Game
    Public Sub StartGame()
        BuildAndShuffleDeck()           ' Create a full deck and randomize it
        DealCards()                     ' Give 26 cards to each player
        ui.UpdateHandCounts(26, 26)     ' Show initial hand sizes on screen
        ui.UpdateCapturedCounts(0, 0)   ' Both start with no captured cards
        ui.UpdateRoundAndWarCounts(0, 0) ' Reset round and war counters
        PlayNextRound()                 ' Start the first round
    End Sub

    ' Creates a complete 52-card deck
    Private Sub BuildAndShuffleDeck()
        deck.Clear()
        For suit = 0 To 3
            For rank = 2 To 14
                deck.Add(New Card(rank, CType(suit, Card.CardSuitEnum)))
            Next
        Next

        ' Fisher-Yates shuffle – standard way to randomize a list
        Dim rnd As New Random()
        For i = deck.Count - 1 To 1 Step -1
            Dim j = rnd.Next(0, i + 1)
            Dim temp = deck(i)
            deck(i) = deck(j)
            deck(j) = temp
        Next
    End Sub

    ' Deals cards alternately so each player gets exactly 26
    Private Sub DealCards()
        player1.Hand.Clear()
        player2.Hand.Clear()
        player1.Captured.Clear()
        player2.Captured.Clear()

        For i = 0 To 51
            If i Mod 2 = 0 Then
                player1.Hand.Enqueue(deck(i))
            Else
                player2.Hand.Enqueue(deck(i))
            End If
        Next
    End Sub

    ' Main game loop – plays one round at a time
    Private Sub PlayNextRound()
        ' Game ends when either player has no cards left
        If Not player1.HasCards() OrElse Not player2.HasCards() Then
            EndGame()
            Return
        End If

        roundCount += 1
        ui.UpdateRoundAndWarCounts(roundCount, warCount)

        ' Remove and play the actual cards
        Dim card1 = player1.PlayCard()
        Dim card2 = player2.PlayCard()

        ' Compare the two played cards
        If card1.CompareTo(card2) > 0 Then
            player1.AddCaptured(New List(Of Card) From {card1, card2})
            ui.DisplayNormalRound(card1.GetImage(), card2.GetImage(), True)
            ui.lblRoundWinner.Text = "Player 1 wins the round!"
        ElseIf card2.CompareTo(card1) > 0 Then
            player2.AddCaptured(New List(Of Card) From {card1, card2})
            ui.DisplayNormalRound(card1.GetImage(), card2.GetImage(), False)
            ui.lblRoundWinner.Text = "Player 2 wins the round!"
        Else
            ' FIX #1: TIE - Show cards FACE-UP first, then start war
            ui.DisplayNormalRound(card1.GetImage(), card2.GetImage(), False)
            ui.lblRoundWinner.Text = "TIE! WAR!"
            ui.lblRoundWinner.ForeColor = Color.Red
            Application.DoEvents()
            System.Threading.Thread.Sleep(1000)

            warCount += 1
            ui.UpdateRoundAndWarCounts(roundCount, warCount)
            ResolveWar(card1, card2)
            Return
        End If

        UpdateUI()
        DelayAndContinue()              ' Wait before next round
    End Sub

    ' Handles war when two cards have the same rank
    Private Sub ResolveWar(tiedCard1 As Card, tiedCard2 As Card)
        Dim inPlay As New List(Of Card) From {tiedCard1, tiedCard2}
        Dim faceDownCount = 3

        Do
            ' FIX #2: Game ends IMMEDIATELY if either player can't continue war
            If Not player1.CanPlayWar(faceDownCount + 1) OrElse Not player2.CanPlayWar(faceDownCount + 1) Then
                EndGame()  ' No award of contested cards - game over by hand depletion
                Return
            End If

            ' Each player puts down their face-down cards + one face-up card
            Dim p1WarCards = player1.TakeWarCards(faceDownCount)
            Dim p2WarCards = player2.TakeWarCards(faceDownCount)
            inPlay.AddRange(p1WarCards)
            inPlay.AddRange(p2WarCards)

            Dim p1FaceUp = p1WarCards.Last()
            Dim p2FaceUp = p2WarCards.Last()

            ' Display current war state using overlapping cards
            ui.DisplayWarOverlap(faceDownCount, p1FaceUp.GetImage(), p2FaceUp.GetImage(), faceDownCount > 3)
            Application.DoEvents()
            System.Threading.Thread.Sleep(1500)

            ' Determine who wins the war
            If p1FaceUp.CompareTo(p2FaceUp) > 0 Then
                player1.AddCaptured(inPlay)
                ui.lblRoundWinner.Text = "Player 1 wins the war!"
                UpdateUI()
                DelayAndContinue()
                Return
            ElseIf p2FaceUp.CompareTo(p1FaceUp) > 0 Then  ' FIX: Typo corrected
                player2.AddCaptured(inPlay)
                ui.lblRoundWinner.Text = "Player 2 wins the war!"
                UpdateUI()
                DelayAndContinue()
                Return
            Else
                ' Another tie – war continues to next level
                inPlay.Add(p1FaceUp)
                inPlay.Add(p2FaceUp)
                faceDownCount += 3
            End If
        Loop
    End Sub

    ' Updates all numeric counters on the form
    Private Sub UpdateUI()
        ui.UpdateHandCounts(player1.Hand.Count, player2.Hand.Count)
        ui.UpdateCapturedCounts(player1.Captured.Count, player2.Captured.Count)
    End Sub

    ' FIX #4: Windows Forms Timer - No ambiguity with clean imports
    Private Sub DelayAndContinue()
        Dim delayTimer As New Timer() With {
            .Interval = 2000  ' 2 seconds
        }
        AddHandler delayTimer.Tick, Sub(sender As Object, e As EventArgs)
                                        Dim tmr = DirectCast(sender, Timer)
                                        tmr.Stop()
                                        tmr.Dispose()
                                        PlayNextRound()
                                    End Sub
        delayTimer.Start()
    End Sub

    ' FIX #3: Correct winner parameter logic
    Private Sub EndGame()
        Dim p1Score = player1.Captured.Count
        Dim p2Score = player2.Captured.Count
        Dim p1Wins = p1Score > p2Score  ' Clear boolean: true if P1 has more captured cards
        ui.ShowGameOver(p1Wins, p1Score, p2Score)
    End Sub
End Class
