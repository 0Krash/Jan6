using Jan6.Models;
using Jan6.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jan6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserDbContext context;
    private readonly ILogger logger;

    public UsersController(UserDbContext context, ILogger logger)
    {
        this.logger = logger;
        this.context = context;
    }

    [HttpGet("/hello")]
    public IActionResult GetUserById()
    {
        logger.LogInformation("Saying hello to the client");
        return Ok("Hello from the container");
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserModel>> GetUserById([FromRoute] int id)
    {
        logger.LogInformation($"{DateTime.Now} | GetUserById...");
        var existUser = await context.Users.AnyAsync(x => x.Id == id);
        if (!existUser) return NotFound($"User with Id: {id} does not exis! ❌");

        return await context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    [HttpGet]
    public async Task<ActionResult<List<UserModel>>> GetUserList()
    {
        return await context.Users.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult> PostUser([FromBody] UserModel user)
    {
        var existUser = await context.Users.AnyAsync(x => x.Name == user.Name);
        if (existUser) return BadRequest($"User with Name {user.Name} already exis! ❌");

        context.Add(user);
        await context.SaveChangesAsync();
        return Ok($"User {user.Name} created successfully! ✅");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> PutUserById([FromRoute] int id, [FromBody] UserModel user)
    {
        if (id != user.Id) return BadRequest("User Id doesn't match! ❌");

        var existUser = await context.Users.AnyAsync(x => x.Id == id);
        if (!existUser) return NotFound($"User with Id: {id} does not exis! ❌");

        context.Update(user);
        await context.SaveChangesAsync();
        return Ok($"User with Id {user.Id} updated successfully! ✅");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUserById([FromRoute] int id)
    {
        var existUser = await context.Users.AnyAsync(x => x.Id == id);
        if (!existUser) return NotFound($"User with Id: {id} does not exis! ❌");

        context.Remove(new UserModel() { Id = id });
        await context.SaveChangesAsync();
        return NoContent();
    }
}