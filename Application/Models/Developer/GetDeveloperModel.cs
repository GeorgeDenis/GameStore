namespace Application.Models.Developer
{
    public class GetDeveloperModel
    {
        public Guid DeveloperId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
