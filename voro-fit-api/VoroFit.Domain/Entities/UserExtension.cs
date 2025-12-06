using System.ComponentModel.DataAnnotations;
using VoroFit.Domain.Entities.Evolution;
using VoroFit.Domain.Entities.Identity;

namespace VoroFit.Domain.Entities
{
    public class UserExtension
    {
        [Key]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Student? Student { get; set; }
        public ICollection<Instance> Instances { get; set; } = [];
    }
}
