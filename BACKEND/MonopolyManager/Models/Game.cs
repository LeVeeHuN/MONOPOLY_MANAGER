namespace MonopolyManager.Models;

public class Game
{
    public string Key { get; set; } // primary key
    public string ViewKey { get; set; }
    public List<Player> Players { get; set; }
    public List<Transaction> Transactions { get; set; }
    public int StartMoney { get; set; }
    public int StartTileMoney { get; set; }
}