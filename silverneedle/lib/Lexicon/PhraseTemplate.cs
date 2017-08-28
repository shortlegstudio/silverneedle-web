// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Lexicon
{
    using HandlebarsDotNet;
    public class PhraseTemplate
    {
        public string Template { get; private set; }
        private System.Func<object, string> compiledTemplate;
        public PhraseTemplate(string template)
        {
            this.Template = template;
            compiledTemplate = Handlebars.Compile(this.Template);
        }

        public string WritePhrase(PhraseContext context)
        {
            var templateObject = context.CreateObject();
            var expandedString = compiledTemplate(templateObject);
            if(ContainsExpansions(expandedString))
            {
                var expandedPhrase = new PhraseTemplate(expandedString);
                return expandedPhrase.WritePhrase(context);
            }

            return expandedString;
        }

        private bool ContainsExpansions(string check)
        {
            //Could regex this to make it more robust but should suffice for now
            return check.Contains("{{") && check.Contains("}}");
        }
    }
}