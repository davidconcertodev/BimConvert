using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Xbim.IO;
using Xbim.ModelGeometry.Scene;
using XbimGeometry.Interfaces;

namespace BimConvert
{
    public partial class FormMain : Form
    {
        
        public FormMain()
        {
            InitializeComponent();
        }

        ConvertWorkerThread convertWorker = null;
        bool isClosing = false;
        bool isFinished = false;
        Guid workerId;
        Semaphore sphFormAvailable = new Semaphore(1 , 1);

        private void buttonBrowseSourceFiles_Click(object sender, EventArgs e)
        {
            string[] sourcefiles;
            try
            {
                openFileDialogSource.DefaultExt = ".ifc";
                openFileDialogSource.Title = "Choose IFC files";
                openFileDialogSource.Multiselect = true;
                openFileDialogSource.Filter = "IFC files (*.ifc)|*.ifc|All files (*.*)|*.*";
                openFileDialogSource.FilterIndex = 1;
                if (openFileDialogSource.ShowDialog() == DialogResult.OK)
                {
                    textBoxDestination.Text = string.Empty;
                    sourcefiles = new string[openFileDialogSource.FileNames.Length];
                    if (openFileDialogSource.FileNames.Length > 0)
                    {
                        openFileDialogSource.FileNames.CopyTo(sourcefiles, 0);
                        textBoxDestination.Text = Path.GetDirectoryName(openFileDialogSource.FileNames[0]);
                    }
                    AddListViewItems(sourcefiles);
                    if (sourcefiles.Length > 1)
                    {
                        textBoxSourceFile.Text = string.Format("({0:D} files)", sourcefiles.Length);
                    }
                    else if (sourcefiles.Length == 1)
                    {
                        textBoxSourceFile.Text = sourcefiles[0] ?? string.Empty;
                    }
                    else
                    {
                        textBoxSourceFile.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Util.ErrorBox(ex);
            }
        }

        private void AddListViewItems(string[] sourcefiles)
        {            
            for (int i = 0; i < sourcefiles.Length;i++ )
            {
                FileConvertItem k = new FileConvertItem {Name = Path.GetFileName(sourcefiles[i]), FullPathName = sourcefiles[i], Status = string.Empty };
                listViewSourceFiles.Items.Add(new FileConvertListViewItem(k));
            }
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (!isClosing)
                {
                    ThreadedConvert();
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Util.ErrorBox(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void ClearListViewStatuses()
        {
            for (int i = 0; i < listViewSourceFiles.Items.Count; i++)
            {
                FileConvertListViewItem li = listViewSourceFiles.Items[i] as FileConvertListViewItem;
                if (li != null)
                {
                    li.SubItems["status"].Text = "";
                }
            }
        }

        void ThreadedConvert()
        {
            try
            {
                SetBusy(true);
                if (!isClosing && (convertWorker == null || !convertWorker.IsAlive))
                {
                    PrepareForWorker();
                    ClearListViewStatuses();
                    List<FileConvertItem> files = GetSourceFileListData();
                    convertWorker = new ConvertWorkerThread(this, sphFormAvailable, files, textBoxDestination.Text);
                    convertWorker.OnFinish += convertWorker_OnFinish;
                    convertWorker.OnListItemUpdate += convertWorker_OnListItemUpdate;
                    workerId = convertWorker.Id;
                    convertWorker.Start();
                }
            }
            catch
            {
                SetBusy(false);
            }
        }

        void PrepareForWorker()
        {
            isFinished = false;
            workerId = Guid.Empty;
        }

        void convertWorker_OnListItemUpdate(object sender, ListItemUpdateEventArgs e)
        {
            if (workerId != e.Id)
                return;
            if (e.Index < listViewSourceFiles.Items.Count)
            {
                FileConvertListViewItem li = listViewSourceFiles.Items[e.Index] as FileConvertListViewItem;
                if (li != null)
                {
                    li.SubItems["status"].Text = e.Message ?? string.Empty;
                    this.Refresh();
                }
            }
        }

        void convertWorker_OnFinish(object sender, WorkerEventArgs e)
        {
            if (workerId != e.Id)
                return;
            isFinished = true;
            SetBusy(false);
            isClosing = false;
            buttonCancel.Text = "Cancel";
        }

        bool isBusy = false;
        void SetBusy(bool isBusy)
        {
            this.isBusy = isBusy;
            buttonCancel.Enabled = isBusy;
            buttonBrowseDestinationFolder.Enabled = !isBusy;
            buttonBrowseSourceFiles.Enabled = !isBusy;
            buttonClear.Enabled = !isBusy;
            buttonConvert.Enabled = !isBusy;
        }

        List<FileConvertItem> GetSourceFileListData()
        {
            List<FileConvertItem> lst = new List<FileConvertItem>(listViewSourceFiles.Items.Count);
            for (int i = 0; i < listViewSourceFiles.Items.Count; i++)
            {
                FileConvertListViewItem li = listViewSourceFiles.Items[i] as FileConvertListViewItem;
                if (li != null)
                {
                    FileConvertItem dataitem = li.Tag as FileConvertItem;
                    if (dataitem != null)
                    {
                        lst.Add(dataitem);
                    }
                }
            }
            return lst;
        }
    
        private void buttonBrowseDestinationFolder_Click(object sender, EventArgs e)
        {
            try
            {                
                folderBrowserDialogDestination.Description = "Destination folder";
                if (folderBrowserDialogDestination.ShowDialog() == DialogResult.OK)
                {
                    textBoxDestination.Text = folderBrowserDialogDestination.SelectedPath;
                }
            }
            catch
            {
            }
        }

        private void listViewSourceFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            InitListViewSourceFiles(listViewSourceFiles);
            SetBusy(false);
        }

        private void InitListViewSourceFiles(ListView lv)
        {
            lv.View = View.Details;
            lv.Columns.Clear();
            lv.Columns.Add("Name", 500);
            lv.Columns.Add("Status", 80);
            lv.LabelEdit = false;
            //lv.ListViewItemSorter = new ListViewItemComparer(listViewSortingColumn);
            //lv.Sorting = SortOrder.Ascending;
            lv.AllowDrop = false;
            lv.AllowColumnReorder = false;
            lv.LabelEdit = false;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            try
            {
                listViewSourceFiles.Items.Clear();
            }
            catch
            {
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                isClosing = true;
                if (convertWorker != null)
                {
                    if (convertWorker.IsAlive)
                    {
                        convertWorker.Quit();
                        if (!isFinished)
                        {
                            buttonCancel.Enabled = false;
                            buttonCancel.Text = "Quitting...";
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch
            {
            }

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (convertWorker != null)
                {
                    if (convertWorker.IsAlive)
                    {
                        convertWorker.Quit();
                        if (!isFinished)
                        {
                            buttonCancel.Enabled = false;
                            buttonCancel.Text = "Quitting...";
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}
