// Add the required libraries
#r "Newtonsoft.Json"
#r "Microsoft.Azure.Workflows.Scripting"
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Workflows.Scripting;
using Newtonsoft.Json.Linq;
using System.Linq;

/// <summary>
/// Executes the inline csharp code.
/// </summary>
/// <param name="context">The workflow context.</param>
/// <remarks> This is the entry-point to your code. The function signature should remain unchanged.</remarks>
public static async Task<Results> Run(WorkflowContext context, ILogger log)
{
  JToken triggerOutputs = (await context.GetTriggerResults().ConfigureAwait(false)).Outputs;
  JToken actionCompose = (await context.GetActionResults("Compose").ConfigureAwait(false)).Outputs;

  var discount = (float)actionCompose;
  var products = triggerOutputs?["body"]?["OrderDetails"] as JArray;
  var totalBeforeDiscount = products?.Sum(item => (float)item["Quantity"] * (float)item["UnitPrice"]) ?? 0;
  var discountValue = totalBeforeDiscount * discount;
  var totalAfterDiscount = totalBeforeDiscount - discountValue;

  return new Results(){
    totalBeforeDiscount = totalBeforeDiscount,
    totalAfterDiscount = totalAfterDiscount,
    discountValue = discountValue
  };
}

public class Results
{
  public float totalBeforeDiscount {get; set;}
  public float discountValue {get; set;}
  public float totalAfterDiscount {get; set;}
}