using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace KeyReader.Windows
{
    internal static class Helpers
    {
        internal static bool GetFirstProcessByName(string processName, [NotNullWhen(true)] out Process? firstProcess)
        {
            var processes = Process.GetProcessesByName(processName);
            var found = processes.Length != 0;
            firstProcess = found ? processes[0] : null;
            return found;
        }

        internal static Process GetCurrentProcess() => Process.GetProcessById(Environment.ProcessId);
    }
}
