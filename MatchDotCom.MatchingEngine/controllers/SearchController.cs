using Microsoft.AspNetCore.Mvc;

namespace MatchDotCom.MatchingEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly Services.SearchService _searchService;

        public SearchController(Services.SearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("criteria")]
        public async Task<ActionResult<IEnumerable<UserProfile.UserProfile>>> SearchByCriteria(UserMatching.MatchCriteria criteria, LocationServices.Coordinates location, double radius)
        {
            var profiles = await _searchService.FindProfilesByCriteria(criteria, location, radius);
            return Ok(profiles);
        }

    }
}