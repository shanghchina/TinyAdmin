using System.Collections.Generic;

namespace Tiny.OPS.Common
{
    public static class StringHelper
    {
        public static string ReplaceBySigns(this string str, List<string> signs)
        {
            foreach (var sign in signs)
            {
                if (sign.Equals("'"))
                    str = str.Replace($"{sign}", $"{sign}'");
                else
                    str = str.Replace($"{sign}", $"[{sign}]");
            }
            return str;
        }
    }
}
