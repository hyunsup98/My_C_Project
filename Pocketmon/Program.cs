using Microsoft.SqlServer.Server;
using System;

namespace Project_250916
{
    #region 게임 상태 열거형
    //게임의 상태를 관리할 GameState 타입 선언
    enum GameState
    {
        Playing,    //게임이 플레이중일 때
        Exit        //게임이 종료됨
    }
    #endregion

    //실질적인 행동 담당 클래스, Trainer와 Monster 관리
    internal class Program
    {
        //트레이너 변수 (플레이어 객체)
        static Trainer trainer;

        //게임 상태 객체 생성
        static GameState gameState = GameState.Playing;

        static void Main(string[] args)
        {
            //플레이어 이름 입력받기
            Console.Write($"플레이어의 이름을 입력해주세요: ");
            trainer = new Trainer(Console.ReadLine());

            while(gameState == GameState.Playing)
            {
                SelectMenu();
            }
        }
        #region 메서드 모음

        #region 메인메뉴 선택 화면 메서드
        //선택 메인메뉴 화면
        static private void SelectMenu()
        {
            //메뉴 목록 보여주기
            Console.WriteLine("=================================");
            Console.WriteLine($"{trainer.trainerName}님! 원하는 행동을 선택해주세요.");
            Console.WriteLine("1. 몬스터 추가하기");
            Console.WriteLine("2. 몬스터 삭제하기");
            Console.WriteLine("3. 현재 보유한 몬스터 확인하기");
            Console.WriteLine("4. 프로그램을 종료하기");
            Console.WriteLine("=================================\n");

            //메뉴 선택 입력값 받기
            int inputNum = InputNumber(Console.ReadLine());

            Console.Clear();

            switch (inputNum)
            {
                case 1:
                    //몬스터 추가하기
                    Console.WriteLine("추가할 몬스터의 이름과 레벨을 입력해주세요. [이름 레벨] 형식으로 (예: 파이리 5)");
                    string[] strings = Console.ReadLine().Split(' ');
                    trainer.Add(new Monster(strings[0], InputNumber(strings[1])));
                    break;
                case 2:
                    //몬스터 삭제하기
                    Console.WriteLine("삭제할 몬스터의 이름을 입력해주세요.");
                    trainer.Remove(Console.ReadLine());
                    break;
                case 3:
                    //몬스터 정보 보기
                    trainer.PrintAll();
                    break;
                case 4:
                    //게임 종료
                    Console.WriteLine("게임을 종료하였습니다.");
                    gameState = GameState.Exit;
                    break;
                default:
                    Console.WriteLine("잘못된 값을 입력하였습니다.");
                    break;
            }
        }
        #endregion

        #region 입력받은 문자열을 숫자로 반환하는 메서드
        //숫자를 입력받아야 할 때 사용, 단 자연수 입력일 때만 사용
        static int InputNumber(string inputStr)
        {
            //입력받을 값 변수
            int inputNum = 0;

            //입력받은 값이 숫자인지 체크하는 bool 변수
            bool isNumber = int.TryParse(inputStr, out inputNum);

            return isNumber ? inputNum : -1;
        }
        #endregion
        #endregion
    }
}
