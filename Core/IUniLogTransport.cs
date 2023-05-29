namespace Potentii.UniLog.Core
{
    public interface IUniLogTransport
    {
        void Transport(ELogSeverity severity, string entry);
    }
}