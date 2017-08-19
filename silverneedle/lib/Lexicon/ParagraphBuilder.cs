// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Lexicon
{
    using System.Text;
    public class ParagraphBuilder
    {
        private StringBuilder paragraph = new StringBuilder();
        private string SentenceStructure= "{0} ";

        public void AddSentence(string sentence)
        {
            paragraph.AppendFormat(this.SentenceStructure, sentence);
        }

        public string GetParagraph()
        {
            return paragraph.ToString().Trim();
        }
    }
}