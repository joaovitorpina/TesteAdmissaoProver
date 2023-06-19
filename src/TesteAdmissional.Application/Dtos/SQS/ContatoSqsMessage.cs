using TesteAdmissional.Application.Dtos.SQS.Enum;

namespace TesteAdmissional.Application.Dtos.SQS;

public record ContatoSqsMessage(MessageType MessageType, string Body);