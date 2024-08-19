namespace Domain.Entities;

public class Developer
{
    public Guid DeveloperId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Game> Games { get; set; }
    public Developer()
    {

    }

    public Developer(string name, string description)
    {
        DeveloperId = Guid.NewGuid();
        Name = name;
        Description = description;
        Games = [];
    }
}
