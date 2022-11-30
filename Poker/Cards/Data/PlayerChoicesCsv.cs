namespace Poker.Cards.Data;

public class PlayerChoicesCsv
{
    public int RoundNr { get; set; }
    public string PlayerName{ get; set; }
    public int turn{ get; set; }
    public Rules.Options Decision{ get; set; }
    public string CardsInHand { get; set; }
    
}