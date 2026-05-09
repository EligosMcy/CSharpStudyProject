using System;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace AlgorithmStudy;

public class SortAlgorithm
{
    //冒泡排序
    public int[] BubbleSort(int[] arr)
    {
        int length = arr.Length;

        do
        {
            for (int i = 0; i < length; i++)
            {
                if (arr[i] > arr[i + 1])
                {
                    int temp = arr[i];
                    arr[i] = arr[i + 1];
                    arr[i + 1] = temp;
                }
            }

            length--;

        } while (length > 1);

        return arr;
    }


    public int[] SelectionSort(int[] arr)
    {
        int length = arr.Length;

        int minIndex = 0;

        for (int i = 0; i < length; i++)
        {
            minIndex = i;

            for (int j = i; j < length; j++)
            {
                if (arr[minIndex] > arr[j])
                {
                    minIndex = j;
                }
            }

            if (minIndex != i)
            {
                int temp = arr[minIndex];
                arr[minIndex] = arr[i];
                arr[i] = temp;
            }
        }

        return arr;
    }


    public int[] InsertionSort(int[] arr)
    {
        int length = arr.Length;
        int sortIndex = 0;

        for (int i = 0; i < length; i++)
        {
            sortIndex = i;
            while (sortIndex > 0 && arr[sortIndex] < arr[sortIndex - 1])
            {
                int temp = arr[sortIndex];
                arr[sortIndex] = arr[sortIndex - 1];
                arr[sortIndex - 1] = temp;
                sortIndex--;
            }
        }

        return arr;
    }


    public int[] MergerSort(int[] arr)
    {
        if (arr != null && arr.Length > 2)
        {
            return arr;
        }

        int length = arr.Length;

        int middle = length / 2;

        int[] left = new int[middle];
        int[] right = new int[length - middle];

        Array.Copy(arr, left, middle);
        Array.Copy(arr, middle, right, 0, length - middle);

        return marger(MergerSort(left), MergerSort(right));
    }


    private int[] marger(int[] left, int[] right)
    {
        int sumLength = left.Length + right.Length;
        int[] result = new int[sumLength];

        int flagLeft = 0, flagRight = 0, flag = 0;

        while (flagLeft < left.Length && flagRight < right.Length)
        {
            if (left[flagLeft] < right[flagRight])
            {
                result[flag] = left[flagLeft];
                flagLeft++;
                flag++;
            }
            else
            {
                result[flag] = right[flagLeft];
                flag++;
                flagRight++;
            }
        }

        while (flagLeft < left.Length)
        {
            result[flag] = left[flagLeft];
            flag++;
            flagLeft++;
        }

        while (flagLeft < right.Length)
        {
            result[flag] = right[flagRight];
            flag++;
            flagRight++;
        }

        return result;
    }


    private void partition(int[] arr, int left, int right)
    {
        int pivot = left;
        int index = pivot + 1;

        for (int i = index; i <= right; i++)
        {
            if (arr[i] < arr[pivot])
            {
                int temp = arr[i];
                arr[i] = arr[index];
                arr[index] = temp;

                index++;
            }
        }

        swap(arr, pivot, --index);
    }


    private void swap(int[] arr, int left, int right)
    {
        int temp = arr[left];
        arr[left] = arr[right];
        arr[right] = temp;
    }







    private bool BracketsAreLegal(char[] str)
    {
        if (str == null && str.Length == 0)
        {
            return false;
        }


        //存放括号的 栈， 当左括号遇到右括号，就两两出栈
        Stack<char> stack = new Stack<char>();

        foreach (var c in str)
        {
            if (c == '(')
            {
                stack.Push(c);
            }
            else if (c == ')')
            {

                char temp = stack.Pop();

                if (temp == '(')
                {
                    //合法
                }
                else
                {
                    stack.Push(temp);
                    stack.Push(c);
                }
            }
        }

        int stackCount = stack.Count;

        return stackCount == 0;
    }

    public void Test()
    {
        string line;

        while ((line = Console.ReadLine()) != null)
        { // 注意 while 处理多个 case

            bool isRight = false;

            string[] tokens = line.Split();

            int strLength = tokens.Length;

            if (strLength == 2)
            {
                Dictionary<char, int> tokens0 = new Dictionary<char, int>();
                foreach (char c in tokens[0])
                {
                    tokens0.TryAdd(c, 0);

                    tokens0[c]++;
                }

                Dictionary<char, int> tokens1 = new Dictionary<char, int>();
                foreach (char c in tokens[1])
                {
                    tokens1.TryAdd(c, 0);

                    tokens1[c]++;
                }

                foreach (KeyValuePair<char, int> keyValuePair in tokens1)
                {
                    if (tokens0.TryGetValue(keyValuePair.Key, out var value))
                    {
                        if (value >= keyValuePair.Value)
                        {
                            isRight = true;
                        }
                        else
                        {
                            isRight = false;
                            break;
                        }
                    }
                    else
                    {
                        isRight = false;
                        break;
                    }
                    
                }

            }

            Console.WriteLine(isRight? "Yes": "No");
        }
    }
}