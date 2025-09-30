using System;

namespace Deque
{
    public class Deque<T>
    {
        private const int INITSIZE = 16;    //초기화 크기

        private T[] buffer;                 //객체를 담을 원형배열
        private int head;                   //제일 앞을 담당
        private int tail;                   //제일 뒤를 담당
        private int count;                  //현재 요소 개수
        public int Count
        {
            get { return count; }
            private set { count = value; }
        }

        public Deque()
        {
            buffer = new T[INITSIZE];
            head = 0;
            tail = 0;
            Count = 0;
        }

        public void AddFirst(T item)
        {
            Resize();
            head = (head - 1 + buffer.Length) % buffer.Length;
            buffer[head] = item;
            Count++;
        }

        public void AddLast(T item)
        {
            Resize();
            buffer[tail] = item;
            tail = (tail + 1) % buffer.Length;
            Count++;
        }

        public T RemoveFirst()
        {
            if (Count == 0)
                throw new Exception();

            var value = buffer[head];
            head = (head + 1) % buffer.Length;
            Count--;
            return value;
        }

        public T RemoveLast()
        {
            if (Count == 0)
                throw new Exception();

            tail = (tail - 1 + buffer.Length) % buffer.Length;
            var value = buffer[tail];
            Count--;
            return value;
        }

        //현재 배열의 크기보다 커져야 할 경우 새로 생성
        public void Resize()
        {
            //배열이 꽉 차면
            if(Count == buffer.Length)
            {
                var newBuf = new T[buffer.Length * 2];

                //새로 만든 버퍼에는 0부터 선형으로 담기게 함
                for (int i = 0; i < Count; i++)
                {
                    newBuf[i] = buffer[(head + i) % buffer.Length];
                }
                buffer = newBuf;
                head = 0;
                tail = Count;
            }
        }
    }
}
