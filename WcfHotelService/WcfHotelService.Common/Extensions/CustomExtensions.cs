using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfHotelService.Common.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class CustomExtensions
    {
        /// <summary>
        /// to check is list is null or empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="List"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this IList<T> List)
        {
            return (List == null);
        }
    }
}
