// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace SilverNeedle.Actions.CharacterGeneration
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class FeatSelector : ICharacterDesignStep
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

        private void SelectFeats(CharacterSheet character, WeightedOptionTable<string> preferredFeats)
        {            
            foreach(var token in character.GetAndRemoveAll<FeatToken>()) 
            {
                FilterPreferredFeats(character, preferredFeats, token);
                
                Feat feat;
                if(preferredFeats.IsEmpty) 
                {
                    var feats = this.feats.Where(x => FeatIsValid(x, token, character));
                    if(feats.HasChoices() == false)
                    {
                        throw new CharacterGeneratorStepFailedException(this, "Could not choose feat from token ({0})".Formatted(token.ToString()));
                    }
                    feat = feats.ChooseOne();
                } 
                else 
                {
                    var selection = preferredFeats.ChooseRandomly();
                    feat = feats.Find(selection);
                }
                character.Add(feat);
            }
        }

        private void FilterPreferredFeats(CharacterSheet character, WeightedOptionTable<string> preferredFeats, FeatToken token)
        {
            foreach(var entry in preferredFeats.All)
            {
                var f = feats.Find(entry.Option);
                if(FeatIsValid(f, token, character))
                {
                    preferredFeats.Enable(entry.Option);
                }
                else 
                {
                    preferredFeats.Disable(entry.Option);
                    ShortLog.DebugFormat("Preferred Feat [{0}] Token [{1}]- Not meeting requirements", f.Name, token.ToString());
                }
            }
        }

        private bool FeatIsValid(Feat feat, FeatToken token, CharacterSheet character)
        {
            return (feat.IsQualified(character) || token.IgnorePrerequisites) && token.Qualifies(feat);
        }

        public void ExecuteStep(CharacterSheet character, CharacterStrategy strategy)
        {
            SelectFeats(character, strategy.FavoredFeats);
        }
    }
}