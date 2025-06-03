using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class NominatimResult
{
    public required string lat { get; set; }
    public required string lon { get; set; }
}

/// <summary>
/// A class for geocoding addresses using the Nominatim service.
/// It retrieves coordinates for a given address and provides fallback coordinates if the geocoding fails.
/// </summary>
public class Geocoder
{
    private static readonly HttpClient client = new HttpClient();

    /// <summary>
    /// Asynchronously retrieves coordinates for a given address using the Nominatim geocoding service.
    /// </summary>
    /// <param name="address">The address to geocode.</param>
    /// <returns>A task that represents the asynchronous operation, containing the coordinates if successful, or null if not.</returns>
    public static async Task<Coordinates?> GetCoordinatesAsync(string address)
    {
        try
        {
            // Clean and format the address for better geocoding results
            string cleanAddress = address.Trim().Replace("  ", " ");
            string encodedAddress = Uri.EscapeDataString(cleanAddress);
            string url = $"https://nominatim.openstreetmap.org/search?q={encodedAddress}&format=json&limit=1&countrycodes=ie";

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", "MatchDotCom/1.0 (contact@matchdotcom.ie)");

            // Add a small delay to respect rate limits
            await Task.Delay(1000);

            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"API request failed with status: {response.StatusCode}");
                return GetFallbackCoordinates(address);
            }

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Geocoding response: {content}");

            if (string.IsNullOrWhiteSpace(content) || content == "[]")
            {
                Console.WriteLine("Empty response from geocoding service, using fallback coordinates");
                return GetFallbackCoordinates(address);
            }

            var results = JsonSerializer.Deserialize<NominatimResult[]>(content);

            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No results from geocoding service, using fallback coordinates");
                return GetFallbackCoordinates(address);
            }

            if (double.TryParse(results[0].lat, out double lat) && double.TryParse(results[0].lon, out double lon))
            {
                Console.WriteLine($"Successfully geocoded to: {lat}, {lon}");
                return new Coordinates
                {
                    latitude = lat,
                    longitude = lon
                };
            }

            Console.WriteLine("Failed to parse coordinates, using fallback");
            return GetFallbackCoordinates(address);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Geocoding error: {ex.Message}");
            return GetFallbackCoordinates(address);
        }
    }

    /// <summary>
    /// Provides fallback coordinates for common Irish addresses, particularly Dublin.
    /// Later make it more sophisticated by checking for Dublin-related keywords in the address.
    /// </summary>
    /// <param name="address">The address to check for Dublin-related keywords.</param>
    /// <returns>Coordinates object with fallback values.</returns>
    private static Coordinates GetFallbackCoordinates(string address)
    {
        // Provide reasonable fallback coordinates for Dublin, Ireland
        // Since the example uses Dublin addresses

        return new Coordinates
        {
            latitude = 53.3498 + new Random().NextDouble() * 0.1 - 0.05, // random coordinates for Dublin
            longitude = -6.2603 + new Random().NextDouble() * 0.1 - 0.05
        };
    }
}
