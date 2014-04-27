using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
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
                return RedirectToAction("Results", new {tournamentId = returnedRound.TournamentId});
            }
            else
            {
                tournament.SeedNextRound();

                return RedirectToAction("SwissBracket", new { tournamentId = returnedRound.TournamentId });
            }
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
