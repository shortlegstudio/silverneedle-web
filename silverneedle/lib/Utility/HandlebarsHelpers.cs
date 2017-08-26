// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Utility
{
    using System.Collections.Generic;
    using HandlebarsDotNet;
    using SilverNeedle.Lexicon;
    using SilverNeedle.Treasure;

    public class HandlebarsHelpers
    {
        private static IList<ITemplateExpander> helpers = new List<ITemplateExpander>();
        public static void ConfigureHelpers() {
            Handlebars.RegisterHelper("descriptor", (writer, context, parameters) => {
                ShortLog.DebugFormat("Getting descriptor: {0}", parameters[0].ToString());
                var value = context.descriptors[parameters[0].ToString()] as string[];
                writer.Write(value.ChooseOne());
            });

            Handlebars.RegisterHelper("choose", (writer, context, parameters) => {
                var descriptorName = parameters[0].ToString();
                var choice = Descriptor.FindAndChooseWord(descriptorName);

                // This is different logic...
                while(choice.Contains("[[") && choice.Contains("]]"))
                {
                    var startIndex = choice.IndexOf("[[");
                    var endIndex = choice.IndexOf("]]");
                    var subDescName = choice.Substring(startIndex + 2, endIndex - startIndex - 2);
                    var subDescChose = Descriptor.FindAndChooseWord(subDescName);
                    choice = choice.Substring(0, startIndex) + subDescChose + choice.Substring(endIndex + 2);
                }
                writer.Write(choice);
            });

            RegisterAllSupportedGateways();
        }

        private static void RegisterAllSupportedGateways()
        {
            if(helpers.Count > 0)
                return;
                
            helpers.Add(new ChooseWordFromGatewayObject<Color>(GatewayProvider.Get<Color>()));
            helpers.Add(new ChooseWordFromGatewayObject<Gem>(GatewayProvider.Get<Gem>()));
        }
    }
}