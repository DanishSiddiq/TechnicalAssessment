using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using WcfHotelService.Common.Entities;
using WcfHotelService.Service;

namespace WcfHotelService.UnitTest
{
    /// <summary>
    /// this class specifcially developed utilizing NUnit and RhinoMocks to pefome unit testing on hotel service
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

        [Test(Description = Constants.CONST_CHECK_RESPONSE_NOT_NULL)]
        public void GetHotels_Return_NotNull_Pass()
        {
            var resp = new ResponseHotel { Hotels = new List<Hotel>(), Code = 1, Messages = new List<CustomMessage>()};            

            //Set Up 
            iHotelSearchService.Stub(dt => dt.SearchResultJson_Get(null, null, null, null, null, null, null, null)).IgnoreArguments().Return(resp);

            //Act 
            resp = hotelSearchService.SearchResultJson_Get(null, null, null, null, null, null, null, null);

            //Assert 
            Assert.IsNotNull(resp, Constants.CONST_CHECK_RESPONSE_NOT_NULL_RESULT);

        }

        [Test(Description = Constants.CONST_CHECK_ALL_HOTELS)]
        public void GetAllHotels_Return_List_Count_Pass()
        {

            //Act 
            var resp = hotelSearchService.SearchResultJson_Get(null, null, null, null, null, null, null, null);

            //Assert 
            Assert.AreEqual(Constants.CONST_HOTEL_COUNT, resp.Hotels.Count, Constants.CONST_CHECK_ALL_HOTELS);


            iHotelSearchService.VerifyAllExpectations();
        }

    }
}
