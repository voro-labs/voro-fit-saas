using System.Text.Json.Serialization;

namespace VoroFit.Application.DTOs.Evolution
{
    public class GroupDto
    {
        public Guid? Id { get; set; }
        public string? RemoteJid { get; set; }

        public string? Name { get; set; }
        public string? ProfilePictureUrl { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public ICollection<GroupMemberDto>? Members { get; set; }
    }
}