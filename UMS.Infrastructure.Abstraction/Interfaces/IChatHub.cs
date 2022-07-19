using UMS.Infrastructure.Abstraction.Models;

namespace UMS.Infrastructure.Abstraction.Interfaces;

public interface IChatHub
{
    Task SendMessage(string partOfTheAppThatSentNotification, string message);
}