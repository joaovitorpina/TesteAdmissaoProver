using Microsoft.Extensions.Caching.Memory;

namespace TesteAdmissional.Api.Authentication;

public class TesteAdmissionalAuthKeyKeeper : ITesteAdmissionalAuthKeyKeeper
{
    private readonly IMemoryCache _cache;

    public TesteAdmissionalAuthKeyKeeper(IMemoryCache memoryCache)
    {
        _cache = memoryCache;
    }

    public string CreateNewToken()
    {
        var token = GenerateRandomToken(32);

        _cache.Set(token, true, TimeSpan.FromHours(4));

        return token;
    }

    public string CreateTokenPasswordRecovery()
    {
        var token = GenerateRandomToken(16);

        _cache.Set(token, true, TimeSpan.FromMinutes(10));

        return token;
    }

    public bool CheckTokenIsValid(string token)
    {
        var valid = _cache.TryGetValue(token, out _);

        return valid;
    }

    private string GenerateRandomToken(int lenght)
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[lenght];
        var random = new Random();

        for (var i = 0; i < stringChars.Length; i++) stringChars[i] = chars[random.Next(chars.Length)];

        var key = new string(stringChars);

        return key;
    }
}