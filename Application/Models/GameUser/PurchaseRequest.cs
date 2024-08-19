namespace Application.Models.GameUser
{
    public class PurchaseRequest
    {
        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
    }
}
