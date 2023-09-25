using System;
using System.IO;
using Collections;

namespace FileManager
{
    public class CommandResolver
    {
        private DirectoryWorker _directoryWorker;

        public CommandResolver(DirectoryWorker directoryWorker)
        {
            _directoryWorker = directoryWorker;
        }

        public string ResolveCommand(string command)
        {
            string result = string.Empty;

            if (command == string.Empty)
            {
                return result;
            }

            List<string> commandTokens = GetCommandTokens(command);

            switch (commandTokens[0])
            {
                case "ls":
                    result = ResolveListCommand(commandTokens);
                    break;
                case "cd":
                    result = ResolveChangeDirCommand(commandTokens);
                    break;
                case "file":
                    result = ResolveReadFileCommand(commandTokens);
                    break;
                case "sfile":
                    result = ResolveSearchFileCommand(commandTokens);
                    break;
                case "mkdir":
                    result = ResolveMakeDirectoryCommand(commandTokens);
                    break;
                case "touch":
                    result = ResolveTouchCommand(commandTokens);
                    break;
                case "rmdir":
                    result = ResolveRemoveDirectoryCommand(commandTokens);
                    break;
                case "rmfile":
                    result = ResolveRemoveFileCommand(commandTokens);
                    break;
                case "rndir":
                    result = ResolveRenameDirectoryCommand(commandTokens);
                    break;
                case "rnfile":
                    result = ResolveRenameFileCommand(commandTokens);
                    break;
                case "help":
                    result = ResolveHelpCommand();
                    break;
                default:
                    result = $"Invalid command: {command}, try help for the list of commands";
                    break;
            }

            return result;
        }

        private List<string> GetCommandTokens(string command)
        {
            List<string> result = new List<string>();
            string current = $"{command[0]}";

            for(int i = 1; i < command.Length + 1; i++)
            {
                if (i > command.Length - 1 || (Char.IsWhiteSpace(command[i]) && !command[i-1].Equals('\\')))
                {
                    if (current != string.Empty)
                    {
                        result.Push(current);
                    }

                    current = string.Empty;
                    continue;
                }

                if (!(command[i].Equals('\\') && i < command.Length - 1 && Char.IsWhiteSpace(command[i+1])))
                {
                    current += command[i];
                }
            }

            return result;
        }

        private string ResolveListCommand(List<string> commandTokens) // Important TODO: refactor this abomination
        {
            string result = string.Empty;

            for (int i = 1; i < commandTokens.Length; i++)
            {
                if (commandTokens[i] != "-nh" && commandTokens[i] != "-s" && commandTokens[i] != "-f" && commandTokens[i] != "-sd")
                {
                    return $"Invalid flag {commandTokens[i]}";
                }
            }

            List<FileInfo> files = _directoryWorker.TryGetCurrentFiles();
            List<DirectoryInfo> directories = _directoryWorker.TryGetCurrentDirectories();

            if (commandTokens.Contains("-nh"))
            {
                files = _directoryWorker.FilterHidden(files);
                directories = _directoryWorker.FilterHidden(directories);
            }
            if (commandTokens.Contains("-s"))
            {
                files = _directoryWorker.SortByName(files);
                directories = _directoryWorker.SortByName(directories);
            }
            if (commandTokens.Contains("-sd"))
            {
                files = _directoryWorker.SortByNameDescending(files);
                directories = _directoryWorker.SortByNameDescending(directories);
            }

            if (commandTokens.Contains("-f"))
            {
                result += _directoryWorker.ShowCurrentContentFullInfo(files, directories);
            }
            else
            {
                result += _directoryWorker.ShowCurrentContent(files, directories);
            }

            return result;
        }

        private string ResolveChangeDirCommand(List<string> commandTokens)
        {
            string result = string.Empty;

            if (commandTokens.Length == 2)
            {
                result = _directoryWorker.TryChangeDirectory(commandTokens[1]);
            }
            else
            {
                result = "Invalid command, try: cd [directory_name]";
            }

            return result;
        }

        private string ResolveReadFileCommand(List<string> commandTokens)
        {
            string result = string.Empty;

            if (commandTokens.Length == 2)
            {
                result = _directoryWorker.TryGetFileContent(commandTokens[1]);
            }
            else
            {
                result = "Invalid command, try: file [file_name]";
            }

            return result;
        }

        private string ResolveSearchFileCommand(List<string> commandTokens)
        {
            string result = string.Empty;

            if (commandTokens.Length == 3)
            {
                result = _directoryWorker.TrySearchFile(commandTokens[1], commandTokens[2]);
            }
            else
            {
                result = "Invalid command, try: sfile [file_name] [substring]";
            }

            return result;
        }

        private string ResolveMakeDirectoryCommand(List<string> commandTokens)
        {
            string result = string.Empty;

            if (commandTokens.Length == 2)
            {
                result = _directoryWorker.TryCreateDirectory(commandTokens[1]);
            }
            else
            {
                result = "Invalid command, try: mkdir [directory_name]";
            }

            return result;
        }

        private string ResolveTouchCommand(List<string> commandTokens)
        {
            string result = string.Empty;

            if (commandTokens.Length == 2)
            {
                result = _directoryWorker.TryCreateFile(commandTokens[1]);
            }
            else
            {
                result = "Invalid command, try: touch [file_name]";
            }

            return result;
        }

        private string ResolveRemoveDirectoryCommand(List<string> commandTokens)
        {
            string result = string.Empty;

            if (commandTokens.Length == 2)
            {
                result = _directoryWorker.TryDeleteDirectory(commandTokens[1]);
            }
            else
            {
                result = "Invalid command, try: rmdir [directory_name]";
            }

            return result;
        }

        private string ResolveRemoveFileCommand(List<string> commandTokens)
        {
            string result = string.Empty;

            if (commandTokens.Length == 2)
            {
                result = _directoryWorker.TryDeleteFile(commandTokens[1]);
            }
            else
            {
                result = "Invalid command, try: rmfile [file_name]";
            }

            return result;
        }

        private string ResolveRenameDirectoryCommand(List<string> commandTokens)
        {
            string result = string.Empty;

            if (commandTokens.Length == 3)
            {
                result = _directoryWorker.TryRenameDirectory(commandTokens[1], commandTokens[2]);
            }
            else
            {
                result = "Invalid command, try: rndir [file_name]";
            }

            return result;
        }

        private string ResolveRenameFileCommand(List<string> commandTokens)
        {
            string result = string.Empty;

            if (commandTokens.Length == 3)
            {
                result = _directoryWorker.TryRenameFile(commandTokens[1], commandTokens[2]);
            }
            else
            {
                result = "Invalid command, try: rnfile [file_name]";
            }

            return result;
        }

        private string ResolveHelpCommand()
        {
            string result = String.Concat("ls - list the current directory contents\n",
                "\t-s - sort by name, ascending\n",
                "\t-sd - sort by name, descending\n",
                "\t-nh - hide hidden files\n",
                "\t-f - show full information about a file or a directory\n",
                "cd [directory_name] - move into said directory\n",
                "file [file_name] - show contents of a file (<= 200 characters)\n",
                "sfile [file_name] [substring] - show whether a file contains a substring\n",
                "mkdir [directory_name] - create directory\n",
                "touch [file_name] - create file\n",
                "rmdir [directory_name] - delete directory\n",
                "rmfile [file_name] - delete file\n",
                "rndir [directory_name] [new_directory_name] - rename/move directory\n",
                "rnfile [file_name] [new_file_name] = rename/move file");

            return result;
        }
    }
}
