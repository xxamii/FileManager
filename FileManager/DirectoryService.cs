using System;

using System.IO;
using Collections;

namespace FileManager
{
    public class DirectoryService
    {
        private string _currentDirectoryPath = string.Empty;

        public string CurrentDirectoryPath => _currentDirectoryPath;

        public DirectoryService()
        {
            _currentDirectoryPath = GetRootDirectoryPath();
        }

        public List<DirectoryInfo> TryGetCurrentDirectories()
        {
            return TryGetDirectories(_currentDirectoryPath);
        }

        private List<DirectoryInfo> TryGetDirectories(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    string[] names = Directory.GetDirectories(path);
                    List<DirectoryInfo> result = new List<DirectoryInfo>();

                    foreach (string name in names)
                    {
                        DirectoryInfo dir = new DirectoryInfo(Path.Combine(path, name));
                        result.Push(dir);
                    }

                    return result;
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Could not fetch directories {path}: access denied");

            }
            catch (Exception)
            {
                Console.WriteLine($"Could not fetch directories from {path}");
            }

            return new List<DirectoryInfo>();
        }

        public List<FileInfo> TryGetCurrentFiles()
        {
            return TryGetFiles(_currentDirectoryPath);
        }

        public List<FileInfo> TryGetFiles(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    string[] names = Directory.GetFiles(path);
                    List<FileInfo> result = new List<FileInfo>();

                    foreach (string name in names)
                    {
                        FileInfo file = new FileInfo(Path.Combine(path, name));
                        result.Push(file);
                    }

                    return result;
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Could not fetch files from {path}: access denied");
            }
            catch (Exception)
            {
                Console.WriteLine($"Could not fetch files from {path}");
            }

