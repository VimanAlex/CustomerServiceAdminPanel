using Customer_Service_Admin_Panel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer_Service_Admin_Panel.ModelView
{
    public class AddCustomerAndAdressContext : IViewModel
    {

       
        public int CustomerId { get; set; }

        [Required]
        [Display(Name = "Organization Number *")]
        public int OrganizationNumber { get; set; }

        [Required(ErrorMessage = "Organization Name field must contain minimum 1 letter ")]
        [StringLength(70)]
        [Display(Name = "Organization Name *")]
        public string OrganizationName { get; set; }


        
        [Required(ErrorMessage = "Street Addess must completed")]
        [Column(Order = 1)]
        [StringLength(70)]
        [Display(Name = "Street Address1 *")]
        public string StreetAddess1 { get; set; }

        [StringLength(70)]
        [Display(Name = "Street Address 2 (Optional)")]
        public string StreetAddress2 { get; set; }

        [Required(ErrorMessage = "ZipCode must have minim 5 cifres")]
        [Display(Name = "Zip Code *")]
        public int ZipCode { get; set; }

        
        [Required(ErrorMessage ="Cty must completed")]
        [Column(Order = 3)]
        [StringLength(50)]
        public string City { get; set; }

        
        [Required(ErrorMessage ="Region must completed")]
        [Column(Order = 4)]
        [StringLength(50)]
        public string Region { get; set; }

        [Required(ErrorMessage = "First Name field must contain minimum 1 letter ")]
        [StringLength(50)]
        [Display(Name = "First Name *")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Last Name field must contain minim 1 letter")]
        [StringLength(50)]
        [Display(Name = "Last Name *")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Incorect Number Mobile! Must have minim 10 numbers")]
        [Display(Name = "Mobile Phone *")]
        public int? Mobile { get; set; }

        [Required(ErrorMessage = "Incorect Format Mail")]
        [StringLength(50)]
        [Display(Name = "Email *")]
        public string Email { get; set; }

        [Required]
        public bool? IsMain { get; set; }

        public int? EmployeeId { get; set; }


 
        public int ContractId { get; set; }

        public int AdressId { get; set; }



        [Display(Name = "Product Name")]
        public int? ProductId { get; set; }


 
        [Display(Name = "Signed Data")]
        [DataType(DataType.Date)]
        public DateTime? SignedDate { get; set; }

        public virtual Customer Customer { get; set; }

    }
}