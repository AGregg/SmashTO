using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;

namespace SmashTO.Models
{
    [Table("SwissBrackets")]
    public class SwissBracket : BracketModel
    {
        public IEnumerable<SwissRound> Rounds()
        {
            var rounds = new List<SwissRound>();

            using (var db = new TournamentContext())
            {
                rounds = db.SwissRounds.Where(x => x.TournamentId == TournamentId).OrderBy(x => x.RoundNumber).ToList();
            }

            return rounds;
        }

        public void SeedFirstRound(List<int> playerIds)
        {
            using (var db = new TournamentContext())
            {
                db.SwissBrackets.Add(this);
                db.SaveChanges();
            }

            var round = new SwissRound(TournamentId, 1);

            List<PlayerModel> players;

            using (var db = new TournamentContext())
            {
                players = db.Players.Where(x => playerIds.Contains(x.PlayerId)).ToList();
            }

            players = players.OrderByDescending(x => x.Rating).ToList();

            var matches = new List<SwissMatch>();

            while (players.Count() > 1)
            {
                var matchModel = new SwissMatch(players.First(), players.Last()) {Round = round};
                matches.Add(matchModel);
                players.RemoveAt(players.Count() - 1);
                players.RemoveAt(0);
            }

            if (players.Any())
            {
                matches.Add(new SwissMatch(players.First()));
            }

            using (var db = new TournamentContext())
            {
                foreach (var match in matches)
                {
                    db.SwissMatches.Add(match);
                }
                db.SaveChanges();
            }
        }

        public void SeedNextRound()
        {
            var lastRound = Rounds().Last();
            var nextRound = new SwissRound{RoundNumber = lastRound.RoundNumber + 1, TournamentId = lastRound.TournamentId};

            using (var db = new TournamentContext())
            {
                db.SwissRounds.Add(nextRound);
                db.SaveChanges();
            }

            var players = SwissPlayers();

            players = SwissPlayers().OrderByDescending(x => x.Wins).ThenByDescending(x => x.Player.Rating).ToList();

            var subSet = new List<SwissPlayerModel>();
            var matches = new List<SwissMatch>();
            
            while (players.Any())
            {
                subSet.Clear();
                var leadingScore = players.First().Wins;
                while (players.Any() && players.First().Wins == leadingScore)
                {
                    subSet.Add(players.First());
                    players.RemoveAt(0);
                }

                while (subSet.Count() > 1)
                {
                    var matchModel = new SwissMatch(subSet.First(), subSet.Last(), nextRound.RoundId);
                    matches.Add(matchModel);
                    subSet.RemoveAt(subSet.Count() - 1);
                    subSet.RemoveAt(0);
                }

                if (subSet.Any())
                {
                    if (players.Any())
                    {
                        var matchModel = new SwissMatch(subSet.First(), players.First(), nextRound.RoundId);
                        matches.Add(matchModel);
                        players.RemoveAt(0);
                        subSet.RemoveAt(0);
                    }
                    else
                    {
                        matches.Add(new SwissMatch(subSet.First(), nextRound.RoundId));
                        subSet.RemoveAt(0);
                    }
                }
            }

            using (var db = new TournamentContext())
            {
                foreach (var match in matches)
                {
                    db.SwissMatches.Add(match);
                }
                db.SaveChanges();
            }
        }

        public override IList<MatchModel> Matches()
        {
            var matches = new List<MatchModel>();
            var rounds = Rounds();
            foreach (var round in rounds)
            {
                matches.AddRange(round.Matches());
            }
            return matches;
        }

        public override IList<PlayerModel> Players()
        {
            var firstRound = Rounds().OrderBy(x => x.RoundNumber).ToList().First();
            var firstRoundMatches = firstRound.Matches();
            var players = new List<PlayerModel>();
            var playerIds = new List<int>();
            foreach (var match in firstRoundMatches)
            {
                if (match.Player1Id > 0) playerIds.Add(match.Player1Id);
                if (match.Player2Id > 0) playerIds.Add(match.Player2Id);
            }

            using (var db = new TournamentContext())
            {
                players = db.Players.Where(x => playerIds.Contains(x.PlayerId)).ToList();
            }

            return players;
        }

        public IList<SwissPlayerModel> SwissPlayers()
        {
            var matches = Matches();
            var players = Players();
            var swissPlayers = players.Select(player => new SwissPlayerModel(player, matches)).ToList();
            return swissPlayers;
        }

        public bool isOver()
        {
            var players = SwissPlayers().OrderByDescending(x => x.Wins).ThenByDescending(x => x.Player.Rating).ToList();
            return (players[0].Wins > players[1].Wins);
        }

