using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfHotelService.Common.Extensions
{
    /// <summary>
    /// Purpose of creating custom exceptions to provide user friendly messages
    /// it will also be utilized for handling validations errors which can be multiple in most obvous cases
    /// this class is also helpful when it is desired to send multiple messages in case of any exception
    /// </summary>
    public static class CustomExtensions
    {
        /// <summary>
        /// to check is list is null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="List"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this IList<T> List)
        {
            return (List == null);
        }

        /// <summary>
        /// to check is list is null or empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="List"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IList<T> List)
        {
            return (List == null || List.Count == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="toCheck"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
            {
                return source.IndexOf(toCheck, comp) >= 0;
            }

    }
}
