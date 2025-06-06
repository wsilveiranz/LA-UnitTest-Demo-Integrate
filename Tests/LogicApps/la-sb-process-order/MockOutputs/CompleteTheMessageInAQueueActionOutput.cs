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
    /// The <see cref="CompleteTheMessageInAQueueActionMock"/> class.
    /// </summary>
    public class CompleteTheMessageInAQueueActionMock : ActionMock
    {
        /// <summary>
        /// Creates a mocked instance for  <see cref="CompleteTheMessageInAQueueActionMock"/> with static outputs.
        /// </summary>
        public CompleteTheMessageInAQueueActionMock(TestWorkflowStatus status = TestWorkflowStatus.Succeeded, string name = null, CompleteTheMessageInAQueueActionOutput outputs = null)
            : base(status: status, name: name, outputs: outputs ?? new CompleteTheMessageInAQueueActionOutput())
        {
        }

        /// <summary>
        /// Creates a mocked instance for  <see cref="CompleteTheMessageInAQueueActionMock"/> with static error info.
        /// </summary>
        public CompleteTheMessageInAQueueActionMock(TestWorkflowStatus status, string name = null, TestErrorInfo error = null)
            : base(status: status, name: name, error: error)
        {
        }

        /// <summary>
        /// Creates a mocked instance for <see cref="CompleteTheMessageInAQueueActionMock"/> with a callback function for dynamic outputs.
        /// </summary>
        public CompleteTheMessageInAQueueActionMock(Func<TestExecutionContext, CompleteTheMessageInAQueueActionMock> onGetActionMock, string name = null)
            : base(onGetActionMock: onGetActionMock, name: name)
        {
        }
    }


    /// <summary>
    /// Class for CompleteTheMessageInAQueueActionOutput representing an empty object.
    /// </summary>
    public class CompleteTheMessageInAQueueActionOutput : MockOutput
    {
        public HttpStatusCode StatusCode {get; set;}

        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteTheMessageInAQueueActionOutput"/> class.
        /// </summary>
        public CompleteTheMessageInAQueueActionOutput()
        {
            this.StatusCode = HttpStatusCode.OK;
        }

    }

}