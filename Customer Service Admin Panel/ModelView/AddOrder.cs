using Customer_Service_Admin_Panel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Customer_Service_Admin_Panel.ModelView
{
    public class AddOrder : IViewModel
    {

        [Key]
        public int OrderId { get; set; }

        [Required]
        [Display(Name ="Order Type")]
        public int? OrderTypeId { get; set; }

        public string OrderType1 { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Create Data")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Choose the date ")]
       
        public DateTime? CreateDate { get; set; }

        [Display(Name ="Order Type")]
        public int OrderLineId { get; set; }

        public int? EmployeeId { get; set; }

        [Required]
        public List<Employee> ListOfEmployees { get; set; }

        [Required]
        public decimal? Amount { get; set; }

        [StringLength(50)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Display(Name ="Last Name")]
        
        public string LastName { get; set; }

        public int? CustomerId { get; set; }

        public bool IsChecked { get; set; }

        [Display(Name ="Order Status")]
        [Required]
        public int? OrderStatusId { set; get; } 

        public string OrderStatus { get; set; } 

    }
}