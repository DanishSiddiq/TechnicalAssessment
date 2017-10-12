using System;
using System.Collections.Generic;

using System.Runtime.Serialization;

namespace WcfHotelService.Common.Entities
{

    /// <summary>
    /// this class will be returned for all operations performed on hotel service WCF
    /// It will hold operation success or failure code and custom messages in case of any failures or warnings along data
    /// </summary>
    public class ResponseHotel
    {
        /// <summary>
        /// multiple codes based in response
        /// as of now o and 1 for success and failure
        /// </summary>
        /// 
        [DataMember]
        public Int32 Code
        {
            get;
            set;
        }

        /// <summary>
        /// to hold multiple messages
        /// </summary>
        [DataMember]
        public List<CustomMessage> Messages
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public IList<Hotel> Hotels
        {
            get;
            set;
        }
    }
}
