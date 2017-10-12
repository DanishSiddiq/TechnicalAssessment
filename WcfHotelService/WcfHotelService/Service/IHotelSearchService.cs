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

        [OperationContract,
         WebGet(UriTemplate = "/echo/{echoString}",
         ResponseFormat = WebMessageFormat.Json)]
        String Echo(String echoString);

        [OperationContract]
        [WebInvoke(UriTemplate = "/searching", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ResponseHotel Search(RequestHotel RequestHotel);

        
        [OperationContract]
        [WebInvoke(UriTemplate = "/testing", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ResponseHotel test(CustomMessage customMessage);                        
    }
}
