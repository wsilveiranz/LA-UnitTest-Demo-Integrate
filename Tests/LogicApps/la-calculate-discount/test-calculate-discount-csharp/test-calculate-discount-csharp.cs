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
using LogicApps.Tests.Mocks.la_calculate_discount;
using Newtonsoft.Json.Linq;

namespace LogicApps.Tests
{
    /// <summary>
    /// The unit test class.
    /// </summary>
    [TestClass]
    public class test_calculate_discount_csharp
    {
        /// <summary>
        /// The unit test executor.
        /// </summary>
        public TestExecutor TestExecutor;

        [TestInitialize]
        public void Setup()
        {
            this.TestExecutor = new TestExecutor("la-calculate-discount/testSettings.config");
        }

        /// <summary>
        /// A sample unit test for executing the workflow named la-calculate-discount with dynamic mocked data.
        /// This method shows how to set up mock data, execute the workflow, and assert the outcome.
        /// </summary>
        [TestMethod]
        public async Task Test_Calculate_Discount_APAC()
        {
            // PREPARE Mock
            // Generate mock action and trigger data.
            var expectedDiscount = 0.1; // Expected discount for APAC region
            var mockData = this.GetTestMockDefinition("APAC", expectedDiscount);

            // ACT
            // Create an instance of UnitTestExecutor, and run the workflow with the mock data.
            var testRun = await this.TestExecutor
                .Create()
                .RunWorkflowAsync(testMock: mockData).ConfigureAwait(continueOnCapturedContext: false);

            // ASSERT
            // Verify that the workflow executed successfully, and the status is 'Succeeded'.
            Assert.IsNotNull(value: testRun);
            Assert.AreEqual(expected: TestWorkflowStatus.Succeeded, actual: testRun.Status);
            Assert.AreEqual(expected: expectedDiscount, actual: testRun.Variables["discount"], message: "The discount variable should be 0.1 as per the mock data."); 
        }

        [TestMethod]
        public async Task Test_Calculate_Discount_EU()
        {
            // PREPARE Mock
            // Generate mock action and trigger data.
            var expectedDiscount = 0.15;
            var mockData = this.GetTestMockDefinition("EU", expectedDiscount);

            // ACT
            // Create an instance of UnitTestExecutor, and run the workflow with the mock data.
            var testRun = await this.TestExecutor
                .Create()
                .RunWorkflowAsync(testMock: mockData).ConfigureAwait(continueOnCapturedContext: false);

            // ASSERT
            // Verify that the workflow executed successfully, and the status is 'Succeeded'.
            Assert.IsNotNull(value: testRun);
            Assert.AreEqual(expected: TestWorkflowStatus.Succeeded, actual: testRun.Status);
            Assert.AreEqual(expected: expectedDiscount, actual: testRun.Variables["discount"], message: "The discount variable should be 0.1 as per the mock data.");
        }

        [TestMethod]
        public async Task Test_Calculate_Discount_NA()
        {
            // PREPARE Mock
            // Generate mock action and trigger data.
            var expectedDiscount = 0.2; // Expected discount for APAC region
            var mockData = this.GetTestMockDefinition("NA", expectedDiscount);

            // ACT
            // Create an instance of UnitTestExecutor, and run the workflow with the mock data.
            var testRun = await this.TestExecutor
                .Create()
                .RunWorkflowAsync(testMock: mockData).ConfigureAwait(continueOnCapturedContext: false);

            // ASSERT
            // Verify that the workflow executed successfully, and the status is 'Succeeded'.
            Assert.IsNotNull(value: testRun);
            Assert.AreEqual(expected: TestWorkflowStatus.Succeeded, actual: testRun.Status);
            Assert.AreEqual(expected: expectedDiscount, actual: testRun.Variables["discount"], message: "The discount variable should be 0.1 as per the mock data."); 
        }

        #region Mock generator helpers

        /// <summary>
        /// Returns deserialized test mock data.  
        /// </summary>
        private TestMockDefinition GetTestMockDefinition(string region, double expectedDiscount)
        {
            var mockDataPath = Path.Combine(TestExecutor.rootDirectory, "Tests", TestExecutor.logicAppName, TestExecutor.workflow, "test-calculate-discount-csharp", "test-calculate-discount-csharp-mock.json");
            var mockDataResult = JsonConvert.DeserializeObject<TestMockDefinition>(File.ReadAllText(mockDataPath));
            mockDataResult.TriggerMock.Outputs["body"]["region"] = region;
            var totalBeforeDiscount = 100.0; // Example total before discount
            mockDataResult.ActionMocks["Calculate_Discount"] = new CalculateDiscountActionMock(name: "Calculate_Discount", status: TestWorkflowStatus.Succeeded, outputs: new CalculateDiscountActionOutput
                    {
                        Outputs = new JObject
                        {
                                ["totalBeforeDiscount"] = totalBeforeDiscount, // Example total before discount
                                ["discountValue"] = totalBeforeDiscount * expectedDiscount,
                                ["totalAfterDiscount"] = totalBeforeDiscount - (totalBeforeDiscount * expectedDiscount)
                            }                        
                    }
                );
            return mockDataResult;
        }
        #endregion
    }
}