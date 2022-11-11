using API.Models.Account;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace API.Data
{
  public class AccountContext : ApiAuthorizationDbContext<ApplicationUser>
  {
    public AccountContext(DbContextOptions<AccountContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }
  }
}