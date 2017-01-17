using MessageLogger.Api.Models;
using MessageLogger.Core.Dto;
using MessageLogger.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageLogger.Api.Helpers
{
    public class Mapper
    {
        /// <summary>
        /// Maps the LogRequestModel object to Log object.
        /// </summary>
        /// <param name="model">The LogRequestModel object to map.</param>
        /// <returns>Mapped Log object</returns>
        public static Log LogRequestModelToLog(LogRequestModel model)
        {
            return new Log
            {
                application_id = model.application_id,
                logger = model.logger,
                level = model.level,
                message = model.message
            };
        }

        /// <summary>
        /// Maps RegistrationDto object to RegisterResponseModel object.
        /// </summary>
        /// <param name="dto">The RegistrationDto object to map.</param>
        /// <returns></returns>
        public static RegisterResponseModel RegistrationDtoToRegisterResponseModel(RegistrationDto dto)
        {
            return new RegisterResponseModel
            {
                application_id = dto.application_id,
                display_name = dto.display_name,
                application_secret = dto.secret
            };
        }
    }
}