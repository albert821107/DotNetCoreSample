﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Sample_AP.Model;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Numerics;

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
                            FROM Customers ORDER BY CustomerID";

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
    public IActionResult UpdateUserName(string customerID, string contactName)
    {
        bool result = false;

        //todo

        return Ok(result);
    }
}