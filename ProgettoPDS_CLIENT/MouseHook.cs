using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ProgettoPDS_CLIENT
{
    /// <summary>
    /// Captures global mouse events
    /// </summary>
    public class MouseHook : GlobalHook
    {

        #region MouseEventType Enum

        private enum MouseEventType
        {
            None,
            MouseDown,
            MouseUp,
            DoubleClick,
            MouseWheel,
            MouseMove
        }

        #endregion

        #region Events

        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MouseUp;
        public event MouseEventHandler MouseMove;
        public event MouseEventHandler MouseWheel;

        public event EventHandler Click;
        public event EventHandler DoubleClick;

        #endregion

        #region Constructor

        public MouseHook()
        {
            HookType = WH_MOUSE_LL;
        }

        #endregion

        #region Methods

        protected override int HookCallbackProcedure(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0) {

                MouseLLHookStruct mouseHookStruct = (MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseLLHookStruct));

                MouseButtons button = GetButton(wParam);
                MouseEventType eventType = GetEventType(wParam);

                int nClicks;

                if (eventType == MouseEventType.MouseMove || eventType == MouseEventType.MouseWheel)
                    nClicks = 0;
                else if (eventType == MouseEventType.DoubleClick)
                    nClicks = 2;
                else
                    nClicks = 1;

                MouseEventArgs e = new MouseEventArgs( button, nClicks, mouseHookStruct.pt.x, mouseHookStruct.pt.y,
                    (eventType == MouseEventType.MouseWheel ? (short)((mouseHookStruct.mouseData >> 16) & 0xffff) : 0));

                if (button == MouseButtons.Right && mouseHookStruct.flags != 0)
                    eventType = MouseEventType.None;

                switch (eventType) {
                    case MouseEventType.MouseDown:
                        if (MouseDown != null) 
                            MouseDown(this, e);

                        break;

                    case MouseEventType.MouseUp:
                        if (Click != null)
                            Click(this, new EventArgs());
                        
                        if (MouseUp != null)                        
                            MouseUp(this, e);

                        break;

                    case MouseEventType.DoubleClick:
                        if (DoubleClick != null)
                            DoubleClick(this, new EventArgs());

                        break;

                    case MouseEventType.MouseWheel:
                        if (MouseWheel != null)
                            MouseWheel(this, e);

                        break;

                    case MouseEventType.MouseMove:
                        if (MouseMove != null)
                            MouseMove(this, e);

                        break;
                }
                
            }

            return CallNextHookEx(hookID, nCode, wParam, lParam);
        
        }

        private MouseButtons GetButton(Int32 wParam)
        {

            switch (wParam) {
                case WM_LBUTTONDOWN:
                case WM_LBUTTONUP:
                case WM_LBUTTONDBLCLK:
                    return MouseButtons.Left;
                case WM_RBUTTONDOWN:
                case WM_RBUTTONUP:
                case WM_RBUTTONDBLCLK:
                    return MouseButtons.Right;
                case WM_MBUTTONDOWN:
                case WM_MBUTTONUP:
                case WM_MBUTTONDBLCLK:
                    return MouseButtons.Middle;
                default:
                    return MouseButtons.None;
            }

        }

        private MouseEventType GetEventType(Int32 wParam)
        {

            switch (wParam) {
                case WM_LBUTTONDOWN:
                case WM_RBUTTONDOWN:
                case WM_MBUTTONDOWN:
                    return MouseEventType.MouseDown;
                case WM_LBUTTONUP:
                case WM_RBUTTONUP:
                case WM_MBUTTONUP:
                    return MouseEventType.MouseUp;
                case WM_LBUTTONDBLCLK:
                case WM_RBUTTONDBLCLK:
                case WM_MBUTTONDBLCLK:
                    return MouseEventType.DoubleClick;
                case WM_MOUSEWHEEL:
                    return MouseEventType.MouseWheel;
                case WM_MOUSEMOVE:
                    return MouseEventType.MouseMove;
                default:
                    return MouseEventType.None;
            }
        }

        #endregion

    }
}
