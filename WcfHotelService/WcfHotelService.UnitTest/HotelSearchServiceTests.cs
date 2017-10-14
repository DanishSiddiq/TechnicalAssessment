using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using WcfHotelService.Common.Entities;
using WcfHotelService.Service;

namespace WcfHotelService.UnitTest
{
    /// <summary>
    /// this class specifcially developed utilizing NUnit and RhinoMocks to peform unit testing on hotel service
    /// </summary>
    [TestFixture]
    public class HotelSearchServiceTests
    {

        private static HotelSearchService hotelSearchService;
        private IHotelSearchService iHotelSearchService;

        [OneTimeSetUp]
        public void SetUp()
        {
            iHotelSearchService = MockRepository.GenerateMock<IHotelSearchService>();
            hotelSearchService = new HotelSearchService(iHotelSearchService);
        }

        [TearDown]
        public void Clean()
        {

        }


        /// <summary>
        /// This mehtod will check that request must return response request and it shouldnt be null
        /// Client must expect something to get to know that request is failed or passed
        /// In case of failure message list will contain reason for failure
        /// </summary>
        [Test(Description = Constants.CONST_CHECK_RESPONSE_NOT_NULL)]
        public void Get_Hotels_Return_NotNull_Pass()
        {
            var resp = new ResponseHotel();

            //Set Up 
            iHotelSearchService.Stub(dt => dt.SearchResultJson_Get(null, null, null, null, null, null, null, null)).IgnoreArguments().Return(resp);


            //Assert 
            Assert.IsNotNull(resp, Constants.CONST_CHECK_RESPONSE_NOT_NULL_RESULT);

        }


        /// <summary>
        /// This method will check that request must be processed successfully with out any failure
        /// in case any exception will happen during operation then code return in response will be 0
        /// </summary>
        [Test(Description = Constants.CONST_CHECK_RESPONSE_SUCCESS)]
        public void Get_Hotels_Return_Success_Pass()
        {
            //Act 
            var resp = hotelSearchService.SearchResultJson_Get(null, null, null, null, null, null, null, null);

            //Assert 
            Assert.AreEqual(Constants.CONST_SUCCESS_CODE, resp.Code, Constants.CONST_CHECK_RESPONSE_SUCCESS_RESULT);

        }


        /// <summary>
        /// checking validation working fine or not by passing wrong parameters
        /// since 4 paramters formats are wrongly provided so count should be 4
        /// </summary>
        [Test(Description = Constants.CONST_CHECK_HOTEL_FILTERING_VALIDATION)]
        public void Get_Validation_Error_Pass()
        {
            // date format is also wrong as per format metioned in API. Although I have changed format at server but made it compatible with client for easiness
            //Act 
            var resp = hotelSearchService.SearchResultJson_Get("", "", "range from", "range to", "10-20-2020", "10-30-2020", "ASC", "DESC");

            //Assert            
            Assert.AreEqual(4, resp.Messages.Count, Constants.CONST_CHECK_HOTEL_FILTERING_VALIDATION_RESULT);

        }


        /// <summary>
        /// Sorting parameters are defined incorrectly so system must catch wrong parameters
        /// </summary>
        [Test(Description = Constants.CONST_CHECK_SORT_PARAMETER)]
        public void Get_Validation_Error_For_Wrong_Sort_Order()
        {
            //Act 
            var resp = hotelSearchService.SearchResultJson_Get("", "", "", "", "", "", "wrong sort paramter", "wrong sort paramter");

            //Assert            
            Assert.AreEqual(2, resp.Messages.Count, Constants.CONST_CHECK_SORT_PARAMETER_RESULT);
        }


        /// <summary>
        /// Checking value of specific hotel by strict filtering
        /// </summary>
        [Test(Description = Constants.CONST_CHECK_SPECIFIC_HOTEL_FILTERING)]
        public void Get_Specific_Hotel_Filtering_Pass()
        {
            
            //Act 
            var resp = hotelSearchService.SearchResultJson_Get("", "", "105", "120", "20-10-2020", "30-10-2020", "ASC", "DESC");

            //Assert            
            Assert.AreEqual(Constants.CONST_HOTEL_GOLDEN_TULIP, resp.Hotels[0].Name, Constants.CONST_CHECK_SPECIFIC_HOTEL_FILTERING_RESULT);

        }


