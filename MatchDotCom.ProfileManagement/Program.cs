using MatchDotCom.UserProfile;
using MatchDotCom.UserDetails;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/// UserProfile Format
/*
{
  "Id": "2ee23a28-d106-4c61-bb6b-45d2aae6e4f1",
  "Username": "john_doe_123",
  "FirstName": "John",
  "MiddleName": "Michael",
  "LastName": "Doe",
  "DateOfBirth": "1990-05-15T00:00:00",
  "Contact": {
    "Id": "7ff452f8-45cd-4513-a7a2-5c8a87b1f7d0",
    "PhoneNumber": "\u002B353 1 234 5678",
    "Email": "john.doe@example.com",
    "Address": {
      "Id": "cde8a2fa-098e-41ec-bc4e-2f4d1439b7fa",
      "Street": "Trinity College Dublin, College Green",
      "City": "Dublin",
      "StateOrProvince": "Leinster",
      "PostalCode": "D02 PN40",
      "Country": "Ireland",
      "Eircode": "D02 PN40",
      "Coordinates": {
        "latitude": 53.32360964191232,
        "longitude": -6.289277102067593
      }
    },
    "CreatedAt": "2025-05-29T21:38:36.3323404Z",
    "UpdatedAt": null
  },
  "Bio": {
    "Id": "91e7e1ff-a2da-4fe5-8fb3-c1138b08e92f",
    "BioText": "I\u0027m a passionate software developer who loves hiking, reading, and exploring new technologies. I enjoy traveling to new places and meeting interesting people. In my free time, I like to cook, play guitar, and spend time outdoors. I\u0027m looking for someone who shares similar interests and values meaningful conversations. I believe in living life to the fullest and making every moment count. Family and friends are very important to me, and I\u0027m always up for new adventures and experiences.",
    "LifeMotto": "Live life to the fullest",
    "Gender": 0,
    "GenderPreference": [
      1
    ],
    "Interests": [
      5,
      2,
      1
    ],
    "CreatedAt": "2025-05-29T21:38:36.3331762Z",
    "UpdatedAt": null
  },
  "CreatedAt": "2025-05-29T21:38:36.3345606Z",
  "UpdatedAt": null
}
*/

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