using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace SocialNetworkBackEnd
{
    public static class Utils
    {
        public static T ConvertFromDBVal<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return default(T);
            }
            else
            {
                return (T)obj;
            }
        }
    }
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "kotiki_s_korotkimi_lapkami12345!!!";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }

    public class Constants 
    {
        public const string EMAIL_NOT_FOUND = "email not found";
        public const string PASSWORD_NOT_CORRECT = "password not correct";
        public const string GOOD = "GOOD";
    }
}
