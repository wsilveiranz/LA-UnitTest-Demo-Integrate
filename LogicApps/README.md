# Local Development Setup

This guide provides step-by-step instructions for setting up local development environment for this Logic Apps Standard project.

## Overview

The `local.settings.json` file contains configuration settings required for local development. This file should **never** be committed to source control as it contains sensitive connection strings and local-specific paths.

## Quick Setup Steps

### 1. Create local.settings.json
```powershell
# Navigate to the LogicApps folder
cd LogicApps

# Copy the template file
copy cloud.settings.json local.settings.json
```

### 2. Configure Required Settings

Update the following values in your `local.settings.json` file:

| Setting Key | Description | Where to Find | Example |
|-------------|-------------|---------------|---------|
| ProjectDirectoryPath | Full path to LogicApps folder | Local file system | `C:\path\to\your\project\LogicApps` |
| WORKFLOWS_SUBSCRIPTION_ID | Azure subscription ID | Azure Portal > Subscriptions | `80d4fe69-c95b-4dd2-a938-9250f1c8ab03` |
| WORKFLOWS_TENANT_ID | Azure tenant ID | Azure Portal > Azure Active Directory > Properties | `72f988bf-86f1-41af-91ab-2d7cd011db47` |
| WORKFLOWS_RESOURCE_GROUP_NAME | Target resource group name | Azure Portal > Resource groups | `WSilveira-Sandbox` |
| WORKFLOWS_LOCATION_NAME | Azure region for deployment | Azure Portal > Resource groups > Location | `australiaeast` |
| serviceBus_connectionString | Service Bus namespace connection string | Azure Portal > Service Bus > Shared access policies | `Endpoint=sb://your-namespace.servicebus.windows.net/;SharedAccessKeyName=...` |

### 3. Pre-configured Values (Do Not Modify)

These values are already set correctly and should remain unchanged:
- `AzureWebJobsStorage`
- `APP_KIND` 
- `FUNCTIONS_WORKER_RUNTIME`
- `AzureWebJobsFeatureFlags`

## Detailed Configuration Guide

### Service Bus Configuration

1. **Create Service Bus Namespace**: 
   - Go to Azure Portal > Create a resource > Service Bus
   - Create a new namespace or use existing one
   - Create a queue for order processing

2. **Get Connection String**:
   - Navigate to your Service Bus namespace
   - Go to Shared access policies
   - Select or create a policy with Send/Listen permissions
   - Copy the connection string

3. **Queue Configuration**:
   - Ensure you have a queue created for order processing
   - Note the queue name for workflow parameters

### Azure Workflows Configuration

Configure the Azure-specific settings for workflow deployment and management:
- Set your subscription ID for resource targeting
- Configure tenant ID for authentication
- Specify resource group for deployment
- Set location for regional deployment

### Connection String Format Examples

**Service Bus:**
```
Endpoint=sb://[namespace].servicebus.windows.net/;SharedAccessKeyName=[keyname];SharedAccessKey=[key]
```

## Sample Configuration

Your completed `local.settings.json` should look similar to this:

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "node",
    "APP_KIND": "workflowapp",
    "AzureWebJobsFeatureFlags": "EnableMultiLanguageWorker",
    "ProjectDirectoryPath": "C:\\path\\to\\your\\project\\LogicApps",
    "WORKFLOWS_SUBSCRIPTION_ID": "your-subscription-id",
    "WORKFLOWS_TENANT_ID": "your-tenant-id",
    "WORKFLOWS_RESOURCE_GROUP_NAME": "your-resource-group",
    "WORKFLOWS_LOCATION_NAME": "australiaeast",
    "WORKFLOWS_MANAGEMENT_BASE_URI": "https://management.azure.com/",
    "serviceBus_connectionString": "Endpoint=sb://your-namespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=your-key"
  }
}
```

## Verification Steps

1. **Test Local Run**: Use VS Code Azure Logic Apps extension to start the local runtime
2. **Check Connections**: Verify all connections are working in the Logic Apps Designer
3. **Test Workflows**: Run a simple test of each workflow to ensure proper configuration

## Important Security Notes

- ⚠️ **Never commit `local.settings.json` to source control**
- 🔐 **Use proper Azure RBAC permissions instead of connection strings when possible**
- 🔄 **Rotate keys regularly and update local settings accordingly**

## Troubleshooting

### Common Issues

**File Path Issues (Windows):**
- Ensure paths use double backslashes (`\\`) or forward slashes (`/`)
- Example: `C:\\projects\\myapp` or `C:/projects/myapp`

**Connection Failures:**
- Verify connection strings are complete and unmodified
- Check firewall settings for local development
- Ensure Azure resources allow access from your IP

**Runtime Issues:**
- Verify .NET and Azure Functions Core Tools are installed
- Check VS Code Azure Logic Apps extension is up to date
- Review terminal output for specific error messages

### Getting Help

**For Service Bus Issues:**
- Use Azure Service Bus Explorer to test connectivity
- Check Azure Portal metrics for failed connections
- Review Service Bus namespace configuration

**For Logic Apps Runtime Issues:**
- Check the terminal output when running `func host start`
- Verify all dependencies are installed (Node.js, .NET SDK, Azure Functions Core Tools)
- Review the Azure Logic Apps extension logs in VS Code

**For Workflow Testing:**
- Use the built-in test functionality in VS Code
- Check the workflow run history for detailed error information
- Verify all required parameters are configured in parameters.json
