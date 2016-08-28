//-----------------------------------------------------------------------
// <copyright file="ParseStatModifiersYaml.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using SilverNeedle.Yaml;

    /// <summary>
    /// Parse stat modifiers yaml.
    /// </summary>
    public static class ParseStatModifiersYaml
    {
        /// <summary>
        /// Parses the yaml.
        /// </summary>
        /// <returns>The yaml.</returns>
        /// <param name="modifierNode">Modifier node.</param>
        /// <param name="source">Source of the modifier.</param>
        public static IList<BasicStatModifier> ParseYaml(YamlNodeWrapper modifierNode, string source)
        {
            IList<BasicStatModifier> modifiers = new List<BasicStatModifier>();

            foreach (var mod in modifierNode.Children())
            {
                var statName = mod.GetString("stat");
                var amount = mod.GetInteger("modifier");
                var type = mod.GetString("type");
                var condition = mod.GetStringOptional("condition");
                BasicStatModifier modifier;

                if (!string.IsNullOrEmpty(condition))
                {
                    modifier = new ConditionalStatModifier(
                        condition,
                        statName,
                        amount,
                        type,
                        source);
                }
                else
                {
                    modifier = new BasicStatModifier(
                        statName,
                        amount,
                        type,
                        source);
                }

                modifiers.Add(modifier);
            }

            return modifiers;
        }
    }
}