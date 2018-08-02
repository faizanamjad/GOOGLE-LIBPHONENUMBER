using System;
using System.Collections.Generic;
using System.Text;

namespace DataValidations.PhoneNumber.Models.ServiceModels
{
    public class ValidatePhoneNumberModel
    {
        public bool IsValidNumber { get; set; }

        public bool IsValidNumberForRegion { get; set; }

        public bool IsMobile { get; set; }

        public string Region { get; set; }

        public string FormattedNumber { get; set; }

    }
}
