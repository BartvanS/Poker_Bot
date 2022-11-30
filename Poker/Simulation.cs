using System.Globalization;
using CsvHelper;
using Poker.Cards;
using Poker.Cards.Data;
using Poker.Csv;

namespace Poker;

public class Simulation
{
    private readonly int _simulationCount = 1;
    private readonly int _playerCount = 4;
    private List<Player> players = new List<Player>();
    private Dealer _dealer = new();
    private Random _random = new();
    private CsvDB _csvDb;

    public Simulation()
    {
        //for simulation we have x players
        for (int i = 0; i < _playerCount; i++)
        {
            players.Add(new Player($"Player_{i}"));
        }

        //create database
        _csvDb = new("test.csv");
        _csvDb.CreateCsvFile();
        //Do simulated Rounds
        for (int i = 0; i < _simulationCount; i++)
        {
            SimulateRound();
        }
    }

    private void SimulateRound()
    {
        //reset dealer
        _dealer.SetupNextRound(players);
        for (int i = 1; i <= 4; i++)
        {
            _dealer.NextTurn();
            SavePlayerData();
        }
    }
    
    private void SavePlayerData()
    {
        List<PlayerChoicesCsv> turn_data = new List<PlayerChoicesCsv>();
        for (int i = 0; i < _playerCount; i++)
        {
                turn_data.Add(GeneratePlayerDataRow(players[i]));
        }
        _csvDb.WriteToCsv(turn_data);
    }

    private void SaveEndOfRoundData()
    {
        
    }
    private PlayerChoicesCsv GeneratePlayerDataRow(Player player)
    {
        var allOptions = Enum.GetValues(typeof(Rules.Options));
        Rules.Options option = (Rules.Options)allOptions.GetValue(_random.Next(allOptions.Length));
        return new PlayerChoicesCsv()
        {
            Decision = option,
            PlayerName = player.Name,
            RoundNr = _dealer.RoundNr,
            turn = _dealer.TurnNr,
            CardsInHand = player.CardsInHandToString(),
        };
    }
}