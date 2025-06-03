namespace MatchDotCom.UserMatching
{

    public class MatchCriteria
    {
        public (int MinAge, int MaxAge) AgeRange { get; set; }
        public required List<MatchDotCom.UserDetails.Interests> InterestsCommon { get; set; }
        public double Distance { get; set; }
    }

}