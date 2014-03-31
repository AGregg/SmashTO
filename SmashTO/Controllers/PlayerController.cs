using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Ajax.Utilities;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using SmashTO.Filters;
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
                using (var db = new PlayersContext())
                {
                    db.Players.Add(model);
                    db.SaveChanges();
                }

                return RedirectToAction("Index", "Home");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public ActionResult ViewPlayers()
        {
            using (var db = new PlayersContext())
            {
                var models = db.Players.ToList();

                return View(models);
            }
        }
    }
}
