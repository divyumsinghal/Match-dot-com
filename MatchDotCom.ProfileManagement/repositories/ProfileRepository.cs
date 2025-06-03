using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchDotCom.ProfileManagement.Repositories
{
    public class ProfileRepository
    {
        private readonly Data.AppDbContext _context;

        public ProfileRepository(Data.AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserProfile.UserProfile?> GetByUsername(string username)
        {
            return await _context.UserProfiles.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<UserProfile.UserProfile?> GetByGuid(Guid id)
        {
            return await _context.UserProfiles.FindAsync(id);
        }

        public async Task<IEnumerable<UserProfile.UserProfile>> GetAll()
        {
            return await _context.UserProfiles.ToListAsync();
        }

        public async Task<UserProfile.UserProfile> Update(UserProfile.UserProfile profile)
        {
            _context.UserProfiles.Update(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

        public async Task DeleteByUsername(string username)
        {
            var profile = await GetByUsername(username);
            if (profile != null)
            {
                _context.UserProfiles.Remove(profile);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteById(Guid id)
        {
            var profile = await GetByGuid(id);
            if (profile != null)
            {
                _context.UserProfiles.Remove(profile);
                await _context.SaveChangesAsync();
            }
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
        /// THe Database enforce unique usernames and GUIDs.
        /// This method checks if the exception is due to a unique constraint violation.
        /// Used for making sure that you can add users without violating the unique constraints set in the database.
        /// CREATE UNIQUE INDEX UX_UserProfiles_Username ON UserProfiles (Username);
        /// CREATE UNIQUE INDEX UX_UserProfiles_Id ON UserProfiles (Id);
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private bool IsUniqueConstraintViolation(DbUpdateException ex)
        {
            // Adjust this based on your DB provider and error codes/messages
            return ex.InnerException?.Message.Contains("UNIQUE") == true;
        }
    }
}