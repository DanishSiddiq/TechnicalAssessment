using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfHotelService.Common
{
    public static class Constants
    {
        #region URL To Fetch Data

        public const String CONST_HOTEL_API = "http://api.myjson.com/bins/tl0bp";

        #endregion

        #region Empty String To Avoid Reinitialization

        public const String CONST_EMPTY_STRING = "";

        #endregion

        #region Hotel JSON Properties

        public const String CONST_HOTEL_HOTELS = "hotels";
        public const String CONST_HOTEL_NAME = "name";
        public const String CONST_HOTEL_PRICE = "price";
        public const String CONST_HOTEL_CITY = "city";
        public const String CONST_HOTEL_AVAILABILITY = "availability";
        public const String CONST_HOTEL_AVAILABILITY_FROM = "from";
        public const String CONST_HOTEL_AVAILABILITY_TO = "to";

        #endregion

        #region Sorting Orders Definition

        public const String CONST_SORT_ASC = "asc";
        public const String CONST_SORT_DESC = "desc";

        #endregion

        #region Validation Errors Codes and Messages

        // validation errors for hotel
        public const String CONST_EXCEPTION_SEARCHABLE_PARAMETER_NOT_VALID = "Error_2001";
        public const String CONST_EXCEPTION_SEARCHABLE_PARAMETER_NOT_VALID_DESCRIPTION = "Searchable parameters are not properly defined.";
        public const String CONST_VALIDATION_FROM_PRICE_GREATER_THAN_TO_PRICE = "Error_2002";
        public const String CONST_VALIDATION_FROM_PRICE_GREATER_THAN_TO_PRICE_DESCRIPTION = "From price is greater than To price.";
        public const String CONST_VALIDATION_FROM_DATE_GREATER_THAN_TO_DATE = "Error_2003";
        public const String CONST_VALIDATION_FROM_DATE_GREATER_THAN_TO_DATE_DESCRIPTION = "From date is grater than To date.";
        public const String CONST_VALIDATION_SORT_BY_NAME_NOT_DEFINED_PROPERLY = "Error_2004";
        public const String CONST_VALIDATION_SORT_BY_NAME_NOT_DEFINED_PROPERLY_DESCRIPTION = "Order by name parameter is not defined properly. Value can only ASC/asc or DESC/desc.";
        public const String CONST_VALIDATION_SORT_BY_PRICE_NOT_DEFINED_PROPERLY = "Error_2005";
        public const String CONST_VALIDATION_SORT_BY_PRICE_NOT_DEFINED_PROPERLY_DESCRIPTION = "Order by price parameter is not defined properly. Value can only ASC/asc or DESC/desc.";

        // validation for GET request that parameter value must be defined properly
        public const String CONST_VALIDATION_PRICE_FROM_NOT_NUMBER = "Error_2006";
        public const String CONST_VALIDATION_PRICE_FROM_NOT_NUMBER_DESCRIPTION = "Price From is not defined correctly. Kindly provide valid number.";
        public const String CONST_VALIDATION_PRICE_TO_NOT_NUMBER = "Error_2007";
        public const String CONST_VALIDATION_PRICE_TO_NOT_NUMBER_DESCRIPTION = "Price To is not defined correctly. Kindly provide valid number.";
        public const String CONST_VALIDATION_PRICE_TO_NOT_NUMBER = "Error_2008";
        public const String CONST_VALIDATION_PRICE_TO_NOT_NUMBER_DESCRIPTION = "Price To is not defined correctly. Kindly provide valid number.";
        public const String CONST_VALIDATION_PRICE_TO_NOT_NUMBER = "Error_2009";
        public const String CONST_VALIDATION_PRICE_TO_NOT_NUMBER_DESCRIPTION = "Price To is not defined correctly. Kindly provide valid number.";

        #endregion

        #region Exception Codes And Messages

        // overall operations errors
        public const Int32 CONST_RESULT_CODE_FAILURE = 0;
        public const Int32 CONST_RESULT_CODE_SUCCESS = 1;

        // parser errors
        public const String CONST_EXCEPTION_JSON_NOT_PARSEABLE = "Error_1001";
        public const String CONST_EXCEPTION_JSON_NOT_PARSEABLE_DESCRIPTION = "JSON is not parseable";

        // query errors
        public const String CONST_EXCEPTION_QUERY_ERROR = "Error_1005";
        public const String CONST_EXCEPTION_QUERY_ERROR_DESCRIPTION = "Some problem happened in query.";
        public const String CONST_EXCEPTION_SORT_NOT_DEFINED = "Error_1006";
        public const String CONST_EXCEPTION_SORT_NOT_DEFINED_DESCRIPTION = "Ordering is not defined properly.";
        public const String CONST_EXCEPTION_QUERY_ERROR_REMOVING_IRRELVANT_ENTRIES = "Error_1007";
        public const String CONST_EXCEPTION_QUERY_ERROR_REMOVING_IRRELVANT_ENTRIES_DESCRIPTION = "Query countered issue while removing irrelevant dates availability.";
        
        // Hotel API returns empty data
        public const String CONST_API_DO_NOT_CONTAIN_OFFERS = "Error_1008";
        public const String CONST_API_DO_NOT_CONTAIN_OFFERS_DESCRIPTION = "Source does not contain any offer.";

        // Hotel API returns empty data
        public const String CONST_API_NOT_ACCESSIBLE = "Error_1009";
        public const String CONST_API_NOT_ACCESSIBLE_DESCRIPTION = "API is not accessible.";

        // General Internal Error
        public const String CONST_EXCEPTION_INTERNAL_ERROR = "Error_1010";
        public const String CONST_EXCEPTION_INTERNAL_ERROR_DESCRIPTION = "Internal error happens on server while processing request.";

        // Search Parameters are not defined properly
        public const String CONST_EXCEPTION_SEARCH_PARAMETERS_ERROR = "Error_1010";
        public const String CONST_EXCEPTION_SEARCH_PARAMETERS_ERROR_DESCRIPTION = "Search paramaters are not defined properly.";

        #endregion

    }
}
