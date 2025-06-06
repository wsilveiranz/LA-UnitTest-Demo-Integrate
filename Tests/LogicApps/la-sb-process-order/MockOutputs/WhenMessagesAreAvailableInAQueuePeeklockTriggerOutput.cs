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
    /// The <see cref="WhenMessagesAreAvailableInAQueuePeeklockTriggerMock"/> class.
    /// </summary>
    public class WhenMessagesAreAvailableInAQueuePeeklockTriggerMock : TriggerMock
    {
        /// <summary>
        /// Creates a mocked instance for  <see cref="WhenMessagesAreAvailableInAQueuePeeklockTriggerMock"/> with static outputs.
        /// </summary>
        public WhenMessagesAreAvailableInAQueuePeeklockTriggerMock(TestWorkflowStatus status = TestWorkflowStatus.Succeeded, string name = null, WhenMessagesAreAvailableInAQueuePeeklockTriggerOutput outputs = null)
            : base(status: status, name: name, outputs: outputs ?? new WhenMessagesAreAvailableInAQueuePeeklockTriggerOutput())
        {
        }

        /// <summary>
        /// Creates a mocked instance for  <see cref="WhenMessagesAreAvailableInAQueuePeeklockTriggerMock"/> with static error info.
        /// </summary>
        public WhenMessagesAreAvailableInAQueuePeeklockTriggerMock(TestWorkflowStatus status, string name = null, TestErrorInfo error = null)
            : base(status: status, name: name, error: error)
        {
        }

        /// <summary>
        /// Creates a mocked instance for <see cref="WhenMessagesAreAvailableInAQueuePeeklockTriggerMock"/> with a callback function for dynamic outputs.
        /// </summary>
        public WhenMessagesAreAvailableInAQueuePeeklockTriggerMock(Func<TestExecutionContext, WhenMessagesAreAvailableInAQueuePeeklockTriggerMock> onGetTriggerMock, string name = null)
            : base(onGetTriggerMock: onGetTriggerMock, name: name)
        {
        }
    }


    /// <summary>
    /// Class for WhenMessagesAreAvailableInAQueuePeeklockTriggerOutput representing an object with properties.
    /// </summary>
    public class WhenMessagesAreAvailableInAQueuePeeklockTriggerOutput : MockOutput
    {
        public HttpStatusCode StatusCode {get; set;}

        public WhenMessagesAreAvailableInAQueuePeeklockTriggerOutputBody Body { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WhenMessagesAreAvailableInAQueuePeeklockTriggerOutput"/> class.
        /// </summary>
        public WhenMessagesAreAvailableInAQueuePeeklockTriggerOutput()
        {
            this.StatusCode = HttpStatusCode.OK;
            this.Body = new WhenMessagesAreAvailableInAQueuePeeklockTriggerOutputBody();
        }

    }

    /// <summary>
    /// Class for WhenMessagesAreAvailableInAQueuePeeklockTriggerOutputBody representing an object with properties.
    /// </summary>
    public class WhenMessagesAreAvailableInAQueuePeeklockTriggerOutputBody
    {
        /// <summary>
        /// Content of the message.
        /// </summary>
        public JObject ContentData { get; set; }

        /// <summary>
        /// The content type of the message.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// The identifier of the session.
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// Any key-value pairs for user properties.
        /// </summary>
        public JObject UserProperties { get; set; }

        /// <summary>
        /// A user-defined value that Service Bus can use to identify duplicate messages, if enabled.
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// The lock token is a reference to the lock that is being held by the broker in peek-lock receive mode.
        /// </summary>
        public string LockToken { get; set; }

        /// <summary>
        /// Sends to address
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// The address where to send a reply.
        /// </summary>
        public string ReplyTo { get; set; }

        /// <summary>
        /// The identifier of the session where to reply.
        /// </summary>
        public string ReplyToSession { get; set; }

        /// <summary>
        /// Application specific label
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The UTC date and time for when to add the message to the queue.
        /// </summary>
        public string ScheduledEnqueueTimeUtc { get; set; }

        /// <summary>
        /// The identifier of the correlation.
        /// </summary>
        public string CorrelationId { get; set; }

        /// <summary>
        /// The number of ticks or duration for when a message is valid. The duration starts from when the message is sent to Service Bus.
        /// </summary>
        public string TimeToLive { get; set; }

        /// <summary>
        /// Only set in messages that have been dead-lettered and later autoforwarded from the dead-letter queue to another entity. Indicates the entity in which the message was dead-lettered.
        /// </summary>
        public string DeadletterSource { get; set; }

        /// <summary>
        /// Number of deliveries that have been attempted for this message. The count is incremented when a message lock expires, or the message is explicitly abandoned by the receiver.
        /// </summary>
        public int DeliveryCount { get; set; }

        /// <summary>
        /// For messages that have been autoforwarded, this property reflects the sequence number that had first been assigned to the message at its original point of submission.
        /// </summary>
        public string EnqueuedSequenceNumber { get; set; }

        /// <summary>
        /// The UTC instant at which the message has been accepted and stored in the entity.
        /// </summary>
        public string EnqueuedTimeUtc { get; set; }

        /// <summary>
        /// For messages retrieved under a lock (peek-lock receive mode, not pre-settled) this property reflects the UTC instant until which the message is held locked in the queue/subscription.
        /// </summary>
        public string LockedUntilUtc { get; set; }

        /// <summary>
        /// The sequence number is a unique 64-bit integer assigned to a message as it is accepted and stored by the broker and functions as its true identifier.
        /// </summary>
        public string SequenceNumber { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WhenMessagesAreAvailableInAQueuePeeklockTriggerOutputBody"/> class.
        /// </summary>
        public WhenMessagesAreAvailableInAQueuePeeklockTriggerOutputBody()
        {
            this.ContentData = new JObject();
            this.ContentType = string.Empty;
            this.SessionId = string.Empty;
            this.UserProperties = new JObject();
            this.MessageId = string.Empty;
            this.LockToken = string.Empty;
            this.To = string.Empty;
            this.ReplyTo = string.Empty;
            this.ReplyToSession = string.Empty;
            this.Label = string.Empty;
            this.ScheduledEnqueueTimeUtc = string.Empty;
            this.CorrelationId = string.Empty;
            this.TimeToLive = string.Empty;
            this.DeadletterSource = string.Empty;
            this.DeliveryCount = 0;
            this.EnqueuedSequenceNumber = string.Empty;
            this.EnqueuedTimeUtc = string.Empty;
            this.LockedUntilUtc = string.Empty;
            this.SequenceNumber = string.Empty;
        }

    }

}