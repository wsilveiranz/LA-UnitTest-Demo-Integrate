using Microsoft.Azure.Workflows.UnitTesting.Definitions;
using Microsoft.Azure.Workflows.UnitTesting.ErrorResponses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System;

namespace LogicApps.Tests.Mocks.la_sb_process_order
{
    /// <summary>
    /// The <see cref="CallExternalAPIActionMock"/> class.
    /// </summary>
    public class CallExternalAPIActionMock : ActionMock
    {
        /// <summary>
        /// Creates a mocked instance for  <see cref="CallExternalAPIActionMock"/> with static outputs.
        /// </summary>
        public CallExternalAPIActionMock(TestWorkflowStatus status = TestWorkflowStatus.Succeeded, string name = null, CallExternalAPIActionOutput outputs = null)
            : base(status: status, name: name, outputs: outputs ?? new CallExternalAPIActionOutput())
        {
        }

        /// <summary>
        /// Creates a mocked instance for  <see cref="CallExternalAPIActionMock"/> with static error info.
        /// </summary>
        public CallExternalAPIActionMock(TestWorkflowStatus status, string name = null, TestErrorInfo error = null)
            : base(status: status, name: name, error: error)
        {
        }

        /// <summary>
        /// Creates a mocked instance for <see cref="CallExternalAPIActionMock"/> with a callback function for dynamic outputs.
        /// </summary>
        public CallExternalAPIActionMock(Func<TestExecutionContext, CallExternalAPIActionMock> onGetActionMock, string name = null)
            : base(onGetActionMock: onGetActionMock, name: name)
        {
        }
    }


    /// <summary>
    /// Class for CallExternalAPIActionOutput representing an object with properties.
    /// </summary>
    public class CallExternalAPIActionOutput : MockOutput
    {
        public HttpStatusCode StatusCode {get; set;}

        public JObject Body { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CallExternalAPIActionOutput"/> class.
        /// </summary>
        public CallExternalAPIActionOutput()
        {
            this.StatusCode = HttpStatusCode.OK;
            this.Body = new JObject();
        }

    }

}