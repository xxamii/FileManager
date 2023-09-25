using FileManager;
using System.IO;
using Xunit;

namespace Testing
{
    public class DirectoryServiceTests
    {
        [Fact]
        public void CurrentDirectoryPath_root()
        {
            string expected = @"E:\";

            DirectoryService directoryService = new DirectoryService();

            Assert.Equal(directoryService.CurrentDirectoryPath, expected);
        }

        [Fact]
        public void TryChangeDirectory_within_current()
        {
            string expected = @"E:\Dev";
            string toChange = "Dev";

            DirectoryService directoryService = new DirectoryService();
            directoryService.TryChangeDirectory(toChange);

            Assert.Equal(directoryService.CurrentDirectoryPath, expected);
        }

        [Fact]
        public void TryChangeDirectory_from_path()
        {
            string expected = @"C:\Program Files";
            string toChange = @"C:\Program Files";

            DirectoryService directoryService = new DirectoryService();
            directoryService.TryChangeDirectory(toChange);

            Assert.Equal(directoryService.CurrentDirectoryPath, expected);
        }

        [Fact]
        public void TryChangeDirectory_directory_does_not_exist()
        {
            string notExpected = @"E:\asdf";
            string toChange = @"asdf";

            DirectoryService directoryService = new DirectoryService();
            directoryService.TryChangeDirectory(toChange);

            Assert.NotEqual(directoryService.CurrentDirectoryPath, notExpected);
        }

        [Fact]
        public void TryChangeDirectory_directory_does_not_exist_from_path()
        {
            string notExpected = @"C:\asdf";
            string toChange = @"C:\asdf";

            DirectoryService directoryService = new DirectoryService();
            directoryService.TryChangeDirectory(toChange);

            Assert.NotEqual(directoryService.CurrentDirectoryPath, notExpected);
        }

        [Fact]
        public void GetCurrentDirectories_from_test_dir()
        {
            string testDirectory = @"E:\Dev\test";

            DirectoryService directoryService = new DirectoryService();
            directoryService.TryChangeDirectory(testDirectory);

            Assert.True(Directory.Exists(directoryService.TryGetCurrentDirectories()[0].FullName));
        }

        [Fact]
        public void GetCurrentFiles_from_test_dir()
        {
            string testDirectory = @"E:\Dev\test";

            DirectoryService directoryService = new DirectoryService();
            directoryService.TryChangeDirectory(testDirectory);

            Assert.True(File.Exists(directoryService.TryGetCurrentFiles()[0].FullName));
        }

        [Fact]
        public void GetFileContent_file_exists()
        {
            string testDirectory = @"E:\Dev\test";
            string testFilePath = @"file.txt";
            string expected = "abcdefghijklmnopqrstuvwxyz";

            DirectoryService directoryService = new DirectoryService();
            directoryService.TryChangeDirectory(testDirectory);

            string content = directoryService.TryGetFileContent(testFilePath);

            Assert.Equal(expected, content);
        }

        [Fact]
        public void GetFileContent_file_exists_from_path()
        {
            string testFilePath = @"E:\Dev\test\file.txt";
            string expected = "abcdefghijklmnopqrstuvwxyz";

            DirectoryService directoryService = new DirectoryService();

            string content = directoryService.TryGetFileContent(testFilePath);

            Assert.Equal(expected, content);
        }

        [Fact]
        public void GetFileContent_file_does_not_exist()
        {
            string testDirectory = @"E:\Dev\test";
            string testFilePath = @"sdgasfsdfasf.txt";
            string expected = "File sdgasfsdfasf.txt does not exist";

            DirectoryService directoryService = new DirectoryService();
            directoryService.TryChangeDirectory(testDirectory);

            string content = directoryService.TryGetFileContent(testFilePath);

            Assert.Equal(expected, content);
        }

        [Fact]
        public void GetFileContent_file_does_not_exist_from_path()
        {
            string testFilePath = @"E:\Dev\test\aasdfsdfa\asdlf\asldkfj.txt";
            string expected = @"File E:\Dev\test\aasdfsdfa\asdlf\asldkfj.txt does not exist";

            DirectoryService directoryService = new DirectoryService();

            string content = directoryService.TryGetFileContent(testFilePath);

            Assert.Equal(expected, content);
        }

