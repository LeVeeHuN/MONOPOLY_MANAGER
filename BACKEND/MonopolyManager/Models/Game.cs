namespace MonopolyManager.Models;

public class Game
{
    public string Key { get; set; } // primary key
    public List<User> Players { get; set; }
    public List<Transaction> Transactions { get; set; }
    public User Owner { get; set; }
    public int StartMoney { get; set; }
    public int StartTileMoney { get; set; }
}