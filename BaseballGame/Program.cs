using System;

namespace Baseball
{
    internal class Program
    {
        const int MaxInning = 9;        //게임 최대 이닝
        const int MaxDigit = 3;         //입력받을 수의 자릿수

        struct NumberDigit
        {
            public int[] digits;        //각 자릿수의 숫자를 담을 배열 -> 인덱스 0: 100의 자리 / 1: 10의 자리 / 2: 1의 자리
        }


        static void Main(string[] args)
        {
            NumberDigit playerND = new NumberDigit();
            NumberDigit pcND = new NumberDigit();

            //플레이어와 컴퓨터의 자리수 정보 배열을 3크기로 초기화
            playerND.digits = new int[MaxDigit];
            pcND.digits = new int[MaxDigit];

            Random rand = new Random();
            int inning = 1;         //현재 진행중인 이닝
            int strike = 0;         //스트라이크 개수
            int ball = 0;           //볼 개수
            bool isPlayerWin = false;

            Console.WriteLine($"============================" +
                $"\n숫\t자\t야\t구\n" +
                $"\n세 자리 숫자를 입력해주세요." +
                $"\n============================" +
                $"\n");

            while (inning <= MaxInning && !isPlayerWin)
            {
                int inputNum = 0;       //입력받을 숫자
                int pcNum = 0;          //컴퓨터가 낼 숫자
                ball = 0;

                //플레이어가 낼 숫자 입력
                int.TryParse(Console.ReadLine(), out inputNum);
                Console.Clear();

                Console.WriteLine($"============================" +
                    $"\n\t{inning} 이 닝" +
                    $"\n============================");

                //입력한 숫자 체크
                if (!CheckInputNumber(playerND, inputNum))
                {
                    Console.WriteLine("잘못된 값을 입력했습니다. 다시 입력해주세요.");
                    continue;
                }

                //피씨가 낼 숫자 랜덤 뽑기
                while(true)
                {
                    pcNum = rand.Next(102, 988);
                    if (CheckInputNumber(pcND, pcNum)) break;
                }

                CheckStrike(playerND, pcND, ref strike, ref ball);

                Console.WriteLine();
                Console.WriteLine($"플레이어 숫자: {inputNum}\t컴퓨터 숫자: {pcNum}");
                Console.WriteLine();

                //Strike, Ball 모두 0일 때 아웃
                if (strike == 0 && ball == 0)
                {
                    Console.WriteLine("아웃!!!!!");
                }
                else
                {
                    Console.Write($"진행 상황\tStrike: ");

                    if (strike > 0)
                    {
                        for (int i = 0; i < strike; i++)
                        {
                            Console.Write(i == strike - 1 ? "●\t" : "● ");
                        }
                    }

                    Console.Write($"\tBall: ");
                    if (ball > 0)
                    {
                        for (int i = 0; i < ball; i++)
                        {
                            Console.Write(i == ball - 1 ? "●" : "● ");
                        }
                    }
                }
                Console.WriteLine();

                //3스트라이크 이상이면 바로 조건문 빠져나오기
                isPlayerWin = strike >= 3 ? true : false;

                inning++;
            }

            //승리, 패배 상태에 따른 출력문
            Console.WriteLine();
            if (isPlayerWin)
            {
                Console.WriteLine($"============================" +
                $"\n축하합니다!! 승리하셨습니다!" +
                $"\n============================");
            }
            else
            {
                Console.WriteLine($"============================" +
                $"\n아쉽지만 컴퓨터의 승리입니다 ㅜoㅜ" +
                $"\n============================");
            }
        }

        //입력받은 숫자 중에 중복된 숫자가 있으면 false, 없으면 true
        static bool CheckInputNumber(NumberDigit nd, int number)
        {
            if (number < 102 || number > 987) return false;

            int hundredsDigit = 0;          //100의 자리
            int tensDigit = 0;              //10의 자리
            int unitsDigit = number;        //1의 자리

            //100의 자리 구하기
            hundredsDigit = number / 100;
            unitsDigit -= hundredsDigit * 100;

            //10의 자리 구하기 및 숫자 비교
            tensDigit = unitsDigit / 10;
            unitsDigit -= tensDigit * 10;

            SetNumberDigit(nd, hundredsDigit, tensDigit, unitsDigit);

            return hundredsDigit != tensDigit && tensDigit != unitsDigit && unitsDigit != hundredsDigit;
        }

        //NumberDigit 구조체에 값 할당
        static void SetNumberDigit(NumberDigit nd, int hundredsDigit, int tensDigit, int unitsDigit)
        {
            nd.digits[0] = hundredsDigit;
            nd.digits[1] = tensDigit;
            nd.digits[2] = unitsDigit;
        }

        //기준이 될 플레이어 숫자를 컴퓨터 숫자 자릿수들과 비교 후 스트라이크, 볼 유무 판단
        static void CheckStrike(NumberDigit playerND, NumberDigit pcND, ref int strike, ref int ball)
        {
            for(int i = 0; i < MaxDigit; i++)
            {
                for(int j = 0; j < MaxDigit; j++)
                {
                    if (i == j)
                        strike = playerND.digits[i] == pcND.digits[j] ? ++strike : strike;
                    else
                        ball = playerND.digits[i] == pcND.digits[j] ? ++ball : ball;
                }
            }
        }
    }
}
