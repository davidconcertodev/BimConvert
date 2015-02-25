using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xbim.IO;
using Xbim.ModelGeometry.Scene;
using XbimGeometry.Interfaces;


namespace BimConvert
{
    internal static class Util
    {
        internal static void ErrorBox(Exception ex)
        {
            MessageBox.Show(ex.Message ?? string.Empty, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

        public static bool ConvertFile(string ifcFileFullName, string destinationFolder, string wexBimFileName, string xbimFile, out string message)
        {
            message = string.Empty;
            try
            {
                ConvertFile(ifcFileFullName, destinationFolder, wexBimFileName, xbimFile);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return false;
        }

        public static void ConvertFile(string ifcFileFullName, string destinationFolder, string wexBimFileName, string xbimFile)
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
    }
}
