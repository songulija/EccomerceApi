﻿using EcommerceCore.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Reflection;
using System.Text;

namespace EcommerceCore
{
    public static class ServiceExtensions
    {
        // Configuration for JWT in Startup . We also need IConfiguration
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration Configuration)
        {
            //getting 'JWT" section from appsettings.json
            var jwtSettings = Configuration.GetSection("Jwt");
            //getting key that i set with Command Line
            var key = Environment.GetEnvironmentVariable("KEY");
            /*var issuer = Environment.GetEnvironmentVariable("Issuer");*/

            //basically adding authentication to app. and default scheme that i want  is JWT
            //when somebody tires to authenticate check for bearer token
            //then i set up parameters. ValidatieIssuer means we want to validate token. validate lifetime
            //and issuer key. Then we set ValidIssuer for any JWT token will be string from appsettings.json
            //then goes key that we hash. most important thing dont put key in appsettings.json
            //based on your situation you may need more validation
            //VALIDATE AUDIENCE TOO. to validate users
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                };
            });
        }

        public static void ConfigurExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {

                error.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        Log.Error($"Something went Wrong in the {contextFeature.Error}");

                        await context.Response.WriteAsync(new Error
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error. Please try agin later"

                        }.ToString());
                    }
                });

            });
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            //there are times when we can have multiple MapperInitilizers, have so many dtos and entities. with different configurations adn etc.
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

    }
}
