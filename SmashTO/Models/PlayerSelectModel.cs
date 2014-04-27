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
        public string TournamentName { get; set; }

        public PlayerSelectModel()
        {
            TournamentName = "";
            SelectedPlayerIds = new List<int>();
        }
    }
}
