﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;
using WcfHotelService.Common.Entities;
using WcfHotelService.Common.Exceptions;

namespace WcfHotelService.Common.Responses
{
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
