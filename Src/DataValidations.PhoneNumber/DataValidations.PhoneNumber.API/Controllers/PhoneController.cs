using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataValidations.PhoneNumber.Contracts;
using DataValidations.PhoneNumber.API.Filters;
using DataValidations.PhoneNumber.Models.ServiceModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataValidations.PhoneNumber.Models;

namespace DataValidations.PhoneNumber.API.Controllers
{
    [Produces("application/json")]
    [Throttling]
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneController : ControllerBase
    {

        private IPhoneServcie phoneService; 

        public PhoneController(IPhoneServcie _phoneService)
        {
            phoneService = _phoneService;
        }


        /// <summary>
        /// Verify Validation of Phone Number 
        /// </summary>
        /// <param name="telephoneNumber"> any formate</param>
        /// <param name="countryCode ISO2">  /param>
        /// <returns></returns>

        // GET: Phone/ValidatePhoneNumber/{telephoneNumber}/{countryCode}
        [Route("ValidatePhoneNumber/{telephoneNumber}/{countryCode}")]
        [HttpGet]
        public ActionResult<GenericResponse<ValidatePhoneNumberModel>> ValidatePhoneNumber(string telephoneNumber, string countryCode)
        {
            var result = phoneService.ValidatePhoneNumber(telephoneNumber, countryCode);
            return result;
        }



        /// <summary>
        /// Formate Phone Number according to calling country 
        /// </summary>
        /// <param name="telephoneNumber"> any formate</param>
        /// <param name="dialFrom ISO2">  </param>
        /// <returns></returns>

        // GET: Phone/FormatePhoneNumber/{telephoneNumber}/{dialFrom}
        [Route("FormatePhoneNumber/{telephoneNumber}/{dialFrom}")]
        [HttpGet]
        public ActionResult<GenericResponse<ValidatePhoneNumberModel>> FormatePhoneNumber(string telephoneNumber, string dialFrom)
        {
            var result = phoneService.FormatePhoneNumberForDisplay(telephoneNumber, dialFrom);
            return result;
        }


       
    }
}
