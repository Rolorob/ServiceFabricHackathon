using System;
using System.Security.Cryptography;
using System.Text;

namespace Common.Utils
{
    public static class PartitionKeyResolver
    {
        public static long ToPartitionKey(string value)
        {
            var md5 = MD5.Create();
            var bytes = md5.ComputeHash(Encoding.ASCII.GetBytes(value));
            return BitConverter.ToInt64(bytes, 0);
        }
    }
}
