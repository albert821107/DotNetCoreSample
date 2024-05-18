using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using OrganizationSample;
using System.Globalization;

namespace Sample_AP.Controllers;

[ApiController]
[Route("api/")]
public class SearchController : ControllerBase
{
    private readonly string _connectionString = "data source=127.0.0.1;initial catalog=sample;persist security info=True;user id=sa;password=Sample01;TrustServerCertificate=true;";

    public SearchController() { }

    [HttpGet]
    [Route("policyholders")]
    public PolicyHolder SearchPolicyHolder(string code)
    {
        PolicyHolder policyHolder = new PolicyHolder();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string sqlQuery1 = @$"SELECT p.code, p.name, CONVERT(VARCHAR(10), p.registration_date, 111) AS registration_date, p.introducer_code,
                                  pb1.policyholder_code AS left_code, pb2.policyholder_code AS right_code,
	                              pb1_name.name AS left_name, CONVERT(VARCHAR(10), pb1_name.registration_date, 111) AS left_regist_date, pb1_name.introducer_code AS left_intro_code,
	                              pb2_name.name AS right_name, CONVERT(VARCHAR(10), pb2_name.registration_date, 111) AS right_regist_date, pb2_name.introducer_code AS right_intro_code
                                  FROM PolicyHolder p 
                                  LEFT JOIN PolicyHolder_BinaryTree pb ON p.code = pb.policyholder_code
                                  LEFT JOIN PolicyHolder_BinaryTree pb1 ON pb.left_child_id = pb1.id
                                  LEFT JOIN PolicyHolder_BinaryTree pb2 ON pb.right_child_id = pb2.id
                                  LEFT JOIN PolicyHolder pb1_name ON pb1.policyholder_code = pb1_name.code
                                  LEFT JOIN PolicyHolder pb2_name ON pb2.policyholder_code = pb2_name.code
                                  WHERE p.code = '{code}'";


            using (SqlCommand command = new SqlCommand(sqlQuery1, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        policyHolder.Code = reader["code"].ToString();
                        policyHolder.Name = reader["name"].ToString();
                        policyHolder.RegistrationDate = Convert.ToDateTime(reader["registration_date"], CultureInfo.InvariantCulture);
                        policyHolder.IntroducerCode = reader["introducer_code"].ToString();

                        policyHolder.LeftTree = new PolicyHolder()
                        {
                            Code = reader["left_code"].ToString(),
                            Name = reader["left_name"].ToString(),
                            RegistrationDate = Convert.ToDateTime(reader["left_regist_date"], CultureInfo.InvariantCulture),
                            IntroducerCode = reader["left_intro_code"].ToString(),
                        };

                        policyHolder.RightTree = new PolicyHolder()
                        {
                            Code = reader["right_code"].ToString(),
                            Name = reader["right_name"].ToString(),
                            RegistrationDate = Convert.ToDateTime(reader["right_regist_date"], CultureInfo.InvariantCulture),
                            IntroducerCode = reader["right_intro_code"].ToString()
                        };
                    }
                }
            }

            return policyHolder;
        }
    }

    [HttpGet]
    [Route("policyholders/{code}/top")]
    public PolicyHolder SearchTopPolicyHolder(string code)
    {
        PolicyHolder policyHolder = new PolicyHolder();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string sqlQuery1 = @$"SELECT p.code, p.name, CONVERT(VARCHAR(10), p.registration_date, 111) AS registration_date, p.introducer_code,
                                  pb1.policyholder_code AS left_code, pb2.policyholder_code AS right_code,
                                  pb1_name.name AS left_name, CONVERT(VARCHAR(10), pb1_name.registration_date, 111) AS left_regist_date, pb1_name.introducer_code AS left_intro_code,
                                  pb2_name.name AS right_name, CONVERT(VARCHAR(10), pb2_name.registration_date, 111) AS right_regist_date, pb2_name.introducer_code AS right_intro_code
                                  FROM PolicyHolder p 
                                  LEFT JOIN PolicyHolder_BinaryTree pb ON p.code = pb.policyholder_code
                                  LEFT JOIN PolicyHolder_BinaryTree pb1 ON pb.left_child_id = pb1.id
                                  LEFT JOIN PolicyHolder_BinaryTree pb2 ON pb.right_child_id = pb2.id
                                  LEFT JOIN PolicyHolder pb1_name ON pb1.policyholder_code = pb1_name.code
                                  LEFT JOIN PolicyHolder pb2_name ON pb2.policyholder_code = pb2_name.code
                                  WHERE p.code = (	
                                  	SELECT introducer_code 
                                  	FROM PolicyHolder 
                                  	WHERE code = '{code}'
                                  )";


            using (SqlCommand command = new SqlCommand(sqlQuery1, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        policyHolder.Code = reader["code"].ToString();
                        policyHolder.Name = reader["name"].ToString();
                        policyHolder.RegistrationDate = Convert.ToDateTime(reader["registration_date"], CultureInfo.InvariantCulture);
                        policyHolder.IntroducerCode = reader["introducer_code"].ToString();

                        policyHolder.LeftTree = new PolicyHolder()
                        {
                            Code = reader["left_code"].ToString(),
                            Name = reader["left_name"].ToString(),
                            RegistrationDate = Convert.ToDateTime(reader["left_regist_date"], CultureInfo.InvariantCulture),
                            IntroducerCode = reader["left_intro_code"].ToString(),
                        };

                        policyHolder.RightTree = new PolicyHolder()
                        {
                            Code = reader["right_code"].ToString(),
                            Name = reader["right_name"].ToString(),
                            RegistrationDate = Convert.ToDateTime(reader["right_regist_date"], CultureInfo.InvariantCulture),
                            IntroducerCode = reader["right_intro_code"].ToString()
                        };
                    }
                }
            }

            return policyHolder;
        }
    }
}