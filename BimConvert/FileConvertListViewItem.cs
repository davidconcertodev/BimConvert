using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BimConvert
{
    internal class FileConvertListViewItem : ListViewItem
    {
        public FileConvertListViewItem(FileConvertItem item)
            : base(item.Name)
        {
            ListViewSubItem lsi;
            ListViewItem li = this;
            lsi = li.SubItems.Add(item.Status);//Status
            lsi.Name = "status";
            li.Tag = item;
        }
    }
}
