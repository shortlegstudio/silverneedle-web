// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Lexicon
{
    using SilverNeedle.Serialization;
    using HandlebarsDotNet;

    public class ChooseWordFromGatewayObject<T> where T : ILexiconGatewayObject
    {
        private EntityGateway<T> gateway;
        private string helperName;

        public ChooseWordFromGatewayObject(EntityGateway<T> source)
        {
            this.gateway = source;
            this.helperName = string.Format("choose-{0}", typeof(T).Name.ToLower());
            RegisterHandlebarHelper();
        }

        private void RegisterHandlebarHelper()
        {

            Handlebars.RegisterHelper(this.helperName, (writer, context, parameters) => 
            {
                var chosen = gateway.ChooseOne();
                writer.Write(chosen.Name);
            });
        }

        public static ChooseWordFromGatewayObject<I> RegisterNewHelper<I>(I obj) where I : ILexiconGatewayObject
        {
            return new ChooseWordFromGatewayObject<I>(GatewayProvider.Get<I>());
        }

    }
}