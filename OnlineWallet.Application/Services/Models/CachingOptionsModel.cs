namespace OnlineWallet.Application.Services.Models
{
    public class CachingOptionsModel
    {
        public int AbsoluteExpiration { get; set; }
        public int SlidingExpiration { get; set; }
        public int Size { get; set; }
    }
}
