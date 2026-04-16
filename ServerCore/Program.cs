using System.Reflection.Metadata;

namespace ServerCore
{
    internal class Program
    {
        static int _answer;
        static bool _complete;
        static void A()
        {
            _answer = 123;
            Thread.MemoryBarrier(); // store _answer before _complete
            _complete = true;
            Thread.MemoryBarrier(); // flush _complete to memory
        }

        static void B()
        {
            Thread.MemoryBarrier(); // read latest _complete from memory
            if(_complete)
            {
                Thread.MemoryBarrier(); // read latest _answer from memory
                System.Console.WriteLine(_answer);
            }
        }
        static void Main(string[] args)
        {
            Task t1 = new Task(A);
            Task t2 = new Task(B);
            t1.Start();
            t2.Start();
            Task.WaitAll(t1, t2);
        }
    }
}
