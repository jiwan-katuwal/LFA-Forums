using LFA.Forum.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFA.Fourm.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine(Logger.Log("console app",DateTime.Now));
            Console.ReadLine();
        }
    }
}
