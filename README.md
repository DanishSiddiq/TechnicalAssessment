# TechnicalAssessment



Assumptions In Development:
======================================================
1. ALl parameters are considered as optional based in user discretion to provide or not
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
	
6. I have written method to remove availability records after getting final result but not called in method since I assume that no need for removing sub array
but in case it is required then it can be performed at the end of code. As of now it is commented

7. First Sort parameter is for hotel name mentioning ASC/DESC and second one is for price mentioning only ASC/DESC . All possible combinations of ordering are perfomred in code
8. Custom classes for exception handling is developed
9. ALl operations are perfomred by means of LINQ 


Test Case Matrix:
======================================================
1. I have tested service with multiple cases and matrix is availble in TestMatrix.xlsx file
	
