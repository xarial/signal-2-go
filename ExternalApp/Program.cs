using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ExternalApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = args[0];
            var content = args[1];

            File.WriteAllText(filePath, content);
        }
    }
}
