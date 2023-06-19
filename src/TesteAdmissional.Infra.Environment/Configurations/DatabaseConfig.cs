using System.ComponentModel.DataAnnotations;

namespace TesteAdmissional.Infra.Environment.Configurations;

public class DatabaseConfig : IConfigurationValidator
{
    [Required] public string User { get; set; }
    [Required] public string Password { get; set; }
}