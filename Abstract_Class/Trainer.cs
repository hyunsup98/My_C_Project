using System;

namespace Abstract_Class
{
    public class Trainer
    {
        private string name;             //트레이너 이름
        private string startMonster;     //스타팅 몬스터
        private int badge = 0;           //뱃지 개수
        public int Badge
        {
            get { return badge; }
            set
            {
                badge = value < 0 ? 0 : value;
            }
        }

        private Monster[] monsters;              //6크기의 몬스터를 담는 배열

        #region 생성자
        //1번 생성자
        public Trainer(string name, string startMonster)
        {
            this.name = name;
            this.startMonster = startMonster;
            monsters = new Monster[6];

            ShowTrainerInfo();
        }

        //2번 생성자
        public Trainer(Monster monster)
        {
            monsters = new Monster[6];
            monsters[0] = monster;

            ShowFirstMob();
        }
        #endregion

        #region 메서드
        //스타팅 몬스터 바꾸는 메서드
        public void ChangeStartMonster(string startMonster)
        {
            this.startMonster = startMonster;
        }

        //트레이너 이름, 스타팅 몬스터 이름 출력 메서드
        //1번 생성자 테스트 메서드
        public void ShowTrainerInfo()
        {
            string[] strings =
            {
                "=========================\n",
                "▼▼▼ 트레이너 정보 ▼▼▼",
                $"이름: {name}",
                $"스타팅 몬스터: {startMonster}",
                "\n=========================\n"
            };

            ConsoleUtil.PrintConsole(strings);
        }


        //0번째 몹 정보 출력 메서드
        //2번 생성자 테스트 메서드
        public void ShowFirstMob()
        {
            string[] strings =
            {
                "=========================\n",
                "▼▼▼ 0번째 몹의 정보 ▼▼▼",
                $"이름: {monsters[0].name}",
                $"레벨: {monsters[0].Level}",
                "\n=========================\n"
            };

            ConsoleUtil.PrintConsole(strings);
        }
        #endregion
    }
}
