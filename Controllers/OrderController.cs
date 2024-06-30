using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Sample_AP.Service;

namespace Sample_AP.Controllers;

[ApiController]
[Route("api/")]
public class OrderController : ControllerBase
{
    // 連線字串
    private readonly string _connectionString = "data source=.;initial catalog=Northwind;persist security info=True;Trusted_Connection=True;TrustServerCertificate=true;";

    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    //Controller 參數整理  回傳結果

    //Service 其他事情

    //Repo 跟資料庫做連接，取得DATA

    [HttpGet]
    [Route("order/{orderID}")]
    public IActionResult GetOrderByID(int orderID)
    {
        var result = _orderService.GetOrderByID(orderID);

        return Ok(result);
    }

    //TODO Create(小陸)

    //TODO Update(小陸)

    //TODO Delete(小陸)

    [HttpDelete]
    [Route("order/{orderID}")]
    public IActionResult DeleteOrderByID(int orderID)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            var parameters = new { OrderID = orderID };

            // 使用 Dapper 調用預存程序
            connection.Execute("DeleteOrderDataBYID", parameters, commandType: System.Data.CommandType.StoredProcedure);

            return Ok("完成刪除資料");
        }
    }

    private int Add(int a ,int b )
    {
        return a + b;
    }

    private int Add(int a, int b,int c)
    {
        return a + b+c;
    }
}
