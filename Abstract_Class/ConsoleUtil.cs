using System;

namespace Abstract_Class
{
    public class ConsoleUtil
    {
        //정적 메서드를 이용해서 유틸 관련 기능을 구현해보고싶어서 만들었습니다.
        public static void PrintConsole(string[] arr)
        {
            foreach (string s in arr)
            {
                Console.WriteLine(s);
            }
        }
    }
}
