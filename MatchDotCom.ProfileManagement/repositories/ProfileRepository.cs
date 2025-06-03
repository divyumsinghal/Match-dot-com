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

        public async Task<UserProfile.UserProfile> Add(UserProfile.UserProfile profile)
        {
            await _context.UserProfiles.AddAsync(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

        public async Task<UserProfile.UserProfile?> GetById(int id)
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

        public async Task Delete(int id)
        {
            var profile = await GetById(id);
            if (profile != null)
            {
                _context.UserProfiles.Remove(profile);
                await _context.SaveChangesAsync();
            }
        }
    }
}