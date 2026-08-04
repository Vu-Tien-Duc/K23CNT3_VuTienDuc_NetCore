using System.Security.Cryptography;
using System.Text;

namespace K23CNT3_VuTienDuc_2310900023.Utilities
{
    public static class VtdSecurity
    {
        // Hàm mã hóa MD5
        public static string ComputeMD5Hash(string rawData)
        {
            // Tạo đối tượng MD5
            using (MD5 md5Hash = MD5.Create())
            {
                // Chuyển chuỗi thành mảng byte và băm
                byte[] bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Chuyển mảng byte thành chuỗi Hex string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}