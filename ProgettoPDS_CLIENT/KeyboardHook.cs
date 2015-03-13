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
    /// Captures global keyboard events
    /// </summary>
    public class KeyboardHook : GlobalHook
    {

        #region Variables

        public enum Modificatore
        {
            C = 1,
            A = 0,
            S = 2,
            CA = 3,
            CS = 4,
            AS = 5,
            CAS = 6
        };

        public static Dictionary<string, string> VirtualKeys = new Dictionary<string, string>();

        #endregion

        #region Events

        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;
        public event KeyPressEventHandler KeyPress;

        #endregion

        #region Constructor

        public KeyboardHook()
        {
            InizializeDictionary();
            HookType = WH_KEYBOARD_LL;
        }

        #endregion

        #region Methods

        protected override int HookCallbackProcedure(int nCode, int wParam, IntPtr lParam)
        {

            bool handled = false;

            if (nCode >= 0 ) {

                KeyboardHookStruct keyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));

                // Is "Control" being held down?
                bool control = ((GetKeyState(VK_LCONTROL) & 0x80) != 0) || ((GetKeyState(VK_RCONTROL) & 0x80) != 0);

                // Is "Shift" being held down?
                bool shift = ((GetKeyState(VK_LSHIFT) & 0x80) != 0) || ((GetKeyState(VK_RSHIFT) & 0x80) != 0);

                // Is "Alt" being held down?
                bool alt = ((GetKeyState(VK_LALT) & 0x80) != 0) || ((GetKeyState(VK_RALT) & 0x80) != 0);

                // Is "CapsLock" on?
                bool capslock = (GetKeyState(VK_CAPITAL) != 0);

                // Create event using keycode and control/shift/alt values found above
                KeyEventArgs e = new KeyEventArgs(
                    (Keys)(
                        keyboardHookStruct.vkCode |
                        (control ? (int)Keys.Control : 0) |
                        (shift ? (int)Keys.Shift : 0) |
                        (alt ? (int)Keys.Alt : 0)
                        ));

                // Handle KeyDown and KeyUp events
                switch (wParam) {

                    case WM_KEYDOWN:
                    case WM_SYSKEYDOWN:
                        if (KeyDown != null) {
                            KeyDown(this, e);
                            handled = handled || e.Handled;
                        }

                        break;

                    case WM_KEYUP:
                    case WM_SYSKEYUP:
                        if (KeyUp != null) {
                            KeyUp(this, e);
                            handled = handled || e.Handled;
                        }

                        break;

                }

                // Handle KeyPress event
                if (wParam == WM_KEYDOWN && !handled && !e.SuppressKeyPress && KeyPress != null) {

                    byte[] keyState = new byte[256];
                    byte[] inBuffer = new byte[2];
                    GetKeyboardState(keyState);

                    if (ToAscii(keyboardHookStruct.vkCode, keyboardHookStruct.scanCode, keyState, inBuffer, keyboardHookStruct.flags) == 1) {
                        char key = (char)inBuffer[0];

                        if ((capslock ^ shift) && Char.IsLetter(key))
                            key = Char.ToUpper(key);

                        KeyPressEventArgs e2 = new KeyPressEventArgs(key);
                        KeyPress(this, e2);
                        handled = handled || e.Handled;

                    }

                }

            }

            if (handled)
                return 1;
            else
                return CallNextHookEx(hookID, nCode, wParam, lParam);

        }

        private void InizializeDictionary()
        {
            VirtualKeys["8"] = "VK_BACK";
            VirtualKeys["9"] = "VK_TAB";
            VirtualKeys["C"] = "VK_CLEAR";
            VirtualKeys["D"] = "VK_RETURN";
            VirtualKeys["13"] = "VK_PAUSE";
            VirtualKeys["14"] = "VK_CAPITAL";
            VirtualKeys["1B"] = "VK_ESCAPE";
            VirtualKeys["20"] = "VK_SPACE";
            VirtualKeys["23"] = "VK_END";
            VirtualKeys["24"] = "VK_HOME";
            VirtualKeys["25"] = "VK_LEFT";  
            VirtualKeys["26"] = "VK_UP";
            VirtualKeys["27"] = "VK_RIGHT";
            VirtualKeys["28"] = "VK_DOWN";
            VirtualKeys["29"] = "VK_SELECT";
            VirtualKeys["2A"] = "VK_PRINT";
            VirtualKeys["2C"] = "VK_SNAPSHOT";
            VirtualKeys["2D"] = "VK_INSERT";
            VirtualKeys["2E"] = "VK_DELETE";
            VirtualKeys["2F"] = "VK_HELP";
            VirtualKeys["60"] = "VK_NUMPAD0";
            VirtualKeys["61"] = "VK_NUMPAD1";
            VirtualKeys["62"] = "VK_NUMPAD2";
            VirtualKeys["63"] = "VK_NUMPAD3";
            VirtualKeys["64"] = "VK_NUMPAD4";
            VirtualKeys["65"] = "VK_NUMPAD5";
            VirtualKeys["66"] = "VK_NUMPAD6";
            VirtualKeys["67"] = "VK_NUMPAD7";
            VirtualKeys["68"] = "VK_NUMPAD8";
            VirtualKeys["69"] = "VK_NUMPAD9";
            VirtualKeys["6A"] = "VK_MULTIPLY";
            VirtualKeys["6B"] = "VK_ADD";
            VirtualKeys["6C"] = "VK_SEPARATOR";
            VirtualKeys["6D"] = "VK_SUBSTRACT";
            VirtualKeys["6E"] = "VK_DECIMAL";
            VirtualKeys["6F"] = "VK_DIVIDE";
            VirtualKeys["70"] = "VK_F1";
            VirtualKeys["71"] = "VK_F2";
            VirtualKeys["72"] = "VK_F3";
            VirtualKeys["73"] = "VK_F4";
            VirtualKeys["74"] = "VK_F5";
            VirtualKeys["75"] = "VK_F6";
            VirtualKeys["76"] = "VK_F7";
            VirtualKeys["77"] = "VK_F8";
            VirtualKeys["78"] = "VK_F9";
            VirtualKeys["79"] = "VK_F10";
            VirtualKeys["7A"] = "VK_F11";
            VirtualKeys["7B"] = "VK_F12";
            VirtualKeys["90"] = "VK_NUMLOCK";
            VirtualKeys["AC"] = "VK_BROWSER_HOME";
            VirtualKeys["AD"] = "VK_VOLUME_MUTE";
            VirtualKeys["AE"] = "VK_VOLUME_DOWN";
            VirtualKeys["AF"] = "VK_VOLUME_UP";

        }

        #endregion

    }
}
