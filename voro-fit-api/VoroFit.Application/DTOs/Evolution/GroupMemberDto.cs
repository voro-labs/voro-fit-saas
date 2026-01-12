using System.Text.Json.Serialization;

namespace VoroFit.Application.DTOs.Evolution
{
    public class GroupMemberDto
    {
        public Guid? Id { get; set; }
        public Guid? GroupId { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public GroupDto? Group { get; set; }

        public Guid? ContactId { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public ContactDto? Contact { get; set; }

        public DateTimeOffset? JoinedAt { get; set; }
    }
}