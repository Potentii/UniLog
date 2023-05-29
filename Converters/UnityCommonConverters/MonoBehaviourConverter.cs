using System;
using Potentii.UniLog.Core;
using UnityEngine;

namespace Potentii.UniLog.Converters.UnityCommonConverters
{
    public class MonoBehaviourConverter : IUniLogConverter<MonoBehaviour>
    {
        public object Convert(MonoBehaviour value)
        {
            if (value == null)
                return null;

            return new Entry
            {
                type = value.GetType().ToString(),
                isEnabled = value.enabled,
            };
        }
            
        [Serializable]
        private struct Entry
        {
            public string type;
            public bool isEnabled;
        }
    }
}