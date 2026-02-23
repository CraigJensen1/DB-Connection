using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CrudController<TEntity, TDto> : ControllerBase
    where TEntity : class
{
    protected readonly CrudService<TEntity> _service;

    public CrudController(CrudService<TEntity> service)
    {
        _service = service;
    }

    protected virtual TDto MapToDto(TEntity entity)
        => throw new NotImplementedException();

    protected virtual TEntity MapToEntity(TDto dto)
        => throw new NotImplementedException();

    [HttpGet]
    public virtual async Task<ActionResult<List<TDto>>> Get()
    {
        // Get all database columns for the entity with the specified id
        //   Then map the entity to a DTO and return it in the response
        // Fine for small tables, but for larger ones consider overriding
        //   this method to only get a subset of columns that the dto needs
        // SELECT * FROM character  -- compared to:
        //   SELECT hero_id, name, class, level, health, mana FROM character
        var entities = await _service.GetAllAsync();
        return Ok(entities.Select(MapToDto));
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TDto>> Get(Guid id)
    {
        var entity = await _service.GetByIdAsync(id);
        if (entity == null) return NotFound();

        return Ok(MapToDto(entity));
    }

    [HttpPost]
    public virtual async Task<ActionResult<TDto>> Create(TDto dto)
    {
        var entity = MapToEntity(dto);
        var created = await _service.CreateAsync(entity);

        return Ok(MapToDto(created));
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}
