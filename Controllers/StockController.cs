﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using Sample_AP.Model;
using Sample_AP.Model.Enum;
using System.Collections.Generic;
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
                SelectDate = [(nowYear - 2).ToString() + "11", (nowYear - 1).ToString() + "12", nowYear.ToString()+"01" ];
                break;
            case 2:
                SelectDate = [(nowYear - 1).ToString() + "12", nowYear.ToString() + "01", nowYear.ToString() + "02"];
                break;
            default:
                SelectDate = [(nowYear).ToString() +"0"+ (nowMonth - 2).ToString(), (nowYear).ToString() + "0" + (nowMonth - 1).ToString(), (nowYear).ToString() + "0" + nowMonth.ToString()];
                break;
        }
        string[] month_reslutURL = new string[SelectDate.Length];
        MonthStockData[] data = new MonthStockData[SelectDate.Length];
        List<string[]> result = new List<string[]>();
        for (int i =0;i< SelectDate.Length; i++)
        {
            month_reslutURL[i] = $"https://www.twse.com.tw/rwd/zh/afterTrading/STOCK_DAY_AVG?date={SelectDate[i]}01&stockNo={stockID}&response=json";
            HttpResponseMessage response = await _httpClient.GetAsync(month_reslutURL[i]);

            response.EnsureSuccessStatusCode();

            //responseBody[i] = await response.Content.ReadAsStringAsync();

            data[i] = JsonSerializer.Deserialize<MonthStockData>(await response.Content.ReadAsStringAsync());
            result.AddRange(data[i].Data.Where(item => item[0].Contains("113/")));
        }
        List<DateStockPrice> _dateStockPrice = new List<DateStockPrice>();
        foreach (string[] s in result)
        {
            DateStockPrice dateStockPrice = new DateStockPrice();
            DateTime dateValue;
            decimal priceValue;
            if (DateTime.TryParse(s[0], out dateValue)& decimal.TryParse(s[1], out priceValue))
            {
                dateStockPrice.Date = dateValue;
                dateStockPrice.StockPrice = priceValue;
            }
            else 
            {
                
            }
            _dateStockPrice.Add(dateStockPrice);
        }
            
        return Ok(_dateStockPrice);
    }
}