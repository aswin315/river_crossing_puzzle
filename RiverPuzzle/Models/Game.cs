using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiverPuzzle1.Models
{
    public class Game
    {
        public int ID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [NotMapped]
        public TimeSpan TotalTime => EndTime.Subtract(StartTime);

        public GameState GameState { get; set; }
    }

    
}
