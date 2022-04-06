using Azure.Core;
using Azure.Identity;
using Microsoft.Identity.Web;

namespace ManagementWebApp.Services
{
    public class TokenProviderService
    {
        private readonly ITokenAcquisition _tokenAcquisition;

        public TokenProviderService(ITokenAcquisition tokenAcquisition)
        {
            _tokenAcquisition = tokenAcquisition;
        }

        public async Task<string> GetUserAccessToken(List<string> scopes)
        {
            return await _tokenAcquisition.GetAccessTokenForUserAsync(scopes);
        }

        public async Task<string> GetAzureCredentialAccessToken(List<string> scopes)
        {
            var tokenRequestContext = new TokenRequestContext(scopes.ToArray());
            return (await new DefaultAzureCredential().GetTokenAsync(tokenRequestContext, CancellationToken.None)).Token;
        }
    }
}
