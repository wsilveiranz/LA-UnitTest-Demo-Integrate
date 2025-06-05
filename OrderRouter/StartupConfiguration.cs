using Microsoft.Azure.Functions.Extensions.Workflows;
using Microsoft.Extensions.DependencyInjection;
using FourthCoffee.Orders.Services;

namespace FourthCoffee.Orders
{

    public class StartupConfiguration : IConfigureStartup
    {
        /// <summary>
        /// Configures services for the Azure Functions application.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        public void Configure(IServiceCollection services)
        {
            // Register the routing service with dependency injection
            services.AddSingleton<IRoutingService, OrderRoutingService>();
            services.AddSingleton<IDiscountService, DiscountService>();
        }
    }
}