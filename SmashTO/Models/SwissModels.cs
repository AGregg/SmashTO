using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace SmashTO.Models
{
    public class SwissBracketContext : DbContext
    {
        public SwissBracketContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<SwissBracket> SwissBrackets { get; set; }
        public DbSet<SwissRound> SwissRounds { get; set; }
    }

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

        public SwissBracket()
        {
            Rounds = new List<SwissRound>();
        }
    }

    [Table("SwissRounds")]
    public class SwissRound
    {
        public int TournamentId { get; set; }
        public int RoundNumber { get; set; }
        public IList<SwissMatchModel> Matches { get; set; }

        public SwissRound(int tid, int round)
        {
            TournamentId = tid;
            RoundNumber = round;
            Matches = new List<SwissMatchModel>();
        }

        public SwissRound()
        {
            TournamentId = 0;
            RoundNumber = 0;
            Matches = new List<SwissMatchModel>();
        }
    }

    //public class SwissModel
    //{
    //    public int TournamentId { get; set; }
    //    public int RoundNumber { get; set; }
        
    //}

    [Table("SwissMatches")]
    public class SwissMatchModel : MatchModel
    {
        public int Player1Wins { get; set; }
        public int Player2Wins { get; set; }

        public SwissMatchModel()
        {
            Player1 = null;
            Player2 = null;
            WinnerId = 0;
        }

        public SwissMatchModel(PlayerModel bye)
        {
            Player1 = bye;
            Player2 = new PlayerModel{PlayerName = "bye", PlayerId = -1, Rating = 0};
            WinnerId = bye.PlayerId;
        }

        public SwissMatchModel(PlayerModel p1, PlayerModel p2)
        {
            Player1 = p1;
            Player2 = p2;
            WinnerId = 0;
            Player1Wins = 0;
            Player2Wins = 0;
        }
    }
}
