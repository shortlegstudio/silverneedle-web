// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Lexicon
{
    using System.IO;
    using System.Collections.Generic;
    using HandlebarsDotNet;
    using SilverNeedle.Serialization;
    public class ChooseDescriptor : ITemplateExpander
    {
        private EntityGateway<Descriptor> descriptors;
        private string helperName = "choose";
        protected ChooseDescriptor(EntityGateway<Descriptor> descriptors, string helperName)
        {
            this.descriptors = descriptors;
            this.helperName = helperName;
            RegisterHelper();
        }

        public ChooseDescriptor()
        {
            this.descriptors = GatewayProvider.Get<Descriptor>();
            RegisterHelper();
        }

        public void ExpandTemplate(TextWriter writer, dynamic context, object[] parameters)
        {
            var name = parameters[0].ToString();
            var word = this.descriptors.Find(name).Words.ChooseOne();
            var dictionary = context as IDictionary<string, object>; 
            if(dictionary != null)
            {
                dictionary[name] = word;
                if(dictionary.ContainsKey(PhraseContext.CONTEXT_KEY))
                {
                    var phraseContext = dictionary[PhraseContext.CONTEXT_KEY] as PhraseContext;
                    phraseContext.Add(name, word);
                }
            }
            writer.WriteSafeString(word);
        }

        private void RegisterHelper()
        {
            Handlebars.RegisterHelper(helperName, this.ExpandTemplate);
        }

        public static ChooseDescriptor CreateIsolatedForUnitTesting(EntityGateway<Descriptor> descriptors, string helperName)
        {
            return new ChooseDescriptor(descriptors, helperName);
        }
    }
}