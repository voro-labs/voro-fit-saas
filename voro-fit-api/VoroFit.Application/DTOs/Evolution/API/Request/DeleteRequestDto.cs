namespace VoroFit.Application.DTOs.Evolution.API.Request
{
    public class DeleteRequestDto
    {
        public string RemoteJid { get; set; } = string.Empty;
        public bool FromMe { get; set; } = true;
        public string Id { get; set; } = string.Empty;
    }
}
