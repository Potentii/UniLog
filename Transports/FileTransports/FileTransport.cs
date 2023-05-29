using System;
using System.IO;
using Potentii.UniLog.Core;

namespace Potentii.UniLog.Transports.FileTransports
{
    public class FileTransport : IUniLogTransport
    {
        public string Directory { get; }

        
        public FileTransport(string directory)
        {
            Directory = directory;
        }

        
        /// <inheritdoc />
        public void Transport(ELogSeverity severity, string entry)
        {
            using var w = File.AppendText(Path.GetFullPath(Directory + Path.DirectorySeparatorChar + GetLogFileNameForToday()));
            w.Write(entry + "\n");
        }


        public string GetLogFileNameForToday()
        {
            return DateTime.UtcNow.ToString("yyyy-MM-dd") + ".log";
        }
    }
}