using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using WcfHotelService.Common;
using WcfHotelService.Common.Entities;
using WcfHotelService.Common.Exceptions;
using WcfHotelService.Common.Extensions;


namespace WcfHotelService.Service
{
    /// <summary>
    /// Hotel service class implement contract for all hotel related services
    /// Currently it is used for searching hotels by pulling JSON information from another source but it can be extended for further operations
    /// </summary>
    public class HotelSearchService : IHotelSearchService
    {

        public string Echo(string echoString)
        {
            return echoString;
        }

        /// <summary>
        /// keeping optional parameters to provide flexibility of searching
        /// JSON returned from URI is transformed into more readable object formats to perform operations easily
        /// </summary>
        /// <param name="hotelName"></param>
        /// <param name="destination"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>        
        public ResponseHotel Search(RequestHotel RequestHotel)
        {

            // TODO: do something with the model
            List<Hotel> lstHotel = null;
            BAL.Business.HotelBAL hotelBAL = null;
            Utility utility = null;

            // Result should be sent with proper details in case of failures
            ResponseHotel responseSearch = new ResponseHotel();

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
                        //dateFrom = null; // new DateTime(2020, 12, 5);
                        //dateTo = new DateTime(2020, 12, 10); ; //new DateTime(2020, 12, 16);
                        
                        // initiating class at requirement
                        hotelBAL = new BAL.Business.HotelBAL();
                        lstHotel = hotelBAL.Search(lstHotel.AsQueryable()
                                                    , String.IsNullOrEmpty(RequestHotel.HotelName) ? RequestHotel.HotelName : RequestHotel.HotelName.ToLower()
                                                    , String.IsNullOrEmpty(RequestHotel.Destination) ? RequestHotel.Destination : RequestHotel.Destination.ToLower()
                                                    , (RequestHotel.RangeFrom.HasValue && RequestHotel.RangeFrom.Value > 0) ? RequestHotel.RangeFrom : null
                                                    , (RequestHotel.RangeTo.HasValue && RequestHotel.RangeTo.Value > 0) ? RequestHotel.RangeTo : null
                                                    , RequestHotel.DateFrom
                                                    , RequestHotel.DateTo
                                                    , String.IsNullOrEmpty(RequestHotel.SortByName) ? RequestHotel.SortByName : RequestHotel.SortByName.ToLower()
                                                    , String.IsNullOrEmpty(RequestHotel.SortByPrice) ? RequestHotel.SortByPrice : RequestHotel.SortByPrice.ToLower());

                        // in case if it is desired to remove those availability records which do no meet requirements so un comment below lines
                        // hotelBAL.RemoveIrrelevantDataFromAvailability(lstHotel, dateFrom, dateTo);
                    }
                    else
                    {
                        if (responseSearch.Messages.IsNull())
                        {
                            responseSearch.Messages = new List<CustomMessage>();
                        }
                        responseSearch.Messages.Add(new CustomMessage { Code = Constants.CONST_API_DO_NOT_CONTAIN_OFFERS
                        ,
                                                                                Message = Constants.CONST_API_DO_NOT_CONTAIN_OFFERS_DESCRIPTION
                        });
                    }

                    // data
                    responseSearch.Hotels = lstHotel;

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
                    ex.LstCustomMessage.Add(new CustomMessage { Code = Constants.CONST_API_NOT_ACCESSIBLE, Message = Constants.CONST_API_NOT_ACCESSIBLE_DESCRIPTION });
                }

                // message lists in case of failure
                responseSearch.Messages = new List<Common.Entities.CustomMessage>(ex.LstCustomMessage);

                // failure code
                responseSearch.Code = Constants.CONST_RESULT_CODE_FAILURE;
            }

            return responseSearch;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public ResponseHotel test(CustomMessage customMessage)
        {
            return new ResponseHotel { Code = Convert.ToInt32(customMessage.Code), Messages = null, Hotels = null };
        }

    }
}
