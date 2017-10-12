using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;
using WcfHotelService.Common.Entities;
using WcfHotelService.Common.Exceptions;

namespace WcfHotelService.Common.Responses
{
    /// <summary>
    /// this class will be returned for all operations performed on hotel service WCF
    /// It will hold operation success or failure code and custom messages in case of any failures or warnings along data
    /// </summary>
    [DataContract]
    public class ResponseSearch
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
        public List<CustomMessage> LstCustomMessage
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public IList<Entities.Hotel> LstResponseData
        {
            get;
            set;
        }
    }
}
