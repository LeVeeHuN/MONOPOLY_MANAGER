namespace MonopolyManager.Models.IncomingData
{
    public class GameCreationData
    {
        public int StartMoney { get; set; }
        public int StartTileMoney {  get; set; }
        public string Owner { get; set; }
        public List<string> Players { get; set; }
    }
}
