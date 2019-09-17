using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace jj
{
    public class SaveFileDirOpen
    {
        [STAThread]
        public static void SaveOpen() 
        {
            OpenFileDialog opd = new OpenFileDialog();
            opd.FileName = "j";
            opd.ShowDialog();
        }
    }
}
