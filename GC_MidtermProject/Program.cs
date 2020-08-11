
using System;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace GC_MidtermProject
{
    class Program
    {
        static void Main(string[] args)
        {
            POSController C = new POSController();

            C.RunProgram();
        }
    }
}

