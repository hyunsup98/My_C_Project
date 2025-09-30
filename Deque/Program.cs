using System;

namespace Deque
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Deque<int> deque = new Deque<int>();


            deque.AddFirst(5);
            deque.AddFirst(7);
            deque.AddFirst(1);
            deque.AddLast(2);
            deque.AddLast(8);

            Console.WriteLine($"1. RemoveFirst: {deque.RemoveFirst()}");    //1. RemoveFirst: 1
            Console.WriteLine($"2. RemoveFirst: {deque.RemoveFirst()}");    //2. RemoveFirst: 7
            Console.WriteLine($"3. RemoveLast: {deque.RemoveLast()}");      //3. RemoveLast: 8

        }
    }
}
