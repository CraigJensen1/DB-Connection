using Back_EndAPI.Entities;
using ClassLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class CharactersController
    : CrudController<Character, CharacterDTO>
{
    private readonly CharacterService _characterService;

    public CharactersController(CharacterService service)
        : base(service)
    {
        _characterService = service;
    }

    protected override CharacterDTO MapToDto(Character entity)
        => new CharacterDTO
        {
            Id = entity.HeroId,
            Name = entity.Name,
            Class = entity.Class,
            Level = entity.Level,
            Health = entity.Health,
            Mana = entity.Mana
        };

    protected override Character MapToEntity(CharacterDTO dto)
        => new Character
        {
            HeroId = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
            Name = dto.Name,
            Class = dto.Class,
            Level = dto.Level,
            Health = dto.Health,
            Mana = dto.Mana,
            CreatedAt = DateTime.UtcNow
        };

    [HttpPost("{id}/levelup")]
    public async Task<IActionResult> LevelUp(Guid id)
    {
        //
        await _characterService.LevelUpAsync(id);
        return NoContent();
    }


    [HttpGet]
    public override async Task<ActionResult<List<CharacterDTO>>> Get()
    {
        // This overrides the base Get() method to return DTOs more efficiently.
        var dtos = await _characterService.GetAllDtosAsync();
        return Ok(dtos);
    }
}








//using ClassLibrary.DTOs;
//using Microsoft.AspNetCore.Mvc;

////
//// CONTROLLER ROLE
//// ----------------
//// Controllers are the "front door" of your backend.
//// They receive HTTP requests, call services to do the work,
//// and translate results into HTTP responses.
////
//// Controllers should be THIN:
//// - No database logic
//// - No business rules
//// - No data transformation logic
////

//[ApiController] // Enables automatic model validation & API behavior
//[Route("api/characters")] // Base route for this controller
//public class CharacterController : ControllerBase
//{
//    // Service that contains the business/data logic
//    private readonly CharacterService _characterService;

//    // Constructor Injection:
//    // ASP.NET gives us CharacterService automatically via Dependency Injection
//    public CharacterController(CharacterService characterService)
//    {
//        _characterService = characterService;
//    }

//    // GET: api/characters
//    // Returns a list of characters to the client
//    [HttpGet]
//    public async Task<ActionResult<List<CharacterDTO>>> GetCharacters()
//    {
//        // Ask the service for character data
//        var characters = await _characterService.GetCharactersAsync();

//        // Return HTTP 200 OK with JSON data
//        return Ok(characters);
//    }
//}
