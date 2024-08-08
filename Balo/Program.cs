using System;

struct DoVat
{
    public string Ten;
    public int TrongLuong;
    public int GiaTri;
    public int PhuongAn;
}

namespace Balo
{
    internal class Program
    {
        static void TaoBang(DoVat[] dsVat, int n, int W, int[,] F, int[,] X)
        {
            for (int v = 0; v <= W; v++)
            {
                X[1, v] = v / dsVat[1].TrongLuong;
                F[1, v] = X[1, v] * dsVat[1].GiaTri;
            }

            for (int k = 2; k <= n; k++)
            {
                for (int v = 0; v <= W; v++)
                {
                    int FMax = F[k - 1, v];
                    int XMax = 0;
                    int yk = v / dsVat[k].TrongLuong;
                    for (int xk = 1; xk <= yk; xk++)
                    {
                        int Fk = F[k - 1, v - xk * dsVat[k].TrongLuong] + xk * dsVat[k].GiaTri;
                        if (Fk > FMax)
                        {
                            FMax = Fk;
                            XMax = xk;
                        }
                    }
                    F[k, v] = FMax;
                    X[k, v] = XMax;
                }
            }
        }

        static void TraBang(DoVat[] dsVat, int n, int W, int[,] F, int[,] X)
        {
            int v = W;
            for (int k = n; k >= 1; k--)
            {
                dsVat[k].PhuongAn = X[k, v];
                v -= X[k, v] * dsVat[k].TrongLuong;
            }
        }

        static void Main()
        {
            DoVat[] dsVat = new DoVat[]
            {
                new DoVat(),
                new DoVat { Ten = "A", TrongLuong = 20, GiaTri = 300 },
                new DoVat { Ten = "B", TrongLuong = 10, GiaTri = 100 },
                new DoVat { Ten = "C", TrongLuong = 30, GiaTri = 120 },
                new DoVat { Ten = "D", TrongLuong = 40, GiaTri = 240 },
                new DoVat { Ten = "E", TrongLuong = 50, GiaTri = 200 }
            };

            int n = 5; // Số lượng đồ vật
            int W = 50; // Trọng lượng tối đa của balo

            int[,] F = new int[n + 1, W + 1];
            int[,] X = new int[n + 1, W + 1];

            TaoBang(dsVat, n, W, F, X);
            TraBang(dsVat, n, W, F, X);

            Console.WriteLine("Phuong an chon do vat:");
            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine($"{dsVat[i].Ten}: {dsVat[i].PhuongAn}");
            }
        }
    }
}
