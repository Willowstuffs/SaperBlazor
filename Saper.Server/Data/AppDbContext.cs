using Microsoft.EntityFrameworkCore;
using Saper.Shared.Models;

namespace Saper.Server.Data
{
    public class AppDbContext : DbContext
    {
        //ta klasa dziedziczy po DbContext i jest odpowiedzialna za interakcje z baza danych.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        // Konstruktor ktory przyjmuje opcje konfiguracji bazy danych i przekazuje je do klasy bazowej DbContext.
        public DbSet<LeaderboardEntry> LeaderboardEntries => Set<LeaderboardEntry>(); //Wlasciwosc ta zwraca zestaw wpisów do tabeli LeaderboardEntries w bazie danych.
    }
}
