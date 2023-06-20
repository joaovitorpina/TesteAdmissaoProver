namespace TesteAdmissional.Api.Authentication;

public interface ITesteAdmissionalAuthKeyKeeper
{
    public string CreateNewToken();
    public string CreateTokenPasswordRecovery();
    public bool CheckTokenIsValid(string token);
}