using EcomparePortal.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.IO;

namespace EcomparePortal.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Agent
        public ActionResult Dashboard()
        {
            var userid = User.Identity.GetUserId();
            var user = db.Users.Where(y => y.Id == userid).FirstOrDefault();
            if(user.CompanyName=="Askari")
            {
                var totalsale = db.PaymentInfoes.Where(y => y.Status == "Active").Include(y=>y.Product).Include(y=>y.Customer).ToList();

                var totalorder = totalsale.Where(y => y.Product.CompanyId == 6).ToList();
                ViewBag.TotalOrder = totalorder.Count();
                int totalearnamount = 0;
                decimal webdocamount = 0;
                decimal companyamount = 0;
                foreach (var item in totalorder)
                {
                    totalearnamount += item.AmountPaid;
                    webdocamount += Convert.ToDecimal(item.WebdocAmount);
                    companyamount += Convert.ToDecimal(item.CompanyAmount);
                }
                ViewBag.TotalEarnAmount = totalearnamount;
                ViewBag.WebdocAmount = webdocamount;
                ViewBag.CompanyAmount = companyamount;
                ViewBag.CompanyName = "Askari";

            }
           else if (user.CompanyName == "Easypaisa")
            {
                var totalsale = db.PaymentInfoes.Where(y => y.Status == "Active").Include(y => y.Product).Include(y => y.Customer).ToList();

                var totalorder = totalsale.Where(y => y.Product.CompanyId == 7).ToList();
                ViewBag.TotalOrder = totalorder.Count();
                int totalearnamount = 0;
                foreach (var item in totalorder)
                {
                    totalearnamount += item.AmountPaid;
                }
                ViewBag.TotalEarnAmount = totalearnamount;
                ViewBag.CompanyName = "Easypaisa";
            }

            return View();
        }
        public ActionResult AddProduct()
        {
            var list = db.Companies.ToList();
            ViewBag.CompanyId = new SelectList(list, "Id", "CompanyName");
            var list2 = db.SubCategories.ToList();
            ViewBag.SubCategoryId = new SelectList(list2, "Id", "SubCategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product product, HttpPostedFileBase MyImages, HttpPostedFileBase file, HttpPostedFileBase ClaimDocument, HttpPostedFileBase PolicyDocument, FormCollection fc)
        {

            var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];

            string logo = Path.Combine(Server.MapPath("~/ProductImages/"), file.FileName);
            file.SaveAs(logo);
            string path = "https://ecompareportal.webddocsystems.com/ProductImages/" + file.FileName;
            product.Logo = path;

            var list = db.Companies.ToList();
            ViewBag.CompanyId = new SelectList(list, "Id", "CompanyName");
            var list2 = db.SubCategories.ToList();
            ViewBag.SubCategoryId = new SelectList(list2, "Id", "SubCategoryName");
          
            product.IsFeatured = "Featured";
            product.Status = "Active";
            product.Priority = "1";
            var userid = User.Identity.GetUserId();
            //var userdata = db.Users.Where(y => y.Id == userid).FirstOrDefault();
            //if(userdata.CompanyName=="Askari")
            //{
            //    product.CompanyId = 6;
            //}
            //else
            //{
            //    product.CompanyId = 7;
            //}
            //product.SubCategoryId = id1;
            db.Products.Add(product);
            db.SaveChanges();
            ProductMainDetails details = new ProductMainDetails();
            int productid = Convert.ToInt32(product.Id);
            var data = (fc.Count - 6) / 2;
            var lists = new List<Dictionary<string, string>>();

            for (int i = 0; i < data; i++)
            {
                var Tlist = new Dictionary<string, string>();
                Tlist.Add("HeadingText", fc["test[" + i + "][HeadingText]"].ToString());
                Tlist.Add("HeadingValue", fc["test[" + i + "][HeadingValue]"].ToString());

                lists.Add(Tlist);
            }
            var datalist = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(lists);
            details.ProductId = productid;
            details.Fields = datalist;
            db.ProductMainDetails.Add(details);
           
        
            
            ClaimDocuments claim = new ClaimDocuments();
            string claimdocument = Path.Combine(Server.MapPath("~/ClaimDocument/"), ClaimDocument.FileName);
            file.SaveAs(claimdocument);
            string claimdocumentpath = "https://ecompareportal.webddocsystems.com/ClaimDocument/" + ClaimDocument.FileName;
            claim.ClaimDocumentURL = claimdocumentpath;
            claim.ProductId = productid;
            db.ClaimDocuments.Add(claim);
            

            PolicyDocuments policy = new PolicyDocuments();
            string policydocument = Path.Combine(Server.MapPath("~/PolicyDocument/"), PolicyDocument.FileName);
            file.SaveAs(policydocument);
            string policydocumentpath = "https://ecompareportal.webddocsystems.com/PolicyDocument/" + PolicyDocument.FileName;
            policy.PolicyDocumentURL = policydocumentpath;
            policy.ProductId = productid;
            db.PolicyDocuments.Add(policy);
            var data2 = productid;
            db.SaveChanges();

            return Json(data2,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddBenefits(ProductBenefits benefit, FormCollection fc)
        {

            var id = fc["Id"];
            var id1 = Int32.Parse(id);

            var data = (fc.Count - 1) / 2;
            var lists = new List<Dictionary<string, string>>();

            for (int i = 0; i < data; i++)
            {
                var Tlist = new Dictionary<string, string>();
                Tlist.Add("HeadingText", fc["test[" + i + "][HeadingText]"].ToString());
                Tlist.Add("HeadingValue", fc["test[" + i + "][HeadingValue]"].ToString());

                lists.Add(Tlist);
            }
            var datalist = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(lists);

            benefit.Fields = datalist;
            benefit.ProductId = id1;
            db.ProductBenefits.Add(benefit);
            db.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult AddCustomerInputDetails(CustomerDynamicInputDetails customer, FormCollection fc)
        {
            var productid = fc["Id"];
            var id1 = Int32.Parse(productid);
            var data = (fc.Count - 3) / 3;
            //var lists = new List<Dictionary<string, FormFields>>();
            var Tlist = new List<FormFields>();
            for (int i = 0; i < data; i++)
            {


                var ListOfValues = new List<HeadingValue>();
                var values = fc["test[" + i + "][HeadingValue]"].ToString().Split(',');
                if (values.Count() > 1)
                {
                    foreach (var val in values)
                    {
                        ListOfValues.Add(new HeadingValue { Value = val });
                    }

                }

                Tlist.Add(new FormFields
                {
                    HeadingText = fc["test[" + i + "][HeadingText]"].ToString(),
                    HeadingType = fc["test[" + i + "][HeadingText]"].ToString(),
                    HeadingValue = ListOfValues,

                });
                // lists.Add(Tlist);
            }
            var datalist = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(Tlist);

            customer.Fields = datalist;
            customer.ProductId = id1;
            customer.Status = "Active";
             db.CustomerDynamicInputDetails.Add(customer);

             db.SaveChanges();
            return Json(productid, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AddFeature(ProductFeatures features, FormCollection fc)
        {
           // var termconditition = fc["TermAndCondition"];
            var id = fc["Id"];
            var id1 = Int32.Parse(id);
            ProductTermAndCondition term = new ProductTermAndCondition();
           
            //term.ProductId = id1;
            //term.TermAndCondition = termconditition;
            //db.ProductTermAndConditions.Add(term);
            //db.SaveChanges();

            var data = (fc.Count - 2) / 1;
            var lists = new List<Dictionary<string, string>>();

            for (int i = 0; i < data; i++)
            {


                //Tlist.Add("HeadingValue", fc["test[" + i + "][HeadingValue]"].ToString());
                var feature = fc["test[" + i + "][HeadingValue]"].ToString();
                features.ProductId = id1;
                features.Feature = feature;
                db.ProductFeatures.Add(features);
                db.SaveChanges();

            }




            return Json(id, JsonRequestBehavior.AllowGet);
        }
    }

}