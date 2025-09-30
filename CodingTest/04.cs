//using System;
//using System.Collections.Generic;

//public class Solution
//{
//    public struct Vector
//    {
//        public int a;
//        public int b;
//        public int line;
//    }

//    public int solution(string dirs)
//    {
//        int answer = 0;

//        //현재 플레이어 좌표
//        int x = 0;
//        int y = 0;

//        List<Vector> listX = new List<Vector>();
//        List<Vector> listY = new List<Vector>();

//        for (int i = 0; i < dirs.Length; i++)
//        {
//            Vector vec;

//            switch (dirs[i])
//            {
//                case 'U':
//                    if (y == 5) continue;
//                    if (!CheckExists(listY, y, y + 1, x))
//                    {
//                        vec.a = y;
//                        vec.b = y + 1;
//                        vec.line = x;
//                        listY.Add(vec);
//                        answer++;
//                    }
//                    y++;
//                    break;

//                case 'D':
//                    if (y == -5) continue;
//                    if (!CheckExists(listY, y, y - 1, x))
//                    {
//                        vec.a = y;
//                        vec.b = y - 1;
//                        vec.line = x;
//                        listY.Add(vec);
//                        answer++;
//                    }
//                    y--;
//                    break;

//                case 'R':
//                    if (x == 5) continue;
//                    if (!CheckExists(listX, x, x + 1, y))
//                    {
//                        vec.a = x;
//                        vec.b = x + 1;
//                        vec.line = y;
//                        listX.Add(vec);
//                        answer++;
//                    }
//                    x++;
//                    break;

//                case 'L':
//                    if (x == -5) continue;
//                    if (!CheckExists(listX, x, x - 1, y))
//                    {
//                        vec.a = x;
//                        vec.b = x - 1;
//                        vec.line = y;
//                        listX.Add(vec);
//                        answer++;
//                    }
//                    x--;
//                    break;
//            }
//        }

//        return answer;
//    }

//    //이미 간 곳인지 체크
//    public bool CheckExists(List<Vector> list, int a, int b, int line)
//    {
//        foreach (var i in list)
//        {
//            if ((i.a == a && i.b == b && i.line == line) || (i.a == b && i.b == a && i.line == line))
//                return true;
//        }

//        return false;
//    }
//}