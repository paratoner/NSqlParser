using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSqlParser.Util
{
    public static class Extensions
    {
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }
        public static void DeleteCharAt(this StringBuilder stringBuilder, int index)
        {
            stringBuilder = stringBuilder.Remove(index, 1);
        }
    }
}
