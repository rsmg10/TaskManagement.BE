using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;
using BCryptNet = BCrypt.Net.BCrypt;

namespace MITT.Services.Helpers
{
    public static class Hasher
    {
        public static string Hash(this string password) => BCryptNet.HashPassword(password, BCryptNet.GenerateSalt());

        public static bool Verify(this string password, string saltHash) => BCryptNet.Verify(password, saltHash);

        public static string GenerateDefaultPassword() => RandomNumberGenerator.GetInt32(0, 999999).ToString();
    }
}