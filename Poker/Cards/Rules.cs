namespace Poker.Cards;

public class Rules
{
    public enum Hands
    {
        OnePair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush,
        HighCard
    }

    public enum Options
    {
        Call,
        Bet,
        Check,
        Raise,
        Fold,
    }
    //returns the available hand based on the given cards
    public Hands DetermineHandByCards(List<Card> cards)
    {
        return Hands.Flush;
    }
}