using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace QuickSort
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> _intList = new List<int>() { 5, 3, 2,2, 1, 4 };

            _intList = quickSortList(_intList);

            foreach (int i in _intList)
            {
                Console.WriteLine(i);
            }

            Console.ReadKey();
        }

        private static List<int> quickSortList(List<int> intList)
        {
            List<int> returnList = new List<int>();

            if (intList != null && intList.Count > 2)
            {
                int standardValue = intList[0];

                List<int> smallList = new List<int>();

                List<int> bigList = new List<int>();

                for (int i = 1; i < intList.Count; i++)
                {
                    int value = intList[i];

                    if (standardValue > value)
                    {
                        smallList.Add(value);
                    }
                    else
                    {
                        bigList.Add(value);
                    }
                }

                if (smallList.Count > 1)
                {
                    smallList = quickSortList(smallList);
                }

                if (bigList.Count > 1)
                {
                    bigList = quickSortList(bigList);
                }

                foreach (int i in smallList)
                {
                    returnList.Add(i);
                }

                returnList.Add(standardValue);

                foreach (int i in bigList)
                {
                    returnList.Add(i);
                }
            }
            else
            {
                foreach (int i in intList)
                {
                    returnList.Add(i);
                }
            }

            return returnList;

        }
    }
}
