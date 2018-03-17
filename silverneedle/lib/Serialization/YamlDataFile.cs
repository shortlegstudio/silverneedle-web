namespace SilverNeedle.Serialization
{
    using System.Linq;
    using System.Collections.Generic;
    using YamlDotNet.RepresentationModel;

    public class YamlDataFile
    {
        private YamlNode baseNode;
        public YamlDataFile(YamlNode yaml)
        {
            baseNode = yaml;
        }

        public IEnumerable<IObjectStore> GetEntities()
        {
            if(baseNode is YamlSequenceNode)
            {
                var seq = baseNode as YamlSequenceNode;
                return seq.Children.Select(x => new YamlObjectStore(x));
            }
            else
            {
                return new IObjectStore[] { new YamlObjectStore(baseNode) };
            }
        }

        public string FileType()
        {
            return baseNode.Tag;
        }
    }
}