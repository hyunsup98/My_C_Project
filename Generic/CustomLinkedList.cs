using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Generic
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Next { get; set; }


        public Node(T value)
        {
            Value = value;
            Next = null;
        }
    }

    public class LinkedList2<T>
    {
        public int Count { get; private set; }       //총 노드의 개수를 담당
        public Node<T> Head { get; private set; }

        public LinkedList2()
        {
            Head = null;
            Count = 0;
        }

        public void AddLast(T value)
        {
            Node<T> newNode = new Node<T>(value);

            if (Head == null)
            {
                Head = newNode;
            }
            else
            {
                Node<T> current = Head;

                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }

            Count++;
        }

        public bool Remove(T value)
        {
            if (Head == null) return false;

            if(Head.Value.Equals(value))
            {
                Head = Head.Next;
                Count--;

                return true;
            }

            Node<T> current = Head;

            while(current.Next != null && !current.Next.Value.Equals(value))
            {
                current = current.Next;
            }

            if (current.Next == null) return false;

            current.Next = current.Next.Next;
            Count--;

            return true;
        }

        public Node<T> Find(T value)
        {
            Node<T> current = Head;

            while (current.Next != null)
            {
                if (current.Value.Equals(value))
                    return current;

                current = current.Next;
            }

            return null;
        }
    }
}
