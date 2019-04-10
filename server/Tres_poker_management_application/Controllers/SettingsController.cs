using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tres_poker_management_application.Models;

namespace Tres_poker_management_application.Controllers
{
    /// <summary>
    /// contains all actions regarding profile page
    /// </summary>
    public class SettingsController : Controller
    {
        /// <summary>
        /// inital page load
        /// </summary>
        /// <returns>index page</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// gets the selected profiles data
        /// </summary>
        /// <param name="id">url id(profile)</param>
        /// <param name="gameprofile">profile model</param>
        /// <returns>loads the partialview with the selected profile data</returns>
        public ActionResult GetProfileData(int? id, Gameprofile gameprofile)
        {
            if (id != null)
            {
                var SQLModel = new Gameprofile_SQL();
                SQLModel.FindProfile(id, gameprofile);
            }
            else
                return PartialView("PartialProfileData");

            AllModels allModels = new AllModels();
            allModels.Gameprofile = gameprofile;
            return PartialView("PartialProfileData", allModels);
        }

        /// <summary>
        /// loads the create profile page
        /// </summary>
        /// <returns>create profile page</returns>
        public ActionResult CreateProfile()
        {
            return View();
        }

        /// <summary>
        /// creates a profile with a given number of rounds
        /// </summary>
        /// <param name="gameprofile">profile model</param>
        /// <param name="round">round model</param>
        /// <returns>edit profile page + created profile data</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProfile(Gameprofile gameprofile, Round round)
        {
            if (ModelState.IsValid)
            {
                var Gameprofile_SQLModel = new Gameprofile_SQL();
                int AddedProfileID = Gameprofile_SQLModel.CreateProfile(gameprofile);

                //Leave i = 1 !! needed for creating the correct round numbers
                for (int i = 1; i <= round.Number_Of_Rounds; i++)
                {
                    round.Round_NR = i;
                    round.Gameprofile_Profile_ID = AddedProfileID;
                    var Round_SQLModel = new Round_SQL();
                    Round_SQLModel.CreateRound(round);
                }
                return RedirectToAction($"EditProfile/{AddedProfileID}");
            }
            return View("Index");
        }

        /// <summary>
        /// loads edit profile page with the selected profile data
        /// </summary>
        /// <param name="id">url id(profile)</param>
        /// <param name="gameprofile">profile model</param>
        /// <returns>edit page with the selected profile data</returns>
        public ActionResult EditProfile(int? id, Gameprofile gameprofile)
        {
            if (id != null)
            {
                var SQLModel = new Gameprofile_SQL();
                SQLModel.FindProfile(id, gameprofile);
            }
            else
                return View("Index");

            AllModels allModels = new AllModels();
            allModels.Gameprofile = gameprofile;
            return View(allModels);
        }

        /// <summary>
        /// pushes the changed profile data to the database
        /// </summary>
        /// <param name="id">url id(profile)</param>
        /// <param name="gameprofile">profile model</param>
        /// <returns>to the edit page where the changed data will be displayed</returns>
        [HttpPost, ActionName("EditProfile")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmEditProfile(int? id, Gameprofile gameprofile)
        {
            var SQLModel = new Gameprofile_SQL();
            SQLModel.EditProfile(id, gameprofile);

            return RedirectToAction("EditProfile", id);
        }

        /// <summary>
        /// delete selected profile
        /// </summary>
        /// <param name="id">url id(profile)</param>
        /// <returns>index page of profile</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProfile(int? id)
        {
            if (id != null)
            {
                var SQLModel = new Gameprofile_SQL();
                SQLModel.DeleteGameprofile(id);
            }
            return View("Index");
        }
    }
}