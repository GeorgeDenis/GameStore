namespace Application.Models.Review
{
    public class GetReviewModel
    {
        public Guid ReviewId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime ReviewDate { get; set; }

        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
    }
}
