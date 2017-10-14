using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfHotelService.UnitTest
{
    public static class Constants
    {
        #region Hotel Service Test Case

        public const Int32 CONST_SUCCESS_CODE = 1;
        public const Int32 CONST_HOTEL_COUNT = 6;

        public const String CONST_CHECK_RESPONSE_SUCCESS = "Check that request processed successfully and no error appeared";
        public const String CONST_CHECK_RESPONSE_SUCCESS_RESULT = "Request Failure. Request is not processed in success mode.";

        public const String CONST_CHECK_RESPONSE_NOT_NULL = "Check whether the response is null";
        public const String CONST_CHECK_RESPONSE_NOT_NULL_RESULT = "The response is null.";


        public const String CONST_CHECK_ALL_HOTELS = "Check we get all hotels list ";
        public const String CONST_CHECK_ALL_HOTELS_RESULT = "A test to check we get all hotels.";

        public const String CONST_CHECK_SPECIFIC_HOTEL_FILTERING = "Check we get specific hotel by providing parameters as per choice of hotel to match";
        public const String CONST_CHECK_SPECIFIC_HOTEL_FILTERING_RESULT = "Hotel not found as per defined parameters.";

        public const String CONST_CHECK_HOTEL_FILTERING_VALIDATION = "Check parameters format for range and date";
        public const String CONST_CHECK_HOTEL_FILTERING_VALIDATION_RESULT = "Parameters format not provided correctly are not validated properly.";

        public const String CONST_CHECK_MULTIPLE_HOTEL_FILTERING = "Check multiple result for hotels from server";
        public const String CONST_CHECK_MULTIPLE_HOTEL_FILTERING_RESULT = "Multiple hotels filtering not working properly.";

        public const String CONST_CHECK_SORT_PARAMETER = "Check wrong value for sort is cathced or not";
        public const String CONST_CHECK_SORT_PARAMETER_RESULT = "Wrong sort parameters.";

        #endregion
    }
}



