using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EcomparePortal.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Icon { get; set; }
        public int Priority { get; set; }
    }
    public class SubCategory
    {
        [Key]
        public int Id { get; set; }
        public Category Category { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public string SubCategoryName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Icon { get; set; }
        public int Priority { get; set; }
    }
    
    public class Company
    {
        [Key]
        public int Id { get; set; }
        
        public string CompanyName { get; set; }

        public string CompanyEmail { get; set; }
        public string CardImageURL { get; set; }
        public string TagImageURL { get; set; }
        public string ColorCode { get; set; }
       

        public string Logo { get; set; }
        public string Status { get; set; }

        public string Priority { get; set; }
        public string IsFeatured { get; set; }
    }
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string ProductPrice { get; set; }
        public double SalePrice { get; set; }

        public string Logo { get; set; }

        public SubCategory SubCategory { get; set; }
        [ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }

        public Company Company { get; set; }
        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        public string Status { get; set; }

        public string Priority { get; set; }
        public string IsFeatured { get; set; }
        public string Description { get; set; }

    }
    public class ProductImages
    {
        [Key]
        public int Id { get; set; }

        public string ImageURL { get; set; }
        

        public Product Product { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }

    }
    public class ProductFeatures
    {
        [Key]
        public int Id { get; set; }

        public string Feature { get; set; }


        public Product Product { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }

    }
    public class ProductBenefits
    {
        [Key]
        public int Id { get; set; }

        public string Fields { get; set; }


        public Product Product { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }

    }
    public class CustomerDynamicInputDetails
    {
        [Key]
        public int Id { get; set; }

        public Product Product { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Fields { get; set; }



    }
    public class ProductMainDetails
    {
        [Key]
        public int Id { get; set; }

        public string Fields { get; set; }


        public Product Product { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }

    }
    public class PolicyDocuments
    {
        [Key]
        public int Id { get; set; }

        public string PolicyDocumentURL { get; set; }


        public Product Product { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }

    }
    public class ClaimDocuments
    {
        [Key]
        public int Id { get; set; }

        public string ClaimDocumentURL { get; set; }

        public Product Product { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }

    }
    public class ProductTermAndCondition
    {
        [Key]
        public int Id { get; set; }

        public string TermAndCondition { get; set; }


        public Product Product { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }

    }
    public class CityNames
    {
        [Key]
        public int Id { get; set; }

        public string CityName { get; set; }
    }
    public class PaymentInfo
    {
        [Key]
        public int Id { get; set; }
        public int AmountPaid { get; set; }
        public string CompanyAmount { get; set; }
        public string WebdocAmount { get; set; }
        public string BankCharges { get; set; }
        public string InstallmentPlan { get; set; }
        public string Bank { get; set; }
        public string Accountnumber { get; set; }
        public string Mobilenumber { get; set; }
        public string TransactionType { get; set; }
        public string TransactionReferenceNumber { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Status { get; set; }

        public Product Product { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public CustomerProfile Customer { get; set; }
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }

    }
    public class CustomerSaleData
    {
        [Key]
        public int Id { get; set; }
        
        public string Fields { get; set; }

        public Product Product { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public CustomerProfile Customer { get; set; }
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }


    }
    public class CarCompany
    {
        [Key]
        public string Id { get; set; }

        public string CompanyName { get; set; }
       
    }
    public class CarCompanyProduct
    {
        [Key]
        public int CarModelId { get; set; }

        public CarCompany CarCompany { get; set; }
        [ForeignKey("CarCompany")]
        public string CarCompanyId { get; set; }

        public string ModelName { get; set; }

    }

    public class ClaimsDynamicInput
    {
        [Key]
        public int Id { get; set; }

        public SubCategory SubCategory { get; set; }
        [ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }

      
        public string Fields { get; set; }



    }

    public class ClaimRaisingDetail    {        [Key]        public int id { get; set; }



        public string Status { get; set; }        public string Fields { get; set; }        public CustomerProfile CustomerProfile { get; set; }        [ForeignKey("CustomerProfile")]        public string CustomerProfileId { get; set; }              public Product Product { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }               public DateTime ClaimDateTime { get; set; }        public DateTime IncompleteDateTime { get; set; }        public DateTime CompleteDateTime { get; set; }        public DateTime ProcessingDateTime { get; set; }        public DateTime ApprovedDateTime { get; set; }           }


}