using System;
using System.Collections.Generic;

namespace FourthCoffee.Orders.Services
{
    /// <summary>
    /// Represents a customer order with order details and total amount.
    /// </summary>
    public class Order
    {
        public required string OrderId { get; set; }
        public required string CustomerId { get; set; }
        public required string Region { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public decimal OrderTotal { get; set; }

        public class OrderDetail
        {
            public required string ProductId { get; set; }
            public required string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
        }
    }
}