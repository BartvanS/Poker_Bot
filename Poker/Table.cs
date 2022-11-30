using Poker.Cards;

namespace Poker;

public class Table
{
    public List<Card> OpenCards = new List<Card>();

    public int RoundNr { get; private set; } = 0;
    public int TurnNr { get; private set; } = 0;

    private Dealer _dealer = new();
    private List<Player> _playersInRound = new();

    public void SetupNextRound(List<Player> players)
    {
        //Add count after each new round
        this.RoundNr++;
        //Each round resets its turns
        this.TurnNr = 0;
        //Empty all cards
        this.OpenCards.Clear();
        _playersInRound = players;
        foreach (var player in players)
        {
            player.CardsInHand.Clear();
        }

        //reset dealer deck
        SetupDeck();
    }

    private void SetupDeck()
    {
        string[] cardNames =
            { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king" };
        List<Card> newDeck = new();
        foreach (string cardName in cardNames)
        {
            newDeck.Add(new(cardName, SuitTypes.Clubs));
            newDeck.Add(new(cardName, SuitTypes.Diamonds));
            newDeck.Add(new(cardName, SuitTypes.Spades));
            newDeck.Add(new(cardName, SuitTypes.Hearts));
        }

        _dealer.SetDeck(newDeck);
    }

    public void NextTurn()
    {
        TurnNr++;
        switch (TurnNr)
        {
            case 1:
                FirstTurn();
                break;
            case 2:
                SecondTurn();
                break;
            case 3:
                ThirdTurn();
                break;
            case 4:
                FourthTurn();
                break;
            case 5:
            {
                break;
                //game over
            }
            default:
            {
                Console.WriteLine("Something went wrong");
                break;
            }
        }
        CheckGameStatus()
    }


//first turn
    private void FirstTurn()
    {
        // bot receives 2 cards from pot and reads them
        foreach (var player in this._playersInRound)
        {
            List<Card> cards = _dealer.GiveCards(2);
            player.CardsInHand.AddRange(cards);
        }
    }

//"flop" turn where 3 cards lay on the table
/*
 * returns: All cards public on the table
 */
    private void SecondTurn()
    {
        List<Card> cards = _dealer.GiveCards(3);
        this.OpenCards.AddRange(cards);
    }

//third turn "the turn". Now there are 4 open cards on the table
    private void ThirdTurn()
    {
        List<Card> cards = _dealer.GiveCards(1);
        this.OpenCards.AddRange(cards);
    }

//"river" turn. Now there are 5 open cards on the table
    private void FourthTurn()
    {
        List<Card> cards = _dealer.GiveCards(1);
        this.OpenCards.AddRange(cards);
    }
}