using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Application.RepositoryInterfaces
{
    public interface INotificationRepository
    {
        public Task<bool> SendTaggedUserNotification(string userId, string notifReceiverId);
        public Task<List<string>> GetUserNotifications(string userId);
    }
}
