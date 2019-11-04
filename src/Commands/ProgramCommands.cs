using System;
using System.Collections.Generic;
using Console.CommandHandler;
using Console.CommandHandler.Commands;
//using CommandManager;
//using CommandManager.Commands;
using MSSQLClrExtract.Exporter;

namespace ClrExtract.Commands
{
    public class ProgramCommands : ICommandContainer
    {

        [DefaultCommand("l", "list", "list all CLR assemblies from a specified database")]
        public Func<IEnumerable<DBAssembly>> ListAssemblies => DBExporter.ListAssemblies;

        [DefaultCommand("s", "save", "saves a specific CLR assemblies from a specified database")]
        public Action<string, string> SaveAssembly => DBExporter.SaveAssembly;




        [DefaultCommand("bc", "buildconstring", "allows you to modify the connection string for a specified database")]
        public Action ConStringBuilder => () =>
        {
            Handler.StepIn("Connection String Builder");

            Handler.AddCommands(new[] { new ConnectionStringBuilderCommands() });

            Handler.Listen();
        };


        [DefaultCommand("cs", "constring", "returns the connection string")]
        public Func<string> GetConnectionString => () =>
        {
            return $"data source={Program.ConnString.DataSource};User ID={Program.ConnString.UserID};Password={Program.ConnString.Password};initial catalog={Program.ConnString.Catalog};";
        };

    }
}
