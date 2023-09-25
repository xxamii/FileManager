using System;
using System.IO;
using Collections;

namespace FileManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            CommandReader commandReader = new CommandReader();

            commandReader.StartSession();
        }
    }
}
