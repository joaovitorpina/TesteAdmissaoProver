namespace TesteAdmissional.Application.Ports;

public interface ISqsFacade
{
    public Task SendMessage(string body);
}