﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;

namespace ProgettoPDS_CLIENT
{
    /// <summary>
    /// Abstract base class for Mouse and Keyboard hooks
    /// </summary>
    public abstract class GlobalHook
    {
        protected delegate int HookProc(int nCode, int wParam, IntPtr lParam);

        #region Private Variables

        protected int HookType;
        protected int hookID;
        protected bool isStarted;
        protected HookProc HookCallback;

        #endregion

        #region Windows API Code

        protected const int WH_MOUSE_LL = 14;
        protected const int WH_KEYBOARD_LL = 13;

        protected const int WH_MOUSE = 7;
        protected const int WH_KEYBOARD = 2;
        protected const int WM_MOUSEMOVE = 0x200;
        protected const int WM_LBUTTONDOWN = 0x201;
        protected const int WM_RBUTTONDOWN = 0x204;
        protected const int WM_MBUTTONDOWN = 0x207;
        protected const int WM_LBUTTONUP = 0x202;
        protected const int WM_RBUTTONUP = 0x205;
        protected const int WM_MBUTTONUP = 0x208;
        protected const int WM_LBUTTONDBLCLK = 0x203;
        protected const int WM_RBUTTONDBLCLK = 0x206;
        protected const int WM_MBUTTONDBLCLK = 0x209;
        protected const int WM_MOUSEWHEEL = 0x020A;
        protected const int WM_KEYDOWN = 0x100;
        protected const int WM_KEYUP = 0x101;
        protected const int WM_SYSKEYDOWN = 0x104;
        protected const int WM_SYSKEYUP = 0x105;

        protected const byte VK_SHIFT = 0x10;
        protected const byte VK_CAPITAL = 0x14;
        protected const byte VK_NUMLOCK = 0x90;

        protected const byte VK_LSHIFT = 0xA0;
        protected const byte VK_RSHIFT = 0xA1;
        protected const byte VK_LCONTROL = 0xA2;
        protected const byte VK_RCONTROL = 0x3;
        protected const byte VK_LALT = 0xA4;
        protected const byte VK_RALT = 0xA5;
        protected const byte VK_PAUSE = 0x13;
        protected const byte VK_BACK = 0x08;
        protected const byte VK_TAB = 0x09;
        internal const byte VK_M = 0x4D;
        internal const byte VK_S = 0x53;
        internal const byte VK_R = 0x52;
        internal const byte VK_UP = 0x26;
        internal const byte VK_DOWN = 0x28;
        internal const byte VK_C = 0x43;

        protected const byte LLKHF_ALTDOWN = 0x20;

        [StructLayout(LayoutKind.Sequential)]
        protected class POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        protected class MouseHookStruct
        {
            public POINT pt;
            public int hwnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        protected class MouseLLHookStruct
        {
            public POINT pt;
            public int mouseData;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        protected class KeyboardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        protected static extern int SetWindowsHookEx( int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        protected static extern int UnhookWindowsHookEx(int idHook);


        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        protected static extern int CallNextHookEx( int idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("user32")]
        protected static extern int ToAscii( int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);

        [DllImport("user32")]
        protected static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        protected static extern short GetKeyState(int vKey);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        #endregion

        #region Properties

        public bool IsStarted
        {
            get { return isStarted; }
        }

        #endregion

        #region Methods

        public static bool IsActive(IntPtr handle)
        {
            IntPtr activeHandle = GetForegroundWindow();
            return (activeHandle == handle);
        }

        public void Install() 
        {
            if (!isStarted && HookType != 0) {
                HookCallback = new HookProc(HookCallbackProcedure);
                ProcessModule module = Process.GetCurrentProcess().MainModule;

                hookID = SetWindowsHookEx(HookType, HookCallback, GetModuleHandle(module.ModuleName), 0);

                if ( hookID != 0)
                    isStarted = true;

            }
        }

        public void Unistall() 
        {
            if (isStarted) {

                UnhookWindowsHookEx(hookID);

                isStarted = false;
            }    
        }

        protected virtual int HookCallbackProcedure(int nCode, int wParam, IntPtr lParam)
        {
            // This method must be overriden by each extending hook
            return 0;
        }

        #endregion

    }
}
