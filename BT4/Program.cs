namespace MaTran
{
    class Program
    {
        static public void Main(string[] args)
        {
            int m, n;
            List<List<int>> matran = new List<List<int>>();

            // Tao ma tran random
            randomMaTran(matran, out n, out m);
            Console.Write("Nhap vi tri dong can xoa: ");
            String strK = Console.ReadLine();
            int k = int.Parse(strK);

            // Xuat ma tran
            xuatMaTran(matran, n, m);

            // Phan tu lon nhat
            int max, maxRow, maxCol;
            if (phanTuLonNhat(matran, n, m, out max, out maxRow, out maxCol))
                Console.WriteLine("Phan tu lon nhat ma tran la: " + max);
            else
                Console.WriteLine("Ma tran rong!");

            // Phan tu nho nhat
            int min;
            if (phanTuNhoNhat(matran, n, m, out min, out _, out _))
                Console.WriteLine("Phan tu nho nhat ma tran la: " + min);
            else
                Console.WriteLine("Ma tran rong!");

            // Dong co tong lon nhat
            int maxRowSum, maxRowIndex;
            if (dongCoTongLonNhat(matran, n, m, out maxRowSum, out maxRowIndex))
                Console.WriteLine("Vi tri dong co tong lon nhat " + maxRowSum + " la: " + (maxRowIndex + 1));
            else
                Console.WriteLine("Khong tim duoc dong co tong lon nhat");

            // Tong cac so khong phai la so nguyen to
            int notPrimeSum;
            tongCacSoKhongPhaiSNT(matran, n, m, out notPrimeSum);
            Console.WriteLine("Tong cac so khong la so nguyen to: " + notPrimeSum);

            // Xoa dong thu k
            if (xoaDong(matran, ref n, m, k))
            {
                Console.WriteLine("Ma tran sau khi xoa dong thu " + k);
                xuatMaTran(matran, n, m);
            }
            else
                Console.WriteLine("Vi tri dong can xoa khong hop le!");

            // Xoa cot chua phan tu lon nhat
            phanTuLonNhat(matran, n, m, out max, out _, out maxCol);
            if (xoaCot(matran, n, ref m, maxCol + 1))
            {
                Console.WriteLine("Ma tran sau khi xoa cot phan tu lon nhat (" + max + "):");
                xuatMaTran(matran, n, m);
            }
            
        }
        // Nhap n, m va tao ma tran random n x m
        static public void randomMaTran(List<List<int>> matran, out int n, out int m)
        {
            Random rand = new Random();
            matran.Clear();
            Console.Write("Nhap so dong: ");
            String strN = Console.ReadLine();
            Console.Write("Nhap so cot: ");
            String strM = Console.ReadLine();
            int.TryParse(strN, out n);
            int.TryParse(strM, out m);
            
            for (int i = 0; i < n; i++)
            {
                List<int> row = new List<int>();
                for (int j = 0; j < m; j++)
                {
                    row.Add(rand.Next(-100, 100));
                }
                matran.Add(row);
            }
        }
        // In ma tran
        static public void xuatMaTran(List<List<int>> matran, int n, int m)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write(matran[i][j].ToString() + ' ');
                }
                Console.WriteLine();
            }
        }
        // Tim phan tu lon nhat
        static public bool phanTuLonNhat(List<List<int>> matran, int n, int m, out int max, out int row, out int col)
        {
            max = int.MinValue;
            row = -1;
            col = -1;
            if (matran.Count == 0)
                return false;
            max = matran[0][0];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    if (matran[i][j] > max)
                    {
                        max = matran[i][j];
                        row = i;
                        col = j;
                    }
            }
            return true;
        }
        // Tim phan tu be nhat
        static public bool phanTuNhoNhat(List<List<int>> matran, int n, int m, out int min, out int row, out int col)
        {
            min = int.MinValue;
            row = -1;
            col = -1;
            if (matran.Count == 0)
                return false;
            min = matran[0][0];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    if (matran[i][j] < min)
                    {
                        min = matran[i][j];
                        row = i;
                        col = j;
                    }
            }
            return true;
        }
        // Tim dong co tong lon nhat
        static public bool dongCoTongLonNhat(List<List<int>> matran, int n, int m, out int maxSum, out int maxRow)
        {
            maxSum = int.MinValue;
            maxRow = -1;
            if (matran.Count == 0)
                return false;
            
            for (int i = 0; i < n; i++)
            {
                int sum = 0;
                for (int j = 0; j < m; j++)
                    sum += matran[i][j];
                if (i == 0 || sum > maxSum)
                {
                    maxRow = i;
                    maxSum = sum;
                }
            }
            return true;
        }
        // Kiem tra so nguyen to
        static public bool checkSNT(int x)
        {
            if (x < 2)
                return false;
            for (int i = 2; i <= Math.Sqrt(x); i++)
                if (x % i == 0)
                    return false;
            return true;
        }
        // Tinh tong cac so khong phai la so nguyen to
        static public void tongCacSoKhongPhaiSNT(List<List<int>> matran, int n, int m, out int sum)
        {
            sum = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0;j < m; j++)
                    if (!checkSNT(matran[i][j]))
                        sum += matran[i][j];
        }
        // Xoa dong
        static public bool xoaDong(List<List<int>> matran, ref int n, int m, int pos)
        {
            pos--;
            if (pos < 0 || pos >= n)
                return false;

            matran.RemoveAt(pos);
            n--;
            return true;
        }
        // Xoa cot
        static public bool xoaCot(List<List<int>> matran, int n, ref int m, int pos)
        {
            pos--;
            if (pos < 0 || pos >= m)
                return false;

            for (int i = 0; i < n; i++)
                matran[i].RemoveAt(pos);
            m--;

            return true;
        }
    }
}
