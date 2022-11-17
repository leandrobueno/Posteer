using System.Security.Claims;
using API.Interfaces;
using API.Models;
using API.Models.Account;
using API.Models.ToDo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ToDoController : ControllerBase
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITodoRepository _repo;
    public ToDoController(UserManager<ApplicationUser> userManager, ITodoRepository repo)
    {
      _repo = repo;
      _userManager = userManager;
    }

    [HttpPost(Name = "Create")]
    public async Task<ActionResult<Response<ItemToReturn>>> Create(ItemToCreate item)
    {
      var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email));
      if (user == null) return BadRequest("User not found. Please, login and try again with a valid user.");

      item.UserId = Guid.Parse(user.Id);

      var createdItem = await _repo.Create(item);

      if (!createdItem.Success)
        return StatusCode(StatusCodes.Status500InternalServerError, createdItem);

      return CreatedAtRoute("Get", new { createdItem.Data!.Id }, createdItem);
    }

    [HttpGet("{id}", Name = "Get")]
    public async Task<ActionResult<Response<ItemToReturn>>> Get(string id)
    {
      if (id == null) return BadRequest("Invalid Id. Please provide a valid item Id");
      var item = await _repo.Get(Guid.Parse(id));

      if (!item.Success) return NotFound(item);

      return Ok(item);
    }

    [HttpGet(Name = "GetAll")]
    public async Task<ActionResult<Response<ItemToReturn>>> GetAll()
    {
      var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email));
      if (user == null) return BadRequest("User not found. Please, login and try again with a valid user.");
      var item = await _repo.GetAll(Guid.Parse(user.Id));

      if (!item.Success) return NotFound(item);

      return Ok(item);
    }

    [HttpDelete("{id}", Name = "Delete")]
    public async Task<ActionResult<Response<ItemToReturn>>> Delete(string id)
    {
      var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email));
      if (user == null) return BadRequest("User not found. Please, login and try again with a valid user.");
      var item = await _repo.Delete(Guid.Parse(id));

      if (!item.Success) return NotFound(item);

      return Ok(item);
    }

  }
}