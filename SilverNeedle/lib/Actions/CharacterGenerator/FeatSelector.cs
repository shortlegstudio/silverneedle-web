// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace SilverNeedle.Actions.CharacterGenerator
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    //TODO: Instead of passing in the gateway and the character sheet just use the token, strategy, and qualifying feats
    public class FeatSelector : ICreateStep, ILevelUpStep
    {
        EntityGateway<Feat> feats;

        public FeatSelector(EntityGateway<Feat> feats)
        {
            this.feats = feats;
        }

        public FeatSelector()
        {
            feats = GatewayProvider.Get<Feat>();
        }

        public void SelectFeats(CharacterSheet character, WeightedOptionTable<string> preferredFeats)
        {            
            foreach(var token in character.FeatTokens) 
            {
                
                //Enable/Disable options based on whether qualified for feat
                foreach(var entry in preferredFeats.All())
                {
                    var f = feats.Find(entry.Option);
                    if(!f.IsQualified(character)) {
                        ShortLog.DebugFormat("Feat {0} Disabled - Unqualified", f.Name); 
                        preferredFeats.Disable(entry.Option);
                    }
                    else if(!token.Qualifies(f)) {
                        ShortLog.DebugFormat("Feat {0} Disabled - Token Unable {1}", f.Name, token.ToString()); 
                        preferredFeats.Disable(entry.Option);
                    } else {
                        preferredFeats.Enable(entry.Option);
                    }
                }

                if(preferredFeats.IsEmpty) {
                    var feats = this.feats.Where(x => x.IsQualified(character)).ToList();
                    character.AddFeat(feats.ChooseOne());
                } else {
                    var selection = preferredFeats.ChooseRandomly();
                    var feat = feats.Find(selection);
                    character.AddFeat(feat);
                }               
            }
            character.FeatTokens.Clear();
        }

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            SelectFeats(character, strategy.FavoredFeats);
        }
    }
}