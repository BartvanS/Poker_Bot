using Poker.Cards;
namespace Poker.Calculations;

public class PokerHandEvaluation: IHandEvaluation
{
    public Rules.Hands DetermineHand(List<Card> cards)
    {
        return Rules.Hands.Flush;
    }
    
    public int GetScore(List<Card> cards){
        return 1;
    }

    public int GetScoreAsPercentage(List<Card> cards)
    {
        return Map(GetScore(cards), 1, 7462, 0, 100);
    }
    private int Map(int value, int in_min, int in_max, int out_min, int out_max)
    {
        return (value - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}