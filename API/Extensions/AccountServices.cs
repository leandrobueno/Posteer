using System.Text;
using API.Data;
using API.Models.Account;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
  public static class AccountServices
  {
    public static IServiceCollection AddAccountServices(this IServiceCollection services, IConfiguration config)
    {
      services.AddIdentity<ApplicationUser, IdentityRole>
          (x => x.SignIn.RequireConfirmedAccount = true)
          .AddEntityFrameworkStores<DataContext>()
          .AddSignInManager<SignInManager<ApplicationUser>>()
          .AddDefaultTokenProviders();
      services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(x =>
      {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["SecretKey"]!)),
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateLifetime = true,
          ClockSkew = TimeSpan.Zero
        };
        x.Events = new JwtBearerEvents
        {
          OnMessageReceived = context =>
          {
            var accessToken = context.Request.Query["access_token"].ToString();
            Console.WriteLine(accessToken);
            return Task.CompletedTask;
          }
        };
      });

      return services;
    }
  }
}