using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using SmashTO.Filters;
using SmashTO.Models;

namespace SmashTO.Controllers
{
    public class TournamentController : Controller
    {
        public ActionResult BracketHome()
        {
            return View();
        }

        public ActionResult PlayerSelect(TournamentFormat format)
        {
            var model = new PlayerSelectModel { Format = format };

            return View(model);
        }
    }
}
