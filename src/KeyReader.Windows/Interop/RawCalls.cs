using System;
using System.Runtime.InteropServices;

namespace KeyReader.Windows.Interop
{
    internal static class RawCalls
    {
        /// <summary>
        /// Copies the status of the 256 virtual keys to the specified buffer.<br/>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeyboardstate
        /// </summary>
        /// <param name="lpKeyState">
        /// <c>[OUT]</c> The 256-byte array that receives the status data for each virtual key.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.<br/>
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("User32.dll", SetLastError = true)]
        internal static unsafe extern Boolean GetKeyboardState(Byte* lpKeyState);

        /// <summary>
        /// Attaches or detaches the input processing mechanism of one thread to that of another thread.<br/>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-attachthreadinput
        /// </summary>
        /// <param name="idAttach">
        /// <c>[IN]</c> The identifier of the thread to be attached to another thread. The thread to be attached cannot be a system thread.
        /// </param>
        /// <param name="idAttachTo">
        /// <c>[IN]</c>The identifier of the thread to which idAttach will be attached. This thread cannot be a system thread.<br/>
        /// A thread cannot attach to itself. Therefore, idAttachTo cannot equal idAttach.
        /// </param>
        /// <param name="fAttach">
        /// <c>[IN]</c> If this parameter is TRUE, the two threads are attached. If the parameter is FALSE, the threads are detached.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.<br/>
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("User32.dll", SetLastError = true)]
        internal static extern bool AttachThreadInput(UInt32 idAttach, UInt32 idAttachTo, Boolean fAttach);

        #region Work in Progress

        // This may not be strict necessary at all given System.Diagnostics.Interop.Errors exists
        // https://source.dot.net/#System.Diagnostics.Process/Interop.Errors.cs,1a88cd14436b5d01
        // This has a decent subset of the error list, but it'd be good practice to implement FormatMessage regardless

        // https://docs.microsoft.com/en-us/windows/win32/debug/system-error-codes--0-499
        // https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-formatmessage
        // https://www.pinvoke.net/default.aspx/kernel32.formatmessage
        //    No longer associated with RedGate + completely dead... RIP PInvoke.net
        /*
            DWORD FormatMessage(
              [in]           DWORD   dwFlags,
              [in, optional] LPCVOID lpSource,
              [in]           DWORD   dwMessageId,
              [in]           DWORD   dwLanguageId,
              [out]          LPTSTR  lpBuffer,
              [in]           DWORD   nSize,
              [in, optional] va_list *Arguments
            );
         */
        //[DllImport("???.dll", SetLastError = true)]
        //internal static extern UInt32 FormatMessage(
        //    UInt32 dwFlags,
        //    ___ lpSource,
        //    UInt32 dwMessageId,
        //    UInt32 dwLanguageId,
        //    ___ lpBuffer,
        //    UInt32 nSize,
        //    ___? va_list);

        #endregion Work in Progress
    }
}
