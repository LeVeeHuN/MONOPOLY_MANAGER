using MonopolyManager.Models;

namespace MonopolyManager.Data;

public interface IGameRepository
{
    void Create(Game game);
    
    IEnumerable<Game> Read();
    Game? Read(string key);
    
    void Update(Game game);
    
    void Delete(string key);
}