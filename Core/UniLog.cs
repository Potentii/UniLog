using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Potentii.UniLog.Base;
using Assets.Potentii.UniLog.Transports.Core;
using JetBrains.Annotations;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Potentii.UniLog.Core
{
    public class UniLog
    {
        private readonly List<JsonConverter> _converters = new List<JsonConverter>();
        private readonly List<ILogTransport> _transports = new List<ILogTransport>();
        

        public void Debug(Component ctx, string code, string message, params (string, object)[] data)
        {
            _Log(ctx, ELogSeverity.Debug, code, message, null, data);
        }
        
        public void Trace(Component ctx, string code, string message, params (string, object)[] data)
        {
            _Log(ctx, ELogSeverity.Trace, code, message, null, data);
        }
        
        public void Info(Component ctx, string code, string message, params (string, object)[] data)
        {
            _Log(ctx, ELogSeverity.Info, code, message, null, data);
        }
        
        public void Warning(Component ctx, string code, string message, params (string, object)[] data)
        {
            _Log(ctx, ELogSeverity.Warning, code, message, null, data);
        }
        
        public void Error(Component ctx, string code, Exception cause, params (string, object)[] data)
        {
            _Log(ctx, ELogSeverity.Error, code, cause?.Message, cause, data);
        }
        
        public void Error(Component ctx, string code, string message, Exception cause, params (string, object)[] data)
        {
            _Log(ctx, ELogSeverity.Error, code, message, cause, data);
        }

        [Serializable]
        private struct LogEntry
        {
            public string level;
            public string code;
            [CanBeNull] public string message;
            public DateTime date;
            [CanBeNull] public string ctx;
            [CanBeNull] public string go;
            [CanBeNull] public Exception e;
            [CanBeNull] public Dictionary<string, object> data;
        }
        
        
        private void _Log([CanBeNull] Component ctx, ELogSeverity severity, string code, string message = null, Exception cause = null, params (string, object)[] data)
        {
            try
            {
                var log = "";
                
                log += $"[{severity.ToString()}] {code}: {message}\n"; // TODO disable on production
                
                
                log += JsonConvert.SerializeObject(new LogEntry
                {
                    level = severity.ToString(),
                    code = code,
                    message = message,
                    date = DateTime.UtcNow,
                    ctx = ctx?.GetType()?.Name,
                    go = $"{ctx?.gameObject?.name} ({ctx?.gameObject?.GetInstanceID()})",
                    e = cause,
                    data = data.Aggregate(new Dictionary<string, object>(), (map, tuple) =>
                    {
                        if (map.ContainsKey(tuple.Item1))
                            map[tuple.Item1] = tuple.Item2;
                        else
                            map.Add(tuple.Item1, tuple.Item2);
                        return map;
                    }),
                }, 
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Converters = _converters,
                    Formatting = Formatting.Indented, // TODO disable on production
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                });
                
                foreach (var logTransport in _transports)
                    logTransport.Transport(severity, log);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(e);
            }
        }



        public void RegisterConverter(JsonConverter converter)
        {
            _converters.Add(converter);
        }
        
        public void RegisterTransport(ILogTransport transport)
        {
            _transports.Add(transport);
        }
    }
}