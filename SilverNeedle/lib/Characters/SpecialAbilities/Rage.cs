// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class Rage : SpecialAbility, IComponent
    {
        public Rage(IObjectStore configuration)
        {
            this.Name = "Rage";
            StrengthModifier = new ValueStatModifier(
                StatNames.Strength,
                configuration.GetInteger("strength"),
                "morale",
                "Rage"); 

            ConstitutionModifier = new ValueStatModifier(
                StatNames.Constitution,
                configuration.GetInteger("constitution"),
                "morale",
                "Rage"); 

            ACModifier = new ValueStatModifier(
                StatNames.BaseArmorClass,
                configuration.GetInteger("armor-class"),
                "penalty",
                "Rage"); 

            WillModifier = new ValueStatModifier(
                StatNames.WillSave,
                configuration.GetInteger("will"),
                "morale",
                "Rage"); 
        }

        public ValueStatModifier StrengthModifier { get; private set; }
        public ValueStatModifier ConstitutionModifier { get; private set; }
        public ValueStatModifier ACModifier { get; private set; }
        public ValueStatModifier WillModifier { get; private set; }

        public void Initialize(ComponentBag components)
        {
            components.ApplyStatModifiers(
                new IStatModifier[] { WillModifier, StrengthModifier, ConstitutionModifier, ACModifier}
            );
        }

        public void Update(IObjectStore data)
        {
            Name = data.GetString("name");
            StrengthModifier.Modifier = data.GetInteger("strength");
            ConstitutionModifier.Modifier = data.GetInteger("constitution");
            WillModifier.Modifier = data.GetInteger("will");
        }
    }
}