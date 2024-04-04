namespace OnlineWallet.Application.Extensions
{
    public static class PropertyValueGetter
    {
        public static T GetValue<T>(object src, string propName)
        {
            return (T)src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
