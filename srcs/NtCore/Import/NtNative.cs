﻿﻿using System.Runtime.InteropServices;

namespace NtCore.Import
{
    internal static class NtNative
    {
        private const string LibraryName = "NtNative.dll";

        public delegate void PacketCallback(string packet);
        
        [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern void SetRecvCallback(PacketCallback callback);

        [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern void SetSendCallback(PacketCallback callback);

        [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern void Setup(uint moduleBase, uint moduleSize);

        [DllImport(LibraryName, EntryPoint = "SendPacket", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern void SendPacket(string packet);

        [DllImport(LibraryName, EntryPoint = "RecvPacket", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern void RecvPacket(string packet);
    }
}