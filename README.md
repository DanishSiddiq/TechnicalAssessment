# TechnicalAssessment: 


##################

Status: Completed

##################

WCF application developed to meet all requirements. Get and Post calls created to facilitate in easy manner as per choice of use. 

Both Get and Post calls have options to respond as JSON or XML. Separate API's created for this purpose based on client beahvior.



Based address: http://localhost:50311/Service/HotelSearchService.svc/result/


All API's are listed below:




##################
Get API's:

##################
For JSON based Response
:
http://localhost:50311/Service/HotelSearchService.svc/result/get_json?name=&destination=&price_from=&price_to=&date_from=&date_to=&name_order=&price_order=



For XML based Response:

http://localhost:50311/Service/HotelSearchService.svc/result/get_xml?name=&destination=&price_from=&price_to=&date_from=&date_to=&name_order=&price_order=




All parameters in get call are string based. They are converted by WCF service itself. 

Dates from API(https://api.myjson.com/bins/tl0bp) are in dd-mm-yyyy so kept in same format in get calls. Server will transform itself. 



##################




Post API's and Data:

##################

For JSON based Response:

http://localhost:50311/Service/HotelSearchService.svc/result/search_json




For XML based Response:

http://localhost:50311/Service/HotelSearchService.svc/result/search_xml





Data for Post:

==============
{

 "HotelName": "Anything" or null,

 "Destination": "Anything" or null,

 "RangeFrom":"decimal number/number" or null,

 "RangeTo":decimal number or number or null,

 "DateFrom":"Valid date" or null,

 "DateTo":"Valid date or null",

 "SortByName":"ASC"  or null,

 "SortByPrice":"DESC"  or null

}




Sample Raw Data:
============
===
{

 "HotelName": "hotel",

 "Destination": "",

 "RangeFrom":"80",

 "RangeTo":100,

 "DateFrom":null,

 "DateTo":null,

 "SortByName":"ASC",

 "SortByPrice":null

}




####################################

Debugging, Testing and Deployment:
####################################
1. Base address is mentioned above and definitely defined in web.config to work smoothly with out any hastle


2. Application can be deployed in IIS in virtual directory with Port mentioned as 50311 in web.conifg which is changeable in web.config base address property


3. Application can also run by opening solution in Visual Studio 2012 or 2015. By debugging IHotelSearchService, application will start debugging 
since metadata publishing is disabled. 


4. API'can directly be called from browser for get calls to check functionality


5. PostMan in google chrome can be used for testing post calls or any other plugin of choice for making post calls for data as mentioned in previous section








####################################

Assumptions & Inmplementation In Development:
####################################
1. All parameters are considered as optional based on user discretion to provide value or not in get & post calls



2. API will work in both cases either parameters are provided or not. Get call is flexible enough with empty parameters or null



3. Price range is made flexible enough to work together with start and end range. It also works independently of start and end which means:
	
	a. If starting price and ending price is provided then records will be filtered where price come in between or equal to provided range.
    
	b. If only starting value is provided but ending value is not provided then all hotels with price greater than or equal to start value will be listed
	
	c. If only ending value is provided but starting value is not provided then all hotels with price lesser than or equal to end value will be listed
	


4. Date range is made flexible enough to work together with start and end range. It also works independently of from and end which means:

	a. If starting date and ending date is provided then records will be filtered where dates come in between or equal to provided range.

    	b. If only starting date is provided but ending date is not provided then all hotels with availability dates greater than or equal to starting date will be listed

	c. If only ending date is provided but starting date is not provided then all hotels with availability dates lesser than or equal to ending date will be listed



5. Validations for price range and date range is applied so that they must not be provided in wrong manner. Multiple validations applied to check provided
input for price is valid number or not and from must be lesser or equal to ending range. Date format for request is checked along with valid date checks.



6. Sorting parameters are flexible to work with ASC/asc and DESC/desc. Any other garbage value provided in these fields will inform client through 
proper message that order fields are not defined correctly.


7. First Sort parameter is for hotel name: ASC/asc/DESC/desc and second one is for price: ASC/asc/DESC/desc . All possible  combinations of ordering are perfomred in code



8. All operations are perfomred  means of LINQ 




##################

UNIT TEST PROJECT:

##################
1. NUnit is used for Unit Testing and RhinoFramwork is used for mocks. Both are added into test project from Nuget Manager. Information for packages also
 included in  project package file 


2. I tried to covered all test cases including validations, filtering, sorting and combination of them.


3. Test cases are also added in TestMatrix.xlsx file. It contains most test cases related to filtering, sorting and combination of them. 
Validations error messages are not written in matrix file but test methods are covering most of them in unit test project.


4. Resharper tool can be used in conjuction to perform testing more conveniently for easiness



	




##################

Deleting Data:

##################
1. I have deleted irrelevant availability data from json. Keeping from and to date in availability array which are relevant to search paarmeters. 
In case it is not desired then kinldy comment code in HotelBAL with name "RemoveIrrelevantDataFromAvailability" which will start returning all availability data





#########################

Things not able to cover:

##################
#######
1. Travis and coverall not able to integrate as of limitation.

