using System;
using System.IO;
using Collections;

namespace FileManager
{
    public class DirectoryWorker
    {
        private DirectoryService _directoryService;

        public string CurrentDirectoryPath => _directoryService.CurrentDirectoryPath;

        public DirectoryWorker()
        {
            _directoryService = new DirectoryService();
        }

        public List<FileInfo> TryGetCurrentFiles()
        {
            return _directoryService.TryGetCurrentFiles();
        }

        public List<DirectoryInfo> TryGetCurrentDirectories()
        {
            return _directoryService.TryGetCurrentDirectories();
        }

        public string ShowCurrentContentFullInfo(List<FileInfo> files, List<DirectoryInfo> directories)
        {
            string result = string.Empty;

            foreach(DirectoryInfo dir in directories)
            {
                string entry = $"{dir.Name}\\-time created: {dir.CreationTime}\n\t-time last edited: {dir.LastWriteTime}\n\t-time last accessed: {dir.LastAccessTime}\n";
                result += entry;
            }

            foreach(FileInfo file in files)
            {
                string entry = $"{file.Name}\n\t-file size: {FormatSize(file.Length)}\n\t-time created: {file.CreationTime}\n\t-time last edited: {file.LastWriteTime}\n";
                result += entry;
            }

            return result;
        }

        public string ShowCurrentContent(List<FileInfo> files, List<DirectoryInfo> directories)
        {
            string result = string.Empty;

            foreach (DirectoryInfo dir in directories)
            {
                result += dir.Name + "\\\n";
            }

            foreach (FileInfo file in files)
            {
                result += file.Name + "\n";
            }

            return result;
        }

        public List<DirectoryInfo> SortByName(List<DirectoryInfo> directories)
        {
            directories.Sort((DirectoryInfo a, DirectoryInfo b) => String.CompareOrdinal(a.Name.ToLower(), b.Name.ToLower()) > 0);
            return directories;
        }

        public List<FileInfo> SortByName(List<FileInfo> files)
        {
            files.Sort((FileInfo a, FileInfo b) => String.CompareOrdinal(a.Name.ToLower(), b.Name.ToLower()) > 0);
            return files;
        }

        public List<DirectoryInfo> SortByNameDescending(List<DirectoryInfo> directories)
        {
            directories.Sort((DirectoryInfo a, DirectoryInfo b) => String.CompareOrdinal(a.Name.ToLower(), b.Name.ToLower()) < 0);
            return directories;
        }

        public List<FileInfo> SortByNameDescending(List<FileInfo> files)
        {
            files.Sort((FileInfo a, FileInfo b) => String.CompareOrdinal(a.Name.ToLower(), b.Name.ToLower()) < 0);
            return files;
        }

        public List<DirectoryInfo> FilterHidden(List<DirectoryInfo> directories)
        {
            return directories.Filter((a) => a.Attributes.HasFlag(FileAttributes.Hidden));
        }

        public List<FileInfo> FilterHidden(List<FileInfo> files)
        {
            return files.Filter((a) => a.Attributes.HasFlag(FileAttributes.Hidden));
        }

        public string TryChangeDirectory(string name)
        {
            return _directoryService.TryChangeDirectory(name);
        }

        public string FormatSize(long size)
        {
            if (size < 0)
            {
                return "0 B";
            }

            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            float floatSize = (float)size;
            string result = string.Empty;

            int counter = 0;
            while (floatSize >= 1024f)
            {
                floatSize /= 1024f;
                counter++;
            }

            result = $"{Math.Round(floatSize, 2)} {sizes[counter]}";
            return result;
        }

        public string TryGetFileContent(string filePath)
        {
            string text = _directoryService.TryGetFileContent(filePath);
            string result = string.Empty;

            for (int i = 0; i < (text.Length > 200 ? 200 : text.Length); i++)
            {
                result += text[i];
            }

            return result;
        }

        public string TrySearchFile(string filePath, string substring)
        {
            return _directoryService.TrySearchFile(filePath, substring);
        }

        public string TryCreateFile(string name)
        {
            return _directoryService.TryCreateFile(name);
        }

        public string TryCreateDirectory(string name)
        {
            return _directoryService.TryCreateDirectory(name);
        }

        public string TryDeleteFile(string name)
        {
            return _directoryService.TryDeleteFile(name);
        }

        public string TryDeleteDirectory(string name)
        {
            return _directoryService.TryDeleteDirectory(name);
        }

        public string TryRenameFile(string name, string newName)
        {
            return _directoryService.TryRenameFile(name, newName);
        }

        public string TryRenameDirectory(string name, string newName)
        {
            return _directoryService.TryRenameDirectory(name, newName);
        }
    }
}
