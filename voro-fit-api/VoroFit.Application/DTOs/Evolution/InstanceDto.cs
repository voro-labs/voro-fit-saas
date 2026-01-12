using System.Text.Json.Serialization;

namespace VoroFit.Application.DTOs.Evolution
{
    public class InstanceDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }

        public Guid? UserExtensionId { get; set; }
        public UserExtensionDto? UserExtension { get; set; }
        public InstanceExtensionDto? InstanceExtension { get; set; }
    }
}
