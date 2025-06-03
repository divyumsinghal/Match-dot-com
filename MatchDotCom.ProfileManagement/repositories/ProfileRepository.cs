using Microsoft.EntityFrameworkCore;

namespace MatchDotCom.ProfileManagement.Repositories
{
    /// <summary>
    /// Repository class for managing user profile data operations.
    /// Provides CRUD operations for UserProfile entities using Entity Framework Core.
    /// </summary>
    public class ProfileRepository
    {
        /// <summary>
        /// The database context for user profile operations.
        /// </summary>
        private readonly Data.AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the ProfileRepository class.
        /// </summary>
        /// <param name="context">The database context to use for data operations.</param>
        public ProfileRepository(Data.AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a user profile by username.
        /// </summary>
        /// <param name="username">The username to search for.</param>
        /// <returns>The user profile if found; otherwise, null.</returns>
        public async Task<UserProfile.UserProfile?> GetByUsername(string username)
        {
            return await _context.UserProfiles.FirstOrDefaultAsync(u => u.Username == username);
        }

        /// <summary>
        /// Retrieves a user profile by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier (GUID) of the user profile.</param>
        /// <returns>The user profile if found; otherwise, null.</returns>
        public async Task<UserProfile.UserProfile?> GetByGuid(Guid id)
        {
            return await _context.UserProfiles.FindAsync(id);
        }

        /// <summary>
        /// Retrieves all user profiles from the database.
        /// </summary>
        /// <returns>A collection of all user profiles.</returns>
        public async Task<IEnumerable<UserProfile.UserProfile>> GetAll()
        {
            return await _context.UserProfiles.ToListAsync();
        }

        /// <summary>
        /// Updates an existing user profile in the database.
        /// </summary>
        /// <param name="profile">The user profile with updated information.</param>
        /// <returns>The updated user profile.</returns>
        public async Task<UserProfile.UserProfile> Update(UserProfile.UserProfile profile)
        {
            _context.UserProfiles.Update(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

        /// <summary>
        /// Deletes a user profile by username.
        /// </summary>
        /// <param name="username">The username of the profile to delete.</param>
        /// <returns>True if the profile was deleted; otherwise, false.</returns>
        public async Task<bool> DeleteByUsername(string username)
        {
            var profile = await GetByUsername(username);
            if (profile != null)
            {
                _context.UserProfiles.Remove(profile);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes a user profile by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier (GUID) of the profile to delete.</param>
        /// <returns>True if the profile was deleted; otherwise, false.</returns>
        public async Task<bool> DeleteByGuid(Guid id)
        {
            var profile = await GetByGuid(id);
            if (profile != null)
            {
                _context.UserProfiles.Remove(profile);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds a new user profile to the database.
        /// </summary>
        /// <param name="profile">The user profile to add.</param>
        /// <returns>The added user profile.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the profile is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when a profile with the same username / GUID already exists.</exception>
        public async Task<UserProfile.UserProfile> Add(UserProfile.UserProfile profile)
        {
            if (profile == null)
                throw new ArgumentNullException(nameof(profile), "Profile cannot be null.");

            try
            {
                await _context.UserProfiles.AddAsync(profile);
                await _context.SaveChangesAsync();
                return profile;
            }
            catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
            {
                throw new InvalidOperationException("A profile with this username or GUID already exists.", ex);
            }
        }

        /// <summary>
        /// Checks if a database exception is due to a unique constraint violation.
        /// The database enforces unique usernames and GUIDs through the following constraints:
        /// - CREATE UNIQUE INDEX UX_UserProfiles_Username ON UserProfiles (Username);
        /// - CREATE UNIQUE INDEX UX_UserProfiles_Id ON UserProfiles (Id);
        /// </summary>
        /// <param name="ex">The DbUpdateException to check.</param>
        /// <returns>True if the exception is due to a unique constraint violation; otherwise, false.</returns>
        /// <remarks>
        /// This method should be adjusted based on your specific database provider and its error codes/messages.
        /// Currently checks for the presence of "UNIQUE" in the inner exception message.
        /// </remarks>
        private bool IsUniqueConstraintViolation(DbUpdateException ex)
        {
            // Adjust this based on your DB provider and error codes/messages
            return ex.InnerException?.Message.Contains("UNIQUE") == true;
        }
    }
}