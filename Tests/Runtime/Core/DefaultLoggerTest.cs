using NUnit.Framework;
using Potentii.UniLog.Transports.UnityConsoleTransports;

namespace Potentii.UniLog.Tests.Runtime.Core
{
    public class DefaultLoggerTest
    {

        
        
        [Test]
        public void LogInfo()
        {
            UniLog.Core.UniLog.RegisterTransport(new UnityConsoleTransport());
            UniLog.Core.UniLog.Info(null, "XXX", "xxx", ("key1","value1"), ("key2","value2"));
        }
        

    }
}