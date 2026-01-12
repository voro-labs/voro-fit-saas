namespace VoroFit.Domain.Entities.Evolution
{
    public class Instance
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;

        public Guid UserExtensionId { get; set; }
        public UserExtension UserExtension { get; set; } = null!;
        
        public InstanceExtension? InstanceExtension { get; set; }
    }
}
