using System;

namespace Abstract_Class
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region 생성자 테스트
            //몬스터 1번 생성자 출력
            Monster mon1 = new Monster();

            //몬스터 2번 생성자 출력
            Monster mon2 = new Monster("파이리", 1, MobType.Fire);

            //트레이너 1번 생성자 출력
            Trainer jiwoo1 = new Trainer("지우", "파이리");

            //트레이너 2번 생성자 출력
            Trainer jiwoo2 = new Trainer(mon2);
            #endregion
        }
    }
}
