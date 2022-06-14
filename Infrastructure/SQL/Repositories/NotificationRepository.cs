using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.RepositoryInterfaces;
using Core.Entities;
using Dapper;
using Infrastructure.SQL.Context;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.SQL.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly string _connectionString;


        public NotificationRepository(IConfiguration configuration)
        {
            _connectionString = configuration["connection"];
        }

        public Task<bool> SendTaggedUserNotification(string userId, string notifReceiverId)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            var result = sqlConnection.Query<bool>("SendUserTaggedNotification",
                new { UserId = userId, notifReceiverId = notifReceiverId },
                commandType: CommandType.StoredProcedure);
            return Task.FromResult(true);
        }

        public Task<List<string>> GetUserNotifications(string userId)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            var result = sqlConnection.Query<String>("UserNotifications",
                new { UserId = userId },
                commandType: CommandType.StoredProcedure).ToList();
            return Task.FromResult(result);

        }
    }
}
