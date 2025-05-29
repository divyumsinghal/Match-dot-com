using System.Text.RegularExpressions;
using System.Diagnostics.CodeAnalysis;

namespace MatchDotCom.UserProfile
{
    public class UserProfile
    {
        public required Guid Id { get; set; } = Guid.NewGuid();

        public required string Username { get; set; } = "";
        public required string FirstName { get; set; } = "";
        public required string MiddleName { get; set; } = "";
        public required string LastName { get; set; } = "";
        public required DateTime DateOfBirth { get; set; }

        public required MatchDotCom.UserDetails.Contact Contact { get; set; }
        public required MatchDotCom.UserDetails.ProfileBio Bio { get; set; }

        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Creates a new instance of the UserProfile class.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="firstName">The first name of the user.</param>
        /// <param name="middleName">The middle name of the user.</param>
        /// <param name="lastName">The last name of the user.</param>
        /// <param name="dateOfBirth">The date of birth of the user.</param>
        /// <param name="contact">The contact information of the user.</param>
        /// <param name="bio">The profile bio of the user.</param>
        [SetsRequiredMembers]
        public UserProfile(string username, string firstName, string middleName, string lastName, DateTime dateOfBirth, MatchDotCom.UserDetails.Contact contact, MatchDotCom.UserDetails.ProfileBio bio)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Length < 3 || username.Length > 20 || !Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"))
            {
                throw new ArgumentException("Username must be between 3 and 20 characters long and can only contain letters, numbers, and underscores.");
            }

            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < 2 || firstName.Length > 50)
            {
                throw new ArgumentException("First name must be between 2 and 50 characters long.");
            }

            if (string.IsNullOrWhiteSpace(middleName) || middleName.Length < 1 || middleName.Length > 50)
            {
                throw new ArgumentException("Middle name must be between 1 and 50 characters long.");
            }

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 2 || lastName.Length > 50)
            {
                throw new ArgumentException("Last name must be between 2 and 50 characters long.");
            }

            if (dateOfBirth >= DateTime.UtcNow.AddYears(-18) || dateOfBirth < new DateTime(1900, 1, 1))
            {
                throw new ArgumentException("Date of birth must be atleast 18 years old");
            }

            Username = username;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Contact = contact ?? throw new ArgumentNullException(nameof(contact));
            Bio = bio ?? throw new ArgumentNullException(nameof(bio));
        }

    }
}
