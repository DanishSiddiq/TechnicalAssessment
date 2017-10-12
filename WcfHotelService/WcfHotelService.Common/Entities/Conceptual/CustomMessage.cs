using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace WcfHotelService.Common.Entities
{
    [DataContract]
    public class CustomMessage
    {
        /// <summary>
        /// message code
        /// </summary>
        [DataMember]
        public String Code
        {
            get;
            set;
        }

        /// <summary>
        /// message 
        /// </summary>
        [DataMember]
        public string Message
        {
            get;
            set;
        }
    }
}
