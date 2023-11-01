using API.Data;
using API.Dto;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UsersController(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> Get()
    {
        return Ok(await _context.Users.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> Get(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<int>> AddUser([FromBody] CreateUserDto user)
    {
        var appUser = _mapper.Map<AppUser>(user);
        _context.Users.Add(appUser);
        await _context.SaveChangesAsync();
        return Ok(appUser.Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser([FromRoute] int id, [FromBody] CreateUserDto user)
    {
        var existingUser = _context.Users.Find(id);

        if (existingUser != null)
        {
            _mapper.Map(user, existingUser);
            await _context.SaveChangesAsync();
            return Ok();
        }

        return NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        return NotFound();
    }

}
