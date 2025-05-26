using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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

        // Optional: Geolocation or metadata
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> class.
        /// </summary>
        /// <param name="street">The street address.</param>
        /// <param name="city">The city.</param>
        /// <param name="stateOrProvince">The state or province.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="country">The country.</param>
        public Address(string street, string city, string stateOrProvince, string postalCode, string country)
        {
            Street = street;
            City = city;
            StateOrProvince = stateOrProvince;
            PostalCode = postalCode;
            Country = country;
        }
    }
}
