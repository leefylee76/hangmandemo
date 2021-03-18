using System;
using System.ComponentModel.DataAnnotations;

namespace HangMan.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Word { get; set; }
        public int Lives { get; set; }
        public int Score { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatePlayed { get; set; }

    }
}
