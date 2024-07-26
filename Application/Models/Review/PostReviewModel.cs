namespace Application.Models.Review
{
    public class PostReviewModel
    {
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;

        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
    }
}