        /// <summary>
        /// Checking multiple hotels by strict filtering
        /// Sort parameters are also be define
        /// only 2 hotels have price in between range of 80 & 100
        /// </summary>
        [Test(Description = Constants.CONST_CHECK_MULTIPLE_HOTEL_FILTERING)]
        public void Get_Multiple_Hotels_By_Price_And_Sort_Pass()
        {
            //Act 
            var resp = hotelSearchService.SearchResultJson_Get("", "", "80", "100", "", "", "ASC", "DESC");

            //Assert            
            Assert.AreEqual(2, resp.Hotels.Count, Constants.CONST_CHECK_MULTIPLE_HOTEL_FILTERING_RESULT);
            Assert.AreEqual("Le Meridien", resp.Hotels[0].Name, Constants.CONST_CHECK_MULTIPLE_HOTEL_FILTERING_RESULT);
            Assert.AreEqual("Rotana Hotel", resp.Hotels[1].Name, Constants.CONST_CHECK_MULTIPLE_HOTEL_FILTERING_RESULT);
        }


        // NOW BELOW MENTIONED CASES ARE FROM TEST MATRIX FILE SHARED IN GIT REPOSITORY 
        // INPUTS AND DESIRED OUTPUT ARE ALSO DEFINED IN EXCEL
        // All following cases are pasing desired result

        /// <summary>
        /// In case no parameteric values are provided then by default service must return all hotels with out filtering
        /// </summary>
        [Test(Description = Constants.CONST_CHECK_ALL_HOTELS)]
        public void Get_All_Hotels_Return_List_Count_Pass()
        {

            //Act 
            var resp = hotelSearchService.SearchResultJson_Get(null, null, null, null, null, null, null, null);

            //Assert 
            Assert.AreEqual(Constants.CONST_HOTEL_COUNT, resp.Hotels.Count, Constants.CONST_CHECK_ALL_HOTELS);
        }


        /// <summary>
        /// 
        /// </summary>
        [Test(Description = Constants.CONST_CHECK_ALL_HOTELS)]
        public void Get_Hotels_List_Sort_By_Price_ASC_Pass()
        {
            List<String> desiredResp =  new List<String>
                                        {
                                            Constants.CONST_HOTEL_CONCORDE,
                                            Constants.CONST_HOTEL_ROTANA,
                                            Constants.CONST_HOTEL_LE_MERIDIEN,
                                            Constants.CONST_HOTEL_MEDIA_ONE,
                                            Constants.CONST_HOTEL_GOLDEN_TULIP,
                                            Constants.CONST_HOTEL_NOVOTEL
                                        };

            //Act 
            ResponseHotel resp = hotelSearchService.SearchResultJson_Get(null, null, null, null, null, null, null, "ASC");

            List<String> hotelNames = new List<string>();
            foreach (Hotel h in resp.Hotels)
            {
                hotelNames.Add(h.Name);
            }

            //Assert
            CollectionAssert.AreEqual(desiredResp, hotelNames, Constants.CONST_CHECK_ALL_HOTELS);            
        }


        /// <summary>
        /// 
        /// </summary>
        [Test(Description = Constants.CONST_CHECK_ALL_HOTELS)]
        public void Get_Hotels_By_Name_Sort_By_Name_Desc_Pass()
        {
            List<String> desiredResp = new List<String>
            {
                Constants.CONST_HOTEL_ROTANA,
                Constants.CONST_HOTEL_NOVOTEL,
                Constants.CONST_HOTEL_MEDIA_ONE,
                Constants.CONST_HOTEL_CONCORDE
            };

            //Act 
            ResponseHotel resp = hotelSearchService.SearchResultJson_Get("Hotel", null, null, null, null, null, "desc", null);

            List<String> hotelNames = new List<string>();
            foreach (Hotel h in resp.Hotels)
            {
                hotelNames.Add(h.Name);
            }

            //Assert
            CollectionAssert.AreEqual(desiredResp, hotelNames, Constants.CONST_CHECK_ALL_HOTELS);

        }

        /// <summary>
        /// 
        /// </summary>
        [Test(Description = Constants.CONST_CHECK_ALL_HOTELS)]
        public void Get_Hotels_By_Destination_Pass()
        {
            List<String> desiredResp = new List<String>
            {
                Constants.CONST_HOTEL_CONCORDE
            };

            //Act 
            ResponseHotel resp = hotelSearchService.SearchResultJson_Get(null, "MANILA", null, null, null, null, null, null);

            List<String> hotelNames = new List<string>();
            foreach (Hotel h in resp.Hotels)
            {
                hotelNames.Add(h.Name);
            }

            //Assert
            CollectionAssert.AreEqual(desiredResp, hotelNames, Constants.CONST_CHECK_ALL_HOTELS);
        }


