//using System;

//public class Solution
//{
//    public int[] solution(int num, int total)
//    {
//        int[] answer = new int[num];

//        int firstNum = num % 2 == 0 ? (total / num) - ((num / 2) - 1) : (total / num) - (num / 2);

//        for (int i = 0; i < num; i++)
//        {
//            answer[i] = firstNum;
//            firstNum++;
//        }

//        return answer;
//    }
//}