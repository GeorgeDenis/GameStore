using Domain.Common;

namespace Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role UserRole { get; set; }
        public User()
        {

        }
        public User(string name, string email, string password, Role role)
        {
            UserId = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
            UserRole = role;
            Games = [];
        }

        public List<Review> Reviews { get; set; }
        public List<Game> Games { get; set; }
    }
}
