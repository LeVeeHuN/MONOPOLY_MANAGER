namespace MonopolyManager.Models;

public class Game
{
    public int Id { get; set; }
    public string AccessToken { get; set; }
    public List<User> Players { get; set; }
    public List<Transaction> Transactions { get; set; }
    public User Owner { get; set; }
}