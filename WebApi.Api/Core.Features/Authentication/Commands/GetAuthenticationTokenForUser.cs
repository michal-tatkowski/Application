using System.IdentityModel.Tokens.Jwt;
using Core.Application.Wrappers;
using System.Security.Claims;
using System.Text;
using Core.Application.Exceptions;
using Core.Application.Interfaces;
using Core.Domain.Models;
using Core.Enums.Errors;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Core.Features.Authentication.Commands;

public sealed record GetAuthenticationTokenForUserQuery() : IRequest<string>
{
    public string Login { get; set; }
    public string Password { get; set; }
}

internal sealed class GetAuthenticationTokenForUserQueryHandler : IRequestHandler<GetAuthenticationTokenForUserQuery, string>
{
    private readonly IMongoDbService context;
    private readonly IPasswordHasher passwordHasher;
    private readonly JwtSettings jwtSettings;
    
    public GetAuthenticationTokenForUserHandler(IMongoDbService<T> context, IOptions<JwtSettings> jwtSettings, IPasswordHasher passwordHasher)
    {
        this.context = context;
        this.passwordHasher = passwordHasher;
        this.jwtSettings = jwtSettings.Value;
    }

    public async Task<string> Handle(GetAuthenticationTokenForUserQuery request, CancellationToken cancellationToken)
    {
        var user = GetUserAsync(request.Login);

        if (user is null)
        {
            throw new ApiException(BasicError.ERR_INVALID_CREDENTIALS);
        }

        var hashedPassword = passwordHasher.Hash(request.Password);

        return GetNewJwtTokenForUserAsync(user);
    }

    private async Task<User> GetUserAsync(string login)
    {
        return new User();
    }

    private string GetNewJwtTokenForUserAsync(User user)
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
        };
    }
}