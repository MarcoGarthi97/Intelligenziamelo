using Intelligenziamelo.Controllers;
using System.IO.Compression;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SharpCompress.Common;
using System.Text;

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
                Model model = new Model();

                string folderTemp = CreateFolderTemp().Result;
                zip.SaveAs(folderTemp + @"\" + Path.GetFileName(zip.FileName));

                var validity = ValidityZip(folderTemp + @"\" + Path.GetFileName(zip.FileName));
                model = SaveZipToFileSystem(folderTemp + @"\" + Path.GetFileName(zip.FileName), model).Result;

                Atlas atlas = new Atlas();
                atlas.InsertModel(model);
                return "ok";
            }
            else
                return "inserire file zip";
        }

        public static async Task<Model> SaveZipToFileSystem(string path, Model model)
        {
            model.ProjectPath = CreateFolderInFileSystem().Result;

            ZipFile.ExtractToDirectory(path, model.ProjectPath);
            model.DataSetPath = model.ProjectPath + @"\DataSet";

            DirectoryCopy(Directory.GetDirectories(model.ProjectPath)[0], model.DataSetPath);

            model.FileTsvPath = CreateFileTsv(model).Result;

            return model;
        }

        private static async void DirectoryCopy(string sourceDirName, string destinationDirName)
        {
            Directory.CreateDirectory(destinationDirName);

            foreach (string s in Directory.GetDirectories(sourceDirName))
            {
                List<string> pathFiles = Directory.GetFiles(s).ToList();

                string dir = "";
                for (int i = sourceDirName.Split('\\').Length - 1; i > destinationDirName.Split('\\').Length - 1; i--)
                {
                    dir += @"\" + sourceDirName.Split('\\')[i];
                }

                foreach (string pathFile in pathFiles)
                {
                    string path = pathFile.Replace(sourceDirName, destinationDirName + dir);
                    if (!Directory.Exists(Path.GetDirectoryName(path)))
                        Directory.CreateDirectory(Path.GetDirectoryName(path));

                    File.Move(pathFile, path);
                }
            }            

            Directory.Delete(sourceDirName, true);
        }

        public static async Task<string> CreateFileTsv(Model model)
        {
            try
            {
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                foreach (string label in Directory.GetDirectories(model.DataSetPath))
                {
                    foreach (string s in Directory.GetFiles(label))
                    {
                        KeyValuePair<string, string> item = new KeyValuePair<string, string>(label.Split('\\').Last(), s);
                        list.Add(item);
                    }
                }

                CheckDirectory(model.ProjectPath + @"\Data");

                using (var streamWriter = new StreamWriter(model.ProjectPath + @"\Data\Data.tsv", false, Encoding.UTF8))
                {
                    streamWriter.WriteLine("Label\tImageSource");

                    foreach (var item in list)
                    {
                        streamWriter.WriteLine($"{item.Key}\t{item.Value}");
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return model.ProjectPath + @"\Data\Data.tsv";
        }

        static async Task<bool> CheckDirectory(string path)
        {
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);

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
                //string path = dir + @"\Users\" + HomeController.userModel.Username + @"\Project" + i;
                string path = dir + @"Users\" + @"pippo\Project" + i; //per test
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    return path;
                }

                i++;
            }
        }

        public static async Task<string> CreateFolderTemp()
        {
            int i = 0;
            while(true)
            {
                //string path = dir + @"\" + HomeController.userModel.Username + "_" + i;
                string path = dir + @"Temp\" + "pippo" + "_" + i; //per test
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    return path;
                }

                i++;
            }
        }
    }
}