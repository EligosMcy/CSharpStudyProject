namespace AlgorithmStudy;

public class Solution
{
    /*
     *  题目1：盛最多水的容器

        问题描述

        给你 n 个非负整数 a1，a2，...，an，每个数代表坐标中的一个点 (i, ai) 。在坐标内画 n 条垂直线，垂直线 i 的两个端点分别为 (i, ai) 和 (i, 0) 。找出其中的两条线，使得它们与 x 轴共同构成的容器可以容纳最多的水。

        解决方案

        使用双指针法。从两端开始，每次移动高度较小的指针，因为容器的容量由较短的边决定。时间复杂度 O(n)，空间复杂度 O(1)。
     */
    public int MaxArea(int[] heights)
    {
        int left = 0;
        int right = heights.Length - 1;
        int maxArea = 0;

        while (left < right)
        {
            maxArea = Math.Max(maxArea, calculateArea(heights, left, right));

            int leftHeight = heights[left];
            int rightHeight = heights[right];

            if (leftHeight < rightHeight)
            {
                left++;
            }
            else
            {
                right--;
            }
        }

        return maxArea;
    }

    private int calculateArea(int[] heights, int left, int right)
    {
        int width = right - left;
        int height = Math.Min(heights[left], heights[right]);

        return width * height;
    }



    public int ClimbStairs(int n)
    {
        if (n <= 2)
        {
            return n;
        }


        int result = 0;

        int prev2 = 1;
        int prev1 = 2;

        for (int i = 3; i < n; i++)
        {
            result = prev2 + prev1;
            prev2 = prev1;
            prev1 = result;
        }

        return result;
    }


    public int ClimbStairsNdp(int n)
    {
        if (n <= 1)
        {
            return n;
        }

        int[] dp = new int[n + 1];
        dp[0] = 1;

        for (int i = 1; i < n; i++)
        {
            for (int j = 0; j < i; j++)
            {
                dp[i] += dp[j];
            }
        }

        return dp[n];
    }

    public void Rotate(int[][] martix)
    {
        int n = martix.Length;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                (martix[i][j], martix[j][i]) = (martix[j][i], martix[i][j]);
            }
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n / 2; j++)
            {
                (martix[i][j], martix[i][n - 1 - j]) = (martix[i][n - 1 - j], martix[i][j]);
            }
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public double Percentage { get; set; }
    }

    public Item SelectItem(List<Item> items)
    {
        if (items == null || items.Count == 0)
        {
            return null;
        }

        double totalWeight = items.Sum(item => item.Percentage);

        Random random = new Random();
        double randomValue = random.NextDouble() * totalWeight;

        double cumulativeWeight = 0;

        foreach (var item in items)
        {
            cumulativeWeight += randomValue * item.Percentage;
            if (randomValue <= cumulativeWeight)
            {
                return item;
            }
        }

        return items.Last();
    }



    public int MaxNumberOfGroups(int[] tiles)
    {
        if (tiles == null || tiles.Length == 0)
        {
            return 0;
        }

        Dictionary<int, int> countMap = new Dictionary<int, int>();

        foreach (int tile in tiles)
        {
            if (!countMap.ContainsKey(tile))
            {
                countMap[tile] = 0;
            }

            countMap[tile] += 1;
        }

        List<int> sortedTiles = countMap.Keys.OrderBy(x => x).ToList();

        int groups = 0;

        foreach (int tile in sortedTiles)
        {
            while (countMap[tile] > 0)
            {
                if (countMap.ContainsKey(tile + 1)
                    && countMap.ContainsKey(tile + 2)
                    && countMap[tile + 1] > 0
                    && countMap[tile + 2] > 0)
                {
                    countMap[tile]--;
                    countMap[tile + 1]--;
                    countMap[tile + 2]--;
                    groups++;
                }
                else if (countMap[tile] >= 3)
                {
                    countMap[tile] -= 3;
                    groups++;
                }
                else
                {
                    break;
                }
            }
        }

        return groups;
    }
}

