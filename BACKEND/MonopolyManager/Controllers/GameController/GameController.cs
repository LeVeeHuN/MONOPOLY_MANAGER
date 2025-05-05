using Microsoft.AspNetCore.Mvc;
using MonopolyManager.Data;
using MonopolyManager.Models;
using MonopolyManager.Models.OutgoingData;
using MonopolyManager.Models.IncomingData;

namespace MonopolyManager.Controllers.GameController;

[Route("[controller]")]
[ApiController]
public sealed class GameController
{
    private IGameRepository _repo;

    public GameController(IGameRepository repo)
    {
        _repo = repo;
    }

    [HttpPost]
    public GameCreationDataOut Create([FromBody] GameCreationDataIn dataIn)
    {
        Game newGame = new Game();
        newGame.Key = GenerateNewGameKey();
        newGame.ViewKey = GenerateNewGameKey();
        newGame.StartMoney = dataIn.StartMoney;
        newGame.StartTileMoney = dataIn.StartTileMoney;
        newGame.Transactions = new List<Transaction>();

        foreach (var player in dataIn.Players)
        {
            newGame.Players.Add(new Player(){Money = dataIn.StartMoney, Name = player});
        }
        
        _repo.Create(newGame);
        GameCreationDataOut dataOut = new GameCreationDataOut() {Key = newGame.Key, ViewKey = newGame.ViewKey};
        return dataOut;
    }

    [HttpGet("{key}")]
    public Game? Get(string key)
    {
        return _repo.Read(key);
    }

    private string GenerateNewGameKey()
    {
        // Look at that! The key looks like a neptun code :)
        
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string numbers = "0123456789";
        const string allCharacters = characters + numbers;
        
        string key = string.Empty;
        key += characters[Random.Shared.Next(0, characters.Length)];
        for (int i = 0; i <= 4; i++)
        {
            key += allCharacters[Random.Shared.Next(0, allCharacters.Length)];
        }

        // Check if key is used
        if (_repo.KeyExists(key))
        {
            GenerateNewGameKey();
        }

        return key;
    }
}