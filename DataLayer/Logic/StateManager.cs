using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;

namespace DataLayer.Logic
{
    public class StateManager : IStateManager
    {
        public const string MagicString = "__";
        public const string FloatEpsilonValueId = MagicString + "FloatEpsilonValue";
        public const float DefaultFloatEpsilonValue = 0.01f;

        public StateManager()
        {
            StateDictionary = new Dictionary<string, string>();
        }

        public Dictionary<string, string> StateDictionary { get; set; }

        public bool HasVariable(string name)
        {
            return StateDictionary.ContainsKey(name);
        }

        public string GetString(string variableName)
        {
            string result;

            return StateDictionary.TryGetValue(variableName, out result) ? result : null;
        }

        public float GetFloatEpsilonValue()
        {
            string result;
            return StateDictionary.TryGetValue(FloatEpsilonValueId, out result) ? 
                float.Parse(result, CultureInfo.InvariantCulture.NumberFormat) : 
                DefaultFloatEpsilonValue;
        }

        public void SetString(string variableName, string value)
        {
            StateDictionary.Add(variableName, value);
        }
    }
}