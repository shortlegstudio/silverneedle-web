// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Background
{
    using SilverNeedle.Lexicon;
    public class BackgroundStory 
    {
        private string story;
        public BackgroundStory(string story)
        {
            this.story = story;
        }
        public string GetStory()
        {
            return story;
        }
    }
}