namespace Application.Abstractions.Repositories.AuthenticationService;

public interface ICustomAuthManager : ITranscientService
{
    string Authentication(string username, string apiKey);
}

