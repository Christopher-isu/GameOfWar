'====================================================================
' Christopher Z
' Fall 2025
' RCET 3375
' https://github.com/Christopher-isu/GameOfWar.git
' The Game of War
'====================================================================
' WarPlayer.vb
' Represents a single player in the War game. Tracks:
' - Hand: queue of cards to be played (FIFO)
' - Captured: cards the player has won, which are never replayed.
'
' This class supports project requirements by:
' - Providing methods to play cards in order
' - Keeping captured cards separate from the hand
' - Exposing counts needed for the GUI and game-end logic.
'====================================================================
Option Strict On
Option Explicit On

Imports System.Collections.Generic

Public Class WarPlayer
    ' Display name for the player ("You", "Computer", etc.), used in messages.
    Public ReadOnly Property Name As String

    ' Cards currently in the player's hand (deck to play from).
    ' A Queue enforces "top card first" behavior (FIFO), matching real War.
    Public ReadOnly Property Hand As Queue(Of Card)

    ' Cards won during the game; these are never shuffled back into the hand.
    ' This collection is used to determine the final winner by captured count.
    Public ReadOnly Property Captured As List(Of Card)

    ' Initialize a player with a name and empty collections.
    Public Sub New(name As String)
        Me.Name = name
        Hand = New Queue(Of Card)()
        Captured = New List(Of Card)()
    End Sub

    ' Checks whether this player has any cards left to play.
    ' The game must end when one or both players have no cards in hand.
    Public Function HasCards() As Boolean
        Return Hand.Count > 0
    End Function

    ' Plays (removes and returns) the top card from the hand.
    ' If the hand is empty, throw an exception to signal invalid state.
    Public Function PlayCard() As Card
        If Not HasCards() Then
            Throw New InvalidOperationException(Me.Name & " has no cards!")
        End If

        Return Hand.Dequeue()
    End Function

    ' Adds a set of captured cards to this player's captured pile.
    ' The argument is IEnumerable so callers can pass Lists or other sequences.
    Public Sub AddCaptured(cards As IEnumerable(Of Card))
        Captured.AddRange(cards)
    End Sub

    ' Checks if the player has enough cards available for a given war level,
    ' e.g., 3 face-down cards + 1 face-up card (cardsNeeded=4).
    Public Function CanPlayWar(cardsNeeded As Integer) As Boolean
        Return Hand.Count >= cardsNeeded
    End Function

    ' Takes cards for a war: up to faceDownCount face-down plus one more face-up.
    ' This method **removes** cards from the hand and returns them as a list,
    ' letting the game logic decide how to display and compare them.
    Public Function TakeWarCards(faceDownCount As Integer) As List(Of Card)
        Dim cards As New List(Of Card)()

        ' Total cards needed is faceDownCount plus one face-up card.
        ' If the player has fewer, they provide as many as possible.
        Dim total As Integer = Math.Min(faceDownCount + 1, Hand.Count)

        For i As Integer = 1 To total
            cards.Add(Hand.Dequeue())
        Next

        Return cards
    End Function
End Class
