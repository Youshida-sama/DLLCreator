using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ff
{
    public static class Main
    {
        [STAThread]
        public static void Run()
        {
            SaveFileDirOpen.SaveOpen();
        }
    }
}
