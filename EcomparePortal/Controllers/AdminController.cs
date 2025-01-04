using EcomparePortal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Runtime.Serialization;

namespace EcomparePortal.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListCategory()
        {

            var categorydata = db.Categories.FirstOrDefault();
            var categorydata1 = db.Categories.ToList().OrderBy(y=>y.Priority);

            if (categorydata == null)
            {
                return RedirectToAction("AddCategory");
            }
            else
            {
                return View(categorydata1);
            }
        }
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category category, HttpPostedFileBase file)
        {
            string logo = Path.Combine(Server.MapPath("~/CategoryImages/"), file.FileName);
            file.SaveAs(logo);
            string path = "https://ecompareportal.webddocsystems.com/CategoryImages/" + file.FileName;
            category.Icon = path;

            db.Categories.Add(category);
            db.SaveChanges();

            return RedirectToAction("ListCategory");
        }
        public ActionResult EditCategory(string id)
        {
            var id1 = Int32.Parse(id);
            var list = db.Categories.Where(x => x.Id == id1).FirstOrDefault();
            ViewBag.categoryid = id;

            return View(list);
        }
        [HttpPost]
        public ActionResult EditCategory(FormCollection fc, Category category)
        {
            var id = fc["Id"];
            var id1 = Int32.Parse(id);
            var list = db.Categories.Where(x => x.Id == id1).FirstOrDefault();
            list.CategoryName = category.CategoryName;
            list.Description = category.Description;
            list.Status = category.Status;
            list.Priority = category.Priority;
            db.SaveChanges();
            return RedirectToAction("ListCategory");
        }
        public ActionResult DeleteCategory(string id)
        {
            var id1 = Int32.Parse(id);
            var categorydata = db.Categories.Where(x => x.Id == id1).FirstOrDefault();
            var subcategorydata = db.SubCategories.Where(x => x.CategoryId == id1).ToList();


            foreach (var item in subcategorydata)
            {
                db.SubCategories.Remove(item);
            }
            db.Categories.Remove(categorydata);
            db.SaveChanges();
            return RedirectToAction("ListCategory");
        }
        public ActionResult ListSubCategory(string id)
        {
            var id1 = Int32.Parse(id);
            var subcategorydata = db.SubCategories.Where(x => x.CategoryId == id1).Include(y=>y.Category).FirstOrDefault();
            if (subcategorydata != null)
            {

                ViewBag.subcategoryname = subcategorydata.Category.CategoryName;
            }
            var list = db.SubCategories.Where(x => x.CategoryId == id1).Include(y=>y.Category).ToList().OrderBy(y=>y.Priority);


            if (subcategorydata == null)
            {
                return RedirectToAction("AddSubCategory", new { id = id });
            }
            else
            {
                return View(list);
            }
        }
        public ActionResult AddSubCategory(string id)
        {
            ViewBag.categoryid = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddSubCategory(SubCategory subcategory, HttpPostedFileBase file,FormCollection fc)
        {

            var id = fc["Id"];
            var id1 = Int32.Parse(id);
            string logo = Path.Combine(Server.MapPath("~/SubCategoryImages/"), file.FileName);
            file.SaveAs(logo);
            string path = "https://ecompareportal.webddocsystems.com/SubCategoryImages/" + file.FileName;
            subcategory.Icon = path;
            subcategory.CategoryId = id1;

            db.SubCategories.Add(subcategory);
            db.SaveChanges();

            return RedirectToAction("ListCategory");
        }

        public ActionResult AddClaimForm(string id)
        {
            ViewBag.id = id;
            
            return View();
        }
        [HttpPost]
        public ActionResult AddClaimForm(ClaimsDynamicInput claims, FormCollection fc)
        {
            var productidid = fc["Id"];
            var id1 = Int32.Parse(productidid);
            var data = (fc.Count - 1) / 3;
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
                    HeadingType = fc["test[" + i + "][HeadingType]"].ToString(),
                    HeadingValue = ListOfValues,

                });
                // lists.Add(Tlist);
            }
            var datalist = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(Tlist);

            claims.Fields = datalist;
            claims.SubCategoryId = id1;
           
            db.ClaimsDynamicInputs.Add(claims);

            db.SaveChanges();
            return RedirectToAction("ListProduct");
        }


        public ActionResult EditSubCategory(string id)
        {
            var id1 = Int32.Parse(id);
            var list = db.SubCategories.Where(x => x.Id == id1).FirstOrDefault();
            ViewBag.subcategoryid = id;

            return View(list);
        }
        [HttpPost]
        public ActionResult EditSubCategory(FormCollection fc, SubCategory subcategory)
        {
            var id = fc["Id"];
            var id1 = Int32.Parse(id);
            var list = db.SubCategories.Where(x => x.Id == id1).FirstOrDefault();
            list.Priority = subcategory.Priority;
            list.Status = subcategory.Status;
            list.SubCategoryName = subcategory.SubCategoryName;
            list.Description = subcategory.Description;

            db.SaveChanges();
            return RedirectToAction("ListCategory");
        }
        public ActionResult DeleteSubCategory(string id)
        {
            //string path = Path.Combine(Server.MapPath("~/SubCategoryImages/"), file.FileName);
            //System.IO.File.Delete(path);
            var id1 = Int32.Parse(id);
            var data = db.SubCategories.Where(x => x.Id == id1).FirstOrDefault();
            db.SubCategories.Remove(data);
            db.SaveChanges();
            return RedirectToAction("ListCategory");
        }

        public ActionResult ListCompany()
        {

           
            var company = db.Companies.ToList();
            if (company.Count == 0)
            {
                return RedirectToAction("RegisterCompany", "Account");
            }
            return View(company);
        }
      
        public ActionResult EditCompany(string id)
        {
            var id1 = Int32.Parse(id);
            var list = db.Companies.Where(x => x.Id == id1).FirstOrDefault();
            ViewBag.companyid = id;

            return View(list);
        }
        [HttpPost]
        public ActionResult EditCompany(FormCollection fc, Company company)
        {
            var id = fc["Id"];
            var id1 = Int32.Parse(id);
            var list = db.Companies.Where(x => x.Id == id1).FirstOrDefault();
            list.CompanyName = company.CompanyName;
            list.CompanyEmail = company.CompanyEmail;
            list.Status = company.Status;
            list.Priority = company.Priority;
            list.IsFeatured = company.IsFeatured;

            db.SaveChanges();
            return RedirectToAction("ListCompany");
        }

        public ActionResult DeleteCompany(string id)
        {
            var id1 = Int32.Parse(id);
            var companydata = db.Companies.Where(x => x.Id == id1).FirstOrDefault();
            
            db.Companies.Remove(companydata);
            db.SaveChanges();
            return RedirectToAction("ListCompany");
        }

        public ActionResult ListProduct()
        {
            var list = db.Products.Include(y=>y.Company).Include(y=>y.SubCategory).ToList();


            if (list.Count == 0)
            {
                return RedirectToAction("AddProduct");
            }
            else
            {
                return View(list);
            }
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
        public ActionResult AddProduct(Product product, HttpPostedFileBase file, FormCollection fc)
        {
           

            string logo = Path.Combine(Server.MapPath("~/ProductImages/"), file.FileName);
            file.SaveAs(logo);
            string path = "https://ecompareportal.webddocsystems.com/ProductImages/" + file.FileName;
            product.Logo = path;

            var list = db.Companies.ToList();
            ViewBag.CompanyId = new SelectList(list, "Id", "CompanyName");
            var list2 = db.SubCategories.ToList();
            ViewBag.SubCategoryId = new SelectList(list2, "Id", "SubCategoryName");
            var saleprice = Convert.ToDouble(product.ProductPrice) * 1.5 / 100;
            product.SalePrice = Convert.ToDouble(product.ProductPrice) + saleprice;
            //product.SubCategoryId = id1;
            db.Products.Add(product);

            ProductMainDetails details = new ProductMainDetails();
            int productid = Convert.ToInt32(product.Id);
            var data = (fc.Count - 9) / 2;
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
            db.SaveChanges();

            return RedirectToAction("ListCategory");
        }

        public ActionResult EditProduct(string id)
        {
            var id1 = Int32.Parse(id);
            var list = db.Products.Where(x => x.Id == id1).FirstOrDefault();
            ViewBag.productid = id;

            return View(list);
        }
        [HttpPost]
        public ActionResult EditProduct(FormCollection fc, Product product)
        {
            var id = fc["Id"];
            var id1 = Int32.Parse(id);
            var list = db.Products.Where(x => x.Id == id1).FirstOrDefault();
            list.ProductName = product.ProductName;
            list.ProductPrice = product.ProductPrice;
            list.Status = product.Status;
            list.Priority = product.Priority;
            list.IsFeatured = product.IsFeatured;
            list.Description = product.Description;

            db.SaveChanges();
            return RedirectToAction("ListCategory");
        }
        public ActionResult DeleteProduct(string id)
        {
           
            var id1 = Int32.Parse(id);
            var data = db.Products.Where(x => x.Id == id1).FirstOrDefault();
            db.Products.Remove(data);
            db.SaveChanges();
            return RedirectToAction("ListCategory");
        }
        public ActionResult ListProductImages(string id)
        {
            var id1 = Int32.Parse(id);
            var productimagedata = db.ProductImages.Where(y=>y.ProductId==id1).FirstOrDefault();
            var list = db.ProductImages.Where(y=>y.ProductId==id1).Include(y=>y.Product).ToList();


            if (productimagedata == null)
            {
                return RedirectToAction("UploadProductImages",new { id=id});
            }
            else
            {
                ViewBag.firstimage = productimagedata.ImageURL;
                return View(list);
            }
        }
        public ActionResult UploadProductImages(string id)
        {
            ViewBag.productid = id;
            return View();
        }
        [HttpPost]
        public ActionResult UploadProductImages(ProductImages productimage, FormCollection fc,  IEnumerable<HttpPostedFileBase> Images)
        {
            var id = fc["Id"];
            var id1 = Int32.Parse(id);
            foreach (var image in Images)
            {

                string pathofimage = Path.Combine(Server.MapPath("~/ProductImages/" ), image.FileName);
                image.SaveAs(pathofimage);
                productimage.ImageURL = "https://ecompareportal.webddocsystems.com/ProductImages/" +  image.FileName;
                productimage.ProductId = id1;
                
                db.ProductImages.Add(productimage);
                db.SaveChanges();

            }


            return RedirectToAction("ListProduct");


        }
        public ActionResult SubCategoryProduct(string id)
        {
            var id1 = Int32.Parse(id);
            var productdata = db.Products.Where(y=>y.SubCategoryId==id1).FirstOrDefault();
            var list = db.Products.Where(y=>y.SubCategoryId==id1).Include(y => y.Company).Include(y => y.SubCategory).ToList();


            if (productdata == null)
            {
                return RedirectToAction("AddProduct");
            }
            else
            {
                return View(list);
            }
        }
        public ActionResult ProductFeature(string id)
        {
            var id1 = Int32.Parse(id);
            var productfeature = db.ProductFeatures.Where(y => y.ProductId == id1).Include(y=>y.Product).ToList();
            if (productfeature == null)
            {
                return RedirectToAction("AddFeature", new { id = id });
            }

            return View(productfeature);
        }
        public ActionResult AddFeature(string id)
        {
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddFeature(ProductFeatures features,FormCollection fc)
        {

            var id = fc["Id"];
            var id1 = Int32.Parse(id);

            var data = (fc.Count - 2) / 1;
            var lists = new List<Dictionary<string, string>>();

            for (int i = 0; i < data; i++)
            {


                //Tlist.Add("HeadingValue", fc["test[" + i + "][HeadingValue]"].ToString());
               var feature= fc["test[" + i + "][HeadingValue]"].ToString();
                features.ProductId = id1;
                features.Feature = feature;
                db.ProductFeatures.Add(features);
                db.SaveChanges();

            }


           

            return RedirectToAction("ListProduct");
        }
        public ActionResult EditProductFeature(string id)
        {
            var id1 = Int32.Parse(id);
            var Productfeature = db.ProductFeatures.Where(y => y.Id == id1).Include(y => y.Product).FirstOrDefault();
            ViewBag.id = id;
            return View(Productfeature);
        }
        [HttpPost]
        public ActionResult EditProductFeature(ProductFeatures feature, FormCollection fc)
        {
            var id = fc["Id"].ToString();
            var features = fc["Feature"];
            var id1 =Int32.Parse(id) ;
            
            var list = db.ProductFeatures.Where(y => y.Id == id1).Include(y => y.Product).FirstOrDefault();
            list.Feature = features;
            db.SaveChanges();
            return RedirectToAction("ListProduct");
        }
        public ActionResult DeleteProductFeature(string id)
        {
            var id1 = Int32.Parse(id);
            var list = db.ProductFeatures.Where(y => y.Id == id1).Include(y => y.Product).FirstOrDefault();
            db.ProductFeatures.Remove(list);
            db.SaveChanges();
            return RedirectToAction("ListProduct");
        }
        public ActionResult AddBenefits(string id)
        {
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddBenefits(ProductBenefits benefit, FormCollection fc)
        {

            var id = fc["Id"];
            var id1 = Int32.Parse(id);

            var data = (fc.Count - 2 ) / 2;
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

            return RedirectToAction("ListProduct");
        }
        public ActionResult AddPolicyDocument(string id)
        {
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddPolicyDocument(PolicyDocuments policy, HttpPostedFileBase file, FormCollection fc)
        {

            var id = fc["Id"];
            var id1 = Int32.Parse(id);
            string logo = Path.Combine(Server.MapPath("~/PolicyDocument/"), file.FileName);
            file.SaveAs(logo);
            string path = "https://ecompareportal.webddocsystems.com/PolicyDocument/" + file.FileName;
            policy.PolicyDocumentURL = path;
            policy.ProductId = id1;
            db.PolicyDocuments.Add(policy);
            db.SaveChanges();
            return RedirectToAction("ListProduct");
        }
        public ActionResult AddClaimDocument(string id)
        {
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddClaimDocument(ClaimDocuments claim, HttpPostedFileBase file, FormCollection fc)
        {

            var id = fc["Id"];
            var id1 = Int32.Parse(id);
            string logo = Path.Combine(Server.MapPath("~/ClaimDocument/"), file.FileName);
            file.SaveAs(logo);
            string path = "https://ecompareportal.webddocsystems.com/ClaimDocument/" + file.FileName;
            claim.ClaimDocumentURL = path;
            claim.ProductId = id1;
            db.ClaimDocuments.Add(claim);
            db.SaveChanges();
            return RedirectToAction("ListProduct");
        }
        public ActionResult ProductTermAndCondition(string id)
        {
            var id1 = Int32.Parse(id);
            var termandcondition = db.ProductTermAndConditions.Where(y => y.ProductId == id1).Include(y => y.Product).FirstOrDefault();
            if (termandcondition == null)
            {
                return RedirectToAction("AddTermAndCondition", new { id = id });
            }

            return RedirectToAction("EditTermAndCondition", new { id = id });
        }
        public ActionResult AddTermAndCondition(string id)
        {
            ViewBag.id = id;
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AddTermAndCondition(ProductTermAndCondition term, FormCollection fc)
        {

            var id = fc["Id"];
            var termconditition = fc["TermAndCondition"].ToString();
            var id1 = Int32.Parse(id);
            term.ProductId = id1;
            term.TermAndCondition = termconditition;
            db.ProductTermAndConditions.Add(term);
            db.SaveChanges();
            return RedirectToAction("ListProduct");
        }
        public ActionResult EditTermAndCondition(string id)
        {
            var id1 = Int32.Parse(id);
            var termandcondition = db.ProductTermAndConditions.Where(y => y.ProductId == id1).Include(y => y.Product).FirstOrDefault();
            ViewBag.TermAndCondition = termandcondition.TermAndCondition;
            ViewBag.id = id;
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditTermAndCondition(ProductTermAndCondition term, FormCollection fc)
        {

            var id = fc["Id"];
            var termconditition = fc["termcondition"].ToString();
            var id1 = Int32.Parse(id);
            var list = db.ProductTermAndConditions.Where(y => y.ProductId == id1).Include(y => y.Product).FirstOrDefault();
            list.TermAndCondition = termconditition;
            db.SaveChanges();
            return RedirectToAction("ListProduct");
        }
        public ActionResult CustomerFormData(string id)
        {
            var id1 = Int32.Parse(id);
            var customerform = db.CustomerDynamicInputDetails.Where(y => y.ProductId == id1).FirstOrDefault();
            //ViewBag.list = db.CustomerDynamicInputDetails.Where(y => y.ProductId == id1).ToList();
            if (customerform == null)
            {
                return RedirectToAction("AddCustomerInputDetails",new {id=id });
            }
            var customersaledata = db.CustomerSaleDatas.Where(y => y.ProductId == id1).FirstOrDefault();
            ViewBag.datalist = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(customersaledata.Fields);
            return View();
        }
        public ActionResult AddCustomerInputDetails(string id)
        {
            ViewBag.id = id;
            ViewBag.city = db.CityNames.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult AddCustomerInputDetails(CustomerDynamicInputDetails customer, FormCollection fc)
        {
            var productidid = fc["Id"];
            var id1 = Int32.Parse(productidid);
            var data = (fc.Count - 3) / 3;
            //var lists = new List<Dictionary<string, FormFields>>();
            var Tlist = new List<FormFields>();
            for (int i = 0; i < data; i++)
            {
               
              
                var ListOfValues = new List<HeadingValue>();
                var values = fc["test[" + i + "][HeadingValue]"].ToString().Split(',');
                if(values.Count() > 1)
                {
                    foreach (var val in values)
                    {
                        ListOfValues.Add(new HeadingValue { Value = val });
                    }

                }
               
                Tlist.Add(new FormFields
                {
                    HeadingText= fc["test[" + i + "][HeadingText]"].ToString(),
                    HeadingType = fc["test[" + i + "][HeadingType]"].ToString(),
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
            return RedirectToAction("ListProduct");
        }

        
    }

    public class FormFields
    {
        [DataMember]
        public string HeadingText { get; set; }
        [DataMember]
        public string HeadingType { get; set; }
        [DataMember]
        public List<HeadingValue> HeadingValue { get; set; }

        public FormFields()
        {
            HeadingValue = new List<HeadingValue>();
        }

    }
    public class HeadingValue
    {
        [DataMember]
        public string Value { get; set; }
    }
}