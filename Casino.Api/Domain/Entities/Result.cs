namespace Casino.Api.Domain.Entities
{
    public class Result
    {
        public readonly string WIN = "Win";
        public readonly string LOSE = "Lose";
        public int ResultId { get; set; }
        public decimal Amount { get; set; }
        public string ResultType { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Result() { }
        public Result(string resultType) { 
            ResultType = resultType;
        }
    }
}
