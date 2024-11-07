namespace SharedKernel.Utilities.Tokens.Models
{
    public class TokenClaim
    {
        public TokenClaim(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; } = default!;
        public string Value { get; set; } = default!;
    }
}