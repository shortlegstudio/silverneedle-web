// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Appearance
{
    public class FacialDescription
    {
        public FacialDescription()
        {
            HairColor = new HairColor("none");
            HairStyle = new HairStyle("none");
            FacialHair = new FacialHair("none");
            EyeColor = new EyeColor("none");
        }

        public EyeColor EyeColor { get; set; }
        public FacialHair FacialHair { get; set; }
        public HairColor HairColor { get; set; }
        public HairStyle HairStyle { get; set; }
    }
}

