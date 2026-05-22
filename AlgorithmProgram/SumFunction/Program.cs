using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumFunction
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] testIntArray = new int[6] { 2, 4, 5, 6, 27, 1 };

            int sumNumber = sum(testIntArray);

            int length = getArrayLength(testIntArray);

            int max = getMaxNum(testIntArray);

            Console.WriteLine($"Sum: {sumNumber}");
            Console.WriteLine($"Length: {length}");
            Console.WriteLine($"Max: {max}");
            Console.ReadKey();
        }

        private static int sum(int[] sumIntArray)
        {
            if (sumIntArray != null)
            {
                switch (sumIntArray.Length)
                {
                    case 1:
                        return sumIntArray[0];
                    case 0:
                        return 0;
                    default:
                    {
                        int[] newSumIntArray = removeFirstCopy(sumIntArray);

                        //每拷贝一次就减小一次数据规模
                        return sumIntArray[0] + sum(newSumIntArray);
                    }
                }
            }
            else
            {
                return 0;
            }

        }

        private static int getArrayLength(int[] intArray)
        {
            if (intArray != null)
            {
                switch (intArray.Length)
                {
                    case 1:
                        return 1;
                    case 0:
                        return 0;
                    default:
                        int[] newSumIntArray = removeFirstCopy(intArray);

                        //每拷贝一次就减小一次数据规模
                        return 1 + getArrayLength(newSumIntArray);
                }
            }
            else
            {
                return 0;
            }
        }

        private static int getMaxNum(int[] intArray)
        {
            if (intArray != null)
            {
                switch (intArray.Length)
                {
                    case 1:
                        return intArray[0];
                    case 0:
                        return 0;
                    default:
                        int[] newSumIntArray = removeFirstCopy(intArray);

                        int nowNum = intArray[0];

                        int otherMaxNum = getMaxNum(newSumIntArray);

                        //每拷贝一次就减小一次数据规模

                        return nowNum >= otherMaxNum ? nowNum : otherMaxNum;
                }
            }
            else
            {
                return 0;
            }
        }

        private static int[] removeFirstCopy(int[] array)
        {
            int[] copyArray = new int[array.Length - 1];

            Array.Copy(array, 1, copyArray, 0, array.Length - 1);

            return copyArray;
        }
    }
}
