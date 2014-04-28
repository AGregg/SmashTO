using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using SmashTO.Models;

namespace SmashTO.Controllers
{
    public class BracketController : Controller
    {
        public ActionResult BracketHome()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PlayerSelect(TournamentFormat format)
        {
            var model = new PlayerSelectModel { Format = format };

            using (var db = new TournamentContext())
            {
                model.Players = db.Players.ToList();
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult PlayerSelect(PlayerSelectModel returnedPlayersModel)
        {
            if (returnedPlayersModel.Format == TournamentFormat.Swiss)
            {
                var swissBracketModel = new SwissBracket{Name = returnedPlayersModel.TournamentName};
                swissBracketModel.SeedFirstRound(returnedPlayersModel.SelectedPlayerIds.ToList());

                return RedirectToAction("SwissBracket", new { tournamentId = swissBracketModel.TournamentId });
            }

            return View(returnedPlayersModel);
        }

        [HttpGet]
        public ActionResult SwissBracket(int tournamentId)
        {
            var bracket = new SwissBracket();
            using (var db = new TournamentContext())
            {
                bracket = db.SwissBrackets.SingleOrDefault(x => x.TournamentId == tournamentId);
            }

            Debug.Assert(bracket != null, "bracket != null");
            var round = bracket.Rounds().Last();
            
            return View(round.ToModel());
        }

        [HttpPost]
        public ActionResult SwissBracket(SwissRoundModel returnedRound)
        {
            SwissBracket tournament;

            using (var db = new TournamentContext())
            {
                foreach (var swissMatch in returnedRound.Matches)
                {
                    var matchToUpdate = db.SwissMatches
                        .Where(x => x.Player1Id == swissMatch.Player1.Player.PlayerId)
                        .Where(x => x.Player2Id == swissMatch.Player2.Player.PlayerId)
                        .SingleOrDefault(x => x.RoundId == returnedRound.RoundId);

                    matchToUpdate.WinnerId = swissMatch.WinnerId;
                }

                db.SaveChanges();

                tournament = db.SwissBrackets.SingleOrDefault(x => x.TournamentId == returnedRound.TournamentId);
            }

            if (tournament.isOver())
            {
                //update ratings
                var matches = tournament.Matches();
                var players = tournament.Players();
                var playersWithNewRatings = players.Select(player => new PlayerModel {PlayerId = player.PlayerId, Rating = player.Rating}).ToList();

                foreach (var match in matches)
                {
                    var p1Rating = players.Single(x => x.PlayerId == match.Player1Id).Rating;
                    var p2Rating = players.Single(x => x.PlayerId == match.Player2Id).Rating;
                    var p1Expected = (1 / (1 + (10 ^ (p2Rating - p1Rating)/400)));
                    var p1Result = (match.WinnerId == match.Player1Id ? 1.00 : 0.00);
                    var p1Adjust = (int)Math.Round(32*(p1Result - p1Expected));
                    playersWithNewRatings.Single(x => x.PlayerId == match.Player1Id).Rating += p1Adjust;
                    playersWithNewRatings.Single(x => x.PlayerId == match.Player2Id).Rating -= p1Adjust;
                }

                using (var db = new TournamentContext())
                {
                    foreach (var player in playersWithNewRatings)
                    {
                        var playerToEdit = db.Players.SingleOrDefault(x => x.PlayerId == player.PlayerId);
                        playerToEdit.Rating = player.Rating;
                    }
                    db.SaveChanges();
                }

                return RedirectToAction("Results", new {tournamentId = returnedRound.TournamentId});
            }
            
            tournament.SeedNextRound();

            return RedirectToAction("SwissBracket", new { tournamentId = returnedRound.TournamentId });
        }

        [HttpGet]
        public ActionResult Results(int tournamentId)
        {
            SwissBracket tournament;

            using (var db = new TournamentContext())
            {
                tournament = db.SwissBrackets.SingleOrDefault(x => x.TournamentId == tournamentId);
            }

            var results = tournament.Results();

            return View(results);
        }
    }
}
