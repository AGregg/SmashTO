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

            using (var db = new PlayersContext())
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
                var swissBracketModel = new SwissBracket();

                var round = new SwissRound(swissBracketModel.TournamentId, 1);

                var players = new List<PlayerModel>();

                using (var db = new PlayersContext())
                {
                    players = db.Players.Where(x => returnedPlayersModel.SelectedPlayerIds.Contains(x.PlayerId)).ToList();
                }

                players = players.OrderByDescending(x => x.Rating).ToList();

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

                return View("SwissBracket", round);
            }

            return View(returnedPlayersModel);
        }



        [HttpPost]
        public ActionResult SwissBracket(SwissRound returnedRound)
        {
            var swissPlayers = new List<SwissPlayerModel>();

            foreach (var match in returnedRound.Matches)
            {
                if (match.Player1.PlayerId == match.WinnerId)
                {
                    match.Player1Wins++;
                }
                else
                {
                    match.Player2Wins++;
                }

                var player1 = new SwissPlayerModel {Player = match.Player1, Wins = match.Player1Wins };
                var player2 = new SwissPlayerModel { Player = match.Player2, Wins = match.Player2Wins };
                
                swissPlayers.Add(player1);
                swissPlayers.Add(player2);
            }

            swissPlayers = swissPlayers.OrderByDescending(x => x.Wins).ThenByDescending(x => x.Player.Rating).ToList();

            var nextRound = new SwissRound(returnedRound.TournamentId, returnedRound.RoundNumber + 1);

            var subSet = new List<SwissPlayerModel>();
            var leadingScore = 0;

            while (swissPlayers.Any())
            {
                subSet.Clear();
                leadingScore = swissPlayers.First().Wins;
                while (swissPlayers.Any() && swissPlayers.First().Wins == leadingScore)
                {
                    subSet.Add(swissPlayers.First());
                    swissPlayers.RemoveAt(0);
                }

                while (subSet.Count() > 1)
                {
                    var matchModel = new SwissMatchModel(subSet.First(), subSet.Last());
                    matchModel.Player1Wins = subSet.First().Wins;
                    matchModel.Player2Wins = subSet.Last().Wins;
                    nextRound.Matches.Add(matchModel);
                    subSet.RemoveAt(subSet.Count() - 1);
                    subSet.RemoveAt(0);
                }

                if (subSet.Any())
                {
                    if (swissPlayers.Any())
                    {
                        var matchModel = new SwissMatchModel(subSet.First(), swissPlayers.First());
                        nextRound.Matches.Add(matchModel);
                        swissPlayers.RemoveAt(0);
                        subSet.RemoveAt(0);
                    }
                    else
                    {
                        nextRound.Matches.Add(new SwissMatchModel(subSet.First().Player));
                        subSet.RemoveAt(0);
                    }
                }
            }

            //swissBracketModel.Rounds.Add(nextRound);

            //return View("SwissBracket", nextRound);

            return View("SwissBracket", nextRound);
        }
    }
}
