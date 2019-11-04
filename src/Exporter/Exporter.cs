using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ClrExtract;

namespace MSSQLClrExtract.Exporter
{
    public class DBExporter
    {
        public static IEnumerable<DBAssembly> ListAssemblies()
        {
            const string sql = @"SELECT a.name as Name FROM sys.assemblies a INNER JOIN sys.assembly_files af ON a.assembly_id = af.assembly_id";

            using var conn = new SqlConnection($"data source={Program.ConnString.DataSource};User ID={Program.ConnString.UserID};Password={Program.ConnString.Password};initial catalog={Program.ConnString.Catalog};");

            using var cmd = new SqlCommand(sql, conn);

            cmd.Connection.Open();

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                yield return new DBAssembly() { Name = reader.GetString("Name") };
            }
            conn.Close();
        }


        public static void SaveAssembly(string assemblyName, string destinationPath)
        {
            //Pavel Pawlowski  
            const string sql = @"SELECT a.name , af.content FROM sys.assemblies a 
                                INNER JOIN sys.assembly_files af ON a.assembly_id = af.assembly_id 
                                WHERE a.name = @assemblyName";

            using var conn = new SqlConnection($"data source={Program.ConnString.DataSource};User ID={Program.ConnString.UserID};Password={Program.ConnString.Password};initial catalog={Program.ConnString.Catalog};");

            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add(new SqlParameter("@assemblyName", SqlDbType.VarChar) { Value = assemblyName });

            cmd.Connection.Open();

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var bytes = reader.GetSqlBytes(1);

                var outputFile = Path.Combine(destinationPath, $"{ reader.GetString("name") }.dll");

                using var byteStream = new FileStream(outputFile, FileMode.CreateNew);

                byteStream.Write(bytes.Value, 0, (int)bytes.Length);
                byteStream.Close();
            }
            conn.Close();
        }
    }
}
