namespace Poker.Cards;

public class Player
{
    public string Name { get; private set; }
    public List<Card> CardsInHand { get; set; } = new List<Card>();
    public int? StoppedBeforeTurn;
    public Player(string name)
    {
        this.Name = name;
    }
    public string CardsInHandToString()
    {
        string CardsInHandString = "";
        foreach (var card in CardsInHand)
        {
            CardsInHandString += card.Name + ",";
        }

        return CardsInHandString;
    }
}