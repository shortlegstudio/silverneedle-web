// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Externals
{
    using Xunit;
    using Newtonsoft.Json;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using SilverNeedle.Actions.CharacterGeneration;

    public class JsonTests : RequiresDataFiles
    {
        [Fact]
        public void CanSerializeADelegateStatModifier()
        {
            var statMod = new DelegateStatModifier(
                "Some Stat",
                "FooBar",
                () => 3
            );

            var json = JsonConvert.SerializeObject(statMod);
        }


        [Fact]
        public void CanSerializeACharacterSheet()
        {
            var fighter = GatewayProvider.Find<CharacterStrategy>("fighter");
            fighter.TargetLevel = 1;
            var gen = GatewayProvider.Find<CharacterDesigner>(fighter.Designer);
            var character = new CharacterSheet(fighter);
            gen.ExecuteStep(character);

            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new JsonSilverNeedleContractResolver();
            settings.Converters.Add(new JsonIgnoreClassConverter());
            settings.Converters.Add(new JsonCharacterSheetConverter());
            settings.Converters.Add(new JsonValueStatisticConverter());
            settings.Converters.Add(new JsonFamilyTreeConverter());

            var json = JsonConvert.SerializeObject(character, settings);
        }
    }
}