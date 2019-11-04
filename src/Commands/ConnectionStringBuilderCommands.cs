using System;
using Console.CommandHandler;
using Console.CommandHandler.Commands;

namespace ClrExtract.Commands
{
    public class ConnectionStringBuilderCommands : ICommandContainer
    {
        [DefaultCommand("ds", "datasource", "set the connection string")]
        public Func<string, string> DataSource => SetDataSource;

        [DefaultCommand("uid", "userid", "set the userid", "['the user id', 'test']")]
        public Func<string, string> UserID => SetUserID;

        [DefaultCommand("p", "password", "set the password")]
        public Func<string, string> Password => SetPassword;

        [DefaultCommand("cl", "catalog", "set the catalog")]
        public Func<string, string> Catalog => SetCatalog;


        public static string SetDataSource(string datasource)
        {
            Program.ConnString.DataSource = datasource;
            return $"data source={Program.ConnString.DataSource};User ID={Program.ConnString.UserID};Password={Program.ConnString.Password};initial catalog={Program.ConnString.Catalog};";
        }

        public static string SetUserID(string userid)
        {
            Program.ConnString.UserID = userid;
            return $"data source={Program.ConnString.DataSource};User ID={Program.ConnString.UserID};Password={Program.ConnString.Password};initial catalog={Program.ConnString.Catalog};";
        }

        public static string SetPassword(string userid)
        {
            Program.ConnString.Password = userid;
            return WriteConString();
        }
        public static string SetCatalog(string userid)
        {
            Program.ConnString.Catalog = userid;
            return WriteConString();
        }

        private static string WriteConString()
        {
            return $"data source={Program.ConnString.DataSource};User ID={Program.ConnString.UserID};Password={Program.ConnString.Password};initial catalog={Program.ConnString.Catalog};";
        }

    }
}
