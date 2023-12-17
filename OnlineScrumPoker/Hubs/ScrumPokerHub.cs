using System;
using Microsoft.AspNetCore.SignalR;

namespace OnlineScrumPoker.Hubs;

public class ScrumPokerHub : Hub
{
    public override Task OnConnectedAsync()
    {
        //Clients.All.SendAsync("ReceiveMessage", "system", $"{Context.ConnectionId} joined the online scrum poker.");

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        //Clients.All.SendAsync("ReceiveMessage", "system", $"{Context.ConnectionId} left the online scrum poker.");

        return base.OnDisconnectedAsync(exception);
    }

    public void StartNewGame()
    {
        Clients.All.SendAsync("NavigateToGame", Guid.NewGuid());
    }

    public void Vote(string gameId, int vote)
    {
        Clients.All.SendAsync("UpdateVotes", gameId, vote);
    }
}