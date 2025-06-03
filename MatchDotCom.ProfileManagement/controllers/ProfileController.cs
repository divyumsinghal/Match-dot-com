using Microsoft.AspNetCore.Mvc;

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
            return Ok(createdProfile);
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
                return BadRequest();
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
            await _profileService.DeleteProfileByUsername(username);
            return Ok();
        }
    }
}