        [Fact]
        public void SearchFile_contains_substring()
        {
            string testFilePath = @"E:\Dev\test\file.txt";
            string testSubstring = "defghij";
            string expected = @"File E:\Dev\test\file.txt contains substring: defghij";

            DirectoryService directoryService = new DirectoryService();

            string content = directoryService.TrySearchFile(testFilePath, testSubstring);

            Assert.Equal(expected, content);
        }

        [Fact]
        public void SearchFile_does_not_contain_substring()
        {
            string testFilePath = @"E:\Dev\test\file.txt";
            string testSubstring = "kobanchik";
            string expected = @"File E:\Dev\test\file.txt does not contain substring: kobanchik";

            DirectoryService directoryService = new DirectoryService();

            string content = directoryService.TrySearchFile(testFilePath, testSubstring);

            Assert.Equal(expected, content);
        }

        [Fact]
        public void SearchFile_file_does_not_exist()
        {
            string testFilePath = @"E:\Dev\test\sadfsdfa.txt";
            string testSubstring = "defghij";
            string expected = @"File E:\Dev\test\sadfsdfa.txt does not exist";

            DirectoryService directoryService = new DirectoryService();

            string content = directoryService.TrySearchFile(testFilePath, testSubstring);

            Assert.Equal(expected, content);
        }

        [Fact]
        public void CreateFile_file_does_not_exist_dir_exists()
        {
            string testFilePath = @"E:\Dev\test\testfile.txt";

            DirectoryService directoryService = new DirectoryService();

            directoryService.TryCreateFile(testFilePath);

            Assert.True(File.Exists(testFilePath));
        }

        [Fact]
        public void CreateFile_file_exists_dir_exists()
        {
            string testFilePath = @"E:\Dev\test\alreadyexists.txt";
            string expected = @"File E:\Dev\test\alreadyexists.txt already exists";

            DirectoryService directoryService = new DirectoryService();

            string content = directoryService.TryCreateFile(testFilePath);

            Assert.Equal(expected, content);
        }

        [Fact]
        public void CreateFile_file_does_not_exist_dir_does_not_exist()
        {
            string testFilePath = @"E:\Dev\test\doesnotexist\testfile.txt";

            DirectoryService directoryService = new DirectoryService();

            directoryService.TryCreateFile(testFilePath);

            Assert.True(File.Exists(testFilePath));
        }

        [Fact(Skip = "NotSupportedException is not thrown when testing, but is thrown on normal run")]
        public void CreateFile_invalid_name()
        {
            string testFilePath = @"E:\Dev\test\:?";
            string expected = @"Invalid path or file name: E:\Dev\test\:?";

            DirectoryService directoryService = new DirectoryService();

            string content = directoryService.TryCreateFile(testFilePath);

            Assert.Equal(expected, content);
        }

        [Fact]
        public void CreateDirectory_dir_does_not_exist()
        {
            string testDirPath = @"E:\Dev\test\testdir";

            DirectoryService directoryService = new DirectoryService();

            directoryService.TryCreateDirectory(testDirPath);

            Assert.True(Directory.Exists(testDirPath));
        }

        [Fact]
        public void CreateDirectory_dir_exists()
        {
            string testDirPath = @"E:\Dev\test\FolderOne";
            string expected = @"Directory E:\Dev\test\FolderOne already exists";

            DirectoryService directoryService = new DirectoryService();

            string content = directoryService.TryCreateDirectory(testDirPath);

            Assert.Equal(expected, content);
        }

        [Fact(Skip = "NotSupportedException is not thrown when testing, but is thrown on normal run")]
        public void CreateDirectory_invalid_name()
        {
            string testDirPath = @"E:\Dev\test\:?";
            string expected = @"Invalid path or directory name: E:\Dev\test\:?";

            DirectoryService directoryService = new DirectoryService();

            string content = directoryService.TryCreateDirectory(testDirPath);

            Assert.Equal(expected, content);
        }

        [Fact]
        public void DeleteFile_file_exists()
        {
            string testFilePath = @"E:\Dev\test\todelete.txt";

            DirectoryService directoryService = new DirectoryService();

            directoryService.TryDeleteFile(testFilePath);

            Assert.False(File.Exists(testFilePath));
        }

