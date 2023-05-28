using System;
using System.IO;
using Assets.Potentii.UniLog.Base;
using Assets.Potentii.UniLog.Transports.Core;

namespace Assets.Potentii.UniLog.Transports.FileTransports
{
    public class FileTransport : ILogTransport
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