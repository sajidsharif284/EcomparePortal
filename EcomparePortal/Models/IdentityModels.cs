using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EcomparePortal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; internal set; }
        public string CompanyName { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<EcomparePortal.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<EcomparePortal.Models.SubCategory> SubCategories { get; set; }

        public System.Data.Entity.DbSet<EcomparePortal.Models.Company> Companies { get; set; }

        public System.Data.Entity.DbSet<EcomparePortal.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<EcomparePortal.Models.ProductImages> ProductImages { get; set; }

        public System.Data.Entity.DbSet<EcomparePortal.Models.CustomerProfile> CustomerProfiles { get; set; }

        public System.Data.Entity.DbSet<EcomparePortal.Models.ProductMainDetails> ProductMainDetails { get; set; }

        public System.Data.Entity.DbSet<EcomparePortal.Models.ProductFeatures> ProductFeatures { get; set; }

        public System.Data.Entity.DbSet<EcomparePortal.Models.ProductBenefits> ProductBenefits { get; set; }

        public System.Data.Entity.DbSet<EcomparePortal.Models.PolicyDocuments> PolicyDocuments { get; set; }

        public System.Data.Entity.DbSet<EcomparePortal.Models.ClaimDocuments> ClaimDocuments { get; set; }

        public System.Data.Entity.DbSet<EcomparePortal.Models.ProductTermAndCondition> ProductTermAndConditions { get; set; }

        public System.Data.Entity.DbSet<EcomparePortal.Models.CustomerDynamicInputDetails> CustomerDynamicInputDetails { get; set; }

        public System.Data.Entity.DbSet<EcomparePortal.Models.CityNames> CityNames { get; set; }

      
        public System.Data.Entity.DbSet<EcomparePortal.Models.PaymentInfo> PaymentInfoes { get; set; }

        public System.Data.Entity.DbSet<EcomparePortal.Models.CustomerSaleData> CustomerSaleDatas { get; set; }

        public System.Data.Entity.DbSet<EcomparePortal.Models.CarCompany> CarCompanies { get; set; }

        public System.Data.Entity.DbSet<EcomparePortal.Models.CarCompanyProduct> CarCompanyProducts { get; set; }

        public System.Data.Entity.DbSet<EcomparePortal.Models.ClaimsDynamicInput> ClaimsDynamicInputs { get; set; }

        public System.Data.Entity.DbSet<EcomparePortal.Models.ClaimRaisingDetail> ClaimRaisingDetails { get; set; }
    }
}