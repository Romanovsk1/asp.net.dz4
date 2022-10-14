using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Extensions
{
    public static class ListExtensions
    {
        public static List<int> GetEvenNumbers(this List<int> list)
        {
            return list.FindAll(x => x % 2 == 0);
        }
    }
}