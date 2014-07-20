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
        public decimal ListPrice { get; set; }

        [Required(ErrorMessage = "Please enter a cost.")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Please enter a status.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Please enter a location.")]
        public string Location { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateReceived { get; set; }
    }
}
