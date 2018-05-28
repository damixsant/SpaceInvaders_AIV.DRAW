using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    public static class PlayerSound
    {
        [DllImport("winmm.dll")]


        static extern Int32 mciSendString(string lpszCommand, StringBuilder lpszReturnString, int cchReturn, IntPtr hwndCallback);

        public static void Play(string path, string alias)
        {
            mciSendString("open \"" + path + "\" alias " + alias, null, 0, IntPtr.Zero);
            mciSendString("play " + alias, null, 0, IntPtr.Zero);
        }
    }
}
