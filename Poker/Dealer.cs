using Poker.Cards;

namespace Poker;

//The dealer is responsible for giving and taking cards from the deck
public class Dealer
{
    private readonly Random _random = new Random();
    private List<Card> CardsInDeck = new List<Card>();

    /// <summary>
    /// Resets the current deck the dealer has. This does NOT clean all items holding cards like players hands.
    /// </summary>
    /// <param name="cards"></param>
    public void SetDeck(List<Card> cards)
    {
        this.CardsInDeck.Clear();
        this.CardsInDeck = cards;
    }

    /// <summary>
    /// Add cards to the current deck.
    /// </summary>
    /// <param name="cards"></param>
    public void AddToDeck(List<Card> cards)
    {
        this.CardsInDeck.AddRange(cards);
    }

/// <summary>
/// Gives a certain amount of cards from the deck
/// </summary>
/// <param name="amountOfCards"></param>
/// <returns>Cards from deck</returns>
    public List<Card> GiveCards(int amountOfCards)
    {
        List<Card> cards = new();
        for (var i = 0; i < amountOfCards; i++)
        {
            int randomNr = _random.Next(this.CardsInDeck.Count - 1);
            Card card = this.CardsInDeck[randomNr];
            cards.Add(card);
            this.CardsInDeck.RemoveAt(randomNr);
        }

        return cards;
    }
}