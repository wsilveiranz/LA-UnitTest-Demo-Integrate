using System;
using System.Threading.Tasks;

namespace FourthCoffee.Orders.Services
{
    /// <summary>
    /// Provides discount calculation services for customers based on their region and customer type.
    /// </summary>
    public interface IDiscountService
    {
        /// <summary>
        /// Gets the discount percentage for a customer based on their location and customer type.
        /// </summary>
        /// <param name="region">The customer's region (e.g., "NA", "EU", "APAC")</param>
        /// <param name="customerId">The customer ID in format "TYPE-ID" (e.g., "X-12345" for VIP)</param>
        /// <returns>A task containing the discount as a decimal (e.g., 0.2 for 20%)</returns>
        Task<decimal> CalculateDiscountAsync(string region, string customerId);
    }
}