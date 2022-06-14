using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.RepositoryInterfaces;
using Application.ServiceInterfaces;
using Application.Services;
using Application.Services.Common;
using Core.Entities;
using Infrastructure.SQL.Context;
using Infrastructure.SQL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Endpoint.Configs
{
    public static class AddServices
    {
        public static void AddTweeterServices(this IServiceCollection services)
        {
            services.AddScoped<ITagCrudService, TagCrudService>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IUserCrudService, UserCrudService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITweetRepository, TweetRepository>();
            services.AddScoped<ITweetCrudService, TweetCrudService>();
            services.AddScoped<IRoleCrudService, RoleCrudService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountManageService, AccountManageService>();
            services.AddScoped<INotificationRepository, NotificationRepository>();

            services.AddSingleton<VisitSummaryTweet>();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddRoles<Role>();

        }
    }
}
