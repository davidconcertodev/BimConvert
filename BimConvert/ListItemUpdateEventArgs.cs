using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimConvert
{
    class WorkerEventArgs : EventArgs
    {
        public WorkerEventArgs()
        {
        }
        public WorkerEventArgs(Guid id)
        {
            this.Id = id;
        }
        public Guid Id;
    }
    class ListItemUpdateEventArgs : WorkerEventArgs
    {
        public ListItemUpdateEventArgs()
        {
        }
        public ListItemUpdateEventArgs(Guid id, int index, string message)
            :base (id)
        {
            this.Message = message;
            this.Index = index;
        }
        public int Index;
        public string Message;
    }
}
