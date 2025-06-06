using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Workflows.Common.ErrorResponses;
using Microsoft.Azure.Workflows.UnitTesting;
using Microsoft.Azure.Workflows.UnitTesting.Definitions;
using Microsoft.Azure.Workflows.UnitTesting.ErrorResponses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using LogicApps.Tests.Mocks.la_sb_process_order;
using Newtonsoft.Json.Linq;

namespace LogicApps.Tests
{
    /// <summary>
    /// The unit test class.
    /// </summary>
    [TestClass]
    public class test_process_order
    {
        /// <summary>
        /// The unit test executor.
        /// </summary>
        public TestExecutor TestExecutor;

        [TestInitialize]
        public void Setup()
        {
            this.TestExecutor = new TestExecutor("la-sb-process-order/testSettings.config");
        }

        /// <summary>
        /// A sample unit test for executing the workflow named la-sb-process-order with static mocked data.
        /// This method shows how to set up mock data, execute the workflow, and assert the outcome.
        /// </summary>
        [TestMethod]
        public async Task TestNewMessage_APAC()
        {
            // PREPARE Mock
            // Generate mock action and trigger data.
            var mockData = this.GetTestMockDefinition("APAC", "New");

            // ACT
            // Create an instance of UnitTestExecutor, and run the workflow with the mock data.
            var executor = this.TestExecutor.Create();
            var testRun = await executor.RunWorkflowAsync(testMock: mockData).ConfigureAwait(continueOnCapturedContext: false);

            // ASSERT
            // Verify that the workflow executed successfully, and the status is 'Succeeded'.
            Assert.IsNotNull(value: testRun);
            Assert.AreEqual(expected: TestWorkflowStatus.Succeeded, actual: testRun.Status);
            Assert.AreEqual(expected: executor.WorkflowSettings.Parameters["insertTargetUrl"].Value, actual: testRun.Variables["TargetUrl"]);
            Assert.AreEqual(expected: TestWorkflowStatus.Skipped, actual: testRun.Actions["Condition"].ChildActions["Set_Variable_to_Update"].Status, "The action 'Set_Variable_to_Update' should be skipped because the status is 'New'.");
            Assert.IsTrue(((JObject)testRun.Actions["Generate_Final_Order"].Outputs).ContainsKey("totalAfterDiscount"), "The action 'Generate_Final_Order' should have an output named 'totalAfterDiscount'.");
            Assert.IsTrue(((JObject)testRun.Actions["Generate_Final_Order"].Outputs).ContainsKey("totalBeforeDiscount"), "The action 'Generate_Final_Order' should have an output named 'totalBeforeDiscount'.");
            Assert.IsTrue(((JObject)testRun.Actions["Generate_Final_Order"].Outputs).ContainsKey("discountValue"), "The action 'Generate_Final_Order' should have an output named 'discountValue'.");
        }

        [TestMethod]
        public async Task TestUpdateMessage_APAC()
        {
            // PREPARE Mock
            // Generate mock action and trigger data.
            var mockData = this.GetTestMockDefinition("APAC", "Update");

            // ACT
            // Create an instance of UnitTestExecutor, and run the workflow with the mock data.
            var executor = this.TestExecutor.Create();
            var testRun = await executor.RunWorkflowAsync(testMock: mockData).ConfigureAwait(continueOnCapturedContext: false);

            // ASSERT
            // Verify that the workflow executed successfully, and the status is 'Succeeded'.
            Assert.IsNotNull(value: testRun);
            Assert.AreEqual(expected: TestWorkflowStatus.Succeeded, actual: testRun.Status);
            Assert.AreEqual(expected: executor.WorkflowSettings.Parameters["updateTargetUrl"].Value, actual: testRun.Variables["TargetUrl"]);
            Assert.AreEqual(expected: TestWorkflowStatus.Succeeded, actual: testRun.Actions["Condition"].ChildActions["Set_Variable_to_Update"].Status, "The action 'Set_Variable_to_Update' should be executed because the status is 'Update'.");
            Assert.AreEqual(expected: TestWorkflowStatus.Skipped, actual: testRun.Actions["Condition"].ChildActions["Set_Variable_to_Insert"].Status, "The action 'Set_Variable_to_Insert' should be skipped because the status is 'Update'.");
            Assert.IsTrue(((JObject)testRun.Actions["Generate_Final_Order"].Outputs).ContainsKey("totalAfterDiscount"), "The action 'Generate_Final_Order' should have an output named 'totalAfterDiscount'.");
            Assert.IsTrue(((JObject)testRun.Actions["Generate_Final_Order"].Outputs).ContainsKey("totalBeforeDiscount"), "The action 'Generate_Final_Order' should have an output named 'totalBeforeDiscount'.");
            Assert.IsTrue(((JObject)testRun.Actions["Generate_Final_Order"].Outputs).ContainsKey("discountValue"), "The action 'Generate_Final_Order' should have an output named 'discountValue'.");
        }

        #region Mock Data Generation
        private TestMockDefinition GetTestMockDefinition(string region, string status)
        {
            var mockDataPath = Path.Combine(TestExecutor.rootDirectory, "Tests", TestExecutor.logicAppName, TestExecutor.workflow, "test-process-order", "test-process-order-mock.json");
            var mockDataResult = JsonConvert.DeserializeObject<TestMockDefinition>(File.ReadAllText(mockDataPath));
            mockDataResult.TriggerMock.Outputs["body"]["contentData"]["region"] = region;
            mockDataResult.TriggerMock.Outputs["body"]["contentData"]["status"] = status;
            mockDataResult.ActionMocks["Calculate_Discount"] = new CalculateDiscountActionMock(name: "Calculate_Discount", onGetActionMock: CalculateDiscountActionMockCallback);
            return mockDataResult;
        }

        /// <summary>
        /// The callback method to dynamically generate mocked data for the action named 'actionName'.
        /// You can modify this method to return different mock status, outputs, and error based on the test scenario.
        /// </summary>
        /// <param name="context">The test execution context that contains information about the current test run.</param>
        public CalculateDiscountActionMock CalculateDiscountActionMockCallback(TestExecutionContext context)
        {
            var region = context.ActionContext.ActionInputs["body"]["region"]?.ToString();
            var discount = region switch
            {
                "EU" => 0.15, // Example discount for EU region
                "NA" => 0.2,  // Example discount for NA region
                "APAC" => 0.10, // Example discount for APAC region
                _ => 0.0      // Default discount
            };

            var totalBeforeDiscount = 100.0; // Example total before discount
            return new CalculateDiscountActionMock(
                name: "Calculate_Discount",
                status: TestWorkflowStatus.Succeeded,
                outputs: new CalculateDiscountActionOutput
                {
                    Body = new JObject
                    {
                        ["discountValue"] = totalBeforeDiscount * discount,
                        ["totalBeforeDiscount"] = totalBeforeDiscount,
                        ["totalAfterDiscount"] = totalBeforeDiscount - (totalBeforeDiscount * discount)
                    }
                }
            );

        }

        #endregion
    }
}