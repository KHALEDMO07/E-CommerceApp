﻿namespace E_Commerce.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public decimal price { get; set; } 

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}