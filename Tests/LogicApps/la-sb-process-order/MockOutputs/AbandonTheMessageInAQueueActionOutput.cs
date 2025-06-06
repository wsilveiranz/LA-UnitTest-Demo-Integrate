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
    /// The <see cref="AbandonTheMessageInAQueueActionMock"/> class.
    /// </summary>
    public class AbandonTheMessageInAQueueActionMock : ActionMock
    {
        /// <summary>
        /// Creates a mocked instance for  <see cref="AbandonTheMessageInAQueueActionMock"/> with static outputs.
        /// </summary>
        public AbandonTheMessageInAQueueActionMock(TestWorkflowStatus status = TestWorkflowStatus.Succeeded, string name = null, AbandonTheMessageInAQueueActionOutput outputs = null)
            : base(status: status, name: name, outputs: outputs ?? new AbandonTheMessageInAQueueActionOutput())
        {
        }

        /// <summary>
        /// Creates a mocked instance for  <see cref="AbandonTheMessageInAQueueActionMock"/> with static error info.
        /// </summary>
        public AbandonTheMessageInAQueueActionMock(TestWorkflowStatus status, string name = null, TestErrorInfo error = null)
            : base(status: status, name: name, error: error)
        {
        }

        /// <summary>
        /// Creates a mocked instance for <see cref="AbandonTheMessageInAQueueActionMock"/> with a callback function for dynamic outputs.
        /// </summary>
        public AbandonTheMessageInAQueueActionMock(Func<TestExecutionContext, AbandonTheMessageInAQueueActionMock> onGetActionMock, string name = null)
            : base(onGetActionMock: onGetActionMock, name: name)
        {
        }
    }


    /// <summary>
    /// Class for AbandonTheMessageInAQueueActionOutput representing an empty object.
    /// </summary>
    public class AbandonTheMessageInAQueueActionOutput : MockOutput
    {
        public HttpStatusCode StatusCode {get; set;}

        /// <summary>
        /// Initializes a new instance of the <see cref="AbandonTheMessageInAQueueActionOutput"/> class.
        /// </summary>
        public AbandonTheMessageInAQueueActionOutput()
        {
            this.StatusCode = HttpStatusCode.OK;
        }

    }

}