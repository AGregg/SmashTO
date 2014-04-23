using System.Collections.Generic;
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
            
            //using (var db = new PlayersContext())
            //{
            //    var players = db.Players;
            //    foreach (PlayerModel player in players)
            //    {
            //        model.Players.Add(player);
            //    }
            //}

            return View(model);
        }

        [HttpPost]
        public ActionResult PlayerSelect(PlayerSelectModel returnedPlayersModel)
        {
            if (returnedPlayersModel.Format == TournamentFormat.Swiss)
            {
                var swissBracketModel = new SwissBracket();

                var round = new SwissRound(swissBracketModel.TournamentId, 1);

                var players = new List<PlayerModel>();

                foreach (var playerId in returnedPlayersModel.SelectedPlayerIds)
                {
                    foreach (var player in returnedPlayersModel.Players)
                    {
                        if (playerId == player.PlayerId)
                        {
                            players.Add(player);
                        }
                    }
                }

                //var players = returnedPlayersModel.SelectedPlayerIds.OrderByDescending(x => x.Rating).ToList();

                while (players.Count() > 1)
                {
                    var matchModel = new SwissMatchModel(players.First(), players.Last());
                    round.Matches.Add(matchModel);
                    players.RemoveAt(players.Count() - 1);
                    players.RemoveAt(0);
                }

                if (players.Any())
                {
                    round.Matches.Add(new SwissMatchModel(players.First()));
                }

                foreach (var match in round.Matches)
                {
                    match.Player1Wins = 0;
                    match.Player2Wins = 0;
                }

                swissBracketModel.Rounds.Add(round);

                //using (var db = new SwissBracketContext())
                //{
                //    db.SwissBrackets.Add(swissBracketModel);
                //    db.SaveChanges();
                //}

                return View("SwissBracket", round);
            }

            return View(returnedPlayersModel);
        }



        [HttpPost]
        public ActionResult PostSwissBracket(SwissRound returnedRound)
        {

            return View("SwissBracket", returnedRound);
        }
    }
}
