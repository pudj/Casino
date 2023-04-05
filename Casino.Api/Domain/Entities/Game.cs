namespace Casino.Api.Domain.Entities
{
    public class Game
    {
        public int GameId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string GameType { get; set; } = string.Empty;
        public decimal MinimalBet { get; set; }
        public decimal MaximalBet { get; set;  }
        public decimal MaximalGain { get; set; }

    }
}
