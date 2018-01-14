// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;

    /// <summary>
    /// Parse stat modifiers yaml.
    /// </summary>
    public static class ParseStatModifiersYaml
    {
        public static void Load(this IList<IValueStatModifier> list, IObjectStore data, string source)
        {
            var loaded = ParseYaml(data, source);
            foreach(var a in loaded)
            {
                list.Add(a);
            } 
        }

        /// <summary>
        /// Parses the yaml.
        /// </summary>
        /// <returns>The yaml.</returns>
        /// <param name="modifierNode">Modifier node.</param>
        /// <param name="source">Source of the modifier.</param>
        public static IList<IValueStatModifier> ParseYaml(IObjectStore modifierNode, string source)
        {
            IList<IValueStatModifier> modifiers = new List<IValueStatModifier>();

            foreach (var mod in modifierNode.Children)
            {
                var statName = mod.GetString("stat");
                var amount = mod.GetInteger("modifier");
                var type = mod.GetString("type");
                var condition = mod.GetStringOptional("condition");
                var name = mod.GetStringOptional("name");
                if(!string.IsNullOrEmpty(name)) 
                {
                    name = source + " " + name;
                }
                else
                {
                    name = source;
                }
                IValueStatModifier modifier;

                modifier = new ValueStatModifier(
                    statName,
                    amount,
                    type,
                    name);
                if (!string.IsNullOrEmpty(condition))
                {
                    modifier = new ConditionalStatModifier(
                        modifier,
                        condition
                    );
                }

                modifiers.Add(modifier);
            }

            return modifiers;
        }
    }
}