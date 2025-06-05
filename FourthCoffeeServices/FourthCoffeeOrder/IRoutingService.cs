using System;
using System.Threading.Tasks;

namespace FourthCoffee.Orders.Services
{
    /// <summary>
    /// Provides routing services for orders to appropriate systems based on geographical regions.
    /// </summary>
    public interface IRoutingService
    {
        /// <summary>
        /// Routes an order to the appropriate system based on the order location.
        /// </summary>
        /// <param name="region">The region where the order originates (e.g., "NA", "EU", "APAC")</param>
        /// <returns>A task containing the name of the system where the order should be routed</returns>
        /// <exception cref="ArgumentException">Thrown when region is null or empty</exception>
        /// <exception cref="InvalidOperationException">Thrown when routing fails due to an unexpected error</exception>
        Task<string> RouteOrderAsync(string region);
    }
}