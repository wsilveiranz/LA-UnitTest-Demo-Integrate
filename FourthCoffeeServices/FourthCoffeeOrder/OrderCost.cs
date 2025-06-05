using System;

namespace FourthCoffee.Orders.Services
{

    /// <summary>
    /// Represents the cost information for an order including discounts.
    /// </summary>
    public class OrderCost
    {
        /// <summary>
        /// Gets or sets the total cost before any discount is applied.
        /// </summary>
        public decimal TotalBeforeDiscount { get; set; }
        
        /// <summary>
        /// Gets or sets the discount amount applied to the order.
        /// </summary>
        public decimal Discount { get; set; }
        
        /// <summary>
        /// Gets or sets the total cost after the discount is applied.
        /// </summary>
        public decimal TotalAfterDiscount { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the OrderCost class.
        /// </summary>
        /// <param name="totalBeforeDiscount">The total cost before discount.</param>
        /// <param name="discount">The discount amount.</param>
        /// <param name="totalAfterDiscount">The total cost after discount.</param>
        public OrderCost(decimal totalBeforeDiscount, decimal discount, decimal totalAfterDiscount)
        {
            TotalBeforeDiscount = totalBeforeDiscount;
            Discount = discount;
            TotalAfterDiscount = totalAfterDiscount;
        }
    }
}