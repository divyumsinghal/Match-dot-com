namespace MatchDotCom.MatchingEngine.Services
{
    public class SearchService
    {
        private readonly List<UserProfile.UserProfile> _profiles;

        public SearchService(List<UserProfile.UserProfile> profiles)
        {
            _profiles = profiles;
        }

        public List<UserProfile.UserProfile> FindProfilesByCriteria(UserMatching.MatchCriteria criteria, LocationServices.Coordinates location, double radius)
        {
            return _profiles.Where(profile =>
                profile.Age >= criteria.AgeRange.MinAge &&
                profile.Age <= criteria.AgeRange.MaxAge &&
                GetDistance(profile.Contact.Address.Coordinates, location) <= radius
            ).ToList();
        }

        private double GetDistance(LocationServices.Coordinates loc1, LocationServices.Coordinates loc2)
        {
            // Haversine formula to calculate the distance between two points on the Earth
            var R = 6371; // Radius of the Earth in kilometers
            var dLat = DegreesToRadians(loc2.latitude - loc1.latitude);
            var dLon = DegreesToRadians(loc2.longitude - loc1.longitude);
            var a = Math.Pow(Math.Sin(dLat / 2), 2) +
                    Math.Cos(DegreesToRadians(loc1.latitude)) * Math.Cos(DegreesToRadians(loc2.latitude)) *
                    Math.Pow(Math.Sin(dLon / 2), 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c; // Distance in kilometers
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
    }
}