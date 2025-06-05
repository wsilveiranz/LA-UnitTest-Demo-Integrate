//------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
//------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Extensions.Workflows;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using FourthCoffee.Orders.Services;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace FourthCoffee.Orders
{

    /// <summary>
    /// Represents the OrderRouter flow invoked function.
    /// </summary>
    public class OrderRouter
    {
        private readonly ILogger<OrderRouter> logger;
        private readonly IRoutingService routingService;
        private readonly IDiscountService discountService;

        public OrderRouter(ILoggerFactory loggerFactory, IRoutingService routingService, IDiscountService discountService)
        {
            this.logger = loggerFactory.CreateLogger<OrderRouter>();
            this.routingService = routingService;
            this.discountService = discountService;
        }

        /// <summary>
        /// Executes the logic app workflow.
        /// </summary>
        /// <param name="order">The order to be routed.</param>
        [Function("OrderRouter")]
        public Task<string> OrderRouterRun([WorkflowActionTrigger] string orderLocation)
        {
            this.logger.LogInformation("Starting OrderRouter with Region: " + orderLocation);

            var destinationSystem = this.routingService.RouteOrderAsync(orderLocation).GetAwaiter().GetResult();
            this.logger.LogInformation("Order routed to: " + destinationSystem);

            return Task.FromResult(destinationSystem);
        }

        [Function("CalculateDiscount")]
        public Task<OrderCost> CalculateDiscountRun([WorkflowActionTrigger] JsonObject order)
        {
            Order myOrder = JsonSerializer.Deserialize<Order>(order.ToJsonString(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            this.logger.LogInformation("Starting CalculateDiscount with Region  " + myOrder.Region + " and Customer " + myOrder.CustomerId);

            var discount = this.discountService.CalculateDiscountAsync(myOrder.Region, myOrder.CustomerId).GetAwaiter().GetResult();
            this.logger.LogInformation("Calculated discount: " + discount);

            var totalBeforeDiscount = 0.0m;
            foreach (var item in myOrder.OrderDetails)
            {
                totalBeforeDiscount += item.UnitPrice * item.Quantity;
            }
            this.logger.LogInformation("Total order value: " + totalBeforeDiscount);
            var appliedDiscount = totalBeforeDiscount * discount;
            var totalAfterDiscount = totalBeforeDiscount - appliedDiscount;
            return Task.FromResult(new OrderCost(totalBeforeDiscount, appliedDiscount, totalAfterDiscount));
        }

    }

    public class OrderCost
        {
            public decimal TotalBeforeDiscount { get; set; }
            public decimal AppliedDiscount { get; set; }
            public decimal TotalAfterDiscount { get; set; }

            public OrderCost(decimal totalBeforeDiscount, decimal appliedDiscount, decimal totalAfterDiscount)
            {
                TotalBeforeDiscount = totalBeforeDiscount;
                AppliedDiscount = appliedDiscount;
                TotalAfterDiscount = totalAfterDiscount;
            }
        }
}