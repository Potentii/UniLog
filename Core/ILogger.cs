using System;
using Assets.Potentii.UniLog.Transports.Core;
using Unity.Plastic.Newtonsoft.Json;
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


        void RegisterConverter(JsonConverter converter);

        void RegisterTransport(ILogTransport transport);
    }
}