        public override ResultsModel Results()
        {
            var players = SwissPlayers().OrderByDescending(x => x.Wins).ThenByDescending(x => x.Player.Rating).ToList();
            var resultsModel = new ResultsModel();
            for (int i = 0; i < players.Count; i++)
            {
                resultsModel.Results.Add(new Result
                {
                    Placing = i + 1,
                    PlayerName = players[i].Player.PlayerName,
                    Score = players[i].Wins
                });
            }
            resultsModel.TournamentName = Name;
            return resultsModel;
        }
    }

    [Table("SwissRounds")]
    public class SwissRound
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoundId { get; set; }

        [ForeignKey("Bracket")]
        public virtual int TournamentId { get; set; }
        public virtual SwissBracket Bracket { get; set; }

        public int RoundNumber { get; set; }

        public IEnumerable<SwissMatch> Matches()
        {
            var matches = new List<SwissMatch>();
            using (var db = new TournamentContext())
            {
                matches = db.SwissMatches.Where(x => x.RoundId == RoundId).ToList();
            }
            return matches;
        }

        public SwissRoundModel ToModel()
        {
            var model = new SwissRoundModel();
            model.RoundNumber = RoundNumber;
            model.TournamentId = TournamentId;
            model.RoundId = RoundId;
            var matches = Matches();
            
            var bracket = new SwissBracket();

            using (var db = new TournamentContext())
            {
                bracket = db.SwissBrackets.SingleOrDefault(x => x.TournamentId == TournamentId);
            }

            var matchHistory = bracket.Matches();

            using (var db = new TournamentContext())
            {
                foreach (var match in matches)
                {
                    var player1 = db.Players.SingleOrDefault(x => x.PlayerId == match.Player1Id);
                    var player2 = db.Players.SingleOrDefault(x => x.PlayerId == match.Player2Id);
                    model.Matches.Add(new SwissMatchModel(new SwissPlayerModel(player1, matchHistory), new SwissPlayerModel(player2, matchHistory)));
                }
            }

            return model;
        }

        public SwissRound(int tid, int roundNum)
        {
            TournamentId = tid;
            RoundNumber = roundNum;
        }

        public SwissRound()
        {
            TournamentId = 0;
            RoundNumber = 0;
        }
    }

    [Table("SwissMatches")]
    public class SwissMatch : MatchModel
    {
        [ForeignKey("Round")]
        public virtual int RoundId { get; set; }
        public virtual SwissRound Round { get; set; }
        
        public SwissMatch()
        {
            Player1Id = 0;
            Player2Id = 0;
            WinnerId = 0;
        }

        public SwissMatch(PlayerModel bye)
        {
            Player1Id = bye.PlayerId;
            Player2Id = -1;
            WinnerId = bye.PlayerId;
        }

        public SwissMatch(PlayerModel p1, PlayerModel p2)
        {
            Player1Id = p1.PlayerId;
            Player2Id = p2.PlayerId;
            WinnerId = 0;
        }

        public SwissMatch(SwissPlayerModel p1, SwissPlayerModel p2, int id)
        {
            Player1Id = p1.Player.PlayerId;
            Player2Id = p2.Player.PlayerId;
            WinnerId = 0;
            RoundId = id;
        }

        public SwissMatch(SwissPlayerModel bye, int id)
        {
            Player1Id = bye.Player.PlayerId;
            Player2Id = -1;
            WinnerId = bye.Player.PlayerId;
            RoundId = id;
        }
    }

    public class SwissRoundModel
    {
        public int RoundNumber { get; set; }
        public int RoundId { get; set; }
        public int TournamentId { get; set; }
        public IList<SwissMatchModel> Matches { get; set; }

        public SwissRoundModel()
        {
            Matches = new List<SwissMatchModel>();
        }
    }

    public class SwissMatchModel
    {
        public SwissPlayerModel Player1 { get; set; }
        public SwissPlayerModel Player2 { get; set; }
        public int WinnerId { get; set; }

        public SwissMatchModel(SwissPlayerModel bye)
        {
            Player1 = bye;
            Player2 = new SwissPlayerModel(new PlayerModel{PlayerId = -1, PlayerName = "bye", Rating = 0});
        }

        public SwissMatchModel(SwissPlayerModel player1, SwissPlayerModel player2)
        {
            WinnerId = 0;
            Player1 = player1;
            Player2 = player2;
        }

        public SwissMatchModel()
        {
            Player1 = new SwissPlayerModel();
            Player2 = new SwissPlayerModel();
            WinnerId = -1;
        }
    }

    public class SwissPlayerModel
    {
        
        public PlayerModel Player { get; set; }
        public int Wins { get; set; }

        public SwissPlayerModel(PlayerModel player, IEnumerable<MatchModel> matches)
        {
            Player = player;
            Wins = matches.Count(match => match.WinnerId == Player.PlayerId);
        }

        public SwissPlayerModel(PlayerModel player)
        {
            Player = player;
            Wins = 0;
        }

        public SwissPlayerModel()
        {
            Player = null;
            Wins = 0;
        }
    }
}
