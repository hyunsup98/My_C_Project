//using System;
//using System.Collections.Generic;

//public class Solution
//{
//    public string[] solution(string[] players, string[] callings)
//    {
//        //런타임 초과로 효율적인 접근을 위해 딕셔너리 사용
//        Dictionary<string, int> dic = new Dictionary<string, int>();

//        for (int i = 0; i < players.Length; i++)
//        {
//            //딕셔너리에 선수이름, 현재 등수 넣어주기
//            dic.Add(players[i], i);
//        }

//        for (int i = 0; i < callings.Length; i++)
//        {
//            //치고나갈 플레이어 인덱스
//            int index = dic[callings[i]];

//            //앞에 선수 이름(현재 플레이어가 3위면 -> 2위 선수 보기)
//            string prevPlayer = players[index - 1];

//            players[index - 1] = callings[i];
//            players[index] = prevPlayer;

//            dic[callings[i]] = index - 1;
//            dic[prevPlayer] = index;
//        }

//        return players;
//    }
//}