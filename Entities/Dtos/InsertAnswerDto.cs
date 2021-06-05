namespace Entities.Dtos
{
    public interface InsertAnswerDto
    {
        // Question answer will come automatically
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Score { get; set; }
        
    }
}