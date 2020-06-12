using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using PracticeMVC001.Models;

namespace PracticeMVC001.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Autherize(PracticeMVC001.Models.User userModel) 
        {
            using (LoginDBModel db = new LoginDBModel()) 
            {
                var userDetails = db.Users.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong username or password.";
                    return View("index", userModel);
                }
                else {
                    Session["userID"] = userDetails.UserID;
                    Session["UserName"] = userDetails.UserName;
                    return RedirectToAction("Index", "Home");
                }
            }
           
        }
        public ActionResult Logout() 
        {
            int userId = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}