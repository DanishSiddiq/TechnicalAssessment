using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace WcfHotelService.Common.Entities
{
    [DataContract]
    public class Hotel : IEntitiesBase
    {
        /// <summary>
        /// Hotel Name
        /// </summary>
        [DataMember]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Hotel Price: later will be used for logical range field
        /// </summary>
        [DataMember]
        public Double Price
        {
            get;
            set;
        }

        /// <summary>
        /// Hotel City
        /// </summary>
        [DataMember]
        public string City
        {
            get;
            set;
        }

        /// <summary>
        /// Hotel Availability
        /// </summary>
        [DataMember]
        public List<Availability> Availability
        {
            get;
            set;
        }
    }
}
