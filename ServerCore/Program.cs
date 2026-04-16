namespace ServerCore
{
    internal class Program
    {
        static volatile int x = 0;
        static volatile int y = 0;
        static volatile int result1 = 0;
        static volatile int result2 = 0;

        static void Thread_1()
        {
            y = 1; // store y
            Thread.MemoryBarrier();
            result1 = x; // load x
        }

        static void Thread_2()
        {
            x = 1; // store x
            Thread.MemoryBarrier();
            result2 = y; // load y
        }

        static void Main(string[] args)
        {
            int count = 0;
            while(true)
            {
                count++;

                x = y = result1 = result2 = 0;

                Task t1 = new Task(Thread_1);
                Task t2 = new Task(Thread_2);

                t1.Start();
                t2.Start();

                Task.WaitAll(t1, t2);

                if (result1 == 0 && result2 == 0)
                {
                    break;
                }
            }
            System.Console.WriteLine($"count : {count}");
        }
    }
}
