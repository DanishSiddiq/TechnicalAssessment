using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using WcfHotelService.Common.Entities;
using WcfHotelService.Common.Exceptions;
using WcfHotelService.Common.Extensions;

namespace WcfHotelService.Common
{
    /// <summary>
    /// This class will hold gneralize methods which can be used accross projects with out any specific strong bindings
    /// All helpful and frequently used methods will be define here
    /// </summary>
    public class Utility
    {

        #region Public Method

        /// <summary>
        /// Calling API service and passing JSON to method to transform in collection 
        /// </summary>
        /// <returns></returns>
        public List<Hotel> CallHotelsAPI()
        {
            try
            {
                using (var client = new WebClient())
                {

                    // fetch json data and transform it from json into list data to perform calculations
                    var json = client.DownloadString(Constants.CONST_HOTEL_API);
                    return parseJSONIntoHotelData(json);
                }
            }
            catch (CustomException ex)
            {
                // detail code for whole messages trail
                if (ex.Messages.IsNull())
                {
                    ex.Messages = new List<CustomMessage>();
                    ex.Messages.Add(new CustomMessage { Code = Constants.CONST_API_NOT_ACCESSIBLE, Message = Constants.CONST_API_NOT_ACCESSIBLE_DESCRIPTION });
                }
                throw ex;
            }
            catch (Exception ex)
            {
                CustomException cEx = new CustomException(new List<CustomMessage> { new CustomMessage { Code = Constants.CONST_API_NOT_ACCESSIBLE, Message = Constants.CONST_API_NOT_ACCESSIBLE_DESCRIPTION } }
                    , ex.Message
                    , ex.InnerException);
                
                throw cEx;
            }
        }

        #endregion

        #region Private Method

        /// <summary>
        /// transforming json into collection for further modulation
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private List<Hotel> parseJSONIntoHotelData(String json)
        {
            List<Hotel> lstHotel = null;
            try
            {
                if (json != null)
                {
                    JObject jObject = JObject.Parse(json);
                    if (jObject != null)
                    {
                        JToken tokenHotel = jObject[Constants.CONST_HOTEL_HOTELS];
                        if (tokenHotel != null && tokenHotel.Count() > 0)
                        {
                            Hotel tempHotel = null;
                            foreach (JToken hotel in tokenHotel)
                            {
                                tempHotel = new Hotel
                                {
                                    Name = Convert.ToString(hotel[Constants.CONST_HOTEL_NAME]),
                                    City = Convert.ToString(hotel[Constants.CONST_HOTEL_CITY]),
                                    Price = Math.Round(float.Parse(Convert.ToString(hotel[Constants.CONST_HOTEL_PRICE])), 2),
                                    Availability = new List<Availability>()
                                };

                                JToken tokenAvailability = hotel[Constants.CONST_HOTEL_AVAILABILITY];
                                if (tokenAvailability != null && tokenAvailability.Any())
                                {
                                    foreach (JToken availability in tokenAvailability)
                                    {
                                        tempHotel.Availability.Add(new Availability
                                        {
                                            fromDate = DateTime.ParseExact(Convert.ToString(availability[Constants.CONST_HOTEL_AVAILABILITY_FROM]), "dd-MM-yyyy", CultureInfo.InvariantCulture),
                                            toDate = DateTime.ParseExact(Convert.ToString(availability[Constants.CONST_HOTEL_AVAILABILITY_TO]), "dd-MM-yyyy", CultureInfo.InvariantCulture)
                                        });
                                    }
                                }                                

                                // only initiate list object to save memory when first instance will be met
                                if (lstHotel == null)
                                {
                                    lstHotel = new List<Hotel>();
                                }
                                lstHotel.Add(tempHotel);
                            }
                        }
                    }
                }
            }
            catch (CustomException ex)
            {
                // detail code for whole messages trail
                if (ex.Messages.IsNull())
                {
                    ex.Messages = new List<CustomMessage>();
                }

                ex.Messages.Add(new CustomMessage { Code = Constants.CONST_EXCEPTION_JSON_NOT_PARSEABLE, Message = Constants.CONST_EXCEPTION_JSON_NOT_PARSEABLE_DESCRIPTION });
                throw ex;
            }
            catch (Exception ex)
            {
                CustomException cEx = new CustomException(new List<CustomMessage> { new CustomMessage { Code = Constants.CONST_EXCEPTION_JSON_NOT_PARSEABLE, Message = Constants.CONST_EXCEPTION_JSON_NOT_PARSEABLE_DESCRIPTION } }
                    , ex.Message
                    , ex.InnerException);

                throw cEx;
            }

            return lstHotel;
        }

        #endregion
    }
}
