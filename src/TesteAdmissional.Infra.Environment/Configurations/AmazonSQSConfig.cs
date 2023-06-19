using System.ComponentModel.DataAnnotations;

namespace TesteAdmissional.Infra.Environment.Configurations;

public class AmazonSQSConfig : IConfigurationValidator
{
    [Required] public string AccessKeyId { get; set; }
    [Required] public string SecretAccessKey { get; set; }
    [Required] public string SQSUrl { get; set; }
}