namespace Customer_Service_Admin_Panel.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Adress")]
    public partial class Adress
    {
        public int AdressId { get; set; }

        public int? CustomerId { get; set; }

        [Required]
        [StringLength(70)]
        public string StreetAddess1 { get; set; }

       
        [StringLength(70)]
        public string StreetAddress2 { get; set; }
        [Required]
        public int ZipCode { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string Region { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
