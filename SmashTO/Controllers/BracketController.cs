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
            //model.Players.Add(new PlayerModel { PlayerName = "scrub 1", PlayerId = 1 });
            //model.Players.Add(new PlayerModel { PlayerName = "scrub 2", PlayerId = 2 });
            //model.Players.Add(new PlayerModel { PlayerName = "scrub 3", PlayerId = 3 });
            //model.Players.Add(new PlayerModel { PlayerName = "scrub 4", PlayerId = 4 });
            //model.Players.Add(new PlayerModel { PlayerName = "scrub 5", PlayerId = 5 });
            //model.Players.Add(new PlayerModel { PlayerName = "scrub 6", PlayerId = 6 });
            //model.Players.Add(new PlayerModel { PlayerName = "scrub 7", PlayerId = 7 });
            
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
            //if (returnedPlayersModel.Format == TournamentFormat.Swiss)
            //{
            //    var swissModel = new SwissModel();
            //    using (var db = new PlayersContext())
            //    {
            //        var players = db.Players;
            //        foreach (PlayerModel player in players)
            //        {
            //            swissModel.Players.Add(player);
            //        }
            //    }
            //}
            
            return View(returnedPlayersModel);
        }
    }
}
