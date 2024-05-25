using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Data.SqlClient;
using Sample_AP.Model;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection.PortableExecutable;

namespace Sample_AP.Controllers;

[ApiController]
[Route("api/")]
public class UserController : ControllerBase
{
    // 連線字串
    private readonly string _connectionString = "data source=.;initial catalog=Northwind;persist security info=True;Trusted_Connection=True;TrustServerCertificate=true;";

    public UserController() { }

    [HttpGet]
    [Route("user/{customerID}")]
    public IActionResult GetUserByID(string customerID)
    {
        Customer result = new Customer();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string sql = @$"SELECT * 
                            FROM Customers 
                            WHERE CustomerID = '{customerID}'";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        result.CustomerID = reader["CustomerID"].ToString();
                        result.CompanyName = reader["CompanyName"].ToString();
                        result.ContactName = reader["ContactName"].ToString();
                        result.ContactTitle = reader["ContactTitle"].ToString();
                        result.Address = reader["Address"].ToString();
                        result.City = reader["City"].ToString();
                        result.Region = reader["Region"].ToString();
                        result.PostalCode = reader["PostalCode"].ToString();
                        result.Country = reader["Country"].ToString();
                        result.Phone = reader["Phone"].ToString();
                        result.Fax = reader["Fax"].ToString();
                    }
                }
            }

            return Ok(result);
        }
    }

    // Memo 說明建構式
    [HttpGet]
    [Route("user")]
    public IActionResult GetAllUsers()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string sql = @$"SELECT TOP 20 *
                            FROM Customers
                            ORDER BY CustomerID";

            List<Customer> result = new List<Customer>();

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer newcustomer = new Customer
                        {
                            CustomerID = reader["CustomerID"].ToString(),
                            CompanyName = reader["CompanyName"].ToString(),
                            ContactName = reader["ContactName"].ToString(),
                            ContactTitle = reader["ContactTitle"].ToString(),
                            Address = reader["Address"].ToString(),
                            City = reader["City"].ToString(),
                            Region = reader["Region"].ToString(),
                            PostalCode = reader["PostalCode"].ToString(),
                            Country = reader["Country"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Fax = reader["Fax"].ToString(),
                        };

                        result.Add(newcustomer);
                    }

                    reader.Close();
                }
            }

            return Ok(result);
        }
    }

    [HttpPut]
    [Route("user")]
    public void UpdateUserName(string customerID, string contactName)
    {
        
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            
            connection.Open();

            string sql = @$"UPDATE Customers
                            SET ContactName = '{contactName}'
                            WHERE CustomerID = '{customerID}'";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }

    }
    /*
     * 
DELETE od
    FROM [Order Details] AS od
    INNER JOIN Orders AS o
        ON od.OrderID = o.OrderID
    INNER JOIN Customers AS c
        ON o.CustomerID = c.CustomerID
    WHERE c.CustomerID = 'ALFKI'

DELETE o
    FROM Orders AS o
    INNER JOIN Customers AS c
        ON o.CustomerID = c.CustomerID
    WHERE o.CustomerID = 'ALFKI'

DELETE FROM Customers
    WHERE CustomerID = 'ALFKI'
     */
    [HttpDelete]
    [Route("user")]
    public void Deletecustomer(string customerID)
    {

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {

            connection.Open();

            string sql = @$"DELETE FROM Customers
                            WHERE CustomerID = '{customerID}'";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }

    }
    [HttpPost]
    [Route("user")]
    public void Postcustomer()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sql = @$"INSERT INTO Customers
                            VALUES ('ALFK2','Alfreds Futterkiste','asdasd','Sales Representative','Obere Str. 57'
                            ,'Berlin','','12209','Germany','030-0074321','030-0076545')";
            using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }         
        }
    }
}