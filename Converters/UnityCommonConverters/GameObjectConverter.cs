using System;
using Potentii.UniLog.Core;
using UnityEngine;

namespace Potentii.UniLog.Converters.UnityCommonConverters
{
    public class GameObjectConverter : IUniLogConverter<GameObject>
    {
        public object Convert(GameObject value)
        {
            if (value == null)
            {
                return null;
            }

            return new Entry
            {
                instanceId = value.GetInstanceID(),
                name = value.name,
            };

        }
        
        [Serializable]
        private struct Entry
        {
            public int instanceId;
            public string name;
        }
    }
}