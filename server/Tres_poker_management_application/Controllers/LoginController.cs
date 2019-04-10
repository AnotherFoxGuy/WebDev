using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tres_poker_management_application.Controllers
{
    /// <summary>
    /// contains all actions regarding login page
    /// </summary>
    public class LoginController : Controller
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
        /// login authentication
        /// </summary>
        /// <param name="formcolletion">submittng form</param>
        /// <returns>index gamecontroller</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Auth(FormCollection formcolletion)
        {
            if (formcolletion["password"] == "Test123" && formcolletion["username"] == "Admin")
                Session["loggedIn"] = 1;

            return RedirectToAction("Index", "Game");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index", "Login");
        }
    }
}