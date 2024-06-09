using System;
using System.Threading;

class Program
{
    private static Mutex mutex = new Mutex();
    private static bool firstThreadCompleted = false;

    static void Main()
    {
        
        Thread firstThread = new Thread(DisplayAscending);
        Thread secondThread = new Thread(DisplayDescending);

       
        firstThread.Start();
        secondThread.Start();

        
        firstThread.Join();
        secondThread.Join();
    }

    private static void DisplayAscending()
    {
        mutex.WaitOne(); 

        for (int i = 0; i <= 20; i++)
        {
            Console.WriteLine($"Перший потік: {i}");
            Thread.Sleep(100); 
        }

        firstThreadCompleted = true;
        mutex.ReleaseMutex(); 
    }

    private static void DisplayDescending()
    {
        while (true)
        {
            mutex.WaitOne(); 

            if (firstThreadCompleted)
            {
                for (int i = 10; i >= 0; i--)
                {
                    Console.WriteLine($"Другий потік: {i}");
                    Thread.Sleep(100); 
                }

                mutex.ReleaseMutex(); 
                break;
            }

            mutex.ReleaseMutex(); 
            Thread.Sleep(100); 
        }
    }
}

