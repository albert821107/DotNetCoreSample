using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using Sample_AP.Model;
using Sample_AP.Model.Enum;
using System.Text.Json;

namespace Sample_AP.Controllers;

[ApiController]
[Route("api/")]
public class StockController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public StockController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    [Route("Stock/{stockID}")]
    public async Task<IActionResult> GetStockByID(string stockID)
    {
        string resultURL = $"https://mis.twse.com.tw/stock/api/getStockInfo.jsp?json=1&delay=0&ex_ch=tse_{stockID}.tw";
        //https://mis.twse.com.tw/stock/api/getStockInfo.jsp?ex_ch=tse_2330.tw&json=1&delay=0&_=1717942658851

        HttpResponseMessage response = await _httpClient.GetAsync(resultURL);

        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();

        return Ok(responseBody);
    }

    [HttpGet]
    [Route("Stock/{stockTradeType}/{stockID}")]
    public async Task<IActionResult> GetStockByStockID(StockTradeType stockTradeType, string stockID)
    {
        string stockTradeTypePrefix = stockTradeType.GetDisplayName().ToLower();

        string resultURL = $"https://mis.twse.com.tw/stock/api/getStockInfo.jsp?json=1&delay=0&ex_ch={stockTradeTypePrefix}_{stockID}.tw";

        HttpResponseMessage response = await _httpClient.GetAsync(resultURL);

        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();

        var data = JsonSerializer.Deserialize<StockResponse>(responseBody);

        return Ok(data);
    }

    //查詢歷史股價
    //需求:我要輸入股票代號和股票交易類型，就能得到近三個月或是半年的股價
}