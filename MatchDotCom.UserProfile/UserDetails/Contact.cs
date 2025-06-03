using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace MatchDotCom.UserDetails
{
    /// <summary>
    /// Represents a contact information entity for a user profile.
    /// </summary>
    public class Contact
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Phone number of the user.
        /// </summary>
        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [MaxLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
        public required string PhoneNumber { get; set; }

        /// <summary>
        /// Email address of the user.
        /// </summary>
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [MaxLength(254, ErrorMessage = "Email address cannot exceed 254 characters.")]
        public required string Email { get; set; }

        /// <summary>
        /// Adress of the user.
        /// </summary>
        public required Address Address { get; set; }

        /// <summary>
        /// Audit fields for real-world tracking
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Contact"/> class.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <param name="phoneNumber">The phone number of the user.</param>
        /// /// <param name="address">The address of the user.</param>
        [SetsRequiredMembers]
        public Contact(string email, string phoneNumber, Address address)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        /// <summary>
        /// Converts the Contact object into a json for storage in database.
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
