using Assets.Potentii.UniLog.Base;
using Assets.Potentii.UniLog.Transports.Core;

namespace Potentii.UniLog.Transports.UnityConsoleTransports
{
    public class UnityConsoleTransport : ILogTransport
    {
        /// <inheritdoc />
        public void Transport(ELogSeverity severity, string entry)
        {
            if(severity == ELogSeverity.Error)
            {
                UnityEngine.Debug.LogError(entry);
            }
            else if(severity == ELogSeverity.Warning)
            {
                UnityEngine.Debug.LogWarning(entry);
            }
            else
            {
                UnityEngine.Debug.Log(entry);
            }
            
        }
    }
}