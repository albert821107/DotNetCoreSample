using Microsoft.AspNetCore.Mvc;

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

        HttpResponseMessage response = await _httpClient.GetAsync(resultURL);

        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();

        return Ok(responseBody);
    }
}