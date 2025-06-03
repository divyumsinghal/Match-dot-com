using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MatchDotCom.ProfileManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly Services.ProfileService _profileService;

        public ProfileController(Services.ProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] UserProfile.UserProfile userProfile)
        {
            var createdProfile = await _profileService.AddProfile(userProfile);
            if (createdProfile == null)
                return BadRequest("Profile could not be created.");

            return CreatedAtAction(nameof(GetProfile), new { username = createdProfile.Username }, createdProfile);
        }

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
