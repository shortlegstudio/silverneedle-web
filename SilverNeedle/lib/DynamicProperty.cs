// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    using SilverNeedle.Utility;

    public class DynamicProperty 
    {

        string propertyValue;
        public IDynamicPropertyEvaluator Processor { get; private set; }

        public DynamicProperty(string value)
        {
            if (value[0] == '(')
            {
                propertyValue = value.Replace("(","").Replace(")","");
                ShortLog.DebugFormat("Processing Dynamic Property: {0}", propertyValue);
                Processor = propertyValue.Instantiate<IDynamicPropertyEvaluator>();
            }
            else 
            { 
                propertyValue = value;
            }
        }

        public string GetString()
        {
            if (Processor != null)
                return Processor.GetString();

            return propertyValue;
        }
    }
}