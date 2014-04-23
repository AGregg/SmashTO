using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashTO.Models
{
    [Table("SwissBrackets")]
    public class SwissBracket : BracketModel
    {
        public IList<SwissRound> Rounds { get; set; }

        public override IList<MatchModel> Matches()
        {
            throw new NotImplementedException();
        }

        public override IList<PlayerModel> Players()
        {
            throw new NotImplementedException();
        }

        public override IList<ResultsModel> Results()
        {
            throw new NotImplementedException();
        }
    }

    [Table("SwissRounds")]
    public class SwissRound
    {
        public int TournamentId { get; set; }
        public int RoundNumber { get; set; }
        public IList<MatchModel> Matches { get; set; } 
    }

    public class SwissModel
    {
        public int TournamentId { get; set; }
        public int RoundNumber { get; set; }
        
    }

    public class MatchModel
    {
        public int Player1ID { get; set; }
        public int Player2ID { get; set; }
        public int WinnerID { get; set; }

        public MatchModel(int id1, int id2)
        {
            Player1ID = id1;
            Player2ID = id2;
            WinnerID = 0;
        }
    }
}
