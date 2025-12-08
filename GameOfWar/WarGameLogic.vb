'====================================================================
' Christopher Z
' Fall 2025
' RCET 3375
' git placeholder
' The Game of War
'====================================================================
' WarGameLogic.vb
' Full game engine – handles shuffling, dealing, rounds, and recursive wars
' Reference Version 1.1 – Fixed missing backToBack argument
'====================================================================
Imports System.Collections.Generic

Public Class WarGameLogic
    Private ReadOnly player1 As WarPlayer
    Private ReadOnly player2 As WarPlayer
    Private ReadOnly ui As WarGUIForm
    Private ReadOnly deck As New List(Of Card)
    Private roundCount As Integer = 0
    Private warCount As Integer = 0

    ' Constructor – receives the form so it can update the UI
    Public Sub New(form As WarGUIForm)
        ui = form
        player1 = New WarPlayer("Player 1")
        player2 = New WarPlayer("Player 2")
    End Sub

    ' Called when the user clicks Start New Game
    Public Sub StartGame()
        BuildAndShuffleDeck()           ' Create and randomize deck
        DealCards()                     ' Give 26 cards to each player
        ui.UpdateHandCounts(26, 26)     ' Show initial hand sizes
        ui.UpdateCapturedCounts(0, 0)   ' Both start with zero captured cards
        ui.UpdateRoundAndWarCounts(0, 0) ' Reset round and war counters
        PlayNextRound()                 ' Begin the first round
    End Sub

    ' Builds a complete 52-card deck
    Private Sub BuildAndShuffleDeck()
        deck.Clear()
        For suit = 0 To 3
            For rank = 2 To 14
                deck.Add(New Card(rank, CType(suit, Card.CardSuitEnum)))
            Next
        Next

        ' Fisher-Yates shuffle
        Dim rnd As New Random()
        For i = deck.Count - 1 To 1 Step -1
            Dim j = rnd.Next(0, i + 1)
            Dim temp = deck(i)
            deck(i) = deck(j)
            deck(j) = temp
        Next
    End Sub

    ' Alternates dealing cards so each player gets 26
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

    ' Main loop – plays one round at a time
    Private Sub PlayNextRound()
        ' Game ends when either player runs out of cards
        If Not player1.HasCards() OrElse Not player2.HasCards() Then
            EndGame()
            Return
        End If

        roundCount += 1
        ui.UpdateRoundAndWarCounts(roundCount, warCount)

        ' Show what each player is about to play (preview)
        Dim p1Next = player1.PeekTopCard()
        Dim p2Next = player2.PeekTopCard()
        If p1Next IsNot Nothing AndAlso p2Next IsNot Nothing Then
            ui.ShowNextCards(p1Next.GetImage(), p2Next.GetImage())
        End If

        Application.DoEvents()
        Threading.Thread.Sleep(800)     ' Short pause so user can see the preview

        ' Actually play the cards
        Dim card1 = player1.PlayCard()
        Dim card2 = player2.PlayCard()

        ' Compare ranks
        If card1.CompareTo(card2) > 0 Then
            ' Player 1 wins the round
            player1.AddCaptured(New List(Of Card) From {card1, card2})
            ui.DisplayNormalRound(card1.GetImage(), card2.GetImage(), True)
        ElseIf card2.CompareTo(card1) > 0 Then
            ' Player 2 wins the round
            player2.AddCaptured(New List(Of Card) From {card1, card2})
            ui.DisplayNormalRound(card1.GetImage(), card2.GetImage(), False)
        Else
            ' Tie → start a war
            warCount += 1
            ui.UpdateRoundAndWarCounts(roundCount, warCount)
            ResolveWar(card1, card2)
            Return
        End If

        UpdateUI()

        ' Update preview for the next round
        If player1.HasCards() AndAlso player2.HasCards() Then
            Dim newP1 = player1.PeekTopCard()
            Dim newP2 = player2.PeekTopCard()
            If newP1 IsNot Nothing AndAlso newP2 IsNot Nothing Then
                ui.ShowNextCards(newP1.GetImage(), newP2.GetImage())
            End If
        End If

        DelayAndContinue()              ' Wait before next round
    End Sub

    ' Handles a war when two cards tie
    Private Sub ResolveWar(tiedCard1 As Card, tiedCard2 As Card)
        Dim inPlay As New List(Of Card) From {tiedCard1, tiedCard2}

        ' Show the two tied cards face-down before war starts
        ui.DisplayWar(0, My.Resources.CardBack, My.Resources.CardBack, False)
        Application.DoEvents()
        Threading.Thread.Sleep(1000)

        Dim faceDownCount = 3
        Do
            ' Make sure both players have enough cards for this war level
            If Not player1.CanPlayWar(faceDownCount + 1) OrElse Not player2.CanPlayWar(faceDownCount + 1) Then
                HandleInsufficientCards(inPlay)
                Return
            End If

            ' Each player puts down face-down cards + one face-up card
            Dim p1WarCards = player1.TakeWarCards(faceDownCount)
            Dim p2WarCards = player2.TakeWarCards(faceDownCount)
            inPlay.AddRange(p1WarCards)
            inPlay.AddRange(p2WarCards)

            Dim p1FaceUp = p1WarCards.Last()
            Dim p2FaceUp = p2WarCards.Last()

            ' Show the current war state
            ui.DisplayWar(faceDownCount, p1FaceUp.GetImage(), p2FaceUp.GetImage(), faceDownCount > 3)
            Application.DoEvents()
            Threading.Thread.Sleep(1500)

            ' Decide who wins the war
            If p1FaceUp.CompareTo(p2FaceUp) > 0 Then
                player1.AddCaptured(inPlay)
                ui.DisplayNormalRound(p1FaceUp.GetImage(), p2FaceUp.GetImage(), True)
                ui.lblRoundWinner.Text = "Player 1 wins the war!"
                UpdateUI()
                DelayAndContinue()
                Return
            ElseIf p2FaceUp.CompareTo(p1FaceUp) > 0 Then
                player2.AddCaptured(inPlay)
                ui.DisplayNormalRound(p1FaceUp.GetImage(), p2FaceUp.GetImage(), False)
                ui.lblRoundWinner.Text = "Player 2 wins the war!"
                UpdateUI()
                DelayAndContinue()
                Return
            Else
                ' Another tie – continue to next war level
                inPlay.Add(p1FaceUp)
                inPlay.Add(p2FaceUp)
                faceDownCount += 3
            End If
        Loop
    End Sub

    ' Called when a player doesn't have enough cards during a war
    Private Sub HandleInsufficientCards(inPlay As List(Of Card))
        If player1.HasCards() Then player1.AddCaptured(inPlay)
        If player2.HasCards() Then player2.AddCaptured(inPlay)
        UpdateUI()
        If Not player1.HasCards() OrElse Not player2.HasCards() Then
            EndGame()
        Else
            DelayAndContinue()
        End If
    End Sub

    ' Updates all counters on the form
    Private Sub UpdateUI()
        ui.UpdateHandCounts(player1.Hand.Count, player2.Hand.Count)
        ui.UpdateCapturedCounts(player1.Captured.Count, player2.Captured.Count)
    End Sub

    ' Waits 2 seconds before playing the next round
    Private Sub DelayAndContinue()
        Dim t As New Timer With {.Interval = 2000}
        AddHandler t.Tick, Sub(sender As Object, e As EventArgs)
                               CType(sender, Timer).Stop()
                               CType(sender, Timer).Dispose()
                               PlayNextRound()
                           End Sub
        t.Start()
    End Sub

    ' Ends the game and shows the winner based on captured cards
    Private Sub EndGame()
        Dim p1Score = player1.Captured.Count
        Dim p2Score = player2.Captured.Count
        Dim winnerIsP1 = p1Score > p2Score
        Dim isTie = p1Score = p2Score

        ui.ShowGameOver(winnerIsP1 OrElse isTie, p1Score, p2Score)
    End Sub
End Class