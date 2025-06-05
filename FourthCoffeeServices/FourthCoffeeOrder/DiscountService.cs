using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FourthCoffee.Orders.Services
{
    public class DiscountService : IDiscountService
    {
        // Constants for customer types
        private const string VipCustomerCode = "X";        // Discount configuration - in a real application, this would come from configuration/database
        private static readonly Dictionary<(string Region, string CustomerType), decimal> _regionDiscountMatrix = new()
    {
        { ("NA", VipCustomerCode), 0.2m },      // 20% discount for VIP customers in North America
        { ("NA", "REGULAR"), 0.1m },              // 10% discount for Regular customers in North America
        { ("EU", VipCustomerCode), 0.15m },     // 15% discount for VIP customers in Europe
        { ("EU", "REGULAR"), 0.0m },              // No discount for Regular customers in Europe
        { ("APAC", VipCustomerCode), 0.1m },    // 10% discount for VIP customers in Asia-Pacific
        { ("APAC", "REGULAR"), 0.05m },           // 5% discount for Regular customers in Asia-Pacific
    };        public Task<decimal> CalculateDiscountAsync(string region, string customerId)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(region))
                throw new ArgumentException("Region cannot be null or empty.", nameof(region));

            if (string.IsNullOrWhiteSpace(customerId))
                throw new ArgumentException("Customer ID cannot be null or empty.", nameof(customerId));

            try
            {
                var customerType = ParseCustomerType(customerId);
                var normalizedRegion = region.ToUpperInvariant();

                // Look up discount from configuration
                var discount = LookupDiscountRate(normalizedRegion, customerType);

                return Task.FromResult(discount);
            }
            catch (Exception ex) when (!(ex is ArgumentException))
            {
                // Log the exception in a real application
                throw new InvalidOperationException($"Failed to calculate discount for customer {customerId} in region {region}.", ex);
            }
        }

        private static string ParseCustomerType(string customerId)
        {
            // Validate customer ID format
            if (!customerId.Contains('-'))
                throw new ArgumentException($"Invalid customer ID format: {customerId}. Expected format: 'TYPE-ID'.", nameof(customerId));

            var parts = customerId.Split('-');
            if (parts.Length < 2 || string.IsNullOrWhiteSpace(parts[0]))
                throw new ArgumentException($"Invalid customer ID format: {customerId}. Expected format: 'TYPE-ID'.", nameof(customerId));            var customerType = parts[0].ToUpperInvariant();

            // Return standardized customer type
            return customerType == VipCustomerCode ? VipCustomerCode : "REGULAR";
        }

        private static decimal LookupDiscountRate(string region, string customerType)
        {
            // Try to find exact match first
            if (_regionDiscountMatrix.TryGetValue((region, customerType), out var discount))
                return discount;

            // Fallback: no discount for unknown regions or customer types
            return 0.0m;
        }
    }
}