using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Customer_Service_Admin_Panel.ModelView
{
    public class AddContract : IViewModel
    {
        // Contract fields

        [Key]
        
        public int ContractId { get; set; }

        public int? CustomerId { get; set; }

        [Display(Name ="Product Name")]
        [Required(ErrorMessage ="Select a product")]
        public int? ProductId { get; set; }

        public string ProductName { get; set; }



        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Signed Data")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage ="Choise a date")]
        
        public DateTime? SignedDate { get; set; }

        
    }
}