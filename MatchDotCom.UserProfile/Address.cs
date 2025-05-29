using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

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
        public required Coordinates Coordinates { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> class.
        /// </summary>
        /// <param name="street">The street address.</param>
        /// <param name="city">The city.</param>
        /// <param name="stateOrProvince">The state or province.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="country">The country.</param>
        /// <param name="eircode">The Eircode (optional).</param>

        [SetsRequiredMembers]
        public Address(string street, string city, string stateOrProvince, string postalCode, string country, string eircode)
        {
            Street = street;
            City = city;
            StateOrProvince = stateOrProvince;
            PostalCode = postalCode;
            Country = country;
            Eircode = eircode;

            // Initialize with default coordinates that will be updated asynchronously
            Coordinates = new Coordinates
            {
                latitude = 53.3498, // Dublin city center default
                longitude = -6.2603
            };
        }

        /// <summary>
        /// Asynchronously initializes a new instance of the <see cref="Address"/> class with geocoded coordinates.
        /// </summary>
        public static async Task<Address> CreateAsync(string street, string city, string stateOrProvince, string postalCode, string country, string eircode)
        {
            var address = new Address(street, city, stateOrProvince, postalCode, country, eircode);
            await address.UpdateCoordinatesAsync();
            return address;
        }

        /// <summary>
        /// Updates the coordinates by geocoding the address.
        /// </summary>
        public async Task UpdateCoordinatesAsync()
        {
            try
            {
                // Create a comprehensive address string for geocoding
                string fullAddress = $"{Street}, {City}, {StateOrProvince}, {PostalCode}, {Country}";
                if (!string.IsNullOrWhiteSpace(Eircode))
                {
                    fullAddress = $"{Eircode}, {fullAddress}";
                }

                var coords = await Geocoder.GetCoordinatesAsync(fullAddress);
                if (coords != null)
                {
                    Coordinates = coords;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update coordinates: {ex.Message}");
                // Keep the default coordinates if geocoding fails
            }
        }
    }
}
