using Assets.Potentii.UniLog.Base;

namespace Assets.Potentii.UniLog.Transports.Core
{
    public interface ILogTransport
    {
        void Transport(ELogSeverity severity, string entry);
    }
}