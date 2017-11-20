using System;

namespace LogAn
{
    public class LogAnalyzer
    {
        private IExtensionManager manager;

        public LogAnalyzer(IExtensionManager mgr)
        {
            manager = mgr;
        }
        public bool WasLastFileNameValid { get; set; }
        // public bool IsValidLogFileName(string fileName)
        // {
        //     WasLastFileNameValid = false;

        //     if (string.IsNullOrEmpty(fileName))
        //     {
        //         throw new Exception("filename has to be provided");
        //     }
            
        //     if (!fileName.EndsWith(".SLF", StringComparison.CurrentCultureIgnoreCase))
        //     {
        //         return false;
        //     }

        //     WasLastFileNameValid = true;
        //     return true;
        // }

        public bool IsValidLogFileName(string fileName)
        {
            return manager.IsValid(fileName);
        }
    }
}
