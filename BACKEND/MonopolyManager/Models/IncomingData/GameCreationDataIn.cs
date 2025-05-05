namespace MonopolyManager.Models.IncomingData
{
    public class GameCreationDataIn
    {
        public int StartMoney { get; set; }
        public int StartTileMoney {  get; set; }
        public List<string> Players { get; set; }
    }
}
