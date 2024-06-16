using ClosedXML.Excel;
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
    [Route("Stock/{stockTradeType}/{stockID}")]
    public async Task<IActionResult> GetStockByStockID(StockTradeType stockTradeType, string stockID)
    {
        string stockTradeTypePrefix = stockTradeType.GetDisplayName().ToLower();

        string resultURL = $"https://mis.twse.com.tw/stock/api/getStockInfo.jsp?json=1&delay=0&ex_ch={stockTradeTypePrefix}_{stockID}.tw";

        HttpResponseMessage response = await _httpClient.GetAsync(resultURL);

        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();

        var data = JsonSerializer.Deserialize<StockResponse>(responseBody);//DataModel <=> DomainModel <=> ViewModel

        return Ok(data);
    }

    //查詢歷史股價
    //需求:我要輸入股票代號和股票交易類型，就能得到近三個月或是半年的股價
    [HttpGet]
    [Route("Stock_PastThreeMonthDate/{stockID}")]
    public async Task<IActionResult> GetStock_PastThreeMonthDate_ByStockID(string stockID)
    {
        int nowYear = DateTime.Now.Year;
        int nowMonth = DateTime.Now.Month;
        string[] SelectDate = new string[3];
        switch (nowMonth)
        {
            case 1:
                SelectDate = [(nowYear - 2).ToString() + "11", (nowYear - 1).ToString() + "12", nowYear.ToString() + "01"];
                break;
            case 2:
                SelectDate = [(nowYear - 1).ToString() + "12", nowYear.ToString() + "01", nowYear.ToString() + "02"];
                break;
            default:
                SelectDate = [(nowYear).ToString() + "0" + (nowMonth - 2).ToString(), (nowYear).ToString() + "0" + (nowMonth - 1).ToString(), (nowYear).ToString() + "0" + nowMonth.ToString()];
                break;
        }
        //string resultURL = $"https://www.twse.com.tw/rwd/zh/afterTrading/STOCK_DAY_AVG?date=2024{month}01&stockNo={stockID}&response=json";
        string[] month_reslutURL = new string[SelectDate.Length];
        //string[] responseBody =new string[month.Length];
        MonthStockData[] data = new MonthStockData[SelectDate.Length];
        List<List<string>> result = new List<List<string>>();
        for (int i = 0; i < SelectDate.Length; i++)
        {
            month_reslutURL[i] = $"https://www.twse.com.tw/rwd/zh/afterTrading/STOCK_DAY_AVG?date={SelectDate[i]}01&stockNo={stockID}&response=json";
            HttpResponseMessage response = await _httpClient.GetAsync(month_reslutURL[i]);

            response.EnsureSuccessStatusCode();

            //responseBody[i] = await response.Content.ReadAsStringAsync();

            data[i] = JsonSerializer.Deserialize<MonthStockData>(await response.Content.ReadAsStringAsync());
            result.AddRange(data[i].Data.Where(item => item[0].Contains("113/")).ToList());
        }

        return Ok(result);
    }

    [HttpPost]
    [Route("Stock/ExportStockExcel/{stockID}")]
    public async Task<IActionResult> ExportStockExcel(string stockID)
    {
        List<DateTime> dates = new List<DateTime>();//要加日期
        List<decimal> prices = new List<decimal> { 123, 124, 126, 123, 124, 126, 123, 124, 126, 123, 124, 126, 127, 129, 130 };
        decimal avg = 125;


        // 步驟一 


        decimal a = 1;//假設(需要計算)
        List<decimal> pricesA = new List<decimal> { avg + a, avg + a, avg + a, avg + a, avg + a };//後面壓上去
        List<decimal> pricesB = new List<decimal> { avg - a, avg - a, avg - a, avg - a, avg - a };//後面壓上去

        // 步驟二 輸入價格和時間就自己整理成excel
        string result = CreateExcel(dates, prices);

        return Ok(result);
    }

    //TODO 計算標準差
    private decimal CalcStandardDeviation (List<decimal> prices )
    {
        return 1;
    }

    private string CreateExcel(List<DateTime> dates, List<decimal> prices)
    {
        // 創建一個新的工作簿
        var workbook = new XLWorkbook();

        // 添加工作表
        var worksheet = workbook.Worksheets.Add("布林");

        // 寫入數據
        worksheet.Cell("A1").Value = "日期";
        worksheet.Cell("B1").Value = "股價";

        for (int i = 0; i < prices.Count; i++)
        {
            worksheet.Cell($"A{i + 2}").Value = DateTime.Now;
            worksheet.Cell($"B{i + 2}").Value = prices[i];
        }

        // 保存文件
        workbook.SaveAs("Stock.xlsx");

        return "下載成功";
    }
}