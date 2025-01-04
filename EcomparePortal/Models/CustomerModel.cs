using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EcomparePortal.Models
{
    public class CustomerProfile
    {
        public ApplicationUser ApplicationUser { get; set; }
        [Key, ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }


        public string CNIC { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }
        
        public string Address { get; set; }
        
        public string Country { get; set; }
        
        public string City { get; set; }
        
        public string MobileNumber { get; set; }

        
        public string Status { get; set; }

        public string RegisterFrom { get; set; }




    }
}