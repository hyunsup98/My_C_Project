using System;

namespace _250924
{
    #region 게임 상태 열거형
    public enum GameState
    {
        Playing, Stop
    }
    #endregion

    //기존 문제에서 VendingMachine 클래스를 제네릭으로 만들고 다형성을 이용해 우유, 커피 자판기 머신 객체를 만들어 사용했습니다.
    //Drink 부모 클래스를 만들고 파생 클래스로 Milk, Coffee 클래스를 만들어 추후에 기능을 확장하기 용이하게 설계했습니다.
    //
    //유통기한은 DateTime, Timespan을 이용했습니다.
    //유통기한 - 생성되는 시점의 시간 + 10~100초 랜덤값
    //유통기한 - 자판기에서 꺼내질 시점의 시간 → 0보다 크면 유통기한이 남아있고, 0보다 작으면 유통기한이 지남을 판단

    internal class Program
    {
        private static GameState gameState = GameState.Playing;

        static void Main(string[] args)
        {
            #region 프로그램 실행
            VendingMachine<Milk> milkMachine = new VendingMachine<Milk>();
            VendingMachine<Coffee> coffeeMachine = new VendingMachine<Coffee>();

            SetDrink(milkMachine, "우유");
            SetDrink(coffeeMachine, "커피");

            Console.Clear();
            Console.WriteLine($"1번키 - 우유 꺼내기 | 2번키 - 커피 꺼내기 | A키 - 우유 추가 | S키 - 커피 추가. (0 입력 시 프로그램 종료)");

            while(gameState == GameState.Playing)
            {
                #region 키 입력 구역
                var inputChar = Console.ReadKey(true);

                if (inputChar.Key == ConsoleKey.D1)
                {
                    //1번키 누름 - 자판기에서 우유 꺼내기

                    Console.WriteLine("=====================================\n");
                    var drink = milkMachine.TakeObject();
                    if(drink != null)
                    {
                        drink.PrintExpirationDate();
                    }
                    Console.WriteLine("\n=====================================\n");
                }
                else if(inputChar.Key == ConsoleKey.D2)
                {
                    //2번키 누름 - 자판기에서 커피 꺼내기

                    Console.WriteLine("=====================================\n");
                    var drink = coffeeMachine.TakeObject();
                    if (drink != null)
                        drink.PrintExpirationDate();
                    Console.WriteLine("\n=====================================\n");
                }
                else if (inputChar.Key == ConsoleKey.A)
                {
                    //A키 누름 - 자판기에 우유 추가
                    var milk = new Milk();
                    milk.Init("우유");
                    milkMachine.GetObject(milk);
                    Console.WriteLine("자판기에 우유를 한 개 추가했습니다!");
                }
                else if(inputChar.Key == ConsoleKey.S)
                {
                    //S키 누름 - 자판기에 커피 추가
                    var coffee = new Coffee();
                    coffee.Init("커피");
                    coffeeMachine.GetObject(coffee);
                    Console.WriteLine("자판기에 커피 한 개 추가했습니다!");
                }
                else if (inputChar.Key == ConsoleKey.D0)
                {
                    //0번키 누름 - 프로그램 종료

                    gameState = GameState.Stop;
                    Console.WriteLine("프로그램이 종료되었습니다.");
                }
                #endregion
            }
            #endregion
        }

        #region 메서드
        /// <summary>
        /// 자판기의 음료 초기 세팅
        /// </summary>
        public static void SetDrink<T>(VendingMachine<T> machine, string drinkName) where T : Drink, new()
        {
            while (true)
            {
                Console.WriteLine($"자판기에 들어갈 {drinkName}의 개수를 입력해주세요. (1 ~ 30 입력 가능)");

                //자판기에 넣을 음료 개수 입력
                if (int.TryParse(Console.ReadLine(), out var input))
                {
                    if (input >= 1 && input <= 30)
                    {
                        AddDrink(machine, input, drinkName);
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("1이상 30이하의 숫자를 입력해주세요.\n");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 값을 입력했습니다. 1이상 30이하의 숫자를 입력해주세요.\n");
                }
            }
        }

        //자판기 우유 세팅 메서드
        /// <summary>
        /// 우유 자판기의 우유 개수 초기화 메서드
        /// </summary>
        /// <param name="machine"> 자판기 클래스 객체 </param>
        /// <param name="count"> 넣을 우유 개수 </param>
        public static void AddDrink<T>(VendingMachine<T> machine, int count, string name) where T : Drink, new()
        {
            if (machine == null) return;

            for(int i = 0; i < count; i++)
            {
                var obj = new T();
                obj.Init(name);

                machine.GetObject(obj);
            }
        }
        #endregion
    }
}
