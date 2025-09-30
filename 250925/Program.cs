using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _250925
{
    //오늘의 수업 내용
    //그래프와 트리
    internal class Program
    {

        static void Main(string[] args)
        {



            //해쉬충돌 -> 체이닝, 같은 칸에 여러개를 리스트같은걸로 묶는다.
            //        -> 개방 주소법이라고, 다른 빈 칸 찾아다가 넣는 방식

            #region 그래프
            //SimpleGraph<string> subway = new SimpleGraph<string>();


            //var JungJa = subway.AddNode("정자역");
            //var PanGyo = subway.AddNode("판교역");
            //var SungNam = subway.AddNode("성남역");
            //var Imae = subway.AddNode("이매역");
            //var SamDong = subway.AddNode("삼동역");
            //var GyeongGiGwangJu = subway.AddNode("경기광주");

            //subway.AddUndirectedEdge(JungJa, PanGyo, 2);
            //subway.AddUndirectedEdge(PanGyo, SungNam, 2);
            //subway.AddUndirectedEdge(SungNam, Imae, 1);
            //subway.AddUndirectedEdge(Imae, SamDong, 8);
            //subway.AddUndirectedEdge(SamDong, GyeongGiGwangJu, 6);

            //subway.PrintGraph();
            #endregion

            #region BFS

            #endregion
        }
        public static void PrintCurser()
        {
            Console.Write("▶");
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.RightArrow)
                {
                    continue;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    Console.Write("\b\b　");
                    Console.SetCursorPosition(90, Console.CursorTop + 1);
                    Console.Write("▶");
                    continue;
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    Console.Write("\b\b　");
                    Console.SetCursorPosition(90, Console.CursorTop - 1);
                    Console.Write("▶");
                    continue;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }
        }
    }
}