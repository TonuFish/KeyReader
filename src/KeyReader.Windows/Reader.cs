#define DEBUG_OUTPUT

using System;
using System.Diagnostics;
using static KeyReader.Windows.Helpers;
using static KeyReader.Windows.Interop.Wrappers;

namespace KeyReader.Windows
{
    public static class Reader
    {
        public static void ReadFromProcessByName(string processName)
        {
            if (GetFirstProcessByName(processName, out var targetProcess))
            {
                ReadFromProcess(targetProcess);
            }
            else
            {
                // TODO: Error handling; Process with supplied name not found
                return;
            }
        }

        public static void ReadFromProcessById(int processId)
        {
            Process process;
            try
            {
                process = Process.GetProcessById(processId);
            }
            catch (ArgumentException)
            {
                // TODO: Error handling; Process with supplied id not found
                return;
            }

            ReadFromProcess(process);
        }

        private static unsafe void ReadFromProcess(Process targetProcess)
        {
            const int HalfBufferWidth = 256;
            const int BufferWidth = HalfBufferWidth * 2;
            const int ActiveKeyValue = 128;

            // Take the root thread as it should(?) be guaranteed to recieve all keyboard events
            if (MirrorTargetThreadMessageQueue(targetProcess.Threads[0].Id, true) == false)
            {
                // TODO: Error handling; Failed to mirror target process
                return;
            }

            Span<byte> buffer = stackalloc byte[BufferWidth];
            fixed (byte* readPtr = &buffer[0], storPtr = &buffer[HalfBufferWidth])
            {
                ReadOnlySpan<byte> readBuffer = buffer[..HalfBufferWidth];
                Span<byte> storBuffer = buffer[HalfBufferWidth..];
                var hasReadKeyboard = false;

                while (true)
                {
                    // TODO: Exit condition + Detach message queue

                    readBuffer.CopyTo(storBuffer);
                    hasReadKeyboard = GetCurrentThreadKeyboardState(readPtr);

                    if (hasReadKeyboard)
                    {
                        // TODO: State key handling

                        /*
                            Check the state of Control,Shift,Alt,Windows,Language modifier keys
                            Check the capslock state (lower bit set stuff - 1)

                            Ignore keypresses that don't actually output a key

                            Translate modified keypresses into actual outputs (EG. 1 => !)

                            Account for different keyboard settings (EG. Test dvorak -> does it change what GetKeyboardState finds?)

                            -- I wonder if it's not easier to just feed keys to some form of keyboard layout to get the actual char press...
                         */

#if DEBUG_OUTPUT
                        // Basic keyboard reading dumped to stdout

                        for (var keyCode = 0; keyCode < HalfBufferWidth; ++keyCode)
                        {
                            // TODO: Make this bad lol
                            // Maybe swap to using vector128s directly? Avx.Xor? Sse Xor? One of the compares?

                            // (Get read|stor differences) (Keep differences shared with read) (Only keep key UP/DOWN events)
                            if (((readBuffer[keyCode] ^ storBuffer[keyCode]) & readBuffer[keyCode] & ActiveKeyValue)
                                == ActiveKeyValue)
                            {
                                // KeyName : KeyCode as Hex
                                Console.WriteLine($"{(Keys)keyCode,-20} : {keyCode:X2}");
                            }
                        }
#endif
                    }
                }
            }
        }
    }
}
