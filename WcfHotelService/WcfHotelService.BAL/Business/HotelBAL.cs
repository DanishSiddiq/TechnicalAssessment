using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WcfHotelService.Common;
using WcfHotelService.Common.Extensions;
using WcfHotelService.Common.Exceptions;
using WcfHotelService.Common.Enums;
using WcfHotelService.Common.Entities;

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
        public List<Hotel> Search(  IQueryable<Hotel> queryableHotels
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
                // first validate data for search parameters
                if (IsDataValidForSearch(rangeFrom, rangeTo, dateFrom, dateTo))
                {

                    // if nullable values are not provided then it will pass in first OR clause
                    // else it will match data in second clause 
                    queryableHotels = queryableHotels.Where(h => (String.IsNullOrEmpty(hotelName) || h.Name.ToLower().Contains(hotelName))
                                            && (String.IsNullOrEmpty(destination) || h.City.ToLower().Contains(destination))
                                            && (rangeFrom == null || h.Price >= rangeFrom)
                                            && (rangeTo == null || h.Price <= rangeTo)
                                            && (h.Availability.Any(a => (dateFrom == null || a.fromDate <= dateFrom) && (dateTo == null || a.toDate >= dateTo))));

                    
                    
                    // multiple combinations of ordering are possible
                    CommonEnums.HotelSearchOrders hotelSearchOrders = DecideSearchOrder(sortByName, sortByPrice);
                    queryableHotels = ApplySort(queryableHotels, hotelSearchOrders);
                }   
            }
            catch (CustomException ex)
            {
                // only add message in this method if it crashed in this method
                if (ex.LstCustomMessage.IsNull())
                {
                    ex.LstCustomMessage = new List<CustomMessage>();
                    ex.LstCustomMessage.Add(new CustomMessage { Code = Constants.CONST_EXCEPTION_QUERY_ERROR, Message = Constants.CONST_EXCEPTION_QUERY_ERROR_DESCRIPTION });
                }

                throw ex;
            }

            return queryableHotels.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstHotel"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        public void RemoveIrrelevantDataFromAvailability(List<Hotel> lstHotel, DateTime? dateFrom, DateTime? dateTo)
        {
            try
            {
                // since now we have limited data then we can further refine it
                lstHotel.ForEach(h => h.Availability.RemoveAll(a => (dateFrom != null && DateTime.Compare(a.fromDate, dateFrom.Value) > 0)
                                                                    || (dateTo != null && DateTime.Compare(a.toDate, dateTo.Value) < 0)));               
            }
            catch (CustomException ex)
            {
                // only add message in this method if it crashed in this method
                if (ex.LstCustomMessage.IsNull())
                {
                    ex.LstCustomMessage = new List<CustomMessage>();
                    ex.LstCustomMessage.Add(new CustomMessage { Code = Constants.CONST_EXCEPTION_QUERY_ERROR_REMOVING_IRRELVANT_ENTRIES
                    ,
                                                                Message = Constants.CONST_EXCEPTION_QUERY_ERROR_REMOVING_IRRELVANT_ENTRIES_DESCRIPTION
                    });
                }

                throw ex;
            }
        }

        #endregion

        #region Private Methods

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
                if (ex.LstCustomMessage.IsNull())
                {
                    ex.LstCustomMessage = new List<CustomMessage>();
                }
                ex.LstCustomMessage.Add(new CustomMessage { Code = Constants.CONST_EXCEPTION_SORT_NOT_DEFINED, Message = Constants.CONST_EXCEPTION_SORT_NOT_DEFINED_DESCRIPTION });


                throw ex;
            }

            return queryableHotels;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public Boolean IsDataValidForSearch(Double? rangeFrom, Double? rangeTo, DateTime? dateFrom, DateTime? dateTo)
        {
            List<CustomMessage> lstCustomMessage = null;
            if (rangeFrom.HasValue && rangeTo.HasValue)
            {
                // from value is greater than to value
                if (rangeFrom.Value > rangeTo.Value)
                {
                    lstCustomMessage = new List<CustomMessage>();
                    lstCustomMessage.Add(new CustomMessage { Code = Constants.CONST_VALIDATION_FROM_PRICE_GREATER_THAN_TO_PRICE
                                                            , 
                                                            Message = Constants.CONST_VALIDATION_FROM_PRICE_GREATER_THAN_TO_PRICE_DESCRIPTION
                    });
                }
            }

            if (dateFrom.HasValue && dateTo.HasValue)
            {
                // from value is greater than to value
                if (dateFrom.Value > dateTo.Value)
                {
                    if (lstCustomMessage.IsNull())
                    {
                        lstCustomMessage = new List<CustomMessage>();
                    }

                    lstCustomMessage.Add(new CustomMessage { Code = Constants.CONST_VALIDATION_FROM_DATE_GREATER_THAN_TO_DATE
                    ,
                                                             Message = Constants.CONST_VALIDATION_FROM_DATE_GREATER_THAN_TO_DATE_DESCRIPTION
                    });
                }
            }

            if (!lstCustomMessage.IsNull())
            {
                // validation failed and all validation messages will later be transported to servcie class
                throw new CustomException() { LstCustomMessage = lstCustomMessage }; 
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
                    if (sortByName.Equals(Constants.CONST_SORT_ASC))
                    {
                        return CommonEnums.HotelSearchOrders.AscendingByName;
                    }
                    else if (sortByName.Equals(Constants.CONST_SORT_DESC))
                    {
                        return CommonEnums.HotelSearchOrders.DescendingByName;
                    }
                }
                else if (isSortByNameExist && isSortByPriceExist)
                {
                    if (sortByName.Equals(Constants.CONST_SORT_ASC) && sortByPrice.Equals(Constants.CONST_SORT_ASC))
                    {
                        return CommonEnums.HotelSearchOrders.AscendingByNameAndAscendingByPrice;
                    }
                    else if (sortByName.Equals(Constants.CONST_SORT_ASC) && sortByPrice.Equals(Constants.CONST_SORT_DESC))
                    {
                        return CommonEnums.HotelSearchOrders.AscendingByNameAndDescendingByPrice;
                    }
                    else if (sortByName.Equals(Constants.CONST_SORT_DESC) && sortByPrice.Equals(Constants.CONST_SORT_ASC))
                    {
                        return CommonEnums.HotelSearchOrders.DescendingByNameAndAscendingByPrice;
                    }
                    else if (sortByName.Equals(Constants.CONST_SORT_DESC) && sortByPrice.Equals(Constants.CONST_SORT_DESC))
                    {
                        return CommonEnums.HotelSearchOrders.DescendingByNameAndDescendingByPrice;
                    }
                }
                else if (!isSortByNameExist && isSortByPriceExist)
                {
                    if (sortByPrice.Equals(Constants.CONST_SORT_ASC))
                    {
                        return CommonEnums.HotelSearchOrders.AscendingByPrice;
                    }
                    else if (sortByPrice.Equals(Constants.CONST_SORT_DESC))
                    {
                        return CommonEnums.HotelSearchOrders.DescendingByPrice;
                    }
                }
            }
             // must catch exception but it shouldnt stop whole functionality in live system
            catch 
            {
                // logging can be introduced for this purpose
            }
           
            // in case of exception or no case matching then defaut value
            return CommonEnums.HotelSearchOrders.None;
        }

        #endregion
           
    }
}
