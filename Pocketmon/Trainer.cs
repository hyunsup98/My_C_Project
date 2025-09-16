using System;

namespace Project_250916
{
    //플레이어 클래스
    class Trainer
    {
        //트레이너의 이름 프로퍼티, 읽기 전용
        public string trainerName { get; }

        //트레이너가 가지고 있는 Monster 타입의 배열 => 6크기
        Monster[] monsters = new Monster[6];

        #region 생성자
        public Trainer(string trainerName)
        {
            //인스턴스 생성시 받은 트레이너 이름을 할당
            this.trainerName = trainerName;

            Console.WriteLine($"\n{trainerName}님 환영합니다.");
        }
        #endregion

        #region 메서드 모음

        #region 몬스터 객체 추가 메서드
        //몬스터 배열에 monster 추가
        public void Add(Monster monster)
        {
            for(int i = 0; i < monsters.Length; i++)
            {
                if (monsters[i] == null)
                {
                    //monsters 배열에 빈 공간을 찾을 시 인자로 받은 monster 넣어주고 종료
                    monsters[i] = monster;
                    return;
                }
            }

            //여기까지 왔다는건 monsters 배열에 남은 공간이 없다는 것
            Console.WriteLine("이미 최대 몬스터 개수에 도달하였습니다.\n");
        }
        #endregion

        #region 플레이어가 소지중인 몬스터 객체 제거 메서드
        //인자로 받은 문자열과 동일한 이름의 몬스터 삭제
        public void Remove(string monsterName)
        {
            //monsterName과 동일한 이름의 몬스터가 있는지 탐색 있으면 해당 인덱스 반환, 없으면 -1 반환
            //단 중복된 몬스터가 있어도 앞쪽의 첫 몬스터만 삭제
            var index = Array.FindIndex(monsters, x => x.monsterName == monsterName);

            if(index == -1)
            {
                //조건에 맞는 몬스터가 없을 경우
                Console.WriteLine("동일한 이름의 몬스터를 찾을 수 없습니다.");
            }
            else
            {
                //해당 인덱스 데이터 삭제
                monsters[index] = null;
                Console.WriteLine($"{index} 번째에 있던 {monsterName} 몬스터를 제거했습니다.");
            }
        }
        #endregion

        #region 소지중인 몬스터 객체 출력 메서드
        //가지고 있는 모든 몬스터 정보 출력
        public void PrintAll()
        {
            bool isExists = false;

            for (int i = 0; i < monsters.Length; i++)
            {
                if (monsters[i] != null)
                {
                    //현재 주소에 몬스터 데이터가 있을 경우 실행
                    isExists = true;

                    Console.WriteLine("=================================");
                    Console.WriteLine($"{i + 1}번째 몬스터 정보");
                    monsters[i].Print();
                    Console.WriteLine("=================================\n");
                }
            }

            if(!isExists)
                Console.WriteLine("몬스터를 가지고 있지 않습니다.");
        }
        #endregion

        #endregion
    }
}
