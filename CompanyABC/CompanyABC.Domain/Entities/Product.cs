using System;
using System.ComponentModel.DataAnnotations;

namespace CompanyABC.Domain.Entities
{
    public class Product
    {
        public Guid ABCID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Vendor { get; set; }
        public decimal ListPrice { get; set; }
        public decimal Cost { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateReceived { get; set; }
    }
}
