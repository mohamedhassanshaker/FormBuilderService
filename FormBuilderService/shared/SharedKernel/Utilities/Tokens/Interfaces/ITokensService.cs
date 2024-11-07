using SharedKernel.Utilities.Tokens.Models;

namespace SharedKernel.Utilities.Tokens.Interfaces
{
    public interface ITokensService
    {
        Task<string> GenerateToken(IEnumerable<TokenClaim> claims);
    }
}