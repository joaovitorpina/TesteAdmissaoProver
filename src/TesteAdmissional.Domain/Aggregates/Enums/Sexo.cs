using System.Runtime.Serialization;

namespace TesteAdmissional.Domain.Aggregates.Enums;

public enum Sexo
{
    [EnumMember(Value = "M")]Masculino,
    [EnumMember(Value = "F")]Feminino
}