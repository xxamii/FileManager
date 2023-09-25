using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public class CommandReader
    {
        private CommandResolver _commandResolver;
        private DirectoryWorker _directoryWorker;

        public CommandReader()
        {
            _directoryWorker = new DirectoryWorker();
            _commandResolver = new CommandResolver(_directoryWorker);
        }

        public void StartSession()
        {
            while(true)
            {
                Console.Write($"{_directoryWorker.CurrentDirectoryPath}: ");
                string command = Console.ReadLine();

                string result = _commandResolver.ResolveCommand(command);

                Console.WriteLine(result);
            }
        }
    }
}
