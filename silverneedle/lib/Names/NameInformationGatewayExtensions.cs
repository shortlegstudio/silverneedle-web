
namespace SilverNeedle.Names
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public static class NameInformationGatewayExtensions
    {
        public static IList<string> GetFirstNames(this EntityGateway<NameInformation> gateway)
        {
            return gateway
                .Where(x => x.IsFirstName)
                .SelectMany(x => x.Names)
                .ToList();
        }

        public static IList<string> GetFirstNames(this EntityGateway<NameInformation> gateway, Gender gender, string race)
        {
            return gateway
                .Where(x => x.IsFirstName && 
                    x.MatchesRace(race) && 
                    x.MatchesGender(gender))
                .SelectMany(x => x.Names)
                .ToList();
        }

        public static IList<string> GetLastNames(this EntityGateway<NameInformation> gateway)
        {
            return gateway
                .Where(x => x.IsLastName)
                .SelectMany(x => x.Names)
                .ToList();
        }

         public static IList<string> GetLastNames(this EntityGateway<NameInformation> gateway, string race)
        {
            return gateway
                .Where(x => x.IsLastName &&
                    x.MatchesRace(race))
                .SelectMany(x => x.Names)
                .ToList();
        }
    }
}