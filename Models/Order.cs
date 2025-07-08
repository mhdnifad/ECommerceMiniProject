using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerceMini.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public required string CustomerEmail { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public decimal TotalAmount { get; set; }

       
        public string Status { get; set; } = "Pending";

        
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