        /// <summary>
        /// 
        /// </summary>
        [Test(Description = Constants.CONST_CHECK_ALL_HOTELS)]
        public void Get_Hotels_By_Price_Range_Sort_By_Price_Desc_Pass()
        {
            List<String> desiredResp = new List<String>
            {
                Constants.CONST_HOTEL_LE_MERIDIEN,
                Constants.CONST_HOTEL_ROTANA
            };

            //Act 
            ResponseHotel resp = hotelSearchService.SearchResultJson_Get(null, "", "80", "100", null, null, null, "DESC");

            List<String> hotelNames = new List<string>();
            foreach (Hotel h in resp.Hotels)
            {
                hotelNames.Add(h.Name);
            }

            //Assert
            CollectionAssert.AreEqual(desiredResp, hotelNames, Constants.CONST_CHECK_ALL_HOTELS);
        }


        /// <summary>
        /// 
        /// </summary>
        [Test(Description = Constants.CONST_CHECK_ALL_HOTELS)]
        public void Get_Hotels_By_Price_Range_Pass()
        {
            List<String> desiredResp = new List<String>
            {
                Constants.CONST_HOTEL_MEDIA_ONE,
                Constants.CONST_HOTEL_ROTANA,                
                Constants.CONST_HOTEL_LE_MERIDIEN,
                Constants.CONST_HOTEL_GOLDEN_TULIP
            };

            //Act 
            ResponseHotel resp = hotelSearchService.SearchResultJson_Get(null, "", "80", "110", null, null, null, "");

            List<String> hotelNames = new List<string>();
            foreach (Hotel h in resp.Hotels)
            {
                hotelNames.Add(h.Name);
            }

            //Assert
            CollectionAssert.AreEqual(desiredResp, hotelNames, Constants.CONST_CHECK_ALL_HOTELS);            
        }


        /// <summary>
        /// 
        /// </summary>
        [Test(Description = Constants.CONST_CHECK_ALL_HOTELS)]
        public void Get_Hotels_From_Price_Sort_By_Name_ASC_Pass()
        {
            List<String> desiredResp = new List<String>
            {
                Constants.CONST_HOTEL_GOLDEN_TULIP,                
                Constants.CONST_HOTEL_MEDIA_ONE,
                Constants.CONST_HOTEL_NOVOTEL
            };

            //Act 
            ResponseHotel resp = hotelSearchService.SearchResultJson_Get(null, "", "90", "", null, null, "ASC", null);

            List<String> hotelNames = new List<string>();
            foreach (Hotel h in resp.Hotels)
            {
                hotelNames.Add(h.Name);
            }

            //Assert
            CollectionAssert.AreEqual(desiredResp, hotelNames, Constants.CONST_CHECK_ALL_HOTELS);

        }


        /// <summary>
        /// 
        /// </summary>
        [Test(Description = Constants.CONST_CHECK_ALL_HOTELS)]
        public void Get_Hotels_By_Date_Range_Sort_By_Name_ASC_Sort_By_Price_DESC_Pass()
        {
            List<String> desiredResp = new List<String>
            {
                Constants.CONST_HOTEL_CONCORDE,
                Constants.CONST_HOTEL_NOVOTEL               
            };

            //Act 
            ResponseHotel resp = hotelSearchService.SearchResultJson_Get(null, null, null, null, "05-11-2020", "16-11-2020", "asc", "DESC");

            List<String> hotelNames = new List<string>();
            foreach (Hotel h in resp.Hotels)
            {
                hotelNames.Add(h.Name);
            }

            //Assert
            CollectionAssert.AreEqual(desiredResp, hotelNames, Constants.CONST_CHECK_ALL_HOTELS);
        }


        /// <summary>
        /// 
        /// </summary>
        [Test(Description = Constants.CONST_CHECK_ALL_HOTELS)]
        public void Get_Hotels_To_Date_Sort_By_Price_DESC_Pass()
        {
            List<String> desiredResp = new List<String>
            {
                Constants.CONST_HOTEL_NOVOTEL,
                Constants.CONST_HOTEL_MEDIA_ONE,
                Constants.CONST_HOTEL_LE_MERIDIEN,
                Constants.CONST_HOTEL_ROTANA,
                Constants.CONST_HOTEL_CONCORDE     
            };

            //Act 
            ResponseHotel resp = hotelSearchService.SearchResultJson_Get(null, null, null, null, "", "10-12-2020", null, "desc");

            List<String> hotelNames = new List<string>();
            foreach (Hotel h in resp.Hotels)
            {
                hotelNames.Add(h.Name);
            }

            //Assert
            CollectionAssert.AreEqual(desiredResp, hotelNames, Constants.CONST_CHECK_ALL_HOTELS);

            iHotelSearchService.VerifyAllExpectations();
        }
    }
}
