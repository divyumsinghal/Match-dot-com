using MatchDotCom.UserProfile;
using MatchDotCom.UserDetails;

class Program
{
    static void Main(string[] args)
    {
        // Create an Address
        var address = new Address(
    street: "123 Main Street",
    city: "Dublin",
    stateOrProvince: "Leinster",
    postalCode: "D01 A1B2",
    country: "Ireland",
    eircode: "D01 A1B2"
);


        // Create Contact information
        var contact = new Contact(
            email: "john.doe@example.com",
            phoneNumber: "+353 1 234 5678",
            address: address
        );

        // Create ProfileBio
        var bio = new ProfileBio(
            bioText: "I'm a passionate software developer who loves hiking, reading, and exploring new technologies. I enjoy traveling to new places and meeting interesting people. In my free time, I like to cook, play guitar, and spend time outdoors. I'm looking for someone who shares similar interests and values meaningful conversations. I believe in living life to the fullest and making every moment count. Family and friends are very important to me, and I'm always up for new adventures and experiences.",
            lifeMotto: "Live life to the fullest",
            gender: GenderOptions.Male,
            genderPreference: new List<GenderOptions> { GenderOptions.Female },
            interests: new List<Interests>()
        );
        bio.Interests.Add(Interests.Technology);
        bio.Interests.Add(Interests.Travel);
        bio.Interests.Add(Interests.Music);

        // Create UserProfile
        var userProfile = new UserProfile
        (
            username: "john_doe_123",
            firstName: "John",
            middleName: "Michael",
            lastName: "Doe",
            dateOfBirth: new DateTime(1990, 5, 15),
            contact: contact,
            bio: bio
        );

        Console.WriteLine($"Created user profile for: {userProfile.FirstName} {userProfile.LastName}");
        Console.WriteLine($"Username: {userProfile.Username}");
        Console.WriteLine($"Email: {userProfile.Contact.Email}");
        Console.WriteLine($"Bio length: {userProfile.Bio.BioText.Length} characters");
        Console.WriteLine($"Interests: {string.Join(", ", userProfile.Bio.Interests)}");
    }
}