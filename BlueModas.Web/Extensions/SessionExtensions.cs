using Microsoft.AspNetCore.Http;

namespace BlueModas.Web.Extensions
{
    public static class Session
    {
        public static int GetOrElse(this ISession session, string key, int value)
        {
            return session.GetInt32(key) ?? value;
        }
    }
}
