using Microsoft.AspNetCore.Mvc;
using MonopolyManager.Data;
using MonopolyManager.Enums;
using MonopolyManager.Models.IncomingData;
using MonopolyManager.Models;

namespace MonopolyManager.Controllers.TransactionController;

[Route("[controller]")]
[ApiController]
public class TransactionController
{
    private IGameRepository _repo;

    public TransactionController(IGameRepository repo)
    {
        _repo = repo;
    }
    
    [HttpPost]
    public void Create([FromBody] TransactionCreate transactionData)
    {
        Game? game = _repo.Read(transactionData.GameKey);
        if (game == null)
        {
            return;
        }

        // In case that the given key is only a view key
        if (game.Key != transactionData.GameKey)
        {
            return;
        }
        
        Transaction transaction = new Transaction();
        transaction.Id = Guid.NewGuid().ToString();
        transaction.Amount = transactionData.Amount;
        transaction.Type = transactionData.Type;
        transaction.Time = DateTime.Now;
        transaction.To = transactionData.To;
        transaction.From = transactionData.From;

        switch (transactionData.Type)
        {
            case TransactionType.Transfer:
                // Might refactor this code later
                foreach (var playerFrom in game.Players)
                {
                    if (playerFrom.Name.Equals(transactionData.From))
                    {
                        if (playerFrom.Money >= transactionData.Amount)
                        {
                            foreach (var playerTo in game.Players)
                            {
                                if (playerTo.Name.Equals(transactionData.To))
                                {
                                    playerFrom.Money -= transactionData.Amount;
                                    playerTo.Money += transactionData.Amount;
                                    transaction.Result = TransactionResult.Successful;
                                }
                            }
                        }
                        else
                        {
                            transaction.Result = TransactionResult.Failed;
                        }
                    }
                }
                break;
            case TransactionType.Deposit:
                // Player gets money
                foreach (var player in game.Players)
                {
                    if (player.Name.Equals(transactionData.To))
                    {
                        player.Money += transactionData.Amount;
                        transaction.Result = TransactionResult.Successful;
                    }
                }
                break;
            case TransactionType.Withdraw:
                // Player loses money
                foreach (var player in game.Players)
                {
                    if (player.Name.Equals(transactionData.From))
                    {
                        if (player.Money >= transactionData.Amount)
                        {
                            player.Money -= transactionData.Amount;
                            transaction.Result = TransactionResult.Successful;
                        }
                        else
                        {
                            transaction.Result = TransactionResult.Failed;
                        }
                    }
                }
                break;
            case TransactionType.Start:
                foreach (var player in game.Players)
                {
                    if (player.Name.Equals(transactionData.From))
                    {
                        player.Money += game.StartTileMoney;
                        transaction.Result = TransactionResult.Successful;
                    }
                }
                break;
        }
        game.Transactions.Add(transaction);
    }
}