using Api.Models.WardMax;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Api.Data.WardMax
{
    public class DataPortal
    {

        string connString = "";

        public void sqlTest()
        {
            using (var connection = new SqlConnection(connString))
            {
                string sampleSql = "SELECT * FROM CreditCard";

                // Use the Query method to execute the query and return a list of objects    
                //var books = connection.Query<Book>(sql, new { authorName = "John Smith" }).ToList();
                var creditCards = connection.QueryAsync<CreditCard>(sampleSql);

                foreach (var creditCard in creditCards.Result)
                {
                    Console.WriteLine("ID: " + creditCard.Id);
                    Console.WriteLine("Card Name: " + creditCard.Name);
                }


            }
        }


    }
}
