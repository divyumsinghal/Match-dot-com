using MatchDotCom.UserProfile;
using MatchDotCom.UserDetails;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;



class Program
{
    static async Task Main(string[] args)
    {
        // Create an Address using the async factory method
        var address = await Address.CreateAsync(
            street: "Trinity College Dublin, College Green",
            city: "Dublin",
            stateOrProvince: "Leinster",
            postalCode: "D02 PN40",
            country: "Ireland",
            eircode: "D02 PN40"
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

        var json = userProfile.ToJson();

        Console.WriteLine($"Created user profile for: {json}");
    }
}