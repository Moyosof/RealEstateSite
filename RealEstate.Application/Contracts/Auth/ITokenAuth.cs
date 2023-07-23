﻿using RealEstate.Domain.Entities.ReadOnly;
using RealEstate.DTO.WriteOnly.AuthDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Contracts.Auth
{
    public interface ITokenAuth
    {
        /// <summary>
        /// Add OTP and send Email to the sender
        /// </summary>
        /// <param name="oneTimeCodeDTO"></param>
        /// <returns></returns>
        Task AddOneTimeCodeToSender(OneTimeCodeDTO oneTimeCodeDTO, CancellationToken cancellationToken);

        Task<string> VerifyOneTimeCode(VerifyOneTimeCodeDTO verifyOneTimeCodeDTO);

        event Func<object, OneTimeCodeDTO, CancellationToken, Task> OneTimePasswordEmailEventhandler;
    }
}
