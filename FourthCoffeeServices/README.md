# Fourth Coffee Orders Services

A comprehensive .NET service library for Fourth Coffee order processing that provides region-based order routing and customer discount management.

## Features

- **Order Routing**: Automatically route orders to appropriate systems based on customer region
- **Discount Management**: Calculate customer discounts based on region and customer type
- **Region Support**: North America (NA), Europe (EU), and Asia-Pacific (APAC)
- **Enterprise Ready**: Built with proper error handling, async/await patterns, and comprehensive logging support

## Installation

```bash
dotnet add package FourthCoffee.Orders.Services
```

## Quick Start

### Discount Service

```csharp
using FourthCoffee.Orders.Services;

// Initialize the discount service
IDiscountService discountService = new DiscountService();

// Calculate discount for a VIP customer in North America
var discount = await discountService.CalculateDiscountAsync("NA", "X-12345");
Console.WriteLine($"Discount: {discount:P}"); // Output: Discount: 20.00%
```

### Order Routing Service

```csharp
using FourthCoffee.Orders.Services;

// Initialize the routing service
IRoutingService routingService = new OrderRoutingService();

// Route an order from Europe
var targetSystem = await routingService.RouteOrderAsync("EU");
Console.WriteLine($"Route to: {targetSystem}"); // Output: Route to: Dynamics365
```

## Supported Regions

| Region Code | Description | Routing Destination | VIP Discount | Regular Discount |
|-------------|-------------|-------------------|--------------|------------------|
| NA | North America | Salesforce | 20% | 10% |
| EU | Europe | Dynamics365 | 15% | 0% |
| APAC | Asia-Pacific | CustomAPI | 10% | 5% |

## Customer ID Format

Customer IDs should follow the format: `{TYPE}-{ID}`

- **VIP Customers**: `X-{ID}` (e.g., "X-12345")
- **Regular Customers**: `{ANY}-{ID}` (e.g., "R-67890")

## Error Handling

The services include comprehensive error handling:

- `ArgumentException`: Thrown for invalid input parameters
- `InvalidOperationException`: Thrown for unexpected processing errors

## Version History

### 1.1.0 (Current)
- Updated naming conventions to follow C# best practices
- Improved method and parameter names for better clarity
- Enhanced namespace organization
- Added comprehensive XML documentation

### 1.0.0
- Initial release with basic routing and discount functionality

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For questions and support, please contact the Fourth Coffee Development Team.
