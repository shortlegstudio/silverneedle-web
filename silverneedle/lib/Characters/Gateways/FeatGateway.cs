// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using System.Linq;
using SilverNeedle;
using SilverNeedle.Utility;

namespace SilverNeedle.Characters
{
    public class FeatGateway : IFeatGateway
    {
              /// <summary>
        /// The trait data file.
        /// </summary>
        private const string FeatDataFileType = "feat";

        private IList<Feat> feats = new List<Feat>();

        public FeatGateway()
        {
            var objects = DatafileLoader.Instance.GetDataFiles(FeatDataFileType);
            foreach(var y in objects) {
                this.feats.Add(LoadObjects(y));
            }
        }
        public FeatGateway(IObjectStore dataStore)
        {
            feats.Add(LoadObjects(dataStore));
        }

        public IEnumerable<Feat> All()
        {
            throw new NotImplementedException();
        }

        public Feat GetByName(string name)
        {
            return feats.FirstOrDefault(x => x.Name.EqualsIgnoreCase(name));
        }

        public IEnumerable<Feat> GetQualifyingFeats(CharacterSheet character)
        {
            return feats.Where(x => x.IsQualified(character));
        }

        private IList<Feat> LoadObjects(IObjectStore yaml)
        {
            var loadedFeats = new List<Feat>();
            foreach (var featNode in yaml.Children)
            {
                var feat = new Feat();
                feat.Name = featNode.GetString("name"); 
                ShortLog.DebugFormat("Loading Feat: {0}", feat.Name);
                feat.Description = featNode.GetString("description");

                // Get Any skill Modifiers if they exist
                var skills = featNode.GetObjectOptional("modifiers");
                if (skills != null)
                {
                    foreach (var skillAdj in skills.Children)
                    {
                        var skillName = skillAdj.GetString("stat");
                        var modifier = skillAdj.GetInteger("modifier");
                        var type = skillAdj.GetString("type");
                        feat.Modifiers.Add(new BasicStatModifier(
                            skillName,
                            modifier,
                            type,
                            string.Format("{0} (feat)", feat.Name)));
                    }
                }

                // Get any prerequisites
                var prereq = featNode.GetObjectOptional("prerequisites");
                if (prereq != null)
                {
                    feat.Prerequisites = new Prerequisites(prereq);
                }

                feat.Tags = featNode.GetListOptional("tags").ToList();
                loadedFeats.Add(feat);
            }

            return loadedFeats;
        }
    }
}