using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace WebApp.DAO
{
    public class DirectoryData
    {
        static string connectionString = @"Data Source=gjatin-z01; Initial Catalog=WebApp; User ID=WebAppAdmin; Password=root";
        static SqlConnection con;

        public static List<FolderModel> GetAllFolders()
        {
            List<FolderModel> allFolders = new List<FolderModel>();
            con = new SqlConnection(connectionString);
            string commandText = "Select * from folders";
            SqlCommand command = new SqlCommand(commandText, con);
            try
            {
                con.Open();
                SqlDataReader r = command.ExecuteReader();
                int nameCol = r.GetOrdinal("name");
                while (r.Read())
                {
                    FolderModel folder = new FolderModel();
                    folder.Name = (string)r.GetValue(nameCol);
                    allFolders.Add(folder);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Debug.WriteLine(e.Message);

            }
            finally
            {
                con.Close();
            }
            return allFolders;
        }

        public static List<FileModel> GetFilesFromFolder(string folderName)
        {
            
            List<FileModel> allFiles = new List<FileModel>();
            con = new SqlConnection(connectionString);
            string commandText = $"Select * from files where folder='{folderName}'";
            SqlCommand command = new SqlCommand(commandText, con);
            try
            {
                con.Open();
                SqlDataReader r = command.ExecuteReader();
                int nameCol = r.GetOrdinal("name");
                int idCol = r.GetOrdinal("id");
                int folderCol = r.GetOrdinal("folder");

                while (r.Read())
                {
                    FileModel file = new FileModel();
                    file.Name = (string)r.GetValue(nameCol);
                    file.Id = (int)r.GetValue(idCol);
                    file.Folder = (string)r.GetValue(folderCol);

                    allFiles.Add(file);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Debug.WriteLine(e.Message);

            }
            finally
            {
                con.Close();
            }
            return allFiles;
        }

        public static FileModel GetFileFromId(int id)
        {
            FileModel file = new FileModel();
            con = new SqlConnection(connectionString);
            string commandText = $"Select * from files where id={id}";
            SqlCommand command = new SqlCommand(commandText, con);
            try
            {
                con.Open();
                SqlDataReader r = command.ExecuteReader();
                int nameCol = r.GetOrdinal("name");
                int idCol = r.GetOrdinal("id");
                int folderCol = r.GetOrdinal("folder");

                if (r.Read())
                {
                    file.Name = (string)r.GetValue(nameCol);
                    file.Id = (int)r.GetValue(idCol);
                    file.Folder = (string)r.GetValue(folderCol);
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Debug.WriteLine(e.Message);
                return null;
            }
            finally
            {
                con.Close();
            }
            return file;
        }
    }
}
