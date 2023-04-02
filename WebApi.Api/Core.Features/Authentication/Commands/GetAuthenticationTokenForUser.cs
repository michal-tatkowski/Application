using Core.Application.Wrappers;
using System.Security.Claims;
using System.Text;
using Core.Application.Interfaces;
using MediatR;

namespace Core.Features.Authentication.Commands;

public sealed record GetAuthenticationTokenForUser() : IRequest<string> { }

internal sealed class GetAuthenticationTokenForUserHandler : IRequestHandler<GetAuthenticationTokenForUser, string>
{
    private readonly IMongoDbContext<T> context;
    private readonly IPasswordHasher passwordHasher;
    private readonly JwtSettings jwtSettings;
    
    public GetAuthenticationTokenForUserHandler(IMongoDbContext<T> context, IOptions<JwtSettings> jwtSettings, IPasswordHasher passwordHasher)
    {
        this.context = context;
        this.passwordHasher = passwordHasher;
        this.jwtSettings = jwtSettings.Value;
    }

    public async Task<string> Handle(GetAuthenticationTokenForUser request, CancellationToken cancellationToken)
    {
        var user = 1;

        if (user is null)
        {
            throw new ApiException(BasicError.ERR - INVALID_CREDENTIALS);
        }

        var hashedPassword = passwordHasher.Hash(request.Password);

        return GetNewJwtTokenForUserAsync(user);
    }

    private string GetNewJwtTokenForUserAsync(user)
    {
        var claimsUnioned = CreateJwtClaims(user);
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(jwtSettings.Issuer, jwtSettings.Audience, claimsUnioned,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    private List<Claim> CreateJwtClaims(User user)
    {
        return new List<Claim>
        {
            new("uid", user.Id.ToString()),
            new(JwtRegisteredClaimNames.Sub, user.FirstName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("phone", user.Phone),
        };
    }
}