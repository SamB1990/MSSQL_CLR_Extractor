using System;
using System.Collections.Generic;
using System.Text;

namespace MSSQLClrExtract.Exporter
{
    public class DBAssembly
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Assembly Name: { Name }";
        }
    }
}
