//using System;
//using System.Collections.Generic;

//public class Solution
//{
//    public int[] solution(string[] park, string[] routes)
//    {
//        int playerX = 0;
//        int playerY = 0;

//        int y = park.Length;
//        int x = park[0].Length;

//        string[,] maps = new string[y, x];

//        for (int i = 0; i < y; i++)
//        {
//            for (int j = 0; j < park[i].Length; j++)
//            {
//                string str = park[i];

//                maps[i, j] = str[j].ToString();
//                if (maps[i, j] == "S")
//                {
//                    playerY = i;
//                    playerX = j;
//                }
//            }
//        }

//        foreach (var s in routes)
//        {
//            string[] strings = s.Split(' ');
//            int.TryParse(strings[1], out var count);

//            bool isSuccess = true;
//            int tempX = playerX;
//            int tempY = playerY;

//            for (int i = 0; i < count; i++)
//            {
//                switch (strings[0])
//                {
//                    case "N": tempY -= 1; break;
//                    case "S": tempY += 1; break;
//                    case "W": tempX -= 1; break;
//                    case "E": tempX += 1; break;
//                }

//                if (tempY < 0 || tempY >= y || tempX < 0 || tempX >= x || !CheckWall(maps, tempY, tempX))
//                {
//                    isSuccess = false;
//                    break;
//                }
//            }

//            if (isSuccess)
//            {
//                playerY = tempY;
//                playerX = tempX;
//            }

//        }
//        int[] answer = new int[2] { playerY, playerX };
//        return answer;
//    }

//    public bool CheckWall(string[,] maps, int y, int x)
//    {
//        switch (maps[y, x])
//        {
//            case "O":
//            case "S":
//                return true;

//            case "X":
//                return false;
//        }

//        return false;
//    }
//}