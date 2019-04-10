using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tres_poker_management_application.Models;

namespace Tres_poker_management_application.Controllers
{
    /// <summary>
    /// contains all actions regarding rounds on the profilepage
    /// </summary>
    public class RoundsController : Controller
    {
        /// <summary>
        /// rounds doesent have a index >> redirect to profile page
        /// </summary>
        /// <returns>profile page</returns>
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Settings");
        }

        /// <summary>
        /// adds rounds to a given profile
        /// </summary>
        /// <param name="round">rounds model</param>
        /// <returns>refrech the editprofile page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRounds(Round round)
        {
            new Round_SQL().CreateRound(round);

            return RedirectToAction("EditProfile", "Settings");
        }

        /// <summary>
        /// edit a given round
        /// </summary>
        /// <param name="id">url id(round)</param>
        /// <param name="round">round model</param>
        /// <returns>refrech the editprofile page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRound(int? id, Round round)
        {
            if (id != null)
                new Round_SQL().EditRound(id, round);

            return RedirectToAction("EditProfile", "Settings");
        }

        /// <summary>
        /// updates the round number(number shwon on screen, not the round ID)
        /// </summary>
        /// <param name="id">url id(round)</param>
        /// <param name="round">round model</param>
        /// <returns>refrech the editprofile page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRoundNumber(int? id, Round round)
        {
            if (id != null)
                new Round_SQL().UpdateRoundNumber(id, round);

            return RedirectToAction("EditProfile", "Settings");
        }

        /// <summary>
        /// delete a given round
        /// </summary>
        /// <param name="id">url id(round)</param>
        /// <returns>refrech the editprofile page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRound(int? id)
        {
            if (id != null)
                new Round_SQL().DeleteRound(id);

            return RedirectToAction("EditProfile", "Settings");
        }
    }
}