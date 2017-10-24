// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Dice;
    using SilverNeedle.Serialization;

    public class Maturity : IGatewayObject
    {
        public Maturity()
        {
        }

        public Maturity(IObjectStore data)
        {
            Name = data.GetString("race");
            Adulthood = data.GetInteger("adulthood");
            Young = DiceStrings.ParseDice(data.GetString("young"));
            Trained = DiceStrings.ParseDice(data.GetString("trained"));
            Studied = DiceStrings.ParseDice(data.GetString("studied"));                
        }

        public string Name { get; set; }
        public int Adulthood { get; set; }
        public Cup Young { get; set; }
        public Cup Trained { get; set; }
        public Cup Studied { get; set; } 

        public bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }
    }
}

