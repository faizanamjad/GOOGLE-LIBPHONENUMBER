using DataValidations.PhoneNumber.API.Controllers;
using DataValidations.PhoneNumber.Contracts;
using DataValidations.PhoneNumber.Models;
using DataValidations.PhoneNumber.Models.ServiceModels;
using DataValidations.PhoneNumber.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataValidations.PhoneNumber.Tests
{
   [TestClass]
   public class PhoneTest
    {

        private Mock<IPhoneServcie> mockPhoneService;


        [TestInitialize]
        public void InitializeTest()
        {
            mockPhoneService = new Mock<IPhoneServcie>();
        }



        #region Controller Calls Tests

        // Controller calls Test

        // <summary>
        /// for action "api/Phone/ValidatePhoneNumber/"
        /// </summary>

        [TestMethod]
        public void TestValidatePhoneNumber()
        {

            //Arrange
            string telephone = "923336323900";
            string countryCode = "PK";

            mockPhoneService.Setup(x => x.ValidatePhoneNumber(telephone, countryCode)).Returns(new GenericResponse<ValidatePhoneNumberModel>());

            //Act
            PhoneController phoneController = new PhoneController(mockPhoneService.Object);
            var result = phoneController.ValidatePhoneNumber(telephone,countryCode);

            //Assert
            mockPhoneService.Verify(x => x.ValidatePhoneNumber(telephone,countryCode), Times.AtMostOnce);
        }


        // <summary>
        /// for action "api/Phone/FormatePhoneNumber/"
        /// </summary>

        [TestMethod]
        public void TestFormatePhoneNumber()
        {

            //Arrange
            string telephone = "+923336323900";
            string dialFrom = "PK";

            mockPhoneService.Setup(x => x.FormatePhoneNumberForDisplay(telephone, dialFrom)).Returns(new GenericResponse<ValidatePhoneNumberModel>());

            //Act
            PhoneController phoneController = new PhoneController(mockPhoneService.Object);
            var result = phoneController.FormatePhoneNumber(telephone, dialFrom);

            //Assert
            mockPhoneService.Verify(x => x.ValidatePhoneNumber(telephone, dialFrom), Times.AtMostOnce);
        }

        #endregion Controller Calls Tests


        #region Method Calls Test

        [TestMethod]
        public void ValidatePhoneNumber()
        {

            //Arrange
            string telephone = "0333 6323990";
            string countryCode = "PK";
            bool expectedResult = true;

            mockPhoneService.Setup(x => x.ValidatePhoneNumber(telephone, countryCode)).Returns(new GenericResponse<ValidatePhoneNumberModel>());

            //Act
            PhoneService phoneServcie = new PhoneService();
            var result = phoneServcie.ValidatePhoneNumber(telephone, countryCode);

            //Assert 
            Assert.AreEqual(expectedResult, result.Data.IsValidNumber);

            Assert.AreEqual(expectedResult, result.Data.IsMobile);

            Assert.AreEqual(expectedResult, result.Data.IsValidNumberForRegion);

            Assert.AreEqual("PK", result.Data.Region);

            Assert.AreEqual("+923336323990", result.Data.FormattedNumber);
        }


        [TestMethod]
        public void FormatePhoneNumberLocal()
        {
            //Arrange
            string telephone = "+923336323900";
            string dialFrom = "PK";
            string expectedResult = "0333 6323900";

            mockPhoneService.Setup(x => x.FormatePhoneNumberForDisplay(telephone, dialFrom)).Returns(new GenericResponse<ValidatePhoneNumberModel>());

            //Act
            PhoneService phoneServcie = new PhoneService();
            var result = phoneServcie.FormatePhoneNumberForDisplay(telephone, dialFrom);

            //Assert 
            
            Assert.AreEqual(expectedResult, result.Data.FormattedNumber);
        }



        [TestMethod]
        public void FormatePhoneNumberInternational()
        {
            //Arrange
            string telephone = "00923336323900";
            string dialFrom = "GB";

            string expectedResult = "00 92 333 6323900";

            mockPhoneService.Setup(x => x.FormatePhoneNumberForDisplay(telephone, dialFrom)).Returns(new GenericResponse<ValidatePhoneNumberModel>());

            //Act
            PhoneService phoneServcie = new PhoneService();
            var result = phoneServcie.FormatePhoneNumberForDisplay(telephone, dialFrom);

            //Assert 

            Assert.AreEqual(false, result.Data.IsValidNumberForRegion);

            Assert.AreEqual("PK", result.Data.Region);

            Assert.AreEqual(expectedResult, result.Data.FormattedNumber);

        }


        [TestMethod]
        public void FormatePhoneNumberForUS()
        {
            //Arrange
            string telephone = "+923336323900";
            string dialFrom = "US";

            string expectedResult = "011 92 333 6323900";

            mockPhoneService.Setup(x => x.FormatePhoneNumberForDisplay(telephone, dialFrom)).Returns(new GenericResponse<ValidatePhoneNumberModel>());

            //Act
            PhoneService phoneServcie = new PhoneService();
            var result = phoneServcie.FormatePhoneNumberForDisplay(telephone, dialFrom);

            //Assert 
           
            Assert.AreEqual(expectedResult, result.Data.FormattedNumber);

        }



        #endregion Method Calls Test
    }
}
