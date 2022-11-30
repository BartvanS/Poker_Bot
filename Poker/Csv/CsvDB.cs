using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Poker.Cards.Data;

namespace Poker.Csv;

public class CsvDB
{
    public string FileName { get; private set; }

    public CsvDB(string fileName)
    {
        this.FileName = fileName;
    }

    public void CreateCsvFile()
    {
        using (var writer = new StreamWriter(this.FileName))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteHeader<PlayerChoicesCsv>();
            csv.NextRecord();
        }
    }

    public void WriteToCsv(List<PlayerChoicesCsv> data)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            // Don't write the header again.
            HasHeaderRecord = false,
        };
        using (var stream = File.Open(this.FileName, FileMode.Append))
        using (var writer = new StreamWriter(stream))
        using (var csv = new CsvWriter(writer, config))
        {
            csv.WriteRecords(data);
        }
    }
}