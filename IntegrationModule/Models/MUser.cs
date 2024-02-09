
namespace IntegrationModule.Models
{
    public class MUser
    {
        public int Iduser { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool Verified { get; set; }

        public string SecurityToken { get; set; } = null!;

        public int CountryId { get; set; }

        public bool Deactivated { get; set; }

        public int UserTypeId { get; set; }

    }
}
