namespace MatchDotCom.ProfileManagement.Services
{
    public class ProfileService
    {
        private readonly Repositories.ProfileRepository _profileRepository;

        public ProfileService(Repositories.ProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task AddProfile(UserProfile.UserProfile profile)
        {
            await _profileRepository.Add(profile);
        }

        public async Task<UserProfile.UserProfile?> GetProfileByUsername(string username)
        {
            return await _profileRepository.GetByUsername(username);
        }

        public async Task<UserProfile.UserProfile?> GetProfileByGuid(Guid id)
        {
            return await _profileRepository.GetByGuid(id);
        }

        public async Task UpdateProfile(UserProfile.UserProfile profile)
        {
            await _profileRepository.Update(profile);
        }


        public async Task DeleteProfileByUsername(string username)
        {
            await _profileRepository.DeleteByUsername(username);
        }

        public async Task DeleteProfileByGuid(Guid id)
        {
            await _profileRepository.DeleteByGuid(id);
        }

        public async Task<List<UserProfile.UserProfile>> GetAllProfiles()
        {
            var profiles = await _profileRepository.GetAll();
            return profiles.ToList();
        }
    }
}