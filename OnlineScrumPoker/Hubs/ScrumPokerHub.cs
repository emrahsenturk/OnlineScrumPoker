using System;
using System.Transactions;
using Microsoft.AspNetCore.SignalR;
using OnlineScrumPoker.Pages;

namespace OnlineScrumPoker.Hubs;

public class ScrumPokerHub : Hub
{
    public ScrumPokerHub()
    {
    }

    public override Task OnConnectedAsync()
    {return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }  

    public async Task NewGamerJoined(string gameId, string gamerName, string connectionId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
        await Clients.Group(gameId).SendAsync("AddNewGamerVoteToEveryone", gameId, gamerName, connectionId);
    }

    public async Task SendCurrentVotes(string gameId, string connectionId, Dictionary<string, int> votes)
    {
        await Clients.Group(gameId).SendAsync("UpdateVotesForNewGamer", gameId, votes);
    }

    public async Task SendCurrentTransactions(string gameId, string connectionId, List<string> transactions)
    {
        await Clients.Group(gameId).SendAsync("UpdateTransactionsForNewGamer", gameId, transactions);
    }

    public void SendVotesToEveryone(string gameId, Dictionary<string, int> votes)
    {
        Clients.Group(gameId).SendAsync("UpdateVotes", gameId, votes);
    }

    public void Vote(string gameId, string gamerName, int vote, Dictionary<string, int> votes)
    {
        Clients.Group(gameId).SendAsync("UpdateVotes", gameId, votes);
        Clients.Group(gameId).SendAsync("InformEveryone", gameId, $"{gamerName} voted the game.");
    }

    public void Reset(string gameId, string gamerName)
    {
        Clients.Group(gameId).SendAsync("Reset", gameId);
        Clients.Group(gameId).SendAsync("InformEveryone", gameId, $"{gamerName} reset the game.");
    }

    public void ShowResults(string gameId, string gamerName)
    {
        Clients.Group(gameId).SendAsync("ShowResults", gameId);
        Clients.Group(gameId).SendAsync("InformEveryone", gameId, $"{gamerName} showed the results.");
    }
}