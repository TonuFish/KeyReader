using System;
using System.Diagnostics;

namespace KeyReader.Console
{
    internal static class Program
    {
        private static void Main()
        {
            if (OperatingSystem.IsWindowsVersionAtLeast(10))
            {
                System.Console.WriteLine("Starting modern windows reader...");
                var process = Process.Start("notepad.exe");
                KeyReader.Windows.Reader.ReadFromProcessById(process.Id);
            }
        }
    }
}
