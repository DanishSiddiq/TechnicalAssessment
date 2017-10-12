using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WcfHotelService.Common;
using WcfHotelService.Common.Extensions;
using WcfHotelService.Common.Exceptions;
using WcfHotelService.Common.Entities;
using Newtonsoft.Json.Linq;
using System.Globalization;

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
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public List<Hotel> parseJSONIntoHotelData(String json)
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
                                    Price = float.Parse(Convert.ToString(hotel[Constants.CONST_HOTEL_PRICE])),
                                    Availability = new List<Availability>()
                                };

                                JToken tokenAvailability = hotel[Constants.CONST_HOTEL_AVAILABILITY];
                                if (tokenAvailability != null && tokenAvailability.Count() > 0)
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
                if (ex.LstCustomMessage.IsNull())
                {
                    ex.LstCustomMessage = new List<CustomMessage>();
                }

                ex.LstCustomMessage.Add(new CustomMessage { Code = Constants.CONST_EXCEPTION_QUERY_ERROR, Message = Constants.CONST_EXCEPTION_QUERY_ERROR_DESCRIPTION });
                throw ex;
            }

            return lstHotel;
        }

        #endregion
    }
}
