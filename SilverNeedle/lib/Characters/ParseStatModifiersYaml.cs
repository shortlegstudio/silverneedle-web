//-----------------------------------------------------------------------
// <copyright file="ParseStatModifiersYaml.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;

    /// <summary>
    /// Parse stat modifiers yaml.
    /// </summary>
    public static class ParseStatModifiersYaml
    {
        public static void Load(this IList<ValueStatModifier> list, IObjectStore data, string source)
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
        public static IList<ValueStatModifier> ParseYaml(IObjectStore modifierNode, string source)
        {
            IList<ValueStatModifier> modifiers = new List<ValueStatModifier>();

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
                ValueStatModifier modifier;

                if (!string.IsNullOrEmpty(condition))
                {
                    modifier = new ConditionalStatModifier(
                        condition,
                        statName,
                        amount,
                        type,
                        name);
                }
                else
                {
                    modifier = new ValueStatModifier(
                        statName,
                        amount,
                        type,
                        name);
                }

                modifiers.Add(modifier);
            }

            return modifiers;
        }
    }
}