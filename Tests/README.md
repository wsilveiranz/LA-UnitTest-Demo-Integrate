# Logic Apps Unit Testing Framework

This project provides comprehensive unit testing capabilities for Azure Logic Apps Standard workflows using MSTest framework and the Microsoft Azure Workflows Unit Testing SDK.

## Overview

The testing framework enables isolated testing of Logic Apps workflows by mocking external dependencies and validating workflow execution paths, business logic, and data transformations without requiring actual Azure resources.

## Project Structure

```
Tests/
├── Tests.sln                          # Visual Studio solution file
├── nuget.config                       # NuGet package configuration
├── LogicApps/                         # Main test project
│   ├── LogicApps.csproj               # MSTest project configuration
│   ├── TestExecutor.cs                # Base test execution framework
│   ├── la-calculate-discount/         # Discount calculation workflow tests
│   │   ├── testSettings.config        # Test configuration for this workflow
│   │   ├── MockOutputs/               # Mock response classes
│   │   └── test-calculate-discount-csharp/
│   │       ├── test-calculate-discount-csharp.cs      # Test implementation
│   │       └── test-calculate-discount-csharp-mock.json # Mock data definitions
│   ├── la-sb-process-order/          # Service Bus order processing tests
│   │   ├── testSettings.config        # Test configuration for this workflow
│   │   ├── MockOutputs/               # Mock response classes
│   │   └── test-process-order/
│   │       ├── test-process-order.cs  # Test implementation
│   │       └── test-process-order-mock.json # Mock data definitions
│   └── TestResults/                   # Test execution results
```

## Technologies Used

- **MSTest 3.2.0** - Microsoft testing framework for .NET
- **Microsoft.Azure.Workflows.WebJobs.Tests.Extension 1.0.0** - Logic Apps unit testing SDK
- **.NET 8.0** - Target framework
- **Coverlet** - Code coverage collection

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022 or VS Code with C# extension
- Azure Logic Apps Standard workspace (for testing against)

### Setup Instructions

1. **Clone or navigate to the Tests directory**:
   ```powershell
   cd "c:\dev\Integrate Demos\UnitTest-DynamicCallBack\Tests"
   ```

2. **Restore dependencies**:
   ```powershell
   dotnet restore
   ```

3. **Build the solution**:
   ```powershell
   dotnet build
   ```

4. **Configure test settings** (if needed):
   - Update `testSettings.config` files in each workflow test folder
   - Ensure paths point to your Logic Apps workspace

## Test Architecture

### TestExecutor Class

The `TestExecutor` class provides the core testing infrastructure:

```csharp
public class TestExecutor
{
    public string rootDirectory;    // Path to Logic Apps workspace
    public string logicAppName;     // Logic App folder name
    public string workflow;         // Specific workflow to test
    
    public UnitTestExecutor Create(); // Creates test execution context
}
```

### Configuration Structure

Each workflow test has its own `testSettings.config`:

```xml
<configuration>
    <TestSettings>
        <WorkspacePath>../../../../../</WorkspacePath>
        <LogicAppName>LogicApps</LogicAppName>
        <WorkflowName>la-calculate-discount</WorkflowName>
    </TestSettings>
</configuration>
```

### Mock Data Framework

The testing framework uses two types of mocking:

1. **Action Mocks**: Mock individual workflow actions
2. **Trigger Mocks**: Mock workflow triggers and inputs

Example mock structure:
```csharp
[TestMethod]
public async Task Test_Calculate_Discount_APAC()
{
    // PREPARE - Create mock data
    var mockData = this.GetTestMockDefinition("APAC", expectedDiscount);
    
    // ACT - Execute workflow with mocks
    var testRun = await this.TestExecutor
        .Create()
        .RunWorkflowAsync(testMock: mockData);
    
    // ASSERT - Validate results
    Assert.IsNotNull(testRun);
    Assert.AreEqual(TestWorkflowStatus.Succeeded, testRun.Status);
}
```

## Available Test Suites

### 1. Calculate Discount Workflow Tests (`la-calculate-discount`)

**Purpose**: Tests the discount calculation logic for different regions and scenarios.

**Test Cases**:
- `Test_Calculate_Discount_APAC()` - Validates APAC region discount calculations
- Dynamic mock data generation for different regions
- C# script execution validation

**Key Features**:
- Region-based discount logic testing
- Dynamic mock data generation
- Custom C# script validation

### 2. Service Bus Process Order Tests (`la-sb-process-order`)

**Purpose**: Tests the Service Bus message processing workflow.

**Test Cases**:
- `TestNewMessage_APAC()` - Tests new message processing for APAC region
- Message completion/abandonment scenarios
- External API call mocking

