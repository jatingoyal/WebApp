using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WebApp.Models;
using WebApp.DAO;
using System.Diagnostics;
namespace WebApp.Services
{
    public class FileDirectoryService
    {
        public static List<FolderModel> GetAllFolders()
        {
            return DirectoryData.GetAllFolders();
        }

        public static List<FileModel> GetFiles(string folderName)
        {
            return DirectoryData.GetFilesFromFolder(folderName);
        }

        public static void DownloadFile(int fileId)
        {

            FileModel file = DirectoryData.GetFileFromId(fileId);
            string basePath = @"~\corpus";
            if (file == null)
                return;
            Debug.WriteLine("ZZ" + fileId);
            string filePath = basePath + "\\" + file.Folder + "\\" + file.Name;
            filePath = HttpContext.Current.Server.MapPath(filePath);
            if (!System.IO.File.Exists(filePath))
                return;
            System.IO.Stream oStream = null;
            try
            {
                oStream =
                    new System.IO.FileStream
                    (path: filePath,
                    mode: System.IO.FileMode.Open,
                    share: System.IO.FileShare.Read,
                    access: System.IO.FileAccess.Read);

                long lngFileLength = oStream.Length;
                HttpContext.Current.Response.Buffer = false;
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);

                long lngDataToRead = lngFileLength;
                int count = 0;
                while (lngDataToRead > 0)
                {
                    Debug.WriteLine((++count)+ " ");
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
              
                        int intBufferSize = 8 * 1024; 
                        byte[] bytBuffers =
                            new System.Byte[intBufferSize];
                        int intTheBytesThatReallyHasBeenReadFromTheStream =
                            oStream.Read(buffer: bytBuffers, offset: 0, count: intBufferSize);
                        HttpContext.Current.Response.OutputStream.Write
                            (buffer: bytBuffers, offset: 0,
                            count: intTheBytesThatReallyHasBeenReadFromTheStream);
                        HttpContext.Current.Response.Flush();
                        lngDataToRead =
                            lngDataToRead - intTheBytesThatReallyHasBeenReadFromTheStream;
                    }
                    else
                    {
                        lngDataToRead = -1;
                    }
                }
            }
            catch(Exception e) {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                if (oStream != null)
                {
                    oStream.Close();
                    oStream.Dispose();
                    oStream = null;
                }
                HttpContext.Current.Response.Close();

            }
        }
    }
    
}