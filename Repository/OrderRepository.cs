using Microsoft.Data.SqlClient;
using Sample_AP.Model;
using Dapper;

namespace Sample_AP.Repository;

public class OrderRepository 
{
    // 連線字串
    private readonly string _connectionString = "data source=.;initial catalog=Northwind;persist security info=True;Trusted_Connection=True;TrustServerCertificate=true;";

    public Order GetOrderByID(int orderID)
    {
        string sql = @"
            SELECT *
            FROM Orders
            WHERE OrderID = @OrderID";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            var order = connection.QuerySingleOrDefault<Order>(sql, new { OrderID = orderID });

            return order;
        }
    }
}
