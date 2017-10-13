using System;
using System.Collections.Generic;

using WcfHotelService.BAL.Business;
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
        /// <summary>
        /// This method wil return JSON and in case further operations are desired on result set it can be perfomrmed in this method before returning result
        /// </summary>
        /// <param name="hotelName"></param>
        /// <param name="destination"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>        
        public ResponseHotel SearchResultJson(RequestHotel requestHotel) 
        {
            // in case in future further operations need to be performed on JSON data
            // Result should be sent with proper details in case of failures either error or validations
            ResponseHotel responseSearch = new ResponseHotel();

            try
            {
                // in case in further operations need to be performed on XML data
                responseSearch.Hotels = Search(requestHotel);

                // if all operations are perfomred well return success code
                responseSearch.Code = Constants.CONST_RESULT_CODE_SUCCESS;
            }
            catch (CustomException ex)
            {
                responseSearch.Messages = ex.Messages;

                // General failure code to inform client that request went unsuccessful
                responseSearch.Code = Constants.CONST_RESULT_CODE_FAILURE;
            }
            catch
            {
                responseSearch.Messages = new List<CustomMessage> { new CustomMessage { Code = Constants.CONST_EXCEPTION_INTERNAL_ERROR, Message = Constants.CONST_EXCEPTION_INTERNAL_ERROR_DESCRIPTION } };

                // General failure code to inform client that request went unsuccessful
                responseSearch.Code = Constants.CONST_RESULT_CODE_FAILURE;
            }

            return responseSearch;
        }

        /// <summary>
        /// This method wil return JSON and in case further operations are desired on result set it can be perfomrmed in this method before returning result
        /// </summary>
        /// <param name="hotelName"></param>
        /// <param name="destination"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>        
        public ResponseHotel SearchResultXML(RequestHotel requestHotel) 
        {
            // in case in future further operations need to be performed on XML data
            // Result should be sent with proper details in case of failures either error or validations
            ResponseHotel responseSearch = new ResponseHotel();

            try
            {
                // in case in future further operations need to be performed on XML data
                responseSearch.Hotels = Search(requestHotel);

                // if all operations are perfomred well return success code
                responseSearch.Code = Constants.CONST_RESULT_CODE_SUCCESS;
            }
            catch (CustomException ex)
            {
                responseSearch.Messages = ex.Messages;

                // General failure code to inform client that request went unsuccessful
                responseSearch.Code = Constants.CONST_RESULT_CODE_FAILURE;
            }
            catch
            {
                responseSearch.Messages = new List<CustomMessage> { new CustomMessage { Code = Constants.CONST_EXCEPTION_INTERNAL_ERROR, Message = Constants.CONST_EXCEPTION_INTERNAL_ERROR_DESCRIPTION } };

                // General failure code to inform client that request went unsuccessful
                responseSearch.Code = Constants.CONST_RESULT_CODE_FAILURE;
            }

            return responseSearch;
        }

        /// <summary>
        /// testing method that echo is working fine in wcf
        /// </summary>
        /// <param name="echoString"></param>
        /// <returns></returns>
        public string Echo(string echoString)
        {
            return echoString;
        }

        /// <summary>
        /// same logic as post but to serve as a purpose for rest call. 
        /// provide easines to call APi by any means
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
        public ResponseHotel SearchResultJson_Get(String HotelName, String Destination, String RangeFrom, String RangeTo, String DateFrom, String DateTo, String SortByName, String SortByPrice)
        {
            // in case in future further operations need to be performed on XML data
            // Result should be sent with proper details in case of failures either error or validations
            ResponseHotel responseSearch = new ResponseHotel();
            RequestHotel requestHotel = new RequestHotel();

            try
            {
                // in case in future further operations need to be performed on XML data
                responseSearch.Hotels = Search(requestHotel);

                // if all operations are perfomred well return success code
                responseSearch.Code = Constants.CONST_RESULT_CODE_SUCCESS;
            }
            catch (CustomException ex)
            {
                responseSearch.Messages = ex.Messages;

                // General failure code to inform client that request went unsuccessful
                responseSearch.Code = Constants.CONST_RESULT_CODE_FAILURE;
            }
            catch
            {
                responseSearch.Messages = new List<CustomMessage> { new CustomMessage { Code = Constants.CONST_EXCEPTION_INTERNAL_ERROR, Message = Constants.CONST_EXCEPTION_INTERNAL_ERROR_DESCRIPTION } };

                // General failure code to inform client that request went unsuccessful
                responseSearch.Code = Constants.CONST_RESULT_CODE_FAILURE;
            }

            return responseSearch;
        }        

        #region Private Method

        /// <summary>
        /// unified method to perform all search opearations for hotels without any difference of calling method
        /// </summary>
        /// <param name="requestHotel"></param>
        /// <returns></returns>
        private List<Hotel> Search(RequestHotel requestHotel) 
        {
            HotelBAL hotelBAL = new HotelBAL();

            try
            {
                // calling business layer methos to perform all operations and just return result for hotels
                return hotelBAL.SearchHotels(requestHotel);

                // in case it is required to remove irrelavant date entries from nested availability collection then this method will do the job
                // simply pass list of hotels into this methos with date ranges
                //hotelBAL().RemoveIrrelevantDataFromAvailability

            }
            catch (CustomException ex)
            {
                // in case some exception appears which was not defined and method crashed
                // all nested methods are defined with exception which specifically will populate message based on error with in internal system
                // but in case any failure happens which was not handled or it is external issue then general message will be assigned with failure
                if (ex.Messages.IsNullOrEmpty())
                {
                    ex.Messages = new List<CustomMessage>();
                    ex.Messages.Add(new CustomMessage { Code = Constants.CONST_EXCEPTION_INTERNAL_ERROR, Message = Constants.CONST_EXCEPTION_INTERNAL_ERROR_DESCRIPTION });
                }

                throw ex;
            }
            catch (Exception ex)
            {
                CustomException cEx = new CustomException(new List<CustomMessage> { new CustomMessage { Code = Constants.CONST_EXCEPTION_INTERNAL_ERROR, Message = Constants.CONST_EXCEPTION_INTERNAL_ERROR_DESCRIPTION } }
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
        /// <returns></returns>
        private Boolean AreParameterTypesValidForSearch(String rangeFrom, String rangeTo, String dateFrom, String dateTo) 
        {
            List<CustomMessage> lstCustomMessage = null;

            try
            {
                // checking price_from  is valid double number or not
                if (!String.IsNullOrEmpty(rangeFrom))
                {
                    Double rangeStart;
                    if (!Double.TryParse(rangeFrom, out rangeStart))
                    {
                        lstCustomMessage = new List<CustomMessage>();
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
                    Double rangeEnd;
                    if (!Double.TryParse(rangeFrom, out rangeEnd))
                    {
                        lstCustomMessage = new List<CustomMessage>();
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
                    DateTime dateStart;
                    if (!DateTime.TryParse(dateFrom, out dateStart))
                    {
                        lstCustomMessage = new List<CustomMessage>();
                        lstCustomMessage.Add(new CustomMessage
                        {
                            Code = Constants.CONST_VALIDATION_PRICE_TO_NOT_NUMBER
                         ,
                            Message = Constants.CONST_VALIDATION_PRICE_TO_NOT_NUMBER_DESCRIPTION
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

        #endregion

    }
}
