using EntityFrameworkAbstraction.Interfaces;
using EntityFrameworkAbstraction.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Example.Controllers;

[Route("api/v1/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUnitOfWork _dbContext;

    public UserController(IUnitOfWork dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> Get()
    {
        return Ok(await _dbContext.UserRepository.ListNoTrackingAsync());
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User>> Get([FromRoute]Guid id)
    {
        
        return Ok(await _dbContext.UserRepository.GetByIdAsync(id));
    }
    [HttpGet("find")]
    public async Task<ActionResult<IEnumerable<User>>> Search([FromQuery]string search)
    {
        var query = _dbContext.UserRepository.GetQueryable();
        
        return Ok(await query.Where(user => user.Name != null && user.Name.Contains(search)).ToListAsync());
    }
    [HttpPost]
    public async Task<ActionResult> Post([FromBody]User user)
    {
        await _dbContext.UserRepository.CreateAsync(user);
        try
        {
            _dbContext.Commit();
        }
        catch (Exception)
        {
            return BadRequest();
        }
        
        return CreatedAtAction(nameof(Get), new{id = user.Id}, user);
    }
    
    [HttpPut("{id:guid}")]
    public ActionResult Put([FromRoute]Guid id, [FromBody]User user)
    {
        if (id != user.Id) return BadRequest();
        _dbContext.UserRepository.Update(user);
        try
        {
            _dbContext.Commit();
        }
        catch (Exception)
        {
            return BadRequest();
        }

        return NoContent();
    }
    [HttpDelete]
    public ActionResult Delete(Guid id)
    {
        _dbContext.UserRepository.Delete(id);
        try
        {
            _dbContext.Commit();
        }
        catch (Exception)
        {
            BadRequest();
        }
        
        return NoContent();
    }
}