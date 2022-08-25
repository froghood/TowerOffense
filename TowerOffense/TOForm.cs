// using System.Windows.Forms;
// using System.Runtime.InteropServices;
// using System;

// namespace TowerOffense {
//     public class TOForm : Form {

//         public bool CloseEnabled {
//             get => _closeBoxEnabled;
//             set {
//                 EnableMenuItem(GetSystemMenu(Handle, false), SC_CLOSE, value ? MF_ENABLED : MF_GREYED);
//                 _closeBoxEnabled = value;
//             }
//         }

//         public bool UserMovingEnabled {
//             get => _userMovingEnabled;
//             set => _userMovingEnabled = value;
//         }

//         [DllImport("user32")]
//         private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool revert);

//         [DllImport("user32")]
//         private static extern int EnableMenuItem(IntPtr hWndMenu, int itemID, int enable);

//         private const int SC_CLOSE = 0xF060;
//         private const int MF_ENABLED = 0x0000;
//         private const int MF_GREYED = 0x0001;
//         private const int WM_SYSCOMMAND = 0x0112;
//         private const int SC_MOVE = 0xF010;

//         private bool _closeBoxEnabled = true;
//         private bool _userMovingEnabled = true;

//         protected override void WndProc(ref Message m) {
//             switch (m.Msg) {
//                 case WM_SYSCOMMAND:
//                     int command = m.WParam.ToInt32() & 0xfff0;
//                     System.Console.WriteLine(command);
//                     if (command == SC_MOVE)
//                         if (!_userMovingEnabled) return;
//                     break;
//             }

//             base.WndProc(ref m);
//         }
//     }
// }