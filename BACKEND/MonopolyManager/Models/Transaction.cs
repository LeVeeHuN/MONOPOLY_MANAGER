using MonopolyManager.Enums;

namespace MonopolyManager.Models;

public class Transaction
{
    public int Id { get; set; }
    public User? From { get; set; }
    public User? To { get; set; }
    public int Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Time { get; set; }
}