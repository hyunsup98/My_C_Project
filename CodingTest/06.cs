//using System;
//using System.Collections.Generic;

//class Solution
//{
//    public struct Vector
//    {
//        public int y;
//        public int x;
//        public int count;

//        public Vector(int y, int x, int count = 0)
//        {
//            this.y = y;
//            this.x = x;
//            this.count = count;
//        }
//    }

//    public int solution(int[,] maps)
//    {
//        Vector[] neighbor = { new Vector(0, 1), new Vector(0, -1), new Vector(1, 0), new Vector(-1, 0) };

//        Queue<Vector> queue = new Queue<Vector>();
//        queue.Enqueue(new Vector(0, 0, 1));
//        maps[0, 0] = 0;

//        while (queue.Count != 0)
//        {
//            Vector currentPos = queue.Dequeue();

//            for (int i = 0; i < neighbor.Length; i++)
//            {
//                Vector movePos = new Vector(currentPos.y + neighbor[i].y, currentPos.x + neighbor[i].x);
//                movePos.count = currentPos.count + 1;

//                if (movePos.y < 0 || movePos.y >= maps.GetLength(0) || movePos.x < 0 || movePos.x >= maps.GetLength(1) || maps[movePos.y, movePos.x] == 0)
//                    continue;

//                if (movePos.y == maps.GetLength(0) - 1 &&
//                   movePos.x == maps.GetLength(1) - 1)
//                    return movePos.count;

//                maps[movePos.y, movePos.x] = 0;
//                queue.Enqueue(movePos);
//            }
//        }

//        return -1;
//    }
////}