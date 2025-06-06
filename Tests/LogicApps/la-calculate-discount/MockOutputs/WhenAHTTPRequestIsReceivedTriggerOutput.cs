using Microsoft.Azure.Workflows.UnitTesting.Definitions;
using Microsoft.Azure.Workflows.UnitTesting.ErrorResponses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System;

namespace LogicApps.Tests.Mocks.la_calculate_discount
{
    /// <summary>
    /// The <see cref="WhenAHTTPRequestIsReceivedTriggerMock"/> class.
    /// </summary>
    public class WhenAHTTPRequestIsReceivedTriggerMock : TriggerMock
    {
        /// <summary>
        /// Creates a mocked instance for  <see cref="WhenAHTTPRequestIsReceivedTriggerMock"/> with static outputs.
        /// </summary>
        public WhenAHTTPRequestIsReceivedTriggerMock(TestWorkflowStatus status = TestWorkflowStatus.Succeeded, string name = null, WhenAHTTPRequestIsReceivedTriggerOutput outputs = null)
            : base(status: status, name: name, outputs: outputs ?? new WhenAHTTPRequestIsReceivedTriggerOutput())
        {
        }

        /// <summary>
        /// Creates a mocked instance for  <see cref="WhenAHTTPRequestIsReceivedTriggerMock"/> with static error info.
        /// </summary>
        public WhenAHTTPRequestIsReceivedTriggerMock(TestWorkflowStatus status, string name = null, TestErrorInfo error = null)
            : base(status: status, name: name, error: error)
        {
        }

        /// <summary>
        /// Creates a mocked instance for <see cref="WhenAHTTPRequestIsReceivedTriggerMock"/> with a callback function for dynamic outputs.
        /// </summary>
        public WhenAHTTPRequestIsReceivedTriggerMock(Func<TestExecutionContext, WhenAHTTPRequestIsReceivedTriggerMock> onGetTriggerMock, string name = null)
            : base(onGetTriggerMock: onGetTriggerMock, name: name)
        {
        }
    }


    /// <summary>
    /// Class for WhenAHTTPRequestIsReceivedTriggerOutput representing an object with properties.
    /// </summary>
    public class WhenAHTTPRequestIsReceivedTriggerOutput : MockOutput
    {
        public HttpStatusCode StatusCode {get; set;}

        public WhenAHTTPRequestIsReceivedTriggerOutputBody Body { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WhenAHTTPRequestIsReceivedTriggerOutput"/> class.
        /// </summary>
        public WhenAHTTPRequestIsReceivedTriggerOutput()
        {
            this.StatusCode = HttpStatusCode.OK;
            this.Body = new WhenAHTTPRequestIsReceivedTriggerOutputBody();
        }

    }

    /// <summary>
    /// Class for WhenAHTTPRequestIsReceivedTriggerOutputBody representing an object with properties.
    /// </summary>
    public class WhenAHTTPRequestIsReceivedTriggerOutputBody
    {
        public string OrderId { get; set; }

        public string CustomerId { get; set; }

        public string Region { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WhenAHTTPRequestIsReceivedTriggerOutputBody"/> class.
        /// </summary>
        public WhenAHTTPRequestIsReceivedTriggerOutputBody()
        {
            this.OrderId = string.Empty;
            this.CustomerId = string.Empty;
            this.Region = string.Empty;
            this.OrderDetails = new List<OrderDetail>();
        }

    }

    /// <summary>
    /// Class for  representing an object with properties.
    /// </summary>
    public class OrderDetail
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public int UnitPrice { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref=""/> class.
        /// </summary>
        public OrderDetail()
        {
            this.ProductId = string.Empty;
            this.ProductName = string.Empty;
            this.Quantity = 0;
            this.UnitPrice = 0;
        }

    }

}