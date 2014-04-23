using System.ComponentModel.DataAnnotations.Schema;

namespace SmashTO.Models
{
    [Table("SwissMatches")]
    public class MatchModel
    {
        public PlayerModel Player1 { get; set; }
        public PlayerModel Player2 { get; set; }
        public int WinnerId { get; set; }

        public MatchModel()
        {
            Player1 = null;
            Player2 = null;
            WinnerId = 0;
        }

        public MatchModel(PlayerModel bye)
        {
            Player1 = bye;
            Player2 = new PlayerModel{PlayerName = "bye", PlayerId = -1, Rating = 0};
            WinnerId = bye.PlayerId;
        }

        public MatchModel(PlayerModel p1, PlayerModel p2)
        {
            Player1 = p1;
            Player2 = p2;
            WinnerId = 0;
        }
    }
}
