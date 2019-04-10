using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tres_poker_management_application.Models;

namespace Tres_poker_management_application.Controllers
{
    /// <summary>
    /// contains all actions regarding rules page
    /// </summary>
    public class RulesController : Controller
    {
        /// <summary>
        /// initial page load
        /// </summary>
        /// <param name="id">url id(profile)</param>
        /// <param name="gameprofile">profile model</param>
        /// <returns>index page</returns>
        public ActionResult Index(int? id, Gameprofile gameprofile)
        {
            if (id != null)
            {
                new Rules_SQL().FindProfile(id, gameprofile);
                return View(gameprofile);
            }

            return View();
        }

        /// <summary>
        /// retrive rules from the database
        /// </summary>
        /// <param name="id">url id(profile)</param>
        /// <param name="gameprofile">profile model</param>
        /// <returns>rules</returns>
        public string GetRules(int? id, Gameprofile gameprofile)
        {
            if (id != null)
                new Rules_SQL().FindProfile(id, gameprofile);

            return gameprofile.Rules;
        }

        /// <summary>
        /// gets the edit page
        /// </summary>
        /// <param name="id">url id(profile)</param>
        /// <param name="gameprofile">profile model</param>
        /// <returns>edit page with profile data</returns>
        public ActionResult Edit(int? id, Gameprofile gameprofile)
        {
            if (id != null)
                new Rules_SQL().FindProfile(id, gameprofile);

            else
                return View("Index");

            return View("Edit", gameprofile);
        }


        /// <summary>
        /// confirm edit
        /// </summary>
        /// <param name="id">url id(profile)</param>
        /// <param name="gameprofile">profile model</param>
        /// <returns>index page of edit</returns>
        [HttpPost, ActionName("Edit")]
        public ActionResult EditRule(int? id, Gameprofile gameprofile)
        {
            if (id != null)
            {
                var SQLModel = new Rules_SQL();
                SQLModel.EditRules(id, gameprofile);
            }

            return View("Index");
        }
    }
}