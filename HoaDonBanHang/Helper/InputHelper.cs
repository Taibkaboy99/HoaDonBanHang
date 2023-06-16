namespace HoaDonBanHang.Helper
{
    public class InputHelper
    {
        public static int KSN(string mes, string err = "Can phai nhap so nguyen!")
        {
            int so;
            bool check = true;
            do
            {
                Console.WriteLine(mes);
                check = int.TryParse(err, out so);
                if (!check)
                {
                    Console.WriteLine(err);
                }
            } while (!check);
            return so;
        }
        public static string KieuChuoi(string mes1, string err1 = "Loi", int min = 0, int max = int.MaxValue)
        {
            string chuoi;
            bool ok;
            do
            {
                Console.WriteLine(mes1);
                chuoi = Console.ReadLine();
                ok = chuoi.Length >= min && chuoi.Length <= max;
                if (!ok)
                {
                    Console.WriteLine(err1);
                }
            } while (!ok);
            return chuoi;
        }
    }
}
