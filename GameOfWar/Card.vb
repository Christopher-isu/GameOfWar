'====================================================================
' Christopher Z
' Fall 2025
' RCET 3375
' https://github.com/Christopher-isu/GameOfWar.git
' The Game of War
'====================================================================
' Card.vb
' Represents a single playing card (rank + suit) and provides access
' to its corresponding image in the Resources. This class is used by
' the game logic to build a standard 52-card deck, compare card ranks,
' and by the GUI to display the correct card faces during play/war.
'====================================================================
Option Strict On
Option Explicit On

Public Class Card
    Implements IComparable(Of Card)

    ' Rank of this card (2–14, where 11=J, 12=Q, 13=K, 14=Ace).
    ' Using an Integer makes comparisons straightforward in the game logic.
    Public ReadOnly Property Rank As Integer

    ' Suit of this card (Clubs, Diamonds, Hearts, Spades).
    ' An Enum makes suits type-safe and self-describing.
    Public ReadOnly Property CardSuit As CardSuitEnum

    ' The exact filename of this card’s image in the Resources, e.g. "10H.jpg".
    ' This is derived from Rank + Suit when the card is constructed.
    Public ReadOnly Property FileName As String

    ' Enumeration of allowed suits. The numeric values (0–3) line up with loops
    ' that build the full deck.
    Public Enum CardSuitEnum
        Clubs
        Diamonds
        Hearts
        Spades
    End Enum

    ' Constructor: creates a card with the given rank and suit.
    ' This is used when building the 52-card deck at game start.
    Public Sub New(rank As Integer, suit As CardSuitEnum)
        ' Enforce valid rank range once at construction. This prevents creating
        ' invalid cards and guarantees all game logic sees only legal ranks.
        If rank < 2 OrElse rank > 14 Then
            Throw New ArgumentException("Rank must be 2-14")
        End If

        Me.Rank = rank
        Me.CardSuit = suit

        ' Convert numeric rank to the text used in your image filenames:
        ' 11 → "J", 12 → "Q", 13 → "K", 14 → "A", otherwise the number itself.
        Dim rankStr As String = If(rank = 14, "A",
                                 If(rank = 11, "J",
                                 If(rank = 12, "Q",
                                 If(rank = 13, "K", rank.ToString()))))

        ' Map suit enum to the one-letter suffix used in filenames:
        ' Clubs → "C", Diamonds → "D", Hearts → "H", Spades → "S".
        Dim suitChar As Char = If(suit = CardSuitEnum.Clubs, "C"c,
                              If(suit = CardSuitEnum.Diamonds, "D"c,
                              If(suit = CardSuitEnum.Hearts, "H"c, "S"c)))

        ' Build the filename that matches the resource names, e.g. "10H.jpg".
        Me.FileName = rankStr & suitChar & ".jpg"
    End Sub

    ' Compare this card to another card by rank. This is the core operation
    ' to determine who wins a battle/war card comparison.
    Public Function CompareTo(other As Card) As Integer Implements IComparable(Of Card).CompareTo
        Return Me.Rank.CompareTo(other.Rank)
    End Function

    ' Retrieves the Image object for this card from the embedded Resources.
    ' This enables the GUI to display the card face using a simple call.
    Public Function GetImage() As Image
        ' Strip the extension to derive the base resource name.
        Dim resourceName As String = FileName.Replace(".jpg", "")

        ' Some resource managers prefix names beginning with digits or "AS"
        ' with an underscore. This logic accounts for that naming quirk.
        If Char.IsDigit(resourceName(0)) OrElse resourceName = "AS" Then
            resourceName = "_" & resourceName
        End If

        ' Look up the resource by name. This uses the ResourceManager so we
        ' don't hard-code 52 separate resource properties.
        Dim img As Object = My.Resources.ResourceManager.GetObject(resourceName)

        ' If not found under the underscore-prefixed name, try again without it.
        ' This makes the code more robust against how the resources were named.
        If img Is Nothing Then
            resourceName = resourceName.Replace("_", "")
            img = My.Resources.ResourceManager.GetObject(resourceName)
        End If

        ' Cast to Image so callers get a strongly typed result.
        Return DirectCast(img, Image)
    End Function

End Class
