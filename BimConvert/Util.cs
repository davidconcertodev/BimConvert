using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BimConvert
{
    internal static class Util
    {
        internal static void ErrorBox(Exception ex)
        {
            MessageBox.Show(ex.Message ?? string.Empty, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }
    }
}
