// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Serialization
{
    using Xunit;
    using YamlDotNet;
    using SilverNeedle.Serialization;

    public class YamlHelperTests
    {
        [Fact]
        public void CanWriteObjectsIntoYamlString()
        {

            var yamlStore = new YamlObjectStore();
            yamlStore.SetValue("name", "A Name");
            yamlStore.SetValue("number", 2383);
            yamlStore.SetValue("list-of-values", new string[] { "one", "two", "three" });
            var childObj = new YamlObjectStore();
            childObj.SetValue("name", "childName");
            yamlStore.SetValue("child", childObj);

            var listOfObject = new YamlObjectStore[] { new YamlObjectStore(), new YamlObjectStore() };
            listOfObject[0].SetValue("name", "Test 1");
            listOfObject[1].SetValue("name", "Test 2");
            yamlStore.SetValue("list-of-objects", listOfObject);

            var yamlOutput = yamlStore.Serialize();
            var expectedYaml =@"name: A Name
number: 2383
list-of-values:
- one
- two
- three
child:
  name: childName
list-of-objects:
- name: Test 1
- name: Test 2
...
";
            Assert.Equal(expectedYaml, yamlOutput);

            // Deserializing Results In Same object store
            var deserialize = expectedYaml.ParseYaml();

            Assert.Equal("A Name", deserialize.GetString("name"));
            Assert.Equal(2383, deserialize.GetInteger("number"));
            Assert.Equal(new string[] { "one", "two", "three" }, deserialize.GetList("list-of-values"));
            Assert.Equal("childName", deserialize.GetObject("child").GetString("name"));
        }
    }
}