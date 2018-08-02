using DataValidations.PhoneNumber.Contracts;
using DataValidations.PhoneNumber.Models.ServiceModels;
using DataValidations.PhoneNumber.Models;
using PhoneNumbers;
using System;
using System.Net;

namespace DataValidations.PhoneNumber.Services
{

     //  Integrate google libray  to Validate Phone
     //  https://github.com/googlei18n/libphonenumber 
     // Demo 
     // https://rawgit.com/googlei18n/libphonenumber/master/javascript/i18n/phonenumbers/demo-compiled.html

    public class PhoneService : IPhoneServcie
    {
        #region ValidatePhoneNumber
        
        public GenericResponse<ValidatePhoneNumberModel> ValidatePhoneNumber(string telephoneNumber, string countryCode)
        {
            //validate-phone-number? number = 00447825152591 & countryCode = GB

            GenericResponse<ValidatePhoneNumberModel> returnResult;


            if (string.IsNullOrEmpty(telephoneNumber))
            {


                string errorMessage = "Error : The string supplied did not seem to be a phone number";

                returnResult = new GenericResponse<ValidatePhoneNumberModel>()
                {
                    Message = errorMessage,
                    StatusCode = HttpStatusCode.BadRequest,
                    StatusMessage = "Failed"
                };

                //  throw new ArgumentException();

                return returnResult;

            }
            else if ((string.IsNullOrEmpty(countryCode)) || ((countryCode.Length != 2) && (countryCode.Length != 3)))
            {

                string errorMessage = "Error : Invalid country calling code";

                returnResult = new GenericResponse<ValidatePhoneNumberModel>()
                {
                    Message = errorMessage,
                    StatusCode = HttpStatusCode.BadRequest,
                    StatusMessage = "Failed"
                };

                return returnResult;
            }
            else
            {
                PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
                try
                {
                    PhoneNumbers.PhoneNumber phoneNumber = phoneUtil.Parse(telephoneNumber, countryCode);

                    bool isMobile = false;
                    bool isValidNumber = phoneUtil.IsValidNumber(phoneNumber); // returns true for valid number

                    bool isValidRegion = phoneUtil.IsValidNumberForRegion(phoneNumber, countryCode); // returns  w.r.t phone number region

                    string region = phoneUtil.GetRegionCodeForNumber(phoneNumber); // GB, US , PK

                    var numberType = phoneUtil.GetNumberType(phoneNumber); // Produces Mobile , FIXED_LINE

                    string phoneNumberType = numberType.ToString();

                    if (!string.IsNullOrEmpty(phoneNumberType) && phoneNumberType == "MOBILE")
                    {
                        isMobile = true;
                    }

                    var originalNumber = phoneUtil.Format(phoneNumber, PhoneNumberFormat.E164); // Produces "+447825152591"

                    var data = new ValidatePhoneNumberModel
                    {
                        FormattedNumber = originalNumber,
                        IsMobile = isMobile,
                        IsValidNumber = isValidNumber,
                        IsValidNumberForRegion = isValidRegion,
                        Region = region
                    };

                    returnResult = new GenericResponse<ValidatePhoneNumberModel>() { Data = data, StatusCode = HttpStatusCode.OK, StatusMessage = "Success" };

                }
                catch (NumberParseException ex)
                {

                    String errorMessage = "NumberParseException was thrown: " + ex.Message.ToString();


                    returnResult = new GenericResponse<ValidatePhoneNumberModel>()
                    {
                        Message = errorMessage,
                        StatusCode = HttpStatusCode.BadRequest,
                        StatusMessage = "Failed"
                    };


                }



                return returnResult;



            }
        }


        #endregion ValidatePhoneNumber


        #region FormatePhoneNumberForDisplay


        public GenericResponse<ValidatePhoneNumberModel> FormatePhoneNumberForDisplay(string telephoneNumber, string dialFrom)
        {
            ///format-number-for-display?number=075450175&countryCode=GB&dialFrom=US


            GenericResponse<ValidatePhoneNumberModel> returnResult;



            if (string.IsNullOrEmpty(telephoneNumber))
            {


                string errorMessage = "Error : The string supplied did not seem to be a phone number";

                returnResult = new GenericResponse<ValidatePhoneNumberModel>()
                {
                    Message = errorMessage,
                    StatusCode = HttpStatusCode.BadRequest,
                    StatusMessage = "Failed"
                };

                //  throw new ArgumentException();

                return returnResult;

            }
            else if ((string.IsNullOrEmpty(dialFrom)) || ((dialFrom.Length != 2) && (dialFrom.Length != 3)))
            {

                string errorMessage = "Error : Invalid country calling code";

                returnResult = new GenericResponse<ValidatePhoneNumberModel>()
                {
                    Message = errorMessage,
                    StatusCode = HttpStatusCode.BadRequest,
                    StatusMessage = "Failed"
                };

                return returnResult;
            }
            else
            {

                PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
                try
                {

                    PhoneNumbers.PhoneNumber phoneNumber = phoneUtil.Parse(telephoneNumber, dialFrom);



                    bool isMobile = false;
                    string displayNumber = string.Empty;

                    bool isValidNumber = phoneUtil.IsValidNumber(phoneNumber); // returns true for valid number

                    bool isValidRegion = phoneUtil.IsValidNumberForRegion(phoneNumber, dialFrom); // returns true  w.r.t phone number region

                    string region = phoneUtil.GetRegionCodeForNumber(phoneNumber); // GB, US , PK

                    var numberType = phoneUtil.GetNumberType(phoneNumber); // Produces Mobile , FIXED_LINE

                    string phoneNumberType = numberType.ToString();

                    if (!string.IsNullOrEmpty(phoneNumberType) && phoneNumberType == "MOBILE")
                    {
                        isMobile = true;
                    }

                    if (isValidRegion)
                    {
                        displayNumber = phoneUtil.Format(phoneNumber, PhoneNumberFormat.NATIONAL); // Produces 07825 152591

                    }
                    else
                    {
                        // Produces  International format: +44 7825 152591
                        //Out - of - country format from US: 011 44 7825 152591
                        //Out - of - country format from Switzerland: 00 44 7825 152591

                        displayNumber = phoneUtil.FormatOutOfCountryCallingNumber(phoneNumber, dialFrom);
                    }


                    var data = new ValidatePhoneNumberModel
                    {
                        FormattedNumber = displayNumber,
                        IsMobile = isMobile,
                        IsValidNumber = isValidNumber,
                        IsValidNumberForRegion = isValidRegion,
                        Region = region
                    };



                    returnResult = new GenericResponse<ValidatePhoneNumberModel>() { Data = data, StatusCode = HttpStatusCode.OK, StatusMessage = "Success" };


                }
                catch (NumberParseException ex)
                {

                    var errorMessage = "FormatePhoneNumberForDisplay was thrown: " + ex.Message.ToString();
                    Console.WriteLine(errorMessage);

                    returnResult = new GenericResponse<ValidatePhoneNumberModel>()
                    {
                        Message = errorMessage,
                        StatusCode = HttpStatusCode.BadRequest,
                        StatusMessage = "Failed"
                    };
                }

                return returnResult;

            }

        }


        #endregion FormatePhoneNumberForDisplay
    }
}
