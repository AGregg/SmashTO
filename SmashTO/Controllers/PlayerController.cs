using System.Linq;
using System.Web.Mvc;
using SmashTO.Models;

namespace SmashTO.Controllers
{
    public class PlayerController : Controller
    {
        [HttpGet]
        public ActionResult AddPlayer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPlayer(PlayerModel model)
        {
            if (ModelState.IsValid)
            {
                model.Rating = 1000;
                using (var db = new TournamentContext())
                {
                    db.Players.Add(model);
                    db.SaveChanges();
                }

                return RedirectToAction("ViewPlayers", "Player");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public ActionResult ViewPlayers()
        {
            using (var db = new TournamentContext())
            {
                var models = db.Players.ToList().OrderByDescending(x => x.Rating);

                return View(models);
            }
        }

        public ActionResult Remove(int playerId)
        {
            using (var db = new TournamentContext())
            {
                var playerToRemove = db.Players.Find(playerId);
                db.Players.Remove(playerToRemove);
                db.SaveChanges();
            }

            return RedirectToAction("ViewPlayers", "Player");
        }
    }
}
