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
            return View();
        }

        [HttpPost]
        public ActionResult AddPlayer(PlayerModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                //try
                //{

                    return RedirectToAction("Index", "Home");
                //}
                //catch (Exception e)
                //{
                //    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                //}
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}
