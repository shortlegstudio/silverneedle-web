// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Appearance
{
    using System.Linq;
    using SilverNeedle.Utility;

    public class PhysicalFeature : DescriptionDetail
    {
        public string[] Locations { get; set; }
        public string[] Templates { get; set; }

        public PhysicalFeature(IObjectStore data) : base(data)
        {
            var defaultTemplate = "{{pronoun}} has a {{description}} on {{possessivepronoun}} {{location}}.";

            Locations = data.GetListOptional("locations");
            if (Locations == null || Locations.Length == 0)
            {
                Locations = new string [] { "" };
                defaultTemplate = "{{pronoun}} has a {{description}}.";
            }

            var temps = data.GetObjectOptional("templates");
            if(temps != null)
            {
                Templates = temps.Children.Select(x => x.GetString("template")).ToArray();
            }
            else
            {
                Templates = new string[] {
                    defaultTemplate
                };
            }
        }
    }
}