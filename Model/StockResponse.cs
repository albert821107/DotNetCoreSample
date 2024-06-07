using System.Text.Json.Serialization;

namespace Sample_AP.Model;

public class StockResponse
{
    [JsonPropertyName("msgArray")]
    public List<MsgArray> MsgArray { get; set; }

    [JsonPropertyName("referer")]
    public string Referer { get; set; }

    [JsonPropertyName("userDelay")]
    public int UserDelay { get; set; }

    [JsonPropertyName("rtcode")]
    public string Rtcode { get; set; }

    [JsonPropertyName("queryTime")]
    public QueryTime QueryTime { get; set; }

    [JsonPropertyName("rtmessage")]
    public string Rtmessage { get; set; }

    [JsonPropertyName("exKey")]
    public string ExKey { get; set; }

    [JsonPropertyName("cachedAlive")]
    public int CachedAlive { get; set; }
}

public class QueryTime
{
    [JsonPropertyName("sysDate")]
    public string SysDate { get; set; }

    [JsonPropertyName("stockInfoItem")]
    public int StockInfoItem { get; set; }

    [JsonPropertyName("stockInfo")]
    public int StockInfo { get; set; }

    [JsonPropertyName("sessionStr")]
    public string SessionStr { get; set; }

    [JsonPropertyName("sysTime")]
    public string SysTime { get; set; }

    [JsonPropertyName("showChart")]
    public bool ShowChart { get; set; }

    [JsonPropertyName("sessionFromTime")]
    public int SessionFromTime { get; set; }

    [JsonPropertyName("sessionLatestTime")]
    public int SessionLatestTime { get; set; }
}

public class MsgArray
{
    [JsonPropertyName("tv")]
    public string Tv { get; set; }

    [JsonPropertyName("ps")]
    public string Ps { get; set; }

    [JsonPropertyName("pz")]
    public string Pz { get; set; }

    [JsonPropertyName("bp")]
    public string Bp { get; set; }

    [JsonPropertyName("fv")]
    public string Fv { get; set; }

    [JsonPropertyName("oa")]
    public string Oa { get; set; }

    [JsonPropertyName("ob")]
    public string Ob { get; set; }

    [JsonPropertyName("a")]
    public string A { get; set; }

    [JsonPropertyName("b")]
    public string B { get; set; }

    [JsonPropertyName("c")]
    public string C { get; set; }

    [JsonPropertyName("d")]
    public string D { get; set; }

    [JsonPropertyName("ch")]
    public string Ch { get; set; }

    [JsonPropertyName("ot")]
    public string Ot { get; set; }

    [JsonPropertyName("tlong")]
    public string Tlong { get; set; }

    [JsonPropertyName("f")]
    public string F { get; set; }

    [JsonPropertyName("ip")]
    public string Ip { get; set; }

    [JsonPropertyName("g")]
    public string G { get; set; }

    [JsonPropertyName("mt")]
    public string Mt { get; set; }

    [JsonPropertyName("ov")]
    public string Ov { get; set; }

    [JsonPropertyName("h")]
    public string H { get; set; }

    [JsonPropertyName("i")]
    public string I { get; set; }

    [JsonPropertyName("it")]
    public string It { get; set; }

    [JsonPropertyName("oz")]
    public string Oz { get; set; }

    [JsonPropertyName("l")]
    public string L { get; set; }

    [JsonPropertyName("n")]
    public string N { get; set; }

    [JsonPropertyName("o")]
    public string O { get; set; }

    [JsonPropertyName("p")]
    public string P { get; set; }

    [JsonPropertyName("ex")]
    public string Ex { get; set; }

    [JsonPropertyName("s")]
    public string S { get; set; }

    [JsonPropertyName("t")]
    public string T { get; set; }

    [JsonPropertyName("u")]
    public string U { get; set; }

    [JsonPropertyName("v")]
    public string V { get; set; }

    [JsonPropertyName("w")]
    public string W { get; set; }

    [JsonPropertyName("nf")]
    public string Nf { get; set; }

    [JsonPropertyName("y")]
    public string Y { get; set; }

    [JsonPropertyName("z")]
    public string Z { get; set; }

    [JsonPropertyName("ts")]
    public string Ts { get; set; }
}
