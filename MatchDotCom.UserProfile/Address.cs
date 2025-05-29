using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchDotCom.UserDetails
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Street is required.")]
        [MaxLength(100)]
        public required string Street { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [MaxLength(50)]
        public required string City { get; set; }

        [MaxLength(50)]
        public required string StateOrProvince { get; set; }

        [Required(ErrorMessage = "Postal code is required.")]
        [MaxLength(20)]
        public required string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [MaxLength(50)]
        public required string Country { get; set; }

        /// <summary>
        /// Eircode
        /// /// </summary>
        /// [MaxLength(10)]
        [MaxLength(10, ErrorMessage = "Eircode cannot exceed 10 characters.")]
        public string Eircode { get; set; }

        // Geolocation
        public required double Latitude { get; set; }
        public required double Longitude { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> class.
        /// </summary>
        /// <param name="street">The street address.</param>
        /// <param name="city">The city.</param>
        /// <param name="stateOrProvince">The state or province.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="country">The country.</param>
        /// <param name="eircode">The Eircode (optional).</param>
        public Address(string street, string city, string stateOrProvince, string postalCode, string country, string eircode)
        {
            Street = street;
            City = city;
            StateOrProvince = stateOrProvince;
            PostalCode = postalCode;
            Country = country;
            Eircode = eircode;

            // Get Latitude and Longitude from Eircode
            Latitude = 0.0; // Placeholder for actual geolocation logic
            Longitude = 0.0; // Placeholder for actual geolocation logic
        }
    }
}
