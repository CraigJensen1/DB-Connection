using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.DTOs
{
    //
    // DTO ROLE
    // ---------
    // DTOs define the SHAPE of data sent to or received from clients.
    //
    // DTOs:
    // - Do NOT reference EF Core
    // - Do NOT match the database exactly
    // - Protect internal fields from being exposed(easier to hack if they are known)
    //
    // Think of DTOs as your API contract.
    //
    public class CharacterDTO
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = "";

        [Required]
        public string Class { get; set; } = "";

        [Range(1, 50)]
        public int Level { get; set; }

        [Range(1, int.MaxValue)]
        public int Health { get; set; }

        [Range(0, int.MaxValue)]
        public int Mana { get; set; }
    }
}
