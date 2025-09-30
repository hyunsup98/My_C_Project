using System;

namespace Abstract_Class
{
    //몬스터 타입 열거형
    public enum MobType
    {
        Normal,
        Fire,
        Water,
        Grass
    }

    public class Monster
    {
        #region 변수
        public string name { get; set; }    //몬스터 이름
        private int level;
        public int Level
        {
            get { return level; }
            set
            {
                level = value < 1 ? 1 : value;
            }
        }
        private MobType type;               //몬스터 타입
        #endregion

        #region 생성자
        //1번 생성자
        public Monster()
        {
            //정보를 기본으로 초기화
            name = string.Empty;
            Level = 1;
            type = MobType.Normal;

            ShowMonsterInfo();
        }

        //2번 생성자
        public Monster(string name, int level, MobType type)
        {
            this.name = name;
            Level = level;
            this.type = type;

            ShowMonsterInfo();
        }
        #endregion

        //몬스터 정보 출력
        public void ShowMonsterInfo()
        {
            string[] strings =
            {
                "=========================\n",
                "▼▼▼ 몹의 정보 ▼▼▼",
                $"이름: {name}",
                $"레벨: {Level}",
                $"타입: {type}",
                "\n=========================\n"
            };

            ConsoleUtil.PrintConsole(strings);
        }
    }
}
