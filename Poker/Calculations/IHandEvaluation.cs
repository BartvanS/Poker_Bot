using Poker.Cards;

namespace Poker.Calculations;

public interface IHandEvaluation
{
    public Rules.Hands DetermineHand(List<Card> cards);
    
    /// <summary>
    /// Hand strength is valued on a scale of 1 to 7462, where 1 is a Royal Flush and 7462 is unsuited 7-5-4-3-2, as there are only 7642 distinctly ranked hands in poker.
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public int GetScore(List<Card> cards);

    /// <summary>
    /// Hand strength defined as a percentage
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public int GetScoreAsPercentage(List<Card> cards);



}