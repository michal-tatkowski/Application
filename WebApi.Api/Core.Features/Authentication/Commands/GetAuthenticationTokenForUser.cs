using System.IdentityModel.Tokens.Jwt;
using Core.Application.Wrappers;
using System.Security.Claims;
using System.Text;
using Core.Application;
using Core.Application.Exceptions;
using Core.Application.Interfaces;
using Core.Domain.Models;
using Core.Enums.Errors;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Core.Features.Authentication.Commands;

public sealed record GetAuthenticationTokenForUserQuery() : IRequest<string>
{
    public string Login { get; set; }
    public string Password { get; set; }
}

internal sealed class GetAuthenticationTokenForUserQueryHandler : IRequestHandler<GetAuthenticationTokenForUserQuery, string>
{
    private readonly MongoDbContext dbContext;
    private readonly IPasswordHasher passwordHasher;
    private readonly JwtSettings jwtSettings;
    
    public GetAuthenticationTokenForUserHandler(MongoDbContext dbContext, IOptions<JwtSettings> jwtSettings, IPasswordHasher passwordHasher)
    {
        this.dbContext = dbContext;
        this.passwordHasher = passwordHasher;
        this.jwtSettings = jwtSettings.Value;
    }

    public async Task<string> Handle(GetAuthenticationTokenForUserQuery request, CancellationToken cancellationToken)
    {
        var user = await GetUserAsync(request.Login, cancellationToken).ConfigureAwait(false);
        
        var hashedPassword = passwordHasher.Hash(request.Password);

        return GetNewJwtTokenForUserAsync(user);
    }

    private async Task<User> GetUserAsync(string login, CancellationToken cancellationToken)
    {
        return await dbContext.Users.Find(x => x.Login == login).FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false)
            ?? throw new ApiException(UserError.)
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