using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfHotelService.UnitTest
{
    internal static class Constants
    {
        #region Hotel Service Test Case

        internal const Int32 CONST_SUCCESS_CODE = 1;
        internal const Int32 CONST_HOTEL_COUNT = 6;

        internal const String CONST_CHECK_RESPONSE_SUCCESS = "Check that request processed successfully and no error appeared";
        internal const String CONST_CHECK_RESPONSE_SUCCESS_RESULT = "Request Failure. Request is not processed in success mode.";

        internal const String CONST_CHECK_RESPONSE_NOT_NULL = "Check whether the response is null";
        internal const String CONST_CHECK_RESPONSE_NOT_NULL_RESULT = "The response is null.";

        internal const String CONST_CHECK_SPECIFIC_HOTEL_FILTERING = "Check we get specific hotel by providing parameters as per choice of hotel to match";
        internal const String CONST_CHECK_SPECIFIC_HOTEL_FILTERING_RESULT = "Hotel not found as per defined parameters.";

        internal const String CONST_CHECK_HOTEL_FILTERING_VALIDATION = "Check parameters format for range and date";
        internal const String CONST_CHECK_HOTEL_FILTERING_VALIDATION_RESULT = "Parameters format not provided correctly are not validated properly.";

        internal const String CONST_CHECK_MULTIPLE_HOTEL_FILTERING = "Check multiple result for hotels from server";
        internal const String CONST_CHECK_MULTIPLE_HOTEL_FILTERING_RESULT = "Multiple hotels filtering not working properly.";

        internal const String CONST_CHECK_SORT_PARAMETER = "Check wrong value for sort is cathced or not";
        internal const String CONST_CHECK_SORT_PARAMETER_RESULT = "Wrong sort parameters.";

        internal const String CONST_CHECK_ALL_HOTELS = "Check we get all hotels list";
        internal const String CONST_CHECK_ALL_HOTELS_RESULT = "Check we get all hotels list failed.";

        internal const String CONST_CHECK_ALL_HOTELS_ORDER_BY_PRICE_ASC = "Check we get all hotels in ascedning order by price";
        internal const String CONST_CHECK_ALL_HOTELS_ORDER_BY_PRICE_ASC_RESULT = "Check we get all hotels in ascedning order by price failed.";

        internal const String CONST_CHECK_ALL_HOTELS_ORDER_BY_NAME_DESC = "Check we get all hotels in descending order by name";
        internal const String CONST_CHECK_ALL_HOTELS_ORDER_BY_NAME_DESC_RESULT = "Check we get all hotels in descending order by name failed.";

        internal const String CONST_CHECK_HOTEL_BY_DESINATION = "Check hotels by detination";
        internal const String CONST_CHECK_HOTEL_BY_DESINATION_RESULT = "Check hotels by detination failed.";

        internal const String CONST_CHECK_HOTEL_BY_PRICE_RANGE = "Check hotels by price range ";
        internal const String CONST_CHECK_HOTEL_BY_PRICE_RANGE_RESULT = "Check hotels by price range failed.";

        internal const String CONST_CHECK_HOTEL_BY_PRICE_RANGE_ORDER_BY_PRICE_DESC = "Check hotels by price range and order by price in descending order";
        internal const String CONST_CHECK_HOTEL_BY_PRICE_RANGE_ORDER_BY_PRICE_DESC_RESULT = "Check hotels by price range and order by price in descending order failed.";

        internal const String CONST_CHECK_HOTEL_PRICE_FROM_ORDER_BY_NAME_ASC = "Check hotels which starts from 90 and order them by name in ascending order";
        internal const String CONST_CHECK_HOTEL_PRICE_FROM_ORDER_BY_NAME_ASC_RESULT = "Check hotels which starts from 90 and order them by name in ascending order failed.";
        
        internal const String CONST_CHECK_HOTEL_BY_DATE_RANGE_ORDER_BY_NAME_ASC_ORDER_BY_PRICE_DESC = "Check hotel with in date range and order by name in ascending order and price in descending order";
        internal const String CONST_CHECK_HOTEL_BY_DATE_RANGE_ORDER_BY_NAME_ASC_ORDER_BY_PRICE_DESC_RESULT = "Check hotel with in date range and order by name in ascending order and price in descending order failed.";

        internal const String CONST_CHECK_HOTEL_BY_TO_DATE_ORDER_BY_NAME_PRICE_DESC = "Check hotels only by to date and order by price in descending";
        internal const String CONST_CHECK_HOTEL_BY_TO_DATE_ORDER_BY_NAME_PRICE_DESC_RESULT = "Check hotels only by to date and order by price in descending failed.";

       
        #endregion


        #region Hotel Names

        internal const String CONST_HOTEL_CONCORDE = "Concorde Hotel";
        internal const String CONST_HOTEL_GOLDEN_TULIP = "Golden Tulip";
        internal const String CONST_HOTEL_LE_MERIDIEN = "Le Meridien";
        internal const String CONST_HOTEL_MEDIA_ONE = "Media One Hotel";
        internal const String CONST_HOTEL_NOVOTEL = "Novotel Hotel";
        internal const String CONST_HOTEL_ROTANA = "Rotana Hotel";

        #endregion
    }
}



