using Microsoft.AspNetCore.Authorization;
using src.Infra.Data;

namespace src.Endpoints.Employees;

public class EmployeeGetAll
{
  public static string Template => "/employees";
  public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
  public static Delegate Handle => Action;

  [Authorize(Policy = "EmployeePolicy")]
  public static async Task<IResult> Action(int? page, int? rows, QueryAllUserWithClaimName query)
  {
    var result = await query.Execute(page.Value, rows.Value);

    return Results.Ok(result);
  }
}
