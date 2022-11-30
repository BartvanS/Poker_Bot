using Poker.Cards;

namespace Poker;


public class DealerOld
{
    private readonly Random _random = new Random();
    private List<Card> CardsInDeck = new List<Card>();
    public List<Card> OpenCards = new List<Card>();

    public int RoundNr { get; private set; } = 0;
    public int TurnNr { get; private set; } = 0;
    public string[] CardNames { get; private set; } =
        { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king" };

    private List<Player> _playersInRound;
    public void SetupNextRound(List<Player> players)
    {
        _playersInRound = players;
        foreach (var player in players)
        {
            player.CardsInHand.Clear();
        }
        //Add count after each new round
        RoundNr++;
        //Each round resets its turns
        TurnNr = 0;
        //Setup all cards in the poker round
        this.CardsInDeck.Clear();
        this.OpenCards.Clear();
        foreach (string cardName in CardNames)
        {
            this.CardsInDeck.Add(new Card(cardName, SuitTypes.Clubs));
            this.CardsInDeck.Add(new Card(cardName, SuitTypes.Diamonds));
            this.CardsInDeck.Add(new Card(cardName, SuitTypes.Spades));
            this.CardsInDeck.Add(new Card(cardName, SuitTypes.Hearts));
        }
    }

    public void NextTurn()
    {
        TurnNr++;
        switch (TurnNr)
        {
            case 1: FirstTurn();
                break;
            case 2: SecondTurn();
                break;
            case 3: ThirdTurn();
                break;
            case 4: FourthTurn();
                break;
            default:
            {
                EndOfRound();
                TurnNr = 0;
                break;
            }
        }
    }

    private void EndOfRound()
    {
        //something something end of round
        Console.WriteLine("no more turns left");
    }
    private void GiveCardsToPlayer(Player player, int amountOfCards)
    {
        for (var i = 0; i < amountOfCards; i++)
        {
            int randomNr = _random.Next(CardsInDeck.Count - 1);
            Card card = this.CardsInDeck[randomNr];
            player.CardsInHand.Add(card);
            this.CardsInDeck.RemoveAt(randomNr);
        }
    }

    private void AddCardsToTable(int amountOfCards)
    {
        for (var i = 0; i < amountOfCards; i++)
        {
            int randomNr = _random.Next(CardsInDeck.Count - 1);
            Card card = this.CardsInDeck[randomNr];
            OpenCards.Add(card);
            this.CardsInDeck.RemoveAt(randomNr);
        }
    }

//first turn
    public void FirstTurn()
    {
        // bot receives 2 cards from pot and reads them
        foreach (var player in this._playersInRound)
        {
            GiveCardsToPlayer(player, 2);
        }
    }

//"flop" turn where 3 cards lay on the table
/*
 * returns: All cards public on the table
 */
    public List<Card> SecondTurn()
    {
        AddCardsToTable(3);
        return this.OpenCards;
    }

//third turn "the turn". Now there are 4 open cards on the table
    public void ThirdTurn()
    {
        AddCardsToTable(1);
    }

//"river" turn. Now there are 5 open cards on the table
    public void FourthTurn()
    {
        AddCardsToTable(1);
    }
}