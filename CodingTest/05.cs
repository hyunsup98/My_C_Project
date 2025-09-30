//using System;

//public class Solution
//{
//    public bool solution(string s)
//    {
//        int result = 0;

//        foreach (char c in s)
//        {
//            if (c == '(')
//            {
//                result += 1;
//                continue;
//            }

//            if (result == 0)
//            {
//                return false;
//            }

//            result -= 1;
//        }

//        return result == 0 ? true : false;
//    }
//}