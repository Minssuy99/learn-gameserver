namespace ServerCore
{
    internal class Program
    {
        static void PrintMessage()
        {
            for (int i = 0; i < 5; i++)
                Console.WriteLine($"Hello Thread! {i}");
        }

        static void PrintMessageWithState(object state)
        {
            for (int i = 0; i < 5; i++)
                Console.WriteLine($"Hello Thread! {i}");
        }

        static void ThreadExample()
        {
            Thread t = new Thread(PrintMessage);
            t.Name = "MyThread";
            t.IsBackground = true;
            t.Start();
            t.Join();
        }

        static void ThreadPoolExample()
        {
            ThreadPool.QueueUserWorkItem(PrintMessageWithState);
            Thread.Sleep(1000);
        }

        static void TaskExample()
        {
            Task t = new Task(PrintMessage);
            t.Start();
            t.Wait();
        }

        static void Main(string[] args)
        {
            ThreadExample();
            //ThreadPoolExample();
            //TaskExample();
        }
    }
}
