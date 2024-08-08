namespace GiaoHang
{
    internal class Program
    {
        static int N;
        static int[,] Distances;
        static int[,] DP;
        static int VISITED_ALL;

        static void Main(string[] args)
        {
            // Số điểm và khoảng cách giữa các điểm
            N = 4;
            Distances = new int[,]
            {
            { 0, 10, 15, 20 },
            { 10, 0, 35, 25 },
            { 15, 35, 0, 30 },
            { 20, 25, 30, 0 }
            };

            // Khởi tạo các biến cho quy hoạch động
            VISITED_ALL = (1 << N) - 1;
            DP = new int[N, 1 << N];

            // Đặt giá trị mặc định cho bảng DP
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < (1 << N); j++)
                {
                    DP[i, j] = -1;
                }
            }

            int result = TSP(1, 0);

            Console.WriteLine("Chi phi toi thieu la: " + result);
        }

        static int TSP(int mask, int pos)
        {
            // Nếu đã thăm tất cả các điểm
            if (mask == VISITED_ALL)
            {
                return Distances[pos, 0];
            }

            // Nếu đã tính toán trước đó
            if (DP[pos, mask] != -1)
            {
                return DP[pos, mask];
            }

            int minCost = int.MaxValue;

            // Thử đi đến các điểm chưa thăm
            for (int city = 0; city < N; city++)
            {
                if ((mask & (1 << city)) == 0)
                {
                    int newCost = Distances[pos, city] + TSP(mask | (1 << city), city);
                    minCost = Math.Min(minCost, newCost);
                }
            }

            return DP[pos, mask] = minCost;
        }
    }
}
