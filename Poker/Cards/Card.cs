namespace Poker.Cards;

public enum SuitTypes
{
    Diamonds,
    Clubs,
    Hearts,
    Spades
}

public class Card
{
    public int Ranking { get; private set; }
    public string Name { get; private set; }
    public SuitTypes SuitType { get; private set; }

    public Card(string name, SuitTypes suitType)
    {
        this.Name = name;
        this.SuitType = suitType;
        if (name == "jack")
        {
            this.Ranking = 11;
        }
        else if (name == "queen")
        {
            this.Ranking = 12;
        }
        else if (name == "king")
        {
            this.Ranking = 13;
        }
        else if (name == "ace")
        {
            Ranking = 14;
        }
        else
        {
            int result;
            bool parsed = Int32.TryParse(name, out result);
            Ranking = result;
        }
    }

 
}