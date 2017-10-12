# TechnicalAssessment


Testing and Deployment Techniques
======================================================



Assumptions & Inmplementation In Development:
======================================================
1. All parameters are considered as optional based on user discretion to provide valu or not in post call or directly calling from web reference

2. In case no parameter provided then complete data will be returned since client does not want to filter any record

3. Price range work independently of start and end which means:
	a. If starting price and ending price is provided then records will be filtered where price come in between or equal to provided range.
    b. If only starting value is provided but ending value is not provided then all hotels with price greater than or equal to start value will be listed
	c. If only ending value is provided but starting value is not provided then all hotels with price lesser than or equal to end value will be listed
	
4. Date range work independently of start and end which means:
	a. If starting date and ending date is provided then records will be filtered where dates come in between or equal to provided range.
    b. If only starting date is provided but ending date is not provided then all hotels with availability dates greater than or equal to starting date will be listed
	c. If only ending date is provided but starting date is not provided then all hotels with availability dates lesser than or equal to ending date will be listed

5. Validations for price range and date range is also applied so that they must not be provided in wrong manner
	
7. First Sort parameter is for hotel name mentioning ASC/DESC and second one is for price mentioning only ASC/DESC . All possible combinations of ordering are perfomred in code

8. Custom classes for exception handling is developed

9. All operations are perfomred by means of LINQ 

#########################################################
Delete Data:
#########################################################
1. I have written method to remove availability records after getting final result but not called in service 
since I assume that no need for removing sub array of availability
but in case it is required then it can be performed at the end of code of service. As of now it is commented
method is available in HotelBAL with name "RemoveIrrelevantDataFromAvailability" 

Test Case Matrix:
======================================================
1. I have tested service with multiple cases and matrix is availble in TestMatrix.xlsx file
	