using System;

namespace Project_250916
{
    //몬스터 클래스
    class Monster
    {
        //몬스터의 이름, 읽기 전용 프로퍼티로 선언
        public string monsterName { get; }

        //몬스터의 레벨 프로퍼티
        private int monsterLevel;
        public int MonsterLevel
        {
            get { return monsterLevel; }
            set
            {
                //레벨의 최소값은 1이기 때문에 1 미만의 값이 들어온 경우 1로 할당
                monsterLevel = value < 1 ? 1 : value;
            }
        }

        #region 생성자
        public Monster(string monsterName, int MonsterLevel = 1)
        {
            this.monsterName = monsterName;
            this.MonsterLevel = MonsterLevel;
        }
        #endregion

        #region 몬스터 메서드

        #region 현재 몬스터 정보 출력 메서드
        //자신(몬스터)에 대한 정보를 출력
        public void Print()
        {
            Console.WriteLine($"현재 몬스터의 이름: {monsterName}");
            Console.WriteLine($"현재 몬스터의 레벨: {monsterLevel}");
        }
        #endregion

        #endregion
    }
}
