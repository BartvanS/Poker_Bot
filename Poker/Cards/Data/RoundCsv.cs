namespace Poker.Cards.Data;

public class RoundCsv
{
    public int RoundNr{ get; set; }
    public int EndedAtTurn { get; set; }
    public Player Winner{ get; set; }
    public List<Card> CardsOnTable{ get; set; }

}