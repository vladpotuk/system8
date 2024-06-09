using System;
using System.Threading;

namespace system7
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isNewInstance;
            string mutexName = "SingleInstanceAppMutex";

            using (Mutex mutex = new Mutex(true, mutexName, out isNewInstance))
            {
                if (isNewInstance)
                {
                    Console.WriteLine("Додаток запущено. Натисніть будь-яку клавішу для виходу.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Додаток вже запущено. Друга копія не може бути запущена.");
                }
            }
        }
    }
}

