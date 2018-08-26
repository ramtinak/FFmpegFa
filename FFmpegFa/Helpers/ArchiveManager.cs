using Shell32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FFmpegFa.Helpers
{
    class ArchiveManager
    {
        private static string _lastError = "";
        private const int SLEEP_DURATION = 100;
        private const int TIMEOUT_VALUE = 5;

        public static bool Archive(string archiveFile, string unArchiveFolder)
        {
            try
            {
                _lastError = "";
                if (!VerifyExtension(archiveFile))
                {
                    return false;
                }
                byte[] buffer = new byte[] {
                    80, 0x4b, 5, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0
                 };
                FileStream stream = File.Create(Path.GetFullPath(archiveFile));
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
                stream.Close();
                stream = null;
                Shell shell = (Shell)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("13709620-C279-11CE-A49E-444553540000")));
                Folder folderObjDestination = shell.NameSpace(Path.GetFullPath(archiveFile));
                Folder folderObjSource = shell.NameSpace(Path.GetFullPath(unArchiveFolder));
                folderObjDestination.CopyHere(folderObjSource.Items(), 20);
                if (!WaitTillItemCountIsEqual(folderObjSource, folderObjDestination))
                {
                    return false;
                }
                return true;
            }
            catch (Exception exception)
            {
                _lastError = "ERROR: Could not create archive. Exception: " + exception.Message;
                return false;
            }
        }

        private static bool CheckAndGetExtractionPath(ref string fileName, out string extractionPath)
        {
            _lastError = "";
            try
            {
                fileName = Path.GetFullPath(fileName);
                extractionPath = Path.GetDirectoryName(fileName);
                if (!File.Exists(fileName))
                {
                    _lastError = "ERROR: File " + fileName + " does NOT exist";
                    return false;
                }
                extractionPath = Path.Combine(extractionPath, Path.GetFileNameWithoutExtension(fileName));
                if (!Directory.Exists(extractionPath))
                {
                    Directory.CreateDirectory(extractionPath);
                }
                return true;
            }
            catch (Exception exception)
            {
                extractionPath = "";
                _lastError = "ERROR: Could not get/create the extraction path. Exception: " + exception.Message;
                return false;
            }
        }

        public static bool CopyPermissions(string sourceFile, string destFile)
        {
            try
            {
                _lastError = "";
                FileInfo info = new FileInfo(Path.GetFullPath(sourceFile));
                FileInfo info2 = new FileInfo(Path.GetFullPath(destFile));
                FileSecurity accessControl = info.GetAccessControl();
                accessControl.SetAccessRuleProtection(true, true);
                info2.SetAccessControl(accessControl);
                return true;
            }
            catch (Exception exception)
            {
                _lastError = "ERROR: Could not copy permissions. Exception: " + exception.Message;
                return false;
            }
        }

        public static bool UnArchive(string archiveFile)
        {
            try
            {
                _lastError = "";
                string extractionPath = "";
                if (!CheckAndGetExtractionPath(ref archiveFile, out extractionPath))
                {
                    return false;
                }
                if (!UnArchive(archiveFile, extractionPath))
                {
                    return false;
                }
                return true;
            }
            catch (Exception exception)
            {
                _lastError = "ERROR: Could not unarchive. Exception: " + exception.Message;
                return false;
            }
        }
       
        public static bool UnArchive(string archiveFile, string unArchiveFolder)
        {
            try
            {
                _lastError = "";
                if (!VerifyExtension(archiveFile))
                {
                    return false;
                }
                Shell shell = (Shell)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("13709620-C279-11CE-A49E-444553540000")));
                Folder folderObjSource = shell.NameSpace(Path.GetFullPath(archiveFile));
                Folder folderObjDestination = shell.NameSpace(Path.GetFullPath(unArchiveFolder));
                foreach (FolderItem item in folderObjSource.Items())
                {
                    folderObjDestination.CopyHere(item, 20);
                }
                if (!WaitTillItemCountIsEqual(folderObjSource, folderObjDestination))
                {
                    return false;
                }
                return true;
            }
            catch (Exception exception)
            {
                _lastError = "ERROR: Could not unarchive. Exception: " + exception.Message;
                return false;
            }
        }

        private static bool VerifyExtension(string fileName)
        {
            try
            {
                _lastError = "";
                string str = Path.GetExtension(fileName).ToUpper().Trim();
                if (str != ".ZIP")
                {
                    _lastError = "ERROR: Invalid extension '" + str + "' found in file name";
                    return false;
                }
                return true;
            }
            catch (Exception exception)
            {
                _lastError = "ERROR: Could not get/create the extraction path. Exception: " + exception.Message;
                return false;
            }
        }

        private static bool WaitTillItemCountIsEqual(Folder folderObjSource, Folder folderObjDestination)
        {
            try
            {
                _lastError = "";
                if ((folderObjSource == null) || (folderObjDestination == null))
                {
                    _lastError = "ERROR: One or more Folder object(s) is/are null";
                    return false;
                }
                int count = folderObjSource.Items().Count;
                int num2 = 50;
                int num3 = 0;
                while (folderObjDestination.Items().Count < count)
                {
                    if (num2 <= num3++)
                    {
                        _lastError = "ERROR: Timeout occurred while processing archive";
                        return false;
                    }
                    Thread.Sleep(100);
                }
                return true;
            }
            catch (Exception exception)
            {
                _lastError = "ERROR: Could not create archive. Exception: " + exception.Message;
                return false;
            }
        }

        public static string LastError
        {
            get
            {
                return _lastError;
            }
        }
    }
}
