using NUnit.Framework;
using Potentii.UniLog.Core;
using UnityEngine;

namespace Potentii.UniLog.Tests.Editor.Core
{
    public class DefaultLoggerTest
    {

        private DefaultLogger _logger = new();
        
        
        [Test]
        public void LogInfo()
        {
            _logger.Info(new Component(), "XXX", "xxx");
        }
        

    }
}