using Microsoft.AspNetCore.Mvc;

namespace MatchDotCom.ProfileManagement.Controllers
{
    /// <summary>
    /// Controller responsible for managing user profiles in the MatchDotCom application.
    /// Provides REST API endpoints for creating, retrieving, updating, and deleting user profiles.
    /// </summary>
    /// <remarks>
    /// This controller handles HTTP requests for profile management operations and delegates
    /// business logic to the ProfileService. All endpoints work with UserProfile objects
    /// and use username as the primary identifier for profile operations.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        /// <summary>
        /// The profile service dependency used for profile-related business operations.
        /// </summary>
        private readonly Services.ProfileService _profileService;

        /// <summary>
        /// Initializes a new instance of the ProfileController class.
        /// </summary>
        /// <param name="profileService">The profile service instance for handling profile operations.</param>

        public ProfileController(Services.ProfileService profileService)
        {
            _profileService = profileService;
        }

        /// <summary>
        /// Creates a new user profile.
        /// </summary>
        /// <param name="userProfile">The user profile data to create.</param>
        /// <returns>
        /// Returns 201 Created with the created profile if successful,
        /// or 400 Bad Request if the profile could not be created.
        /// </returns>
        ///
        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] UserProfile.UserProfile userProfile)
        {
            var createdProfile = await _profileService.AddProfile(userProfile);
            if (createdProfile == null)
                return BadRequest("Profile could not be created.");

            return CreatedAtAction(nameof(GetProfile), new { username = createdProfile.Username }, createdProfile);
        }

        /// <summary>
        /// Retrieves a user profile by username.
        /// </summary>
        /// <param name="username">The username of the profile to retrieve.</param>
        /// <returns>
        /// Returns 200 OK with the profile data if found,
        /// or 404 Not Found if the profile does not exist.
        /// </returns>
        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile(string username)
        {
            var profile = await _profileService.GetProfileByUsername(username);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }

        /// <summary>
        /// Updates an existing user profile.
        /// </summary>
        /// <param name="username">The username of the profile to update.</param>
        /// <param name="userProfile">The updated profile data.</param>
        /// <returns>
        /// Returns 200 OK with the updated profile if successful,
        /// 400 Bad Request if the username in URL and body don't match,
        /// or 404 Not Found if the profile does not exist.
        /// </returns>
        [HttpPut("{username}")]
        public async Task<IActionResult> UpdateProfile(string username, [FromBody] UserProfile.UserProfile userProfile)
        {
            if (username != userProfile.Username)
            {
                return BadRequest("Username in URL and body do not match.");
            }

            var updatedProfile = await _profileService.UpdateProfile(userProfile);
            if (updatedProfile == null)
            {
                return NotFound();
            }
            return Ok(updatedProfile);
        }

        /// <summary>
        /// Deletes a user profile by username.
        /// </summary>
        /// <param name="username">The username of the profile to delete.</param>
        /// <returns>
        /// Returns 200 OK if the profile was successfully deleted,
        /// or 404 Not Found if the profile does not exist.
        /// </returns>
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteProfile(string username)
        {
            var deleted = await _profileService.DeleteProfileByUsername(username);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
