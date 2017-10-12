using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WcfHotelService.Common.Entities
{
    [DataContract]
    public class RequestHotel
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public String HotelName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public String Destination
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public Double? RangeFrom
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public Double? RangeTo
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime? DateFrom
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime? DateTo
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public String SortByName
        {
            get;
            set;
        }

       /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public String SortByPrice
        {
            get;
            set;
        }
    }
}