        [Fact]
        public void DeleteFile_file_does_not_exist()
        {
            string testFilePath = @"E:\Dev\test\asdcvgdf.txt";
            string expected = @"File E:\Dev\test\asdcvgdf.txt does not exist";

            DirectoryService directoryService = new DirectoryService();

            string content = directoryService.TryDeleteFile(testFilePath);

            Assert.Equal(expected, content);
        }

        [Fact]
        public void DeleteDirectory_dir_exists()
        {
            string testDirPath = @"E:\Dev\test\FolderThree";

            DirectoryService directoryService = new DirectoryService();

            directoryService.TryDeleteDirectory(testDirPath);

            Assert.False(Directory.Exists(testDirPath));
        }

        [Fact]
        public void DeleteDirectory_dir_does_not_exist()
        {
            string testDirPath = @"E:\Dev\test\asdcvgdf";
            string expected = @"Directory E:\Dev\test\asdcvgdf does not exist";

            DirectoryService directoryService = new DirectoryService();

            string content = directoryService.TryDeleteDirectory(testDirPath);

            Assert.Equal(expected, content);
        }

        [Fact]
        public void RenameFile_old_file_exists()
        {
            string testFilePath = @"E:\Dev\test\torename.txt";
            string testNewFilePath = @"E:\Dev\test\renamed.txt";

            DirectoryService directoryService = new DirectoryService();

            directoryService.TryRenameFile(testFilePath, testNewFilePath);

            Assert.False(File.Exists(testFilePath));
            Assert.True(File.Exists(testNewFilePath));
        }

        [Fact]
        public void RenameFile_old_file_does_not_exist()
        {
            string testFilePath = @"E:\Dev\test\doesnotexist.txt";
            string testNewFilePath = @"E:\Dev\test\renamed.txt";
            string expected = @"File E:\Dev\test\doesnotexist.txt does not exist";

            DirectoryService directoryService = new DirectoryService();

            string content = directoryService.TryRenameFile(testFilePath, testNewFilePath);

            Assert.Equal(expected, content);
            Assert.False(File.Exists(testFilePath));
        }

        [Fact]
        public void RenameFile_new_file_exists()
        {
            string testFilePath = @"E:\Dev\test\file.txt";
            string testNewFilePath = @"E:\Dev\test\newfile.txt";
            string expected = @"File E:\Dev\test\newfile.txt already exists";

            DirectoryService directoryService = new DirectoryService();

            string content = directoryService.TryRenameFile(testFilePath, testNewFilePath);

            Assert.Equal(expected, content);
            Assert.True(File.Exists(testNewFilePath));
        }

        [Fact]
        public void RenameDirectory_old_dir_exists()
        {
            string testDirPath = @"E:\Dev\test\torename";
            string testNewFilePath = @"E:\Dev\test\renamed";

            DirectoryService directoryService = new DirectoryService();

            directoryService.TryRenameDirectory(testDirPath, testNewFilePath);

            Assert.False(Directory.Exists(testDirPath));
            Assert.True(Directory.Exists(testNewFilePath));
        }

        [Fact]
        public void RenameDirectory_old_dir_does_not_exist()
        {
            string testFilePath = @"E:\Dev\test\asdfasfdsf";
            string testNewFilePath = @"E:\Dev\test\renamed.txt";
            string expected = @"Directory E:\Dev\test\asdfasfdsf does not exist";

            DirectoryService directoryService = new DirectoryService();

            string content = directoryService.TryRenameDirectory(testFilePath, testNewFilePath);

            Assert.Equal(expected, content);
            Assert.False(Directory.Exists(testFilePath));
        }

        [Fact]
        public void RenameDirectory_new_dir_exists()
        {
            string testFilePath = @"E:\Dev\test\test";
            string testNewFilePath = @"E:\Dev\test\FolderOne";
            string expected = @"Directory E:\Dev\test\FolderOne already exists";

            DirectoryService directoryService = new DirectoryService();

            string content = directoryService.TryRenameDirectory(testFilePath, testNewFilePath);

            Assert.Equal(expected, content);
            Assert.True(Directory.Exists(testNewFilePath));
        }
    }
}
