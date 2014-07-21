using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CompanyABC.Domain.Entities
{
    public class Product
    {
        public Guid ABCID { get; set; }

        [Required(ErrorMessage = "Please enter a product name.")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a vendor name.")]
        public string Vendor { get; set; }

        [Required(ErrorMessage = "Please enter a list price.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal ListPrice { get; set; }

        [Required(ErrorMessage = "Please enter a cost.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive cost.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Please enter a status.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Please enter a location.")]
        public string Location { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime? DateReceived { get; set; }
    }
}
