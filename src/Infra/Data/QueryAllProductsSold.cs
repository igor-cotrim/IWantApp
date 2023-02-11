namespace src.Infra.Data;

public class QueryAllProductsSold
{
  private readonly IConfiguration _configuration;
  public QueryAllProductsSold(IConfiguration configuration)
  {
    this._configuration = configuration;
  }

  public async Task<IEnumerable<ProductSoldReportResponse>> Execute()
  {
    var db = new SqlConnection(_configuration["ConnectionString:IWantDb"]);
    var query =
      @"SELECT p.Id, p.Name, COUNT(*) Amount 
      FROM Orders o INNER JOIN OrderProducts op on o.Id = op.OrdersId
      INNER JOIN Products p on p.Id = op.ProductsId
      GROUP BY p.Id, p.Name ORDER by Amount desc";

    return await db.QueryAsync<ProductSoldReportResponse>(query);
  }
}
