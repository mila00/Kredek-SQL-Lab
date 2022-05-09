using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using MichalNajwerLab2.Models;
using Microsoft.Extensions.Configuration;

namespace MichalNajwerLab2.Repository
{
    public class PizzaRepository
    {
        private string _connectionString;

        public PizzaRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("PizzaDb");
        }

        public IList<Pizza> GetPizzas()
        {
            var query =
                "SELECT [PizzaId] AS Id, [Name], [Price], [Description], [ImagePath] AS Image FROM [KredekPizza].[dbo].[Pizzas]";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Pizza>(query).ToList();
            }
        }
    }
}
