using MonopolyManager.Enums;

namespace MonopolyManager.Models.IncomingData;

public class TransactionCreate
{
    public string GameKey { get; set; }
    public TransactionType Type { get; set; }
    public int Amount { get; set; }
    public string? From { get; set; }
    public string? To { get; set; }
}