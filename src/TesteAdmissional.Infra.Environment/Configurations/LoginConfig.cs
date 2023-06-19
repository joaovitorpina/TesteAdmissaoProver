namespace TesteAdmissional.Infra.Environment.Configurations;

public class LoginConfig : IConfigurationValidator
{
    public string User { get; set; }
    public string Password { get; set; }
}