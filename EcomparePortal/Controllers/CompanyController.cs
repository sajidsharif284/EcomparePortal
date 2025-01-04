using EcomparePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace EcomparePortal.Controllers
{
    public class CompanyController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Company
        public ActionResult CompanyProduct()
        {
            var id = User.Identity.GetUserId();
            var companydata = db.Users.Where(y => y.Id == id).FirstOrDefault();
            var list = db.Products.Where(y=>y.Company.CompanyName== companydata.CompanyName).Include(y => y.Company).Include(y => y.SubCategory).ToList();
            
                return View(list);
           
        }
    }
}