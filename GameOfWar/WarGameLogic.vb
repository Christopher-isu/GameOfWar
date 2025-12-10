'====================================================================
' Christopher Z
' Fall 2025
' RCET 3375
' https://github.com/Christopher-isu/GameOfWar.git
' The Game of War
'====================================================================
' WarGameLogic.vb
' Encapsulates the War game mechanics:
' - Builds and shuffles a standard 52-card deck
' - Deals cards evenly between two players
' - Provides operations to play cards and compare them
' - Tracks captured cards and computes the final winner
'
' This class separates game rules from the GUI:
' the form calls into these methods to perform game actions,
' and the logic uses callbacks to update counts and show the final result.
'====================================================================
Option Strict On
Option Explicit On

Imports System.Collections.Generic

' Represents the result of comparing two cards in a single battle:
' Player 1 wins, Player 2 wins, or a tie (which triggers war handling).
Public Enum RoundResult
    P1Wins
    P2Wins
    Tie
End Enum

Public Class WarGameLogic
    ' Public player references so the GUI can access counts and hands as needed.
    Public ReadOnly Property Player1 As WarPlayer
    Public ReadOnly Property Player2 As WarPlayer

    ' Reference to the main GUI form. This is used for:
    ' - Updating numeric labels (hands, captured)
    ' - Showing the final game result
    ' Note: Card-by-card animation is handled by the form itself.
    Private ReadOnly ui As WarGUIForm

    ' Internal representation of the deck used at the start of each game.
    Private ReadOnly deck As List(Of Card)

    ' Constructor: ties game logic to the main form and creates two players.
    Public Sub New(form As WarGUIForm)
        ui = form
        Player1 = New WarPlayer("You")
        Player2 = New WarPlayer("Computer")
        deck = New List(Of Card)()
    End Sub

    ' Sets up a new game:
    ' - Builds and shuffles a 52-card deck
    ' - Deals cards evenly to both players
    ' - Notifies the UI about initial counts
    Public Sub StartGame()
        BuildAndShuffleDeck()
        DealCards()
        ui.UpdateHandCounts(Player1.Hand.Count, Player2.Hand.Count)
        ui.UpdateCapturedCounts(Player1.Captured.Count, Player2.Captured.Count)
        ui.UpdateRoundAndWarCounts(0, 0)
    End Sub

    ' Called when a specific player needs to play one card.
    ' This removes the top card from the player's hand and updates hand counts.
    Public Function PlayPlayerCard(player As WarPlayer) As Card
        If Not player.HasCards() Then
            Throw New InvalidOperationException(player.Name & " has no cards!")
        End If

        Dim played As Card = player.PlayCard()
        ui.UpdateHandCounts(Player1.Hand.Count, Player2.Hand.Count)
        Return played
    End Function

    ' Compares two cards and returns who wins that battle.
    ' This is used both for normal rounds and for war comparisons.
    Public Function CompareCards(card1 As Card, card2 As Card) As RoundResult
        Dim cmp As Integer = card1.CompareTo(card2)

        If cmp > 0 Then
            Return RoundResult.P1Wins
        ElseIf cmp < 0 Then
            Return RoundResult.P2Wins
        Else
            Return RoundResult.Tie
        End If
    End Function

    ' Awards a collection of cards to the given winner's captured pile
    ' and updates the UI with the new captured counts.
    Public Sub AwardRound(winner As WarPlayer, cards As IEnumerable(Of Card))
        winner.AddCaptured(cards)
        ui.UpdateCapturedCounts(Player1.Captured.Count, Player2.Captured.Count)
    End Sub

    ' Called at the end of the game (either hand is empty or a war stops play).
    ' It compares captured card counts to determine the final winner
    ' and then calls back into the form to display the Game Over screen.
    Public Sub EndGame()
        Dim p1Score As Integer = Player1.Captured.Count
        Dim p2Score As Integer = Player2.Captured.Count

        ' Player 1 is the winner if they captured more cards.
        Dim p1Wins As Boolean = (p1Score > p2Score)

        ' The form shows detailed result and handles tie display when counts match.
        ui.ShowGameOver(p1Wins, p1Score, p2Score)
    End Sub

    '====================================================================
    ' Internal deck setup helpers (not exposed to the GUI)
    '====================================================================

    ' Builds a fresh 52-card deck with ranks 2–14 and all four suits,
    ' then randomizes it using a Fisher–Yates shuffle.
    Private Sub BuildAndShuffleDeck()
        deck.Clear()

        ' For each suit (0–3), create ranks 2 through 14.
        For suit As Integer = 0 To 3
            For rank As Integer = 2 To 14
                deck.Add(New Card(rank, CType(suit, Card.CardSuitEnum)))
            Next
        Next

        ' Shuffle the deck in-place so card order is random for each game.
        Dim rnd As New Random()
        For i As Integer = deck.Count - 1 To 1 Step -1
            Dim j As Integer = rnd.Next(0, i + 1)
            Dim temp As Card = deck(i)
            deck(i) = deck(j)
            deck(j) = temp
        Next
    End Sub

    ' Deals the shuffled deck alternately between Player1 and Player2,
    ' ensuring they each receive exactly half (26 cards in a standard deck).
    Private Sub DealCards()
        Player1.Hand.Clear()
        Player2.Hand.Clear()
        Player1.Captured.Clear()
        Player2.Captured.Clear()

        For i As Integer = 0 To deck.Count - 1
            If (i Mod 2) = 0 Then
                Player1.Hand.Enqueue(deck(i))
            Else
                Player2.Hand.Enqueue(deck(i))
            End If
        Next
    End Sub

End Class
