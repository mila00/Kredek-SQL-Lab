using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MichalNajwerLab2.Models;
using Microsoft.Extensions.Configuration;

namespace MichalNajwerLab2.Repository
{
    public class OrderRepository
    {
        private string _connectionString;

        public OrderRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("PizzaDb");
        }

        public void InsertOrder(Order order)
        {
            var query =
                "INSERT INTO Orders ([PizzaId],[City],[Address],[Email],[PhoneNumber],[OrderDate]) " +
                "VALUES (@PizzaId, @City, @Address, @Email, @PhoneNumber, @OrderDate)";

            var queryParams = new
            {
                PizzaId = order.PizzaId,
                City = order.City,
                Address = order.Adress,
                Email = order.Email,
                PhoneNumber = order.Phone,
                OrderDate = order.OrderDate
            };

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, queryParams);
            }
        }
    }
}
