using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashTO.Models
{
    [Table("Players")]
    public class PlayerSelectModel
    {
        public IEnumerable<PlayerModel> Players { get; set; }
        public IList<int> SelectedPlayerIds { get; set; }
        public TournamentFormat Format { get; set; }

        public PlayerSelectModel()
        {
            Players = new[]
            {
                new PlayerModel{PlayerId = 1, PlayerName = "scrub 1", Rating = 1600},
                new PlayerModel{PlayerId = 2, PlayerName = "scrub 2", Rating = 1550},
                new PlayerModel{PlayerId = 3, PlayerName = "scrub 3", Rating = 1500},
                new PlayerModel{PlayerId = 4, PlayerName = "scrub 4", Rating = 1450},
                new PlayerModel{PlayerId = 5, PlayerName = "scrub 5", Rating = 1400},
                new PlayerModel{PlayerId = 6, PlayerName = "scrub 6", Rating = 1350},
                new PlayerModel{PlayerId = 7, PlayerName = "scrub 7", Rating = 1300},
                new PlayerModel{PlayerId = 8, PlayerName = "scrub 8", Rating = 1250},
                new PlayerModel{PlayerId = 9, PlayerName = "scrub 9", Rating = 1200},
                new PlayerModel{PlayerId = 10, PlayerName = "scrub 10", Rating = 1150},
                new PlayerModel{PlayerId = 11, PlayerName = "scrub 11", Rating = 1100},
                new PlayerModel{PlayerId = 12, PlayerName = "scrub 12", Rating = 1050},
                new PlayerModel{PlayerId = 13, PlayerName = "scrub 13", Rating = 1000},
                new PlayerModel{PlayerId = 14, PlayerName = "scrub 14", Rating = 950},
                new PlayerModel{PlayerId = 15, PlayerName = "scrub 15", Rating = 900},
                new PlayerModel{PlayerId = 16, PlayerName = "scrub 16", Rating = 850},
                new PlayerModel{PlayerId = 17, PlayerName = "scrub 17", Rating = 800},
                new PlayerModel{PlayerId = 18, PlayerName = "scrub 18", Rating = 750},
                new PlayerModel{PlayerId = 19, PlayerName = "scrub 19", Rating = 700},
                new PlayerModel{PlayerId = 20, PlayerName = "scrub 20", Rating = 650},
                new PlayerModel{PlayerId = 21, PlayerName = "scrub 21", Rating = 600},
                new PlayerModel{PlayerId = 22, PlayerName = "scrub 22", Rating = 550},
                new PlayerModel{PlayerId = 23, PlayerName = "scrub 23", Rating = 500},
                new PlayerModel{PlayerId = 24, PlayerName = "scrub 24", Rating = 450} 
            };
            SelectedPlayerIds = new List<int>();
        }
    }
}
