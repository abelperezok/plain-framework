using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Plain.Library.FileUtility
{
    public class FileHelper
    {
        public static bool CopyFile(string sourcePath, string destinyPath, string directoryPath, bool deleteSourceFile = false)
        {
            bool result = false;
            try
            {
                if (!String.IsNullOrEmpty(directoryPath))
                {
                    System.IO.Directory.CreateDirectory(directoryPath);
                }

                if (!destinyPath.Equals(sourcePath))
                {
                    System.IO.File.Copy(sourcePath, destinyPath, true);
                }
                result = true;

                if (deleteSourceFile && !destinyPath.Equals(sourcePath))
                {
                    System.IO.File.Delete(sourcePath);
                }
            }
            catch (Exception)
            {
            }
            return result;
        }

        public static void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void DeleteFolder(string path, bool deleteContent)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, deleteContent);
            }
        }

        public static void Copy(string originFile, string destinationFile)
        {
            if ((File.Exists(originFile)) && (!File.Exists(destinationFile)))
            {
                File.Copy(originFile, destinationFile);
            }
        }

        public static void Copy(string originFile, string destinationFile, bool overwrite)
        {
            if (File.Exists(originFile))
            {
                File.Copy(originFile, destinationFile, overwrite);
            }
        }

        /// <summary>
        /// Escribe un flujo de byte a un fichero
        /// </summary>
        /// <param name="filename">Camino y nombre del fichero</param>
        /// <param name="bytes">Flujo de byte</param>
        public static void WriteToFile(string path, string filename, byte[] bytes, bool createFolder)
        {
            if (createFolder)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }

            if (filename != null)
            {
                using (FileStream full = File.Open(Path.Combine(path, filename), FileMode.Create))
                {
                    full.Write(bytes, 0, bytes.Length);
                    full.Flush();
                }
            }
        }
        /// <summary>
        /// Elimina un fichero
        /// </summary>
        /// <param name="filename">Fichero a Eliminar</param>
        public static void DeleteFile(string filename)
        {
            if (filename != null)
            {
                try
                {
                    if (File.Exists(filename))
                        File.Delete(filename);
                }
                catch { }
            }
        }
        /// <summary>
        /// Mueve un fichero de un lado a otro
        /// </summary>
        /// <param name="oldPath"></param>
        /// <param name="newPath"></param>
        public static void MoveFile(string oldPath, string newPath)
        {
            if (oldPath != null)
            {
                if (File.Exists(oldPath))
                    File.Move(oldPath, newPath);
            }
        }
    }
}
