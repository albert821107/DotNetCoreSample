using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Sample_AP.Model;
//ddddd
namespace Sample_AP.Controllers;

[ApiController]
[Route("api/")]
public class OrderController : ControllerBase
{
    // 連線字串
    private readonly string _connectionString = "data source=.;initial catalog=Northwind;persist security info=True;Trusted_Connection=True;TrustServerCertificate=true;";

    public OrderController() { }

    [HttpGet]
    [Route("order/{orderID}")]
    public IActionResult GetOrderByID(int orderID)
    {
        string sql = @"
            SELECT *
            FROM Orders
            WHERE OrderID = @OrderID";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            var order = connection.QuerySingleOrDefault<Order>(sql, new { OrderID = orderID });

            return Ok(order);
        }
    }

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
}
