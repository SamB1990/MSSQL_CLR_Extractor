using System;
using ClrExtract.Commands;
using Console.CommandHandler;

namespace ClrExtract
{
    class Program
    {
        public static ConnectionString ConnString = new ConnectionString();

        static void Main()
        {
            ConsoleX.WriteTitle("MS SQL CLR Assembly Extractor");
            
            Handler.AddCommands(new ICommandContainer[] { new ProgramCommands() });
            
            ConsoleX.WriteLine("Use the commands to build a connection string", ConsoleX.MessageStatus.Info);

            ConsoleX.WriteLine("Once complete use -c to return to the exporter.", ConsoleX.MessageStatus.Info);

            Handler.Execute("-bc");

            Handler.Listen();

        }

    }

}
