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

        /// <summary>
        /// response is in JSON but then in ajax needs conversion for date time data members
        /// </summary>
        /// <param name="RequestHotel"></param>
        /// <returns></returns>
        [OperationContract, WebInvoke(UriTemplate = "/search_json", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ResponseHotel SearchResultJson(RequestHotel RequestHotel);
        
        /// <summary>
        /// response is in XML for to avoid additional effort in ajax for conversion of date time
        /// </summary>
        /// <param name="RequestHotel"></param>
        /// <returns></returns>
        [OperationContract, WebInvoke(UriTemplate = "/search_xml", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Xml)]
        ResponseHotel SearchResultXML(RequestHotel RequestHotel);
        
        /// <summary>
        /// Just to test that WCF service is working fine
        /// </summary>
        /// <param name="echoString"></param>
        /// <returns></returns>
        [OperationContract, WebGet(UriTemplate = "/search_json_get/{echoString}", ResponseFormat = WebMessageFormat.Json)]
        String Echo(String echoString);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HotelName"></param>
        /// <param name="Destination"></param>
        /// <param name="RangeFrom"></param>
        /// <param name="RangeTo"></param>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <param name="SortByName"></param>
        /// <param name="SortByPrice"></param>
        /// <returns></returns>
        [OperationContract, WebGet(UriTemplate = "/echo?name={HotelName}&destination={Destination}&price_from={RangeFrom}&price_to={RangeTo}&date_from={DateFrom}&date_to={DateTo}&name_order={SortByName}&price_order={SortByPrice}"
            , ResponseFormat = WebMessageFormat.Json)]
        ResponseHotel SearchResultJson_Get(String HotelName, String Destination, String RangeFrom, String RangeTo, String DateFrom, String DateTo, String SortByName, String SortByPrice);      
        
    }
}
