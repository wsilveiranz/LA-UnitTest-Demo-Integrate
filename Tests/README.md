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
- VS Code with C# extension
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

The `TestExecutor` class provides the core testing infrastructure for executing Logic Apps workflows in isolation.

### Configuration Structure

Each workflow test has its own `testSettings.config` file that defines the workspace path, Logic App name, and specific workflow to test.

### Mock Data Framework

The testing framework uses two types of mocking:

1. **Action Mocks**: Mock individual workflow actions
2. **Trigger Mocks**: Mock workflow triggers and inputs

Tests follow the standard Arrange-Act-Assert pattern for validating workflow execution, status, and outputs.

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

### VS Code Execution

1. Install the C# extension for VS Code
2. Install .NET Core Test Explorer extension
3. Open the Tests folder in VS Code
4. Use the Test Explorer panel to run individual tests or all tests
5. Use the integrated terminal to run dotnet test commands

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

### Test Structure

New workflow tests should follow the established folder structure with:
- Test configuration files (`testSettings.config`)
- Mock output classes in `MockOutputs/` folder
- Test implementation files with mock data JSON files

### Implementation Guidelines

Test classes should use the MSTest framework with proper test initialization and the TestExecutor class for workflow execution. Each test should follow the Arrange-Act-Assert pattern for clear and maintainable test code.

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

2. **Debug individual tests** in VS Code:
   - Set breakpoints in test methods
   - Use the Debug configuration in VS Code to run tests with debugging

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

- [Azure Logic Apps Unit Testing Documentation](https://learn.microsoft.com/en-us/azure/logic-apps/testing-framework/create-unit-tests-standard-workflow-runs-visual-studio-code)
- [MSTest Framework Documentation](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest)
- [.NET Testing Best Practices](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)
