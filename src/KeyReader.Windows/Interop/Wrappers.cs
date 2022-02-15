using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static KeyReader.Windows.Helpers;
using static KeyReader.Windows.Interop.RawCalls;

namespace KeyReader.Windows.Interop
{
    internal static class Wrappers
    {
        internal static unsafe bool MirrorTargetThreadMessageQueue(int targetThreadId, bool attach)
        {
            Debug.Assert(targetThreadId > 0);

            var thisThreadId = GetCurrentProcess().Threads[0].Id;

            var rc = AttachThreadInput((UInt32)thisThreadId, (UInt32)targetThreadId, attach);
            if (rc == false) { LastErrorHandling(); }

            return rc;
        }

        internal static unsafe bool GetCurrentThreadKeyboardState(byte* buffer)
        {
            var rc = GetKeyboardState(buffer);
            if (rc == false) { LastErrorHandling(); }

            return rc;
        }

        private static unsafe void LastErrorHandling()
        {
            // TODO: Error handling; FormatMessage call
            var errorCode = Marshal.GetLastWin32Error();
            Console.WriteLine($"Error code: {errorCode}");
        }
    }
}
