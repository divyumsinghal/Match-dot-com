using System;
using System.ComponentModel.DataAnnotations;

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
        public Contact(string email, string phoneNumber)
        {
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
