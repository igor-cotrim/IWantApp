namespace src.Endpoints.Categories;

public class CategoryGetAll
{
  public static string Template => "/categories";
  public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
  public static Delegate Handle => Action;

  [Authorize(Policy = "EmployeePolicy")]
  public static async Task<IResult> Action(ApplicationDbContext context)
  {
    var categories = await context.Categories.ToListAsync();
    var response = categories.Select(c => new CategoryResponse(c.Id, c.Name, c.Active));

    return Results.Ok(response);
  }
}
