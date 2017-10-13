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
        /// post :: response is in JSON but then in ajax needs conversion for date time data members
        /// </summary>
        /// <param name="RequestHotel"></param>
        /// <returns></returns>
        [OperationContract, WebInvoke(UriTemplate = "/search_json", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ResponseHotel SearchResultJson(RequestHotel RequestHotel);

        /// <summary>
        /// post :: response is in XML to avoid additional effort in ajax for conversion of date time
        /// </summary>
        /// <param name="RequestHotel"></param>
        /// <returns></returns>
        [OperationContract, WebInvoke(UriTemplate = "/search_xml", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Xml)]
        ResponseHotel SearchResultXML(RequestHotel RequestHotel);

        /// <summary>
        /// get :: response is in JSON but then in ajax needs conversion for date time data members :: get call
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
        [OperationContract, WebGet(UriTemplate = "/get_json?name={hotelName}&destination={destination}&price_from={rangeFrom}&price_to={rangeTo}&date_from={dateFrom}&date_to={dateTo}&name_order={sortByName}&price_order={sortByPrice}"
            , ResponseFormat = WebMessageFormat.Json)]
        ResponseHotel SearchResultJson_Get(String hotelName, String destination, String rangeFrom, String rangeTo, String dateFrom, String dateTo, String sortByName, String sortByPrice);

        /// <summary>
        /// get :: response is in XML to avoid additional effort in ajax for conversion of date time
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
        [OperationContract, WebGet(UriTemplate = "/get_xml?name={hotelName}&destination={destination}&price_from={rangeFrom}&price_to={rangeTo}&date_from={dateFrom}&date_to={dateTo}&name_order={sortByName}&price_order={sortByPrice}"
            , ResponseFormat = WebMessageFormat.Xml)]
        ResponseHotel SearchResultXML_Get(String hotelName, String destination, String rangeFrom, String rangeTo, String dateFrom, String dateTo, String sortByName, String sortByPrice);


        /// <summary>
        /// Just to ping WCF service to check it is working fine
        /// </summary>
        /// <param name="echoString"></param>
        /// <returns></returns>
        [OperationContract, WebGet(UriTemplate = "/search_json_get/{echoString}", ResponseFormat = WebMessageFormat.Json)]
        String Echo(String echoString);
    }
}
