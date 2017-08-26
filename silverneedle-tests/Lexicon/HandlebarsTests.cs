// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Lexicon
{
    using Xunit;
    using HandlebarsDotNet;

    public class HandlebarsTests
    {
        [Fact]
        public void CallHelpersWithArguments()
        {
            Handlebars.RegisterHelper("test", (writer, context, parameters) => {
                writer.Write("test-write");
            });

            var handleBarsTemplate = Handlebars.Compile("{{test}}");
            var result = handleBarsTemplate(string.Empty);
            Assert.Equal("test-write", result);
        }

        [Fact]
        public void CallHelperFromHelper()
        {
            Handlebars.RegisterHelper("test-one", (writer, context, parameters) => 
            {
                string subTemplate = "{{test-two}}";
                var template = Handlebars.Compile(subTemplate);
                var output = template(string.Empty);
                
                writer.Write(output);
            });

            Handlebars.RegisterHelper("test-two", (writer, context, parameters) => {
                writer.Write("test-two-called");
            });

            var handleBarsTemplate = Handlebars.Compile("{{test-one}}");
            var result = handleBarsTemplate("");

            Assert.Equal("test-two-called", result);
        }

        [Fact]
        public void CallHelperFromHelperWithContext()
        {
            Handlebars.RegisterHelper("test-one", (writer, context, parameters) => 
            {
                string subTemplate = "{{test-two}}";
                var template = Handlebars.Compile(subTemplate);
                var output = template(context);
                
                writer.Write(output);
            });

            Handlebars.RegisterHelper("test-two", (writer, context, parameters) => {
                writer.Write(context.test_two_called);
            });

            var handleBarsTemplate = Handlebars.Compile("{{test-one}}");
            var data = new {
                test_two_called = "test-two-called"
            };
            var result = handleBarsTemplate(data);

            Assert.Equal("test-two-called", result);
        }
    }
}