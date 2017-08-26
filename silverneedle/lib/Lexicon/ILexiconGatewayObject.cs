// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Lexicon
{

    using SilverNeedle.Serialization;
    public interface ILexiconGatewayObject : IGatewayObject
    {
        string Name { get; }
    }
}