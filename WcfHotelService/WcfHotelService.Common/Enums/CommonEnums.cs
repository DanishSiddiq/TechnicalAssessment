using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfHotelService.Common.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public class CommonEnums
    {

        /// <summary>
        /// 
        /// </summary>
        public enum HotelSearchOrders
        {
            None,
            AscendingByName,
            AscendingByNameAndAscendingByPrice,
            AscendingByNameAndDescendingByPrice,
            AscendingByPrice,
            DescendingByNameAndAscendingByPrice,
            DescendingByPrice,
            DescendingByName,
            DescendingByNameAndDescendingByPrice
        }
    }
}
