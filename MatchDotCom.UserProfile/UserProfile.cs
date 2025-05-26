using System.Text.RegularExpressions;

namespace MatchDotCom.UserProfile
{
    public class UserProfile
    {
        public required Guid Id { get; set; } = Guid.NewGuid();

        public required string Username { get; set; } = "";
        public required string FirstName { get; set; } = "";
        public required string MiddleName { get; set; } = "";
        public required string LastName { get; set; } = "";

        public required MatchDotCom.UserDetails.GenderOptions Gender { get; set; }

        public required MatchDotCom.UserDetails.Contact Contact { get; set; }
        public required string Bio { get; set; } = "";

        // Add more properties as needed
    }
}
