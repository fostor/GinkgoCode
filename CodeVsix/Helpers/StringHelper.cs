using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Fostor.CodeVsix
{
    public class StringHelper
    {
        /// <summary>
        /// 将字符串的首字母转换成大写，比如将user转换成User
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string InitialToUpper(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            return string.Concat(s.Substring(0, 1).ToUpper(), s.Substring(1));
        }

        /// <summary>
        /// 将字符串的首字母转换成小写，比如将User转换成user
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string InitialToLower(string s)
        {
            return string.Concat(s.Substring(0, 1).ToLower(), s.Substring(1));
        }
        /// <summary>
        /// 将特定分隔符组成的字符串转换成Pascal形式
        /// </summary>
        /// <param name="s"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToPascalCase(string s, string separator)
        {
            string[] ss = s.Split(separator.ToCharArray());
            StringBuilder sb = new StringBuilder();
            foreach (string item in ss)
            {
                if (item.Length > 2)
                {
                    sb.Append(item.Substring(0, 1).ToUpper() + item.Substring(1).ToLower());
                }
                else
                {
                    sb.Append(item.ToUpper());
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 将特定分隔符组成的字符串转换成Camel形式
        /// </summary>
        /// <param name="s"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToCamelCase(string s, string separator)
        {
            return InitialToLower(ToPascalCase(s, separator));
        }
        /// <summary>
        /// 单词变成单数形式
        /// </summary>
        public static string ToSingular(string word)
        {
            Regex plural1 = new Regex("(?<keep>[^aeiou])ies$");
            Regex plural2 = new Regex("(?<keep>[aeiou]y)s$");
            Regex plural3 = new Regex("(?<keep>[sxzh])es$");
            Regex plural4 = new Regex("(?<keep>[^sxzhyu])s$");

            if (plural1.IsMatch(word))
                return plural1.Replace(word, "${keep}y");
            else if (plural2.IsMatch(word))
                return plural2.Replace(word, "${keep}");
            else if (plural3.IsMatch(word))
                return plural3.Replace(word, "${keep}");
            else if (plural4.IsMatch(word))
                return plural4.Replace(word, "${keep}");

            return word;
        }

        /// <summary>
        /// 单词变成复数形式
        /// </summary>
        public static string ToPlural(string word)
        {
            Regex plural1 = new Regex("(?<keep>[^aeiou])y$");
            Regex plural2 = new Regex("(?<keep>[aeiou]y)$");
            Regex plural3 = new Regex("(?<keep>[sxzh])$");
            Regex plural4 = new Regex("(?<keep>[^sxzhy])$");

            if (plural1.IsMatch(word))
                return plural1.Replace(word, "${keep}ies");
            else if (plural2.IsMatch(word))
                return plural2.Replace(word, "${keep}s");
            else if (plural3.IsMatch(word))
                return plural3.Replace(word, "${keep}es");
            else if (plural4.IsMatch(word))
                return plural4.Replace(word, "${keep}s");

            return word;
        }
        /// <summary>
        /// 反转字符串
        /// </summary>
        /// <returns></returns>
        public static string Reverse(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            char[] characters = s.ToCharArray();
            Array.Reverse(characters);
            return new string(characters);
        }
    }
}
