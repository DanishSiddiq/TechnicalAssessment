using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace WcfHotelService.Common.Entities
{
    [DataContract]
    public class Availability 
    {
        /// <summary>
        /// From Availability Date dd/MM/yyyy
        /// </summary>
        [DataMember]
        public DateTime fromDate
        {
            get;
            set;
        }

        /// <summary>
        /// To Availability Date dd/MM/yyyy
        /// </summary>
        [DataMember]
        public DateTime toDate
        {
            get;
            set;
        }
    }
}
