using System;

namespace Assets.Potentii.UniLog.Base
{
    [Serializable]
    public enum ELogSeverity : byte
    {
        Debug = 1,
        Trace = 2,
        Info = 3,
        Warning = 4,
        Error = 5,
    }
}