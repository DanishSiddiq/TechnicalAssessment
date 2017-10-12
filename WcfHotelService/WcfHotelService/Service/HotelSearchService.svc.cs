using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;

using WcfHotelService.BAL;
using WcfHotelService.Common;
using WcfHotelService.Common.Extensions;
using WcfHotelService.Common.Exceptions;
using WcfHotelService.Common.Enums;
using WcfHotelService.Common.Entities;



namespace WcfHotelService.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HotelSearchService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select HotelSearchService.svc or HotelSearchService.svc.cs at the Solution Explorer and start debugging.
    public class HotelSearchService : IHotelSearchService
    {
        /// <summary>
        /// keeping optional parameters to provide flexibility of initialization and searching
        /// </summary>
        /// <param name="hotelName"></param>
        /// <param name="destination"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>        
        public Common.Responses.ResponseSearch Search(String hotelName = null, String destination = null, Double? rangeFrom = null, Double? rangeTo = null
            , DateTime? dateFrom = null, DateTime? dateTo = null, String sortByName = null, String sortByPrice = null)
        {
            // TODO: do something with the model
            List<Hotel> lstHotel = null;
            BAL.Business.HotelBAL hotelBAL = null;
            Utility utility = null;

            // Result should be sent with proper details in case of failures
            Common.Responses.ResponseSearch responseSearch = new Common.Responses.ResponseSearch();

            try
            {
                using (var client = new WebClient())
                {

                    // fetch json data and transform it from json into list data to perform calculations
                    var json = client.DownloadString(Constants.CONST_HOTEL_API);

                    // parsing json into list
                    utility = new Utility();
                    lstHotel = utility.parseJSONIntoHotelData(json);

                    // first check that it must contains some values
                    if (lstHotel != null)
                    {
                        // testing data
                        //hotelName = "hotel";
                        //destination = "MANILA";
                        //rangeFrom = 90;
                        //rangeTo = null;
                        //sortByPrice = "ASC";                        
                        dateFrom = null; // new DateTime(2020, 12, 5);
                        dateTo = new DateTime(2020, 12, 10); ; //new DateTime(2020, 12, 16);
                        
                        // initiating class at requirement
                        hotelBAL = new BAL.Business.HotelBAL();
                        lstHotel = hotelBAL.Search(lstHotel.AsQueryable()
                                                    , String.IsNullOrEmpty(hotelName) ? hotelName : hotelName.ToLower()
                                                    , String.IsNullOrEmpty(destination) ? destination : destination.ToLower()
                                                    , (rangeFrom.HasValue && rangeFrom.Value > 0) ? rangeFrom : null
                                                    , (rangeTo.HasValue && rangeTo.Value > 0) ? rangeTo : null
                                                    , dateFrom
                                                    , dateTo
                                                    , String.IsNullOrEmpty(sortByName) ? sortByName : sortByName.ToLower()
                                                    , String.IsNullOrEmpty(sortByPrice) ? sortByPrice : sortByPrice.ToLower());

                        // in case if it is desired to remove those availability records which do no meet requirements so un comment below lines
                        // hotelBAL.RemoveIrrelevantDataFromAvailability(lstHotel, dateFrom, dateTo);
                    }
                    else
                    {
                        if (responseSearch.LstCustomMessage.IsNull())
                        {
                            responseSearch.LstCustomMessage = new List<CustomMessage>();
                        }
                        responseSearch.LstCustomMessage.Add(new CustomMessage { Code = Constants.CONST_API_DO_NOT_CONTAIN_OFFERS });
                    }

                    // data
                    responseSearch.LstResponseData = lstHotel;

                    // success code
                    responseSearch.Code = Constants.CONST_RESULT_CODE_SUCCESS;
                }
            }
            catch (CustomException ex)
            {
                // all nested methods are defined with exception which specifically will populate message based on error with in internal system
                // but in case any failure happens which was not handled or it is external issue then general message will be assigned with failure
                if (ex.LstCustomMessage.IsNull())
                {
                    ex.LstCustomMessage = new List<CustomMessage>();
                    ex.LstCustomMessage.Add(new CustomMessage { Code = Constants.CONST_API_NOT_ACCESSIBLE });
                }

                // message lists in case of failure
                responseSearch.LstCustomMessage = new List<Common.Entities.CustomMessage>(ex.LstCustomMessage);

                // failure code
                responseSearch.Code = Constants.CONST_RESULT_CODE_FAILURE;
            }

            return responseSearch;
        }


        private void ParseMessages(List<Common.Entities.CustomMessage> lstCustomMessages)
        {
        }
    }
}
