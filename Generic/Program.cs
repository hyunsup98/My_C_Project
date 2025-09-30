using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Stack의 핵심 요소
            //Push, Pop, Peek, Any
            //Any: 요소가 하나라도 존재하면 참을 반환한다.
            Stack<char> keyInputs = new Stack<char>();

            while (true)
            {
                var keyInput = Console.ReadKey(true);

                if (keyInput.KeyChar == (int)ConsoleKey.Enter) break;

                keyInputs.Push(keyInput.KeyChar);
            }

            //Queue 요소
            //Enqueue, Dequeue, Peek, Any
            Queue<char> queueInputs = new Queue<char>();

            //딕셔너리는 Key와 Value로 이루어진 자료구조
            Dictionary<int, string> dic = new Dictionary<int, string>();
            var inGameCharacters = new Dictionary<string, List<GameObject>>();
            
            ArrayList array = new ArrayList();
            List<int> intArray = new List<int>();

            //ArrayList의 문제점: object 형을 받는다
            //박싱 언박싱 필요
            //형변환 필요
            //연산 불가능

            array.Add(1);
            array.Remove(1);

            intArray.Add(1);
            intArray.Remove(1);
        }
    }

    class GameObject
    {
        public string Name { get; set; }
    }
    class Enemy : GameObject { }

    class Ally : GameObject { }
}