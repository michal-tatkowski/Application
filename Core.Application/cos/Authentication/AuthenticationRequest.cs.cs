namespace Core.Application.cos.Authentication
{
    public record AuthenticationRequest
    {
        public string Login { get; init; }
        public string Password { get; init; }
    }
}
