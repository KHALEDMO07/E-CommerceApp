namespace E_Commerce.Interfaces
{
    public interface ITokenBlacklist
    {

        bool IsTokenBlacklistedAsync(string token); 

        void AddTokenToBlacklistAsync(string token);
    }
}
