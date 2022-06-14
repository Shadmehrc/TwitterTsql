using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Endpoint.Configs
{
    public static class AddJwtConfig
    {

        public static void AddJwtAuthorization(this IServiceCollection services, TokenModel tokenSettings)
        {
            services.AddAuthentication(option =>
                {
                    option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(configureOptions =>
                {
                    configureOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = tokenSettings.issuer,
                        ValidAudience = tokenSettings.audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Key)),
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                    };
                    configureOptions.SaveToken = true;
                });
        }
    }
}
