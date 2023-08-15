using System;
using System.Threading;

namespace Homework_7
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Repository rep1 = new Repository();
            
            //rep1.CreateRandomDB(100);     //создание базы данных со случайными работниками

            rep1.PrintAll();
            Console.WriteLine();
            Console.ReadKey();
            rep1.SortWorkers(4); //сортировка по четвертому полю (возраст)
            rep1.RefreshFile();
            rep1.PrintAll();


        }

        
        
    }
}
