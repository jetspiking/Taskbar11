using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Taskbar11.Controllers
{
    public static class ApplicationUtilities
    {
        private const String ProcessNameExplorer = "explorer";

        /// <summary>
        /// Forces the explorer.exe process to restart by killing it (the UI needs to reload to display the changes). 
        /// </summary>
        public static void RestartExplorer()
        {
            Process p = new Process();
            foreach (Process process in Process.GetProcesses())
                if (process.ProcessName == ProcessNameExplorer)
                    process.Kill();
        }

    }
}
