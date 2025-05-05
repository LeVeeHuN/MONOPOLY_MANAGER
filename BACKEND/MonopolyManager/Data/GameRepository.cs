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

    public Game? Read(int id)
    {
        return _repo.FirstOrDefault(g => g.Id == id);
    }

    public void Update(Game game)
    {
        Game? gameToUpdate = _repo.FirstOrDefault(g => g.Id == game.Id);
        if (gameToUpdate == null)
        {
            return;
        }
        gameToUpdate.Owner = game.Owner;
        gameToUpdate.Players = game.Players;
        gameToUpdate.Transactions = game.Transactions;
        gameToUpdate.AccessToken = game.AccessToken;
    }

    public void Delete(int id)
    {
        Game? gameToDelete = _repo.FirstOrDefault(g => g.Id == id);
        if (gameToDelete == null)
        {
            return;
        }
        _repo.Remove(gameToDelete);
    }
}