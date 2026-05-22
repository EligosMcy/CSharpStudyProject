using System;
using System.Collections.Generic;
using System.Linq;


namespace BinarySearch
{
    class Program
    {
        private static int[] _bucketArray;

        private static int _bucketNum = 50;

        private static List<int> _bucketList;

        private static Random _random;

        static void Main(string[] args)
        {
            // _random = new Random();
            //
            // _bucketArray = Enumerable.Range(0, _bucketNum).ToArray();
            //
            // int index = _random.Next(0, _bucketNum);
            // int bucketItem = _bucketArray[index];
            //
            // Console.WriteLine($"判定的 Item Index 是{index}");
            //
            // index = binarySearch(_bucketArray, bucketItem);
            //
            // Console.WriteLine($"计算得到的 Item Index 是{index}");
            // Console.ReadKey();


            //
            // _bucketList = Enumerable.Range(0, _bucketNum).ToList();
            //
            // for (int i = 0; i < _bucketNum; i++)
            // {
            //     int randomIndex = _random.Next(0, _bucketNum);
            //
            //     int temp = _bucketList[i];
            //
            //     _bucketList[i] = _bucketList[randomIndex];
            //     _bucketList[randomIndex] = temp;
            // }
            //
            // Console.WriteLine("-- 打乱前 --");
            // foreach (int i in _bucketList)
            // {
            //     Console.WriteLine($"I 是{i}");
            // }
            // Console.WriteLine("-- 选择排序后 --");
            // List<int> newList = selectionSort(_bucketList);
            // foreach (int i in newList)
            // {
            //     Console.WriteLine($"I 是{i}");
            // }
            // Console.ReadKey();

            // greet("Eligos");
            // Console.ReadKey();


            ChopBlock(1246, 722);

            Console.ReadKey();
        }

        private static int binarySearch(int[] bucketList, int item)
        {
            int low;
            int high;
            int mid;

            int guess;

            int itemIndex = -1;
            int serarchCount = 0;

            if (bucketList != null && bucketList.Length > 0)
            {
                low = 0;
                high = bucketList.Length - 1;

                while (low <= high)
                {
                    Console.WriteLine($"第{serarchCount}次查找");

                    mid = (low + high) / 2;
                    guess = bucketList[mid];

                    if (guess == item)
                    {
                        itemIndex = mid;
                        break;
                    }
                    else if (guess > item)
                    {
                        high = mid - 1;
                    }
                    else
                    {
                        low = mid + 1;
                    }

                    serarchCount++;
                }
            }

            return itemIndex;
        }

        private static List<int> selectionSort(List<int> list)
        {
            List<int> newList = new List<int>();

            if (list != null && list.Count > 0)
            {
                int arrayCount = list.Count;

                for (int i = 0; i < arrayCount; i++)
                {
                    newList.Add(findSmallest(list));
                }
            }

            return newList;
        }

        private static int findSmallest(List<int> sourceList)
        {
            int smallest = sourceList[0];
            int smallestIndex = 0;

            for (int i = 0; i < sourceList.Count; i++)
            {
                if (sourceList[i] < smallest)
                {
                    smallest = sourceList[i];
                    smallestIndex = i;
                }
            }

            sourceList.Remove(smallest);

            return smallest;
        }

        private static void greet(string name)
        {
            Console.WriteLine($"Hello - {name}!");
            greet2(name);
            Console.WriteLine("getting ready to say bye");
            bye();
        }

        private static void greet2(string name)
        {
            Console.WriteLine($"How Are You - {name}?");
        }

        private static void bye()
        {
            Console.WriteLine("ok bye");
        }

        private static void ChopBlock(float length, float width)
        {
            float ratio = length / width;

            //按照宽进行最大方块切分
            float integerRatio = (float)Math.Floor(ratio);

            float fractionalRatio = ratio % 1;


            //拆分数据规模
            float allowanceWidth = length % width;

            float allowanceLength = (length - allowanceWidth) / integerRatio;

            Console.WriteLine($"Ratio: {ratio} - IntegerRatio: {integerRatio} - FractionalRatio: {fractionalRatio}");

            //递归基线条件
            if (fractionalRatio == 0)
            {
                Console.WriteLine($"拆分最小 方格的长度是:{width}");
            }
            else
            {
                Console.WriteLine($"AllowanceWidth: {allowanceWidth} - AllowanceLength: {allowanceLength}\n");
                ChopBlock(allowanceLength, allowanceWidth);
            }

            //欧几里得算法: 适用小地块的最大方块,也适用整块地的最大方块 (拆分原理)
        }
    }
}
