using EcomparePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using EcomparePortal.Class;

namespace EcomparePortal.Controllers
{
    public class CustomerController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult ListProduct()
        {
            //var list = db.Products.Include(y => y.Company).Include(y => y.SubCategory).ToList();
            ViewBag.list = db.ProductMainDetails.Include(y => y.Product).Include(y => y.Product.Company).Include(y => y.Product.SubCategory).ToList();
            return View();
            
        }

        [AllowAnonymous]
        public ActionResult RegisterCustomer(int? temp)
        {
            if(temp!=null)
            {
                ViewBag.msg = "This Email Already Exist....";
            }
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterCustomer(CustomerProfile customer)
        {
            var result = db.Users.Where(x => x.UserName == customer.Email).FirstOrDefault();
            if (result == null)
            {
                UserStore<IdentityUser> userStore = new UserStore<IdentityUser>();
                userStore.Context.Database.Connection.ConnectionString = "data source=mi3-wsq2.my-hosting-panel.com;initial catalog=mangemen_ecompareportal;user id=mangemen_webdocUser;password=Flash@123";//System.Configuration.ConfigurationManager.ConnectionStrings["mangemen_GMCarsEntities"].ConnectionString;

                UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);
                IdentityUser user = new IdentityUser();
                user.UserName = customer.Email;
                user.Email = customer.Email;
                manager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 4,
                    RequireNonLetterOrDigit = false,
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireUppercase = false,
                };

                var password = "8484";
                PasswordHasher h = new PasswordHasher();
                var newpassword = h.HashPassword(password);


                IdentityResult result1 = manager.Create(user, newpassword);
                if (result1.Succeeded)
                {
                    var userResult = db.Users.Where(t => t.Id == user.Id).FirstOrDefault();
                    userResult.Role = "Customer";
                    db.SaveChanges();
                    IdentityUserRole userrole = new IdentityUserRole();
                    userrole.UserId = user.Id;
                    userrole.RoleId = "Customer";

                    manager.AddToRole(userrole.UserId, userrole.RoleId);

                    customer.ApplicationUserId = user.Id;

                    db.CustomerProfiles.Add(customer);
                    db.SaveChanges();

                }
            }
            else
            {
                return RedirectToAction("RegisterCustomer",new { temp=1});
            }

            return View();
        }

    }
}