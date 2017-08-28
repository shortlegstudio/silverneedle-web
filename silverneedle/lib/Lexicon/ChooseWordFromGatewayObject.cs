// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Lexicon
{
    using SilverNeedle.Serialization;
    using HandlebarsDotNet;

    public class ChooseWordFromGatewayObject<T> : ITemplateExpander where T : ILexiconGatewayObject
    {
        private EntityGateway<T> gateway;
        private string helperName;

        public ChooseWordFromGatewayObject(EntityGateway<T> source)
        {
            this.gateway = source;
            this.helperName = string.Format("choose-{0}", typeof(T).Name.ToLower());
            RegisterHandlebarHelper();
        }

        public void ExpandTemplate(System.IO.TextWriter writer, dynamic context, object[] parameters)
        {
            var chosen = gateway.ChooseOne();
            writer.WriteSafeString(chosen.Name);
        }

        private void RegisterHandlebarHelper()
        {
            Handlebars.RegisterHelper(this.helperName, this.ExpandTemplate);
        }
    }
}