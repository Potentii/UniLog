using System;
using System.Collections.Generic;
using Potentii.UniLog.Core;
using UnityEngine.Networking;

namespace Potentii.UniLog.Converters.UnityCommonConverters
{
    public class UnityWebRequestConverter : IUniLogConverter<UnityWebRequest>
    {
        public object Convert(UnityWebRequest value)
        {
            if (value == null)
            {
                return null;
            }

            return new Entry
            {
                method = value.method,
                url = value.url,
                done = value.isDone,
                error = value.error,
                resBody = value.downloadHandler?.text,
                resStatus = "" + value.responseCode,
                resHeaders = value.GetResponseHeaders(),
            };

        }
        
        [Serializable]
        private struct Entry
        {
            public string method;
            public string url;
            public bool done;
            public string error;
            public string resBody;
            public string resStatus;
            public Dictionary<string,string> resHeaders;
        }
    }
}