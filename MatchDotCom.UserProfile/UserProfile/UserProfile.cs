using System.Text.RegularExpressions;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

/// <summary>
/// MatchDotCom UserProfile namespace contains classes and methods related to user profiles.
/// </summary>
/// Each user has a unique username which is used to identify them on the platform.
///
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
        public int Age =>
            DateOfBirth > DateTime.UtcNow.AddYears(-(DateTime.UtcNow.Year - DateOfBirth.Year)) ?
                DateTime.UtcNow.Year - DateOfBirth.Year - 1 :
                DateTime.UtcNow.Year - DateOfBirth.Year;

        public required UserDetails.Contact Contact { get; set; }
        public required UserDetails.ProfileBio Bio { get; set; }

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
        public UserProfile(string username, string firstName, string middleName, string lastName, DateTime dateOfBirth, UserDetails.Contact contact, UserDetails.ProfileBio bio)
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

        /// <summary>
        /// Converts the UserProfile object into a json for storage in database.
        /// </summary>
        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true // Optional: makes the output human-readable
            };

            return JsonSerializer.Serialize(this, options);
        }

    }
}

/// UserProfile Format
/*
{
  "Id": "2ee23a28-d106-4c61-bb6b-45d2aae6e4f1",
  "Username": "john_doe_123",
  "FirstName": "John",
  "MiddleName": "Michael",
  "LastName": "Doe",
  "DateOfBirth": "1990-05-15T00:00:00",
  "Contact": {
    "Id": "7ff452f8-45cd-4513-a7a2-5c8a87b1f7d0",
    "PhoneNumber": "\u002B353 1 234 5678",
    "Email": "john.doe@example.com",
    "Address": {
      "Id": "cde8a2fa-098e-41ec-bc4e-2f4d1439b7fa",
      "Street": "Trinity College Dublin, College Green",
      "City": "Dublin",
      "StateOrProvince": "Leinster",
      "PostalCode": "D02 PN40",
      "Country": "Ireland",
      "Eircode": "D02 PN40",
      "Coordinates": {
        "latitude": 53.32360964191232,
        "longitude": -6.289277102067593
      }
    },
    "CreatedAt": "2025-05-29T21:38:36.3323404Z",
    "UpdatedAt": null
  },
  "Bio": {
    "Id": "91e7e1ff-a2da-4fe5-8fb3-c1138b08e92f",
    "BioText": "I\u0027m a passionate software developer who loves hiking, reading, and exploring new technologies. I enjoy traveling to new places and meeting interesting people. In my free time, I like to cook, play guitar, and spend time outdoors. I\u0027m looking for someone who shares similar interests and values meaningful conversations. I believe in living life to the fullest and making every moment count. Family and friends are very important to me, and I\u0027m always up for new adventures and experiences.",
    "LifeMotto": "Live life to the fullest",
    "Gender": 0,
    "GenderPreference": [
      1
    ],
    "Interests": [
      5,
      2,
      1
    ],
    "CreatedAt": "2025-05-29T21:38:36.3331762Z",
    "UpdatedAt": null
  },
  "CreatedAt": "2025-05-29T21:38:36.3345606Z",
  "UpdatedAt": null
}
*/