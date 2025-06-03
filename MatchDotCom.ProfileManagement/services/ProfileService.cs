namespace MatchDotCom.ProfileManagement.Services
{
    /// <summary>
    /// Service class for managing user profile operations.
    /// Provides business logic layer for user profile management including
    /// creation, retrieval, updating, and deletion of user profiles.
    /// </summary>
    public class ProfileService
    {
        /// <summary>
        /// The repository instance for data access operations.
        /// </summary>
        private readonly Repositories.ProfileRepository _profileRepository;

        /// <summary>
        /// Initializes a new instance of the ProfileService class.
        /// </summary>
        /// <param name="profileRepository">The repository instance for profile data operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when profileRepository is null.</exception>
        public ProfileService(Repositories.ProfileRepository profileRepository)
        {
            _profileRepository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
        }

        /// <summary>
        /// Adds a new user profile to the system.
        /// </summary>
        /// <param name="profile">The user profile to add.</param>
        /// <returns>A task representing the asynchronous add operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when profile is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when a profile with the same username or GUID already exists.</exception>
        public async Task<UserProfile.UserProfile?> AddProfile(UserProfile.UserProfile profile)
        {
            return await _profileRepository.Add(profile);
        }

        /// <summary>
        /// Retrieves a user profile by username.
        /// </summary>
        /// <param name="username">The username to search for.</param>
        /// <returns>The user profile if found; otherwise, null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when username is null.</exception>
        /// <exception cref="ArgumentException">Thrown when username is empty or whitespace.</exception>
        public async Task<UserProfile.UserProfile?> GetProfileByUsername(string username)
        {
            return await _profileRepository.GetByUsername(username);
        }

        /// <summary>
        /// Retrieves a user profile by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier (GUID) of the user profile.</param>
        /// <returns>The user profile if found; otherwise, null.</returns>
        public async Task<UserProfile.UserProfile?> GetProfileByGuid(Guid id)
        {
            return await _profileRepository.GetByGuid(id);
        }

        /// <summary>
        /// Updates an existing user profile in the system.
        /// </summary>
        /// <param name="profile">The user profile with updated information.</param>
        /// <returns>The updated User Profile</returns>
        /// <exception cref="ArgumentNullException">Thrown when profile is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the profile does not exist in the system.</exception>
        public async Task<UserProfile.UserProfile?> UpdateProfile(UserProfile.UserProfile profile)
        {
            return await _profileRepository.Update(profile);
        }


        /// <summary>
        /// Deletes a user profile by username.
        /// </summary>
        /// <param name="username">The username of the profile to delete.</param>
        /// <returns>A Bool representing the asynchronous delete operation. True if found and deleted</returns>
        /// <exception cref="ArgumentNullException">Thrown when username is null.</exception>
        /// <exception cref="ArgumentException">Thrown when username is empty or whitespace.</exception>
        /// <remarks>
        /// If no profile with the specified username exists, the operation completes without error.
        /// </remarks>
        public async Task<bool> DeleteProfileByUsername(string username)
        {
            return await _profileRepository.DeleteByUsername(username);
        }

        /// <summary>
        /// Deletes a user profile by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier (GUID) of the profile to delete.</param>
        /// <returns>A Bool representing the asynchronous delete operation.</returns>
        /// <remarks>
        /// If no profile with the specified GUID exists, the operation completes without error.
        /// </remarks>
        public async Task<bool> DeleteProfileByGuid(Guid id)
        {
            return await _profileRepository.DeleteByGuid(id);
        }

        /// <summary>
        /// Retrieves all user profiles from the system.
        /// </summary>
        /// <returns>A list containing all user profiles in the system.</returns>
        /// <remarks>
        /// This method loads all profiles into memory. Use with caution in systems with large numbers of profiles.
        /// Consider implementing pagination for production scenarios with many profiles.
        /// </remarks>
        public async Task<List<UserProfile.UserProfile>> GetAllProfiles()
        {
            var profiles = await _profileRepository.GetAll();
            return profiles.ToList();
        }
    }
}