using System.Text.Json.Serialization;

namespace Sample_AP.Model
{
    public class MonthStockData
    {
        [JsonPropertyName("stat")]
        public string Stat { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("fields")]
        public List<string> Fields { get; set; }

        [JsonPropertyName("data")]
        public List<string[]> Data { get; set; }

        [JsonPropertyName("notes")]
        public List<string> Notes { get; set; }

        [JsonPropertyName("hints")]
        public string Hints { get; set; }
    }
    public class DateStockPrice
    {
        public DateTime Date { get; set; }
        public decimal StockPrice { get; set; }
    }
}
