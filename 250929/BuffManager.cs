using System;
using System.Collections.Generic;

namespace _250929
{
    /// <summary>
    /// 모든 버프를 관리하는 클래스
    /// </summary>
    public class BuffManager
    {
        //버프 리스트
        public List<Buff> buffList = new List<Buff>();

        //디버프 리스트
        public List<Buff> debuffList = new List<Buff>();

        #region 버프 추가 메서드
        /// <summary>
        /// 버프 객체를 받아와 맞는 리스트(버프 리스트 / 디버프 리스트)에 추가
        /// </summary>
        /// <param name="buff"></param>
        public void AddBuff(Buff buff)
        {
            if (buff == null) return;

            if(buff.buffType == BuffType.PositiveBuff)
            {
                //버프 타입이 이로운 버프일 경우 buffList에 추가
                CheckBuffExists(buffList, buff);
                Console.WriteLine("buffList에 추가");
            }
            else
            {
                //버프 타입이 해로운 버프일 경우 debuffList에 추가
                CheckBuffExists(debuffList, buff);
                Console.WriteLine("debuffList에 추가");
            }
        }

        private void CheckBuffExists(List<Buff> list, Buff buff)
        {
            if(list.Contains(buff))
            {
                //이미 리스트에 있는 버프일 경우 갱신만
                var index = list.IndexOf(buff);
                list[index].InitBuff();
            }
            else
            {
                //리스트에 없는 새로운 버프일 경우 추가
                list.Add(buff);
            }
        }
        #endregion

        #region 버프 업데이트 메서드
        public void UpdateBuffs()
        {
            //버프 갱신
            foreach(var buff in buffList)
            {
                buff.Buff_Duration -= 0.1f;
            }

            //디버프 갱신
            foreach(var debuff in debuffList)
            {
                debuff.Buff_Duration -= 0.1f;
            }
        }
        #endregion

        #region 적용중인 버프 보여주는 메서드
        public void ShowActiveBuffs()
        {
            Console.Clear();

            int x = Console.WindowWidth - (Console.WindowWidth / 2);
            int y = 0;

            string title = "[버프]";
            Console.SetCursorPosition(15 - (title.Length / 2), y++);
            Console.WriteLine(title);
            foreach (var buff in buffList)
            {
                ShowBuffFormat(buff, 0, y);
                y += 6;
            }

            y = 0;

            title = "[디버프]";
            Console.SetCursorPosition(x + 15 - (title.Length / 2), y++);
            Console.WriteLine(title);
            foreach (var debuff in debuffList)
            {
                Console.SetCursorPosition(x, y);
                ShowBuffFormat(debuff, x, y);
                y += 6;
            }

            Console.SetCursorPosition(0, 34);
            Console.WriteLine("1. 스피드업 버프 추가 or 갱신");
            Console.SetCursorPosition(0, 36);
            Console.WriteLine("2. 무적     버프 추가 or 갱신");
            Console.SetCursorPosition(0, 38);
            Console.WriteLine("3. 사거리   버프 추가 or 갱신");
            Console.SetCursorPosition(0, 40);
            Console.WriteLine("4. 리퍼     상향 추가 or 갱신");

            Console.SetCursorPosition(x, 34);
            Console.WriteLine("5. 독       디버프 추가 or 갱신");
            Console.SetCursorPosition(x, 36);
            Console.WriteLine("6. 기절     디버프 추가 or 갱신");
            Console.SetCursorPosition(x, 38);
            Console.WriteLine("7. 화상     디버프 추가 or 갱신");
            Console.SetCursorPosition(x, 40);
            Console.WriteLine("8. 빙결     디버프 추가 or 갱신");
        }

        private void ShowBuffFormat(Buff buff, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine("==============================");
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine($"버프 이름:\t{buff.buff_Name}");
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine($"남은 지속시간:\t{(buff.Buff_Duration > 0 ? buff.Buff_Duration.ToString() : "버프 종료")}");
            Console.SetCursorPosition(x, y + 3);
            Console.WriteLine("==============================\n");
        }
        #endregion
    }
}
