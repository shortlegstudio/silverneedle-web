// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Lexicon
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Serialization;
    public abstract class TemplateSentenceGenerator : IGatewayObject
    {
        public List<PhraseTemplate> Templates { get; set; }
        public string Name { get; set; }
        public IDictionary<string, string[]> Descriptors { get; set; }

        public TemplateSentenceGenerator()
        {
            Descriptors = new Dictionary<string, string[]>();
            Templates = new List<PhraseTemplate>();
        }
        public TemplateSentenceGenerator(IObjectStore data) : this()
        {
            Name = data.GetString("name");
            var descs = data.GetObjectListOptional("descriptors");
            LoadDescriptors(descs);

            var temps = data.GetObjectListOptional("templates");
            if(temps != null)
            {
                this.Templates.Add(temps.Select(x => new PhraseTemplate(x.GetString("template"))));
            }
        }

        public TemplateSentenceGenerator(string name) : this()
        {
            Name = name;
        }


        public override string ToString()
        {
            return Name;
        }

        public virtual string CreateDescription()
        {
            var result = "";
            foreach(var d in Descriptors.Values)
            {
                result += string.Format("{0} ", d.ChooseOne());
            }

            if(result.Length > 0)
            {
                result = result.Substring(0, result.Length - 1);
            }

            result = string.Format("{0} {1}", result, Name);

            return result.Trim();
        }

        public virtual bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }

        public void AddDescriptor(string name, string[] items)
        {
            this.Descriptors.Add(name, items);
        }

        public void AddTemplate(string template)
        {
            this.Templates.Add(new PhraseTemplate(template));
        }

        private void LoadDescriptors(IEnumerable<IObjectStore> descriptors)
        {
            if(descriptors == null)
                return;
            
            foreach(var descriptor in descriptors) 
            {
                // Descriptor format in YAML is "- key: [item1, item2, item3]"
                var keyName = descriptor.Keys.First();
                ShortLog.DebugFormat("Descriptor KeyName: {0}", keyName);

                var m = descriptor.GetList(keyName);
                Descriptors.Add(keyName, m);
            }
        }
    }
}