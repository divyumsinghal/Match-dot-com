using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace MatchDotCom.UserDetails
{
    public class ProfileBio
    {
        /// <summary>
        /// Gets or sets the unique identifier for the profile bio inlcuding bio, life, data, and other details.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the bio text of the user.
        /// </summary>
        [Required(ErrorMessage = "Bio is required.")]
        [MinLength(500, ErrorMessage = "Bio must be at least 500 characters.")]
        [MaxLength(5000, ErrorMessage = "Bio cannot exceed 5000 characters.")]
        public required string BioText { get; set; }

        /// <summary>
        /// Gets or sets the life motto or quote of the user.
        /// </summary>
        [MaxLength(200, ErrorMessage = "Life motto cannot exceed 200 characters.")]
        public string? LifeMotto { get; set; }

        /// <summary>
        /// User's Gender.
        /// </summary>
        [Required(ErrorMessage = "Gender is required.")]
        public required MatchDotCom.UserDetails.GenderOptions Gender { get; set; }

        /// <summary>
        /// Gender Preference for the user.
        /// </summary>
        [Required(ErrorMessage = "Gender preference is required.")]
        [MinLength(1, ErrorMessage = "At least one gender preference is required.")]
        public required List<MatchDotCom.UserDetails.GenderOptions> GenderPreference { get; set; } = new List<MatchDotCom.UserDetails.GenderOptions>();

        /// <summary>
        /// Gets or sets the list of interests of the user.
        /// </summary>
        [Required(ErrorMessage = "At least one interest is required.")]
        [MinLength(1, ErrorMessage = "At least one interest is required.")]
        [MaxLength(10, ErrorMessage = "A maximum of 10 interests can be specified.")]
        public required List<MatchDotCom.UserDetails.Interests> Interests { get; set; } = new List<MatchDotCom.UserDetails.Interests>();

        /// <summary>
        /// Gets or sets the date when the bio was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date when the bio was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileBio"/> class.
        /// </summary>
        /// <param name="bioText">The bio text of the user.</param>
        /// <param name="lifeMotto">The life motto or quote of the user.</param>
        [SetsRequiredMembers]
        public ProfileBio(string bioText, string? lifeMotto, MatchDotCom.UserDetails.GenderOptions gender, List<MatchDotCom.UserDetails.GenderOptions> genderPreference, List<MatchDotCom.UserDetails.Interests> interests)
        {
            BioText = bioText;
            LifeMotto = lifeMotto;
            Gender = gender;
            GenderPreference = genderPreference;
            Interests = interests;
        }


    }
}