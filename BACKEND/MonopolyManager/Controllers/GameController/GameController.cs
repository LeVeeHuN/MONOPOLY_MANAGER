using Microsoft.AspNetCore.Mvc;
using MonopolyManager.Data;
using MonopolyManager.Models;
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

    public Game Create(GameCreationData data)
    {
        throw new NotImplementedException();
    }

    private string GenerateNewGameKey()
    {
        const string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string numbers = "0123456789";
        const string allCharacters = characters + numbers;
        
        string key = string.Empty;
        key += characters[Random.Shared.Next(0, characters.Length)];
        for (int i = 0; i <= 4; i++)
        {
            key += allCharacters[Random.Shared.Next(0, allCharacters.Length)];
        }

        return key;
    }
}