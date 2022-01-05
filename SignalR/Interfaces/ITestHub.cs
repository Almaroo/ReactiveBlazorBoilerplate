using SignalR.Models;

namespace SignalR.Interfaces;

public interface ITestHub
{
    Task UpdateValue(Payload value);
}