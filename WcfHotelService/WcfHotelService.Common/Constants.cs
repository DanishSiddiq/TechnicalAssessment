using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfHotelService.Common
{
    public static class Constants
    {
        #region URL to fetch data

        public const String CONST_HOTEL_API = "http://api.myjson.com/bins/tl0bp";

        #endregion

        #region Empty string to avoid reinitialization

        public const String CONST_EMPTY_STRING = "";

        #endregion

        // range be default value if not provided
        public const Double CONST_PRICE_RANGE_BY_DEFAULT_VALUE = 0;

        #region Hotel json parsin properties

        public const String CONST_HOTEL_HOTELS = "hotels";
        public const String CONST_HOTEL_NAME = "name";
        public const String CONST_HOTEL_PRICE = "price";
        public const String CONST_HOTEL_CITY = "city";
        public const String CONST_HOTEL_AVAILABILITY = "availability";
        public const String CONST_HOTEL_AVAILABILITY_FROM = "from";
        public const String CONST_HOTEL_AVAILABILITY_TO = "to";

        #endregion

        #region Sorting orders defined

        public const String CONST_SORT_ASC = "asc";
        public const String CONST_SORT_DESC = "desc";

        #endregion

        #region Exception/Result/Validation Codes

        // overall operations errors
        public const Int32 CONST_RESULT_CODE_FAILURE = 0;
        public const Int32 CONST_RESULT_CODE_SUCCESS = 1;


        // parser errors
        public const String CONST_EXCEPTION_JSON_NOT_PARSEABLE = "Error_1001";

        // validation errors
        public const String CONST_EXCEPTION_SEARCHABLE_PARAMETER_NOT_VALID = "Error_1002";
        public const String CONST_VALIDATION_FROM_PRICE_GREATER_THAN_TO_PRICE = "Error_1003";
        public const String CONST_VALIDATION_FROM_DATE_GREATER_THAN_TO_DATE = "Error_1004";

        // query errors
        public const String CONST_EXCEPTION_QUERY_ERROR = "Error_1005";
        public const String CONST_EXCEPTION_SORT_NOT_DEFINED = "Error_1006";
        public const String CONST_EXCEPTION_QUERY_ERROR_REMOVING_IRRELVANT_ENTRIES = "Error_1007";

        // Hotel API returns empty data
        public const String CONST_API_DO_NOT_CONTAIN_OFFERS = "Error_1008";

        // Hotel API returns empty data
        public const String CONST_API_NOT_ACCESSIBLE = "Error_1009";

        #endregion

    }
}
