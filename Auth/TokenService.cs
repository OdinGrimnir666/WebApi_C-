public class TokenServie : ITokenService
{
    private TimeSpan ExpiryDuration = new TimeSpan(0, 30 , 0);
    public string BuildToken(string key, string issuer, UserDto user)
    {
        var claims = new []
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
        };

        var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securitykey, 
            SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
        expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}