**Key Features**:
- Service Bus trigger mocking
- Message processing validation
- External API integration testing
- Queue operation simulation

## Running Tests

### Command Line Execution

**Run all tests**:
```powershell
dotnet test
```

**Run specific test class**:
```powershell
dotnet test --filter "ClassName=test_calculate_discount_csharp"
```

**Run with verbose output**:
```powershell
dotnet test --verbosity normal
```

**Generate code coverage**:
```powershell
dotnet test --collect:"XPlat Code Coverage"
```

### Visual Studio Execution

1. Open `Tests.sln` in Visual Studio
2. Build the solution (Ctrl+Shift+B)
3. Open Test Explorer (Test → Test Explorer)
4. Run individual tests or all tests

### VS Code Execution

1. Install the C# extension
2. Install .NET Core Test Explorer extension
3. Open the Tests folder in VS Code
4. Use the Test Explorer panel to run tests

## Test Configuration

### Workspace Path Configuration

The tests reference the Logic Apps workspace through relative paths:
- Default: `../../../../../` (points to project root)
- Adjust in `testSettings.config` if your structure differs

### Environment Variables

Tests use the following configuration sources:
- `testSettings.config` - Test-specific settings
- `workflow.json` - Workflow definitions from Logic Apps
- `connections.json` - Connection configurations
- `parameters.json` - Workflow parameters
- `local.settings.json` - Local development settings

## Writing New Tests

### 1. Create Test Structure

```
la-your-workflow/
├── testSettings.config
├── MockOutputs/
│   └── YourActionOutput.cs
└── test-your-scenario/
    ├── test-your-scenario.cs
    └── test-your-scenario-mock.json
```

### 2. Configure Test Settings

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <TestSettings>
        <WorkspacePath>../../../../../</WorkspacePath>
        <LogicAppName>LogicApps</LogicAppName>
        <WorkflowName>la-your-workflow</WorkflowName>
    </TestSettings>
</configuration>
```

### 3. Implement Test Class

```csharp
[TestClass]
public class test_your_scenario
{
    public TestExecutor TestExecutor;

    [TestInitialize]
    public void Setup()
    {
        this.TestExecutor = new TestExecutor("la-your-workflow/testSettings.config");
    }

    [TestMethod]
    public async Task Test_Your_Scenario()
    {
        // Arrange
        var mockData = CreateMockData();
        
        // Act
        var testRun = await this.TestExecutor
            .Create()
            .RunWorkflowAsync(testMock: mockData);
        
        // Assert
        Assert.IsNotNull(testRun);
        Assert.AreEqual(TestWorkflowStatus.Succeeded, testRun.Status);
    }
}
```

## Best Practices

### Test Organization
- One test class per workflow scenario
- Descriptive test method names
- Clear Arrange-Act-Assert structure

### Mock Data Management
- Use realistic test data that represents actual scenarios
- Create reusable mock data generators
- Version control mock JSON files for repeatability

### Assertions
- Test both success and failure scenarios
- Validate workflow status and outputs
- Check intermediate action results when necessary

### Configuration Management
- Keep test configurations separate from runtime configurations
- Use relative paths for portability
- Document any special configuration requirements

## Troubleshooting

### Common Issues

**Test Discovery Problems**:
- Ensure project targets .NET 8.0
- Verify MSTest packages are properly referenced
- Check that test classes are marked with `[TestClass]`

**Workflow Loading Errors**:
- Verify `testSettings.config` paths are correct
- Ensure Logic Apps workspace structure is intact
- Check that required JSON files exist (workflow.json, connections.json, etc.)

**Mock Data Issues**:
- Validate JSON syntax in mock files
- Ensure mock classes inherit from appropriate base classes
- Check that mock data matches expected workflow inputs

**Runtime Errors**:
- Review test output for detailed error messages
- Verify all required NuGet packages are installed
- Check that workflow definitions are valid

### Debugging Tips

1. **Enable verbose logging**:
   ```powershell
   dotnet test --verbosity diagnostic
   ```

2. **Debug individual tests** in Visual Studio:
   - Set breakpoints in test methods
   - Use Debug → Start Debugging on specific tests

3. **Validate workflow definitions**:
   - Use the Logic Apps Designer to verify workflow syntax
   - Test workflows manually before creating unit tests

## Contributing

When adding new tests:

1. Follow the established folder structure
2. Include comprehensive mock data
3. Add documentation for complex test scenarios
4. Ensure tests are deterministic and repeatable
5. Update this README with any new testing patterns

## References

- [Azure Logic Apps Unit Testing Documentation](https://docs.microsoft.com/en-us/azure/logic-apps/test-logic-apps-mock-data-static-results)
- [MSTest Framework Documentation](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest)
- [.NET Testing Best Practices](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)
