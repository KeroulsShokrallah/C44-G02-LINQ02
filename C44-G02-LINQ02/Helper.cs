using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C44_G02_LINQ02
{
    internal class Helper
    {
        public static void Print<T>(List<T> arrays)
        {
            foreach (var item in arrays)
            {
                Console.WriteLine(item);
            }
        }
    }
}
