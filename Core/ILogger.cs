using System;
using UnityEngine;

namespace Potentii.UniLog.Core
{

    public interface ILogger
    {
        void Debug(Component ctx, string code, string message, params (string, object)[] data);
        void Trace(Component ctx, string code, string message, params (string, object)[] data);
        void Info(Component ctx, string code, string message, params (string, object)[] data);
        void Warning(Component ctx, string code, string message, params (string, object)[] data);
        void Error(Component ctx, string code, string message, Exception cause, params (string, object)[] data);

        
        public void SetFormatter(IUniLogFormatter formatter);
        public void RegisterConverter<T>(IUniLogConverter<T> converter);
        public void RegisterTransport(IUniLogTransport transport);
    }
}