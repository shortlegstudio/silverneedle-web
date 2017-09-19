// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Lexicon
{
    using System.Collections.Generic;
    using HandlebarsDotNet;
    using SilverNeedle.Characters;
    using SilverNeedle.Lexicon;
    using SilverNeedle.Treasure;
    using SilverNeedle.Serialization;

    public static class HandlebarsHelpers
    {
        private static IList<ITemplateExpander> helpers = new List<ITemplateExpander>();
        public static void InitializeHelpers() {
            //Only initialize Once TODO - Better solutions
            if(helpers.Count > 0)
                return;

            Handlebars.RegisterHelper("descriptor", (writer, context, parameters) => {
                ShortLog.DebugFormat("Getting descriptor: {0}", parameters[0].ToString());
                var value = context.descriptors[parameters[0].ToString()] as string[];
                writer.WriteSafeString(value.ChooseOne());
            });

            RegisterSupportedHelpers();
        }

        private static void RegisterSupportedHelpers()
        {
            helpers.Add(new ChooseDescriptor());
            helpers.Add(new ChooseWordFromGatewayObject<Color>(GatewayProvider.Get<Color>()));
            helpers.Add(new ChooseWordFromGatewayObject<Gem>(GatewayProvider.Get<Gem>()));
            helpers.Add(new ChooseWordFromGatewayObject<Occupation>(GatewayProvider.Get<Occupation>()));
        }
    }
}