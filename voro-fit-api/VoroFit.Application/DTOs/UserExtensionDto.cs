using VoroFit.Application.DTOs.Evolution;
using VoroFit.Application.DTOs.Identity;

namespace VoroFit.Application.DTOs
{
    public class UserExtensionDto
    {
        public Guid? UserId { get; set; }
        public UserDto? User { get; set; } = null!;

        public ContactDto? Contact { get; set; }
        public StudentDto? Student { get; set; }
        public ICollection<InstanceDto>? Instances { get; set; } = [];
        public ICollection<ExerciseDto> Exercises { get; set; } = [];
    }
}
