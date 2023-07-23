using RealEstate.Domain.Entities.ReadOnly;
using RealEstate.DTO.WriteOnly.AuthDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.EmailService
{
    public interface IFluentEmailClient
    {
        Task SendChangePasswordEmail(string userEmail);
        Task SendForgotPasswordEmail(ForgetPasswordDTO forgetPassword, string link);
        Task SendResetPasswordEmail(ResetPasswordDTO resetPassword);

        /// <summary>
        /// Sends email to the sender once the event is trigered
        /// </summary>
        /// <param name="source"></param>
        /// <param name="code"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SendOneTimeCodeEmail(Object source, OneTimeCodeDTO oneTimeCodeDTO, CancellationToken cancellationToken);
    }
}
