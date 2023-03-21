using System;
using System.Text;
using System.Threading;

class Program
{
    static Mutex mutex = new Mutex(); // створюємо об'єкт Mutex.
    static void Main(string[] args)
    {
        Console.OutputEncoding = UTF8Encoding.UTF8;
        Thread neparThread = new Thread(NeparNumbers);
        Thread parThread = new Thread(ParNumbers);

        neparThread.Start();
        parThread.Start();

        neparThread.Join();
        parThread.Join();

        mutex.Dispose(); // звільняємо ресурси Mutex.
    }

    static void NeparNumbers()
    {
        for (int i = 1; i <= 99; i += 2)
        {
            mutex.WaitOne(); // отримуємо блокування Mutex.
            Console.WriteLine($"Непарні : {i}");
            mutex.ReleaseMutex(); // звільняємо блокування Mutex.
        }
    }

    static void ParNumbers()
    {
        for (int i = 2; i <= 100; i += 2)
        {
            mutex.WaitOne();
            Console.WriteLine($"Парні : {i}");
            mutex.ReleaseMutex();
        }
    }
}