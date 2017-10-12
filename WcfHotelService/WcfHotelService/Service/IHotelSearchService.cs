using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using WcfHotelService.Common;
using WcfHotelService.Common.Entities;

namespace WcfHotelService.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IHotelSearchService" in both code and config file together.
    [ServiceContract]
    public interface IHotelSearchService
    {
        [OperationContract]
        Common.Responses.ResponseSearch Search(String hotelName = null
                        , String destination = null
                        , Double? rangeFrom = null
                        , Double? rangeTo = null
                        , DateTime? DateFrom = null
                        , DateTime? dateTo = null
                        , String sortByName = null
                        , String sortByPrice = null);
    }
}
