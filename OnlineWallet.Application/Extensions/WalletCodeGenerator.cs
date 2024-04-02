using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Application.Extensions
{
    public static class WalletCodeGenerator
    {
        private static Random random = new Random();

        public static string GenerateRandom(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s=> s[random.Next(s.Length)]).ToArray());
        }
    }
}
