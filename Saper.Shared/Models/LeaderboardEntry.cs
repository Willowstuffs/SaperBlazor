using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Saper.Shared.Models
{
    //Wzorzec dla wpisu w tabeli wynikow
    public class LeaderboardEntry
    {
        public int Id { get; set; } // klucz glowny w bazie danych
        [Required]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Player name must be between 1 and 10 characters.")]
        public string PlayerName { get; set; } = string.Empty; // nazwa gracza
        public int Time { get; set; } // czas ukonczenia gry w sekundach
        public string Difficulty { get; set; } = "Easy"; // poziom trudnosci gry: easy, intermediate, hard
        public DateTime CreatedAt { get; set; } //= DateTime.UtcNow; // data i czas ukonczenia gry
    }
}
