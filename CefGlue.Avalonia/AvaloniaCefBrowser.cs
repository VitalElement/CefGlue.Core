using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Avalonia;
using Xilium.CefGlue;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using Avalonia.Input;
using Avalonia.VisualTree;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using System.Runtime.InteropServices;
using Avalonia.Platform;
using System.Security;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace CefGlue.Avalonia
{
    public static class KeyInterop
    {
        partial class NativeMethods
        {
            public const int VK_CANCEL = 0x03;

            public const int VK_BACK = 0x08;

            public const int VK_CLEAR = 0x0C;

            public const int VK_RETURN = 0x0D;

            public const int VK_PAUSE = 0x13;

            public const int VK_CAPITAL = 0x14;

            public const int VK_KANA = 0x15;

            public const int VK_HANGEUL = 0x15;

            public const int VK_HANGUL = 0x15;

            public const int VK_JUNJA = 0x17;

            public const int VK_FINAL = 0x18;

            public const int VK_HANJA = 0x19;

            public const int VK_KANJI = 0x19;

            public const int VK_ESCAPE = 0x1B;

            public const int VK_CONVERT = 0x1C;

            public const int VK_NONCONVERT = 0x1D;

            public const int VK_ACCEPT = 0x1E;

            public const int VK_MODECHANGE = 0x1F;

            public const int VK_SPACE = 0x20;

            public const int VK_PRIOR = 0x21;

            public const int VK_NEXT = 0x22;

            public const int VK_END = 0x23;

            public const int VK_HOME = 0x24;

            public const int VK_LEFT = 0x25;

            public const int VK_UP = 0x26;

            public const int VK_RIGHT = 0x27;

            public const int VK_DOWN = 0x28;

            public const int VK_SELECT = 0x29;

            public const int VK_PRINT = 0x2A;

            public const int VK_EXECUTE = 0x2B;

            public const int VK_SNAPSHOT = 0x2C;

            public const int VK_INSERT = 0x2D;

            public const int VK_DELETE = 0x2E;

            public const int VK_HELP = 0x2F;

            public const int VK_0 = 0x30;

            public const int VK_1 = 0x31;

            public const int VK_2 = 0x32;

            public const int VK_3 = 0x33;

            public const int VK_4 = 0x34;

            public const int VK_5 = 0x35;

            public const int VK_6 = 0x36;

            public const int VK_7 = 0x37;

            public const int VK_8 = 0x38;

            public const int VK_9 = 0x39;

            public const int VK_A = 0x41;

            public const int VK_B = 0x42;

            public const int VK_C = 0x43;

            public const int VK_D = 0x44;

            public const int VK_E = 0x45;

            public const int VK_F = 0x46;

            public const int VK_G = 0x47;

            public const int VK_H = 0x48;

            public const int VK_I = 0x49;

            public const int VK_J = 0x4A;

            public const int VK_K = 0x4B;

            public const int VK_L = 0x4C;

            public const int VK_M = 0x4D;

            public const int VK_N = 0x4E;

            public const int VK_O = 0x4F;

            public const int VK_P = 0x50;

            public const int VK_Q = 0x51;

            public const int VK_R = 0x52;

            public const int VK_S = 0x53;

            public const int VK_T = 0x54;

            public const int VK_U = 0x55;

            public const int VK_V = 0x56;

            public const int VK_W = 0x57;

            public const int VK_X = 0x58;

            public const int VK_Y = 0x59;

            public const int VK_Z = 0x5A;

            public const int VK_LWIN = 0x5B;

            public const int VK_RWIN = 0x5C;

            public const int VK_APPS = 0x5D;

            public const int VK_POWER = 0x5E;

            public const int VK_SLEEP = 0x5F;

            public const int VK_NUMPAD0 = 0x60;

            public const int VK_NUMPAD1 = 0x61;

            public const int VK_NUMPAD2 = 0x62;

            public const int VK_NUMPAD3 = 0x63;

            public const int VK_NUMPAD4 = 0x64;

            public const int VK_NUMPAD5 = 0x65;

            public const int VK_NUMPAD6 = 0x66;

            public const int VK_NUMPAD7 = 0x67;

            public const int VK_NUMPAD8 = 0x68;

            public const int VK_NUMPAD9 = 0x69;

            public const int VK_MULTIPLY = 0x6A;

            public const int VK_ADD = 0x6B;

            public const int VK_SEPARATOR = 0x6C;

            public const int VK_SUBTRACT = 0x6D;

            public const int VK_DECIMAL = 0x6E;

            public const int VK_DIVIDE = 0x6F;

            public const int VK_F1 = 0x70;

            public const int VK_F2 = 0x71;

            public const int VK_F3 = 0x72;

            public const int VK_F4 = 0x73;

            public const int VK_F5 = 0x74;

            public const int VK_F6 = 0x75;

            public const int VK_F7 = 0x76;

            public const int VK_F8 = 0x77;

            public const int VK_F9 = 0x78;

            public const int VK_F10 = 0x79;

            public const int VK_F11 = 0x7A;

            public const int VK_F12 = 0x7B;

            public const int VK_F13 = 0x7C;

            public const int VK_F14 = 0x7D;

            public const int VK_F15 = 0x7E;

            public const int VK_F16 = 0x7F;

            public const int VK_F17 = 0x80;

            public const int VK_F18 = 0x81;

            public const int VK_F19 = 0x82;

            public const int VK_F20 = 0x83;

            public const int VK_F21 = 0x84;

            public const int VK_F22 = 0x85;

            public const int VK_F23 = 0x86;

            public const int VK_F24 = 0x87;

            public const int VK_NUMLOCK = 0x90;

            public const int VK_SCROLL = 0x91;


            public const int VK_RSHIFT = 0xA1;

            public const int VK_BROWSER_BACK = 0xA6;

            public const int VK_BROWSER_FORWARD = 0xA7;

            public const int VK_BROWSER_REFRESH = 0xA8;

            public const int VK_BROWSER_STOP = 0xA9;

            public const int VK_BROWSER_SEARCH = 0xAA;

            public const int VK_BROWSER_FAVORITES = 0xAB;

            public const int VK_BROWSER_HOME = 0xAC;

            public const int VK_VOLUME_MUTE = 0xAD;

            public const int VK_VOLUME_DOWN = 0xAE;

            public const int VK_VOLUME_UP = 0xAF;

            public const int VK_MEDIA_NEXT_TRACK = 0xB0;

            public const int VK_MEDIA_PREV_TRACK = 0xB1;

            public const int VK_MEDIA_STOP = 0xB2;

            public const int VK_MEDIA_PLAY_PAUSE = 0xB3;

            public const int VK_LAUNCH_MAIL = 0xB4;

            public const int VK_LAUNCH_MEDIA_SELECT = 0xB5;

            public const int VK_LAUNCH_APP1 = 0xB6;

            public const int VK_LAUNCH_APP2 = 0xB7;

            public const int VK_PROCESSKEY = 0xE5;

            public const int VK_PACKET = 0xE7;

            public const int VK_ATTN = 0xF6;

            public const int VK_CRSEL = 0xF7;

            public const int VK_EXSEL = 0xF8;

            public const int VK_EREOF = 0xF9;

            public const int VK_PLAY = 0xFA;

            public const int VK_ZOOM = 0xFB;

            public const int VK_NONAME = 0xFC;

            public const int VK_PA1 = 0xFD;

            public const int VK_OEM_CLEAR = 0xFE;

            public const int VK_TAB = 0x09;
            public const int VK_SHIFT = 0x10;
            public const int VK_CONTROL = 0x11;
            public const int VK_MENU = 0x12;

            public const int VK_LSHIFT = 0xA0;
            public const int VK_RMENU = 0xA5;
            public const int VK_LMENU = 0xA4;
            public const int VK_LCONTROL = 0xA2;
            public const int VK_RCONTROL = 0xA3;
            public const int VK_LBUTTON = 0x01;
            public const int VK_RBUTTON = 0x02;
            public const int VK_MBUTTON = 0x04;
            public const int VK_XBUTTON1 = 0x05;
            public const int VK_XBUTTON2 = 0x06;

            public const int VK_OEM_1 = 0xBA;
            public const int VK_OEM_PLUS = 0xBB;
            public const int VK_OEM_COMMA = 0xBC;
            public const int VK_OEM_MINUS = 0xBD;
            public const int VK_OEM_PERIOD = 0xBE;
            public const int VK_OEM_2 = 0xBF;
            public const int VK_OEM_3 = 0xC0;
            public const int VK_C1 = 0xC1;   // Brazilian ABNT_C1 key (not defined in winuser.h).
            public const int VK_C2 = 0xC2;   // Brazilian ABNT_C2 key (not defined in winuser.h).
            public const int VK_OEM_4 = 0xDB;
            public const int VK_OEM_5 = 0xDC;
            public const int VK_OEM_6 = 0xDD;
            public const int VK_OEM_7 = 0xDE;
            public const int VK_OEM_8 = 0xDF;
            public const int VK_OEM_AX = 0xE1;
            public const int VK_OEM_102 = 0xE2;
            public const int VK_OEM_RESET = 0xE9;
            public const int VK_OEM_JUMP = 0xEA;
            public const int VK_OEM_PA1 = 0xEB;
            public const int VK_OEM_PA2 = 0xEC;
            public const int VK_OEM_PA3 = 0xED;
            public const int VK_OEM_WSCTRL = 0xEE;
            public const int VK_OEM_CUSEL = 0xEF;
            public const int VK_OEM_ATTN = 0xF0;
            public const int VK_OEM_FINISH = 0xF1;
            public const int VK_OEM_COPY = 0xF2;
            public const int VK_OEM_AUTO = 0xF3;
            public const int VK_OEM_ENLW = 0xF4;
            public const int VK_OEM_BACKTAB = 0xF5;


        }

        /// <summary>
        ///     Convert our Key enum into a Win32 VirtualKey.
        /// </summary>
        public static int VirtualKeyFromKey(Key key)
        {
            int virtualKey = 0;

            switch (key)
            {
                case Key.Cancel:
                    virtualKey = NativeMethods.VK_CANCEL;
                    break;

                case Key.Back:
                    virtualKey = NativeMethods.VK_BACK;
                    break;

                case Key.Tab:
                    virtualKey = NativeMethods.VK_TAB;
                    break;

                case Key.Clear:
                    virtualKey = NativeMethods.VK_CLEAR;
                    break;

                case Key.Return:
                    virtualKey = NativeMethods.VK_RETURN;
                    break;

                case Key.Pause:
                    virtualKey = NativeMethods.VK_PAUSE;
                    break;

                case Key.Capital:
                    virtualKey = NativeMethods.VK_CAPITAL;
                    break;

                case Key.KanaMode:
                    virtualKey = NativeMethods.VK_KANA;
                    break;

                case Key.JunjaMode:
                    virtualKey = NativeMethods.VK_JUNJA;
                    break;

                case Key.FinalMode:
                    virtualKey = NativeMethods.VK_FINAL;
                    break;

                case Key.KanjiMode:
                    virtualKey = NativeMethods.VK_KANJI;
                    break;

                case Key.Escape:
                    virtualKey = NativeMethods.VK_ESCAPE;
                    break;

                case Key.ImeConvert:
                    virtualKey = NativeMethods.VK_CONVERT;
                    break;

                case Key.ImeNonConvert:
                    virtualKey = NativeMethods.VK_NONCONVERT;
                    break;

                case Key.ImeAccept:
                    virtualKey = NativeMethods.VK_ACCEPT;
                    break;

                case Key.ImeModeChange:
                    virtualKey = NativeMethods.VK_MODECHANGE;
                    break;

                case Key.Space:
                    virtualKey = NativeMethods.VK_SPACE;
                    break;

                case Key.Prior:
                    virtualKey = NativeMethods.VK_PRIOR;
                    break;

                case Key.Next:
                    virtualKey = NativeMethods.VK_NEXT;
                    break;

                case Key.End:
                    virtualKey = NativeMethods.VK_END;
                    break;

                case Key.Home:
                    virtualKey = NativeMethods.VK_HOME;
                    break;

                case Key.Left:
                    virtualKey = NativeMethods.VK_LEFT;
                    break;

                case Key.Up:
                    virtualKey = NativeMethods.VK_UP;
                    break;

                case Key.Right:
                    virtualKey = NativeMethods.VK_RIGHT;
                    break;

                case Key.Down:
                    virtualKey = NativeMethods.VK_DOWN;
                    break;

                case Key.Select:
                    virtualKey = NativeMethods.VK_SELECT;
                    break;

                case Key.Print:
                    virtualKey = NativeMethods.VK_PRINT;
                    break;

                case Key.Execute:
                    virtualKey = NativeMethods.VK_EXECUTE;
                    break;

                case Key.Snapshot:
                    virtualKey = NativeMethods.VK_SNAPSHOT;
                    break;

                case Key.Insert:
                    virtualKey = NativeMethods.VK_INSERT;
                    break;

                case Key.Delete:
                    virtualKey = NativeMethods.VK_DELETE;
                    break;

                case Key.Help:
                    virtualKey = NativeMethods.VK_HELP;
                    break;

                case Key.D0:
                    virtualKey = NativeMethods.VK_0;
                    break;

                case Key.D1:
                    virtualKey = NativeMethods.VK_1;
                    break;

                case Key.D2:
                    virtualKey = NativeMethods.VK_2;
                    break;

                case Key.D3:
                    virtualKey = NativeMethods.VK_3;
                    break;

                case Key.D4:
                    virtualKey = NativeMethods.VK_4;
                    break;

                case Key.D5:
                    virtualKey = NativeMethods.VK_5;
                    break;

                case Key.D6:
                    virtualKey = NativeMethods.VK_6;
                    break;

                case Key.D7:
                    virtualKey = NativeMethods.VK_7;
                    break;

                case Key.D8:
                    virtualKey = NativeMethods.VK_8;
                    break;

                case Key.D9:
                    virtualKey = NativeMethods.VK_9;
                    break;

                case Key.A:
                    virtualKey = NativeMethods.VK_A;
                    break;

                case Key.B:
                    virtualKey = NativeMethods.VK_B;
                    break;

                case Key.C:
                    virtualKey = NativeMethods.VK_C;
                    break;

                case Key.D:
                    virtualKey = NativeMethods.VK_D;
                    break;

                case Key.E:
                    virtualKey = NativeMethods.VK_E;
                    break;

                case Key.F:
                    virtualKey = NativeMethods.VK_F;
                    break;

                case Key.G:
                    virtualKey = NativeMethods.VK_G;
                    break;

                case Key.H:
                    virtualKey = NativeMethods.VK_H;
                    break;

                case Key.I:
                    virtualKey = NativeMethods.VK_I;
                    break;

                case Key.J:
                    virtualKey = NativeMethods.VK_J;
                    break;

                case Key.K:
                    virtualKey = NativeMethods.VK_K;
                    break;

                case Key.L:
                    virtualKey = NativeMethods.VK_L;
                    break;

                case Key.M:
                    virtualKey = NativeMethods.VK_M;
                    break;

                case Key.N:
                    virtualKey = NativeMethods.VK_N;
                    break;

                case Key.O:
                    virtualKey = NativeMethods.VK_O;
                    break;

                case Key.P:
                    virtualKey = NativeMethods.VK_P;
                    break;

                case Key.Q:
                    virtualKey = NativeMethods.VK_Q;
                    break;

                case Key.R:
                    virtualKey = NativeMethods.VK_R;
                    break;

                case Key.S:
                    virtualKey = NativeMethods.VK_S;
                    break;

                case Key.T:
                    virtualKey = NativeMethods.VK_T;
                    break;

                case Key.U:
                    virtualKey = NativeMethods.VK_U;
                    break;

                case Key.V:
                    virtualKey = NativeMethods.VK_V;
                    break;

                case Key.W:
                    virtualKey = NativeMethods.VK_W;
                    break;

                case Key.X:
                    virtualKey = NativeMethods.VK_X;
                    break;

                case Key.Y:
                    virtualKey = NativeMethods.VK_Y;
                    break;

                case Key.Z:
                    virtualKey = NativeMethods.VK_Z;
                    break;

                case Key.LWin:
                    virtualKey = NativeMethods.VK_LWIN;
                    break;

                case Key.RWin:
                    virtualKey = NativeMethods.VK_RWIN;
                    break;

                case Key.Apps:
                    virtualKey = NativeMethods.VK_APPS;
                    break;

                case Key.Sleep:
                    virtualKey = NativeMethods.VK_SLEEP;
                    break;

                case Key.NumPad0:
                    virtualKey = NativeMethods.VK_NUMPAD0;
                    break;

                case Key.NumPad1:
                    virtualKey = NativeMethods.VK_NUMPAD1;
                    break;

                case Key.NumPad2:
                    virtualKey = NativeMethods.VK_NUMPAD2;
                    break;

                case Key.NumPad3:
                    virtualKey = NativeMethods.VK_NUMPAD3;
                    break;

                case Key.NumPad4:
                    virtualKey = NativeMethods.VK_NUMPAD4;
                    break;

                case Key.NumPad5:
                    virtualKey = NativeMethods.VK_NUMPAD5;
                    break;

                case Key.NumPad6:
                    virtualKey = NativeMethods.VK_NUMPAD6;
                    break;

                case Key.NumPad7:
                    virtualKey = NativeMethods.VK_NUMPAD7;
                    break;

                case Key.NumPad8:
                    virtualKey = NativeMethods.VK_NUMPAD8;
                    break;

                case Key.NumPad9:
                    virtualKey = NativeMethods.VK_NUMPAD9;
                    break;

                case Key.Multiply:
                    virtualKey = NativeMethods.VK_MULTIPLY;
                    break;

                case Key.Add:
                    virtualKey = NativeMethods.VK_ADD;
                    break;

                case Key.Separator:
                    virtualKey = NativeMethods.VK_SEPARATOR;
                    break;

                case Key.Subtract:
                    virtualKey = NativeMethods.VK_SUBTRACT;
                    break;

                case Key.Decimal:
                    virtualKey = NativeMethods.VK_DECIMAL;
                    break;

                case Key.Divide:
                    virtualKey = NativeMethods.VK_DIVIDE;
                    break;

                case Key.F1:
                    virtualKey = NativeMethods.VK_F1;
                    break;

                case Key.F2:
                    virtualKey = NativeMethods.VK_F2;
                    break;

                case Key.F3:
                    virtualKey = NativeMethods.VK_F3;
                    break;

                case Key.F4:
                    virtualKey = NativeMethods.VK_F4;
                    break;

                case Key.F5:
                    virtualKey = NativeMethods.VK_F5;
                    break;

                case Key.F6:
                    virtualKey = NativeMethods.VK_F6;
                    break;

                case Key.F7:
                    virtualKey = NativeMethods.VK_F7;
                    break;

                case Key.F8:
                    virtualKey = NativeMethods.VK_F8;
                    break;

                case Key.F9:
                    virtualKey = NativeMethods.VK_F9;
                    break;

                case Key.F10:
                    virtualKey = NativeMethods.VK_F10;
                    break;

                case Key.F11:
                    virtualKey = NativeMethods.VK_F11;
                    break;

                case Key.F12:
                    virtualKey = NativeMethods.VK_F12;
                    break;

                case Key.F13:
                    virtualKey = NativeMethods.VK_F13;
                    break;

                case Key.F14:
                    virtualKey = NativeMethods.VK_F14;
                    break;

                case Key.F15:
                    virtualKey = NativeMethods.VK_F15;
                    break;

                case Key.F16:
                    virtualKey = NativeMethods.VK_F16;
                    break;

                case Key.F17:
                    virtualKey = NativeMethods.VK_F17;
                    break;

                case Key.F18:
                    virtualKey = NativeMethods.VK_F18;
                    break;

                case Key.F19:
                    virtualKey = NativeMethods.VK_F19;
                    break;

                case Key.F20:
                    virtualKey = NativeMethods.VK_F20;
                    break;

                case Key.F21:
                    virtualKey = NativeMethods.VK_F21;
                    break;

                case Key.F22:
                    virtualKey = NativeMethods.VK_F22;
                    break;

                case Key.F23:
                    virtualKey = NativeMethods.VK_F23;
                    break;

                case Key.F24:
                    virtualKey = NativeMethods.VK_F24;
                    break;

                case Key.NumLock:
                    virtualKey = NativeMethods.VK_NUMLOCK;
                    break;

                case Key.Scroll:
                    virtualKey = NativeMethods.VK_SCROLL;
                    break;

                case Key.LeftShift:
                    virtualKey = NativeMethods.VK_LSHIFT;
                    break;

                case Key.RightShift:
                    virtualKey = NativeMethods.VK_RSHIFT;
                    break;

                case Key.LeftCtrl:
                    virtualKey = NativeMethods.VK_LCONTROL;
                    break;

                case Key.RightCtrl:
                    virtualKey = NativeMethods.VK_RCONTROL;
                    break;

                case Key.LeftAlt:
                    virtualKey = NativeMethods.VK_LMENU;
                    break;

                case Key.RightAlt:
                    virtualKey = NativeMethods.VK_RMENU;
                    break;

                case Key.BrowserBack:
                    virtualKey = NativeMethods.VK_BROWSER_BACK;
                    break;

                case Key.BrowserForward:
                    virtualKey = NativeMethods.VK_BROWSER_FORWARD;
                    break;

                case Key.BrowserRefresh:
                    virtualKey = NativeMethods.VK_BROWSER_REFRESH;
                    break;

                case Key.BrowserStop:
                    virtualKey = NativeMethods.VK_BROWSER_STOP;
                    break;

                case Key.BrowserSearch:
                    virtualKey = NativeMethods.VK_BROWSER_SEARCH;
                    break;

                case Key.BrowserFavorites:
                    virtualKey = NativeMethods.VK_BROWSER_FAVORITES;
                    break;

                case Key.BrowserHome:
                    virtualKey = NativeMethods.VK_BROWSER_HOME;
                    break;

                case Key.VolumeMute:
                    virtualKey = NativeMethods.VK_VOLUME_MUTE;
                    break;

                case Key.VolumeDown:
                    virtualKey = NativeMethods.VK_VOLUME_DOWN;
                    break;

                case Key.VolumeUp:
                    virtualKey = NativeMethods.VK_VOLUME_UP;
                    break;

                case Key.MediaNextTrack:
                    virtualKey = NativeMethods.VK_MEDIA_NEXT_TRACK;
                    break;

                case Key.MediaPreviousTrack:
                    virtualKey = NativeMethods.VK_MEDIA_PREV_TRACK;
                    break;

                case Key.MediaStop:
                    virtualKey = NativeMethods.VK_MEDIA_STOP;
                    break;

                case Key.MediaPlayPause:
                    virtualKey = NativeMethods.VK_MEDIA_PLAY_PAUSE;
                    break;

                case Key.LaunchMail:
                    virtualKey = NativeMethods.VK_LAUNCH_MAIL;
                    break;

                case Key.SelectMedia:
                    virtualKey = NativeMethods.VK_LAUNCH_MEDIA_SELECT;
                    break;

                case Key.LaunchApplication1:
                    virtualKey = NativeMethods.VK_LAUNCH_APP1;
                    break;

                case Key.LaunchApplication2:
                    virtualKey = NativeMethods.VK_LAUNCH_APP2;
                    break;

                case Key.OemSemicolon:
                    virtualKey = NativeMethods.VK_OEM_1;
                    break;

                case Key.OemPlus:
                    virtualKey = NativeMethods.VK_OEM_PLUS;
                    break;

                case Key.OemComma:
                    virtualKey = NativeMethods.VK_OEM_COMMA;
                    break;

                case Key.OemMinus:
                    virtualKey = NativeMethods.VK_OEM_MINUS;
                    break;

                case Key.OemPeriod:
                    virtualKey = NativeMethods.VK_OEM_PERIOD;
                    break;

                case Key.OemQuestion:
                    virtualKey = NativeMethods.VK_OEM_2;
                    break;

                case Key.OemTilde:
                    virtualKey = NativeMethods.VK_OEM_3;
                    break;

                case Key.AbntC1:
                    virtualKey = NativeMethods.VK_C1;
                    break;

                case Key.AbntC2:
                    virtualKey = NativeMethods.VK_C2;
                    break;

                case Key.OemOpenBrackets:
                    virtualKey = NativeMethods.VK_OEM_4;
                    break;

                case Key.OemPipe:
                    virtualKey = NativeMethods.VK_OEM_5;
                    break;

                case Key.OemCloseBrackets:
                    virtualKey = NativeMethods.VK_OEM_6;
                    break;

                case Key.OemQuotes:
                    virtualKey = NativeMethods.VK_OEM_7;
                    break;

                case Key.Oem8:
                    virtualKey = NativeMethods.VK_OEM_8;
                    break;

                case Key.OemBackslash:
                    virtualKey = NativeMethods.VK_OEM_102;
                    break;

                case Key.ImeProcessed:
                    virtualKey = NativeMethods.VK_PROCESSKEY;
                    break;

                case Key.OemAttn:                           // DbeAlphanumeric
                    virtualKey = NativeMethods.VK_OEM_ATTN; // VK_DBE_ALPHANUMERIC
                    break;

                case Key.OemFinish:                           // DbeKatakana
                    virtualKey = NativeMethods.VK_OEM_FINISH; // VK_DBE_KATAKANA
                    break;

                case Key.OemCopy:                           // DbeHiragana
                    virtualKey = NativeMethods.VK_OEM_COPY; // VK_DBE_HIRAGANA
                    break;

                case Key.OemAuto:                           // DbeSbcsChar
                    virtualKey = NativeMethods.VK_OEM_AUTO; // VK_DBE_SBCSCHAR
                    break;

                case Key.OemEnlw:                           // DbeDbcsChar
                    virtualKey = NativeMethods.VK_OEM_ENLW; // VK_DBE_DBCSCHAR
                    break;

                case Key.OemBackTab:                           // DbeRoman
                    virtualKey = NativeMethods.VK_OEM_BACKTAB; // VK_DBE_ROMAN
                    break;

                case Key.Attn:                          // DbeNoRoman
                    virtualKey = NativeMethods.VK_ATTN; // VK_DBE_NOROMAN
                    break;

                case Key.CrSel:                          // DbeEnterWordRegisterMode
                    virtualKey = NativeMethods.VK_CRSEL; // VK_DBE_ENTERWORDREGISTERMODE
                    break;

                case Key.ExSel:                          // EnterImeConfigureMode
                    virtualKey = NativeMethods.VK_EXSEL; // VK_DBE_ENTERIMECONFIGMODE
                    break;

                case Key.EraseEof:                       // DbeFlushString
                    virtualKey = NativeMethods.VK_EREOF; // VK_DBE_FLUSHSTRING
                    break;

                case Key.Play:                           // DbeCodeInput
                    virtualKey = NativeMethods.VK_PLAY;  // VK_DBE_CODEINPUT
                    break;

                case Key.Zoom:                           // DbeNoCodeInput
                    virtualKey = NativeMethods.VK_ZOOM;  // VK_DBE_NOCODEINPUT
                    break;

                case Key.NoName:                          // DbeDetermineString
                    virtualKey = NativeMethods.VK_NONAME; // VK_DBE_DETERMINESTRING
                    break;

                case Key.Pa1:                          // DbeEnterDlgConversionMode
                    virtualKey = NativeMethods.VK_PA1; // VK_ENTERDLGCONVERSIONMODE
                    break;

                case Key.OemClear:
                    virtualKey = NativeMethods.VK_OEM_CLEAR;
                    break;

                case Key.DeadCharProcessed:             //This is usused.  It's just here for completeness.
                    virtualKey = 0;                     //There is no Win32 VKey for this.
                    break;

                default:
                    virtualKey = 0;
                    break;
            }

            return virtualKey;
        }

    }

    public class TaskStringVisitor : CefStringVisitor
    {
        private readonly TaskCompletionSource<string> taskCompletionSource;

        public TaskStringVisitor()
        {
            taskCompletionSource = new TaskCompletionSource<string>();
        }

        protected override void Visit(string value)
        {
            taskCompletionSource.SetResult(value);
        }

        public Task<string> Task
        {
            get { return taskCompletionSource.Task; }
        }
    }

    public static class CEFExtensions
    {
        public static Task<string> GetSourceAsync(this CefBrowser browser)
        {
            TaskStringVisitor taskStringVisitor = new TaskStringVisitor();
            browser.GetMainFrame().GetSource(taskStringVisitor);
            return taskStringVisitor.Task;
        }
    }


    public class AvaloniaCefBrowser : TemplatedControl
    {
        private bool _disposed;
        private bool _created;

        private Image _browserPageImage;

        private WriteableBitmap _browserPageBitmap;

        private int _browserWidth;
        private int _browserHeight;
        private bool _browserSizeChanged;

        private CefBrowser _browser;
        private CefBrowserHost _browserHost;
        private WpfCefClient _cefClient;

        private ToolTip _tooltip;
        private DispatcherTimer _tooltipTimer;

        private Popup _popup;
        private Image _popupImage;
        private WriteableBitmap _popupImageBitmap;

        private TaskCompletionSource<string> _messageReceiveCompletionSource;

        public string StartUrl { get; set; }
        public bool AllowsTransparency { get; set; }
        public Key Keys { get; private set; }


        public CefBrowser Browser => _browser;

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);

            _browserPageImage = e.NameScope.Find<Image>("PART_Image");
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            var size = base.ArrangeOverride(arrangeBounds);

            if (_browserPageImage != null)
            {
                var newWidth = (int)size.Width;
                var newHeight = (int)size.Height;

                if (newWidth > 0 && newHeight > 0)
                {
                    if (!_created)
                    {
                        AttachEventHandlers(this); // TODO: ?

                        // Create the bitmap that holds the rendered website bitmap
                        _browserWidth = newWidth;
                        _browserHeight = newHeight;
                        _browserSizeChanged = true;

                        // Find the window that's hosting us                        
                        Window parentWnd = this.GetVisualRoot() as Window;

                        if (parentWnd != null)
                        {

                            IntPtr hParentWnd = parentWnd.PlatformImpl.Handle.Handle;

                            var windowInfo = CefWindowInfo.Create();
                            windowInfo.SetAsWindowless(hParentWnd, AllowsTransparency);

                            var settings = new CefBrowserSettings();
                            _cefClient = new WpfCefClient(this);

                            _messageReceiveCompletionSource = new TaskCompletionSource<string>();

                            _cefClient.MessageReceived += (sender, e) =>
                            {
                                if (e.Message.Name == "executeJsResult")
                                {
                                    _messageReceiveCompletionSource.SetResult(e.Message.Arguments.GetString(0));
                                }
                            };

                            // This is the first time the window is being rendered, so create it.
                            CefBrowserHost.CreateBrowser(windowInfo, _cefClient, settings, !string.IsNullOrEmpty(StartUrl) ? StartUrl : "about:blank");

                            _created = true;
                        }
                    }
                    else
                    {
                        // Only update the bitmap if the size has changed
                        if (_browserPageBitmap == null || (_browserPageBitmap.PixelSize.Width != newWidth || _browserPageBitmap.PixelSize.Height != newHeight))
                        {
                            _browserWidth = newWidth;
                            _browserHeight = newHeight;
                            _browserSizeChanged = true;

                            // If the window has already been created, just resize it
                            if (_browserHost != null)
                            {
                                _browserHost.WasResized();
                            }
                        }
                    }
                }
            }

            return size;

        }

        private static CefEventFlags GetKeyboardModifiers(InputModifiers kbModifiers)
        {
            CefEventFlags modifiers = new CefEventFlags();

            if (kbModifiers == InputModifiers.Alt)
                modifiers |= CefEventFlags.AltDown;

            if (kbModifiers == InputModifiers.Control)
                modifiers |= CefEventFlags.ControlDown;

            if (kbModifiers == InputModifiers.Shift)
                modifiers |= CefEventFlags.ShiftDown;

            return modifiers;
        }

        private static CefEventFlags GetMouseModifiers(InputModifiers mouseModifiers)
        {
            CefEventFlags modifiers = new CefEventFlags();

            if (mouseModifiers == InputModifiers.LeftMouseButton)
                modifiers |= CefEventFlags.LeftMouseButton;

            if (mouseModifiers == InputModifiers.MiddleMouseButton)
                modifiers |= CefEventFlags.MiddleMouseButton;

            if (mouseModifiers == InputModifiers.RightMouseButton)
                modifiers |= CefEventFlags.RightMouseButton;

            return modifiers;
        }

        private void AttachEventHandlers(AvaloniaCefBrowser browser)
        {
            browser.GotFocus += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        _browserHost.SendFocusEvent(true);
                    }
                }
                catch (Exception ex)
                {

                }
            };

            browser.LostFocus += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        _browserHost.SendFocusEvent(false);
                    }
                }
                catch (Exception ex)
                {

                }
            };

            browser.PointerLeave += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        CefMouseEvent mouseEvent = new CefMouseEvent()
                        {
                            X = 0,
                            Y = 0
                        };

                        mouseEvent.Modifiers = GetMouseModifiers(arg.InputModifiers);

                        _browserHost.SendMouseMoveEvent(mouseEvent, true);
                        //_logger.Debug("Browser_MouseLeave");
                    }
                }
                catch (Exception ex)
                {

                }
            };

            browser.PointerMoved += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        Point cursorPos = arg.GetPosition(this);

                        CefMouseEvent mouseEvent = new CefMouseEvent()
                        {
                            X = (int)cursorPos.X,
                            Y = (int)cursorPos.Y
                        };

                        mouseEvent.Modifiers = GetMouseModifiers(arg.InputModifiers);

                        _browserHost.SendMouseMoveEvent(mouseEvent, false);

                        //_logger.Debug(string.Format("Browser_MouseMove: ({0},{1})", cursorPos.X, cursorPos.Y));
                    }
                }
                catch (Exception ex)
                {

                }
            };

            browser.PointerPressed += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        Focus();

                        Point cursorPos = arg.GetPosition(this);

                        CefMouseEvent mouseEvent = new CefMouseEvent()
                        {
                            X = (int)cursorPos.X,
                            Y = (int)cursorPos.Y,
                        };

                        mouseEvent.Modifiers = GetMouseModifiers(arg.InputModifiers);

                        if (arg.MouseButton == MouseButton.Left)
                            _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Left, false, arg.ClickCount);
                        else if (arg.MouseButton == MouseButton.Middle)
                            _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Middle, false, arg.ClickCount);
                        else if (arg.MouseButton == MouseButton.Right)
                            _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Right, false, arg.ClickCount);

                        //_logger.Debug(string.Format("Browser_MouseDown: ({0},{1})", cursorPos.X, cursorPos.Y));
                    }
                }
                catch (Exception ex)
                {

                }
            };

            browser.PointerReleased += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        Point cursorPos = arg.GetPosition(this);

                        CefMouseEvent mouseEvent = new CefMouseEvent()
                        {
                            X = (int)cursorPos.X,
                            Y = (int)cursorPos.Y,
                        };

                        mouseEvent.Modifiers = GetMouseModifiers(arg.InputModifiers);

                        if (arg.MouseButton == MouseButton.Left)
                            _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Left, true, 1);
                        else if (arg.MouseButton == MouseButton.Middle)
                            _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Middle, true, 1);
                        else if (arg.MouseButton == MouseButton.Right)
                            _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Right, true, 1);

                        //_logger.Debug(string.Format("Browser_MouseUp: ({0},{1})", cursorPos.X, cursorPos.Y));
                    }
                }
                catch (Exception ex)
                {
                    //_logger.ErrorException("WpfCefBrowser: Caught exception in MouseUp()", ex);
                }
            };

            browser.PointerWheelChanged += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        Point cursorPos = arg.GetPosition(this);

                        CefMouseEvent mouseEvent = new CefMouseEvent()
                        {
                            X = (int)cursorPos.X,
                            Y = (int)cursorPos.Y,
                        };

                        _browserHost.SendMouseWheelEvent(mouseEvent, 0, (int)arg.Delta.Y);
                    }
                }
                catch (Exception ex)
                {
                    //_logger.ErrorException("WpfCefBrowser: Caught exception in MouseWheel()", ex);
                }
            };

            // TODO: require more intelligent processing
            browser.TextInput += (sender, arg) =>
            {
                if (_browserHost != null)
                {
                    foreach (var c in arg.Text)
                    {
                        CefKeyEvent keyEvent = new CefKeyEvent()
                        {
                            EventType = CefKeyEventType.Char,
                            WindowsKeyCode = (int)c,
                            Character = c,
                        };

                        //keyEvent.Modifiers = GetKeyboardModifiers(KeyboardDevice.Instance.);

                        _browserHost.SendKeyEvent(keyEvent);
                    }
                }

                arg.Handled = true;
            };

            // TODO: require more intelligent processing
            browser.KeyDown += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        //_logger.Debug(string.Format("KeyDown: system key {0}, key {1}", arg.SystemKey, arg.Key));
                        CefKeyEvent keyEvent = new CefKeyEvent()
                        {
                            EventType = CefKeyEventType.RawKeyDown,
                            WindowsKeyCode = KeyInterop.VirtualKeyFromKey(arg.Key),
                            NativeKeyCode = 0,
                            IsSystemKey = arg.Key == Key.System,
                        };

                        if (arg.Key == Key.Enter)
                        {
                            keyEvent.EventType = CefKeyEventType.Char;
                        }

                        keyEvent.Modifiers = GetKeyboardModifiers(arg.Modifiers);

                        _browserHost.SendKeyEvent(keyEvent);
                    }
                }
                catch (Exception ex)
                {
                    //_logger.ErrorException("WpfCefBrowser: Caught exception in PreviewKeyDown()", ex);
                }

                //arg.Handled = HandledKeys.Contains(arg.Key);
            };

            // TODO: require more intelligent processing
            browser.KeyUp += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        //_logger.Debug(string.Format("KeyUp: system key {0}, key {1}", arg.SystemKey, arg.Key));
                        CefKeyEvent keyEvent = new CefKeyEvent()
                        {
                            EventType = CefKeyEventType.KeyUp,
                            WindowsKeyCode = KeyInterop.VirtualKeyFromKey(arg.Key),
                            NativeKeyCode = 0,
                            IsSystemKey = arg.Key == Key.System,
                        };

                        keyEvent.Modifiers = GetKeyboardModifiers(arg.Modifiers);

                        _browserHost.SendKeyEvent(keyEvent);
                    }
                }
                catch (Exception ex)
                {
                    //_logger.ErrorException("WpfCefBrowser: Caught exception in PreviewKeyUp()", ex);
                }

                arg.Handled = true;

                var location = System.Reflection.Assembly.GetEntryAssembly().Location;
                var directory = System.IO.Path.GetDirectoryName(location);                                
            };

            /*browser._popup.MouseMove += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        Point cursorPos = arg.GetPosition(this);

                        CefMouseEvent mouseEvent = new CefMouseEvent()
                        {
                            X = (int)cursorPos.X,
                            Y = (int)cursorPos.Y
                        };

                        mouseEvent.Modifiers = GetMouseModifiers();

                        _browserHost.SendMouseMoveEvent(mouseEvent, false);

                        //_logger.Debug(string.Format("Popup_MouseMove: ({0},{1})", cursorPos.X, cursorPos.Y));
                    }
                }
                catch (Exception ex)
                {
                    _logger.ErrorException("WpfCefBrowser: Caught exception in Popup.MouseMove()", ex);
                }
            };

            browser._popup.MouseDown += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        Point cursorPos = arg.GetPosition(this);

                        CefMouseEvent mouseEvent = new CefMouseEvent()
                        {
                            X = (int)cursorPos.X,
                            Y = (int)cursorPos.Y
                        };

                        mouseEvent.Modifiers = GetMouseModifiers();

                        _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Left, true, 1);

                        //_logger.Debug(string.Format("Popup_MouseDown: ({0},{1})", cursorPos.X, cursorPos.Y));
                    }
                }
                catch (Exception ex)
                {
                    _logger.ErrorException("WpfCefBrowser: Caught exception in Popup.MouseDown()", ex);
                }
            };

            browser._popup.MouseWheel += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        Point cursorPos = arg.GetPosition(this);
                        int delta = arg.Delta;
                        CefMouseEvent mouseEvent = new CefMouseEvent()
                        {
                            X = (int)cursorPos.X,
                            Y = (int)cursorPos.Y
                        };

                        mouseEvent.Modifiers = GetMouseModifiers();
                        _browserHost.SendMouseWheelEvent(mouseEvent, 0, delta);

                        //_logger.Debug(string.Format("MouseWheel: ({0},{1})", cursorPos.X, cursorPos.Y));
                    }
                }
                catch (Exception ex)
                {
                    _logger.ErrorException("WpfCefBrowser: Caught exception in Popup.MouseWheel()", ex);
                }
            };*/
        }

        internal bool OnTooltip(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                // _tooltipTimer.Stop();
                UpdateTooltip(null);
            }
            else
            {
                /*   _tooltipTimer.Tick += (sender, args) => UpdateTooltip(text);
                   _tooltipTimer.Start();*/
            }

            return true;
        }

        public event LoadStartEventHandler LoadStart;
        public event LoadEndEventHandler LoadEnd;
        public event LoadingStateChangeEventHandler LoadingStateChange;
        public event LoadErrorEventHandler LoadError;

        internal void OnLoadStart(CefFrame frame)
        {
            if (this.LoadStart != null)
            {
                var e = new LoadStartEventArgs(frame);
                this.LoadStart(this, e);
            }
        }

        internal void OnLoadEnd(CefFrame frame, int httpStatusCode)
        {
            if (this.LoadEnd != null)
            {
                var e = new LoadEndEventArgs(frame, httpStatusCode);
                this.LoadEnd(this, e);
            }
        }

        private object _scriptLock = new object();

        public async Task<string> ExecuteScriptAsync(string code, string scriptUrl = null)
        {
            var message = CefProcessMessage.Create("executeJs");
            message.Arguments.SetString(0, code);
            message.Arguments.SetString(1, scriptUrl);

            _messageReceiveCompletionSource = new TaskCompletionSource<string>();

            _browser.SendProcessMessage(CefProcessId.Renderer, message);

            await _messageReceiveCompletionSource.Task;

            var messageReceived = _messageReceiveCompletionSource.Task.Result;

            return messageReceived;
        }

        internal void OnLoadingStateChange(bool isLoading, bool canGoBack, bool canGoForward)
        {
            if (this.LoadingStateChange != null)
            {
                var e = new LoadingStateChangeEventArgs(isLoading, canGoBack, canGoForward);
                this.LoadingStateChange(this, e);
            }
        }
        internal void OnLoadError(CefFrame frame, CefErrorCode errorCode, string errorText, string failedUrl)
        {
            if (this.LoadError != null)
            {
                var e = new LoadErrorEventArgs(frame, errorCode, errorText, failedUrl);
                this.LoadError(this, e);
            }
        }

        private void UpdateTooltip(string text)
        {
            Dispatcher.UIThread.InvokeAsync(
                    () =>
                    {
                        if (string.IsNullOrEmpty(text))
                        {
                            //_tooltip.IsOpen = false;
                        }
                        else
                        {
                            //_tooltip.Placement = PlacementMode.Mouse;
                            _tooltip.Content = text;
                            //_tooltip.IsOpen = true;
                            //_tooltip.Visibility = Visibility.Visible;
                        }
                    });

            //_tooltipTimer.Stop();
        }

        public void HandleAfterCreated(CefBrowser browser)
        {
            int width = 0, height = 0;

            bool hasAlreadyBeenInitialized = false;

            //Dispatcher.UIThread.InvokeTaskAsync(() =>
            {
                if (_browser != null)
                {
                    hasAlreadyBeenInitialized = true;
                }
                else
                {
                    _browser = browser;
                    _browserHost = _browser.GetHost();

                    // _browserHost.SetFocus(IsFocused);

                    width = (int)_browserWidth;
                    height = (int)_browserHeight;
                }
            }//);

            // Make sure we don't initialize ourselves more than once. That seems to break things.
            if (hasAlreadyBeenInitialized)
                return;

            if (width > 0 && height > 0)
                _browserHost.WasResized();

            // 			mainUiDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            // 			{
            // 				if (!string.IsNullOrEmpty(this.initialUrl))
            // 				{
            // 					NavigateTo(this.initialUrl);
            // 					this.initialUrl = string.Empty;
            // 				}
            // 			}));
        }

        internal bool GetViewRect(ref CefRectangle rect)
        {
            bool rectProvided = false;
            CefRectangle browserRect = new CefRectangle();

            // TODO: simplify this
            //_mainUiDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            //{
            try
            {
                // The simulated screen and view rectangle are the same. This is necessary
                // for popup menus to be located and sized inside the view.
                browserRect.X = browserRect.Y = 0;
                browserRect.Width = (int)_browserWidth;
                browserRect.Height = (int)_browserHeight;

                rectProvided = true;
            }
            catch (Exception ex)
            {
                rectProvided = false;
            }
            //}));

            if (rectProvided)
            {
                rect = browserRect;
            }

            return rectProvided;
        }

        internal void GetScreenPoint(int viewX, int viewY, ref int screenX, ref int screenY)
        {
            Point ptScreen = new Point();

            //Dispatcher.UIThread.InvokeAsync(()=>
            {
                try
                {
                    Point ptView = new Point(viewX, viewY);
                    ptScreen = this.PointToScreen(ptView);
                }
                catch (Exception ex)
                {

                }
            }//);

            screenX = (int)ptScreen.X;
            screenY = (int)ptScreen.Y;
        }

        internal void HandleViewPaint(CefBrowser browser, CefPaintElementType type, CefRectangle[] dirtyRects, IntPtr buffer, int width, int height)
        {
            // When browser size changed - we just skip frame updating.
            // This is dirty precheck to do not do Invoke whenever is possible.
            if (_browserSizeChanged && (width != _browserWidth || height != _browserHeight)) return;

            //Dispatcher.UIThread.InvokeAsync(()=>
            {
                // Actual browser size changed check.
                if (_browserSizeChanged && (width != _browserWidth || height != _browserHeight)) return;

                try
                {
                    if (_browserSizeChanged)
                    {
                        _browserPageBitmap = new WriteableBitmap(new PixelSize((int)_browserWidth, (int)_browserHeight), new Vector(96, 96), PixelFormat.Bgra8888);//new WriteableBitmap((int)_browserWidth, (int)_browserHeight, 96, 96, AllowsTransparency ? PixelFormats.Bgra32 : PixelFormats.Bgr32, null);
                        _browserPageImage.Source = _browserPageBitmap;

                        _browserSizeChanged = false;
                    }

                    if (_browserPageBitmap != null)
                    {
                        DoRenderBrowser(_browserPageBitmap, width, height, dirtyRects, buffer);

                        _browserPageImage.InvalidateVisual();
                    }

                }
                catch (Exception ex)
                {
                }
            }//);
        }

        internal void HandlePopupPaint(int width, int height, CefRectangle[] dirtyRects, IntPtr sourceBuffer)
        {
            if (width == 0 || height == 0)
            {
                return;
            }

            Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        int stride = width * 4;
                        int sourceBufferSize = stride * height;

                        foreach (CefRectangle dirtyRect in dirtyRects)
                        {
                            if (dirtyRect.Width == 0 || dirtyRect.Height == 0)
                            {
                                continue;
                            }

                            int adjustedWidth = dirtyRect.Width;

                            int adjustedHeight = dirtyRect.Height;

                            Rect sourceRect = new Rect(dirtyRect.X, dirtyRect.Y, adjustedWidth, adjustedHeight);

                            // _popupImageBitmap.WritePixels(sourceRect, sourceBuffer, sourceBufferSize, stride, dirtyRect.X, dirtyRect.Y);
                        }
                    });
        }

        private void DoRenderBrowser(WriteableBitmap bitmap, int browserWidth, int browserHeight, CefRectangle[] dirtyRects, IntPtr sourceBuffer)
        {
            int stride = browserWidth * 4;
            int sourceBufferSize = stride * browserHeight;

            if (browserWidth == 0 || browserHeight == 0)
            {
                return;
            }

            foreach (CefRectangle dirtyRect in dirtyRects)
            {
                if (dirtyRect.Width == 0 || dirtyRect.Height == 0)
                {
                    continue;
                }

                // If the window has been resized, make sure we never try to render too much
                int adjustedWidth = (int)dirtyRect.Width;
                //if (dirtyRect.X + dirtyRect.Width > (int) bitmap.Width)
                //{
                //    adjustedWidth = (int) bitmap.Width - (int) dirtyRect.X;
                //}

                int adjustedHeight = (int)dirtyRect.Height;
                //if (dirtyRect.Y + dirtyRect.Height > (int) bitmap.Height)
                //{
                //    adjustedHeight = (int) bitmap.Height - (int) dirtyRect.Y;
                //}

                // Update the dirty region
                var sourceRect = new Rect((int)dirtyRect.X, (int)dirtyRect.Y, adjustedWidth, adjustedHeight);


                //bitmap.WritePixels(sourceRect, sourceBuffer, sourceBufferSize, stride, (int)dirtyRect.X, (int)dirtyRect.Y);

                // 			int adjustedWidth = browserWidth;
                // 			if (browserWidth > (int)bitmap.Width)
                // 				adjustedWidth = (int)bitmap.Width;
                // 
                // 			int adjustedHeight = browserHeight;
                // 			if (browserHeight > (int)bitmap.Height)
                // 				adjustedHeight = (int)bitmap.Height;
                // 
                // 			int sourceBufferSize = browserWidth * browserHeight * 4;
                // 			int stride = browserWidth * 4;
                // 
                // 			Int32Rect sourceRect = new Int32Rect(0, 0, adjustedWidth, adjustedHeight);
                // 			bitmap.WritePixels(sourceRect, sourceBuffer, sourceBufferSize, stride, 0, 0);
            }

            using (var l = bitmap.Lock())
            {
                byte[] managedArray = new byte[sourceBufferSize];

                Marshal.Copy(sourceBuffer, managedArray, 0, sourceBufferSize);

                Marshal.Copy(managedArray, 0, l.Address, sourceBufferSize);
            }
        }

        byte count = 0;

        internal void OnPopupShow(bool show)
        {
            if (_popup == null)
            {
                return;
            }

            Dispatcher.UIThread.InvokeAsync(() => _popup.IsOpen = show);
        }


    }
}


