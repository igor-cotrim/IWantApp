using Dapper;
using Microsoft.Data.SqlClient;
using src.Endpoints.Employees;

namespace src.Infra.Data;

public class QueryAllUserWithClaimName
{
  private readonly IConfiguration _configuration;
  public QueryAllUserWithClaimName(IConfiguration configuration)
  {
    this._configuration = configuration;
  }

  public async Task<IEnumerable<EmployeeResponse>> Execute(int page, int rows)
  {
    var db = new SqlConnection(_configuration["ConnectionString:IWantDb"]);
    var query = @"select Email, ClaimValue as Name
    from AspNetUsers u inner
    join AspNetUserClaims c
    on u.id = c.UserId and claimtype = 'Name'
    order by name
    OFFSET (@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

    return await db.QueryAsync<EmployeeResponse>(query, new { page, rows });
  }
}
