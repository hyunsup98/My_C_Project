using System;
using System.Collections.Generic;

namespace _250924
{
    public class VendingMachine<T> where T : Drink
    {
        private Queue<T> queue = new Queue<T>();

        #region 입출력 메서드
        //큐에 집어넣는 기능
        /// <summary>
        /// 제네릭(T) 타입의 객체를 Queue에 추가합니다.
        /// </summary>
        /// <param name="milk">추가할 제네릭(T) 타입 객체</param>
        public void GetObject(T obj)
        {
            if (obj == null || queue == null) return;

            queue.Enqueue(obj);
        }

        //큐에서 꺼내는 기능 (단, 우유의 유통기한, 큐에 남아있는 우유 개수 출력) 개수가 0일때 다른 멘트 나오게 함
        /// <summary>
        /// 제네릭(T) 타입의 객체를 받아옵니다.
        /// </summary>
        /// <returns>큐에서 반환된 객체</returns>
        public virtual T TakeObject()
        {
            //만약 큐에서 꺼낼 데이터가 없는 경우
            if(queue.Count == 0)
            {
                Console.WriteLine("꺼낼 데이터가 더 이상 없습니다.");
                return null;
            }

            T obj = queue.Dequeue();
            Console.WriteLine($"현재 자판기에 남은 개수는 {queue.Count}개 입니다.");

            return obj;
        }
        #endregion
    }
}
