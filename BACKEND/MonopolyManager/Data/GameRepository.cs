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
        throw new NotImplementedException();
    }

    public IEnumerable<Game> Read()
    {
        return _repo;
    }

    public Game? Read(string key)
    {
        return _repo.FirstOrDefault(g => g.Key == key);
    }

    public void Update(Game game)
    {
        Game? gameToUpdate = _repo.FirstOrDefault(g => g.Key == game.Key);
        if (gameToUpdate == null)
        {
            return;
        }
        gameToUpdate.Owner = game.Owner;
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
}