using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    private static Mutex mutex = new Mutex();
    private static int[] dataArray;
    private static bool isModified = false;
    private static int maxValue = 0;

    static void Main()
    {
       
        dataArray = new int[] { 1, 2, 3, 4, 5 };

       
        Task modifyTask = Task.Run(() => ModifyArray());
        Task findMaxTask = Task.Run(() => FindMaxValue());

        
        Task.WaitAll(modifyTask, findMaxTask);

        
        Console.WriteLine("Модифікований масив:");
        foreach (var item in dataArray)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine($"Максимальне значення в масиві: {maxValue}");
    }

    private static void ModifyArray()
    {
        Random random = new Random();
        mutex.WaitOne();

        for (int i = 0; i < dataArray.Length; i++)
        {
            int randomValue = random.Next(1, 10);
            dataArray[i] += randomValue;
            Console.WriteLine($"Перший потік: Модифікований елемент {i} = {dataArray[i]}");
        }

        isModified = true;
        mutex.ReleaseMutex();
    }

    private static void FindMaxValue()
    {
        while (true)
        {
            mutex.WaitOne();

            if (isModified)
            {
                maxValue = dataArray[0];
                for (int i = 1; i < dataArray.Length; i++)
                {
                    if (dataArray[i] > maxValue)
                    {
                        maxValue = dataArray[i];
                    }
                }
                mutex.ReleaseMutex();
                break;
            }

            mutex.ReleaseMutex();
            Thread.Sleep(100);
        }
    }
}

