using System;

namespace _250924
{
    public class Drink
    {
        protected DateTime ExpirationDate { get; private set; }     //우유의 유통기한 (우유 생성 시점 + 랜덤 유통기한 시간)

        private string drinkName;                                   //음료의 이름
        protected string DrinkName                                 
        {
            get
            {
                if (string.IsNullOrEmpty(drinkName))
                    return "음료";
                else
                    return drinkName;
            }
            private set { drinkName = value; }
        }

        private int min_ExpirationDate = 10;   //최소 유통기한 랜덤값
        private int max_ExpirationDate = 100;   //최대 유통기한 랜덤값

        private static Random random = new Random();

        //생성자
        public Drink()
        {
            //우유를 생성할 때 유통기한을 랜덤값으로 넣어주기
            int ExpirationTime = random.Next(min_ExpirationDate, max_ExpirationDate + 1);

            //현재 시간 + 10 ~ 100초 랜덤 값
            ExpirationDate = DateTime.Now.AddSeconds(ExpirationTime);
        }

        public void Init(string name)
        {
            DrinkName = name;
        }

        /// <summary>
        /// 남은 유통기한을 보여줄 추상메서드
        /// </summary>
        public void PrintExpirationDate()
        {
            //현재 시간과 음료 유통기한 비교하기
            TimeSpan elapsedTime = ExpirationDate - DateTime.Now;
            double elased = Math.Round(elapsedTime.TotalSeconds * 100) / 100;

            if (elased > 0)
            {
                //음료가 싱싱
                Console.WriteLine($"현재 {DrinkName}의 남은 유통기한은 {elased}초 입니다.");
            }
            else
            {
                //음료가 상함
                Console.WriteLine($"{DrinkName}의 유통기한이 {Math.Abs(elased)}초 만큼 지났습니다.");

            }
        }
    }


    public class Milk : Drink
    {
        public Milk() { }
    }

    public class Coffee : Drink
    {
        public Coffee() { }
    }
}
