using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using WcfHotelService.Common;
using WcfHotelService.Common.Entities;

namespace WcfHotelService.Service
{
    /// <summary>
    /// This is contract class for defining hotel related srevices which will be implemented by HotelSearchService class
    /// </summary>
    [ServiceContract(SessionMode = SessionMode.NotAllowed)] 
    public interface IHotelSearchService
    {
        [WebInvoke(UriTemplate = "/search", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Common.Responses.ResponseSearch Search(String hotelName = null
                        , String destination = null
                        , Double? rangeFrom = null
                        , Double? rangeTo = null
                        , DateTime? dateFrom = null
                        , DateTime? dateTo = null
                        , String sortByName = null
                        , String sortByPrice = null);
    }
}
