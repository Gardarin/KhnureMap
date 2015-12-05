using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map.Model
{
    public class Model
    {
        public List<Node> AllNodes;
        public Node NodeStart;
        public Node NodeFinish;
        public int[,] Array;

        public Model(int[,] array)
        {
            Array = array;
            AllNodes = new List<Node>();
        }

        public void SetStartFinish(Node start, Node finish)
        {
            NodeStart = start;
            NodeFinish = finish;
        }

        public List<Node> GetRout()
        {
            if (NodeStart == null || NodeFinish == null)
            {
                return null;
            }
            int s=FindInd(NodeStart);
            int f=FindInd(NodeFinish);
            List<Node> Result = new List<Node>();
            //Array = new int[AllNodes.Count, AllNodes.Count];
            int[] NodAr = new int[AllNodes.Count];
            List<int> Lar=new List<int>();
            for (int i = 0; i < NodAr.Length; ++i)
            {
                NodAr[i] = Int32.MaxValue;
            }

            NodAr[s] = 0;
            for(int i=0;i<NodAr.Length;++i)
            {
                if (NodAr[i] != Int32.MaxValue && !Lar.Contains(i))
                {
                    for (int j = 0; j < NodAr.Length; ++j)
                    {
                        if (Array[i, j] != 0 && !Lar.Contains(j))
                        {
                            if (NodAr[j] > (NodAr[i] + Array[i, j]))
                            {
                                NodAr[j] = NodAr[i] + Array[i, j];
                                
                            }
                        }
                    }
                    Lar.Add(i);
                    i = 0;
                }
            }
            int min=int.MaxValue;
            int res = f;
            int a = f;
            Result.Add(AllNodes[res]);
            for (; ; )
            {
                min = int.MaxValue;
                for (int i = 0; i < NodAr.Length; ++i)
                {
                    if (Array[a, i] != 0)
                    {
                        if (NodAr[i] + Array[a, i] < min)
                        {
                            min = NodAr[i] + Array[a, i];
                            res = i;
                        }
                    }
                    
                }
                a = res;
                Result.Add(AllNodes[res]);
                if (res == s)
                {
                    break;
                }
                
            }
            return Result;
        }

        public int FindInd(Node node)
        {
            int i = 0;
            foreach (Node n in AllNodes)
            {
                if (n.X == node.X && n.Y == node.Y)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }
    }
}
