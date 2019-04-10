using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tres_poker_management_application.Models;

namespace Tres_poker_management_application.Controllers
{
    /// <summary>
    /// contains all actions regarding chips page
    /// </summary>
    public class ChipsController : Controller
    {
        /// <summary>
        /// initial page load
        /// </summary>
        /// <returns>index page</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// gets the data for the poker chips
        /// </summary>
        /// <param name="id">url id(profile)</param>
        /// <param name="gameprofile">gameprofile data</param>
        /// <returns></returns>
        public ActionResult GetChipsData(int? id, Gameprofile gameprofile)
        {
            if (id != null)
                new Gameprofile_SQL().FindProfile(id, gameprofile);

            return PartialView("PartialChipsData", JsonConvert.DeserializeObject<Chips>(gameprofile.Chips));
        }

        /// <summary>
        /// returns the chips edit page with the data to edit the selected item
        /// </summary>
        /// <param name="id">url id(profile)</param>
        /// <param name="gameprofile">gameprofile data</param>
        /// <returns></returns>
        public ActionResult Edit(int? id, Gameprofile gameprofile)
        {
            if (id == null) return View("Index");
                new Gameprofile_SQL().FindProfile(id, gameprofile);

            return View(JsonConvert.DeserializeObject<Chips>(gameprofile.Chips));
        }

        /// <summary>
        /// confirm the chips edit
        /// </summary>
        /// <param name="id">url id(profile)</param>
        /// <param name="gameprofile">gameprofile data</param>
        /// <returns>index page(chips)</returns>
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditChips(int? id, Chips chips)
        {
            if (id != null)
                new Chips_SQL().EditChips(id, chips);

            return View("Index");
        }
    }
}