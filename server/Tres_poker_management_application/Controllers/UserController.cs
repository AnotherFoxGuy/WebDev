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
    public class UserController : Controller
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
        /// gets the create page
        /// </summary>
        /// <returns>create index page</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// pushes the created user to the database
        /// </summary>
        /// <param name="user">user model</param>
        /// <returns>index page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                new User_SQL().CreateUser(user);
            }
            return View("Index");
        }

        /// <summary>
        /// edit a user
        /// </summary>
        /// <param name="id">url id(user)</param>
        /// <param name="user">user model</param>
        /// <returns>edit page with selected user data</returns>
        public ActionResult Edit(int? id, User user)
        {
            if (id == null) return View("Index");
            new User_SQL().FindUser(id, user);

            return View(user);
        }

        /// <summary>
        /// pushes the updated user data to the database
        /// </summary>
        /// <param name="id">url id(user)</param>
        /// <param name="user">user model</param>
        /// <returns>index page</returns>
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirm(int id, User user)
        {
            if (ModelState.IsValid)
                new User_SQL().EditUser(id, user);
            
            return View("Index");
        }

        /// <summary>
        /// delete selected user
        /// </summary>
        /// <param name="id">url id(user)</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (id != null)
                new User_SQL().DeleteUser(id);

            return View("Index");
        }
    }
}