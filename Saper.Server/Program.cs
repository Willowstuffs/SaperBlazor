using Microsoft.EntityFrameworkCore;
using Saper.Server.Data;
using System;
namespace Saper.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //dodanie EF Core SQLite jako bazy danych
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Data Source=leaderboard.db"));

            builder.Services.AddAuthorization(); //dodanie autoryzacji, by mozna bylo korzystac z atrybutu [Authorize] w kontrolerach
            builder.Services.AddControllers(); //zapomnialem je zarejestrowac lol

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer(); // rejestracja eksploratora punktow koncowych, by Swagger mogl dzialac
            builder.Services.AddSwaggerGen(); // dodanie Swaggera do projektu, by mozna bylo testowac API

            //zezwolenia CORS by Blazor mogl komunikowac sie z api (z template)
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("https://localhost:7251") //adres https klienta Blazor, any nie dzialal :(
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            }); 

            var app = builder.Build(); // budowanie aplikacji

            if (app.Environment.IsDevelopment()) // sprawdzenie czy aplikacja jest w trybie deweloperskim
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection(); // przekierowanie z http na https
            app.UseCors(); //wazne by CORS byl aktywny przed autoryzacja (unikniecie bledow CORS)
            app.UseAuthorization();

            app.MapControllers(); //rejestruje LeaderboardController

            app.Run();
        }
    }
}
