using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace BimConvert
{
    class ConvertWorkerThread
    {
        public event EventHandler<WorkerEventArgs> OnFinish;
        public event EventHandler<WorkerEventArgs> OnStarted;
        public event EventHandler<ListItemUpdateEventArgs> OnListItemUpdate;
        public Guid Id = new Guid();
        Thread worker;
        object m_lock = new object();
        bool isQuit = false;
        ManualResetEvent evtQuit = new ManualResetEvent(false);
        List<FileConvertItem> files;
        string destinationPath;
        Control window;
        Semaphore sphFormAvailable;

        public ConvertWorkerThread(Control window, Semaphore sphFormAvailable, List<FileConvertItem> files, string destinationPath)
        {
            this.Id = Guid.NewGuid();
            this.window = window;
            this.sphFormAvailable = sphFormAvailable;
            this.files = files;
            this.destinationPath = destinationPath;
        }

        private static void StartThread(object o)
        {
            ConvertWorkerThread t = (ConvertWorkerThread)o;
            t.StartThread();
        }

        private void StartThread()
        {
            try
            {
                SendStarted();
                int i;
                for (i = 0; i < files.Count && !IsQuit; i++)
                {
                    FileConvertItem dataitem = files[i];
                    if (dataitem != null)
                    {
                        SendListItemUpdate(i, "Converting...");
                        bool ok = false;
                        try
                        {
                            Util.ConvertFile(dataitem.FullPathName, destinationPath, null, null);
                            ok = true;
                        }
                        catch (Exception ex)
                        {
                            SendListItemUpdate(i, ex.Message);
                        }
                        GC.Collect();
                        if (ok)
                        {
                            SendListItemUpdate(i, "Done");
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                SendFinished();
            }
        }

        private void SendStarted()
        {
            IAsyncResult ar;
            int i = WaitHandle.WaitAny(new WaitHandle[] { sphFormAvailable, evtQuit });
            if (i == 0)
            {
                try
                {
                    if (window != null)
                    {
                        ar = window.BeginInvoke(new EventHandler<WorkerEventArgs>(SafeSendStarted), this, new WorkerEventArgs(Id));

                        WaitHandle.WaitAny(new WaitHandle[] { ar.AsyncWaitHandle, evtQuit });
                        if (ar.IsCompleted)
                            window.EndInvoke(ar);
                    }
                }
                finally
                {
                    sphFormAvailable.Release();
                }
            }
        }

        private void SafeSendStarted(object sender, WorkerEventArgs e)
        {
            try
            {
                if (OnStarted != null)
                    OnStarted(this, e);
            }
            catch
            {
            }
        }

        private void SendFinished()
        {
            IAsyncResult ar;
            int i = WaitHandle.WaitAny(new WaitHandle[] { sphFormAvailable, evtQuit });
            if (i == 0)
            {
                try
                {
                    if (window != null)
                    {
                        ar = window.BeginInvoke(new EventHandler<WorkerEventArgs>(SafeSendFinished), this, new WorkerEventArgs(Id));

                        WaitHandle.WaitAny(new WaitHandle[] { ar.AsyncWaitHandle, evtQuit });
                        if (ar.IsCompleted)
                            window.EndInvoke(ar);
                    }
                }
                finally
                {
                    sphFormAvailable.Release();
                }
            }
        }

        private void SafeSendFinished(object sender, WorkerEventArgs e)
        {
            try
            {
                if (OnFinish != null)
                    OnFinish(this, e);
            }
            catch
            {
            }
        }

        private void SendListItemUpdate(int index, string message)
        {
            IAsyncResult ar;
            int i = WaitHandle.WaitAny(new WaitHandle[] { sphFormAvailable, evtQuit });
            if (i == 0)
            {
                try
                {
                    if (window != null)
                    {
                        ar = window.BeginInvoke(new EventHandler<ListItemUpdateEventArgs>(SafeSendListItemUpdate), this, new ListItemUpdateEventArgs(Id, index, message));

                        WaitHandle.WaitAny(new WaitHandle[] { ar.AsyncWaitHandle, evtQuit });
                        if (ar.IsCompleted)
                            window.EndInvoke(ar);
                    }
                }
                finally
                {
                    sphFormAvailable.Release();
                }
            }
        }

        private void SafeSendListItemUpdate(object sender, ListItemUpdateEventArgs e)
        {
            try
            {
                if (OnListItemUpdate != null)
                    OnListItemUpdate(this, e);
            }
            catch
            {
            }
        }

        public void Start()
        {
            worker = new Thread(new ParameterizedThreadStart(StartThread));
            worker.Start(this);
        }

        public bool IsAlive
        {
            get
            {
                return worker != null && worker.IsAlive;
            }
        }

        public bool IsQuit
        {
            get
            {
                lock (m_lock)
                {
                    return isQuit;
                }
            }
        }

        public void Quit()
        {
            lock (m_lock)
            {
                isQuit = true;
                evtQuit.Set();
            }
        }

        public void Join()
        {
            if (worker != null && worker.IsAlive)
                worker.Join();
        }

        public void StopEvents()
        {
            StartEvents(null);
        }

        public void StartEvents(Control control)
        {
            sphFormAvailable.WaitOne();
            try
            {
                window = control;
            }
            finally
            {
                sphFormAvailable.Release();
            }
        }

    }
}
