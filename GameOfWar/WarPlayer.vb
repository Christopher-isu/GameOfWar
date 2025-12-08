'====================================================================
' Christopher Z
' Fall 2025
' RCET 3375
' git placeholder
' The Game of War
'====================================================================
' WarPlayer.vb
' Manages one player's hand (queue) and captured cards (list)
'====================================================================
Imports System.Collections.Generic

Public Class WarPlayer
    Public ReadOnly Property Name As String
    ' Cards waiting to be played - FIFO order
    Public ReadOnly Property Hand As Queue(Of Card) = New Queue(Of Card)()
    ' Cards won during the game - never returned to hand
    Public ReadOnly Property Captured As List(Of Card) = New List(Of Card)()

    Public Sub New(name As String)
        Me.Name = name
    End Sub

    ' Returns true if player has cards left to play
    Public Function HasCards() As Boolean
        Return Hand.Count > 0
    End Function

    ' Removes and returns the top card from the hand
    Public Function PlayCard() As Card
        If Not HasCards() Then Throw New InvalidOperationException($"{Name} has no cards!")
        Return Hand.Dequeue()
    End Function

    ' Adds a collection of won cards to the captured pile
    Public Sub AddCaptured(cards As List(Of Card))
        Captured.AddRange(cards)
    End Sub

    ' Checks if player has enough cards for a war level
    Public Function CanPlayWar(cardsNeeded As Integer) As Boolean
        Return Hand.Count >= cardsNeeded
    End Function

    ' Takes cards needed for a war: up to faceDownCount face-down + 1 face-up
    Public Function TakeWarCards(faceDownCount As Integer) As List(Of Card)
        Dim cards As New List(Of Card)
        Dim total = Math.Min(faceDownCount + 1, Hand.Count)   ' +1 for the face-up card
        For i = 1 To total
            cards.Add(Hand.Dequeue())
        Next
        Return cards
    End Function

    ' Lets UI see the next card without removing it - used for preview
    Public Function PeekTopCard() As Card
        If Hand.Count = 0 Then Return Nothing
        Return Hand.Peek()
    End Function
End Class