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
    /// The <see cref="CalculateDiscountActionMock"/> class.
    /// </summary>
    public class CalculateDiscountActionMock : ActionMock
    {
        /// <summary>
        /// Creates a mocked instance for  <see cref="CalculateDiscountActionMock"/> with static outputs.
        /// </summary>
        public CalculateDiscountActionMock(TestWorkflowStatus status = TestWorkflowStatus.Succeeded, string name = null, CalculateDiscountActionOutput outputs = null)
            : base(status: status, name: name, outputs: outputs ?? new CalculateDiscountActionOutput())
        {
        }

        /// <summary>
        /// Creates a mocked instance for  <see cref="CalculateDiscountActionMock"/> with static error info.
        /// </summary>
        public CalculateDiscountActionMock(TestWorkflowStatus status, string name = null, TestErrorInfo error = null)
            : base(status: status, name: name, error: error)
        {
        }

        /// <summary>
        /// Creates a mocked instance for <see cref="CalculateDiscountActionMock"/> with a callback function for dynamic outputs.
        /// </summary>
        public CalculateDiscountActionMock(Func<TestExecutionContext, CalculateDiscountActionMock> onGetActionMock, string name = null)
            : base(onGetActionMock: onGetActionMock, name: name)
        {
        }
    }


    /// <summary>
    /// Class for CalculateDiscountActionOutput representing an object with properties.
    /// </summary>
    public class CalculateDiscountActionOutput : MockOutput
    {
        public JObject Outputs { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculateDiscountActionOutput"/> class.
        /// </summary>
        public CalculateDiscountActionOutput()
        {
            this.Outputs = new JObject();
        }

    }

}