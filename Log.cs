using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    public class Log
    {
        public string FilePath { get; set; }
        public Log() 
        {
            string workingDirectory = Environment.CurrentDirectory;
            this.FilePath = Path.Combine(Directory.GetParent(workingDirectory).Parent.Parent.FullName, "log.txt");
            if (!File.Exists(this.FilePath))
                File.Create(this.FilePath);
        }
        public void LogInfo(string message) 
        { 
            using (StreamWriter sw = File.AppendText(this.FilePath))
            {
                sw.WriteLine(message);                
            }
            Thread.Sleep(500);
        }

        public void LogFlush(ref string logBatch)
        {
            this.LogInfo(logBatch);
            logBatch = "";
        }
    }
}
