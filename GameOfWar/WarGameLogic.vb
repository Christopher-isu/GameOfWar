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
'====================================================================
' WarGameLogic.vb – PURE LOGIC VERSION (Human vs Computer)
' No timers, no auto-play - called by WarGUIForm state machine
'====================================================================
Imports System.Collections.Generic
Imports System.Windows.Forms

Public Enum RoundResult
    P1Wins
    P2Wins
    Tie
End Enum

Public Class WarGameLogic
    Public ReadOnly Property Player1 As WarPlayer
    Public ReadOnly Property Player2 As WarPlayer
    Private ReadOnly ui As WarGUIForm
    Private ReadOnly deck As New List(Of Card)

    Public Sub New(form As WarGUIForm)
        ui = form
        Player1 = New WarPlayer("You")
        Player2 = New WarPlayer("Computer")
    End Sub

    Public Sub StartGame()
        BuildAndShuffleDeck()
        DealCards()
        ui.UpdateHandCounts(26, 26)
        ui.UpdateCapturedCounts(0, 0)
        ui.UpdateRoundAndWarCounts(0, 0)
    End Sub

    Public Function PlayPlayerCard(player As WarPlayer) As Card
        If Not player.HasCards() Then Throw New InvalidOperationException($"{player.Name} has no cards!")
        Return player.PlayCard()
    End Function

    Public Function CompareCards(c1 As Card, c2 As Card) As RoundResult
        If c1.CompareTo(c2) > 0 Then Return RoundResult.P1Wins
        If c2.CompareTo(c1) > 0 Then Return RoundResult.P2Wins
        Return RoundResult.Tie
    End Function

    Public Sub AwardRound(winner As WarPlayer, cards As List(Of Card))
        winner.AddCaptured(cards)
        ui.UpdateCapturedCounts(Player1.Captured.Count, Player2.Captured.Count)
    End Sub

    Private Sub BuildAndShuffleDeck()
        deck.Clear()
        For suit = 0 To 3
            For rank = 2 To 14
                deck.Add(New Card(rank, CType(suit, Card.CardSuitEnum)))
            Next
        Next
        Dim rnd As New Random()
        For i = deck.Count - 1 To 1 Step -1
            Dim j = rnd.Next(0, i + 1)
            Dim temp = deck(i)
            deck(i) = deck(j)
            deck(j) = temp
        Next
    End Sub

    Private Sub DealCards()
        Player1.Hand.Clear()
        Player2.Hand.Clear()
        Player1.Captured.Clear()
        Player2.Captured.Clear()
        For i = 0 To 51
            If i Mod 2 = 0 Then
                Player1.Hand.Enqueue(deck(i))
            Else
                Player2.Hand.Enqueue(deck(i))
            End If
        Next
    End Sub

    Public Sub EndGame()
        Dim p1Score = Player1.Captured.Count
        Dim p2Score = Player2.Captured.Count
        Dim p1Wins = p1Score > p2Score
        ui.ShowGameOver(p1Wins, p1Score, p2Score)  ' Now has all 3 parameters
    End Sub

    Public Sub CheckGameOver()
        If Not Player1.HasCards() OrElse Not Player2.HasCards() Then
            EndGame()
        End If
    End Sub
End Class