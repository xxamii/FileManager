# FileManager

A custom-made console application to manage files with commands. A simplified clone of Windows terminal. It was developed for my .NET Bootcamp during my first year of university.

It was originally developed in my [DotnetBootcampYakovliev](https://github.com/xxamii/DotnetBootcampYakovliev) repository, but I copied it here for more convenient viewing.

# What I Learned

- How to use the System.IO library and Directory, DirectoryInfo, File, and FileInfo classes.

# How to Use It

- Download the repository, and unzip it.
- Open the solution inside Visual Studio.
- Build the project and run it.

### Commands

    help - list all available commands
    ls - list the current directory contents
        -s - sort by name, ascending
        -sd - sort by name, descending
        -nh - hide hidden files
        -f - show full information about a file or a directory
    cd [directory_name] - move into said directory
    file [file_name] - show contents of a file (<= 200 characters)
    sfile [file_name] [substring] - show whether a file contains a substring
    mkdir [directory_name] - create directory
    touch [file_name] - create file
    rmdir [directory_name] - delete directory
    rmfile [file_name] - delete file
    rndir [directory_name] [new_directory_name] - rename/move directory
    rnfile [file_name] [new_file_name] = rename/move file
