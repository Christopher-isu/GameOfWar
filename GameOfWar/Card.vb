'====================================================================
' Christopher Z
' Fall 2025
' RCET 3375
' git placeholder
' The Game of War
'====================================================================
' Card.vb
' Represents one playing card with rank, suit, and image support
'====================================================================
Public Class Card
    Implements IComparable(Of Card)

    ' Rank from 2 (lowest) to 14 (Ace high)
    Public ReadOnly Property Rank As Integer
    ' Suit of the card
    Public ReadOnly Property CardSuit As CardSuitEnum
    ' Filename exactly matching the image in Resources (e.g., "10H.jpg")
    Public ReadOnly Property FileName As String

    ' Possible suits
    Public Enum CardSuitEnum
        Clubs
        Diamonds
        Hearts
        Spades
    End Enum

    ' Constructor - called when creating a new card
    Public Sub New(rank As Integer, suit As CardSuitEnum)
        ' Validate rank is within legal range
        If rank < 2 OrElse rank > 14 Then Throw New ArgumentException("Rank must be 2-14")
        Me.Rank = rank
        Me.CardSuit = suit

        ' Convert rank number to text for filename (A, J, Q, K, or number)
        Dim rankStr As String = If(rank = 14, "A",
                                 If(rank = 11, "J",
                                 If(rank = 12, "Q",
                                 If(rank = 13, "K", rank.ToString()))))

        ' Convert suit to single letter
        Dim suitChar As Char = If(suit = CardSuitEnum.Clubs, "C"c,
                              If(suit = CardSuitEnum.Diamonds, "D"c,
                              If(suit = CardSuitEnum.Hearts, "H"c, "S"c)))

        ' Build the exact filename used in Resources
        Me.FileName = $"{rankStr}{suitChar}.jpg"
    End Sub

    ' Used to compare two cards - higher rank wins
    Public Function CompareTo(other As Card) As Integer Implements IComparable(Of Card).CompareTo
        Return Me.Rank.CompareTo(other.Rank)
    End Function

    ' Returns the actual image from Resources
    Public Function GetImage() As Image
        Dim resourceName As String = FileName.Replace(".jpg", "")
        ' VB.NET sometimes prefixes numbers with underscore in Resources
        If Char.IsDigit(resourceName(0)) OrElse resourceName = "AS" Then
            resourceName = "_" & resourceName
        End If

        ' Try to get the image, fall back if name was altered
        Dim img = My.Resources.ResourceManager.GetObject(resourceName)
        If img Is Nothing Then
            resourceName = resourceName.Replace("_", "")
            img = My.Resources.ResourceManager.GetObject(resourceName)
        End If

        Return DirectCast(img, Image)
    End Function

    ' Shows card as text like "A♠" or "10♥" - used for debugging
    Public Overrides Function ToString() As String
        Dim ranks() = {"", "", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"}
        Dim suits() = {"♣", "♦", "♥", "♠"}
        Return ranks(Rank) & suits(CardSuit)
    End Function
End Class