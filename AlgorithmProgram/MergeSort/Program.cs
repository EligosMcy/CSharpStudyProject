using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> temp = new List<int>() {5, 4, 6, 1};

            
            List<int> sortList = mergeSortList(temp);

            Console.WriteLine("\nSortList");
            foreach (int i in sortList)
            {
                Console.WriteLine(i);
            }

            Console.ReadKey();
        }

        private static List<int> mergeSortList(List<int> sortList)
        {
            var len = sortList.Count;

            if (len < 2)
            {
                return sortList;
            }
            else
            {
                var middle = len / 2;
                List<int> left = new List<int>();

                List<int> right = new List<int>();

                for (int i = 0; i < middle; i++)
                {
                    left.Add(sortList[i]);
                }

                for (int y = middle; y < len; y++)
                {
                    right.Add(sortList[y]);
                }

                return mergeList(mergeSortList(left), mergeSortList(right));
            }
        }

        private static List<int> mergeList(List<int> left, List<int> right)
        {
            var returnList = new List<int>();

            if (left.Count > 0 && right.Count > 0)
            {
                int smallTamp;

                if (left[0] < right[0])
                {
                    smallTamp = left[0];
                    left.RemoveAt(0);
                    returnList.Add(smallTamp);
                }
                else
                {
                    smallTamp = right[0];
                    right.RemoveAt(0);
                    returnList.Add(smallTamp);
                }
            }

            foreach (int i in left)
            {
                returnList.Add(i);
            }

            foreach (int i in right)
            {
                returnList.Add(i);
            }

            return returnList;
        }
    }
}
