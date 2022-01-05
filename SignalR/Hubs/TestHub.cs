using Microsoft.AspNetCore.SignalR;
using SignalR.Interfaces;
using SignalR.Models;

namespace SignalR.Hubs;

public class TestHub : Hub<ITestHub>
{
    public async Task UpdateValue(Payload value)
    {
        await Clients.All.UpdateValue(value);
    }
}