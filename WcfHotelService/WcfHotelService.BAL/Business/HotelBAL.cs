using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using WcfHotelService.Common;
using WcfHotelService.Common.Entities;
using WcfHotelService.Common.Enums;
using WcfHotelService.Common.Exceptions;
using WcfHotelService.Common.Extensions;

namespace WcfHotelService.BAL.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class HotelBAL
    {
        
        #region Public Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestHotel"></param>
        /// <returns></returns>
        public List<Hotel> SearchHotels(RequestHotel requestHotel) 
        {
            Utility utility = null;
            List<Hotel> lstHotel = null;

            try
            {
                // first of all validate data for search parameters
                if (IsDataValidForSearch(requestHotel.RangeFrom, requestHotel.RangeTo, requestHotel.DateFrom, requestHotel.DateTo
                                        , requestHotel.SortByName, requestHotel.SortByPrice))
                {
                    // parsing json into list
                    utility = new Utility();
                    lstHotel = utility.CallHotelsAPI();
                 
                    // first check that it must contains some values
                    if (!lstHotel.IsNullOrEmpty())
                    {
                        lstHotel = Search(lstHotel.AsQueryable()
                                                    , String.IsNullOrEmpty(requestHotel.HotelName) ? requestHotel.HotelName : requestHotel.HotelName
                                                    , String.IsNullOrEmpty(requestHotel.Destination) ? requestHotel.Destination : requestHotel.Destination
                                                    , (requestHotel.RangeFrom.HasValue && requestHotel.RangeFrom.Value > 0) ? requestHotel.RangeFrom : null
                                                    , (requestHotel.RangeTo.HasValue && requestHotel.RangeTo.Value > 0) ? requestHotel.RangeTo : null
                                                    , requestHotel.DateFrom
                                                    , requestHotel.DateTo
                                                    , String.IsNullOrEmpty(requestHotel.SortByName) ? requestHotel.SortByName : requestHotel.SortByName
                                                    , String.IsNullOrEmpty(requestHotel.SortByPrice) ? requestHotel.SortByPrice : requestHotel.SortByPrice);

                        // in case if it is desired to remove those availability records which do no meet requirements so un comment below lines
                        // RemoveIrrelevantDataFromAvailability(lstHotel, dateFrom, dateTo);
                    }
                    else
                    {
                        CustomException ex = new CustomException { Messages = new List<CustomMessage>() };
                        ex.Messages.Add(new CustomMessage
                        {
                            Code = Constants.CONST_API_DO_NOT_CONTAIN_OFFERS
                            ,
                            Message = Constants.CONST_API_DO_NOT_CONTAIN_OFFERS_DESCRIPTION
                        });

                        throw ex;
                    }
                }
            }
            catch (CustomException ex)
            {
                // only add message if it is crashed as of query disorder
                if (ex.Messages.IsNull())
                {
                    ex.Messages = new List<CustomMessage>();
                    ex.Messages.Add(new CustomMessage { Code = Constants.CONST_EXCEPTION_QUERY_ERROR, Message = Constants.CONST_EXCEPTION_QUERY_ERROR_DESCRIPTION });
                }
                throw ex;
            }
            catch (Exception ex)
            {
                CustomException cEx = new CustomException(new List<CustomMessage> { new CustomMessage { Code = Constants.CONST_EXCEPTION_QUERY_ERROR, Message = Constants.CONST_EXCEPTION_QUERY_ERROR_DESCRIPTION } }
                    , ex.Message
                    , ex.InnerException);

                throw cEx;
            }

            return lstHotel.ToList();
        }

        /// <summary>
        /// removing availability sub collection entries which do not satisfy dates filtered requirements from hotels results after performing whole search operation
        /// </summary>
        /// <param name="lstHotel"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        public void RemoveIrrelevantDataFromAvailability(List<Hotel> lstHotel, DateTime? dateFrom, DateTime? dateTo) 
        {
            try
            {
                if(!lstHotel.IsNullOrEmpty())
                {
                    // since now we have limited data then we can further refine it
                    lstHotel.ForEach(h => h.Availability.RemoveAll(a => (dateFrom != null && DateTime.Compare(a.fromDate, dateFrom.Value) > 0)
                                                                        || (dateTo != null && DateTime.Compare(a.toDate, dateTo.Value) < 0)));
                }
            }
            catch (CustomException ex)
            {
                // only add message in this method if it crashed in this method
                if (ex.Messages.IsNull())
                {
                    ex.Messages = new List<CustomMessage>();
                    ex.Messages.Add(new CustomMessage
                    {
                        Code = Constants.CONST_EXCEPTION_QUERY_ERROR_REMOVING_IRRELVANT_ENTRIES
                        ,
                        Message = Constants.CONST_EXCEPTION_QUERY_ERROR_REMOVING_IRRELVANT_ENTRIES_DESCRIPTION
                    });
                }

                throw ex;
            }
            catch (Exception ex)
            {
                CustomException cEx = new CustomException(new List<CustomMessage> { new CustomMessage { Code = Constants.CONST_EXCEPTION_QUERY_ERROR_REMOVING_IRRELVANT_ENTRIES, Message = Constants.CONST_EXCEPTION_QUERY_ERROR_REMOVING_IRRELVANT_ENTRIES_DESCRIPTION } }
                    , ex.Message
                    , ex.InnerException);

                throw cEx;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="rangeStart"></param>
        /// <param name="rangeEnd"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public Boolean AreParameterTypesValidForSearch(String rangeFrom, String rangeTo, String dateFrom, String dateTo) 
        {
            List<CustomMessage> lstCustomMessage = null;
            Double num;
            DateTime dt;

            try
            {
                // checking price_from is valid double number or not
                if (!String.IsNullOrEmpty(rangeFrom))
                {
                    if (!Double.TryParse(rangeFrom, out num))
                    {
                        lstCustomMessage = lstCustomMessage.IsNull() ? new List<CustomMessage>() : lstCustomMessage;                        
                        lstCustomMessage.Add(new CustomMessage
                        {
                            Code = Constants.CONST_VALIDATION_PRICE_FROM_NOT_NUMBER
                         ,
                            Message = Constants.CONST_VALIDATION_PRICE_FROM_NOT_NUMBER_DESCRIPTION
                        });
                    }
                }

                // checking price_to is valid double number or not
                if (!String.IsNullOrEmpty(rangeTo))
                {
                    if (!Double.TryParse(rangeTo, out num))
                    {
                        lstCustomMessage = lstCustomMessage.IsNull() ? new List<CustomMessage>() : lstCustomMessage;
                        lstCustomMessage.Add(new CustomMessage
                        {
                            Code = Constants.CONST_VALIDATION_PRICE_TO_NOT_NUMBER
                         ,
                            Message = Constants.CONST_VALIDATION_PRICE_TO_NOT_NUMBER_DESCRIPTION
                        });
                    }
                }

                // checking from date is valid date or not
                if (!String.IsNullOrEmpty(dateFrom))
                {
                    if (!DateTime.TryParse(dateFrom, out dt))
                    {
                        lstCustomMessage = lstCustomMessage.IsNull() ? new List<CustomMessage>() : lstCustomMessage;
                        lstCustomMessage.Add(new CustomMessage
                        {
                            Code = Constants.CONST_VALIDATION_DATE_FROM_NOT_VALID
                         ,
                            Message = Constants.CONST_VALIDATION_DATE_FROM_NOT_VALID_DESCRIPTION
                        });
                    }
                }
                
                // checking from date is valid date or not
                if (!String.IsNullOrEmpty(dateFrom))
                {
                    if (!Regex.Match(dateFrom, Constants.CONST_REGEX_FOR_DD_MM_YYYY).Success)
                    {
                        lstCustomMessage = lstCustomMessage.IsNull() ? new List<CustomMessage>() : lstCustomMessage;
                        lstCustomMessage.Add(new CustomMessage
                        {
                            Code = Constants.CONST_VALIDATION_DATE_FROM_FORMAT_NOT_VALID
                         ,
                            Message = Constants.CONST_VALIDATION_DATE_FROM_FORMAT_NOT_VALID_DESCRIPTION
                        });
                    }
                }

                // checking from date is valid date or not
                if (!String.IsNullOrEmpty(dateTo))
                {
                    if (!DateTime.TryParse(dateTo, out dt))
                    {
                        lstCustomMessage = lstCustomMessage.IsNull() ? new List<CustomMessage>() : lstCustomMessage;
                        lstCustomMessage.Add(new CustomMessage
                        {
                            Code = Constants.CONST_VALIDATION_DATE_TO_NOT_VALID
                         ,
                            Message = Constants.CONST_VALIDATION_DATE_TO_NOT_VALID_DESCRIPTION
                        });
                    }
                }

                // checking from date is valid date or not
                if (!String.IsNullOrEmpty(dateTo))
                {
                    if (!Regex.Match(dateTo, Constants.CONST_REGEX_FOR_DD_MM_YYYY).Success)
                    {
                        lstCustomMessage = lstCustomMessage.IsNull() ? new List<CustomMessage>() : lstCustomMessage;
                        lstCustomMessage.Add(new CustomMessage
                        {
                            Code = Constants.CONST_VALIDATION_DATE_FROM_FORMAT_NOT_VALID
                         ,
                            Message = Constants.CONST_VALIDATION_DATE_FROM_FORMAT_NOT_VALID_DESCRIPTION
                        });
                    }
                }

                if (!lstCustomMessage.IsNull())
                {
                    // validation failed and all validation messages will later be transported to servcie class
                    throw new CustomException() { Messages = lstCustomMessage };
                }
            }
            catch (CustomException ex)
            {
                // in case some exception appears which was not defined and method crashed                
                // but in case any failure happens which was not handled or it is external issue then general message will be assigned with failure
                if (ex.Messages.IsNullOrEmpty())
                {
                    ex.Messages = new List<CustomMessage>();
                    ex.Messages.Add(new CustomMessage
                    {
                        Code = Constants.CONST_EXCEPTION_SEARCH_PARAMETERS_ERROR
                        ,
                        Message = Constants.CONST_EXCEPTION_SEARCH_PARAMETERS_ERROR_DESCRIPTION
                    });
                }

                throw ex;
            }
            catch (Exception ex)
            {
                CustomException cEx = new CustomException(new List<CustomMessage> { new CustomMessage { Code = Constants.CONST_EXCEPTION_SEARCH_PARAMETERS_ERROR, Message = Constants.CONST_EXCEPTION_SEARCH_PARAMETERS_ERROR_DESCRIPTION } }
                    , ex.Message
                    , ex.InnerException);

                throw cEx;
            }

            return true;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstHotel"></param>
        /// <param name="hotelName"></param>
        /// <param name="destination"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="sortByName"></param>
        /// <param name="sortByPrice"></param>
        /// <returns></returns>
        private List<Hotel> Search(IQueryable<Hotel> queryableHotels
                                    , String hotelName
                                    , String destination
                                    , Double? rangeFrom
                                    , Double? rangeTo
                                    , DateTime? dateFrom
                                    , DateTime? dateTo
                                    , String sortByName
                                    , String sortByPrice) 
        {

            try
            {

                // if nullable values are not provided then it will pass in first OR clause
                // else it will match data in second clause 
                queryableHotels = queryableHotels.Where(h => (String.IsNullOrEmpty(hotelName) || h.Name.Contains(hotelName, StringComparison.OrdinalIgnoreCase))
                                        && (String.IsNullOrEmpty(destination) || h.City.Contains(destination, StringComparison.OrdinalIgnoreCase))
                                        && (rangeFrom == null || h.Price >= rangeFrom)
                                        && (rangeTo == null || h.Price <= rangeTo)
                                        && (h.Availability.Any(a => (dateFrom == null || a.fromDate <= dateFrom) && (dateTo == null || a.toDate >= dateTo))));



                // multiple combinations of ordering are possible
                CommonEnums.HotelSearchOrders hotelSearchOrders = DecideSearchOrder(sortByName, sortByPrice);
                queryableHotels = ApplySort(queryableHotels, hotelSearchOrders);

            }
            catch (CustomException ex)
            {
                // only add message if it is crashed as of query disorder
                if (ex.Messages.IsNull())
                {
                    ex.Messages = new List<CustomMessage>();
                    ex.Messages.Add(new CustomMessage { Code = Constants.CONST_EXCEPTION_QUERY_ERROR, Message = Constants.CONST_EXCEPTION_QUERY_ERROR_DESCRIPTION });
                }

                throw ex;
            }
            catch (Exception ex)
            {
                CustomException cEx = new CustomException(new List<CustomMessage> { new CustomMessage { Code = Constants.CONST_EXCEPTION_QUERY_ERROR, Message = Constants.CONST_EXCEPTION_QUERY_ERROR_DESCRIPTION } }
                    , ex.Message
                    , ex.InnerException);

                throw cEx;
            }

            return queryableHotels.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        private Boolean IsDataValidForSearch(Double? rangeFrom, Double? rangeTo, DateTime? dateFrom, DateTime? dateTo, String sortByName, String sortByPrice) 
        {
            List<CustomMessage> lstCustomMessage = null;

            try
            {
                // checking from price should be lesser than to price
                if (rangeFrom.HasValue && rangeTo.HasValue)
                {
                    // from value is greater than to value
                    if (rangeFrom.Value > rangeTo.Value)
                    {
                        lstCustomMessage = lstCustomMessage.IsNull() ? new List<CustomMessage>() : lstCustomMessage;
                        lstCustomMessage.Add(new CustomMessage
                        {
                            Code = Constants.CONST_VALIDATION_FROM_PRICE_GREATER_THAN_TO_PRICE
                         ,
                            Message = Constants.CONST_VALIDATION_FROM_PRICE_GREATER_THAN_TO_PRICE_DESCRIPTION
                        });
                    }
                }

                // checking from date should be lesser than to date
                if (dateFrom.HasValue && dateTo.HasValue)
                {
                    // from value is greater than to value
                    if (dateFrom.Value > dateTo.Value)
                    {
                        lstCustomMessage = lstCustomMessage.IsNull() ?  new List<CustomMessage>() : lstCustomMessage;
                        lstCustomMessage.Add(new CustomMessage
                        {
                            Code = Constants.CONST_VALIDATION_FROM_DATE_GREATER_THAN_TO_DATE
                            ,
                            Message = Constants.CONST_VALIDATION_FROM_DATE_GREATER_THAN_TO_DATE_DESCRIPTION
                        });
                    }
                }

                // checking if any value is provided for sorting parameter then it should be ASC or DESC either capital or small
                // checking name order 
                if (!String.IsNullOrEmpty(sortByName))
                {
                    // if value defined is not ASC neither DESC
                    if (!sortByName.Equals(Constants.CONST_SORT_ASC, StringComparison.OrdinalIgnoreCase)
                        && !sortByName.Equals(Constants.CONST_SORT_DESC, StringComparison.OrdinalIgnoreCase))
                    {
                        lstCustomMessage = lstCustomMessage.IsNull() ? new List<CustomMessage>() : lstCustomMessage;
                        lstCustomMessage.Add(new CustomMessage
                        {
                            Code = Constants.CONST_VALIDATION_SORT_BY_NAME_NOT_DEFINED_PROPERLY
                            ,
                            Message = Constants.CONST_VALIDATION_SORT_BY_NAME_NOT_DEFINED_PROPERLY_DESCRIPTION
                        });
                    }
                }

                // checking if any value is provided for sorting parameter then it should be ASC or DESC either capital or small
                // checking price order 
                if (!String.IsNullOrEmpty(sortByPrice))
                {
                    // if value defined is not ASC neither DESC
                    if (!sortByPrice.Equals(Constants.CONST_SORT_ASC, StringComparison.OrdinalIgnoreCase)
                        && !sortByPrice.Equals(Constants.CONST_SORT_DESC, StringComparison.OrdinalIgnoreCase))
                    {
                        lstCustomMessage = lstCustomMessage.IsNull() ? new List<CustomMessage>() : lstCustomMessage;
                        lstCustomMessage.Add(new CustomMessage
                        {
                            Code = Constants.CONST_VALIDATION_SORT_BY_PRICE_NOT_DEFINED_PROPERLY
                            ,
                            Message = Constants.CONST_VALIDATION_SORT_BY_PRICE_NOT_DEFINED_PROPERLY_DESCRIPTION
                        });
                    }
                }

                if (!lstCustomMessage.IsNull())
                {
                    // validation failed and all validation messages will later be transported to servcie class
                    throw new CustomException() { Messages = lstCustomMessage };
                }
            }
            catch (CustomException ex)
            {
                // in case some exception appears which was not defined and method crashed
                // all nested methods are defined with exception which specifically will populate message based on error with in internal system
                // but in case any failure happens which was not handled or it is external issue then general message will be assigned with failure
                if (ex.Messages.IsNullOrEmpty())
                {
                    ex.Messages = new List<CustomMessage>();
                    ex.Messages.Add(new CustomMessage
                    {
                        Code = Constants.CONST_EXCEPTION_SEARCH_PARAMETERS_ERROR
                        ,
                        Message = Constants.CONST_EXCEPTION_SEARCH_PARAMETERS_ERROR_DESCRIPTION
                    });
                }

                throw ex;
            }
            catch (Exception ex)
            {
                CustomException cEx = new CustomException(new List<CustomMessage> { new CustomMessage { Code = Constants.CONST_EXCEPTION_SEARCH_PARAMETERS_ERROR, Message = Constants.CONST_EXCEPTION_SEARCH_PARAMETERS_ERROR_DESCRIPTION } }
                    , ex.Message
                    , ex.InnerException);

                throw cEx;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortByName"></param>
        /// <param name="sortByPrice"></param>
        /// <returns></returns>
        private CommonEnums.HotelSearchOrders DecideSearchOrder(String sortByName, String sortByPrice)
        {
            try
            {
                bool isSortByNameExist = !String.IsNullOrEmpty(sortByName);
                bool isSortByPriceExist = !String.IsNullOrEmpty(sortByPrice);


                if (isSortByNameExist && !isSortByPriceExist)
                {
                    if (sortByName.Equals(Constants.CONST_SORT_ASC, StringComparison.OrdinalIgnoreCase))
                    {
                        return CommonEnums.HotelSearchOrders.AscendingByName;
                    }
                    else if (sortByName.Equals(Constants.CONST_SORT_DESC, StringComparison.OrdinalIgnoreCase))
                    {
                        return CommonEnums.HotelSearchOrders.DescendingByName;
                    }
                }
                else if (isSortByNameExist && isSortByPriceExist)
                {
                    if (sortByName.Equals(Constants.CONST_SORT_ASC, StringComparison.OrdinalIgnoreCase) && sortByPrice.Equals(Constants.CONST_SORT_ASC, StringComparison.OrdinalIgnoreCase))
                    {
                        return CommonEnums.HotelSearchOrders.AscendingByNameAndAscendingByPrice;
                    }
                    else if (sortByName.Equals(Constants.CONST_SORT_ASC, StringComparison.OrdinalIgnoreCase) && sortByPrice.Equals(Constants.CONST_SORT_DESC, StringComparison.OrdinalIgnoreCase))
                    {
                        return CommonEnums.HotelSearchOrders.AscendingByNameAndDescendingByPrice;
                    }
                    else if (sortByName.Equals(Constants.CONST_SORT_DESC, StringComparison.OrdinalIgnoreCase) && sortByPrice.Equals(Constants.CONST_SORT_ASC, StringComparison.OrdinalIgnoreCase))
                    {
                        return CommonEnums.HotelSearchOrders.DescendingByNameAndAscendingByPrice;
                    }
                    else if (sortByName.Equals(Constants.CONST_SORT_DESC, StringComparison.OrdinalIgnoreCase) && sortByPrice.Equals(Constants.CONST_SORT_DESC, StringComparison.OrdinalIgnoreCase))
                    {
                        return CommonEnums.HotelSearchOrders.DescendingByNameAndDescendingByPrice;
                    }
                }
                else if (!isSortByNameExist && isSortByPriceExist)
                {
                    if (sortByPrice.Equals(Constants.CONST_SORT_ASC, StringComparison.OrdinalIgnoreCase))
                    {
                        return CommonEnums.HotelSearchOrders.AscendingByPrice;
                    }
                    else if (sortByPrice.Equals(Constants.CONST_SORT_DESC, StringComparison.OrdinalIgnoreCase))
                    {
                        return CommonEnums.HotelSearchOrders.DescendingByPrice;
                    }
                }
            }
            // must catch exception but it shouldnt stop whole functionality in live system
            catch (Exception)
            {
                // logging can be introduced for this purpose but very rare since data is already passing into this method after extensive validation
            }

            // in case of exception or no case matching then defaut value
            return CommonEnums.HotelSearchOrders.None;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryableHotels"></param>
        /// <param name="hotelSearchOrders"></param>
        /// <returns></returns>
        private IQueryable<Hotel> ApplySort(IQueryable<Hotel> queryableHotels, CommonEnums.HotelSearchOrders hotelSearchOrders) 
        {
            try
            {
                // apply sorting cases
                switch (hotelSearchOrders)
                {
                    // ascending by name
                    case CommonEnums.HotelSearchOrders.AscendingByName:
                        {
                            queryableHotels = queryableHotels.OrderBy(h2 => h2.Name);
                            break;
                        }

                    // ascending by name and ascending by price
                    case CommonEnums.HotelSearchOrders.AscendingByNameAndAscendingByPrice:
                        {
                            queryableHotels = queryableHotels.OrderBy(h2 => h2.Name).ThenBy(h3 => h3.Price);
                            break;
                        }

                    // ascending by name and descending by price
                    case CommonEnums.HotelSearchOrders.AscendingByNameAndDescendingByPrice:
                        {
                            queryableHotels = queryableHotels.OrderBy(h2 => h2.Name).ThenByDescending(h3 => h3.Price);
                            break;
                        }

                    // ascending by price
                    case CommonEnums.HotelSearchOrders.AscendingByPrice:
                        {
                            queryableHotels = queryableHotels.OrderBy(h2 => h2.Price);
                            break;
                        }

                    // descending by name and ascneding by price
                    case CommonEnums.HotelSearchOrders.DescendingByNameAndAscendingByPrice:
                        {
                            queryableHotels = queryableHotels.OrderByDescending(h2 => h2.Name).ThenBy(h3 => h3.Price);
                            break;
                        }

                    // descending by price
                    case CommonEnums.HotelSearchOrders.DescendingByPrice:
                        {
                            queryableHotels = queryableHotels.OrderByDescending(h2 => h2.Price);
                            break;
                        }

                    // descending by name 
                    case CommonEnums.HotelSearchOrders.DescendingByName:
                        {
                            queryableHotels = queryableHotels.OrderByDescending(h2 => h2.Name);
                            break;
                        }

                    // descending by name and descending by price
                    case CommonEnums.HotelSearchOrders.DescendingByNameAndDescendingByPrice:
                        {
                            queryableHotels = queryableHotels.OrderByDescending(h2 => h2.Name).ThenByDescending(h3 => h3.Price);
                            break;
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
                ex.Messages.Add(new CustomMessage { Code = Constants.CONST_EXCEPTION_SORT_NOT_DEFINED, Message = Constants.CONST_EXCEPTION_SORT_NOT_DEFINED_DESCRIPTION });

                throw ex;
            }
            catch (Exception ex)
            {
                CustomException cEx = new CustomException(new List<CustomMessage> { new CustomMessage { Code = Constants.CONST_EXCEPTION_SORT_NOT_DEFINED, Message = Constants.CONST_EXCEPTION_SORT_NOT_DEFINED_DESCRIPTION } }
                    , ex.Message
                    , ex.InnerException);

                throw cEx;
            }

            return queryableHotels;
        }        

        #endregion
           
    }
}
