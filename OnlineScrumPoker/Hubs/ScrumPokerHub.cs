using System;
using System.Transactions;
using Microsoft.AspNetCore.SignalR;
using OnlineScrumPoker.Pages;

namespace OnlineScrumPoker.Hubs;

public class ScrumPokerHub : Hub
{
    private readonly Dictionary<string, int> votes;

    public ScrumPokerHub()
    {
        votes = new();
    }

    public override Task OnConnectedAsync()
    {return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }  

    public void StartNewGame()
    {
        Clients.All.SendAsync("NavigateToGame", Guid.NewGuid());
    }

    public void NewGamerJoined(string gameId, string gamerName)
    {
        Clients.All.SendAsync("InformEveryone", gameId, $"{gamerName} joined the game.");
    }

    public void Vote(string gameId, string gamerName, int vote)
    {
        votes.Add(gamerName, vote);

        Clients.All.SendAsync("InformEveryone", gameId, $"{gamerName} voted the game.");
    }

    public void Reset(string gameId, string gamerName)
    {
        votes.Clear();

        Clients.All.SendAsync("Reset", gameId);
        Clients.All.SendAsync("InformEveryone", gameId, $"{gamerName} reset the game.");
    }

    public void ShowResults(string gameId, string gamerName)
    {
        Clients.All.SendAsync("ShowResults", gameId, votes);
        Clients.All.SendAsync("InformEveryone", gameId, $"{gamerName} showed the results.");
    }
}