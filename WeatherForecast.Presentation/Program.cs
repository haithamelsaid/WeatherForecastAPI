using Microsoft.EntityFrameworkCore;
using WeatherForecast.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WeatherForecast.Domain.Interfaces;
using WeatherForecast.Infrastructure.Repositories.Base;
using WeatherForecast.Domain.Accounts;
using Microsoft.AspNetCore.Identity;
using WeatherForecast.Application.S_AccountActions;
using WeatherForecast.Presentation.MapperProfiles;
using WeatherForecast.Application.MapperProfiles;
using WeatherForecast.Presentation.Services.S_JwtServices;
using WeatherForecast.Presentation.Settings;
using WeatherForecast.Application.S_WeatherActions;

namespace WeatherForecast.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                  });
            });
            builder.Services.AddDbContext<WeatherDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("WeatherForecast")));
            
            builder.Services.AddIdentity<Account, IdentityRole>()
                .AddEntityFrameworkStores<WeatherDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMemoryCache();


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JwtToken:Issuer"],
                    ValidAudience = builder.Configuration["JwtToken:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtToken:SigningKey"]))
                };
            });

            builder.Services.Configure<JwtTokenSettings>(builder.Configuration.GetSection("JwtToken"));
            builder.Services.AddAutoMapper(typeof(PresentationAccountProfile), typeof(AccountProfile));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IJWTService, JWTService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IWeatherService, WeatherService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
