using System;
using System.Collections.Generic;

namespace _250925
{
    public class Node<T>
    {
        //각 노드가 들고 있을 값
        public T Value { get; set; }

        public List<Edge<T>> Neighbors { get; private set; }

        public Node(T value)
        {
            Neighbors = new List<Edge<T>>();
            Value = value;
        }
    }

    public class Edge<T>
    {
        public Node<T> To { get; set; }
        public int Weight { get; set; }

        public Edge(Node<T> to, int weight)
        {
            To = to;
            Weight = weight;
        }
    }

    public class SimpleGraph<T>
    {
        List<Node<T>> nodes = new List<Node<T>>();

        //노드 추가 메서드
        public Node<T> AddNode(T value)
        {
            var node = new Node<T>(value);
            nodes.Add(node);
            return node;
        }

        //단방향 간선 추가
        public void AddEdge(Node<T> from, Node<T> to, int weight)
        {
            from.Neighbors.Add(new Edge<T>(to, weight));
        }

        //양방향 간선 추가
        public void AddUndirectedEdge(Node<T> a, Node<T> b, int weight)
        {
            a.Neighbors.Add(new Edge<T>(b, weight));
            b.Neighbors.Add(new Edge<T>(a, weight));
        }

        //그래프 출력
        public void PrintGraph()
        {
            foreach (var node in nodes)
            {
                Console.Write($"{node.Value} → ");

                foreach (var edge in node.Neighbors)
                {
                    Console.Write($"{edge.To.Value}, 가중치: {edge.Weight}");
                }

                Console.WriteLine();
            }
        }

        //public void BFS(Node<T> start, Node<T> target)
        //{
        //    Queue<Node<T>> queue = new Queue<Node<T>>();
        //    List<Node<T>> visited = new List<Node<T>>();

        //    queue.Enqueue(start);

        //    while(queue.Count > 0)
        //    {
        //        Node<T> current = queue.Dequeue();

        //        if (visited.Contains(current)) continue;

        //        Console.WriteLine($"지하철 역 방문: {current.Value}");
        //        visited.Add(current);

        //        if(current.Equals(target))
        //        {
        //            Console.WriteLine($"목표 역: {target.Value}");
        //            return;
        //        }

        //        foreach(var neighbor in current.Neighbors)
        //        {
        //            if (!visited.Contains(neighbor))
        //            {
        //                queue.Enqueue(neighbor);
        //            }
        //        }
        //    }
        //    Console.WriteLine("경로 탐색 실패");
        //}

        //public void DFS(Node<T> start, Node<T> target)
        //{
        //    Stack<Node<T>> stack = new Stack<Node<T>>();
        //    List<Node<T>> visited = new List<Node<T>>();

        //    stack.Push(start);

        //    while (stack.Count > 0)
        //    {
        //        Node<T> current = stack.Pop();

        //        if (visited.Contains(current)) continue;

        //        Console.WriteLine($"지하철 역 방문: {current.Value}");
        //        visited.Add(current);

        //        if (current.Equals(target))
        //        {
        //            Console.WriteLine($"목표 역: {target.Value}");
        //            return;
        //        }

        //        foreach (var neighbor in current.Neighbors)
        //        {
        //            if (!visited.Contains(neighbor))
        //            {
        //                stack.Push(neighbor);
        //            }
        //        }
        //    }
        //    Console.WriteLine("경로 탐색 실패");
        //}
    }
}
