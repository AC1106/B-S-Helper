using System;

class Program
{
    public static (int, int, int) MaxProfit(int[] prices, int fee)
    {
        if (prices == null || prices.Length == 0)
            return (0, 0, 0);

        int n = prices.Length;
        int[,] dp = new int[n, 2];
        int[,] buySellDays = new int[n, 2];

        dp[0, 0] = 0;            // 第一天不持有股票的最大收益
        dp[0, 1] = -prices[0] - fee; // 第一天持有股票的最大收益，扣除交易費
        buySellDays[0, 1] = 0;   // 第一天買入股票的日子

        for (int i = 1; i < n; i++)
        {
            // 不持有股票（可能是前一天也不持有，或者是今天賣出，扣除交易費）
            if (dp[i - 1, 0] > dp[i - 1, 1] + prices[i] - fee)
            {
                dp[i, 0] = dp[i - 1, 0];
                buySellDays[i, 0] = buySellDays[i - 1, 0];
            }
            else
            {
                dp[i, 0] = dp[i - 1, 1] + prices[i] - fee;
                buySellDays[i, 0] = i;  // 記錄賣出日
            }

            // 持有股票（可能是前一天也持有，或者是今天買入，扣除交易費）
            if (dp[i - 1, 1] > dp[i - 1, 0] - prices[i] - fee)
            {
                dp[i, 1] = dp[i - 1, 1];
                buySellDays[i, 1] = buySellDays[i - 1, 1];
            }
            else
            {
                dp[i, 1] = dp[i - 1, 0] - prices[i] - fee;
                buySellDays[i, 1] = i;  // 記錄買入日
            }
        }

        int maxProfit = dp[n - 1, 0];
        int sellDay = buySellDays[n - 1, 0];
        int buyDay = 0;
        for (int i = 0; i <= sellDay; i++)
        {
            if (prices[i] == prices[sellDay] - maxProfit + fee)
            {
                buyDay = i;
                break;
            }
        }

        return (buyDay, sellDay, maxProfit);
    }

    static void Main(string[] args)
    {
        int[] prices = { 7, 1, 5, 3, 6, 4 };
        int fee = 1;  // 假設每次交易的費用為1
        var result = MaxProfit(prices, fee);
        Console.WriteLine($"最佳買入時間: 第 {result.Item1 + 1} 天, 最佳賣出時間: 第 {result.Item2 + 1} 天, 最大利潤: {result.Item3}");
    }
}
