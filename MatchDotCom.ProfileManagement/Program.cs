using MatchDotCom.UserProfile;

var user1 = new UserProfile
{
    FirstName = "Alice",
    LastName = "Smith",
    Age = 25,
    Gender = "Female",
    LookingFor = "Male",
    Bio = "Love hiking and photography!",
    Interests = new List<string> { "Hiking", "Photography", "Cooking" }
};

Console.WriteLine($"Created profile for {user1.FirstName}, age {user1.Age}");
