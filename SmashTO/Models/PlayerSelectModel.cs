using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Security;

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
                new PlayerModel{PlayerId = 1, PlayerName = "scrub 1"},
                new PlayerModel{PlayerId = 2, PlayerName = "scrub 2"},
                new PlayerModel{PlayerId = 3, PlayerName = "scrub 3"},
                new PlayerModel{PlayerId = 4, PlayerName = "scrub 4"},
                new PlayerModel{PlayerId = 5, PlayerName = "scrub 5"},
                new PlayerModel{PlayerId = 6, PlayerName = "scrub 6"},
                new PlayerModel{PlayerId = 7, PlayerName = "scrub 7"},
                new PlayerModel{PlayerId = 8, PlayerName = "scrub 8"},
                new PlayerModel{PlayerId = 9, PlayerName = "scrub 9"},
                new PlayerModel{PlayerId = 10, PlayerName = "scrub 10"},
                new PlayerModel{PlayerId = 11, PlayerName = "scrub 11"},
                new PlayerModel{PlayerId = 12, PlayerName = "scrub 12"},
                new PlayerModel{PlayerId = 13, PlayerName = "scrub 13"},
                new PlayerModel{PlayerId = 14, PlayerName = "scrub 14"},
                new PlayerModel{PlayerId = 15, PlayerName = "scrub 15"},
                new PlayerModel{PlayerId = 16, PlayerName = "scrub 16"},
                new PlayerModel{PlayerId = 17, PlayerName = "scrub 17"},
                new PlayerModel{PlayerId = 18, PlayerName = "scrub 18"},
                new PlayerModel{PlayerId = 19, PlayerName = "scrub 19"},
                new PlayerModel{PlayerId = 20, PlayerName = "scrub 20"},
                new PlayerModel{PlayerId = 21, PlayerName = "scrub 21"},
                new PlayerModel{PlayerId = 22, PlayerName = "scrub 22"},
                new PlayerModel{PlayerId = 23, PlayerName = "scrub 23"},
                new PlayerModel{PlayerId = 24, PlayerName = "scrub 24"} 
            };
            SelectedPlayerIds = new List<int>();
        }
    }
}
