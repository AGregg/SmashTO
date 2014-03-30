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
    public class PlayerController : Controller
    {
        [HttpGet]
        public ActionResult AddPlayer()
        {
            var model = new PlayerModel { PlayerName = "" };

            return View(model);
        }
    }
}
