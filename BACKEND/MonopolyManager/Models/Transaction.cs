using MonopolyManager.Enums;

namespace MonopolyManager.Models;

public class Transaction
{
    public string Id { get; set; }
    public string? From { get; set; }
    public string? To { get; set; }
    public int Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Time { get; set; }
    public TransactionResult Result { get; set; }
}