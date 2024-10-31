using E_Commerce.Interfaces;
using System.Collections.Concurrent;

namespace E_Commerce.Services
{
    public class TokenBlacklistService : ITokenBlacklist
    {
        private readonly ConcurrentBag<string> _blacklist = new ConcurrentBag<string>();
        public void AddTokenToBlacklistAsync(string token)
        {
             _blacklist.Add(token);
            
        }

        public bool IsTokenBlacklistedAsync(string token)
        {
           bool res = _blacklist.Contains(token);

            return res;

        }
    }
}
