using ECommerceAPI.Base;
using ECommerceAPI.Schema.DataSets.Token;

namespace ECommerceAPI.Business.Services.Token
{
    public interface ITokenService
    {
        ApiResponse<TokenResponse> GetToken(TokenRequest request);

    }
}
