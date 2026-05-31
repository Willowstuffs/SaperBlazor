using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saper.Server.Data;
using Saper.Shared.Models;

namespace Saper.Server.Controllers;
//kontroler API dla Blazora z leaderboardu. Dziala na zasadzie RESTful API
[ApiController] //automatycznie waliduje dane wejsciowe
[Route("api/[controller]")] //adres endpointu API
public class LeaderboardController : ControllerBase
{
    private readonly AppDbContext _dbContext; //kontekst bazy danych, zdefiniowany w AppDbContext.cs
    //kontekst sluzy do interakcji z baza danych, czyli wykonywania zapytan, dodawania, usuwania i aktualizowania danych

    public LeaderboardController(AppDbContext dbContext) 
    //konstruktor przyjmuje AppDbContext jako parametr, ktory jest wstrzykiwany przez DI (Dependency Injection)
    {
        _dbContext = dbContext; // przypisanie kontekstu bazy danych do pola prywatnego
    }

    //GET: api/leaderboard - pobieranie danych leaderboardu
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LeaderboardEntry>>> GetLeaderboard() //pobieranie danych leaderboardu rosnaco po czasie
    {
        var results = await _dbContext.LeaderboardEntries
            .OrderBy(e => e.Time) // sortowanie po czasie
            .ToListAsync(); //ToListAsync wykonuje zapytanie i zwraca wyniki jako liste
        return Ok(results); // zwraca wyniki jako odpowiedz HTTP 200 OK
    }

    //POST: api/leaderboard - dodawanie nowego wpisu do leaderboardu

    [HttpPost]
    public async Task<ActionResult<LeaderboardEntry>> PostLeaderboardEntry(LeaderboardEntry entry) // dodawanie nowego wpisu do leaderboardu
    {
        try
        {
            entry.CreatedAt = DateTime.UtcNow; // ustawienie daty i czasu utworzenia wpisu na aktualny czas w formacie UTC

            _dbContext.LeaderboardEntries.Add(entry); // dodanie nowego wpisu do kontekstu bazy danych
            await _dbContext.SaveChangesAsync(); // zapisanie zmian w bazie danych

            return CreatedAtAction(nameof(GetLeaderboard), new { id = entry.Id }, entry); // zwraca odpowiedz HTTP 201 Created z lokalizacja nowego wpisu i samym wpisem
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in PostLeaderboardEntry: {ex.Message}"); // logowanie bledu w konsoli
            return StatusCode(500, "Internal server error");
        }
    }
}