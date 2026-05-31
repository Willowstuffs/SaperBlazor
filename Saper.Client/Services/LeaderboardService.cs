using Saper.Shared;
using Saper.Shared.Models;
using System.Net.Http.Json;

namespace Saper.Client.Services
{
    //LeaderboardService to usluga do komunikacji z API leaderboardu.
    public class LeaderboardService
    {
        private readonly HttpClient _http; //klient do komunikacji z API

        public LeaderboardService(HttpClient http) //konstruktor zajmujacy sie inicjalizacja klienta HTTP
        {
            _http = http;
        }

        public async Task<List<LeaderboardEntry>> GetEntriesAsync()
        {
            var result = await _http.GetFromJsonAsync<List<LeaderboardEntry>>("api/leaderboard");  // pobiera liste wpisow z API jako JSON
            return result ?? new List<LeaderboardEntry>(); // zwraca liste wpisow lub pusta liste, jesli nie ma wpisow
        }

        public async Task AddEntryAsync(LeaderboardEntry entry) // metoda do dodawania wpisu do leaderboardu
        {
            await _http.PostAsJsonAsync("api/leaderboard", entry); // wysyla wpis do API jako JSON
        }
    }
}