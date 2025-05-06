using MonopolyManager.Models;

namespace MonopolyManager.Data;

public class GameRepository : IGameRepository
{
    private List<Game> _repo;

    public GameRepository()
    {
        _repo = new List<Game>();
    }


    public void Create(Game game)
    {
        _repo.Add(game);
    }

    public IEnumerable<Game> Read()
    {
        return _repo;
    }

    public Game? Read(string key)
    {
        Game? game = _repo.FirstOrDefault(g => g.Key == key);
        if (game == null)
        {
            game = _repo.FirstOrDefault(g => g.ViewKey == key);
        }

        return game;
    }

    public void Update(Game game)
    {
        Game? gameToUpdate = _repo.FirstOrDefault(g => g.Key == game.Key);
        if (gameToUpdate == null)
        {
            return;
        }
        gameToUpdate.Players = game.Players;
        gameToUpdate.Transactions = game.Transactions;
        gameToUpdate.StartMoney = game.StartMoney;
        gameToUpdate.StartTileMoney = game.StartTileMoney;
    }

    public void Delete(string key)
    {
        Game? gameToDelete = _repo.FirstOrDefault(g => g.Key == key);
        if (gameToDelete == null)
        {
            return;
        }
        _repo.Remove(gameToDelete);
    }

    public bool KeyExists(string key)
    {
        foreach (Game game in _repo)
        {
            if (game.Key == key || game.ViewKey == key)
            {
                return true;
            }
        }
        return false;
    }
}