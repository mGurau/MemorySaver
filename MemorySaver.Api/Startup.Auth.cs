using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MemorySaver.Authentication;
using MemorySaver.Domain.ServiceContracts.DTOs.Request;
using MemorySaver.Domain.ServiceContracts.DTOs.Response;
using MemorySaver.Domain.ServiceContracts.Interfaces;

namespace MemorySaver.Api
{
    public partial class Startup
    {
        private string secretKey;

        private IApplicationBuilder appBuilder;

        private void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(
                        JwtBearerDefaults.AuthenticationScheme,
                        CookieAuthenticationDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });

            secretKey = Configuration["Tokens:Key"];
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = Configuration["Tokens:Issuer"],

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = Configuration["Tokens:Audience"],

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero,
            };

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = tokenValidationParameters;
                    options.Authority = Configuration["WhiteList"];
                    options.Audience = Configuration["WhiteList"];
                    options.RequireHttpsMetadata = false;
                })
                .AddCookie(options =>
                {
                    options.Cookie.Name = "access_token";
                    options.ClaimsIssuer = Configuration["Tokens:Issuer"];
                    options.TicketDataFormat =
                        new CustomJwtDataFormat(SecurityAlgorithms.HmacSha256, tokenValidationParameters);
                    options.Events = new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = context =>
                        {
                            context.Response.StatusCode =
                                (int)HttpStatusCode.Unauthorized;
                            return Task.CompletedTask;
                        },
                    };
                });
        }

        private void AddAuth(IApplicationBuilder app)
        {
            appBuilder = app;

            secretKey = Configuration["Tokens:Key"];
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            app.UseSimpleTokenProvider(new TokenProviderOptions
            {
                Path = "/api/login",
                Audience = Configuration["Tokens:Audience"],
                Issuer = Configuration["Tokens:Issuer"],
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                IdentityResolver = GetIdentity,
            });

            app.UseSimpleTokenProvider(new TokenProviderOptions
            {
                Path = "/api/social-login",
                Audience = Configuration["Tokens:Audience"],
                Issuer = Configuration["Tokens:Issuer"],
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                IdentityResolver = GetSocialIdentity,
            });
        }

        private Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            using (var serviceScope = appBuilder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var loginCredentials = new LoginUserRequestDTO(email, password);
                var userService = serviceScope.ServiceProvider.GetRequiredService<IUserService>();
                var response = userService.Login(loginCredentials);
                return ClaimsTask(response);
            }
        }

        private Task<ClaimsIdentity> GetSocialIdentity(string email, string name)
        {
            using (var serviceScope = appBuilder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var loginCredentials = new LoginSocialUserRequestDTO(email, name);
                var userService = serviceScope.ServiceProvider.GetRequiredService<IUserService>();
                var response = userService.VerifySocialUser(loginCredentials);
                return ClaimsTask(response);
            }
        }

        private Task<ClaimsIdentity> ClaimsTask(LoginUserResponseDTO response)
        {
            if (response == null)
            {
                return Task.FromResult<ClaimsIdentity>(null);
            }

            var claims = new List<Claim>
            {
                new Claim("userid", response.Id.ToString()),
                new Claim(Path.GetFileName(ClaimTypes.GivenName), $"{response.FirstName} {response.LastName}"),
                new Claim(Path.GetFileName(ClaimTypes.Email), response.Email)
            };
            var claimsIdentity = new ClaimsIdentity(claims);

            return Task.FromResult(claimsIdentity);
        }
    }
}
