using API.Models.Account;
using API.Models.ToDo;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace API.Data
{
  public class DataContext : ApiAuthorizationDbContext<ApplicationUser>
  {
    public DataContext(DbContextOptions<DataContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }

    public DbSet<ToDoItem> ToDoItems { get; set; }
  }
}