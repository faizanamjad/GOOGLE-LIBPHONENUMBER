
using DataValidations.PhoneNumber.Models;
using DataValidations.PhoneNumber.Models.ServiceModels;
using System;

namespace DataValidations.PhoneNumber.Contracts
{
    public interface IPhoneServcie
    {
        GenericResponse<ValidatePhoneNumberModel> ValidatePhoneNumber(string telephoneNumber, string countryCode);

        GenericResponse<ValidatePhoneNumberModel> FormatePhoneNumberForDisplay(string telephoneNumber, string dialFrom);

    }
}
