using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Potentii.UniLog.Core
{
    public class DefaultLogger : ILogger
    {
        
        private IUniLogFormatter _formatter;
        private readonly List<JsonConverter> _newtonsoftConverters = new();
        private readonly List<IUniLogTransport> _transports = new();


        
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


        public void Error(Component ctx, string code, string message, Exception cause, params (string, object)[] data)
        {
            _Log(ctx, ELogSeverity.Error, code, message, cause, data);
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
                        Converters = _newtonsoftConverters,
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
        
        
        

        public void SetFormatter(IUniLogFormatter formatter)
        {
            _formatter = formatter;
        }

        public void RegisterConverter<T>(IUniLogConverter<T> converter)
        {
            _newtonsoftConverters.Add(new WrapperConverter<T>(converter));
        }
        
        public void RegisterTransport(IUniLogTransport transport)
        {
            _transports.Add(transport);
        }
        
        
        private class WrapperConverter<T> : JsonConverter<T>
        {
            private readonly IUniLogConverter<T> _baseConverter;

            public WrapperConverter(IUniLogConverter<T> baseConverter)
            {
                _baseConverter = baseConverter;
            }
            
            
            /// <inheritdoc />
            public override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer)
            {
                if(value == null)
                {
                    writer.WriteNull();
                    return;
                }
                
                serializer.Serialize(writer, _baseConverter.Convert(value));
            }
        
            /// <inheritdoc />
            public override T? ReadJson(JsonReader reader, Type objectType, T? existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
        
    }
}