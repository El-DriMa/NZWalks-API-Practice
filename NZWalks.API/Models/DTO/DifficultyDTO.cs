using System.Reflection.Metadata;

namespace NZWalks.API.Models.DTO
{
    public class DifficultyDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }

    }
}
