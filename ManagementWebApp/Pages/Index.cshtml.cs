using ManagementWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Web;

namespace ManagementWebApp.Pages
{
    [Authorize]
    [AuthorizeForScopes(Scopes = new[] { "https://management.core.windows.net/user_impersonation" })]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly TokenProviderService _tokenProvbiderService;
        private readonly ServiceLimitsService _serviceLimitsService;

        public IndexModel(
            ITokenAcquisition tokenAcquisition,
            HttpClient httpClient,
            ILogger<IndexModel> logger
        ) {;
            _logger = logger;
            _tokenProvbiderService = new TokenProviderService(tokenAcquisition);
            _serviceLimitsService = new ServiceLimitsService(httpClient);
        }

        public async Task OnGet()
        {
            var subscription1 = "SUBSCRIPTION1";
            var subscription2 = "SUBSCRIPTION2";
            var tokenScopes = new List<string> { "https://management.core.windows.net/user_impersonation" };
            var azureTokenScopes = new List<string> { "https://management.core.windows.net/.default" };
            var userToken = await _tokenProvbiderService.GetUserAccessToken(tokenScopes);
            var azureToken = await _tokenProvbiderService.GetAzureCredentialAccessToken(azureTokenScopes);

            await _serviceLimitsService.GetServiceLimitsAsync(userToken, subscription1);
            await _serviceLimitsService.GetServiceLimitsAsync(azureToken, subscription2);
        }
    }
}