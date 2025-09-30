using System;

namespace _250929
{
    /// <summary>
    /// 버프 관리 프로그램
    /// 
    /// -조작법-
    /// A키    : 0.1초씩 게임 시간 흐름
    /// 1~8번키: 버프/디버프 적용
    /// 
    /// ● Enum의 [Flags] 속성 VS Buff 클래스
    ///   ○ Enum의 [Flags] 속성으로 버프를 관리하는 방법은 버프의 적용 유무 체크 등 최소한의 판단을 하는 경우에만 효율적이라고 생각합니다.
    ///   ○ 따라서 버프 관련 기능이 여럿 필요하기에 Buff 클래스를 통해 관리하는 것이 좋다고 판단했습니다.
    ///   
    /// ● Buff(추상 클래스) - PositiveBuff/NegativeBuff(추상 클래스) - Buff_...(객체가 될 클래스) 구조
    ///   ○ 모든 버프의 공통 기능을 수행하는 코드들을 Buff 클래스에서 구현했습니다.
    ///   ○ 버프는 크게 '이로운 버프' / '해로운 디버프' 로 나뉘기 때문에 각각의 공통 기능을 담는 추상 클래스를 만들었습니다.
    ///   ○ 이후에 각 버프/디버프들을 상속을 통해 클래스로 만들었습니다.
    ///   
    /// ● BuffManager에서 버프를 관리하는 자료구조로 어떤 것이 좋을까?
    ///   ○ 버프를 계속 삽입/삭제를 통해 관리하는 경우에는 LinkedList가 좋다고 생각합니다.
    ///   ○ 하지만 대부분의 게임에서 동일한 종류의 버프/디버프는 중첩되지 않으며, 저 또한 이와 같은 버프 구조를 구상하며 만들었습니다.
    ///   ○ 버프가 종류당 하나만 적용이 된다면 여러 객체를 만들 필요가 없기 때문에, List를 통해 버프 종류당 하나만을 저장한 뒤
    ///     버프를 다시 추가할 때 초기화를 통해 재사용하는 방식으로 만들었습니다.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            //콘솔창 크기 조정
            Console.SetWindowSize(150, 60);
            Console.WriteLine("A키를 눌러 시작하세요.\n\n" +
                "1 ~ 8번 키를 눌러 버프/디버프를 추가할 수 있습니다.\n\n" +
                "버프/디버프를 추가한 뒤 A키를 통해 버프 시간 흐름을 확인할 수 있습니다.");

            #region 버프 클래스 객체 생성
            //버프 객체 생성
            PositiveBuff buff_SpeedUp = new Buff_SpeedUp("이속 증가", 5f);
            PositiveBuff buff_Invincible = new buff_Invincible("무적", 2.5f);
            PositiveBuff buff_RangeUp = new buff_RangeUp("사거리 증가", 5.5f);
            PositiveBuff buff_Reaper = new buff_Reaper("리퍼 상향좀요..", 999f);

            //디버프 객체 생성
            NegativeBuff debuff_Poisoned = new debuff_Poisoned("독 상태", 3.7f);
            NegativeBuff debuff_Stunned = new debuff_Stunned("기절 상태", 1.8f);
            NegativeBuff debuff_Burning = new debuff_Burning("화상 상태", 5.6f);
            NegativeBuff debuff_Frozen = new debuff_Frozen("빙결 상태", 2.3f);
            #endregion

            #region 버프/디버프 입력받기
            BuffManager buffManager = new BuffManager();

            while(true)
            {
                var input = Console.ReadKey(true);

                switch(input.Key)
                {
                    case ConsoleKey.A:
                        buffManager.UpdateBuffs();
                        buffManager.ShowActiveBuffs();
                        break;

                    case ConsoleKey.D1:
                        buffManager.AddBuff(buff_SpeedUp);
                        buffManager.ShowActiveBuffs();
                        break;

                    case ConsoleKey.D2:
                        buffManager.AddBuff(buff_Invincible);
                        buffManager.ShowActiveBuffs();
                        break;

                    case ConsoleKey.D3:
                        buffManager.AddBuff(buff_RangeUp);
                        buffManager.ShowActiveBuffs();
                        break;

                    case ConsoleKey.D4:
                        buffManager.AddBuff(buff_Reaper);
                        buffManager.ShowActiveBuffs();
                        break;

                    case ConsoleKey.D5:
                        buffManager.AddBuff(debuff_Poisoned);
                        buffManager.ShowActiveBuffs();
                        break;

                    case ConsoleKey.D6:
                        buffManager.AddBuff(debuff_Stunned);
                        buffManager.ShowActiveBuffs();
                        break;

                    case ConsoleKey.D7:
                        buffManager.AddBuff(debuff_Burning);
                        buffManager.ShowActiveBuffs();
                        break;

                    case ConsoleKey.D8:
                        buffManager.AddBuff(debuff_Frozen);
                        buffManager.ShowActiveBuffs();
                        break;
                }
            }
            #endregion
        }
    }
}
