using System;
using System.IO;
using Xunit;
using Collections;
using FileManager;

namespace Testing
{
    public class DirectoryWorkerTests
    {
        [Fact]
        public void SortByName_directories()
        {
            List<DirectoryInfo> testDirectories = new List<DirectoryInfo>(3);

            testDirectories[0] = new DirectoryInfo(@"E:\Dev\test_alphabet\j");
            testDirectories[1] = new DirectoryInfo(@"E:\Dev\test_alphabet\b");
            testDirectories[2] = new DirectoryInfo(@"E:\Dev\test_alphabet\a");

            string expected = "a";

            DirectoryWorker directoryWorker = new DirectoryWorker();

            testDirectories = directoryWorker.SortByName(testDirectories);

            Assert.Equal(expected, testDirectories[0].Name);
        }

        [Fact]
        public void SortByName_files()
        {
            List<FileInfo> testFiles = new List<FileInfo>(3);

            testFiles[0] = new FileInfo(@"E:\Dev\test_alphabet\j.txt");
            testFiles[1] = new FileInfo(@"E:\Dev\test_alphabet\b.txt");
            testFiles[2] = new FileInfo(@"E:\Dev\test_alphabet\a.txt");

            string expected = "a.txt";

            DirectoryWorker directoryWorker = new DirectoryWorker();

            testFiles = directoryWorker.SortByName(testFiles);

            Assert.Equal(expected, testFiles[0].Name);
        }

        [Fact]
        public void SortByNameDescending_directories()
        {
            List<DirectoryInfo> testDirectories = new List<DirectoryInfo>(3);

            testDirectories[0] = new DirectoryInfo(@"E:\Dev\test_alphabet\j");
            testDirectories[1] = new DirectoryInfo(@"E:\Dev\test_alphabet\b");
            testDirectories[2] = new DirectoryInfo(@"E:\Dev\test_alphabet\a");

            string expected = "j";

            DirectoryWorker directoryWorker = new DirectoryWorker();

            testDirectories = directoryWorker.SortByNameDescending(testDirectories);

            Assert.Equal(expected, testDirectories[0].Name);
        }

        [Fact]
        public void SortByNameDescending_files()
        {
            List<FileInfo> testFiles = new List<FileInfo>(3);

            testFiles[0] = new FileInfo(@"E:\Dev\test_alphabet\j.txt");
            testFiles[1] = new FileInfo(@"E:\Dev\test_alphabet\b.txt");
            testFiles[2] = new FileInfo(@"E:\Dev\test_alphabet\a.txt");

            string expected = "j.txt";

            DirectoryWorker directoryWorker = new DirectoryWorker();

            testFiles = directoryWorker.SortByNameDescending(testFiles);

            Assert.Equal(expected, testFiles[0].Name);
        }

        [Fact]
        public void FilterHidden_directories()
        {
            List<DirectoryInfo> testDirectories = new List<DirectoryInfo>(3);

            testDirectories[0] = new DirectoryInfo(@"E:\Dev\test_alphabet\j");
            testDirectories[0].Attributes = FileAttributes.Hidden;

            testDirectories[1] = new DirectoryInfo(@"E:\Dev\test_alphabet\b");
            testDirectories[2] = new DirectoryInfo(@"E:\Dev\test_alphabet\a");

            int expected = 2;

            DirectoryWorker directoryWorker = new DirectoryWorker();

            testDirectories = directoryWorker.FilterHidden(testDirectories);

            Assert.Equal(expected, testDirectories.Length);
        }

        [Fact]
        public void FilterHidden_files()
        {
            List<FileInfo> testFiles = new List<FileInfo>(3);

            testFiles[0] = new FileInfo(@"E:\Dev\test_alphabet\j.txt");
            testFiles[1] = new FileInfo(@"E:\Dev\test_alphabet\b.txt");
            testFiles[2] = new FileInfo(@"E:\Dev\test_alphabet\a.txt");

            File.SetAttributes(@"E:\Dev\test_alphabet\a.txt", FileAttributes.Hidden);

            int expected = 2;

            DirectoryWorker directoryWorker = new DirectoryWorker();

            testFiles = directoryWorker.FilterHidden(testFiles);

            Assert.Equal(expected, testFiles.Length);
        }

        [Fact]
        public void GetFileContent_more_than_200()
        {
            string filePath = @"E:\Dev\test_length\morethantwohundred.txt";
            int expected = 200;

            DirectoryWorker directoryWorker = new DirectoryWorker();

            string content = directoryWorker.TryGetFileContent(filePath);

            Assert.Equal(expected, content.Length);
        }

        [Fact]
        public void GetFileContent_less_than_200()
        {
            string filePath = @"E:\Dev\test_length\lessthantwohundred.txt";
            int expected = 199;

            DirectoryWorker directoryWorker = new DirectoryWorker();

            string content = directoryWorker.TryGetFileContent(filePath);

            Assert.InRange<int>(content.Length, 0, expected);
        }

        [Fact]
        public void GetFileContent_file_does_not_exist()
        {
            string filePath = @"E:\Dev\test_length\doesnotexist.txt";
            string expected = @"File E:\Dev\test_length\doesnotexist.txt does not exist";

            DirectoryWorker directoryWorker = new DirectoryWorker();

            string content = directoryWorker.TryGetFileContent(filePath);

            Assert.Equal(expected, content);
        }

        [Fact]
        public void FormatSize_B()
        {
            long bytes = 1;
            string expected = "1 B";

            DirectoryWorker directoryWorker = new DirectoryWorker();
            string content = directoryWorker.FormatSize(bytes);

            Assert.Equal(expected, content);
        }

        [Fact]
        public void FormatSize_KB()
        {
            long bytes = 1024;
            string expected = "1 KB";

            DirectoryWorker directoryWorker = new DirectoryWorker();
            string content = directoryWorker.FormatSize(bytes);

            Assert.Equal(expected, content);
        }

        [Fact]
        public void FormatSize_MB()
        {
            long bytes = 1048576;
            string expected = "1 MB";

            DirectoryWorker directoryWorker = new DirectoryWorker();
            string content = directoryWorker.FormatSize(bytes);

            Assert.Equal(expected, content);
        }

        [Fact]
        public void FormatSize_GB()
        {
            long bytes = 1073741824;
            string expected = "1 GB";

            DirectoryWorker directoryWorker = new DirectoryWorker();
            string content = directoryWorker.FormatSize(bytes);

            Assert.Equal(expected, content);
        }

        [Fact]
        public void FormatSize_TB()
        {
            long bytes = 1099511627776;
            string expected = "1 TB";

            DirectoryWorker directoryWorker = new DirectoryWorker();
            string content = directoryWorker.FormatSize(bytes);

            Assert.Equal(expected, content);
        }

        [Fact]
        public void FormatSize_less_than_0()
        {
            long bytes = -10;
            string expected = "0 B";

            DirectoryWorker directoryWorker = new DirectoryWorker();
            string content = directoryWorker.FormatSize(bytes);

            Assert.Equal(expected, content);
        }
    }
}
