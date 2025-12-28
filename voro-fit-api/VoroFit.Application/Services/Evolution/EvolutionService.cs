using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using VoroFit.Application.DTOs.Evolution.API.Request;
using VoroFit.Application.DTOs.Evolution.API.Response;
using VoroFit.Application.Services.Interfaces.Evolution;
using VoroFit.Shared.Extensions;
using VoroFit.Shared.Utils;

namespace VoroFit.Application.Services.Evolution
{
    public class EvolutionService : IEvolutionService
    {
        private readonly HttpClient _httpClient;
        private readonly EvolutionUtil _evolutionUtil;

        private string InstanceName = string.Empty;

        public EvolutionService(IHttpClientFactory httpClientFactory,
            IOptions<EvolutionUtil> evolutionUtil)
        {
            _evolutionUtil = evolutionUtil.Value;

            _httpClient = httpClientFactory.CreateClient(nameof(EvolutionService));
            _httpClient.BaseAddress = new Uri(_evolutionUtil.BaseUrl);
            _httpClient.DefaultRequestHeaders.Add("apikey", _evolutionUtil.Key);
        }

        public async Task SetInstanceName(string instanceName)
        {
            this.InstanceName = instanceName;
        }

        public async Task<IEnumerable<InstanceResponseDto>> GetAllInstancesAsync()
        {
            var url = $"/instance/fetchInstances";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<InstanceResponseDto>>(responseContent) ?? [];
        }

        public async Task<InstanceCreateResponseDto> CreateInstanceAsync(InstanceRequestDto request)
        {
            var url = $"/instance/create";
            var payload = request;
            var jsonPayload = JsonSerializer.Serialize(payload, new JsonSerializerOptions().AsDefault());
            var response = await _httpClient.PostAsync(url, new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<InstanceCreateResponseDto>(responseContent) ?? new();
        }
        public async Task DeleteInstanceAsync()
        {
            var url = $"/instance/delete/{this.InstanceName}";
            var response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }
        public async Task<QrCodeJsonDto> RefreshQrCodeAsync()
        {
            var url = $"/instance/connect/{this.InstanceName}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<QrCodeJsonDto>(responseContent) ?? new();
        }
        public async Task<InstanceCreateResponseDto> GetInstanceStatusAsync()
        {
            var url = $"/instance/connectionState/{this.InstanceName}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<InstanceCreateResponseDto>(responseContent) ?? new();
        }

        

        public async Task<IEnumerable<ContactResponseDto>> GetContactsAsync()
        {
            var url = $"/chat/findContacts/{this.InstanceName}";
            var response = await _httpClient.PostAsync(url, null);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<ContactResponseDto>>(responseContent) ?? [];
        }
        public async Task<GroupResponseDto?> GetGroupAsync(string groupJId)
        {
            var url = $"/group/findGroupInfos/{this.InstanceName}?groupJid={groupJId}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GroupResponseDto>(responseContent) ?? null;
        }

        public async Task<IEnumerable<GroupResponseDto>> GetGroupsAsync()
        {
            var url = $"/group/fetchAllGroups/{this.InstanceName}?getParticipants=false";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<GroupResponseDto>>(responseContent) ?? [];
        }

        public async Task<string> SendMessageAsync(MessageRequestDto request)
        {
            var url = $"/message/sendText/{this.InstanceName}";
            var payload = request;
            var jsonPayload = JsonSerializer.Serialize(payload, new JsonSerializerOptions().AsDefault());
            var response = await _httpClient.PostAsync(url, new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> SendQuotedMessageAsync(MessageRequestDto request)
        {
            var url = $"/message/sendText/{this.InstanceName}";
            var payload = request;
            var jsonPayload = JsonSerializer.Serialize(payload, new JsonSerializerOptions().AsDefault());
            var response = await _httpClient.PostAsync(url, new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> SendMediaMessageAsync(MediaRequestDto request)
        {
            var url = $"/message/sendMedia/{this.InstanceName}";
            var payload = request;
            var jsonPayload = JsonSerializer.Serialize(payload, new JsonSerializerOptions().AsDefault());
            var response = await _httpClient.PostAsync(url, new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> SendLocationMessageAsync(LocationRequestDto request)
        {
            var url = $"/message/sendLocation/{this.InstanceName}";
            var payload = request;
            var jsonPayload = JsonSerializer.Serialize(payload, new JsonSerializerOptions().AsDefault());
            var response = await _httpClient.PostAsync(url, new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> SendContactMessageAsync(ContactRequestDto request)
        {
            var url = $"/message/sendContact/{this.InstanceName}";
            var payload = request;
            var jsonPayload = JsonSerializer.Serialize(payload, new JsonSerializerOptions().AsDefault());
            var response = await _httpClient.PostAsync(url, new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> SendReactionMessageAsync(ReactionRequestDto request)
        {
            var url = $"/message/sendReaction/{this.InstanceName}";
            var payload = request;
            var jsonPayload = JsonSerializer.Serialize(payload, new JsonSerializerOptions().AsDefault());
            var response = await _httpClient.PostAsync(url, new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteMessageAsync(DeleteRequestDto request)
        {
            var url = $"/chat/deleteMessageForEveryone/{this.InstanceName}";
            var jsonPayload = JsonSerializer.Serialize(
                request,
                new JsonSerializerOptions().AsDefault()
            );
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = new StringContent(
                    jsonPayload,
                    Encoding.UTF8,
                    "application/json"
                )
            };
            var response = await _httpClient.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