            return new List<FileInfo>();
        }

        private string GetRootDirectoryPath()
        {
            string currentPath = Directory.GetCurrentDirectory();
            string rootPath = Directory.GetDirectoryRoot(currentPath);

            return rootPath;
        }

        public string TryChangeDirectory(string name)
        {
            if (name == "..")
            {
                DirectoryInfo directory = Directory.GetParent(_currentDirectoryPath);

                if (directory != null && directory.Exists)
                {
                    _currentDirectoryPath = Directory.GetParent(_currentDirectoryPath).FullName;
                    return string.Empty;
                }
            }
            else if (Directory.Exists(Path.Combine(_currentDirectoryPath, name)))
            {
                DirectoryInfo directory = new DirectoryInfo(Path.Combine(_currentDirectoryPath, name));
                _currentDirectoryPath = directory.FullName;
                return string.Empty;
            }

            return $"No such directory: {name}";
        }

        public long GetDirectorySize(DirectoryInfo directory)
        {
            long result = 0;

            List<FileInfo> files = TryGetFiles(directory.FullName);
            foreach (FileInfo file in files)
            {
                result += file.Length;
            }

            List<DirectoryInfo> directories = TryGetDirectories(directory.FullName);

            if (directories.Length > 0)
            {
                foreach (DirectoryInfo dir in directories)
                {
                    result += GetDirectorySize(dir);
                }
            }

            return result;
        }

        public string TryGetFileContent(string filePath)
        {
            try
            {
                string result = string.Empty;
                string path = Path.Combine(_currentDirectoryPath, filePath);

                if (File.Exists(path))
                {
                    result = File.ReadAllText(path);
                }
                else
                {
                    result = $"File {filePath} does not exist";
                }

                return result;
            }
            catch (UnauthorizedAccessException)
            {
                return $"Could not fetch file {filePath}: access denied";
            }
            catch (Exception)
            {
                return $"Could not fetch file {filePath}";
            }
        }

        public string TrySearchFile(string filePath, string substring) // TODO: Move to DirectoryWorker
        {
            try
            {
                string result = string.Empty;
                string path = Path.Combine(_currentDirectoryPath, filePath);

                if (File.Exists(path))
                {
                    string text = File.ReadAllText(path);
                    if (text.Contains(substring))
                    {
                        result = $"File {filePath} contains substring: {substring}";
                    }
                    else
                    {
                        result = $"File {filePath} does not contain substring: {substring}";
                    }
                }
                else
                {
                    result = $"File {filePath} does not exist";
                }

                return result;
            }
            catch (UnauthorizedAccessException)
            {
                return $"Could not fetch file {filePath}: access denied";
            }
            catch (Exception)
            {
                return $"Could not fetch file {filePath}";
            }
        }

        public string TryCreateFile(string filePath)
        {
            try
            {
                string result = string.Empty;
                string path = Path.Combine(_currentDirectoryPath, filePath);
                string dirName = Path.GetDirectoryName(path);

                if (!Directory.Exists(dirName) && dirName != null)
                {
                    Directory.CreateDirectory(dirName);
                }

                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                }
                else
                {
                    result = $"File {filePath} already exists";
                }

                return result;
            }
            catch (NotSupportedException)
            {
                return $"Invalid path or file name: {filePath}";
            }
            catch (UnauthorizedAccessException)
            {
                return $"Could not create file {filePath}: access denied";
            }
            catch (Exception)
            {
                return $"Could not create file {filePath}";
            }
        }

        public string TryCreateDirectory(string dirPath)
        {
            try
            {
                string result = string.Empty;
                string path = Path.Combine(_currentDirectoryPath, dirPath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                else
                {
                    result = $"Directory {path} already exists";
                }

                return result;
            }
            catch (NotSupportedException)
            {
                return $"Invalid path or directory name: {dirPath}";
            }
            catch (UnauthorizedAccessException)
            {
                return $"Could not create directory {dirPath}: access denied";
            }
            catch (Exception)
            {
                return $"Could not create directory {dirPath}";
            }
        }

        public string TryDeleteFile(string filePath)
        {
            try
            {
                string result = string.Empty;
                string path = Path.Combine(_currentDirectoryPath, filePath);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                else
                {
                    result = $"File {filePath} does not exist";
                }

                return result;
            }
            catch (NotSupportedException)
            {
                return $"Invalid path or file name: {filePath}";
            }
            catch (UnauthorizedAccessException)
            {
                return $"Could not delete file {filePath}: access denied";
            }
            catch (Exception)
            {
                return $"Could not delete file {filePath}";
            }
        }

        public string TryDeleteDirectory(string dirPath)
        {
            try
            {
                string result = string.Empty;
                string path = Path.Combine(_currentDirectoryPath, dirPath);

                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                else
                {
                    result = $"Directory {dirPath} does not exist";
                }

                return result;
            }
            catch (NotSupportedException)
            {
                return $"Invalid path or file name: {dirPath}";
            }
            catch (UnauthorizedAccessException)
            {
                return $"Could not delete file {dirPath}: access denied";
            }
            catch (Exception)
            {
                return $"Could not delete file {dirPath}";
            }
        }

        public string TryRenameFile(string oldFilePath, string newFilePath)
        {
            try
            {
                string result = string.Empty;
                string oldPath = Path.Combine(_currentDirectoryPath, oldFilePath);
                string newPath = Path.Combine(_currentDirectoryPath, newFilePath);

                if (File.Exists(oldPath) && !File.Exists(newPath))
                {
                    File.Move(oldPath, newPath);
                }
                else
                {
                    result = !File.Exists(oldPath) ? $"File {oldFilePath} does not exist" : $"File {newFilePath} already exists";
                }

                return result;
            }
            catch (NotSupportedException)
            {
                return $"Invalid path or file name: {oldFilePath}";
            }
            catch (UnauthorizedAccessException)
            {
                return $"Could not rename file {oldFilePath}: access denied";
            }
            catch (Exception)
            {
                return $"Could not rename file {oldFilePath}";
            }
        }

        public string TryRenameDirectory(string oldDirPath, string newDirPath)
        {
            try
            {
                string result = string.Empty;
                string oldPath = Path.Combine(_currentDirectoryPath, oldDirPath);
                string newPath = Path.Combine(_currentDirectoryPath, newDirPath);

                if (Directory.Exists(oldPath) && !Directory.Exists(newPath))
                {
                    Directory.Move(oldPath, newPath);
                }
                else
                {
                    result = !Directory.Exists(oldPath) ? $"Directory {oldDirPath} does not exist" : $"Directory {newDirPath} already exists";
                }

                return result;
            }
            catch (NotSupportedException)
            {
                return $"Invalid path or directory name: {oldDirPath}";
            }
            catch (UnauthorizedAccessException)
            {
                return $"Could not rename directory {oldDirPath}: access denied";
            }
            catch (Exception)
            {
                return $"Could not rename directory {oldDirPath}";
            }
        }
    }
}
