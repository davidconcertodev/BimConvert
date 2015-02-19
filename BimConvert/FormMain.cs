using System;
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

        void ConvertFile(string ifcFileFullName, string destinationFolder, string wexBimFileName, string xbimFile)
        {
            string workingDir = destinationFolder;
            if (string.IsNullOrWhiteSpace(destinationFolder))
            {
                workingDir = Path.GetDirectoryName(ifcFileFullName);
            }
            else
            {
                workingDir = destinationFolder;
            }
            if (string.IsNullOrWhiteSpace(workingDir))
            {
                throw new ApplicationException("The destination folder not specified.");
            }
            if (!Path.IsPathRooted(workingDir))
            {
                throw new ApplicationException("The destination folder not rooted.");
            }
            if (!Directory.Exists(workingDir))
            {
                throw new ApplicationException("The destination folder not found.");
            }
            string fileName = Path.GetFileName(ifcFileFullName);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string coreDestinationFileName = Path.Combine(workingDir, fileNameWithoutExtension);
            if (string.IsNullOrWhiteSpace(wexBimFileName))
            {
                wexBimFileName = Path.ChangeExtension(coreDestinationFileName, "wexbim");
            }
            else
            {
                wexBimFileName = Path.Combine(workingDir, Path.GetFileName(wexBimFileName));
            }
            if (string.IsNullOrWhiteSpace(xbimFile))
            {
                xbimFile = Path.ChangeExtension(coreDestinationFileName, "xbim");
            }
            else
            {
                xbimFile = Path.Combine(workingDir, Path.GetFileName(xbimFile));
            }
            using (FileStream wexBimFile = new FileStream(wexBimFileName, FileMode.Create))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(wexBimFile))
                {
                    using (XbimModel model = new XbimModel())
                    {
                        model.CreateFrom(ifcFileFullName, xbimFile, null, true, false);
                        Xbim3DModelContext geomContext = new Xbim3DModelContext(model);
                        geomContext.CreateContext(XbimGeometryType.PolyhedronBinary); // System.OutOfMemoryException Exception
                        geomContext.Write(binaryWriter);
                    }
                }
            }
        }

        string[] sourcefiles;

        private void buttonBrowseSourceFiles_Click(object sender, EventArgs e)
        {
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
            listViewSourceFiles.Items.Clear();
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
                if (sourcefiles != null)
                {
                    for (int i = 0; i < listViewSourceFiles.Items.Count; i++)
                    {
                        FileConvertListViewItem li = listViewSourceFiles.Items[i] as FileConvertListViewItem;
                        if (li != null)
                        {
                            li.SubItems["status"].Text = "";
                        }
                    }
                    this.Refresh();
                    for (int i = 0; i < listViewSourceFiles.Items.Count; i++)
                    {
                        FileConvertListViewItem li = listViewSourceFiles.Items[i] as FileConvertListViewItem;
                        if (li != null)
                        {
                            li.SubItems["status"].Text = "Converting...";
                            this.Refresh();
                            FileConvertItem dataitem = li.Tag as FileConvertItem;
                            if (dataitem != null)
                            {
                                bool ok = false;
                                try
                                {
                                    ConvertFile(dataitem.FullPathName, textBoxDestination.Text, null, null);
                                    ok = true;
                                }
                                catch (Exception ex)
                                {
                                    li.SubItems["status"].Text = ex.Message;
                                }
                                if (ok)
                                {
                                    li.SubItems["status"].Text = "Done";
                                }
                            }                            
                            this.Refresh();
                        }
                    }
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
        }

        private void InitListViewSourceFiles(ListView lv)
        {
            lv.View = View.Details;
            lv.Columns.Clear();
            lv.Columns.Add("Name", 300);
            lv.Columns.Add("Status", 80);
            lv.LabelEdit = false;
            //lv.ListViewItemSorter = new ListViewItemComparer(listViewSortingColumn);
            //lv.Sorting = SortOrder.Ascending;
            lv.AllowDrop = false;
            lv.AllowColumnReorder = false;
            lv.LabelEdit = false;
        }
    }
}
