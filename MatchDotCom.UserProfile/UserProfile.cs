namespace MatchDotCom.UserProfile
{
    public class UserProfile
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public string LastName { get; set; } = "";

        public MatchDotCom.GenderOptions.GenderOptions Gender { get; set; }
        public string Bio { get; set; } = "";

        // Add more properties as needed
    }
}
