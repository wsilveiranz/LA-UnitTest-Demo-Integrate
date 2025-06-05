using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FourthCoffee.Orders.Services
{
    /// <summary>
    /// Service responsible for routing orders to appropriate target systems based on region.
    /// </summary>
    public class OrderRoutingService : IRoutingService
    {
        // Routing configuration - in a real application, this would come from configuration/database
        private static readonly Dictionary<string, string> _regionRoutingConfiguration = new()
        {
            { "NA", "Salesforce" },        // North America routes to Salesforce
            { "EU", "Dynamics365" },       // Europe routes to Dynamics 365
            { "APAC", "CustomAPI" },       // Asia-Pacific routes to Custom API
        };

        private const string DefaultTargetSystem = "DefaultSystem";        public Task<string> RouteOrderAsync(string region)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(region))
                throw new ArgumentException("Region cannot be null or empty.", nameof(region));

            try
            {
                var normalizedLocation = region.ToUpperInvariant();
                var routingDestination = ResolveTargetSystem(normalizedLocation);
                
                return Task.FromResult(routingDestination);
            }
            catch (Exception ex) when (!(ex is ArgumentException))
            {
                // Log the exception in a real application
                throw new InvalidOperationException($"Failed to route order for region {region}.", ex);
            }
        }

        private static string ResolveTargetSystem(string normalizedLocation)
        {
            // Try to find exact match first
            if (_regionRoutingConfiguration.TryGetValue(normalizedLocation, out var destination))
                return destination;

            // Fallback to default system for unknown regions
            return DefaultTargetSystem;
        }
    }
}