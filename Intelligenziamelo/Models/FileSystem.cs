using Intelligenziamelo.Controllers;
using System.IO.Compression;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Intelligenziamelo.Models
{
    public class FileSystem
    {
        static string dir = Properties.Settings.Default.DirectoryFileSystem;
        public static async Task<string> CheckDataSet(HttpPostedFileBase zip)
        {
            if (zip == null || zip.ContentLength == 0) return "File vuoto.";
            else if (Path.GetExtension(zip.FileName).ToLower() == ".zip")
            {
                string folderTemp = CreateFolderTemp().Result;
                zip.SaveAs(folderTemp + @"\" + Path.GetFileName(zip.FileName));

                var validity = ValidityZip(folderTemp + @"\" + Path.GetFileName(zip.FileName));
                var result = SaveZipToFileSystem(folderTemp + @"\" + Path.GetFileName(zip.FileName));
                return "ok";
            }
            else
                return "inserire file zip";
        }

        public static async Task<bool> SaveZipToFileSystem(string path)
        {
            string directory = CreateFolderInFileSystem().Result;

            ZipFile.ExtractToDirectory(path, directory);
            string sad = Path.GetDirectoryName(path);
            Directory.Delete(Path.GetDirectoryName(path)); //non elimina la directory

            return true;
        }

        public static async Task<bool> ValidityZip(string path)
        {
            List<string> extensions = new List<string>() { ".jpg", ".jpeg", ".png"};
            using (ZipArchive zip = ZipFile.Open(path, ZipArchiveMode.Read))
            {
                foreach (ZipArchiveEntry entry in zip.Entries)
                {
                    if (entry.FullName[entry.FullName.Length - 1] != '/' && !extensions.Contains(Path.GetExtension(entry.FullName).ToLower()))
                        entry.Delete();
                }
            }

            return true;
        }

        public static async Task<string> CreateFolderInFileSystem()
        {
            int i = 0;
            while (true)
            {
                //string path = dir + @"\Users\" + HomeController.userModel.Username + @"\Project" + i + @"\DataSet";
                string path = dir + @"\Users\" + @"pippo\Project" + i + @"\DataSet"; //per test
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    return path;
                }
            }
        }

        public static async Task<string> CreateFolderTemp()
        {
            int i = 0;
            while(true)
            {
                //string path = dir + @"\" + HomeController.userModel.Username + "_" + i;
                string path = dir + @"\Temp\" + "pippo" + "_" + i; //per test
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    return path;
                }
            }
        }
    }
}