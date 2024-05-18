using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Sample_AP.Model;

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
                    var aaa = reader;

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

    [HttpGet]
    [Route("user")]
    public IActionResult GetAllUsers()
    {
        List<Customer> result = new List<Customer>();


        //todo

        return Ok(result);
    }

    [HttpPut]
    [Route("user")]
    public IActionResult UpdateUserName(string contactName)
    {
        bool result = false;

        //todo

        return Ok(result);
    }
}