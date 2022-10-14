using ConsoleApp1.Extensions;

namespace ConsoleApp1
{
    internal class Program
    {

        //static bool IsEven(int x)
        //{
        //    return x % 2 == 0;
        //}

        static void Main(string[] args)
        {
            List<int> list = new List<int>(new int[] { 12, 1, 4, 5, -5, -7, -11, 22, 1 });

            //list.FindAll(IsEven);

            list.FindAll(delegate (int x)
            {
                return x % 2 == 0;
            });

            list.FindAll(x =>
            {
                return x % 2 == 0;
            });

            list.FindAll(x => x % 2 == 0);

            list.GetEvenNumbers();


        }
    }
}