using System;
using UnityEngine;

namespace Potentii.UniLog.Core
{
    public static class UniLog
    {
        private static readonly ILogger Logger = new DefaultLogger();


        public static void Debug(Component ctx, string code, string message, params (string, object)[] data)
        {
            Logger.Debug(ctx, code, message, data);
        }
        
        public static void Trace(Component ctx, string code, string message, params (string, object)[] data)
        {
            Logger.Trace(ctx, code, message, data);
        }
        
        public static void Info(Component ctx, string code, string message, params (string, object)[] data)
        {
            Logger.Info(ctx, code, message, data);
        }
        
        public static void Warning(Component ctx, string code, string message, params (string, object)[] data)
        {
            Logger.Warning(ctx, code, message, data);
        }
        
        public static void Error(Component ctx, string code, Exception cause, params (string, object)[] data)
        {
            Logger.Error(ctx, code, cause?.Message, cause, data);
        }
        
        public static void Error(Component ctx, string code, string message, Exception cause, params (string, object)[] data)
        {
            Logger.Error(ctx, code, message, cause, data);
        }

        
        
        
        public static void SetFormatter(IUniLogFormatter formatter)
        {
            Logger.SetFormatter(formatter);
        }

        public static void RegisterConverter<T>(IUniLogConverter<T> converter)
        {
            Logger.RegisterConverter(converter);
        }
        
        public static void RegisterTransport(IUniLogTransport transport)
        {
            Logger.RegisterTransport(transport);
        }
        
        
        
    }
}