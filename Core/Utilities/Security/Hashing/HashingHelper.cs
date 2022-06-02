using System.Text;
using System.Security.Cryptography;


namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        // string password veriliyor, çıkış olarak hash ve salt değerleri döndürülür.
        public static void CreatePasswordHash (string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                // girilen passwordün byte dizisini encoding.utf8.getbytes ile veriyoruz
            }
        }



        
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }



    }
}
