using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace xLevelReward
{
    public static class MemoryManager
    {
        #region Dll Imports

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess
        (
             ProcessAccessFlags processAccess,
             bool bInheritHandle,
             int processId
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle
        (
            IntPtr hObject
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory
        (
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            [Out] byte[] lpBuffer,
            int dwSize,
            out IntPtr lpNumberOfBytesRead
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory
        (
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            byte[] lpBuffer,
            int nSize,
            out IntPtr lpNumberOfBytesWritten
        );

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx
        (
            IntPtr hProcess,
            IntPtr lpAddress,
            IntPtr dwSize,
            AllocationType flAllocationType,
            MemoryProtection flProtect
        );

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool VirtualFreeEx
        (
            IntPtr hProcess,
            IntPtr lpAddress,
            int dwSize,
            FreeType dwFreeType
        );

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread
        (
            IntPtr hProcess,
            IntPtr lpThreadAttributes,
            uint dwStackSize,
            IntPtr lpStartAddress,
            IntPtr lpParameter,
            uint dwCreationFlags,
            out IntPtr lpThreadId
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        #endregion

        #region Flags

        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }

        [Flags]
        public enum AllocationType
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            Physical = 0x400000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            LargePages = 0x20000000
        }

        [Flags]
        public enum MemoryProtection
        {
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            GuardModifierflag = 0x100,
            NoCacheModifierflag = 0x200,
            WriteCombineModifierflag = 0x400
        }

        [Flags]
        public enum FreeType
        {
            Decommit = 0x4000,
            Release = 0x8000,
        }

        #endregion

        public static int ProcessID = 0;
        private static IntPtr hProcess = IntPtr.Zero;

        public static int AllocateMemory(int Length)
        {
            if (hProcess == IntPtr.Zero)
                hProcess = OpenProcess(ProcessAccessFlags.All, false, ProcessID);

            IntPtr Pointer = IntPtr.Zero;
            Pointer = VirtualAllocEx(hProcess, IntPtr.Zero, (IntPtr)Length, AllocationType.Commit, MemoryProtection.ExecuteReadWrite);

            return (int)Pointer;
        }

        public static bool FreeMemory(int Address, int Length)
        {
            if (hProcess == IntPtr.Zero)
                hProcess = OpenProcess(ProcessAccessFlags.All, false, ProcessID);

            bool Success = false;
            Success = VirtualFreeEx(hProcess, (IntPtr)Address, Length, FreeType.Decommit);

            return Success;
        }

        public static byte[] ReadProcessMemory(int Address, int Length)
        {
            if (hProcess == IntPtr.Zero)
                hProcess = OpenProcess(ProcessAccessFlags.All, false, ProcessID);

            byte[] Buffer = new byte[Length];
            IntPtr bytesRead = IntPtr.Zero;
            ReadProcessMemory(hProcess, (IntPtr)Address, Buffer, Length, out bytesRead);

            return Buffer;
        }

        public static bool WriteProcessMemory(int Address, byte[] Memory)
        {
            if (hProcess == IntPtr.Zero)
                hProcess = OpenProcess(ProcessAccessFlags.All, false, ProcessID);

            bool Success = false;
            IntPtr bytesWritten = IntPtr.Zero;
            Success = WriteProcessMemory(hProcess, (IntPtr)Address, Memory, Memory.Length, out bytesWritten);

            return Success;
        }

        public static bool CreateRemoteThread(int Address)
        {
            if (hProcess == IntPtr.Zero)
                hProcess = OpenProcess(ProcessAccessFlags.All, false, ProcessID);

            IntPtr ThreadID;
            IntPtr hThread = CreateRemoteThread(hProcess, IntPtr.Zero, 0, (IntPtr)Address, IntPtr.Zero, 0, out ThreadID);

            bool ReturnValue = hThread != IntPtr.Zero;

            CloseHandle(hThread);

            return ReturnValue;
        }
    }
}
