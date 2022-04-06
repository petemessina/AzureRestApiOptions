using ManagementWebApp.Models;
using Microsoft.Identity.Client;
using System.Text.Json;

namespace ManagementWebApp.Services
{
    public class ServiceLimitsService
    {
        private readonly string _managementUrl = "https://management.azure.com/subscriptions/";
        private readonly string _providersPath = "/providers/Microsoft.Capacity/resourceProviders/Microsoft.Compute/locations/eastus/serviceLimits?api-version=2020-10-25";
        private readonly HttpClient _httpClient;

        public ServiceLimitsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ArmResult> GetServiceLimitsAsync(
            string accessToken,
            string subscriptionId
        ) {
            string quotaUrl = $"{_managementUrl}{subscriptionId}{_providersPath}";

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            var httpResult = await _httpClient.GetAsync(quotaUrl);
            string json = await httpResult.Content.ReadAsStringAsync();
            ArmResult armTenants = JsonSerializer.Deserialize<ArmResult>(json);

            return armTenants;
        }
    }
}
