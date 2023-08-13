using System;
using Microsoft.AspNetCore.SignalR;

namespace OnlineScrumPoker.Hubs;

public class ScrumPokerHub : Hub
{
    public override Task OnConnectedAsync()
    {
        Clients.All.SendAsync("ReceiveMessage", "system", $"{Context.ConnectionId} joined the online scrum poker.");

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        Clients.All.SendAsync("ReceiveMessage", "system", $"{Context.ConnectionId} left the online scrum poker.");

        return base.OnDisconnectedAsync(exception);
    }

    public void SendMessage(string name, string message)
    {
        Clients.All.SendAsync("ReceiveMessage", name, message);
    }
}