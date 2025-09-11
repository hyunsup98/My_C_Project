using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku
{
    internal class Program
    {
        enum GameState
        {
            Menu,       //초기 메뉴 화면, A 누르면 시작
            InGame,     //게임 진행중일 때 
            Win,        //퍼즐을 다 풀었을 때
        }
        
        struct BlankData            //스도쿠 보드판 각 칸에 입력될 데이터
        {
            public int x, y;        //콘솔상 위치
            public int correctNum;  //실제 정답 데이터
            public int showNum;     //보드판에 보여줄 숫자, 실제 숫자 체크는 따로 함
            public bool isInData;   //데이터가 있는지 빈 공간인지 체크
            public bool isSelected; //현재 커서와 겹쳐있는지
        }

        //게임 상태 관리 열거형 변수
        static GameState gameState = GameState.Menu;

        //스도쿠 보드판 사이즈
        const int boardSize = 9;

        //생성할 빈칸 개수
        const int maxBlankCount = 35;

        //실제 값이 들어갈 보드판
        static BlankData[,] boards = new BlankData[boardSize, boardSize];

        //랜덤 숫자 생성을 위한 객체
        static Random randNum = new Random();

        //콘솔창 가로 넓이
        static int maxWidth = Console.WindowWidth;

        //async Task 취소를 제어하기 위한 토큰 선언
        static CancellationTokenSource cts = new CancellationTokenSource();

        //비동기 동작을 위해서 async Task형식 사용, Thread.Sleep()는 대기중에 다른 동작이 불가능해서 async 사용
        static async Task Main(string[] args)
        {

            await SetMenuScene(cts.Token);

            if (gameState == GameState.InGame)
            {
                Console.CursorVisible = true;

                ShowMessageGenerating();

                //보드판 생성
                CreateBoard();
                //보드판 빈칸 뚫어주기
                MakeBlank(maxBlankCount);
                //보드판 그려주기
                PrintBoard();

                Console.SetCursorPosition(45, 1);

                ConsoleKeyInfo cursorKey;
                BlankData data = new BlankData();
                int blankRow = 0;
                int blankCol = 0;
                int x = Console.CursorLeft;
                int y = Console.CursorTop;
                while (true)
                {
                    if (gameState != GameState.InGame) break;

                    cursorKey = Console.ReadKey(true);

                    if (data.isSelected)
                        data.isSelected = false;

                    switch(cursorKey.Key)
                    {
                        case ConsoleKey.W:
                            y = y - 1 < 0 ? 0 : y - 1;
                            break;

                        case ConsoleKey.S:
                            y += 1;
                            break;

                        case ConsoleKey.A:
                            x = x - 1 < 0 ? 0 : x - 1;
                            break;

                        case ConsoleKey.D:
                            x += 1;
                            break;
                    }

                    Console.SetCursorPosition(x, y);

                    for (int row = 0; row < boardSize; row++)
                    {
                        for(int col = 0; col < boardSize; col++)
                        {
                            Console.SetCursorPosition(x, y);

                            if (boards[row, col].x == x && boards[row, col].y == y && !boards[row, col].isInData)
                            {
                                boards[row, col].isSelected = true;
                                data = boards[row, col];
                                blankRow = row;
                                blankCol = col;

                                break;
                            }
                        }
                        if (data.isSelected) break;
                    }

                    //커서 위치에 빈칸이 있으면
                    while(data.isSelected)
                    {
                        int num = 0;
                        cursorKey = Console.ReadKey(true);

                        switch(cursorKey.Key)
                        {
                            case ConsoleKey.D1:
                                num = 1;
                                break;
                            case ConsoleKey.D2:
                                num = 2;
                                break;
                            case ConsoleKey.D3:
                                num = 3;
                                break;
                            case ConsoleKey.D4:
                                num = 4;
                                break;
                            case ConsoleKey.D5:
                                num = 5;
                                break;
                            case ConsoleKey.D6:
                                num = 6;
                                break;
                            case ConsoleKey.D7:
                                num = 7;
                                break;
                            case ConsoleKey.D8:
                                num = 8;
                                break;
                            case ConsoleKey.D9:
                                num = 9;
                                break;
                            case ConsoleKey.W:
                                data.isSelected = false;
                                y = y - 1 < 0 ? 0 : y - 1;
                                Console.SetCursorPosition(x, y);
                                break;
                            case ConsoleKey.S:
                                data.isSelected = false;
                                y += 1;
                                Console.SetCursorPosition(x, y);
                                break;
                            case ConsoleKey.A:
                                data.isSelected = false;
                                x = x - 1 < 0 ? 0 : x - 1;
                                Console.SetCursorPosition(x, y);
                                break;
                            case ConsoleKey.D:
                                data.isSelected = false;
                                x += 1;
                                Console.SetCursorPosition(x, y);
                                break;
                        }

                        if (num > 0 && num < boardSize + 1)
                        {
                            if (data.correctNum == num)
                            {
                                //정답이면 정답 처리
                                data.isInData = true;
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write($"{data.correctNum}");
                                Console.ForegroundColor = ConsoleColor.White;
                                boards[blankRow, blankCol] = data;
                                data.isSelected = false;
                                Console.SetCursorPosition(x, y);
                                Win();
                                break;
                            }
                            else
                            {
                                //오답이면 오답 처리
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write($"{num}");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            Console.SetCursorPosition(x, y);
                        }
                    }
                }
            }
        }

        static void Win()
        {
            foreach(var i in boards)
            {
                if (!i.isInData) return;
            }
            gameState = GameState.Win;
            Console.Clear();
            StringOutputMiddle("축하합니다. 모든 문제를 풀었습니다!", true, true, false, 0);
        }

        //첫 메인메뉴 화면 출력
        static async Task SetMenuScene(CancellationToken token)
        {
            if (gameState != GameState.Menu) return;

            string asciiCode1 = "  ******** **     ** *******     *******   **   ** **     **";
            string asciiCode2 = " **////// /**    /**/**////**   **/////** /**  ** /**    /**";
            string asciiCode3 = "/**       /**    /**/**    /** **     //**/** **  /**    /**";
            string asciiCode4 = "/*********/**    /**/**    /**/**      /**/****   /**    /**";
            string asciiCode5 = "////////**/**    /**/**    /**/**      /**/**/**  /**    /**";
            string asciiCode6 = "       /**/**    /**/**    ** //**     ** /**//** /**    /**";
            string asciiCode7 = " ******** //******* /*******   //*******  /** //**//******* ";
            string asciiCode8 = "////////   ///////  ///////     ///////   //   //  ///////  ";

            string[] strings = new string[] { asciiCode1, asciiCode2, asciiCode3, asciiCode4, asciiCode5, asciiCode6, asciiCode7, asciiCode8 };

            for (int i = 0; i < strings.Length; i++)
            {
                StringOutputMiddle(strings[i], true, true, false, 0);
            }

            Console.WriteLine();

            bool isShow = false;
            int x = 0;
            Console.CursorVisible = false;
            while (!cts.IsCancellationRequested)
            {
                try
                {
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);

                        if (key.Key == ConsoleKey.A)
                        {
                            gameState = GameState.InGame;
                            Console.Clear();
                            break;
                        }
                    }

                    if (!isShow)
                    {
                        StringOutputMiddle("시작하려면 A 버튼을 눌러주세요.", false, true, false, 0);
                        x = Console.CursorLeft;
                    }
                    else
                    {
                        Console.CursorVisible = false;
                        Console.Write("\r");
                        Console.Write(new string(' ', maxWidth));
                        Console.Write("\r");
                    }
                    isShow = !isShow;

                    await Task.Delay(500, token);
                }
                catch (Exception ex)
                {
                    break;
                }
            }
        }

        //있어보이게 스도쿠 보드판 생성중 문자 띄우기
        static void ShowMessageGenerating()
        {
            string message = "스도쿠 생성중...";
            StringOutputMiddle(message, false, true ,true, 3);

        }

        //가운데 정렬후 출력해주기 위함
        //isNewLine이 true면 줄바꿈, false면 줄바꿈 X || isPadding이 true면 가운데 정렬, false면 정렬 X || isTypingEffect가 true면 타이핑 효과를 repeatCount만큼 반복
        static void StringOutputMiddle(string str, bool isNewLine, bool isPadding, bool isTypingEffect, int repeatCount)
        {
            //왼쪽 여백 사이즈
            //Encoding.Default.GetBytes(문자열 변수).Length => 를 통해 바이트 수를 구해 가운데 정렬을 함 (영어 1바이트, 한글 2바이트)
            int leftPadding = (maxWidth - Encoding.Default.GetBytes(str).Length) / 2 > 0 ? (maxWidth - Encoding.Default.GetBytes(str).Length) / 2 : 0;

            string text = isPadding ? "".PadLeft(leftPadding) + str : str;

            //타이핑 효과 없을 때
            if (!isTypingEffect)
            {
                if (isNewLine)
                    Console.WriteLine(text);
                else
                    Console.Write(text);

                return;
            }
            else
            {
                char[] chars = str.ToCharArray();

                for(int i = 0; i < repeatCount; i++)
                {
                    Console.Write("".PadLeft(leftPadding));
                    for (int j = 0; j < chars.Length; j++)
                    {
                        Console.Write(chars[j]);
                        Thread.Sleep(100);
                    }

                    Thread.Sleep(500);
                    Console.Clear();
                }
            }
        }

        //스도쿠 보드판 생성
        static bool CreateBoard()
        {
            //boardSize X boardSize 크기의 보드판을 기준으로 조건 체크
            for(int row = 0; row < boardSize; row++)
            {
                for(int col = 0; col < boardSize; col++)
                {
                    //boards[row, col] 좌표의 값이 0이면(아무값도 없으면) 1~9의 랜덤 숫자를 넣어줌
                    if (boards[row, col].showNum == 0)
                    {
                        //랜덤 숫자를 꺼낼 리스트 선언
                        List<int> randNumList = new List<int>();
                        for(int i = 0; i < 9; i++)
                        {
                            randNumList.Add(i + 1);
                        }

                        //Fisher-Yates Shuffle을 사용해서 셔플
                        for(int i = randNumList.Count - 1; i > 0; i--)
                        {
                            int j = randNum.Next(0, i + 1);

                            var temp = randNumList[i];
                            randNumList[i] = randNumList[j];
                            randNumList[j] = temp;
                        }

                        //재귀함수를 통해 계속 값을 넣어줌, 조건에 부합하는 수가 없는 경우 상위 호출로 돌아가 다시 검사
                        foreach(var num in randNumList)
                        {
                            if(IsCanPutNumber(row, col, num))
                            {
                                boards[row, col].showNum = num;
                                boards[row, col].correctNum = num;
                                boards[row, col].isInData = true;

                                if (CreateBoard())
                                    return true;        //다음 좌표에 숫자가 들어가면 계속 진행

                                boards[row, col].showNum = 0;
                                boards[row, col].isInData = false;
                            }
                        }

                        return false;       //현재 칸에 들어갈 숫자가 없는 경우 false 반환 => 현재 호출중인 함수는 해제되며 이전 함수로 가서 다시 진행한다.
                    }
                }
            }

            return true;
        }

        //보드를 생성할 때 해당 좌표에 해당 숫자를 놓을 수 있는지 체크
        static bool IsCanPutNumber(int row, int col, int num)
        {
            for(int i = 0; i < boardSize; i++)
            {
                //같은 열 또는 같은 행에 num이 이미 있을 경우 false 반환
                if (boards[row, i].showNum == num || boards[i, col].showNum == num)
                    return false;
            }

            //3x3 보드판에 같은 숫자가 있는지 체크하기 위한 기준 row, col
            //ex) (4, 2) 값을 받아오면 (3, 0)부터 체크 시작
            int startRow = row - (row % 3);
            int startCol = col - (col % 3);

            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    //3x3 보드판에 이미 num 숫자가 있을 경우 false 반환
                    if (boards[startRow + i, startCol + j].showNum == num) return false;
                }
            }

            //위의 조건식에 하나도 해당 사항이 없을 경우 true 반환
            return true;
        }

        //완성된 스도쿠 퍼즐에서 빈칸을 만들어주기
        //백트래킹 알고리즘을 통해서 답이 오로지 하나만 존재할 때만 빈칸을 만들어줌
        //매개변수로 받은 blankCount 만큼의 빈칸을 뚫어주기
        static void MakeBlank(int blankCount)
        {
            //빈칸 만든 수
            int doneCount = 0;

            //튜플 형식의 리스트를 선언
            List<(int, int)> coorShuffleList = new List<(int, int)>();

            while(doneCount < blankCount)
            {
                int row = randNum.Next(0, boardSize);
                int col = randNum.Next(0, boardSize);

                if (boards[row, col].showNum == 0) continue;

                int backupValue = boards[row, col].showNum;
                boards[row, col].showNum = 0;

                int solutionCount = CountSolution(boards);

                if (solutionCount != 1)
                {
                    boards[row, col].showNum = backupValue;
                }
                else
                {
                    doneCount++;
                }
            }

        }

        //가장 최소 후보군을 구해서 재귀호출
        //최소 후보군만 찾아서 생성하기 때문에 경우의 수 탐색을 최소화하고 답이 편향되어있지 않음
        static int CountSolution(BlankData[,] copyBoards)
        {
            //최소 후보군
            int minOptions = 9;

            //조건에 부합하는 [row, col]값을 넣어줄 변수
            //기본이 -1인 이유는 조건에 부합하는 좌표값이 없는 경우 -1로 체크 -> return;
            int targetRow = -1;
            int targetCol = -1;

            List<int> bestNumGroup = new List<int>();

            for(int i = 0; i <boardSize; i++)
            {
                for(int j = 0; j < boardSize; j++)
                {
                    //현재 탐색중인 좌표의 값이 0일 경우
                    if (copyBoards[i, j].showNum == 0)
                    {
                        List<int> numGroup = new List<int>();
                        for(int k = 1; k <= 9; k++)
                        {
                            //numGroup에 현재 좌표에 들어갈 수 있는 숫자 후보군을 다 넣어줌
                            if (IsCanPutNumber(i, j, k))
                                numGroup.Add(k);
                        }

                        //이전까지의 최소 후보군 개수보다 현재 좌표가 더 적을 경우 새로 데이터 갱신
                        if(numGroup.Count < minOptions)
                        {
                            minOptions = numGroup.Count;
                            targetRow = i;
                            targetCol = j;
                            bestNumGroup = numGroup;

                            if (minOptions == 1) break;
                        }
                    }
                }
                if (minOptions == 1) break;
            }

            //targetRow나 targetCol이 -1이면 => 더 이상 빈 칸이 없음
            if (targetRow == -1 || targetCol == -1) return 1;

            //가능한 해답의 경우의 수를 넣어줄 변수
            int solutions = 0;
            foreach(var num in bestNumGroup)
            {
                copyBoards[targetRow, targetCol].showNum = num;
                solutions += CountSolution(copyBoards);
                copyBoards[targetRow, targetCol].showNum = 0;

                if (solutions >= 2) return solutions;
            }

            return solutions;
        }

        //화면상에 스도쿠 Board 출력
        static void PrintBoard()
        {
            string strBlank = "                                             ";

            Console.WriteLine(strBlank + "=====================");
            for (int row = 0; row < boardSize; row++)
            {
                if (row % 3 == 0 && row != 0)
                    Console.WriteLine(strBlank + "----------------------");
                                    
                for (int col = 0; col < boardSize; col++)
                {
                    if(col == 0)
                        Console.Write(strBlank);

                    if (col % 3 == 0 && col != 0)
                        Console.Write("| ");

                    if (boards[row, col].showNum != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{boards[row, col].showNum} ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        boards[row, col].x = Console.CursorLeft;
                        boards[row, col].y = Console.CursorTop;
                        Console.Write($"? ");
                        boards[row, col].isInData = false;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine(strBlank + "=====================");

            strBlank = "                      ";

            //게임 설명 문자열
            Console.Write(strBlank);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("■");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(": 정답을 입력해야 하는 칸   ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("■");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(": 정답   ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("■");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(": 오답   ");

            Console.WriteLine(strBlank + "▶ 'W','A','S','D'를 이용해서 포인터를 움직일 수 있습니다.");
            Console.WriteLine(strBlank + "▶ 파란색의 '?' 칸과 포인터를 겹친 후 숫자키를 입력해 정답을 입력할 수 있습니다.");
            Console.WriteLine(strBlank + "▶ 초록색 숫자가 기입될 경우 정답으로 해당 칸은 수정이 불가능합니다.");
            Console.WriteLine(strBlank + "▶ 빨간색 숫자가 기입될 경우 오답으로 다시 숫자를 입력해 정답을 맞추셔야합니다.");
            Console.WriteLine(strBlank + "▶ 모든 칸을 초록색으로 채우면 플레이어의 승리입니다.");
        }
    }
}
